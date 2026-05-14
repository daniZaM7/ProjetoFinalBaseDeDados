namespace WinFormsApp4
{
    partial class Produtos
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
            dgvProdutos = new DataGridView();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            txtReferencia = new TextBox();
            txtPesquisa = new TextBox();
            txtPreco = new TextBox();
            txtNome = new TextBox();
            txtMarca = new TextBox();
            numStock = new NumericUpDown();
            Referencia = new Label();
            Marca = new Label();
            lblProdutos = new Label();
            Stock = new Label();
            label5 = new Label();
            btnPesquisar = new Button();
            btnVoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            SuspendLayout();
            // 
            // dgvProdutos
            // 
            dgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProdutos.Location = new Point(82, 58);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.Size = new Size(1084, 270);
            dgvProdutos.TabIndex = 0;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1309, 492);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 52;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1204, 492);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 51;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1108, 492);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 50;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            // 
            // txtReferencia
            // 
            txtReferencia.Location = new Point(292, 370);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(100, 23);
            txtReferencia.TabIndex = 53;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(140, 26);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(100, 23);
            txtPesquisa.TabIndex = 55;
            // 
            // txtPreco
            // 
            txtPreco.Location = new Point(645, 411);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(100, 23);
            txtPreco.TabIndex = 56;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(292, 470);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(100, 23);
            txtNome.TabIndex = 57;
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(292, 411);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(100, 23);
            txtMarca.TabIndex = 58;
            // 
            // numStock
            // 
            numStock.Location = new Point(645, 370);
            numStock.Name = "numStock";
            numStock.Size = new Size(120, 23);
            numStock.TabIndex = 59;
            // 
            // Referencia
            // 
            Referencia.AutoSize = true;
            Referencia.Location = new Point(210, 372);
            Referencia.Name = "Referencia";
            Referencia.Size = new Size(62, 15);
            Referencia.TabIndex = 60;
            Referencia.Text = "Referência";
            // 
            // Marca
            // 
            Marca.AutoSize = true;
            Marca.Location = new Point(210, 419);
            Marca.Name = "Marca";
            Marca.Size = new Size(40, 15);
            Marca.TabIndex = 61;
            Marca.Text = "Marca";
            // 
            // lblProdutos
            // 
            lblProdutos.AutoSize = true;
            lblProdutos.Location = new Point(210, 478);
            lblProdutos.Name = "lblProdutos";
            lblProdutos.Size = new Size(55, 15);
            lblProdutos.TabIndex = 62;
            lblProdutos.Text = "Produtos";
            // 
            // Stock
            // 
            Stock.AutoSize = true;
            Stock.Location = new Point(580, 370);
            Stock.Name = "Stock";
            Stock.Size = new Size(36, 15);
            Stock.TabIndex = 63;
            Stock.Text = "Stock";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(580, 411);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 64;
            label5.Text = "Preço";
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(278, 25);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 65;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1337, 65);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 66;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // Produtos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1540, 562);
            Controls.Add(btnVoltar);
            Controls.Add(btnPesquisar);
            Controls.Add(label5);
            Controls.Add(Stock);
            Controls.Add(lblProdutos);
            Controls.Add(Marca);
            Controls.Add(Referencia);
            Controls.Add(numStock);
            Controls.Add(txtMarca);
            Controls.Add(txtNome);
            Controls.Add(txtPreco);
            Controls.Add(txtPesquisa);
            Controls.Add(txtReferencia);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(dgvProdutos);
            Name = "Produtos";
            Text = "Produtos";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProdutos;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private TextBox txtReferencia;
        private TextBox txtPesquisa;
        private TextBox txtPreco;
        private TextBox txtNome;
        private TextBox txtMarca;
        private NumericUpDown numStock;
        private Label Referencia;
        private Label Marca;
        private Label lblProdutos;
        private Label Stock;
        private Label label5;
        private Button btnPesquisar;
        private Button btnVoltar;
    }
}