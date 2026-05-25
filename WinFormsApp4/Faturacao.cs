using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Faturacao : Form
    {
        private string connectionString;
        private int idAgendSelecionado = 0;
        private decimal valorCalculado = 0;
        private int idQueVeioDoAgendamento = 0;

        public Faturacao(string connString, int idAgendamento = 0)
        {
            InitializeComponent();
            this.connectionString = connString;
            this.idQueVeioDoAgendamento = idAgendamento;
        }

        private void Faturacao_Load(object sender, EventArgs e)
        {
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new string[] { "Por Faturar (Concluídos)", "Faturas Emitidas" });

            // Se viemos do ecrã de Agendamentos com um ID específico
            if (idQueVeioDoAgendamento > 0)
            {
                txtPesquisa.Text = idQueVeioDoAgendamento.ToString();

                // Inteligência: Vamos perguntar à BD se isto já tem fatura para abrir a aba certa!
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    try
                    {
                        CN.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM DETAIL_AUTOMOVEL.FATURACAO WHERE Num_Agend = @id", CN))
                        {
                            cmd.Parameters.AddWithValue("@id", idQueVeioDoAgendamento);
                            int jaFoiFaturado = (int)cmd.ExecuteScalar();

                            if (jaFoiFaturado > 0)
                            {
                                cbFiltro.SelectedIndex = 1; // Abre na aba "Faturas Emitidas"
                            }
                            else
                            {
                                cbFiltro.SelectedIndex = 0; // Abre na aba "Por Faturar"
                            }
                        }
                    }
                    catch { cbFiltro.SelectedIndex = 0; }
                }
            }
            else
            {
                // Se abriu o ecrã sem vir de um agendamento específico
                cbFiltro.SelectedIndex = 0;
            }

            AtualizarFaturacaoDoMes();

            // Agora sim, a grelha tem os dados certos e vai selecioná-los automaticamente!
            SelecionarLinhaNaGrelha(idQueVeioDoAgendamento);
        }

        // ===================================================================
        // FUNÇÃO DE UTILIDADE: Seleciona uma linha específica e clica nela
        // ===================================================================
        private void SelecionarLinhaNaGrelha(int idProcurado)
        {
            if (idProcurado > 0)
            {
                foreach (DataGridViewRow r in dgvFaturacao.Rows)
                {
                    if (r.Cells["Num_Agend"].Value != DBNull.Value && Convert.ToInt32(r.Cells["Num_Agend"].Value) == idProcurado)
                    {
                        r.Selected = true;
                        dgvFaturacao.CurrentCell = r.Cells[0];
                        dgvFaturacao_CellClick(dgvFaturacao, new DataGridViewCellEventArgs(0, r.Index));
                        return; // Pára de procurar assim que encontrar
                    }
                }
            }

            // Se chegou aqui e não encontrou (ou se o ID for 0), tenta selecionar o primeiro se existir
            if (dgvFaturacao.Rows.Count > 0)
            {
                dgvFaturacao.Rows[0].Selected = true;
                dgvFaturacao.CurrentCell = dgvFaturacao.Rows[0].Cells[0];
                dgvFaturacao_CellClick(dgvFaturacao, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void AtualizarFaturacaoDoMes()
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = "SELECT DETAIL_AUTOMOVEL.fn_FaturacaoMensal(@mes, @ano)";
                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@mes", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@ano", DateTime.Now.Year);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != DBNull.Value && resultado != null)
                        {
                            decimal totalFaturado = Convert.ToDecimal(resultado);
                            lblFaturacaoMensal.Text = "Faturação este Mês: " + totalFaturado.ToString("0.00") + " €";
                        }
                        else
                        {
                            lblFaturacaoMensal.Text = "Faturação este Mês: 0,00 €";
                        }
                    }
                }
                catch (Exception)
                {
                    lblFaturacaoMensal.Text = "Erro a ler métricas";
                }
            }
        }

        private void CarregarGrelha(string pesquisa = "")
        {
            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = "";

                    if (cbFiltro.SelectedIndex == 0) // POR FATURAR
                    {
                        sql = @"SELECT A.Num_Agend, A.Data_Hora, A.Matricula, C.Nome AS Cliente, C.NIF_Cliente AS NIF, A.Valor_Total
                                FROM DETAIL_AUTOMOVEL.AGENDAMENTO A
                                JOIN DETAIL_AUTOMOVEL.VEICULO V ON A.Matricula = V.Matricula
                                JOIN DETAIL_AUTOMOVEL.CLIENTE C ON V.NIF_Cliente = C.NIF_Cliente
                                WHERE A.Estado = 'Concluído' 
                                AND A.Num_Agend NOT IN (SELECT Num_Agend FROM DETAIL_AUTOMOVEL.FATURACAO)
                                AND (A.Matricula LIKE @p OR C.Nome LIKE @p OR CAST(A.Num_Agend AS VARCHAR) LIKE @p)";
                        btnEmitirFatura.Enabled = true;
                    }
                    else // JÁ FATURADOS
                    {
                        sql = @"SELECT F.Num_Fatura, F.Data_Emissao, F.Valor, A.Matricula, C.Nome AS Cliente, F.NIF_Entidade AS NIF, F.Num_Agend
                                FROM DETAIL_AUTOMOVEL.FATURACAO F
                                JOIN DETAIL_AUTOMOVEL.AGENDAMENTO A ON F.Num_Agend = A.Num_Agend
                                JOIN DETAIL_AUTOMOVEL.VEICULO V ON A.Matricula = V.Matricula
                                JOIN DETAIL_AUTOMOVEL.CLIENTE C ON V.NIF_Cliente = C.NIF_Cliente
                                WHERE A.Matricula LIKE @p OR C.Nome LIKE @p OR CAST(A.Num_Agend AS VARCHAR) LIKE @p
                                ORDER BY F.Data_Emissao DESC";
                        btnEmitirFatura.Enabled = false;
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@p", "%" + pesquisa.Trim() + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvFaturacao.DataSource = dt;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar lista: " + ex.Message); }
            }
        }

        private void dgvFaturacao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvFaturacao.Rows[e.RowIndex].Cells["Num_Agend"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvFaturacao.Rows[e.RowIndex];
                idAgendSelecionado = Convert.ToInt32(r.Cells["Num_Agend"].Value);

                txtNumAgend.Text = idAgendSelecionado.ToString();
                txtMatricula.Text = r.Cells["Matricula"].Value?.ToString();
                txtCliente.Text = r.Cells["Cliente"].Value?.ToString();
                txtNIF.Text = r.Cells["NIF"].Value?.ToString();

                if (cbFiltro.SelectedIndex == 0) // Por Faturar
                {
                    valorCalculado = Convert.ToDecimal(r.Cells["Valor_Total"].Value);
                }
                else // Já Faturado
                {
                    valorCalculado = Convert.ToDecimal(r.Cells["Valor"].Value);
                }

                txtValorTotal.Text = valorCalculado.ToString("0.00") + " €";
            }
        }

        private void btnEmitirFatura_Click(object sender, EventArgs e)
        {
            if (idAgendSelecionado == 0 || string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("Selecione um Agendamento para faturar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Deseja emitir uma fatura para {txtCliente.Text} no valor de {txtValorTotal.Text}?", "Confirmar Fatura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    try
                    {
                        CN.Open();
                        int numFaturaNova = new Random().Next(100000, 999999);
                        int agendamentoGravado = idAgendSelecionado; // Memoriza o ID

                        string sqlFatura = "INSERT INTO DETAIL_AUTOMOVEL.FATURACAO (Num_Fatura, NIF_Entidade, Data_Emissao, Valor, Num_Agend) VALUES (@fatura, @nif, @data, @valor, @agend)";
                        using (SqlCommand cmdF = new SqlCommand(sqlFatura, CN))
                        {
                            cmdF.Parameters.AddWithValue("@fatura", numFaturaNova);
                            cmdF.Parameters.AddWithValue("@nif", txtNIF.Text);
                            cmdF.Parameters.AddWithValue("@data", DateTime.Now);
                            cmdF.Parameters.AddWithValue("@valor", valorCalculado);
                            cmdF.Parameters.AddWithValue("@agend", agendamentoGravado);
                            cmdF.ExecuteNonQuery();
                        }

                        MessageBox.Show("Fatura registada com sucesso!", "Faturação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // O agendamento desapareceu dos pendentes, por isso a grelha vai ser vazia. 
                        // Vamos mudar para o separador de faturas emitidas para o utilizador ver o resultado
                        cbFiltro.SelectedIndex = 1;

                        CarregarGrelha(txtPesquisa.Text);
                        AtualizarFaturacaoDoMes();

                        // Foca na fatura acabada de criar
                        SelecionarLinhaNaGrelha(agendamentoGravado);
                    }
                    catch (Exception ex) { MessageBox.Show("Erro SQL ao emitir fatura: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void cbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCampos();
            CarregarGrelha(txtPesquisa.Text);
            SelecionarLinhaNaGrelha(0); // Seleciona o primeiro da lista
        }

        private void LimparCampos()
        {
            idAgendSelecionado = 0;
            valorCalculado = 0;
            txtNumAgend.Clear();
            txtMatricula.Clear();
            txtCliente.Clear();
            txtNIF.Clear();
            txtValorTotal.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idAgendSelecionado == 0 || cbFiltro.SelectedIndex == 0)
            {
                MessageBox.Show("Selecione uma fatura emitida para eliminar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensagem = $"Tem a certeza que deseja eliminar a fatura do agendamento {idAgendSelecionado}?\n\nO agendamento voltará a aparecer na lista 'Por Faturar'.";

            if (MessageBox.Show(mensagem, "Eliminar Fatura", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    try
                    {
                        CN.Open();
                        int agendamentoApagado = idAgendSelecionado; // Memoriza o ID

                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.FATURACAO WHERE Num_Agend = @agend";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@agend", agendamentoApagado);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Fatura eliminada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Muda para o separador pendentes para o utilizador ver a fatura que devolveu
                        cbFiltro.SelectedIndex = 0;

                        CarregarGrelha(txtPesquisa.Text);
                        AtualizarFaturacaoDoMes();

                        // Foca na fatura que voltou aos pendentes
                        SelecionarLinhaNaGrelha(agendamentoApagado);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao eliminar fatura: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e) => Navegacao.Voltar(this);
        private void btnPesquisar_Click(object sender, EventArgs e) => CarregarGrelha(txtPesquisa.Text);
        private void txtPesquisa_TextChanged(object sender, EventArgs e) => CarregarGrelha(txtPesquisa.Text);
    }
}