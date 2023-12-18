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
        public frmJuegoAhorcado()
        {
            InitializeComponent();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MuestraCategoria(string categoria)
        {
            this.lblCategoria.Text = categoria;
        }

        private void nombresPropiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Nombres pripios";
            MuestraCategoria(categoria);

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

            void LeerArchivoTexto(string nombreArchivo)
            {
                arrayPalabras = new string[MAX];
                int conteo = 0, totalElementos;

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

            
        }
        private int GeneraAleatorios(in totalElementos)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var numeroAleatorio = random.Next(0, totalElementos);
            return numeroAleatorio;
        }

        private void nuevoJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.lblPalabra.Text = arrayPalabras[GeneraAleatorios];
        }
    }
}
