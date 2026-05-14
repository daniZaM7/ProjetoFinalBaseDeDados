namespace WinFormsApp4
{
    partial class CheckIn
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
            btnVoltar = new Button();
            btnPesquisar = new Button();
            txtPesquisa = new TextBox();
            dgvCheckIn = new DataGridView();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            txtKmsEntrada = new TextBox();
            contacto = new Label();
            txtObservacoes = new TextBox();
            email = new Label();
            cbSujidade = new ComboBox();
            cor = new Label();
            label1 = new Label();
            txtEstadoPintura = new TextBox();
            txtDanos = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCheckIn).BeginInit();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1453, 17);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 60;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(484, 31);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 59;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(194, 31);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 58;
            // 
            // dgvCheckIn
            // 
            dgvCheckIn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCheckIn.Location = new Point(194, 84);
            dgvCheckIn.Name = "dgvCheckIn";
            dgvCheckIn.Size = new Size(1169, 240);
            dgvCheckIn.TabIndex = 57;
            dgvCheckIn.CellClick += dgvCheckIn_CellClick;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1393, 654);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 63;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1288, 654);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 62;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1192, 654);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 61;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // txtKmsEntrada
            // 
            txtKmsEntrada.Location = new Point(293, 350);
            txtKmsEntrada.Name = "txtKmsEntrada";
            txtKmsEntrada.Size = new Size(162, 23);
            txtKmsEntrada.TabIndex = 65;
            // 
            // contacto
            // 
            contacto.AutoSize = true;
            contacto.Location = new Point(193, 358);
            contacto.Name = "contacto";
            contacto.Size = new Size(75, 15);
            contacto.TabIndex = 64;
            contacto.Text = "Kms/Entrada";
            // 
            // txtObservacoes
            // 
            txtObservacoes.Location = new Point(282, 496);
            txtObservacoes.Multiline = true;
            txtObservacoes.Name = "txtObservacoes";
            txtObservacoes.Size = new Size(1036, 125);
            txtObservacoes.TabIndex = 67;
            // 
            // email
            // 
            email.AutoSize = true;
            email.Location = new Point(193, 551);
            email.Name = "email";
            email.Size = new Size(74, 15);
            email.TabIndex = 66;
            email.Text = "Observações";
            // 
            // cbSujidade
            // 
            cbSujidade.FormattingEnabled = true;
            cbSujidade.Location = new Point(334, 398);
            cbSujidade.Name = "cbSujidade";
            cbSujidade.Size = new Size(121, 23);
            cbSujidade.TabIndex = 69;
            // 
            // cor
            // 
            cor.AutoSize = true;
            cor.Location = new Point(194, 401);
            cor.Name = "cor";
            cor.Size = new Size(117, 15);
            cor.TabIndex = 68;
            cor.Text = "Nível Sujidade - 1 a 5";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(626, 363);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 70;
            label1.Text = "Estado Pintura";
            // 
            // txtEstadoPintura
            // 
            txtEstadoPintura.Location = new Point(733, 358);
            txtEstadoPintura.Name = "txtEstadoPintura";
            txtEstadoPintura.Size = new Size(151, 23);
            txtEstadoPintura.TabIndex = 71;
            // 
            // txtDanos
            // 
            txtDanos.Location = new Point(733, 398);
            txtDanos.Name = "txtDanos";
            txtDanos.Size = new Size(151, 23);
            txtDanos.TabIndex = 73;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(643, 401);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 72;
            label2.Text = "Danos";
            // 
            // CheckIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1759, 689);
            Controls.Add(txtDanos);
            Controls.Add(label2);
            Controls.Add(txtEstadoPintura);
            Controls.Add(label1);
            Controls.Add(cbSujidade);
            Controls.Add(cor);
            Controls.Add(txtObservacoes);
            Controls.Add(email);
            Controls.Add(txtKmsEntrada);
            Controls.Add(contacto);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(btnVoltar);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvCheckIn);
            Name = "CheckIn";
            Text = "CheckIn";
            WindowState = FormWindowState.Maximized;
            Load += CheckIn_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCheckIn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVoltar;
        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private DataGridView dgvCheckIn;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private TextBox txtKmsEntrada;
        private Label contacto;
        private CheckedListBox clbBoxes;
        private Label label2;
        private Label label1;
        private ComboBox cbFuncionario;
        private TextBox txtObservacoes;
        private Label email;
        private ComboBox cbSujidade;
        private Label cor;
        private TextBox txtEstadoPintura;
        private TextBox txtDanos;
    }
}