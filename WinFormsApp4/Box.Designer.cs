namespace WinFormsApp4
{
    partial class Box
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
            dgvBoxes = new DataGridView();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            txtNumBox = new TextBox();
            txtTipoBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBoxes).BeginInit();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1381, 21);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 60;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(412, 35);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 59;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(122, 35);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 58;
            // 
            // dgvBoxes
            // 
            dgvBoxes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBoxes.Location = new Point(122, 88);
            dgvBoxes.Name = "dgvBoxes";
            dgvBoxes.Size = new Size(1169, 240);
            dgvBoxes.TabIndex = 57;
            dgvBoxes.CellClick += dgvBoxes_CellClick;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1352, 624);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 63;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1247, 624);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 62;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1151, 624);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 61;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // txtNumBox
            // 
            txtNumBox.Location = new Point(352, 395);
            txtNumBox.Name = "txtNumBox";
            txtNumBox.Size = new Size(100, 23);
            txtNumBox.TabIndex = 64;
            // 
            // txtTipoBox
            // 
            txtTipoBox.Location = new Point(352, 441);
            txtTipoBox.Name = "txtTipoBox";
            txtTipoBox.Size = new Size(100, 23);
            txtTipoBox.TabIndex = 65;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(247, 398);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 66;
            label1.Text = "Numero de Box";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(258, 444);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 67;
            label2.Text = "Tipo de Box";
            // 
            // Box
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1591, 659);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTipoBox);
            Controls.Add(txtNumBox);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(btnVoltar);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvBoxes);
            Name = "Box";
            Text = "Box";
            WindowState = FormWindowState.Maximized;
            Load += Box_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBoxes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVoltar;
        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private DataGridView dgvBoxes;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private TextBox txtNumBox;
        private TextBox txtTipoBox;
        private Label label1;
        private Label label2;
    }
}