using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Drawing.Text;
using Microsoft.VisualBasic;

namespace Tarea_16_StringGrob
{
    public partial class frmLecturaArchivos : Form
    {
        public frmLecturaArchivos()
        {
            InitializeComponent();
        }
        const int MAX = 1000;
        string[] arrayNombres;
        int totalElementos = 0;
        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
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
                this.txtArchivos.Text = openFileDialog1.FileName;
                this.LeerArchivoTexto(this.txtArchivos.Text);
             }

        }
        private void LeerArchivoTexto(string nombreArchivo)
        {
            String line;
            arrayNombres = new string[MAX];
            int conteo = 0;

            try
            {
                StreamReader sr = new StreamReader(nombreArchivo, Encoding.UTF8);

                line = sr.ReadLine();

                while (line != null)
                {
                    if (conteo < MAX)
                    {
                        arrayNombres[conteo] = line;
                        totalElementos++;
                    }

                    line = sr.ReadLine();
                    conteo++;
                }

                sr.Close();
                Console.ReadLine();
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
        
    

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Opciones de colores
        List<Color> coloresDisponibles = new List<Color>
        {
            Color.LightGray,
            Color.LightBlue,
            Color.LightCoral,
            Color.LightGreen,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightSkyBlue
        };
        private void btnGenerarNom_Click(object sender, EventArgs e)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var Numeroaleatorio = random.Next(0, totalElementos);
            Color colorAleatorio = coloresDisponibles[random.Next(coloresDisponibles.Count)];

            string nombre = arrayNombres[Numeroaleatorio];
            char caracterUsuario;


            if (this.txtCaracterUsuario.Text.Length == 1 && char.TryParse(this.txtCaracterUsuario.Text, out caracterUsuario))
            {
                caracterUsuario = char.ToUpper(caracterUsuario);

                for (int i = 1; i <= 17; i++)
                {
                    string textBoxName = "textBox" + i;
                    TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                    if (textBox != null)
                    {
                        textBox.Clear();

                        if (i <= nombre.Length)
                        {
                            char letraActual = nombre[i - 1];
                            textBox.Text = letraActual.ToString();

                            if (i % 2 == 0)
                            {
                                textBox.BackColor = colorAleatorio;
                            }

                            if (letraActual == caracterUsuario)
                            {
                                textBox.ForeColor = Color.Blue;
                            }
                            else if (letraActual != caracterUsuario)
                            {
                                textBox.ForeColor = Color.Black;
                            }
                        }
                    }
                }

                for (int i = nombre.Length + 1; i <= 17; i++)
                {
                    string textBoxName = "textBox" + i;
                    TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                    if (textBox != null)
                    {
                        textBox.BackColor = SystemColors.Window;
                    }
                }

                this.lblNombre.Text = $"Nombre Seleccionado es. {nombre}";
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un único carácter válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
