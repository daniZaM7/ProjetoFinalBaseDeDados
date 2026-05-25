using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;

namespace WinFormsApp4
{
    public partial class Agendamentos : Form
    {
        private string connectionString;
        private int idAgendamentoSelecionado = 0;

        public Agendamentos(string connString, string matricula = "")
        {
            InitializeComponent();
            this.connectionString = connString;

            if (!string.IsNullOrEmpty(matricula))
            {
                txtMatricula_Agend.Text = matricula;

                txtPesquisa.Text = matricula;

            }


            Data.Format = DateTimePickerFormat.Custom;
            Data.CustomFormat = "dd/MM/yyyy HH:mm";

            // Evento para atualizar o preço sempre que clicar num serviço
            clbServicos.ItemCheck += (s, e) => this.BeginInvoke(new Action(CalcularTotalEmTempoReal));

            cbPack.SelectedIndexChanged += (s, e) => AtualizarServicosPorPack();
        }

        private void Agendamentos_Load(object sender, EventArgs e)
        {
            ConfigurarComboBoxes();
            CarregarDadosAuxiliares();

            // 1. Verifica se a matrícula veio preenchida do ecrã de Clientes
            if (!string.IsNullOrWhiteSpace(txtMatricula_Agend.Text))
            {
                txtPesquisa.Text = txtMatricula_Agend.Text; // Escreve na caixa de pesquisa
            }

            // 2. Carrega a grelha JÁ filtrada com o que estiver na caixa de pesquisa!
            CarregarAgendamentos(txtPesquisa.Text);
            SelecionarPrimeiroAgendamento();
        }

        private void ConfigurarComboBoxes()
        {
            cbEstado.Items.Clear();
            cbEstado.Items.AddRange(new string[] { "Pendente", "Confirmado", "Em Curso", "Concluído", "Cancelado" });
            cbEstado.SelectedIndex = 0;
        }

        private void CarregarDadosAuxiliares()
        {
            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();

                    // 1. Serviços (CheckedListBox)
                    clbServicos.Items.Clear();
                    using (SqlCommand cmdSrv = new SqlCommand("SELECT Cod_Servico, Nome_Servico FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL", CN))
                    using (SqlDataReader rSrv = cmdSrv.ExecuteReader())
                    {
                        while (rSrv.Read())
                            clbServicos.Items.Add(new ItemServico { Id = (int)rSrv["Cod_Servico"], Nome = rSrv["Nome_Servico"].ToString() });
                    }

                    // 2. Packs (ComboBox)
                    cbPack.Items.Clear();
                    cbPack.Items.Add(new ComboItem { Id = 0, Texto = "-- Nenhum Pack --" });
                    using (SqlCommand cmdPack = new SqlCommand("SELECT Cod_Pack, Nome_Pack FROM DETAIL_AUTOMOVEL.PACK", CN))
                    using (SqlDataReader rPack = cmdPack.ExecuteReader())
                    {
                        while (rPack.Read())
                            cbPack.Items.Add(new ComboItem { Id = (int)rPack["Cod_Pack"], Texto = rPack["Nome_Pack"].ToString() });
                    }
                    cbPack.SelectedIndex = 0;

                    // 3. Funcionário (ComboBox)
                    cbFuncionario.Items.Clear();
                    cbFuncionario.Items.Add(new ComboItem { Id = 0, Texto = "-- Selecione Funcionário --" });
                    using (SqlCommand cmdFunc = new SqlCommand("SELECT Num_Func, Nome FROM DETAIL_AUTOMOVEL.FUNCIONARIO", CN))
                    using (SqlDataReader rFunc = cmdFunc.ExecuteReader())
                    {
                        while (rFunc.Read())
                            cbFuncionario.Items.Add(new ComboItem { Id = (int)rFunc["Num_Func"], Texto = rFunc["Nome"].ToString() });
                    }
                    cbFuncionario.SelectedIndex = 0;

                    // 4. Boxes (CheckedListBox)
                    clbBoxes.Items.Clear();
                    using (SqlCommand cmdBox = new SqlCommand("SELECT Num_Box, Tipo_Box FROM DETAIL_AUTOMOVEL.BOX", CN))
                    using (SqlDataReader rBox = cmdBox.ExecuteReader())
                    {
                        while (rBox.Read())
                            clbBoxes.Items.Add(new ComboItem { Id = (int)rBox["Num_Box"], Texto = "Box " + rBox["Num_Box"] + " (" + rBox["Tipo_Box"] + ")" });
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar auxiliares: " + ex.Message); }
            }
        }


        // ========================================================
        // CARREGAR GRELHA PRINCIPAL (Apenas Agendamento)
        // ========================================================
        private void CarregarAgendamentos(string pesquisa = "")
        {
            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    // Esta query garante que mesmo que não haja Pack ou Funcionario, o agendamento aparece (LEFT JOIN)
                    string query = @"
                    SELECT 
                        A.Num_Agend AS [ID], 
                        A.Data_Hora AS [Data/Hora], 
                        A.Estado, 
                        A.Matricula, 
                        P.Nome_Pack AS [Pack],
                        F.Nome AS [Funcionário],
                        A.Notas_Internas AS [Notas], 
                        A.Valor_Total AS [Total]
                    FROM DETAIL_AUTOMOVEL.AGENDAMENTO A
                    -- Ligação para o Pack
                    LEFT JOIN DETAIL_AUTOMOVEL.AGENDAMENTO_PACK AP ON A.Num_Agend = AP.Num_Agend
                    LEFT JOIN DETAIL_AUTOMOVEL.PACK P ON AP.Cod_Pack = P.Cod_Pack
                    -- Ligação para o Funcionário
                    LEFT JOIN DETAIL_AUTOMOVEL.AGENDAMENTO_FUNCIONARIO AF ON A.Num_Agend = AF.Num_Agend
                    LEFT JOIN DETAIL_AUTOMOVEL.FUNCIONARIO F ON AF.Num_Func = F.Num_Func
                    WHERE A.Matricula LIKE @p OR A.Estado LIKE @p OR CAST(A.Num_Agend AS VARCHAR) LIKE @p
                    ORDER BY A.Data_Hora DESC";

                    using (SqlCommand cmd = new SqlCommand(query, CN))
                    {
                        string termo = pesquisa;
                        cmd.Parameters.AddWithValue("@p", "%" + termo.Trim() + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvAgendamentos.DataSource = dt;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro ao carregar grelha: " + ex.Message); }
            }
        }
        private void SelecionarPrimeiroAgendamento()
        {
            if (dgvAgendamentos.Rows.Count > 0)
            {
                // 1. Seleciona visualmente a primeira linha
                dgvAgendamentos.Rows[0].Selected = true;
                dgvAgendamentos.CurrentCell = dgvAgendamentos.Rows[0].Cells[0];

                // 2. Chama manualmente o evento CellClick para preencher as TextBoxes e ComboBoxes
                dgvAgendamentos_CellClick(dgvAgendamentos, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula_Agend.Text))
            {
                MessageBox.Show("A matrícula é obrigatória!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tratamento de valor para o SQL
            decimal valorParaGuardar = 0;
            string textoLimpo = txtValorTotal.Text.Replace(" €", "").Replace(",", ".").Trim();
            decimal.TryParse(textoLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out valorParaGuardar);

            using (SqlConnection CN = new SqlConnection(this.connectionString))
            {
                try
                {
                    CN.Open();
                    SqlTransaction trans = CN.BeginTransaction();
                    try
                    {
                        int idFinal = idAgendamentoSelecionado;

                        // 1. GRAVAR/ATUALIZAR CABEÇALHO DO AGENDAMENTO
                        if (idAgendamentoSelecionado == 0)
                        {
                            idFinal = new Random().Next(1000, 99999);
                            string sqlA = "INSERT INTO DETAIL_AUTOMOVEL.AGENDAMENTO (Num_Agend, Data_Hora, Estado, Notas_Internas, Matricula, Valor_Total) VALUES (@id, @dt, @st, @nt, @mt, @vt)";
                            using (SqlCommand cmdA = new SqlCommand(sqlA, CN, trans))
                            {
                                cmdA.Parameters.AddWithValue("@id", idFinal);
                                cmdA.Parameters.AddWithValue("@dt", Data.Value);
                                cmdA.Parameters.AddWithValue("@st", cbEstado.Text);
                                cmdA.Parameters.AddWithValue("@nt", txtNotas.Text);
                                cmdA.Parameters.AddWithValue("@mt", txtMatricula_Agend.Text);
                                cmdA.Parameters.AddWithValue("@vt", valorParaGuardar);
                                cmdA.ExecuteNonQuery();
                            }
                            idAgendamentoSelecionado = idFinal;
                        }
                        else
                        {
                            string sqlUp = "UPDATE DETAIL_AUTOMOVEL.AGENDAMENTO SET Data_Hora=@dt, Estado=@st, Notas_Internas=@nt, Matricula=@mt, Valor_Total=@vt WHERE Num_Agend=@id";
                            using (SqlCommand cmdUp = new SqlCommand(sqlUp, CN, trans))
                            {
                                cmdUp.Parameters.AddWithValue("@dt", Data.Value);
                                cmdUp.Parameters.AddWithValue("@st", cbEstado.Text);
                                cmdUp.Parameters.AddWithValue("@nt", txtNotas.Text);
                                cmdUp.Parameters.AddWithValue("@mt", txtMatricula_Agend.Text);
                                cmdUp.Parameters.AddWithValue("@vt", valorParaGuardar);
                                cmdUp.Parameters.AddWithValue("@id", idFinal);
                                cmdUp.ExecuteNonQuery();
                            }
                        }

                        // 2. LIMPEZA DE LIGAÇÕES ANTIGAS
                        new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.AGENDAMENTO_SERVICO WHERE Num_Agend = {idFinal}", CN, trans).ExecuteNonQuery();
                        new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.AGENDAMENTO_PACK WHERE Num_Agend = {idFinal}", CN, trans).ExecuteNonQuery();
                        new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.AGENDAMENTO_FUNCIONARIO WHERE Num_Agend = {idFinal}", CN, trans).ExecuteNonQuery();
                        new SqlCommand($"DELETE FROM DETAIL_AUTOMOVEL.AGENDAMENTO_BOX WHERE Num_Agend = {idFinal}", CN, trans).ExecuteNonQuery();

                        // 3. GRAVAR SERVIÇOS INDIVIDUAIS
                        foreach (ItemServico item in clbServicos.CheckedItems)
                        {
                            string sqlS = "INSERT INTO DETAIL_AUTOMOVEL.AGENDAMENTO_SERVICO (Num_Agend, Cod_Servico) VALUES (@a, @s)";
                            using (SqlCommand cmdS = new SqlCommand(sqlS, CN, trans))
                            {
                                cmdS.Parameters.AddWithValue("@a", idFinal);
                                cmdS.Parameters.AddWithValue("@s", item.Id);
                                cmdS.ExecuteNonQuery();
                            }
                        }

                        // 4. GRAVAR PACK
                        if (cbPack.SelectedItem is ComboItem pack && pack.Id > 0)
                        {
                            string sqlP = "INSERT INTO DETAIL_AUTOMOVEL.AGENDAMENTO_PACK (Num_Agend, Cod_Pack) VALUES (@a, @p)";
                            using (SqlCommand cmdP = new SqlCommand(sqlP, CN, trans))
                            {
                                cmdP.Parameters.AddWithValue("@a", idFinal);
                                cmdP.Parameters.AddWithValue("@p", pack.Id);
                                cmdP.ExecuteNonQuery();
                            }
                        }

                        // 5. GRAVAR FUNCIONÁRIO
                        if (cbFuncionario.SelectedItem is ComboItem func && func.Id > 0)
                        {
                            string sqlF = "INSERT INTO DETAIL_AUTOMOVEL.AGENDAMENTO_FUNCIONARIO (Num_Agend, Num_Func) VALUES (@a, @f)";
                            using (SqlCommand cmdF = new SqlCommand(sqlF, CN, trans))
                            {
                                cmdF.Parameters.AddWithValue("@a", idFinal);
                                cmdF.Parameters.AddWithValue("@f", func.Id);
                                cmdF.ExecuteNonQuery();
                            }
                        }

                        // 6. GRAVAR BOXES
                        int ordem = 1;
                        foreach (ComboItem box in clbBoxes.CheckedItems)
                        {
                            string sqlB = "INSERT INTO DETAIL_AUTOMOVEL.AGENDAMENTO_BOX (Num_Agend, Num_Box, Ordem) VALUES (@a, @b, @o)";
                            using (SqlCommand cmdB = new SqlCommand(sqlB, CN, trans))
                            {
                                cmdB.Parameters.AddWithValue("@a", idFinal);
                                cmdB.Parameters.AddWithValue("@b", box.Id);
                                cmdB.Parameters.AddWithValue("@o", ordem++);
                                cmdB.ExecuteNonQuery();
                            }
                        }

                        // =========================================================
                        // 7. A MAGIA DO SQL PROGRAMMING: CHAMAR A STORED PROCEDURE
                        // =========================================================
                        // Só desconta o stock se o carro estiver efetivamente pronto!
                        if (cbEstado.Text == "Concluído")
                        {
                            using (SqlCommand cmdStock = new SqlCommand("DETAIL_AUTOMOVEL.sp_DescontarStock", CN, trans))
                            {
                                // Aqui dizemos ao C# que isto não é uma query normal, é uma SP!
                                cmdStock.CommandType = CommandType.StoredProcedure;
                                cmdStock.Parameters.AddWithValue("@Num_Agend", idFinal);
                                cmdStock.ExecuteNonQuery();
                            }
                        }

                        // Se chegou até aqui sem dar erro na SP nem nos inserts, grava tudo de vez!
                        trans.Commit();
                        MessageBox.Show("Agendamento e Inventário atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ATUALIZAÇÃO DA GRELHA E BOTÕES
                        CarregarAgendamentos(txtPesquisa.Text);
                        btnFaturacao.Enabled = (idAgendamentoSelecionado > 0 && cbEstado.Text == "Concluído");
                        btnCheckIn.Enabled = (idAgendamentoSelecionado > 0 && cbEstado.Text != "Concluído" && cbEstado.Text != "Cancelado");

                        // APAGAR ISTO:
                        // SelecionarPrimeiroAgendamento();

                        // COLOCAR ISTO NO LUGAR (A Memória da Grelha):
                        bool linhaEncontrada = false;
                        foreach (DataGridViewRow row in dgvAgendamentos.Rows)
                        {
                            if (row.Cells["ID"].Value != DBNull.Value && Convert.ToInt32(row.Cells["ID"].Value) == idFinal)
                            {
                                row.Selected = true;
                                dgvAgendamentos.CurrentCell = row.Cells[0];

                                // Simula o clique silenciosamente para atualizar as variáveis
                                dgvAgendamentos_CellClick(dgvAgendamentos, new DataGridViewCellEventArgs(0, row.Index));
                                linhaEncontrada = true;
                                break;
                            }
                        }

                        // Se por algum motivo não o encontrar na grelha, aí sim seleciona o primeiro
                        if (!linhaEncontrada)
                        {
                            SelecionarPrimeiroAgendamento();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Erro na transação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao ligar à BD: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAgendamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se a linha é válida (ID não nulo)
            if (e.RowIndex >= 0 && dgvAgendamentos.Rows[e.RowIndex].Cells["ID"].Value != DBNull.Value)
            {
                DataGridViewRow r = dgvAgendamentos.Rows[e.RowIndex];

                // 1. Captura o ID
                idAgendamentoSelecionado = Convert.ToInt32(r.Cells["ID"].Value);

                // 2. Preenche os campos de texto básicos
                txtMatricula_Agend.Text = r.Cells["Matricula"].Value?.ToString() ?? "";

                if (r.Cells["Data/Hora"].Value != DBNull.Value)
                    Data.Value = Convert.ToDateTime(r.Cells["Data/Hora"].Value);

                cbEstado.Text = r.Cells["Estado"].Value?.ToString() ?? "Pendente";
                txtNotas.Text = r.Cells["Notas"].Value?.ToString() ?? "";

                // 3. IMPORTAÇÃO DO TOTAL PARA A LABEL/TEXTBOX
                // Se usaste AS [Total] na Query, temos de procurar por "Total"
                if (r.Cells["Total"].Value != DBNull.Value)
                {
                    decimal total = Convert.ToDecimal(r.Cells["Total"].Value);
                    // txtValorTotal é a tua "label" ou TextBox de preço
                    txtValorTotal.Text = total.ToString("0.00") + " €";
                }

                // 4. IMPORTAÇÃO DO FUNCIONÁRIO PARA A COMBOBOX
                // Procuramos o nome que está na coluna "Funcionário" e selecionamos na Combo
                string nomeFuncionarioGrelha = r.Cells["Funcionário"].Value?.ToString();
                string nomePackGrelha = r.Cells["Pack"].Value?.ToString();

                AtualizarSelecaoCombosPelaGrelha(nomePackGrelha, nomeFuncionarioGrelha);

                // 5. Carregar ligações (CheckboxLists de Serviços e Boxes)
                CarregarLigacoesDoAgendamento(idAgendamentoSelecionado);

                // 6. Atualizar botões
                btnFaturacao.Enabled = (cbEstado.Text == "Concluído");
                btnCheckIn.Enabled = (cbEstado.Text != "Concluído" && cbEstado.Text != "Cancelado");
            }
        }

        // Função auxiliar para sincronizar o texto da grelha com as ComboBoxes
        private void AtualizarSelecaoCombosPelaGrelha(string nomePack, string nomeFunc)
        {
            // Sincroniza o Pack
            foreach (ComboItem item in cbPack.Items)
            {
                if (item.Texto == nomePack) { cbPack.SelectedItem = item; break; }
            }

            // Sincroniza o Funcionário (A tua ComboBox chama-se cbFuncionario)
            foreach (ComboItem item in cbFuncionario.Items)
            {
                if (item.Texto == nomeFunc)
                {
                    cbFuncionario.SelectedItem = item;
                    break;
                }
            }
        }

        private void CarregarLigacoesDoAgendamento(int idAgend)
        {
            for (int i = 0; i < clbServicos.Items.Count; i++) clbServicos.SetItemChecked(i, false);
            for (int i = 0; i < clbBoxes.Items.Count; i++) clbBoxes.SetItemChecked(i, false);
            cbPack.SelectedIndex = 0;
            cbFuncionario.SelectedIndex = 0;

            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Cod_Pack FROM DETAIL_AUTOMOVEL.AGENDAMENTO_PACK WHERE Num_Agend=@id", CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idAgend);
                        object val = cmd.ExecuteScalar();
                        if (val != null)
                        {
                            foreach (ComboItem item in cbPack.Items)
                                if (item.Id == (int)val) { cbPack.SelectedItem = item; break; }
                        }
                    }

                    AtualizarServicosPorPack();

                    using (SqlCommand cmd = new SqlCommand("SELECT Cod_Servico FROM DETAIL_AUTOMOVEL.AGENDAMENTO_SERVICO WHERE Num_Agend=@id", CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idAgend);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                int idS = (int)r["Cod_Servico"];
                                for (int i = 0; i < clbServicos.Items.Count; i++)
                                    if (((ItemServico)clbServicos.Items[i]).Id == idS) clbServicos.SetItemChecked(i, true);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT Num_Func FROM DETAIL_AUTOMOVEL.AGENDAMENTO_FUNCIONARIO WHERE Num_Agend=@id", CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idAgend);
                        object val = cmd.ExecuteScalar();
                        if (val != null)
                        {
                            foreach (ComboItem item in cbFuncionario.Items)
                                if (item.Id == (int)val) { cbFuncionario.SelectedItem = item; break; }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT Num_Box FROM DETAIL_AUTOMOVEL.AGENDAMENTO_BOX WHERE Num_Agend=@id ORDER BY Ordem ASC", CN))
                    {
                        cmd.Parameters.AddWithValue("@id", idAgend);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                int idB = (int)r["Num_Box"];
                                for (int i = 0; i < clbBoxes.Items.Count; i++)
                                    if (((ComboItem)clbBoxes.Items[i]).Id == idB) clbBoxes.SetItemChecked(i, true);
                            }
                        }
                    }

                    CalcularTotalEmTempoReal();
                }
                catch (Exception ex) { MessageBox.Show("Erro ao ler ligações: " + ex.Message); }
            }
        }

        private void cbPack_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chama a tua função mágica que já faz o trabalho todo!
            AtualizarServicosPorPack();
        }

        private void CalcularTotalEmTempoReal()
        {
            decimal total = 0;
            int idPack = (cbPack.SelectedItem is ComboItem p) ? p.Id : 0;

            using (SqlConnection CN = new SqlConnection(connectionString))
            {
                try
                {
                    CN.Open();
                    if (idPack > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT Preco_Fixo FROM DETAIL_AUTOMOVEL.PACK WHERE Cod_Pack=@id", CN))
                        {
                            cmd.Parameters.AddWithValue("@id", idPack);
                            total += (decimal)cmd.ExecuteScalar();
                        }
                    }

                    foreach (ItemServico srv in clbServicos.CheckedItems)
                    {
                        string sql = "SELECT Preco_Avulso FROM DETAIL_AUTOMOVEL.SERVICO_INDIVIDUAL WHERE Cod_Servico=@s";
                        if (idPack > 0) sql += " AND Cod_Servico NOT IN (SELECT Cod_Servico FROM DETAIL_AUTOMOVEL.PACK_SERVICO WHERE Cod_Pack=@p)";

                        using (SqlCommand cmd = new SqlCommand(sql, CN))
                        {
                            cmd.Parameters.AddWithValue("@s", srv.Id);
                            if (idPack > 0) cmd.Parameters.AddWithValue("@p", idPack);
                            object res = cmd.ExecuteScalar();
                            if (res != null) total += (decimal)res;
                        }
                    }
                    txtValorTotal.Text = total.ToString("0.00") + " €";
                }
                catch { }
            }
        }

        private void AtualizarServicosPorPack()
        {
            for (int i = 0; i < clbServicos.Items.Count; i++) clbServicos.SetItemChecked(i, false);

            if (cbPack.SelectedItem is ComboItem pack && pack.Id > 0)
            {
                using (SqlConnection CN = new SqlConnection(connectionString))
                {
                    try
                    {
                        CN.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Cod_Servico FROM DETAIL_AUTOMOVEL.PACK_SERVICO WHERE Cod_Pack=@id", CN))
                        {
                            cmd.Parameters.AddWithValue("@id", pack.Id);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    int idS = (int)r["Cod_Servico"];
                                    for (int i = 0; i < clbServicos.Items.Count; i++)
                                        if (((ItemServico)clbServicos.Items[i]).Id == idS) clbServicos.SetItemChecked(i, true);
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            CalcularTotalEmTempoReal();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // 1. Volta ao Modo "Criar Novo" (ID 0 indica que ainda não existe no SQL)
            idAgendamentoSelecionado = 0;

            // 2. MÁGICA DA MATRÍCULA:
            // Se estivermos a pesquisar um carro específico, mantemos a matrícula preenchida 
            // para facilitar o registo de um novo serviço para o mesmo carro.
            if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                txtMatricula_Agend.Clear();
            }
            else
            {
                txtMatricula_Agend.Text = txtPesquisa.Text;
            }

            // 3. Limpa o resto das informações e reseta datas
            txtNotas.Clear();
            txtValorTotal.Text = "0.00 €";
            Data.Value = DateTime.Now;

            // Reseta as ComboBoxes para os valores padrão
            if (cbPack.Items.Count > 0) cbPack.SelectedIndex = 0;
            if (cbFuncionario.Items.Count > 0) cbFuncionario.SelectedIndex = 0;
            if (cbEstado.Items.Count > 0) cbEstado.SelectedIndex = 0;

            // Desmarca todos os itens das CheckedListBoxes
            for (int i = 0; i < clbServicos.Items.Count; i++) clbServicos.SetItemChecked(i, false);
            for (int i = 0; i < clbBoxes.Items.Count; i++) clbBoxes.SetItemChecked(i, false);

            // 4. BLOQUEIO DE SEGURANÇA:
            // Como é um registo novo que ainda não foi guardado, desativamos as ações futuras
            btnCheckIn.Enabled = false;
            btnFaturacao.Enabled = false;

            // Limpa a seleção azul da grelha
            dgvAgendamentos.ClearSelection();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarAgendamentos(txtPesquisa.Text);
            SelecionarPrimeiroAgendamento();
        }
            
        private void txtPesquisa_TextChanged(object sender, EventArgs e) => CarregarAgendamentos(txtPesquisa.Text);

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // 1. Verificação: Temos algum agendamento selecionado?
            if (idAgendamentoSelecionado == 0)
            {
                MessageBox.Show("Por favor, selecione primeiro um agendamento na tabela para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Confirmação do utilizador
            if (MessageBox.Show("Tem a certeza que deseja eliminar este agendamento?\n\nIsso irá apagar também o registo de Check-In e as seleções de serviços associadas.",
                "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection CN = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        CN.Open();

                        // CHAMADA DA STORED PROCEDURE
                        using (SqlCommand cmd = new SqlCommand("DETAIL_AUTOMOVEL.sp_EliminarAgendamento", CN))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Num_Agend", idAgendamentoSelecionado);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Agendamento eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Atualiza a interface
                        CarregarAgendamentos(txtPesquisa.Text);
                        btnNovo_Click(null, null);
                    }
                    catch (SqlException ex)
                    {
                        // AVISO DIRETO: Bloqueio de Contabilidade (Foreign Key da Fatura)
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("NÃO PODE ELIMINAR: Este agendamento já tem uma FATURA emitida.\n\n" +
                                            "Para apagar este serviço, teria primeiro de anular a fatura correspondente no módulo de Faturação.",
                                            "Bloqueio de Contabilidade", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        // AVISO DA STORED PROCEDURE: Se o estado for Concluído
                        else if (ex.Number >= 50000)
                        {
                            MessageBox.Show(ex.Message, "Bloqueio de Estado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnVoltar_Click(object sender, EventArgs e) => Navegacao.Voltar(this);

        // ========================================================
        // NAVEGAÇÃO PARA O CHECK-IN (COM AUTO-GUARDAR)
        // ========================================================
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            // Se o ID for 0, significa que o utilizador escolheu os serviços mas não clicou em Guardar
            if (idAgendamentoSelecionado == 0)
            {
                // Verifica se pelo menos tem matrícula para podermos guardar
                if (string.IsNullOrWhiteSpace(txtMatricula_Agend.Text))
                {
                    MessageBox.Show("Preencha a matrícula e os dados antes de avançar para o Check-In!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. MÁGICA: Finge que o utilizador clicou no botão Guardar!
                btnGuardar_Click(null, null);

                // 2. Como o Guardar recarrega a grelha, vamos lá "pescar" o ID do agendamento que acabou de ser criado
                foreach (DataGridViewRow row in dgvAgendamentos.Rows)
                {
                    if (row.Cells["Matricula"].Value?.ToString() == txtMatricula_Agend.Text)
                    {
                        // Apanhámos o ID novo!
                        idAgendamentoSelecionado = Convert.ToInt32(row.Cells["ID"].Value);
                        break;
                    }
                }
            }

            // Se mesmo assim o ID for 0 (ex: deu erro ao guardar), aborta a viagem por segurança
            if (idAgendamentoSelecionado == 0) return;

            // 3. Viagem segura para o Check-In com os dados todos corretos!
            CheckIn frmCheckIn = new CheckIn(this.connectionString, idAgendamentoSelecionado);
            Navegacao.Abrir(this, frmCheckIn);
        }
        private void LimparCamposPreservandoMatricula()
        {
            idAgendamentoSelecionado = 0;

            txtNotas.Clear();
            txtValorTotal.Text = "0.00 €";
            Data.Value = DateTime.Now;
            cbPack.SelectedIndex = 0;
            cbFuncionario.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;

            for (int i = 0; i < clbServicos.Items.Count; i++) clbServicos.SetItemChecked(i, false);
            for (int i = 0; i < clbBoxes.Items.Count; i++) clbBoxes.SetItemChecked(i, false);
        }

        private void Agendamentos_Activated(object sender, EventArgs e)
        {
            // 1. Recarrega a grelha com o filtro atual
            CarregarAgendamentos(txtPesquisa.Text);

            // 2. Tenta re-selecionar o que estava aberto
            if (idAgendamentoSelecionado > 0)
            {
                foreach (DataGridViewRow row in dgvAgendamentos.Rows)
                {
                    // MUDANÇA AQUI: Agora a coluna chama-se "ID" por causa do SQL Alias
                    if (row.Cells["ID"].Value != DBNull.Value && Convert.ToInt32(row.Cells["ID"].Value) == idAgendamentoSelecionado)
                    {
                        row.Selected = true;
                        // Dispara o clique para atualizar as boxes
                        dgvAgendamentos_CellClick(dgvAgendamentos, new DataGridViewCellEventArgs(0, row.Index));
                        break;
                    }
                }
            }
        }

        private void btnFaturacao_Click(object sender, EventArgs e)
        {
            // 1. MÁGICA: Força a gravação para garantir que a BD sabe que o estado é "Concluído"
            btnGuardar_Click(null, null);

            // 2. Só avança para a Faturação se efetivamente guardou com sucesso e está Concluído
            if (idAgendamentoSelecionado > 0 && cbEstado.Text == "Concluído")
            {
                Faturacao frmFatura = new Faturacao(this.connectionString, idAgendamentoSelecionado);
                Navegacao.Abrir(this, frmFatura);
            }
            else
            {
                MessageBox.Show("O Agendamento precisa de ser guardado com o estado 'Concluído' antes de poder ser faturado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




    }
}

// ========================================================
// CLASSES AUXILIARES (Ficam fora da classe do Form, mas dentro do namespace)
// ========================================================
    public class ItemServico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public override string ToString() => Nome;
    }

    public class ComboItem
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public override string ToString() => Texto;
    }