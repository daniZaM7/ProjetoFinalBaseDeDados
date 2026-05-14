using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Funcionario : Form
    {
        private string connectionString;
        private int idFuncionarioSelecionado = 0;

        public Funcionario(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            // Ligar os eventos aos botões e ao Load
            this.Load += Funcionario_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnNovo.Click += btnNovo_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnVoltar.Click += btnVoltar_Click;
            btnPesquisar.Click += (s, e) => CarregarFuncionarios(txtPesquisa.Text);
            txtPesquisa.TextChanged += (s, e) => CarregarFuncionarios(txtPesquisa.Text);
            dgvFuncionarios.CellClick += dgvFuncionarios_CellClick;
        }

        private void Funcionario_Load(object sender, EventArgs e)
        {
            // Adicionar as especialidades predefinidas à CheckedListBox
            clbEspecialidades.Items.Clear();
            clbEspecialidades.Items.AddRange(new string[] { "Lavagem Exterior", "Lavagem Interior", "Polimento", "Mecânica", "Limpeza de Estofos", "Geral" });

            CarregarFuncionarios();
        }

        // ========================================================
        // CARREGAR GRELHA
        // ========================================================
        private void CarregarFuncionarios(string pesquisa = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = @"SELECT Num_Func, Nome, Contacto, Especialidade 
                                   FROM DETAIL_AUTOMOVEL.FUNCIONARIO 
                                   WHERE Nome LIKE @p OR Especialidade LIKE @p OR Contacto LIKE @p";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@p", "%" + pesquisa.Trim() + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvFuncionarios.DataSource = dt;

                        // Estética da grelha
                        if (dgvFuncionarios.Columns.Contains("Num_Func"))
                            dgvFuncionarios.Columns["Num_Func"].HeaderText = "ID";
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar: " + ex.Message); }
            }
        }

        // ========================================================
        // GUARDAR / ATUALIZAR
        // ========================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtContacto.Text))
            {
                MessageBox.Show("Preencha o Nome e o Contacto do funcionário!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Juntar todos os vistos numa única frase separada por vírgulas
            string especialidadesEscolhidas = "";
            foreach (var item in clbEspecialidades.CheckedItems)
            {
                especialidadesEscolhidas += item.ToString() + ", ";
            }

            // Remover a última vírgula que sobra no fim (se houver alguma coisa selecionada)
            if (especialidadesEscolhidas.Length > 0)
            {
                especialidadesEscolhidas = especialidadesEscolhidas.Substring(0, especialidadesEscolhidas.Length - 2);
            }
            else
            {
                especialidadesEscolhidas = "Geral"; // Valor por defeito se não marcar nada
            }

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();

                    // Se for novo, geramos um número ID (Ex: 1000 a 9999)
                    int idFinal = idFuncionarioSelecionado == 0 ? new Random().Next(1000, 9999) : idFuncionarioSelecionado;

                    string sql = idFuncionarioSelecionado == 0
                        ? "INSERT INTO DETAIL_AUTOMOVEL.FUNCIONARIO (Num_Func, Nome, Contacto, Especialidade) VALUES (@id, @nome, @contacto, @especialidade)"
                        : "UPDATE DETAIL_AUTOMOVEL.FUNCIONARIO SET Nome=@nome, Contacto=@contacto, Especialidade=@especialidade WHERE Num_Func=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idFinal);
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@contacto", txtContacto.Text);
                        cmd.Parameters.AddWithValue("@especialidade", especialidadesEscolhidas);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Funcionário guardado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarFuncionarios();
                    btnNovo_Click(null, null);
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // ========================================================
        // SELECIONAR DA GRELHA
        // ========================================================
        private void dgvFuncionarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvFuncionarios.Rows[e.RowIndex].Cells["Num_Func"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvFuncionarios.Rows[e.RowIndex];
                idFuncionarioSelecionado = Convert.ToInt32(r.Cells["Num_Func"].Value);

                txtNome.Text = r.Cells["Nome"].Value?.ToString();
                txtContacto.Text = r.Cells["Contacto"].Value?.ToString();

                // 1. Limpar todos os vistos antes de marcar
                for (int i = 0; i < clbEspecialidades.Items.Count; i++)
                {
                    clbEspecialidades.SetItemChecked(i, false);
                }

                // 2. Ler da grelha (Ex: "Lavagem Exterior, Polimento")
                string especialidadeDaGrelha = r.Cells["Especialidade"].Value?.ToString() ?? "";

                // 3. Cortar o texto pelas vírgulas
                string[] especialidades = especialidadeDaGrelha.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                // 4. Marcar os vistos que coincidem com os pedaços cortados
                foreach (string esp in especialidades)
                {
                    for (int i = 0; i < clbEspecialidades.Items.Count; i++)
                    {
                        if (clbEspecialidades.Items[i].ToString() == esp)
                        {
                            clbEspecialidades.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            idFuncionarioSelecionado = 0;
            txtNome.Clear();
            txtContacto.Clear();

            // Limpar todos os vistos
            for (int i = 0; i < clbEspecialidades.Items.Count; i++)
            {
                clbEspecialidades.SetItemChecked(i, false);
            }

            txtNome.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idFuncionarioSelecionado == 0) return;

            if (MessageBox.Show("Tem a certeza que deseja eliminar este funcionário?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();
                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.FUNCIONARIO WHERE Num_Func = @id";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@id", idFuncionarioSelecionado);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Funcionário eliminado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarFuncionarios();
                        btnNovo_Click(null, null);
                    }
                    catch (SqlException ex)
                    {
                        // Código de erro 547 é Violação de Foreign Key
                        if (ex.Number == 547)
                            MessageBox.Show("Não podes eliminar este funcionário porque ele já tem agendamentos de trabalho atribuídos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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