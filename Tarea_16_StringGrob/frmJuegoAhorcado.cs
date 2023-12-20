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
    
    //Tarea 17- juego del ahorcado- equipo Eliana Lucas y Amelie Grob
    public partial class frmJuegoAhorcado : Form
    {
        private const int MAX = 1000;
        private string[] arrayPalabras;
        private int totalElementos = 0;
        private PictureBox[] pictureBoxes;//para poder usar las imágenes para el muñequito de ahorcado

        public frmJuegoAhorcado()
        {
            InitializeComponent();
            pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };//Para realizar y acomodar los picture box, esta actividad supongo que es para la siguiente clase
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//cierra todas las ventanas abiertas
        }

        private void MuestraCategoria(string categoria)
        {
            this.lblCategoria.Text = categoria;
        }
        private void LeerArchivoTexto(string nombreArchivo)//Lee archivos de texto ingresado por selecciona archivo
        {
            arrayPalabras = new string[MAX];
            int conteo = 0;
            totalElementos = 0; // Restablecer el contador, este es importante, ya que sin este se crean vacios en la ejecución  

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
            string categoria = "Nombres propios";//Cambia el lblCategorias a nombres propios
            MuestraCategoria(categoria);
            SeleccionarArchivo();//selecciona el archivo correspondiente
        }

        private void animalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Animales";//Cambia el lblCategorias a animalse
            MuestraCategoria(categoria);
            SeleccionarArchivo();//selecciona el archivo correspondiente
        }

        private void ciudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = "Ciudades";//Cambia el lblCategorias a ciudades
            MuestraCategoria(categoria);
            SeleccionarArchivo();//selecciona el archivo correspondiente
        }

        private void SeleccionarArchivo()//Selecciona los archivos correspondientes para cada categoría
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
        private int GeneraAleatorios(int totalElementos)//genera los aleatorios que posteriormente será la palabra que hya que adivinar
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var numeroAleatorio = random.Next(0, totalElementos);
            return numeroAleatorio;
        }

        

        private void nuevoJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimpiarJuegoAnterior(); // Agrega este método para limpiar los recursos del juego anterior

            if (totalElementos > 0)
            {
                int indiceAleatorio = GeneraAleatorios(totalElementos);
                this.lblPalabra.Text = arrayPalabras[indiceAleatorio];
                MustraFrase(this.lblPalabra.Text);
            }
            else
            {
                MessageBox.Show("No hay palabras cargadas. Abra una categoría primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MustraFrase(string frase)
        {
            this.groupBoxFraseAdivinar.Controls.Clear();
            TextBox[] palabras =new TextBox[frase.Length];
            int cont =0, x=15,y=27;    

            foreach(char c in frase )
            {
                palabras[cont] = new TextBox();
                palabras[cont].Size = new Size(80, 100);
                palabras[cont].TextAlign = HorizontalAlignment.Center;
                palabras[cont].MaxLength = 1;
                palabras[cont].Multiline = true;
                palabras[cont].ReadOnly = true;
                Font fuente = new Font("Calibrí", 18);
                palabras[cont].Font = fuente;
                palabras[cont].Text = c.ToString();
                palabras[cont].Location = new Point(x, y);
                x += 82;
                this.groupBoxFraseAdivinar.Controls.Add(palabras[cont]);
            }
        }
        private void LimpiarJuegoAnterior()
        {
            // Restablecer variables y limpiar controles aquí
            this.lblPalabra.Text = string.Empty;

        }

    }
}
