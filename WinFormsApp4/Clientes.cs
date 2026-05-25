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
                    // USO DE STORED PROCEDURE (SQL Programming)
                    using (SqlCommand cmd = new SqlCommand("DETAIL_AUTOMOVEL.sp_GuardarClienteVeiculo", CN))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NIF", txtNIF.Text);
                        cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@Contacto", txtContacto.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        cmd.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
                        cmd.Parameters.AddWithValue("@Marca", txtMarca.Text);
                        cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                        cmd.Parameters.AddWithValue("@Cor", txtCor.Text);

                        cmd.Parameters.AddWithValue("@IsAutomovel", rbAutomovel.Checked);
                        cmd.Parameters.AddWithValue("@TipoAutomovel", txtTipoAutomovel.Text);
                        cmd.Parameters.AddWithValue("@TipoEstofos", txtTipoEstofos.Text);
                        int lugares = 0; int.TryParse(txtNumLugares.Text, out lugares);
                        cmd.Parameters.AddWithValue("@NumLugares", lugares);

                        cmd.Parameters.AddWithValue("@IsMotociclo", rbMotociclo.Checked);
                        cmd.Parameters.AddWithValue("@TipoMotociclo", txtTipoMotociclo.Text);
                        int cilindrada = 0; int.TryParse(txtCilindrada.Text, out cilindrada);
                        cmd.Parameters.AddWithValue("@Cilindrada", cilindrada);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados guardados com sucesso!");
                    CarregarTabela("");
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar: " + ex.Message); }
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];

                txtNIF.Text = linha.Cells["NIF_Cliente"].Value?.ToString();
                txtNome.Text = linha.Cells["Nome"].Value?.ToString();
                txtContacto.Text = linha.Cells["Contacto"].Value?.ToString();
                txtEmail.Text = linha.Cells["Email"].Value?.ToString();
                txtMatricula.Text = linha.Cells["Matricula"].Value?.ToString();
                txtMarca.Text = linha.Cells["Marca"].Value?.ToString();
                txtModelo.Text = linha.Cells["Modelo"].Value?.ToString();
                txtCor.Text = linha.Cells["Cor"].Value?.ToString();
                txtNIF.ReadOnly = true;

                rbAutomovel.Checked = false; rbMotociclo.Checked = false;

                if (linha.Cells["Tipo_Automovel"].Value != DBNull.Value && linha.Cells["Tipo_Automovel"].Value != null)
                {
                    rbAutomovel.Checked = true;
                    txtTipoAutomovel.Text = linha.Cells["Tipo_Automovel"].Value.ToString();
                    txtTipoEstofos.Text = linha.Cells["Tipo_Estofos"].Value?.ToString();
                    txtNumLugares.Text = linha.Cells["Num_Lugares"].Value?.ToString();
                    pnlAutomovel.Visible = true; pnlMotociclo.Visible = false;
                }
                else if (linha.Cells["Tipo_Motociclo"].Value != DBNull.Value && linha.Cells["Tipo_Motociclo"].Value != null)
                {
                    rbMotociclo.Checked = true;
                    txtTipoMotociclo.Text = linha.Cells["Tipo_Motociclo"].Value.ToString();
                    txtCilindrada.Text = linha.Cells["Cilindrada"].Value?.ToString();
                    pnlMotociclo.Visible = true; pnlAutomovel.Visible = false;
                }
                else
                {
                    pnlAutomovel.Visible = false; pnlMotociclo.Visible = false;
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
            pnlAutomovel.Visible = false; pnlMotociclo.Visible = false;
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
            if (string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("Selecione um cliente na tabela para poder eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Tem a certeza que deseja eliminar este cliente e todos os seus veículos?", "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        // USO DE STORED PROCEDURE PARA ELIMINAR EM CASCATA SEGURA
                        using (SqlCommand cmd = new SqlCommand("DETAIL_AUTOMOVEL.sp_EliminarCliente", CN))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@NIF", txtNIF.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Cliente e veículos eliminados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnNovo_Click(sender, e); CarregarTabela("");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            if (ex.Message.Contains("AGENDAMENTO")) MessageBox.Show("Não pode eliminar este cliente porque ele tem AGENDAMENTOS registados.", "Erro de Dependência", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            else if (ex.Message.Contains("FATURACAO")) MessageBox.Show("Não pode eliminar este cliente porque já existem FATURAS emitidas em nome dele.", "Erro de Contabilidade", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            else MessageBox.Show("Não foi possível eliminar: Este registo está a ser usado noutra parte do sistema.", "Erro de Integridade", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else MessageBox.Show("Erro de Base de Dados: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex) { MessageBox.Show("Erro inesperado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnIrParaAgendamento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text)) { MessageBox.Show("Tem de preencher a matrícula do veículo antes de avançar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            bool carroExiste = false;
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
                catch (Exception ex) { MessageBox.Show("Erro ao verificar matrícula: " + ex.Message); return; }
            }

            if (!carroExiste)
            {
                if (string.IsNullOrWhiteSpace(txtNIF.Text) || string.IsNullOrWhiteSpace(txtNome.Text)) { MessageBox.Show("Preencha pelo menos o Nome e o NIF.", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                btnGuardar_Click(null, null);
            }

            Agendamentos frm = new Agendamentos(this.connectionString, txtMatricula.Text);
            Navegacao.Abrir(this, frm);
        }

        private void btnVoltar_Click(object sender, EventArgs e) => Navegacao.Voltar(this);
        private void pnlMotociclo_Paint(object sender, PaintEventArgs e) { }
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}