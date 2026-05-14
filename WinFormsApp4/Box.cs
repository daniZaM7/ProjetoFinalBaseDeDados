using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Box : Form
    {
        private string connectionString;
        private int idBoxSelecionada = 0;

        public Box(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;
        }

        private void Box_Load(object sender, EventArgs e)
        {
            CarregarBoxes();
        }

        // ========================================================
        // CARREGAR GRELHA
        // ========================================================
        private void CarregarBoxes()
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = "SELECT Num_Box, Tipo_Box FROM DETAIL_AUTOMOVEL.BOX ORDER BY Num_Box ASC";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBoxes.DataSource = dt;

                        if (dgvBoxes.Columns.Contains("Num_Box"))
                            dgvBoxes.Columns["Num_Box"].HeaderText = "Nº da Box";
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar Boxes: " + ex.Message); }
            }
        }

        // ========================================================
        // GUARDAR / ATUALIZAR
        // ========================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumBox.Text) || string.IsNullOrWhiteSpace(txtTipoBox.Text))
            {
                MessageBox.Show("Preencha o Número e o Tipo da Box!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int numBoxInput;
            if (!int.TryParse(txtNumBox.Text, out numBoxInput))
            {
                MessageBox.Show("O Número da Box tem de ser um número inteiro (ex: 1, 2, 3)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();

                    // Verifica se a Box já existe para decidir se faz UPDATE ou INSERT
                    string checkSql = "SELECT COUNT(*) FROM DETAIL_AUTOMOVEL.BOX WHERE Num_Box = @id";
                    SqlCommand checkCmd = new SqlCommand(checkSql, CN);
                    checkCmd.Parameters.AddWithValue("@id", numBoxInput);
                    int existe = (int)checkCmd.ExecuteScalar();

                    string sql = existe == 0
                        ? "INSERT INTO DETAIL_AUTOMOVEL.BOX (Num_Box, Tipo_Box) VALUES (@id, @tipo)"
                        : "UPDATE DETAIL_AUTOMOVEL.BOX SET Tipo_Box=@tipo WHERE Num_Box=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@id", numBoxInput);
                        cmd.Parameters.AddWithValue("@tipo", txtTipoBox.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Box guardada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarBoxes();
                    btnNovo_Click(null, null);
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // ========================================================
        // SELECIONAR DA GRELHA PARA EDITAR
        // ========================================================
        private void dgvBoxes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBoxes.Rows[e.RowIndex].Cells["Num_Box"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvBoxes.Rows[e.RowIndex];
                idBoxSelecionada = Convert.ToInt32(r.Cells["Num_Box"].Value);

                txtNumBox.Text = idBoxSelecionada.ToString();
                txtTipoBox.Text = r.Cells["Tipo_Box"].Value?.ToString();

                // Bloqueia o número da box para não mudar a chave primária sem querer
                txtNumBox.ReadOnly = true;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            idBoxSelecionada = 0;
            txtNumBox.Clear();
            txtTipoBox.Clear();
            txtNumBox.ReadOnly = false; // Permite escrever o número para uma nova box
            txtNumBox.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idBoxSelecionada == 0) return;

            if (MessageBox.Show("Tem a certeza que deseja eliminar esta Box?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.BOX WHERE Num_Box = @id";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@id", idBoxSelecionada);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Box eliminada.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarBoxes();
                        btnNovo_Click(null, null);
                    }
                    catch (SqlException ex)
                    {
                        // Código 547 = Violação de Chave Estrangeira (Já foi usada num agendamento)
                        if (ex.Number == 547)
                            MessageBox.Show("Não podes eliminar esta Box porque já existem carros agendados para ela!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show("Erro SQL: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Navegacao.Voltar(this);
        }
    }
}