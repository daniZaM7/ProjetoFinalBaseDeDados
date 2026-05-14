namespace WinFormsApp4
{
    partial class Menu
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
            btnClientes = new Button();
            btnAgendamentos = new Button();
            btnProdutos = new Button();
            btnServicos = new Button();
            btnPacks = new Button();
            btnFuncionarios = new Button();
            btnBoxes = new Button();
            btnCheckIn = new Button();
            txtFaturacao = new Button();
            SuspendLayout();
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(224, 89);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(75, 23);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnAgendamentos
            // 
            btnAgendamentos.Location = new Point(201, 150);
            btnAgendamentos.Name = "btnAgendamentos";
            btnAgendamentos.Size = new Size(98, 24);
            btnAgendamentos.TabIndex = 1;
            btnAgendamentos.Text = "Agendamentos";
            btnAgendamentos.UseVisualStyleBackColor = true;
            btnAgendamentos.Click += btnAgendamentos_Click;
            // 
            // btnProdutos
            // 
            btnProdutos.Location = new Point(700, 322);
            btnProdutos.Name = "btnProdutos";
            btnProdutos.Size = new Size(75, 23);
            btnProdutos.TabIndex = 2;
            btnProdutos.Text = "Produtos";
            btnProdutos.UseVisualStyleBackColor = true;
            btnProdutos.Click += btnProdutos_Click;
            // 
            // btnServicos
            // 
            btnServicos.Location = new Point(700, 383);
            btnServicos.Name = "btnServicos";
            btnServicos.Size = new Size(75, 23);
            btnServicos.TabIndex = 3;
            btnServicos.Text = "Servicos";
            btnServicos.UseVisualStyleBackColor = true;
            btnServicos.Click += btnServicos_Click;
            // 
            // btnPacks
            // 
            btnPacks.Location = new Point(700, 441);
            btnPacks.Name = "btnPacks";
            btnPacks.Size = new Size(75, 23);
            btnPacks.TabIndex = 4;
            btnPacks.Text = "Packs";
            btnPacks.UseVisualStyleBackColor = true;
            // 
            // btnFuncionarios
            // 
            btnFuncionarios.Location = new Point(1115, 107);
            btnFuncionarios.Name = "btnFuncionarios";
            btnFuncionarios.Size = new Size(98, 24);
            btnFuncionarios.TabIndex = 6;
            btnFuncionarios.Text = "Funcionários";
            btnFuncionarios.UseVisualStyleBackColor = true;
            btnFuncionarios.Click += btnFuncionarios_Click;
            // 
            // btnBoxes
            // 
            btnBoxes.Location = new Point(1115, 171);
            btnBoxes.Name = "btnBoxes";
            btnBoxes.Size = new Size(98, 24);
            btnBoxes.TabIndex = 7;
            btnBoxes.Text = "Box";
            btnBoxes.UseVisualStyleBackColor = true;
            btnBoxes.Click += btnBoxes_Click;
            // 
            // btnCheckIn
            // 
            btnCheckIn.Location = new Point(201, 207);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(98, 24);
            btnCheckIn.TabIndex = 8;
            btnCheckIn.Text = "Check-In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // txtFaturacao
            // 
            txtFaturacao.Location = new Point(213, 331);
            txtFaturacao.Name = "txtFaturacao";
            txtFaturacao.Size = new Size(75, 23);
            txtFaturacao.TabIndex = 9;
            txtFaturacao.Text = "Faturacao";
            txtFaturacao.UseVisualStyleBackColor = true;
            txtFaturacao.Click += txtFaturacao_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1523, 562);
            Controls.Add(txtFaturacao);
            Controls.Add(btnCheckIn);
            Controls.Add(btnBoxes);
            Controls.Add(btnFuncionarios);
            Controls.Add(btnPacks);
            Controls.Add(btnServicos);
            Controls.Add(btnProdutos);
            Controls.Add(btnAgendamentos);
            Controls.Add(btnClientes);
            Name = "Menu";
            Text = "Menu";
            WindowState = FormWindowState.Maximized;
            Click += btnPacks_Click;
            ResumeLayout(false);
        }

        #endregion

        private Button btnClientes;
        private Button btnAgendamentos;
        private Button btnProdutos;
        private Button btnServicos;
        private Button btnPacks;
        private Button btnFuncionarios;
        private Button btnBoxes;
        private Button btnCheckIn;
        private Button txtFaturacao;
    }
}