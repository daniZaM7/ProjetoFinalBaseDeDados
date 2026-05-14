namespace WinFormsApp4
{
    partial class Clientes
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
            nome = new Label();
            nif = new Label();
            contacto = new Label();
            email = new Label();
            txtNome = new TextBox();
            txtNIF = new TextBox();
            txtContacto = new TextBox();
            txtEmail = new TextBox();
            txtCor = new TextBox();
            txtModelo = new TextBox();
            txtMarca = new TextBox();
            txtMatricula = new TextBox();
            cor = new Label();
            modelo = new Label();
            marca = new Label();
            matricula = new Label();
            dgvClientes = new DataGridView();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNovo = new Button();
            rbAutomovel = new RadioButton();
            rbMotociclo = new RadioButton();
            label1 = new Label();
            txtNumLugares = new TextBox();
            txtTipoEstofos = new TextBox();
            txtTipoAutomovel = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtCilindrada = new TextBox();
            txtTipoMotociclo = new TextBox();
            Cilindrada = new Label();
            Tipo_Motociclo = new Label();
            pnlAutomovel = new Panel();
            pnlMotociclo = new Panel();
            btnIrParaAgendamento = new Button();
            btnVoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            pnlAutomovel.SuspendLayout();
            pnlMotociclo.SuspendLayout();
            SuspendLayout();
            // 
            // nome
            // 
            nome.AutoSize = true;
            nome.Location = new Point(62, 349);
            nome.Name = "nome";
            nome.Size = new Size(40, 15);
            nome.TabIndex = 8;
            nome.Text = "Nome";
            // 
            // nif
            // 
            nif.AutoSize = true;
            nif.Location = new Point(62, 384);
            nif.Name = "nif";
            nif.Size = new Size(25, 15);
            nif.TabIndex = 9;
            nif.Text = "NIF";
            // 
            // contacto
            // 
            contacto.AutoSize = true;
            contacto.Location = new Point(62, 418);
            contacto.Name = "contacto";
            contacto.Size = new Size(56, 15);
            contacto.TabIndex = 10;
            contacto.Text = "Contacto";
            // 
            // email
            // 
            email.AutoSize = true;
            email.Location = new Point(62, 450);
            email.Name = "email";
            email.Size = new Size(36, 15);
            email.TabIndex = 11;
            email.Text = "Email";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(138, 346);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(270, 23);
            txtNome.TabIndex = 12;
            // 
            // txtNIF
            // 
            txtNIF.Location = new Point(138, 381);
            txtNIF.Name = "txtNIF";
            txtNIF.Size = new Size(270, 23);
            txtNIF.TabIndex = 13;
            // 
            // txtContacto
            // 
            txtContacto.Location = new Point(138, 415);
            txtContacto.Name = "txtContacto";
            txtContacto.Size = new Size(270, 23);
            txtContacto.TabIndex = 14;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(138, 447);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(270, 23);
            txtEmail.TabIndex = 15;
            // 
            // txtCor
            // 
            txtCor.Location = new Point(550, 447);
            txtCor.Name = "txtCor";
            txtCor.Size = new Size(270, 23);
            txtCor.TabIndex = 23;
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(550, 415);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(270, 23);
            txtModelo.TabIndex = 22;
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(550, 381);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(270, 23);
            txtMarca.TabIndex = 21;
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(550, 346);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.Size = new Size(270, 23);
            txtMatricula.TabIndex = 20;
            // 
            // cor
            // 
            cor.AutoSize = true;
            cor.Location = new Point(474, 450);
            cor.Name = "cor";
            cor.Size = new Size(26, 15);
            cor.TabIndex = 19;
            cor.Text = "Cor";
            // 
            // modelo
            // 
            modelo.AutoSize = true;
            modelo.Location = new Point(474, 418);
            modelo.Name = "modelo";
            modelo.Size = new Size(48, 15);
            modelo.TabIndex = 18;
            modelo.Text = "Modelo";
            // 
            // marca
            // 
            marca.AutoSize = true;
            marca.Location = new Point(474, 384);
            marca.Name = "marca";
            marca.Size = new Size(40, 15);
            marca.TabIndex = 17;
            marca.Text = "Marca";
            // 
            // matricula
            // 
            matricula.AutoSize = true;
            matricula.Location = new Point(474, 349);
            matricula.Name = "matricula";
            matricula.Size = new Size(57, 15);
            matricula.TabIndex = 16;
            matricula.Text = "Matrícula";
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(62, 78);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(1377, 240);
            dgvClientes.TabIndex = 24;
            dgvClientes.CellClick += dgvClientes_CellClick;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(62, 25);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(270, 23);
            txtPesquisa.TabIndex = 25;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(352, 25);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(75, 23);
            btnPesquisar.TabIndex = 26;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click_1;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1129, 559);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 52;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1024, 559);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 51;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(928, 559);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(75, 23);
            btnNovo.TabIndex = 50;
            btnNovo.Text = "NOVO";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // rbAutomovel
            // 
            rbAutomovel.AutoSize = true;
            rbAutomovel.Location = new Point(888, 400);
            rbAutomovel.Name = "rbAutomovel";
            rbAutomovel.Size = new Size(84, 19);
            rbAutomovel.TabIndex = 53;
            rbAutomovel.TabStop = true;
            rbAutomovel.Text = "Automóvel";
            rbAutomovel.UseVisualStyleBackColor = true;
            rbAutomovel.CheckedChanged += rbAutomovel_CheckedChanged;
            // 
            // rbMotociclo
            // 
            rbMotociclo.AutoSize = true;
            rbMotociclo.Location = new Point(888, 425);
            rbMotociclo.Name = "rbMotociclo";
            rbMotociclo.Size = new Size(79, 19);
            rbMotociclo.TabIndex = 54;
            rbMotociclo.TabStop = true;
            rbMotociclo.Text = "Motociclo";
            rbMotociclo.UseVisualStyleBackColor = true;
            rbMotociclo.CheckedChanged += rbMotociclo_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(878, 382);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 55;
            label1.Text = "Tipo Veículo";
            // 
            // txtNumLugares
            // 
            txtNumLugares.Location = new Point(118, 81);
            txtNumLugares.Name = "txtNumLugares";
            txtNumLugares.Size = new Size(270, 23);
            txtNumLugares.TabIndex = 62;
            // 
            // txtTipoEstofos
            // 
            txtTipoEstofos.Location = new Point(118, 47);
            txtTipoEstofos.Name = "txtTipoEstofos";
            txtTipoEstofos.Size = new Size(270, 23);
            txtTipoEstofos.TabIndex = 61;
            // 
            // txtTipoAutomovel
            // 
            txtTipoAutomovel.Location = new Point(118, 12);
            txtTipoAutomovel.Name = "txtTipoAutomovel";
            txtTipoAutomovel.Size = new Size(270, 23);
            txtTipoAutomovel.TabIndex = 60;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 84);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 58;
            label3.Text = "Num_Lugares";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 50);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 57;
            label4.Text = "Tipo_Estofos";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 15);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 56;
            label5.Text = "Tipo_Automóvel";
            // 
            // txtCilindrada
            // 
            txtCilindrada.Location = new Point(117, 70);
            txtCilindrada.Name = "txtCilindrada";
            txtCilindrada.Size = new Size(270, 23);
            txtCilindrada.TabIndex = 67;
            // 
            // txtTipoMotociclo
            // 
            txtTipoMotociclo.Location = new Point(117, 35);
            txtTipoMotociclo.Name = "txtTipoMotociclo";
            txtTipoMotociclo.Size = new Size(270, 23);
            txtTipoMotociclo.TabIndex = 66;
            // 
            // Cilindrada
            // 
            Cilindrada.AutoSize = true;
            Cilindrada.Location = new Point(26, 73);
            Cilindrada.Name = "Cilindrada";
            Cilindrada.Size = new Size(61, 15);
            Cilindrada.TabIndex = 64;
            Cilindrada.Text = "Cilindrada";
            // 
            // Tipo_Motociclo
            // 
            Tipo_Motociclo.AutoSize = true;
            Tipo_Motociclo.Location = new Point(16, 38);
            Tipo_Motociclo.Name = "Tipo_Motociclo";
            Tipo_Motociclo.Size = new Size(90, 15);
            Tipo_Motociclo.TabIndex = 63;
            Tipo_Motociclo.Text = "Tipo_Motociclo";
            // 
            // pnlAutomovel
            // 
            pnlAutomovel.Controls.Add(txtTipoAutomovel);
            pnlAutomovel.Controls.Add(label5);
            pnlAutomovel.Controls.Add(txtNumLugares);
            pnlAutomovel.Controls.Add(label4);
            pnlAutomovel.Controls.Add(txtTipoEstofos);
            pnlAutomovel.Controls.Add(label3);
            pnlAutomovel.Location = new Point(1002, 352);
            pnlAutomovel.Name = "pnlAutomovel";
            pnlAutomovel.Size = new Size(406, 118);
            pnlAutomovel.TabIndex = 69;
            // 
            // pnlMotociclo
            // 
            pnlMotociclo.Controls.Add(txtCilindrada);
            pnlMotociclo.Controls.Add(Tipo_Motociclo);
            pnlMotociclo.Controls.Add(Cilindrada);
            pnlMotociclo.Controls.Add(txtTipoMotociclo);
            pnlMotociclo.Location = new Point(1002, 349);
            pnlMotociclo.Name = "pnlMotociclo";
            pnlMotociclo.Size = new Size(406, 128);
            pnlMotociclo.TabIndex = 70;
            pnlMotociclo.Paint += pnlMotociclo_Paint;
            // 
            // btnIrParaAgendamento
            // 
            btnIrParaAgendamento.Location = new Point(197, 539);
            btnIrParaAgendamento.Name = "btnIrParaAgendamento";
            btnIrParaAgendamento.Size = new Size(104, 24);
            btnIrParaAgendamento.TabIndex = 71;
            btnIrParaAgendamento.Text = "Agendamento";
            btnIrParaAgendamento.UseVisualStyleBackColor = true;
            btnIrParaAgendamento.Click += btnIrParaAgendamento_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(1449, 42);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(75, 23);
            btnVoltar.TabIndex = 72;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // Clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1554, 637);
            Controls.Add(btnVoltar);
            Controls.Add(btnIrParaAgendamento);
            Controls.Add(pnlMotociclo);
            Controls.Add(pnlAutomovel);
            Controls.Add(label1);
            Controls.Add(rbMotociclo);
            Controls.Add(rbAutomovel);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNovo);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(dgvClientes);
            Controls.Add(txtCor);
            Controls.Add(txtModelo);
            Controls.Add(txtMarca);
            Controls.Add(txtMatricula);
            Controls.Add(cor);
            Controls.Add(modelo);
            Controls.Add(marca);
            Controls.Add(matricula);
            Controls.Add(txtEmail);
            Controls.Add(txtContacto);
            Controls.Add(txtNIF);
            Controls.Add(txtNome);
            Controls.Add(email);
            Controls.Add(contacto);
            Controls.Add(nif);
            Controls.Add(nome);
            Name = "Clientes";
            Text = "Clientes";
            WindowState = FormWindowState.Maximized;
            Load += Clientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            pnlAutomovel.ResumeLayout(false);
            pnlAutomovel.PerformLayout();
            pnlMotociclo.ResumeLayout(false);
            pnlMotociclo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nome;
        private Label nif;
        private Label contacto;
        private Label email;
        private TextBox txtNome;
        private TextBox txtNIF;
        private TextBox txtContacto;
        private TextBox txtEmail;
        private TextBox txtCor;
        private TextBox txtModelo;
        private TextBox txtMarca;
        private TextBox txtMatricula;
        private Label cor;
        private Label modelo;
        private Label marca;
        private Label matricula;
        private DataGridView dgvClientes;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNovo;
        private RadioButton rbAutomovel;
        private RadioButton rbMotociclo;
        private Label label1;
        private TextBox txtNumLugares;
        private TextBox txtTipoEstofos;
        private TextBox txtTipoAutomovel;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtCilindrada;
        private TextBox txtTipoMotociclo;
        private Label Cilindrada;
        private Label Tipo_Motociclo;
        private Panel pnlAutomovel;
        private Panel pnlMotociclo;
        private Button btnIrParaAgendamento;
        private Button btnVoltar;
    }
}