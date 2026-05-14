using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void TestDBConnection(string dbServer, string dbName, string userName, string userPass)
        {
            string chaveSegura = "Data Source=" + dbServer + ";Initial Catalog=" + dbName +
                                 ";uid=" + userName + ";password=" + userPass + ";Encrypt=false;Persist Security Info=True;";

            using (SqlConnection CN = new SqlConnection(chaveSegura))
            {
                try
                {
                    CN.Open();
                    if (CN.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Successful connection to database " + CN.Database + " on the " + CN.DataSource + " server", "Connection Test", MessageBoxButtons.OK);

                        // ABRIR O MENU COM O NOVO SISTEMA!
                        Menu menu = new Menu(chaveSegura);
                        Navegacao.Abrir(this, menu);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open connection to database due to the error \r\n" + ex.Message, "Connection Test", MessageBoxButtons.OK);
                }
            }
        }

        // --- Resto dos botões que já tinhas ---
        private void button1_Click(object sender, EventArgs e)
        {
            TestDBConnection(txtServidor.Text, txtUser.Text, txtUser.Text, txtPass.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string chave = "Data Source=" + txtServidor.Text + ";Initial Catalog=" + txtUser.Text + ";uid=" + txtUser.Text + ";password=" + txtPass.Text + ";Encrypt=false;";
            using (SqlConnection CN = new SqlConnection(chave))
            {
                try
                {
                    CN.Open();
                    MessageBox.Show("Ligação baseada no botão 2 testada.", "Table Content");
                }
                catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
    }
}