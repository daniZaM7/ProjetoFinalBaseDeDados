using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Produtos : Form
    {
        private string connectionString;

        public Produtos(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            // Ligar Eventos aos objetos
            this.Load += (s, e) => CarregarProdutos();
            btnGuardar.Click += btnGuardar_Click;
            btnNovo.Click += btnNovo_Click;
            btnEliminar.Click += btnEliminar_Click;
            dgvProdutos.CellClick += dgvProdutos_CellClick;
            txtPesquisa.TextChanged += (s, e) => CarregarProdutos(txtPesquisa.Text);
        }

        // ========================================================
        // CARREGAR GRELHA
        // ========================================================
        private void CarregarProdutos(string termo = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = @"SELECT Ref_Produto, Marca, Nome, Qtd_Stock, Preco 
                                 FROM DETAIL_AUTOMOVEL.PRODUTO 
                                 WHERE Nome LIKE @p OR Marca LIKE @p OR Ref_Produto LIKE @p";

                    SqlDataAdapter da = new SqlDataAdapter(sql, CN);
                    da.SelectCommand.Parameters.AddWithValue("@p", "%" + termo.Trim() + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvProdutos.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar: " + ex.Message); }
            }
        }

        // ========================================================
        // GUARDAR / ATUALIZAR
        // ========================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReferencia.Text) || string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Referência e Nome são obrigatórios!");
                return;
            }

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    // Verifica se a referência existe para decidir entre INSERT ou UPDATE
                    string checkSql = "SELECT COUNT(*) FROM DETAIL_AUTOMOVEL.PRODUTO WHERE Ref_Produto = @ref";
                    SqlCommand checkCmd = new SqlCommand(checkSql, CN);
                    checkCmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                    int existe = (int)checkCmd.ExecuteScalar();

                    string sql = existe == 0
                        ? "INSERT INTO DETAIL_AUTOMOVEL.PRODUTO (Ref_Produto, Marca, Nome, Qtd_Stock, Preco) VALUES (@ref, @marca, @nome, @qtd, @preco)"
                        : "UPDATE DETAIL_AUTOMOVEL.PRODUTO SET Marca=@marca, Nome=@nome, Qtd_Stock=@qtd, Preco=@preco WHERE Ref_Produto=@ref";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                        cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@qtd", (int)numStock.Value);
                        cmd.Parameters.AddWithValue("@preco", decimal.Parse(txtPreco.Text.Replace(',', '.')));
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Produto processado com sucesso!");
                    CarregarProdutos();
                    btnNovo_Click(null, null);
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar: " + ex.Message); }
            }
        }

        // ========================================================
        // SELECIONAR DA GRELHA
        // ========================================================
        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProdutos.Rows[e.RowIndex].Cells["Ref_Produto"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvProdutos.Rows[e.RowIndex];
                txtReferencia.Text = r.Cells["Ref_Produto"].Value.ToString();
                txtMarca.Text = r.Cells["Marca"].Value.ToString();
                txtNome.Text = r.Cells["Nome"].Value.ToString();
                numStock.Value = Convert.ToInt32(r.Cells["Qtd_Stock"].Value);
                txtPreco.Text = r.Cells["Preco"].Value.ToString();

                // Bloqueamos a edição da Referência porque é a Chave Primária
                txtReferencia.ReadOnly = true;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtReferencia.ReadOnly = false;
            txtReferencia.Clear();
            txtMarca.Clear();
            txtNome.Clear();
            numStock.Value = 0;
            txtPreco.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReferencia.Text)) return;

            if (MessageBox.Show("Eliminar este produto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    CN.Open();
                    string sql = "DELETE FROM DETAIL_AUTOMOVEL.PRODUTO WHERE Ref_Produto = @ref";
                    SqlCommand cmd = new SqlCommand(sql, CN);
                    cmd.Parameters.AddWithValue("@ref", txtReferencia.Text);
                    cmd.ExecuteNonQuery();
                    CarregarProdutos();
                    btnNovo_Click(null, null);
                }
            }
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Navegacao.Voltar(this);
        }
    }
}