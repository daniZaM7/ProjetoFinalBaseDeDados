using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class c : Form
    {
        private string connectionString;

        public c(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            // Ligar Eventos
            this.Load += (s, e) => CarregarProdutos();
            btnGuardar.Click += btnGuardar_Click;
            btnNovo.Click += btnNovo_Click;
            btnEliminar.Click += btnEliminar_Click;
            dgvProdutos.CellClick += dgvProdutos_CellClick;
            dgvProdutos.CellFormatting += dgvProdutos_CellFormatting;
            txtPesquisa.TextChanged += (s, e) => CarregarProdutos(txtPesquisa.Text);
        }

        private void CarregarProdutos(string termo = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DETAIL_AUTOMOVEL.PRODUTO WHERE Nome LIKE @p OR Marca LIKE @p OR Ref_Produto LIKE @p", CN);
                da.SelectCommand.Parameters.AddWithValue("@p", "%" + termo.Trim() + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProdutos.DataSource = dt;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    CN.Open();
                    string checkSql = "SELECT COUNT(*) FROM DETAIL_AUTOMOVEL.PRODUTO WHERE Ref_Produto = @ref";
                    SqlCommand checkCmd = new SqlCommand(checkSql, CN);
                    checkCmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                    bool existe = (int)checkCmd.ExecuteScalar() > 0;

                    string sql = existe
                        ? "UPDATE DETAIL_AUTOMOVEL.PRODUTO SET Marca=@marca, Nome=@nome, Qtd_Stock=@qtd, Capacidade_Embalagem=@cap, Stock_Minimo=@min, Preco=@preco WHERE Ref_Produto=@ref"
                        : "INSERT INTO DETAIL_AUTOMOVEL.PRODUTO VALUES (@ref, @marca, @nome, @qtd, @cap, @min, @preco)";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                        cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@qtd", numStock.Value);
                        cmd.Parameters.AddWithValue("@cap", decimal.Parse(txtCapacidade.Text, CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@min", decimal.Parse(txtMinimo.Text, CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@preco", decimal.Parse(txtPreco.Text, CultureInfo.InvariantCulture));
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Stock guardado!");
                    CarregarProdutos();
                    btnNovo_Click(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProdutos.Rows[e.RowIndex].Cells["Ref_Produto"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvProdutos.Rows[e.RowIndex];
                txtReferencia.Text = r.Cells["Ref_Produto"].Value.ToString();
                txtMarca.Text = r.Cells["Marca"].Value.ToString();
                txtNome.Text = r.Cells["Nome"].Value.ToString();

                numStock.Value = Convert.ToDecimal(r.Cells["Qtd_Stock"].Value);
                txtCapacidade.Text = r.Cells["Capacidade_Embalagem"].Value.ToString();
                txtMinimo.Text = r.Cells["Stock_Minimo"].Value.ToString();
                txtPreco.Text = r.Cells["Preco"].Value.ToString();
                txtReferencia.ReadOnly = true;
            }
        }

        private void dgvProdutos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProdutos.Columns[e.ColumnIndex].Name == "Qtd_Stock" && e.RowIndex >= 0)
            {
                // 1. Lemos os valores para variáveis temporárias sem tentar converter já
                var celulaQtd = dgvProdutos.Rows[e.RowIndex].Cells["Qtd_Stock"].Value;
                var celulaMin = dgvProdutos.Rows[e.RowIndex].Cells["Stock_Minimo"].Value;

                // 2. O Escudo Protetor: Só avançamos se as células NÃO forem nulas
                if (celulaQtd != null && celulaQtd != DBNull.Value &&
                    celulaMin != null && celulaMin != DBNull.Value)
                {
                    // 3. Agora sim, é seguro converter para decimal
                    decimal qtd = Convert.ToDecimal(celulaQtd);
                    decimal min = Convert.ToDecimal(celulaMin);

                    // CORREÇÃO: Compara embalagens com embalagens diretamente
                    if (qtd <= min)
                    {
                        dgvProdutos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
                    }
                }
            }   
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtReferencia.ReadOnly = false;
            txtReferencia.Clear(); txtMarca.Clear(); txtNome.Clear();
            txtCapacidade.Clear(); txtMinimo.Clear(); txtPreco.Clear();
            numStock.Value = 0;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReferencia.Text)) return;

            if (MessageBox.Show("Eliminar este produto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.PRODUTO WHERE Ref_Produto = @ref";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                            cmd.ExecuteNonQuery();
                        }

                        CarregarProdutos();
                        btnNovo_Click(null, null);
                        MessageBox.Show("Produto eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        // 547 é o código específico do SQL Server para violação de Foreign Key
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("NÃO PODE ELIMINAR: Este produto não pode ser apagado porque já faz parte da 'receita' de um Serviço ativo.\n\nPara o apagar, remova primeiro este produto da ficha técnica dos serviços que o utilizam.",
                                            "Bloqueio de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Navegacao.Voltar(this);
        }
    }
}