using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Jogo_da_memoria
{
    public partial class Form2 : Form
    {
        int Movimentos, Cliques, Cartasencontradas;
        int tagIndex;
        Image[] img = new Image[16];
        List<string> lista = new List<string>();
        int[] tags = new int[2];

        public Form2()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                int tagIndex = int.Parse(string.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = Properties.Resources.Brasileiro_Série_A_HD;
                item.Enabled = true;
            }

            Posicoes();
        }
        private void Posicoes()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                Random rdn = new Random();
                int[] xP = { 26, 144, 262, 380, 498, 616, 734, 852 };
                int[] yP = { 115, 220, 325, 430 };
            Repete:
                var X = xP[rdn.Next(0, xP.Length)];
                var Y = yP[rdn.Next(0, yP.Length)];

                string verificacao = X.ToString() + Y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(X, Y);
                    lista.Add(verificacao);

                }
            }

        }
        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool parEncontrado = false;

            PictureBox pic = (PictureBox)sender;
            Cliques++;

            tagIndex = int.Parse(string.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if (Cliques == 1)
            {
                tags[0] = int.Parse(string.Format("{0}", pic.Tag));
            }
            else if (Cliques == 2)
            {
                Movimentos++;
                lblMovimentos.Text = "Tentativas: " + Movimentos.ToString();
                tags[1] = int.Parse(string.Format("{0}", pic.Tag));
                parEncontrado = checagemPares();
                Desvirar(parEncontrado);
            }
            
        }
        private bool checagemPares()
        {
            Cliques = 0;

            if (tags[0] == tags[1]) { return true; } else { return false; }
        }

        private void Desvirar(bool check)
        {
            Thread.Sleep(500);

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(string.Format("{0}", item.Tag)) == tags[0] ||
                    int.Parse(string.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        Cartasencontradas++;
                    }
                    else
                    {
                        item.Image = Properties.Resources.Brasileiro_Série_A_HD;
                        item.Refresh();

                    }
                }
            }
            Finaljogo();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Finaljogo()
        {
            if (Cartasencontradas == (img.Length * 2) )
            {
                MessageBox.Show("Parabéns, você venceu com " + Movimentos.ToString() + " tentativas", "Jogar novamente");
                DialogResult msg =
                MessageBox.Show("Deseja continuar o  jogo?", "Caixa de pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    Cliques = 0; Movimentos = 0; Cartasencontradas = 0;
                    lista.Clear();
                    Inicio();
                }
                else if (msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado por jogar", "Agradecimento");
                    Application.Exit();
                }
            }
        }
        
    }

}