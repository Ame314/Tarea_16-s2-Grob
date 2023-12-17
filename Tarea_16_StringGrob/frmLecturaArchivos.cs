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
        const int MAX = 1000;
        string[] arrayNombres;
        int totalElementos;
        private TextBox[] cuadrosNombre;




        public frmLecturaArchivos()
        {
            InitializeComponent();
        }

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
            int cont = 0, totalElementos = 0;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(nombreArchivo, Encoding.UTF8);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    //Console.WriteLine(line); enviar los elementos al vector
                   if (cont < MAX)
                    {
                        arrayNombres[cont] = line;
                        totalElementos++;
                    }
                    line = sr.ReadLine();
                    cont++;

                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
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
            Random random = new Random();//genera números aleatorios sin semilla
            int indiceAleatorio = random.Next(0, 458);//los números aleatorios de generan de 0 a 557
            string nombre = arrayNombres[indiceAleatorio];

            this.lblNombre.Text = $"EL NOMBRE SELECCIONADO ES :{arrayNombres[indiceAleatorio]}";//muestra en el lbl el nombre
            Color colorAleatorio = coloresDisponibles[random.Next(coloresDisponibles.Count)];

            for (int i = 1; i <= 10; i++)
            {
                string textBoxName = "textBox" + i;

                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    textBox.Clear();

                    if (i <= nombre.Length)
                    {
                        textBox.Text = nombre.Substring(i - 1, 1);

                        // da color de en las pociciones pares del textbox
                        if (i % 2 == 0)
                        {
                            // da color de manera aleatoria en el textbox
                            textBox.BackColor = colorAleatorio;
                        }
                    }
                }
            }

            //da a las casillas los colores normales

            for (int i = nombre.Length + 1; i <= 10; i++)
            {
                string textBoxName = "textBox" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    textBox.BackColor = SystemColors.Window;
                }
            }

            this.lblNombre.Text = $"Nombre Selecionado es. {arrayNombres[indiceAleatorio]}";
        }


    }
       
    
}
