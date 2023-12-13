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

namespace Tarea_16_StringGrob
{
    public partial class frmLecturaArchivos : Form
    {
        const int MAX = 1000;
        string[] arrayNombres;
        int totalElementos;
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

        private void btnGenerarNom_Click(object sender, EventArgs e)
        {
            this.lblNombre.Text = arrayNombres[1];
        }
    }
}
