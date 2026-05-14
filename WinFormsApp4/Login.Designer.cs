namespace WinFormsApp4
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            txtServidor = new TextBox();
            Hello_Click = new Button();
            label3 = new Label();
            label4 = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(42, 218);
            button1.Name = "button1";
            button1.Size = new Size(140, 23);
            button1.TabIndex = 0;
            button1.Text = "Test Connection";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 132);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 2;
            label1.Text = "Password";
            label1.Click += label1_Click;
            // 
            // txtServidor
            // 
            txtServidor.Location = new Point(171, 37);
            txtServidor.Name = "txtServidor";
            txtServidor.Size = new Size(270, 23);
            txtServidor.TabIndex = 3;
            txtServidor.Tag = "";
            txtServidor.Text = "192.168.182.10";
            txtServidor.TextChanged += textBox1_TextChanged;
            // 
            // Hello_Click
            // 
            Hello_Click.Location = new Point(298, 218);
            Hello_Click.Name = "Hello_Click";
            Hello_Click.Size = new Size(143, 23);
            Hello_Click.TabIndex = 5;
            Hello_Click.Text = "Hello Table";
            Hello_Click.UseVisualStyleBackColor = true;
            Hello_Click.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 40);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 7;
            label3.Text = "Server";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 86);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 8;
            label4.Text = "User";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(171, 78);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(270, 23);
            txtUser.TabIndex = 9;
            txtUser.Text = "p3g11";
            txtUser.TextChanged += textBox2_TextChanged;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(171, 124);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(270, 23);
            txtPass.TabIndex = 10;
            txtPass.Text = "danielluis";
            txtPass.TextChanged += textBox3_TextChanged;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 293);
            Controls.Add(txtPass);
            Controls.Add(txtUser);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Hello_Click);
            Controls.Add(txtServidor);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox txtServidor;
        private Button Hello_Click;
        private Label label3;
        private Label label4;
        private TextBox txtUser;
        private TextBox txtPass;
    }
}
