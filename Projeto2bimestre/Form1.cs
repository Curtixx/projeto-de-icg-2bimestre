using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;

namespace Projeto2bimestre
{
    public partial class Form1 : Form
    {
        int x;
        int y;
        int x1;
        int y1;
        int x2;
        int y2;
        int x3;
        int y3;
        int x4;
        int y4;
        int pontos = 0;
        int pontosRetangulo = 0;
        int pontosTriangulo = 0;
        int pontosPentagono = 0;
        int pontosLosango = 0;
        int largura;
        int altura;
        int ultimaLinha;
        float[] tipoLinha = new float[4];
        int[] corLinha = new int[3];
        string[] dadosDoDesenho;
        string nomeArquivo;

        int raioCirculo;
        int raio1Elipse;
        int raio2Elipse;
        int espessura;
        bool pintar = false;
        bool linha = false;
        bool circulo = false;
        bool elipse = false;
        bool retangulo = false;
        bool triangulo = false;
        bool pentagono = false;
        bool losango = false;
        bool pintarRetangulo = false;
        bool pintarTriangulo = false;
        bool pintarLosango = false;
        bool pintarPentagono = false;
        string desenhoEscolhido;
        
        public Form1()
        {
            InitializeComponent();
        }
        
        public Color CriaCor(int[] cor)
        {
            Color Cor = new Color();
            Cor = Color.FromArgb(cor[0], cor[1], cor[2]);
            return Cor;
        }
        public Pen CriaCaneta()
        {

            Color cor = CriaCor(corLinha);
            return new Pen(cor, espessura);
        }

        public void Ponto(PaintEventArgs e, int x, int y)
        {
            Pen Caneta = CriaCaneta();
            Caneta.DashPattern = tipoLinha;
            e.Graphics.DrawLine(Caneta, x, y, x + 1, y);
        }

        public void Linha(PaintEventArgs e, int x0, int y0, int x1, int y1)
        {

            Pen Caneta = CriaCaneta();
            Caneta.DashPattern = tipoLinha;
            e.Graphics.DrawLine(Caneta, x0, y0, x1, y1);
        }
        public void Circulo(int xc, int yc, int raio, int ti, int tf, PaintEventArgs e)
        {

            for (int teta = 0; teta <= tf; teta++)
            {
                x = (int)(xc + raio * Math.Cos(teta * (180 / Math.PI)));
                y = (int)(yc + raio * Math.Sin(teta * (180 / Math.PI)));
                Ponto(e, x, y);

            }

        }

        public void Elipse(int xc, int yc, int raio, int raio2, int ti, int tf, PaintEventArgs e)
        {
            for (int teta = 0; teta <= tf; teta++)
            {
                x = (int)(xc + raio * Math.Cos(teta * (180 / Math.PI)));
                y = (int)(yc + raio2 * Math.Sin(teta * (180 / Math.PI)));
                Ponto(e, x, y);

            }

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (pintar)
            {
                Ponto(e, x, y);
                pintar = false;
                pontos ++;
            }

            if (pontos == 2)
            {
                Linha(e, x, y, x1, y1);
                pontos = 0;
            }

            if (circulo)
            {
                desenhoEscolhido = "Circulo";
                Circulo(200, 200, raioCirculo, 0, 360, e);
            }
            if (elipse)
            {
                desenhoEscolhido = "Elipse";
                Elipse(500, 200, raio1Elipse, raio2Elipse, 0, 360, e);
            }
            if (pintarRetangulo)
            {
                Ponto(e, x, y);
                pintarRetangulo = false;
                pontosRetangulo++;
                
            }
            if(pontosRetangulo == 4)
            {

                Pen Caneta = CriaCaneta();
                Caneta.DashPattern = tipoLinha;
                e.Graphics.DrawRectangle(Caneta, x, y, altura, largura);
                pontosRetangulo = 0;
            }
            if (pintarTriangulo)
            {
                Ponto(e, x, y);
                pintarTriangulo = false;
                pontosTriangulo++;
            }
            if(pontosTriangulo == 3)
            {
                Linha(e, x, y, x1, y1);
                Linha(e, x1, y1, x2, y2);
                Linha(e, x2, y2, x, y);
                pontosTriangulo = 0;
            }
            if (pintarPentagono)
            {
                Ponto(e, x, y);
                pintarPentagono = false;
                pontosPentagono++;
            }
            if(pontosPentagono == 5)
            {
                Linha(e, x, y, x1, y1);
                Linha(e, x1, y1, x2, y2);
                Linha(e, x2, y2, x3, y3);
                Linha(e, x3, y3, x4, y4);
                Linha(e, x4, y4, x, y);
                pontosPentagono = 0;
            }
            if (pintarLosango)
            {
                Ponto(e, x, y);
                pintarLosango = false;
                pontosLosango++;
            }
            if(pontosLosango == 4)
            {
                Linha(e, x, y, x1, y1);
                Linha(e, x1, y1, x2, y2);
                Linha(e, x2, y2, x3, y3);
                Linha(e, x3, y3, x, y);
                pontosLosango = 0;
            }

        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(linha)
            {
                desenhoEscolhido = "Linha";
                if (pontos > 0)
                {
                    x1 = e.X;
                    y1 = e.Y;
                }
                else
                {
                    x = e.X;
                    y = e.Y;
                }
                pintar = true;
                Invalidate();
            }

            if (retangulo)
            {
                desenhoEscolhido = "Retangulo";
                if(pontosRetangulo == 0)
                {
                    x = e.X;
                    y = e.Y;
                }
                else
                {
                    altura = e.X;
                    largura = e.Y;
                }

                pintarRetangulo = true;
                Invalidate();
            }

            if (triangulo)
            {
                desenhoEscolhido = "Triangulo";
                if(pontosTriangulo == 0)
                {
                    x = e.X;
                    y = e.Y;


                }
                if(pontosTriangulo == 2)
                {
                    x1 = e.X;
                    y1 = e.Y;
                }
                else
                {
                    x2 = e.X;
                    y2 = e.Y;
                }

                pintarTriangulo = true;
                Invalidate();
            }

            if (pentagono)
            {
                desenhoEscolhido = "Pentagono";
                if(pontosPentagono == 0)
                {
                    x = e.X;
                    y = e.Y;
                }
                if(pontosPentagono == 1)
                {
                    x1 = e.X;
                    y1 = e.Y;
                }
                if (pontosPentagono == 2)
                {
                    x2 = e.X;
                    y2 = e.Y;
                }
                if(pontosPentagono == 3)
                {
                    x3 = e.X;
                    y3 = e.Y;
                }
                else
                {
                    x4 = e.X;
                    y4 = e.Y;
                }
                pintarPentagono = true;
                Invalidate();
            }
            if (losango)
            {
                desenhoEscolhido = "Losango";
                if(pontosLosango == 0)
                {
                    x = e.X;
                    y = e.Y;
                }
                if (pontosLosango == 1)
                {
                    x1 = e.X;
                    y1 = e.Y;
                }
                if(pontosLosango == 2)
                {
                    x2 = e.X;
                    y2 = e.Y;
                }
                else
                {
                    x3 = e.X;
                    y3 = e.Y;
                }
                pintarLosango = true;
                Invalidate();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            linha = true;
            circulo = false;
            elipse = false;
            retangulo = false;
            triangulo = false;
            losango = false;
            pentagono = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            raioCirculo = int.Parse(Interaction.InputBox("Defina o raio"));
            circulo = true;
            linha = false;
            elipse = false;
            retangulo = false;
            triangulo = false;
            losango = false;
            pentagono = false;

            Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            raio1Elipse = int.Parse(Interaction.InputBox("Defina o primeiro raio"));
            raio2Elipse = int.Parse(Interaction.InputBox("Defina o regundo raio"));
            elipse = true;
            circulo = false;
            linha = false;
            triangulo = false;
            retangulo = false;
            losango = false;
            pentagono = false;
            Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            retangulo = true;
            linha = false;
            circulo = false;
            elipse = false;
            triangulo = false;
            losango = false;
            pentagono = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            triangulo = true;
            linha = false;
            circulo = false;
            elipse = false;
            retangulo = false;
            losango = false;
            pentagono = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            espessura = int.Parse(Interaction.InputBox("Defina a espeçura!"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string estilo = comboBox1.SelectedItem.ToString();
            if(estilo == "Dashdotdot")
            {
                Array.Resize(ref tipoLinha, 4);
                tipoLinha[0] = 5;
                tipoLinha[1] = 2;
                tipoLinha[2] = 15;
                tipoLinha[3] = 4;
            }
            if(estilo == "Dash")
            {
                Array.Resize(ref tipoLinha, 2);
                tipoLinha[0] = 5;
                tipoLinha[1] = 1;

            }
            if(estilo == "Dashdot")
            {
                Array.Resize(ref tipoLinha, 4);
                tipoLinha[0] = 5;
                tipoLinha[1] = 2;
                tipoLinha[2] = 1;
                tipoLinha[3] = 2;
            }
            if(estilo == "Solid")
            {
                Array.Resize(ref tipoLinha, 1);
                tipoLinha[0] = 1;
            }
            if(estilo == "Dot")
            {
                Array.Resize(ref tipoLinha, 2);
                tipoLinha[0] = 1;
                tipoLinha[1] = 1;

            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            pentagono = true;
            triangulo = false;
            elipse = false;
            losango = false;
            circulo = false;
            linha = false;
            retangulo = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            losango = true;
            pentagono = false;
            triangulo = false;
            elipse = false;
            circulo = false;
            linha = false;
            retangulo = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            corLinha[0] = 0;
            corLinha[1] = 0;
            corLinha[2] = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            corLinha[0] = 255;
            corLinha[1] = 255;
            corLinha[2] = 255;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            corLinha[0] = 128;
            corLinha[1] = 128;
            corLinha[2] = 128;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            corLinha[0] = 255;
            corLinha[1] = 0;
            corLinha[2] = 0;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            corLinha[0] = 255;
            corLinha[1] = 165;
            corLinha[2] = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            corLinha[0] = 0;
            corLinha[1] = 255;
            corLinha[2] = 0;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            corLinha[0] = 0;
            corLinha[1] = 0;
            corLinha[2] = 255;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            corLinha[0] = 128;
            corLinha[1] = 0;
            corLinha[2] = 128;

        }

        private void button18_Click(object sender, EventArgs e)
        {
            corLinha[0] = 255;
            corLinha[1] = 192;
            corLinha[2] = 203;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            nomeArquivo = Interaction.InputBox("De um nome para o arquivo");
            salvarDesenho();
 
        }

        public void salvarDesenho()
        {
            
            String texto = "" + desenhoEscolhido + " " + corLinha[0].ToString() + " " + corLinha[1].ToString() +
                " " + corLinha[2].ToString()+ " " + espessura.ToString() + " " + x.ToString() +
                " " + y.ToString() + " " + x1.ToString() + " " + y1.ToString() + " " + x2.ToString() +
                " " + y2.ToString() + " " + x3.ToString() + " " + y3.ToString() + " " + x4.ToString() +
                " " + y4.ToString() + " " + tipoLinha.Length.ToString() + " ";
            for(int i = 0; i <= tipoLinha.Length -1; i++)
            {
                texto += tipoLinha[i].ToString() + " ";
            }
            File.AppendAllText(@"C:\Users\User\Desktop\"+nomeArquivo.ToString()+".dat", texto);

            if(desenhoEscolhido == "Circulo")
            {
                File.AppendAllText(@"C:\Users\User\Desktop\" + nomeArquivo.ToString() + ".dat", raioCirculo.ToString() + " ");
            }

            if(desenhoEscolhido == "Elipse")
            {
                File.AppendAllText(@"C:\Users\User\Desktop\" + nomeArquivo.ToString() + ".dat", raio1Elipse.ToString() + " " + raio2Elipse.ToString() + " ");

            }
            if(desenhoEscolhido == "Retangulo")
            {
                File.AppendAllText(@"C:\Users\User\Desktop\" + nomeArquivo.ToString() + ".dat", altura.ToString() + " " + largura.ToString() + " ");

            }
            MessageBox.Show("Salvo!!");
        }

        public void carregarDesenho()
        {
            int indice = 0;
            if (File.Exists(@"C:\Users\User\Desktop\" + nomeArquivo.ToString() + ".dat"))
            {
                string[] desenhos = File.ReadAllLines(@"C:\Users\User\Desktop\" + nomeArquivo.ToString() + ".dat");

                if(desenhos.Length > 0)
                {
                    foreach(string desenho in desenhos)
                    {
                        dadosDoDesenho = desenho.Split(' ');

                        if(dadosDoDesenho[0] == "Linha")
                        {
                            x = int.Parse(dadosDoDesenho[5]);
                            y = int.Parse(dadosDoDesenho[6]);
                            x1 = int.Parse(dadosDoDesenho[7]);
                            y1 = int.Parse(dadosDoDesenho[8]);
                            espessura = int.Parse(dadosDoDesenho[4]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for(int i = ultimaLinha; i<= int.Parse(dadosDoDesenho[15]) + ultimaLinha -1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            pontos = 2;
                            Invalidate();
                        }
                        if(dadosDoDesenho[0] == "Circulo")
                        {
                            raioCirculo = int.Parse(dadosDoDesenho[19]);
                            espessura = int.Parse(dadosDoDesenho[4]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            circulo = true;
                            Invalidate();
                        }
                        if(dadosDoDesenho[0] == "Elipse")
                        {
                            raio1Elipse = int.Parse(dadosDoDesenho[19]);
                            raio2Elipse = int.Parse(dadosDoDesenho[20]);
                            espessura = int.Parse(dadosDoDesenho[4]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            elipse = true;
                            Invalidate();
                        }
                        if (dadosDoDesenho[0] == "Triangulo")
                        {
                            x = int.Parse(dadosDoDesenho[5]);
                            y = int.Parse(dadosDoDesenho[6]);
                            x1 = int.Parse(dadosDoDesenho[7]);
                            y1 = int.Parse(dadosDoDesenho[8]);
                            x2 = int.Parse(dadosDoDesenho[9]);
                            y2 = int.Parse(dadosDoDesenho[10]);
                            espessura = int.Parse(dadosDoDesenho[4]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            pontosTriangulo = 3;
                            Invalidate();
                        }
                        if (dadosDoDesenho[0] == "Retangulo")
                        {
                            altura = int.Parse(dadosDoDesenho[17]);
                            largura = int.Parse(dadosDoDesenho[18]);
                            x = int.Parse(dadosDoDesenho[5]);
                            y = int.Parse(dadosDoDesenho[6]);
                            espessura = int.Parse(dadosDoDesenho[4]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            pontosRetangulo = 4;
                            Invalidate();
                        }
                        if(dadosDoDesenho[0] == "Pentagono")
                        {
                            espessura = int.Parse(dadosDoDesenho[4]);
                            x = int.Parse(dadosDoDesenho[5]);
                            y = int.Parse(dadosDoDesenho[6]);
                            x1 = int.Parse(dadosDoDesenho[7]);
                            y1 = int.Parse(dadosDoDesenho[8]);
                            x2 = int.Parse(dadosDoDesenho[9]);
                            y2 = int.Parse(dadosDoDesenho[10]);
                            x3 = int.Parse(dadosDoDesenho[11]);
                            y3 = int.Parse(dadosDoDesenho[12]);
                            x4 = int.Parse(dadosDoDesenho[13]);
                            y4 = int.Parse(dadosDoDesenho[14]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            pontosPentagono = 5;
                            Invalidate();
                        }
                        if(dadosDoDesenho[0] == "Losango")
                        {
                            espessura = int.Parse(dadosDoDesenho[4]);
                            x = int.Parse(dadosDoDesenho[5]);
                            y = int.Parse(dadosDoDesenho[6]);
                            x1 = int.Parse(dadosDoDesenho[7]);
                            y1 = int.Parse(dadosDoDesenho[8]);
                            x2 = int.Parse(dadosDoDesenho[9]);
                            y2 = int.Parse(dadosDoDesenho[10]);
                            x3 = int.Parse(dadosDoDesenho[11]);
                            y3 = int.Parse(dadosDoDesenho[12]);
                            corLinha[0] = int.Parse(dadosDoDesenho[1]);
                            corLinha[1] = int.Parse(dadosDoDesenho[2]);
                            corLinha[2] = int.Parse(dadosDoDesenho[3]);
                            ultimaLinha = 15;
                            Array.Resize(ref tipoLinha, int.Parse(dadosDoDesenho[15]));
                            for (int i = ultimaLinha; i <= int.Parse(dadosDoDesenho[15]) + ultimaLinha - 1; i++)
                            {
                                tipoLinha[indice] = int.Parse(dadosDoDesenho[i]);
                                indice++;
                            }
                            pontosLosango = 4;
                            Invalidate();
                        }



                    }

                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            carregarDesenho();
        }
    }
}
