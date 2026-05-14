using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Clientes : Form
    {
        private string connectionString;

        public Clientes(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            this.Load += new EventHandler(Ecra_Load);
            btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
            txtPesquisa.TextChanged += new EventHandler(txtPesquisa_TextChanged);
        }

        private void CarregarTabela(string termoPesquisa = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string query = @"SELECT C.NIF_Cliente, C.Nome, C.Contacto, C.Email, 
                        V.Matricula, V.Marca, V.Modelo, V.Cor,
                        A.Tipo_Automovel, A.Tipo_Estofos, A.Num_Lugares,
                        M.Tipo_Motociclo, M.Cilindrada
                 FROM DETAIL_AUTOMOVEL.CLIENTE C 
                 LEFT JOIN DETAIL_AUTOMOVEL.VEICULO V ON C.NIF_Cliente = V.NIF_Cliente 
                 LEFT JOIN DETAIL_AUTOMOVEL.AUTOMOVEL A ON V.Matricula = A.Matricula
                 LEFT JOIN DETAIL_AUTOMOVEL.MOTOCICLO M ON V.Matricula = M.Matricula
                 WHERE C.Nome LIKE @pesquisa 
                    OR C.NIF_Cliente LIKE @pesquisa 
                    OR CAST(C.Contacto AS VARCHAR) LIKE @pesquisa 
                    OR V.Matricula LIKE @pesquisa";

                    using (SqlCommand cmd = new SqlCommand(query, CN))
                    {
                        cmd.Parameters.AddWithValue("@pesquisa", "%" + termoPesquisa + "%");
                        SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                        DataTable tabelaVirtual = new DataTable();
                        adaptador.Fill(tabelaVirtual);
                        dgvClientes.DataSource = tabelaVirtual;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar: " + ex.Message); }
            }
        }

        private void Ecra_Load(object sender, EventArgs e) => CarregarTabela("");
        private void btnPesquisar_Click(object sender, EventArgs e) => CarregarTabela(txtPesquisa.Text);
        private void txtPesquisa_TextChanged(object sender, EventArgs e) => CarregarTabela(txtPesquisa.Text);
        private void btnPesquisar_Click_1(object sender, EventArgs e) => CarregarTabela(txtPesquisa.Text);

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIF.Text) || string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha pelo menos o Nome e o NIF do cliente.");
                return;
            }

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    // Inicia uma transação para garantir que ou grava tudo ou não grava nada
                    SqlTransaction trans = CN.BeginTransaction();

                    try
                    {
                        // 1. Gravar/Atualizar Cliente
                        string queryCliente = @"IF EXISTS (SELECT 1 FROM DETAIL_AUTOMOVEL.CLIENTE WHERE NIF_Cliente = @NIF)
                                        UPDATE DETAIL_AUTOMOVEL.CLIENTE SET Nome = @Nome, Contacto = @Contacto, Email = @Email WHERE NIF_Cliente = @NIF
                                        ELSE INSERT INTO DETAIL_AUTOMOVEL.CLIENTE (NIF_Cliente, Nome, Contacto, Email) VALUES (@NIF, @Nome, @Contacto, @Email)";

                        using (SqlCommand cmdCliente = new SqlCommand(queryCliente, CN, trans))
                        {
                            cmdCliente.Parameters.AddWithValue("@NIF", txtNIF.Text);
                            cmdCliente.Parameters.AddWithValue("@Nome", txtNome.Text);
                            cmdCliente.Parameters.AddWithValue("@Contacto", txtContacto.Text);
                            cmdCliente.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmdCliente.ExecuteNonQuery();
                        }

                        // 2. Gravar/Atualizar Veículo Base
                        if (!string.IsNullOrWhiteSpace(txtMatricula.Text))
                        {
                            string queryVeiculo = @"IF EXISTS (SELECT 1 FROM DETAIL_AUTOMOVEL.VEICULO WHERE Matricula = @Matricula)
                                            UPDATE DETAIL_AUTOMOVEL.VEICULO SET Marca=@Marca, Modelo=@Modelo, Cor=@Cor, NIF_Cliente=@NIF WHERE Matricula=@Matricula
                                            ELSE INSERT INTO DETAIL_AUTOMOVEL.VEICULO (Matricula, Marca, Modelo, Cor, NIF_Cliente) VALUES (@Matricula, @Marca, @Modelo, @Cor, @NIF)";

                            using (SqlCommand cmdVeiculo = new SqlCommand(queryVeiculo, CN, trans))
                            {
                                cmdVeiculo.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
                                cmdVeiculo.Parameters.AddWithValue("@Marca", txtMarca.Text);
                                cmdVeiculo.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                                cmdVeiculo.Parameters.AddWithValue("@Cor", txtCor.Text);
                                cmdVeiculo.Parameters.AddWithValue("@NIF", txtNIF.Text);
                                cmdVeiculo.ExecuteNonQuery();
                            }

                            // 3. LIMPEZA E GRAVAÇÃO DE ESPECIALIZAÇÃO (O segredo está aqui)
                            // Primeiro, removemos de ambas as tabelas para evitar que um carro seja "mota e carro" ao mesmo tempo
                            new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.AUTOMOVEL WHERE Matricula = '{txtMatricula.Text}'", CN, trans).ExecuteNonQuery();
                            new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.MOTOCICLO WHERE Matricula = '{txtMatricula.Text}'", CN, trans).ExecuteNonQuery();

                            if (rbAutomovel.Checked)
                            {
                                string queryAuto = "INSERT INTO DETAIL_AUTOMOVEL.AUTOMOVEL (Matricula, Tipo_Automovel, Tipo_Estofos, Num_Lugares) VALUES (@Matricula, @TipoAuto, @Estofos, @Lugares)";
                                using (SqlCommand cmdAuto = new SqlCommand(queryAuto, CN, trans))
                                {
                                    cmdAuto.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
                                    cmdAuto.Parameters.AddWithValue("@TipoAuto", txtTipoAutomovel.Text);
                                    cmdAuto.Parameters.AddWithValue("@Estofos", txtTipoEstofos.Text);
                                    // Evita erro se o número de lugares estiver vazio
                                    int lugares = 0;
                                    int.TryParse(txtNumLugares.Text, out lugares);
                                    cmdAuto.Parameters.AddWithValue("@Lugares", lugares);
                                    cmdAuto.ExecuteNonQuery();
                                }
                            }
                            else if (rbMotociclo.Checked)
                            {
                                string queryMota = "INSERT INTO DETAIL_AUTOMOVEL.MOTOCICLO (Matricula, Tipo_Motociclo, Cilindrada) VALUES (@Matricula, @TipoMota, @Cilindrada)";
                                using (SqlCommand cmdMota = new SqlCommand(queryMota, CN, trans))
                                {
                                    cmdMota.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
                                    cmdMota.Parameters.AddWithValue("@TipoMota", txtTipoMotociclo.Text);
                                    // Evita erro se a cilindrada estiver vazia
                                    int cilindrada = 0;
                                    int.TryParse(txtCilindrada.Text, out cilindrada);
                                    cmdMota.Parameters.AddWithValue("@Cilindrada", cilindrada);
                                    cmdMota.ExecuteNonQuery();
                                }
                            }
                        }

                        trans.Commit(); // Se chegou aqui sem erros, grava definitivamente
                        MessageBox.Show("Dados guardados com sucesso!");
                        CarregarTabela("");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // Se deu erro, desfaz tudo o que tentou fazer nesta sessão
                        MessageBox.Show("Erro ao guardar: " + ex.Message);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro de ligação: " + ex.Message); }
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];

                // Dados do Cliente e Veículo Base
                txtNIF.Text = linha.Cells["NIF_Cliente"].Value?.ToString();
                txtNome.Text = linha.Cells["Nome"].Value?.ToString();
                txtContacto.Text = linha.Cells["Contacto"].Value?.ToString();
                txtEmail.Text = linha.Cells["Email"].Value?.ToString();
                txtMatricula.Text = linha.Cells["Matricula"].Value?.ToString();
                txtMarca.Text = linha.Cells["Marca"].Value?.ToString();
                txtModelo.Text = linha.Cells["Modelo"].Value?.ToString();
                txtCor.Text = linha.Cells["Cor"].Value?.ToString();
                txtNIF.ReadOnly = true;

                // Limpar seleções de rádio antes de verificar
                rbAutomovel.Checked = false;
                rbMotociclo.Checked = false;

                // Se for Automóvel (verifica se tem Tipo_Automovel na BD)
                if (linha.Cells["Tipo_Automovel"].Value != DBNull.Value && linha.Cells["Tipo_Automovel"].Value != null)
                {
                    rbAutomovel.Checked = true;
                    txtTipoAutomovel.Text = linha.Cells["Tipo_Automovel"].Value.ToString();
                    txtTipoEstofos.Text = linha.Cells["Tipo_Estofos"].Value?.ToString();
                    txtNumLugares.Text = linha.Cells["Num_Lugares"].Value?.ToString();
                    pnlAutomovel.Visible = true;
                    pnlMotociclo.Visible = false;
                }
                // Se for Motociclo (verifica se tem Tipo_Motociclo na BD)
                else if (linha.Cells["Tipo_Motociclo"].Value != DBNull.Value && linha.Cells["Tipo_Motociclo"].Value != null)
                {
                    rbMotociclo.Checked = true;
                    txtTipoMotociclo.Text = linha.Cells["Tipo_Motociclo"].Value.ToString();
                    txtCilindrada.Text = linha.Cells["Cilindrada"].Value?.ToString();
                    pnlMotociclo.Visible = true;
                    pnlAutomovel.Visible = false;
                }
                else
                {
                    pnlAutomovel.Visible = false;
                    pnlMotociclo.Visible = false;
                }
            }
        }

        private void rbAutomovel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAutomovel.Checked) { pnlAutomovel.Visible = true; pnlMotociclo.Visible = false; }
        }

        private void rbMotociclo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMotociclo.Checked) { pnlMotociclo.Visible = true; pnlAutomovel.Visible = false; }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            pnlAutomovel.Visible = false;
            pnlMotociclo.Visible = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNIF.Clear(); txtNome.Clear(); txtContacto.Clear(); txtEmail.Clear();
            txtMatricula.Clear(); txtMarca.Clear(); txtModelo.Clear(); txtCor.Clear();
            txtTipoAutomovel.Clear(); txtTipoEstofos.Clear(); txtNumLugares.Clear();
            txtTipoMotociclo.Clear(); txtCilindrada.Clear();

            rbAutomovel.Checked = false; rbMotociclo.Checked = false;
            pnlAutomovel.Visible = false; pnlMotociclo.Visible = false;
            txtNIF.ReadOnly = false;
            txtNome.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // 1. Verificação básica de preenchimento
            if (string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("Selecione um cliente na tabela para poder eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Confirmação de intenção
            if (MessageBox.Show("Tem a certeza que deseja eliminar este cliente e todos os seus veículos?\n\nNota: Se o cliente tiver agendamentos ou faturas, a operação será bloqueada por segurança.",
                "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();

                        // Tuas queries de eliminação organizada de "baixo para cima"
                        string queryEliminar = @"
                    -- Primeiro removemos as especializações dos veículos
                    DELETE FROM DETAIL_AUTOMOVEL.AUTOMOVEL WHERE Matricula IN (SELECT Matricula FROM DETAIL_AUTOMOVEL.VEICULO WHERE NIF_Cliente = @NIF);
                    DELETE FROM DETAIL_AUTOMOVEL.MOTOCICLO WHERE Matricula IN (SELECT Matricula FROM DETAIL_AUTOMOVEL.VEICULO WHERE NIF_Cliente = @NIF);
                    
                    -- Depois removemos os veículos base
                    DELETE FROM DETAIL_AUTOMOVEL.VEICULO WHERE NIF_Cliente = @NIF;
                    
                    -- Por fim, removemos o cliente
                    DELETE FROM DETAIL_AUTOMOVEL.CLIENTE WHERE NIF_Cliente = @NIF;";

                        using (SqlCommand cmd = new SqlCommand(queryEliminar, CN))
                        {
                            cmd.Parameters.AddWithValue("@NIF", txtNIF.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Cliente e veículos eliminados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa o ecrã e atualiza a tabela
                        btnNovo_Click(sender, e);
                        CarregarTabela("");
                    }
                    catch (SqlException ex)
                    {
                        // TRADUÇÃO DE ERROS DO SQL (Avisos diretos)
                        if (ex.Number == 547) // Código de erro para conflito de Foreign Key
                        {
                            if (ex.Message.Contains("AGENDAMENTO"))
                            {
                                MessageBox.Show("Não pode eliminar este cliente porque ele tem AGENDAMENTOS registados.\n\nElimine primeiro o histórico de serviços deste cliente.",
                                    "Erro de Dependência", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else if (ex.Message.Contains("FATURACAO"))
                            {
                                MessageBox.Show("Não pode eliminar este cliente porque já existem FATURAS emitidas em nome dele.\n\nPor motivos legais, clientes com faturas não podem ser apagados.",
                                    "Erro de Contabilidade", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                MessageBox.Show("Não foi possível eliminar: Este registo está a ser usado noutra parte do sistema.",
                                    "Erro de Integridade", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Erro de Base de Dados: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro inesperado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnIrParaAgendamento_Click(object sender, EventArgs e)
        {
            // Verifica se a matrícula pelo menos está escrita na caixa
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("Tem de preencher a matrícula do veículo antes de avançar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool carroExiste = false;

            // 1. Vai à BD ver silenciosamente se este carro já foi guardado
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM DETAIL_AUTOMOVEL.VEICULO WHERE Matricula = @mat", CN))
                    {
                        cmd.Parameters.AddWithValue("@mat", txtMatricula.Text);
                        carroExiste = (int)cmd.ExecuteScalar() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar matrícula: " + ex.Message);
                    return;
                }
            }

            // 2. Se o carro NÃO existe, o programa grava-o automaticamente!
            if (!carroExiste)
            {
                // Garante só que os campos básicos do cliente não estão vazios
                if (string.IsNullOrWhiteSpace(txtNIF.Text) || string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Como este cliente/veículo é novo, preencha pelo menos o Nome e o NIF para podermos guardá-lo automaticamente.", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // MÁGICA: Chama a tua própria função Guardar como se tivesses clicado no botão!
                btnGuardar_Click(null, null);
            }

            // 3. Agora que sabemos que o carro existe (porque já lá estava ou acabou de ser gravado), avança!
            Agendamentos frm = new Agendamentos(this.connectionString, txtMatricula.Text);
            Navegacao.Abrir(this, frm);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            // O ÚNICO COMANDO NECESSÁRIO AGORA PARA VOLTAR
            Navegacao.Voltar(this);
        }

        private void pnlMotociclo_Paint(object sender, PaintEventArgs e) { }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}