namespace WinFormsApp4
{
    partial class Agendamentos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPesquisar = new Button();
            txtPesquisa = new TextBox();
            dgvAgendamentos = new DataGridView();
            matricula = new Label();
            email = new Label();
            nome = new Label();
            btnNovo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            Data = new DateTimePicker();
            cbEstado = new ComboBox();
            txtNotas = new TextBox();
            txtMatricula_Agend = new TextBox();
            matri = new Label();
            btnVoltar = new Button();
            cbFuncionario = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            cbPack = new ComboBox();
            label3 = new Label();
            clbServicos = new CheckedListBox();
            label4 = new Label();
            clbBoxes = new CheckedListBox();
            txtValorTotal = new TextBox();
            label5 = new Label();
            btnCheckIn = new Button();
            btnFaturacao = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).BeginInit();
            SuspendLayout();
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(464, 53);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 45;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(174, 53);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 44;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            // 
            // dgvAgendamentos
            // 
            dgvAgendamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAgendamentos.Location = new Point(174, 106);
            dgvAgendamentos.Name = "dgvAgendamentos";
            dgvAgendamentos.Size = new Size(1169, 240);
            dgvAgendamentos.TabIndex = 43;
            dgvAgendamentos.CellClick += dgvAgendamentos_CellClick;
            // 
            // matricula
            // 
            matricula.AutoSize = true;
            matricula.Location = new Point(197, 459);
            matricula.Name = "matricula";
            matricula.Size = new Size(42, 15);
            matricula.TabIndex = 35;
            matricula.Text = "Estado";
            // 
            // email
            // 
            email.AutoSize = true;
            email.Location = new Point(179, 654);
            email.Name = "email";
            email.Size = new Size(38, 15);
            email.TabIndex = 30;
            email.Text = "Notas";
            // 
            // nome
            // 
            nome.AutoSize = true;
            nome.Location = new Point(187, 408);
            nome.Name = "nome";
            nome.Size = new Size(62, 15);
            nome.TabIndex = 27;
            nome.Text = "Data/Hora";
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1023, 833);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 47;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1119, 833);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 48;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1224, 833);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 49;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // Data
            // 
            Data.Location = new Point(263, 403);
            Data.Name = "Data";
            Data.Size = new Size(200, 23);
            Data.TabIndex = 50;
            // 
            // cbEstado
            // 
            cbEstado.FormattingEnabled = true;
            cbEstado.Location = new Point(263, 456);
            cbEstado.Name = "cbEstado";
            cbEstado.Size = new Size(121, 23);
            cbEstado.TabIndex = 51;
            // 
            // txtNotas
            // 
            txtNotas.Location = new Point(263, 651);
            txtNotas.Multiline = true;
            txtNotas.Name = "txtNotas";
            txtNotas.Size = new Size(1036, 125);
            txtNotas.TabIndex = 52;
            // 
            // txtMatricula_Agend
            // 
            txtMatricula_Agend.Location = new Point(263, 365);
            txtMatricula_Agend.Name = "txtMatricula_Agend";
            txtMatricula_Agend.Size = new Size(190, 23);
            txtMatricula_Agend.TabIndex = 54;
            // 
            // matri
            // 
            matri.AutoSize = true;
            matri.Location = new Point(192, 368);
            matri.Name = "matri";
            matri.Size = new Size(57, 15);
            matri.TabIndex = 55;
            matri.Text = "Matrícula";
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1433, 39);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 56;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // cbFuncionario
            // 
            cbFuncionario.FormattingEnabled = true;
            cbFuncionario.Location = new Point(636, 522);
            cbFuncionario.Name = "cbFuncionario";
            cbFuncionario.Size = new Size(121, 23);
            cbFuncionario.TabIndex = 57;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(912, 541);
            label1.Name = "label1";
            label1.Size = new Size(26, 15);
            label1.TabIndex = 59;
            label1.Text = "Box";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(536, 522);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 60;
            label2.Text = "Funcionário";
            // 
            // cbPack
            // 
            cbPack.FormattingEnabled = true;
            cbPack.Location = new Point(636, 390);
            cbPack.Name = "cbPack";
            cbPack.Size = new Size(121, 23);
            cbPack.TabIndex = 61;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(587, 393);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 62;
            label3.Text = "Pack";
            // 
            // clbServicos
            // 
            clbServicos.FormattingEnabled = true;
            clbServicos.Location = new Point(1039, 380);
            clbServicos.Name = "clbServicos";
            clbServicos.Size = new Size(120, 94);
            clbServicos.TabIndex = 63;
            clbServicos.SelectedIndexChanged += cbPack_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(870, 411);
            label4.Name = "label4";
            label4.Size = new Size(110, 15);
            label4.TabIndex = 64;
            label4.Text = "Serviços Individuais";
            // 
            // clbBoxes
            // 
            clbBoxes.FormattingEnabled = true;
            clbBoxes.Location = new Point(998, 505);
            clbBoxes.Name = "clbBoxes";
            clbBoxes.Size = new Size(181, 94);
            clbBoxes.TabIndex = 65;
            // 
            // txtValorTotal
            // 
            txtValorTotal.Location = new Point(401, 816);
            txtValorTotal.Name = "txtValorTotal";
            txtValorTotal.Size = new Size(100, 23);
            txtValorTotal.TabIndex = 66;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(334, 819);
            label5.Name = "label5";
            label5.Size = new Size(61, 15);
            label5.TabIndex = 67;
            label5.Text = "Valor Final";
            // 
            // btnCheckIn
            // 
            btnCheckIn.Location = new Point(927, 833);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(75, 23);
            btnCheckIn.TabIndex = 68;
            btnCheckIn.Text = "CHECK-IN";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnFaturacao
            // 
            btnFaturacao.Location = new Point(536, 816);
            btnFaturacao.Name = "btnFaturacao";
            btnFaturacao.Size = new Size(75, 23);
            btnFaturacao.TabIndex = 69;
            btnFaturacao.Text = "Faturacao";
            btnFaturacao.UseVisualStyleBackColor = true;
            btnFaturacao.Click += btnFaturacao_Click;
            // 
            // Agendamentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1575, 865);
            Controls.Add(btnFaturacao);
            Controls.Add(btnCheckIn);
            Controls.Add(label5);
            Controls.Add(txtValorTotal);
            Controls.Add(clbBoxes);
            Controls.Add(label4);
            Controls.Add(clbServicos);
            Controls.Add(label3);
            Controls.Add(cbPack);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbFuncionario);
            Controls.Add(btnVoltar);
            Controls.Add(matri);
            Controls.Add(txtMatricula_Agend);
            Controls.Add(txtNotas);
            Controls.Add(cbEstado);
            Controls.Add(Data);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvAgendamentos);
            Controls.Add(matricula);
            Controls.Add(email);
            Controls.Add(nome);
            Name = "Agendamentos";
            Text = "Agendamentos";
            WindowState = FormWindowState.Maximized;
            Activated += Agendamentos_Activated;
            Load += Agendamentos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private DataGridView dgvAgendamentos;
        private Label matricula;
        private Label email;
        private Label nome;
        private Button btnNovo;
        private Button btnGuardar;
        private Button btnEliminar;
        private DateTimePicker Data;
        private ComboBox cbEstado;
        private TextBox txtNotas;
        private TextBox txtMatricula_Agend;
        private Label matri;
        private Button btnVoltar;
        private ComboBox cbFuncionario;
        private Label label1;
        private Label label2;
        private ComboBox cbPack;
        private Label label3;
        private CheckedListBox clbServicos;
        private Label label4;
        private CheckedListBox clbBoxes;
        private TextBox txtValorTotal;
        private Label label5;
        private Button btnCheckIn;
        private Button btnFaturacao;
    }
}