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

namespace Tarea_16_StringGrob
{
    public partial class frmIngresaDatos : Form
    {
        public frmIngresaDatos()
        {
            InitializeComponent();
        }
        string rutaArchivo;
        string nombreArchivo;
        private void btnCargar_Click(object sender, EventArgs e)
        {
            
            rutaArchivo = ObtenerRutaArchivo();
            nombreArchivo = Path.GetFileName(rutaArchivo);

            // se asegura que el usuario ingresó datos
            if (!string.IsNullOrEmpty(rutaArchivo))
            {
                try
                {
                    this.txtGuardarArchivo.Text = rutaArchivo;
                    this.lbldatosg.Text = "Los datos fueron guardados en: " + nombreArchivo;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un pequeño problemita al guardar el archivo: " + ex.Message);
                    
                }
            }
            else
            {
                MessageBox.Show("has cancelado guardar el archivo.");
            }
        }

        // Obtiene la ruta para guardar el archivo
        static string ObtenerRutaArchivo()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "archivos txt (.txt)|.txt";
            saveFileDialog.Title = "Guardar Archivo de Texto";

            // Muestra un cuadro de dialogo y selecciona el archivo a ser guardado
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }

            // Hace que la cadena retorne vacia
            return string.Empty;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rutaArchivo))
            {
                string textoAguardar = this.txtTextoPorGuardar.Text;

                if (!string.IsNullOrEmpty(textoAguardar))
                {
                    // Guardar el texto en el archivo
                    File.AppendAllText(rutaArchivo, textoAguardar + Environment.NewLine);
                    this.txtTextoPorGuardar.Text = File.ReadAllText(rutaArchivo);
                }
                else
                {
                    MessageBox.Show("Por favor ingresa un texto para guardar, nada ha sido ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un archivo para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

        
   

