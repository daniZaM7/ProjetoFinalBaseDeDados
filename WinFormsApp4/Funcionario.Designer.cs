namespace WinFormsApp4
{
    partial class Funcionario
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
            dgvFuncionarios = new DataGridView();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtNome = new TextBox();
            txtContacto = new TextBox();
            clbEspecialidades = new CheckedListBox();
            cbFuncionario = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFuncionarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbFuncionario).BeginInit();
            SuspendLayout();
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1351, 16);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 60;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(382, 30);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 59;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(92, 30);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 58;
            // 
            // dgvFuncionarios
            // 
            dgvFuncionarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFuncionarios.Location = new Point(92, 83);
            dgvFuncionarios.Name = "dgvFuncionarios";
            dgvFuncionarios.Size = new Size(1169, 240);
            dgvFuncionarios.TabIndex = 57;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1347, 619);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 63;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1242, 619);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 62;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(1146, 619);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 61;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(209, 369);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 64;
            label1.Text = "Nome";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(209, 410);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 65;
            label2.Text = "Contacto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(209, 455);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 66;
            label3.Text = "Especialidade";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(341, 369);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(100, 23);
            txtNome.TabIndex = 67;
            // 
            // txtContacto
            // 
            txtContacto.Location = new Point(341, 410);
            txtContacto.Name = "txtContacto";
            txtContacto.Size = new Size(100, 23);
            txtContacto.TabIndex = 68;
            // 
            // clbEspecialidades
            // 
            clbEspecialidades.FormattingEnabled = true;
            clbEspecialidades.Location = new Point(337, 455);
            clbEspecialidades.Name = "clbEspecialidades";
            clbEspecialidades.Size = new Size(214, 202);
            clbEspecialidades.TabIndex = 70;
            // 
            // cbFuncionario
            // 
            cbFuncionario.Location = new Point(771, 389);
            cbFuncionario.Name = "cbFuncionario";
            cbFuncionario.Size = new Size(120, 23);
            cbFuncionario.TabIndex = 71;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(663, 391);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 72;
            label4.Text = "Horas trabalhadas";
            // 
            // Funcionario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1585, 654);
            Controls.Add(label4);
            Controls.Add(cbFuncionario);
            Controls.Add(clbEspecialidades);
            Controls.Add(txtContacto);
            Controls.Add(txtNome);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(btnVoltar);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvFuncionarios);
            Name = "Funcionario";
            Text = "Funcionario";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvFuncionarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbFuncionario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVoltar;
        private Button btnPesquisar;
        private TextBox txtPesquisa;
        private DataGridView dgvFuncionarios;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtNome;
        private TextBox txtContacto;
        private CheckedListBox clbEspecialidades;
        private NumericUpDown cbFuncionario;
        private Label label4;
    }
}