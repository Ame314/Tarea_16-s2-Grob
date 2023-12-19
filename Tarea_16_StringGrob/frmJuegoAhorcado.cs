using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Text;

namespace Tarea_16_StringGrob
{
    

    public partial class frmJuegoAhorcado : Form
    {
        private const int MAX = 1000;
        private string[] arrayPalabras;
        private int totalElementos = 0;
        private PictureBox[] pictureBoxes;

        public frmJuegoAhorcado()
        {
            InitializeComponent();
            pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MuestraCategoria(string categoria)
        {
            this.lblCategoria.Text = categoria;
        }
        private void LeerArchivoTexto(string nombreArchivo)
        {
            arrayPalabras = new string[MAX];
            int conteo = 0;

            try
            {
                using (var sr = new StreamReader(nombreArchivo, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null && conteo < MAX)
                    {
                        arrayPalabras[conteo] = line;
                        totalElementos++;
                        conteo++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine("Ejecutando finalmente el bloque.");
            }
        }

        private void nombresPropiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Nombres propios";
            MuestraCategoria(categoria);
            SeleccionarArchivo();
        }

        private void SeleccionarArchivo()
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString(),
                Title = "Seleccione el archivo de texto",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivos = openFileDialog1.FileName;
                LeerArchivoTexto(nombreArchivos);
            }
        }
        private int GeneraAleatorios(int totalElementos)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var numeroAleatorio = random.Next(0, totalElementos);
            return numeroAleatorio;
        }

        

        private void nuevoJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (totalElementos > 0)
            {
                int indiceAleatorio = GeneraAleatorios(totalElementos);
                this.lblPalabra.Text = arrayPalabras[indiceAleatorio];
            }
            else
            {
                MessageBox.Show("No hay palabras cargadas. Abra una categoría primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarPosicionImagenes()
        {
            int x = 21;
            int y = 54;

            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Location = new System.Drawing.Point(x, y);
                x += 118;

                if (x > 350)
                {
                    x = 21;
                    y += 103;
                }
            }
        }

        private void animalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Animales";
            MuestraCategoria(categoria);
            SeleccionarArchivo();
        }

        private void ciudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Ciudades";
            MuestraCategoria(categoria);
            SeleccionarArchivo();
        }
    }
}
