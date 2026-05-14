using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Servicos : Form
    {
        private string connectionString;
        private int idSelecionado = 0;

        public Servicos(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            // Configurar NumericUpDown do Tempo (Minutos)
            numTempo.Minimum = 1;
            numTempo.Maximum = 1440; // Máximo 1 dia

            // Configurar NumericUpDown da Quantidade (em ml)
            numQtd.DecimalPlaces = 0; // Trabalhamos com ml inteiros
            numQtd.Minimum = 1;       // Mínimo de 1 ml
            numQtd.Maximum = 10000;   // Máximo de 10 Litros
            numQtd.Increment = 5;     // Saltos de 5 em 5 ml
            numQtd.Value = 15;        // Valor padrão ao abrir

            ConfigurarGrelhaConsumo();

            // Eventos Principais
            this.Load += (s, e) => { CarregarServicos(); CarregarProdutos(); AlternarModoProdutos(false); };
            btnGuardar.Click += btnGuardar_Click;
            btnNovo.Click += btnNovo_Click;
            btnEliminar.Click += btnEliminar_Click;
            dgvServicos.CellClick += dgvServicos_CellClick;
            txtPesquisa.TextChanged += (s, e) => CarregarServicos(txtPesquisa.Text);

            // Botões dos Produtos
            btnAdicionarProduto.Click += btnAdicionarProduto_Click;
            // Se o botão não ligar automaticamente, descomenta a linha abaixo:
            // btnRemoverProduto.Click += btnRemoverProduto_Click; 
        }

        private void ConfigurarGrelhaConsumo()
        {
            dgvConsumo.Columns.Clear();

            // Coluna 0: ID do Produto (Texto) - Fica invisível
            dgvConsumo.Columns.Add("ID_Produto", "Referência");
            dgvConsumo.Columns["ID_Produto"].Visible = false;

            // Coluna 1: Nome do Produto
            dgvConsumo.Columns.Add("Nome_Produto", "Produto Usado");
            dgvConsumo.Columns["Nome_Produto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Coluna 2: Quantidade Gasta em ml
            dgvConsumo.Columns.Add("Qtd_Gasta", "Qtd. Gasta (ml)");
            dgvConsumo.Columns["Qtd_Gasta"].Width = 120;

            dgvConsumo.AllowUserToAddRows = false;
            dgvConsumo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsumo.ReadOnly = true;
            dgvConsumo.MultiSelect = false; // Só deixa selecionar um produto de cada vez para remover
        }

        // Função que liga/desliga a secção de produtos (só podes adicionar se o serviço já estiver guardado)
        private void AlternarModoProdutos(bool ativar)
        {
            cbProdutos.Enabled = ativar;
            numQtd.Enabled = ativar;
            btnAdicionarProduto.Enabled = ativar;
            // Descomenta se o teu botão se chamar btnRemoverProduto
            // btnRemoverProduto.Enabled = ativar; 
            dgvConsumo.Enabled = ativar;
        }

        private void CarregarServicos(string termo = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = @"SELECT Cod_Servico, Nome_Servico, Tempo_Minutos, Preco_Avulso 
                                 FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL 
                                 WHERE Nome_Servico LIKE @p OR CAST(Cod_Servico AS VARCHAR) LIKE @p";

                    SqlDataAdapter da = new SqlDataAdapter(sql, CN);
                    da.SelectCommand.Parameters.AddWithValue("@p", "%" + termo.Trim() + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvServicos.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Erro a carregar serviços: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void CarregarProdutos()
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Ref_Produto, Nome FROM DETAIL_AUTOMOVEL.PRODUTO", CN);
                    SqlDataReader r = cmd.ExecuteReader();

                    cbProdutos.Items.Clear();
                    while (r.Read())
                    {
                        cbProdutos.Items.Add(new ComboItemString
                        {
                            Id = r["Ref_Produto"].ToString(),
                            Texto = r["Nome"].ToString()
                        });
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro a carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeServico.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("O nome do serviço e o preço são obrigatórios!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precoTratado = 0;
            string txtP = txtPreco.Text.Replace(" €", "").Replace(",", ".").Trim();
            decimal.TryParse(txtP, NumberStyles.Any, CultureInfo.InvariantCulture, out precoTratado);

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    int idFinal = (idSelecionado == 0) ? new Random().Next(100, 9999) : idSelecionado;

                    // AGORA GRAVA APENAS OS DADOS BASE DO SERVIÇO (Os produtos gravam sozinhos)
                    string sql = idSelecionado == 0
                        ? "INSERT INTO DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL (Cod_Servico, Nome_Servico, Tempo_Minutos, Preco_Avulso) VALUES (@id, @nome, @tempo, @preco)"
                        : "UPDATE DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL SET Nome_Servico=@nome, Tempo_Minutos=@tempo, Preco_Avulso=@preco WHERE Cod_Servico=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idFinal);
                        cmd.Parameters.AddWithValue("@nome", txtNomeServico.Text);
                        cmd.Parameters.AddWithValue("@tempo", (int)numTempo.Value);
                        cmd.Parameters.AddWithValue("@preco", precoTratado);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados do Serviço guardados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Se era um serviço novo, atualiza a variável e ativa a secção de produtos
                    if (idSelecionado == 0)
                    {
                        idSelecionado = idFinal;
                        txtCodServico.Text = idSelecionado.ToString();
                        AlternarModoProdutos(true);
                    }

                    CarregarServicos(txtPesquisa.Text);
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar dados: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvServicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvServicos.Rows[e.RowIndex].Cells["Cod_Servico"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvServicos.Rows[e.RowIndex];
                idSelecionado = Convert.ToInt32(r.Cells["Cod_Servico"].Value);

                txtCodServico.Text = idSelecionado.ToString();
                txtNomeServico.Text = r.Cells["Nome_Servico"].Value.ToString();
                numTempo.Value = Convert.ToInt32(r.Cells["Tempo_Minutos"].Value);
                txtPreco.Text = Convert.ToDecimal(r.Cells["Preco_Avulso"].Value).ToString("0.00") + " €";

                // Ativa a inserção de produtos porque já temos um serviço selecionado
                AlternarModoProdutos(true);

                // Carrega a "receita" de produtos na grelha de baixo
                CarregarConsumoDoServico(idSelecionado);
            }
        }

        private void CarregarConsumoDoServico(int idServico)
        {
            dgvConsumo.Rows.Clear();
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = @"SELECT C.Ref_Produto, P.Nome, C.Qtd_Gasta 
                                   FROM DETAIL_AUTOMOVEL.SERVICO_PRODUTO C
                                   JOIN DETAIL_AUTOMOVEL.PRODUTO P ON C.Ref_Produto = P.Ref_Produto
                                   WHERE C.Cod_Servico = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idServico);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                dgvConsumo.Rows.Add(r["Ref_Produto"].ToString(), r["Nome"].ToString(), r["Qtd_Gasta"]);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar consumos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // =========================================================
        // GESTÃO AUTOMÁTICA DE PRODUTOS (ADICIONAR E REMOVER DA BD)
        // =========================================================

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            // Validação de Segurança
            if (idSelecionado == 0)
            {
                MessageBox.Show("Guarde o Serviço primeiro clicando em 'GUARDAR', antes de lhe adicionar produtos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbProdutos.SelectedItem is ComboItemString prod && numQtd.Value > 0)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        // 1. Grava na BD Automaticamente
                        string sqlConsumo = "INSERT INTO DETAIL_AUTOMOVEL.SERVICO_PRODUTO (Cod_Servico, Ref_Produto, Qtd_Gasta) VALUES (@srv, @prod, @qtd)";
                        using (SqlCommand cmdC = new SqlCommand(sqlConsumo, CN))
                        {
                            cmdC.Parameters.AddWithValue("@srv", idSelecionado);
                            cmdC.Parameters.AddWithValue("@prod", prod.Id);
                            cmdC.Parameters.AddWithValue("@qtd", numQtd.Value);
                            cmdC.ExecuteNonQuery();
                        }

                        // 2. Limpa os campos
                        cbProdutos.SelectedIndex = -1;
                        numQtd.Value = 15;

                        // 3. Atualiza a Grelha
                        CarregarConsumoDoServico(idSelecionado);
                    }
                    catch (SqlException ex)
                    {
                        // Se der erro de PRIMARY KEY (2627), significa que o produto já está lá
                        if (ex.Number == 2627) MessageBox.Show("Este produto já foi adicionado! Remova-o se quiser alterar a quantidade.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else MessageBox.Show("Erro SQL ao adicionar: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto e indique uma quantidade válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // NOVO BOTÃO DE REMOVER PRODUTO
        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 || dgvConsumo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um produto na lista abaixo para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string refProd = dgvConsumo.SelectedRows[0].Cells["ID_Produto"].Value.ToString();
            string nomeProd = dgvConsumo.SelectedRows[0].Cells["Nome_Produto"].Value.ToString();

            if (MessageBox.Show($"Tem a certeza que deseja remover o produto '{nomeProd}' deste serviço?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        // Apaga diretamente na BD
                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.SERVICO_PRODUTO WHERE Cod_Servico = @srv AND Ref_Produto = @prod";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@srv", idSelecionado);
                            cmd.Parameters.AddWithValue("@prod", refProd);
                            cmd.ExecuteNonQuery();
                        }

                        // Atualiza a grelha para refletir a remoção
                        CarregarConsumoDoServico(idSelecionado);
                    }
                    catch (Exception ex) { MessageBox.Show("Erro ao remover: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        // =========================================================

        private void btnNovo_Click(object sender, EventArgs e)
        {
            idSelecionado = 0;
            txtCodServico.Clear();
            txtNomeServico.Clear();
            numTempo.Value = 30;
            txtPreco.Clear();

            // Limpa a grelha e desativa os produtos até o novo serviço ser guardado
            dgvConsumo.Rows.Clear();
            AlternarModoProdutos(false);

            txtNomeServico.Focus();
            dgvServicos.ClearSelection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0) return;

            if (MessageBox.Show("Tem a certeza que deseja eliminar este serviço e a sua receita de produtos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        // Apaga primeiro os produtos ligados a este serviço
                        new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.SERVICO_PRODUTO WHERE Cod_Servico = {idSelecionado}", CN).ExecuteNonQuery();

                        // Apaga o serviço principal
                        SqlCommand cmd = new SqlCommand("DELETE FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL WHERE Cod_Servico = @id", CN);
                        cmd.Parameters.AddWithValue("@id", idSelecionado);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Serviço eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarServicos(txtPesquisa.Text);
                        btnNovo_Click(null, null);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) MessageBox.Show("Não pode eliminar este serviço porque já foi utilizado em Agendamentos ou Packs.", "Bloqueio", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        else MessageBox.Show("Erro SQL: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e) => Navegacao.Voltar(this);

        private void Servicos_Load(object sender, EventArgs e)
        {

        }
    }

    public class ComboItemString
    {
        public string Id { get; set; }
        public string Texto { get; set; }
        public override string ToString() => Texto;
    }
}