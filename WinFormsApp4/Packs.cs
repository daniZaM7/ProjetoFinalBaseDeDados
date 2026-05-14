using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Packs : Form
    {
        private string connectionString;
        private int idPackSelecionado = 0;

        public Packs(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;
            this.WindowState = FormWindowState.Maximized;

            this.Load += Packs_Load;
        }

        private void Packs_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarServicosDisponiveis();
                CarregarPacksExistentes();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void CarregarServicosDisponiveis()
        {
            lstDisponiveis.Items.Clear();
            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                string sql = "SELECT Cod_Servico, Nome_Servico FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL";
                SqlCommand cmd = new SqlCommand(sql, CN);
                CN.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    lstDisponiveis.Items.Add(new ItemListBox { Id = (int)r["Cod_Servico"], Nome = r["Nome_Servico"].ToString() });
                }
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (lstDisponiveis.SelectedItem != null)
            {
                var item = lstDisponiveis.SelectedItem;
                lstSelecionados.Items.Add(item);
                lstDisponiveis.Items.Remove(item);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lstSelecionados.SelectedItem != null)
            {
                var item = lstSelecionados.SelectedItem;
                lstDisponiveis.Items.Add(item);
                lstSelecionados.Items.Remove(item);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomePack.Text) || lstSelecionados.Items.Count == 0)
            {
                MessageBox.Show("Preencha o nome e selecione pelo menos 1 serviço!");
                return;
            }

            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                CN.Open();
                SqlTransaction trans = CN.BeginTransaction();
                try
                {
                    if (idPackSelecionado == 0) idPackSelecionado = new Random().Next(1000, 9999);

                    string sqlPack = @"IF EXISTS (SELECT 1 FROM DETAIL_AUTOMOVEL.PACK WHERE Cod_Pack = @id)
                                       UPDATE DETAIL_AUTOMOVEL.PACK SET Nome_Pack=@n, Preco_Fixo=@p WHERE Cod_Pack=@id
                                       ELSE INSERT INTO DETAIL_AUTOMOVEL.PACK (Cod_Pack, Nome_Pack, Preco_Fixo) VALUES (@id, @n, @p)";

                    SqlCommand cmd = new SqlCommand(sqlPack, CN, trans);
                    cmd.Parameters.AddWithValue("@id", idPackSelecionado);
                    cmd.Parameters.AddWithValue("@n", txtNomePack.Text);

                    string precoTexto = txtPrecoPack.Text.Replace(',', '.');
                    decimal precoFinal = decimal.Parse(precoTexto, System.Globalization.CultureInfo.InvariantCulture);
                    cmd.Parameters.AddWithValue("@p", precoFinal);
                    cmd.ExecuteNonQuery();

                    string sqlLimpar = "DELETE FROM DETAIL_AUTOMOVEL.PACK_SERVICO WHERE Cod_Pack = @id";
                    SqlCommand cmdLimpar = new SqlCommand(sqlLimpar, CN, trans);
                    cmdLimpar.Parameters.AddWithValue("@id", idPackSelecionado);
                    cmdLimpar.ExecuteNonQuery();

                    foreach (ItemListBox item in lstSelecionados.Items)
                    {
                        string sqlIns = "INSERT INTO DETAIL_AUTOMOVEL.PACK_SERVICO (Cod_Pack, Cod_Servico) VALUES (@p, @s)";
                        SqlCommand cmdIns = new SqlCommand(sqlIns, CN, trans);
                        cmdIns.Parameters.AddWithValue("@p", idPackSelecionado);
                        cmdIns.Parameters.AddWithValue("@s", item.Id);
                        cmdIns.ExecuteNonQuery();
                    }

                    trans.Commit();
                    MessageBox.Show("Pack guardado!");
                    btnNovo_Click(null, null);
                    CarregarPacksExistentes();
                }
                catch (Exception ex) { trans.Rollback(); MessageBox.Show("Erro: " + ex.Message); }
            }
        }

        private void CarregarPacksExistentes()
        {
            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DETAIL_AUTOMOVEL.PACK", CN);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPacks.DataSource = dt;
            }
        }

        private void dgvPacks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idPackSelecionado = (int)dgvPacks.Rows[e.RowIndex].Cells["Cod_Pack"].Value;
                txtNomePack.Text = dgvPacks.Rows[e.RowIndex].Cells["Nome_Pack"].Value.ToString();
                txtPrecoPack.Text = dgvPacks.Rows[e.RowIndex].Cells["Preco_Fixo"].Value.ToString();
                CarregarServicosDoPack(idPackSelecionado);
            }
        }

        private void CarregarServicosDoPack(int id)
        {
            lstSelecionados.Items.Clear();
            CarregarServicosDisponiveis();

            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                string sql = @"SELECT S.Cod_Servico, S.Nome_Servico FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL S
                               JOIN DETAIL_AUTOMOVEL.PACK_SERVICO PS ON S.Cod_Servico = PS.Cod_Servico WHERE PS.Cod_Pack = @id";
                SqlCommand cmd = new SqlCommand(sql, CN);
                cmd.Parameters.AddWithValue("@id", id);
                CN.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ItemListBox novoItem = new ItemListBox { Id = (int)r["Cod_Servico"], Nome = r["Nome_Servico"].ToString() };
                    lstSelecionados.Items.Add(novoItem);

                    for (int i = lstDisponiveis.Items.Count - 1; i >= 0; i--)
                    {
                        if (((ItemListBox)lstDisponiveis.Items[i]).Id == novoItem.Id)
                        {
                            lstDisponiveis.Items.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            idPackSelecionado = 0;
            txtNomePack.Clear();
            txtPrecoPack.Clear();
            lstSelecionados.Items.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPackSelecionado == 0) return;
            if (MessageBox.Show("Deseja eliminar este pack?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    CN.Open();
                    string sql = "DELETE FROM DETAIL_AUTOMOVEL.PACK_SERVICO WHERE Cod_Pack=@id; DELETE FROM DETAIL_AUTOMOVEL.PACK WHERE Cod_Pack=@id;";
                    SqlCommand cmd = new SqlCommand(sql, CN);
                    cmd.Parameters.AddWithValue("@id", idPackSelecionado);
                    cmd.ExecuteNonQuery();
                    btnNovo_Click(null, null);
                    CarregarPacksExistentes();
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            // VOLTAR COM O NOVO GESTOR
            Navegacao.Voltar(this);
        }

        private void label3_Click(object sender, EventArgs e) { }
    }

    public class ItemListBox
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public override string ToString() => Nome;
    }
}