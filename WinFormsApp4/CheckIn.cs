using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class CheckIn : Form
    {
        private string connectionString;
        private int idAgendamentoSelecionado = 0;
        private int idInspecaoSelecionada = 0;
        private int idQueVeioDoAgendamento = 0;

        public CheckIn(string connString, int idAgendamento = 0)
        {
            InitializeComponent();
            this.connectionString = connString;
            this.idQueVeioDoAgendamento = idAgendamento;

            // ATENÇÃO: As ligações aos botões (+=) foram apagadas daqui. 
            // O Visual Studio vai usar as ligações que tens no Design (Raio ⚡).
        }

        private void CheckIn_Load(object sender, EventArgs e)
        {
            // 1. Preencher a escala de sujidade
            cbSujidade.Items.Clear();
            for (int i = 1; i <= 5; i++) cbSujidade.Items.Add(i.ToString());
            cbSujidade.SelectedIndex = 2; // Por defeito fica no nível 3

            // 2. Se viermos do Agendamento, escreve o ID na caixa de pesquisa
            if (idQueVeioDoAgendamento > 0)
            {
                txtPesquisa.Text = idQueVeioDoAgendamento.ToString();
            }

            // 3. Carrega a grelha JÁ filtrada! (Isto dispensa o clique no botão)
            CarregarGrelha(txtPesquisa.Text);

            // 4. A MÁGICA DE SELEÇÃO: 
            if (idQueVeioDoAgendamento > 0)
            {
                foreach (DataGridViewRow r in dgvCheckIn.Rows)
                {
                    if (r.Cells["Num_Agend"].Value != DBNull.Value && Convert.ToInt32(r.Cells["Num_Agend"].Value) == idQueVeioDoAgendamento)
                    {
                        r.Selected = true;
                        dgvCheckIn.CurrentCell = r.Cells[0];
                        dgvCheckIn_CellClick(dgvCheckIn, new DataGridViewCellEventArgs(0, r.Index));
                        break;
                    }
                }
            }
        }

        // ========================================================
        // CARREGAR GRELHA (AGORA MOSTRA TUDO!)
        // ========================================================
        private void CarregarGrelha(string pesquisa = "")
        {
            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();
                    string sql = @"
                        SELECT A.Num_Agend, A.Matricula, A.Data_Hora, A.Estado, 
                               C.Num_Inspecao, C.Kms_Entrada, C.Nivel_Sujidade, C.Estado_Pintura, C.Danos, C.Observacoes
                        FROM DETAIL_AUTOMOVEL.AGENDAMENTO A
                        LEFT JOIN DETAIL_AUTOMOVEL.CHECK_IN C ON A.Num_Agend = C.Num_Agend
                        WHERE A.Matricula LIKE @p OR A.Estado LIKE @p OR CAST(A.Num_Agend AS VARCHAR) LIKE @p
                        ORDER BY A.Data_Hora DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, CN))
                    {
                        cmd.Parameters.AddWithValue("@p", "%" + pesquisa.Trim() + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCheckIn.DataSource = dt;

                        // Escondemos APENAS o Num_Inspecao que é irrelevante para o utilizador ver
                        if (dgvCheckIn.Columns.Contains("Num_Inspecao"))
                            dgvCheckIn.Columns["Num_Inspecao"].Visible = false;

                        if (dgvCheckIn.Columns.Contains("Num_Agend"))
                            dgvCheckIn.Columns["Num_Agend"].HeaderText = "ID Agend.";
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar grelha: " + ex.Message); }
            }
        }

        private void dgvCheckIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCheckIn.Rows[e.RowIndex].Cells["Num_Agend"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvCheckIn.Rows[e.RowIndex];

                idAgendamentoSelecionado = Convert.ToInt32(r.Cells["Num_Agend"].Value);

                if (r.Cells["Num_Inspecao"].Value != DBNull.Value)
                    idInspecaoSelecionada = Convert.ToInt32(r.Cells["Num_Inspecao"].Value);
                else
                    idInspecaoSelecionada = 0;

                txtKmsEntrada.Text = r.Cells["Kms_Entrada"].Value?.ToString() ?? "0";
                cbSujidade.SelectedItem = r.Cells["Nivel_Sujidade"].Value?.ToString() ?? "3";
                txtEstadoPintura.Text = r.Cells["Estado_Pintura"].Value?.ToString() ?? "";
                txtDanos.Text = r.Cells["Danos"].Value?.ToString() ?? "";
                txtObservacoes.Text = r.Cells["Observacoes"].Value?.ToString() ?? "";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idAgendamentoSelecionado == 0 && idQueVeioDoAgendamento > 0)
            {
                idAgendamentoSelecionado = idQueVeioDoAgendamento;
                MessageBox.Show("Por favor, selecione um Agendamento na grelha para associar este Check-In.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();
                    SqlTransaction trans = CN.BeginTransaction();
                    try
                    {
                        if (idInspecaoSelecionada == 0)
                        {
                            idInspecaoSelecionada = new Random().Next(1000, 99999);
                            string sqlIns = "INSERT INTO DETAIL_AUTOMOVEL.CHECK_IN (Num_Inspecao, Kms_Entrada, Nivel_Sujidade, Danos, Estado_Pintura, Observacoes, Num_Agend) VALUES (@id, @km, @suj, @danos, @pint, @obs, @agend)";
                            using (SqlCommand cmd = new SqlCommand(sqlIns, CN, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", idInspecaoSelecionada);
                                cmd.Parameters.AddWithValue("@km", string.IsNullOrWhiteSpace(txtKmsEntrada.Text) ? 0 : int.Parse(txtKmsEntrada.Text));
                                cmd.Parameters.AddWithValue("@suj", int.Parse(cbSujidade.Text));
                                cmd.Parameters.AddWithValue("@danos", txtDanos.Text);
                                cmd.Parameters.AddWithValue("@pint", txtEstadoPintura.Text);
                                cmd.Parameters.AddWithValue("@obs", txtObservacoes.Text);
                                cmd.Parameters.AddWithValue("@agend", idAgendamentoSelecionado);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string sqlUp = "UPDATE DETAIL_AUTOMOVEL.CHECK_IN SET Kms_Entrada=@km, Nivel_Sujidade=@suj, Danos=@danos, Estado_Pintura=@pint, Observacoes=@obs WHERE Num_Inspecao=@id";
                            using (SqlCommand cmd = new SqlCommand(sqlUp, CN, trans))
                            {
                                cmd.Parameters.AddWithValue("@km", string.IsNullOrWhiteSpace(txtKmsEntrada.Text) ? 0 : int.Parse(txtKmsEntrada.Text));
                                cmd.Parameters.AddWithValue("@suj", int.Parse(cbSujidade.Text));
                                cmd.Parameters.AddWithValue("@danos", txtDanos.Text);
                                cmd.Parameters.AddWithValue("@pint", txtEstadoPintura.Text);
                                cmd.Parameters.AddWithValue("@obs", txtObservacoes.Text);
                                cmd.Parameters.AddWithValue("@id", idInspecaoSelecionada);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string sqlAgend = "UPDATE DETAIL_AUTOMOVEL.AGENDAMENTO SET Estado = 'Em Curso' WHERE Num_Agend = @agend";
                        using (SqlCommand cmdA = new SqlCommand(sqlAgend, CN, trans))
                        {
                            cmdA.Parameters.AddWithValue("@agend", idAgendamentoSelecionado);
                            cmdA.ExecuteNonQuery();
                        }

                        trans.Commit();
                        MessageBox.Show("Check-In guardado! O carro está oficialmente 'Em Curso'.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recarrega a grelha para mostrar os novos dados inseridos!
                        //CarregarGrelha();
                        //btnNovo_Click(null, null);
                        Navegacao.Voltar(this);
                    }
                    catch (Exception) { trans.Rollback(); throw; }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao guardar Check-In: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            idAgendamentoSelecionado = 0;
            idInspecaoSelecionada = 0;
            txtKmsEntrada.Clear();
            txtDanos.Clear();
            txtEstadoPintura.Clear();
            txtObservacoes.Clear();
            cbSujidade.SelectedIndex = 2;

            // Retira a selecção azul da grelha para o utilizador perceber que limpou
            dgvCheckIn.ClearSelection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idInspecaoSelecionada == 0)
            {
                MessageBox.Show("Este agendamento ainda não tem nenhum Check-In registado para poder eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Tem a certeza que deseja eliminar os registos de Check-In deste veículo?", "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    try
                    {
                        CN.Open();
                        string sql = "DELETE FROM DETAIL_AUTOMOVEL.CHECK_IN WHERE Num_Inspecao = @id";
                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@id", idInspecaoSelecionada);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("O Check-In foi apagado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarGrelha();
                        btnNovo_Click(null, null);
                    }
                    catch (Exception ex) { MessageBox.Show("Erro ao eliminar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e) => Navegacao.Voltar(this);
    }
}