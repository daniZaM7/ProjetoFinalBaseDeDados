namespace WinFormsApp4
{
    partial class Packs
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
            txtNomePack = new TextBox();
            txtPrecoPack = new TextBox();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            lstDisponiveis = new ListBox();
            lstSelecionados = new ListBox();
            label1 = new Label();
            label2 = new Label();
            dgvPacks = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            btnAdicionar = new Button();
            btnRemover = new Button();
            btnVoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPacks).BeginInit();
            SuspendLayout();
            // 
            // txtNomePack
            // 
            txtNomePack.Location = new Point(227, 422);
            txtNomePack.Name = "txtNomePack";
            txtNomePack.Size = new Size(100, 23);
            txtNomePack.TabIndex = 0;
            // 
            // txtPrecoPack
            // 
            txtPrecoPack.Location = new Point(227, 484);
            txtPrecoPack.Name = "txtPrecoPack";
            txtPrecoPack.Size = new Size(100, 23);
            txtPrecoPack.TabIndex = 1;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1431, 582);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 58;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1326, 582);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 57;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1230, 582);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 56;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // lstDisponiveis
            // 
            lstDisponiveis.FormattingEnabled = true;
            lstDisponiveis.ItemHeight = 15;
            lstDisponiveis.Location = new Point(562, 413);
            lstDisponiveis.Name = "lstDisponiveis";
            lstDisponiveis.Size = new Size(120, 94);
            lstDisponiveis.TabIndex = 59;
            // 
            // lstSelecionados
            // 
            lstSelecionados.FormattingEnabled = true;
            lstSelecionados.ItemHeight = 15;
            lstSelecionados.Location = new Point(835, 413);
            lstSelecionados.Name = "lstSelecionados";
            lstSelecionados.Size = new Size(120, 94);
            lstSelecionados.TabIndex = 60;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(137, 430);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 61;
            label1.Text = "Nome Pack";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(137, 487);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 62;
            label2.Text = "Preço Pack";
            // 
            // dgvPacks
            // 
            dgvPacks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPacks.Location = new Point(106, 47);
            dgvPacks.Name = "dgvPacks";
            dgvPacks.Size = new Size(1049, 294);
            dgvPacks.TabIndex = 63;
            dgvPacks.CellClick += dgvPacks_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(562, 378);
            label3.Name = "label3";
            label3.Size = new Size(113, 15);
            label3.TabIndex = 64;
            label3.Text = "Serviços Disponíveis";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(835, 378);
            label4.Name = "label4";
            label4.Size = new Size(122, 15);
            label4.TabIndex = 65;
            label4.Text = "Serviços Selecionados";
            // 
            // btnAdicionar
            // 
            btnAdicionar.Location = new Point(722, 426);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(75, 23);
            btnAdicionar.TabIndex = 66;
            btnAdicionar.Text = ">>";
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Click += btnAdicionar_Click;
            // 
            // btnRemover
            // 
            btnRemover.Location = new Point(722, 468);
            btnRemover.Name = "btnRemover";
            btnRemover.Size = new Size(75, 23);
            btnRemover.TabIndex = 67;
            btnRemover.Text = "<<";
            btnRemover.UseVisualStyleBackColor = true;
            btnRemover.Click += btnRemover_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1404, 70);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 68;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // Packs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1587, 652);
            Controls.Add(btnVoltar);
            Controls.Add(btnRemover);
            Controls.Add(btnAdicionar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dgvPacks);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lstSelecionados);
            Controls.Add(lstDisponiveis);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(txtPrecoPack);
            Controls.Add(txtNomePack);
            Name = "Packs";
            Text = "Packs";
            WindowState = FormWindowState.Maximized;
            Load += Packs_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPacks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNomePack;
        private TextBox txtPrecoPack;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private ListBox lstDisponiveis;
        private ListBox lstSelecionados;
        private Label label1;
        private Label label2;
        private DataGridView dgvPacks;
        private Label label3;
        private Label label4;
        private Button btnAdicionar;
        private Button btnRemover;
        private Button btnVoltar;
    }
}