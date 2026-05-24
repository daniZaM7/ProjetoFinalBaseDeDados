namespace WinFormsApp4
{
    partial class Faturacao
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
            dgvFaturacao = new DataGridView();
            cbFiltro = new ComboBox();
            txtNumAgend = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtMatricula = new TextBox();
            label3 = new Label();
            txtNIF = new TextBox();
            label4 = new Label();
            txtCliente = new TextBox();
            label5 = new Label();
            txtValorTotal = new TextBox();
            btnEmitirFatura = new Button();
            btnEliminar = new Button();
            lblFaturacaoMensal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFaturacao).BeginInit();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1398, 22);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 60;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(429, 36);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 59;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(139, 36);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 58;
            // 
            // dgvFaturacao
            // 
            dgvFaturacao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFaturacao.Location = new Point(139, 89);
            dgvFaturacao.Name = "dgvFaturacao";
            dgvFaturacao.Size = new Size(1169, 240);
            dgvFaturacao.TabIndex = 57;
            dgvFaturacao.CellClick += dgvFaturacao_CellClick;
            // 
            // cbFiltro
            // 
            cbFiltro.FormattingEnabled = true;
            cbFiltro.Location = new Point(242, 374);
            cbFiltro.Name = "cbFiltro";
            cbFiltro.Size = new Size(133, 23);
            cbFiltro.TabIndex = 61;
            cbFiltro.SelectedIndexChanged += cbFiltro_SelectedIndexChanged;
            // 
            // txtNumAgend
            // 
            txtNumAgend.Location = new Point(263, 440);
            txtNumAgend.Name = "txtNumAgend";
            txtNumAgend.ReadOnly = true;
            txtNumAgend.Size = new Size(100, 23);
            txtNumAgend.TabIndex = 62;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(188, 448);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 63;
            label1.Text = "NumAgend";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(188, 506);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 65;
            label2.Text = "Matrícula";
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(263, 498);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.ReadOnly = true;
            txtMatricula.Size = new Size(100, 23);
            txtMatricula.TabIndex = 64;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(441, 506);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 69;
            label3.Text = "NIF";
            // 
            // txtNIF
            // 
            txtNIF.Location = new Point(516, 498);
            txtNIF.Name = "txtNIF";
            txtNIF.ReadOnly = true;
            txtNIF.Size = new Size(100, 23);
            txtNIF.TabIndex = 68;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(441, 448);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 67;
            label4.Text = "Cliente";
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(516, 440);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(100, 23);
            txtCliente.TabIndex = 66;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(329, 570);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 71;
            label5.Text = "Valor Total";
            // 
            // txtValorTotal
            // 
            txtValorTotal.Location = new Point(404, 562);
            txtValorTotal.Name = "txtValorTotal";
            txtValorTotal.ReadOnly = true;
            txtValorTotal.Size = new Size(100, 23);
            txtValorTotal.TabIndex = 70;
            // 
            // btnEmitirFatura
            // 
            btnEmitirFatura.Location = new Point(935, 570);
            btnEmitirFatura.Name = "btnEmitirFatura";
            btnEmitirFatura.Size = new Size(97, 23);
            btnEmitirFatura.TabIndex = 72;
            btnEmitirFatura.Text = "Emitir Fatura";
            btnEmitirFatura.UseVisualStyleBackColor = true;
            btnEmitirFatura.Click += btnEmitirFatura_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(829, 570);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 73;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblFaturacaoMensal
            // 
            lblFaturacaoMensal.AutoSize = true;
            lblFaturacaoMensal.Location = new Point(1122, 448);
            lblFaturacaoMensal.Name = "lblFaturacaoMensal";
            lblFaturacaoMensal.Size = new Size(0, 15);
            lblFaturacaoMensal.TabIndex = 74;
            // 
            // Faturacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1598, 659);
            Controls.Add(lblFaturacaoMensal);
            Controls.Add(btnEliminar);
            Controls.Add(btnEmitirFatura);
            Controls.Add(label5);
            Controls.Add(txtValorTotal);
            Controls.Add(label3);
            Controls.Add(txtNIF);
            Controls.Add(label4);
            Controls.Add(txtCliente);
            Controls.Add(label2);
            Controls.Add(txtMatricula);
            Controls.Add(label1);
            Controls.Add(txtNumAgend);
            Controls.Add(cbFiltro);
            Controls.Add(btnVoltar);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvFaturacao);
            Name = "Faturacao";
            Text = "Faturacao";
            WindowState = FormWindowState.Maximized;
            Load += Faturacao_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFaturacao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVoltar;
        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private DataGridView dgvFaturacao;
        private ComboBox cbFiltro;
        private TextBox txtNumAgend;
        private Label label1;
        private Label label2;
        private TextBox txtMatricula;
        private Label label3;
        private TextBox txtNIF;
        private Label label4;
        private TextBox txtCliente;
        private Label label5;
        private TextBox txtValorTotal;
        private Button btnEmitirFatura;
        private Button btnEliminar;
        private Label lblFaturacaoMensal;
    }
}