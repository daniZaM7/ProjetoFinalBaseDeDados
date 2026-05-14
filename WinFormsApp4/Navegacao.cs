using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public static class Navegacao
    {
        // A "memória" da nossa aplicação (guarda o histórico de janelas)
        private static Stack<Form> historico = new Stack<Form>();

        // Variável de controlo para não fechar o programa ao clicar em Voltar
        private static bool aVoltar = false;

        // FUNÇÃO GENÉRICA PARA ABRIR NOVOS ECRÃS
        public static void Abrir(Form janelaAtual, Form novaJanela)
        {
            historico.Push(janelaAtual); // Guarda a janela atual no histórico
            novaJanela.Show();           // Mostra a nova
            janelaAtual.Hide();          // Esconde a atual

            // Este código vigia o "X" vermelho de todas as janelas que abrires!
            novaJanela.FormClosing += (sender, e) =>
            {
                // Se fechou no X (UserClosing) e não foi no botão "Voltar"
                if (e.CloseReason == CloseReason.UserClosing && !aVoltar)
                {
                    Application.Exit(); // Mata a aplicação de vez para não ficarem zombies
                }
            };
        }

        // FUNÇÃO GENÉRICA PARA VOLTAR ATRÁS
        public static void Voltar(Form janelaAtual)
        {
            if (historico.Count > 0)
            {
                aVoltar = true; // Avisa o sistema que estamos a voltar propositadamente
                Form janelaAnterior = historico.Pop(); // Tira a última janela da pilha
                janelaAnterior.Show();                 // Mostra-a
                janelaAtual.Close();                   // Fecha e limpa a atual da memória
                aVoltar = false; // Reseta para a próxima vez
            }
            else
            {
                // Prevenção de erro crasso
                Application.Exit();
            }
        }
    }
}