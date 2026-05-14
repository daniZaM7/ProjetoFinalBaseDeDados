using System;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Menu : Form
    {
        private string connectionString;

        public Menu(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;

            this.btnPacks.Click += new System.EventHandler(this.btnPacks_Click);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes frm = new Clientes(this.connectionString);
            Navegacao.Abrir(this, frm);
        }

        private void btnAgendamentos_Click(object sender, EventArgs e)
        {
            Agendamentos ecraAgendamentos = new Agendamentos(this.connectionString);
            Navegacao.Abrir(this, ecraAgendamentos);
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            Produtos ecraProdutos = new Produtos(this.connectionString);
            Navegacao.Abrir(this, ecraProdutos);
        }

        private void btnServicos_Click(object sender, EventArgs e)
        {
            Servicos ecraServicos = new Servicos(this.connectionString);
            Navegacao.Abrir(this, ecraServicos);
        }

        private void btnPacks_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.connectionString))
                {
                    MessageBox.Show("Erro: A ConnectionString do Menu está vazia!");
                    return;
                }

                Packs ecraPacks = new Packs(this.connectionString);
                Navegacao.Abrir(this, ecraPacks);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Fatal ao instanciar Packs: " + ex.Message);
            }
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            Funcionario ecraFunc = new Funcionario(this.connectionString);
            Navegacao.Abrir(this, ecraFunc);
        }

        private void btnBoxes_Click(object sender, EventArgs e)
        {
            Box ecraBox = new Box(this.connectionString);
            Navegacao.Abrir(this, ecraBox);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            CheckIn ecraCheckIn = new CheckIn(this.connectionString);
            Navegacao.Abrir(this, ecraCheckIn);
        }

        private void txtFaturacao_Click(object sender, EventArgs e)
        {
            Faturacao ecraFaturacao = new Faturacao(this.connectionString);

            // Usamos a tua classe de Navegação para abrir o ecrã
            Navegacao.Abrir(this, ecraFaturacao);
        }
    }
}