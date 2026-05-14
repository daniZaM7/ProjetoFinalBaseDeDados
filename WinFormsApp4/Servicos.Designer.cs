namespace WinFormsApp4
{
    partial class Servicos
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
            dgvServicos = new DataGridView();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            btnPesquisar = new Button();
            txtPesquisa = new TextBox();
            label5 = new Label();
            label4 = new Label();
            Nome = new Label();
            Código = new Label();
            numTempo = new NumericUpDown();
            txtNomeServico = new TextBox();
            txtPreco = new TextBox();
            txtCodServico = new TextBox();
            btnVoltar = new Button();
            cbProdutos = new ComboBox();
            numQtd = new NumericUpDown();
            btnAdicionarProduto = new Button();
            dgvConsumo = new DataGridView();
            btnRemoverProduto = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvServicos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTempo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQtd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvConsumo).BeginInit();
            SuspendLayout();
            // 
            // dgvServicos
            // 
            dgvServicos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServicos.Location = new Point(76, 34);
            dgvServicos.Name = "dgvServicos";
            dgvServicos.Size = new Size(1091, 275);
            dgvServicos.TabIndex = 0;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1323, 497);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 55;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1218, 497);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 54;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1122, 497);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 53;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(248, 4);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 67;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(110, 5);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(100, 23);
            txtPesquisa.TabIndex = 66;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(585, 399);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 77;
            label5.Text = "Preço";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(585, 358);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 76;
            label4.Text = "Tempo";
            // 
            // Nome
            // 
            Nome.AutoSize = true;
            Nome.Location = new Point(215, 407);
            Nome.Name = "Nome";
            Nome.Size = new Size(40, 15);
            Nome.TabIndex = 74;
            Nome.Text = "Nome";
            // 
            // Código
            // 
            Código.AutoSize = true;
            Código.Location = new Point(215, 360);
            Código.Name = "Código";
            Código.Size = new Size(46, 15);
            Código.TabIndex = 73;
            Código.Text = "Código";
            // 
            // numTempo
            // 
            numTempo.Location = new Point(650, 358);
            numTempo.Name = "numTempo";
            numTempo.Size = new Size(120, 23);
            numTempo.TabIndex = 72;
            // 
            // txtNomeServico
            // 
            txtNomeServico.Location = new Point(297, 399);
            txtNomeServico.Name = "txtNomeServico";
            txtNomeServico.Size = new Size(100, 23);
            txtNomeServico.TabIndex = 71;
            // 
            // txtPreco
            // 
            txtPreco.Location = new Point(650, 399);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(100, 23);
            txtPreco.TabIndex = 69;
            // 
            // txtCodServico
            // 
            txtCodServico.Location = new Point(297, 358);
            txtCodServico.Name = "txtCodServico";
            txtCodServico.Size = new Size(100, 23);
            txtCodServico.TabIndex = 68;
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1374, 52);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 78;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // cbProdutos
            // 
            cbProdutos.FormattingEnabled = true;
            cbProdutos.Location = new Point(224, 514);
            cbProdutos.Name = "cbProdutos";
            cbProdutos.Size = new Size(121, 23);
            cbProdutos.TabIndex = 79;
            // 
            // numQtd
            // 
            numQtd.Location = new Point(225, 557);
            numQtd.Name = "numQtd";
            numQtd.Size = new Size(120, 23);
            numQtd.TabIndex = 80;
            // 
            // btnAdicionarProduto
            // 
            btnAdicionarProduto.Location = new Point(409, 514);
            btnAdicionarProduto.Name = "btnAdicionarProduto";
            btnAdicionarProduto.Size = new Size(121, 23);
            btnAdicionarProduto.TabIndex = 81;
            btnAdicionarProduto.Text = "Adicionar Produto";
            btnAdicionarProduto.UseVisualStyleBackColor = true;
            // 
            // dgvConsumo
            // 
            dgvConsumo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConsumo.Location = new Point(650, 463);
            dgvConsumo.Name = "dgvConsumo";
            dgvConsumo.Size = new Size(330, 150);
            dgvConsumo.TabIndex = 82;
            // 
            // btnRemoverProduto
            // 
            btnRemoverProduto.Location = new Point(409, 555);
            btnRemoverProduto.Name = "btnRemoverProduto";
            btnRemoverProduto.Size = new Size(121, 23);
            btnRemoverProduto.TabIndex = 83;
            btnRemoverProduto.Text = "Eliminar Produto";
            btnRemoverProduto.UseVisualStyleBackColor = true;
            btnRemoverProduto.Click += btnRemoverProduto_Click;
            // 
            // Servicos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1575, 634);
            Controls.Add(btnRemoverProduto);
            Controls.Add(dgvConsumo);
            Controls.Add(btnAdicionarProduto);
            Controls.Add(numQtd);
            Controls.Add(cbProdutos);
            Controls.Add(btnVoltar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Nome);
            Controls.Add(Código);
            Controls.Add(numTempo);
            Controls.Add(txtNomeServico);
            Controls.Add(txtPreco);
            Controls.Add(txtCodServico);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(dgvServicos);
            Name = "Servicos";
            Text = "Servicos";
            WindowState = FormWindowState.Maximized;
            Load += Servicos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvServicos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTempo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQtd).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvConsumo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvServicos;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private Label label5;
        private Label label4;
        private Label Nome;
        private Label Código;
        private NumericUpDown numTempo;
        private TextBox txtNomeServico;
        private TextBox txtPreco;
        private TextBox txtCodServico;
        private Button btnVoltar;
        private ComboBox cbProdutos;
        private NumericUpDown numQtd;
        private Button btnAdicionarProduto;
        private DataGridView dgvConsumo;
        private Button btnRemoverProduto;
    }
}