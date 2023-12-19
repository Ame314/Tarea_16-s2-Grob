using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tarea_16_StringGrob
{
    public partial class frmLecturaArchivos : Form
    {
        private const int MAX = 1000;
        private string[] arrayNombres;
        private int totalElementos = 0;

        public frmLecturaArchivos()
        {
            InitializeComponent();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
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
                txtArchivos.Text = openFileDialog1.FileName;
                LeerArchivoTexto(txtArchivos.Text);
            }
        }

        private void LeerArchivoTexto(string nombreArchivo)
        {
            arrayNombres = new string[MAX];
            int conteo = 0;

            try
            {
                using (var sr = new StreamReader(nombreArchivo, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null && conteo < MAX)
                    {
                        arrayNombres[conteo] = line;
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerarNom_Click(object sender, EventArgs e)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var numeroAleatorio = random.Next(0, totalElementos);
            var colorAleatorio = coloresDisponibles[random.Next(coloresDisponibles.Count)];

            var nombre = arrayNombres[numeroAleatorio];
            char caracterUsuario;

            if (txtCaracterUsuario.Text.Length == 1 && char.TryParse(txtCaracterUsuario.Text, out caracterUsuario))
            {
                caracterUsuario = char.ToUpper(caracterUsuario);

                // Limpiar TextBoxes existentes
                LimpiarTextBoxes();

                // Crear y mostrar TextBoxes para cada letra del nombre
                for (var i = 0; i < nombre.Length; i++)
                {
                    var textBox = new TextBox
                    {
                        Location = new Point(i * 35 + 100, 300), // Ajusta la posición según sea necesario
                        Size = new Size(30, 20),
                        TextAlign = HorizontalAlignment.Center,
                        ReadOnly = true,
                        Text = nombre[i].ToString()
                    };

                    if ((i + 1) % 2 == 0)
                    {
                        textBox.BackColor = colorAleatorio;
                    }

                    if (nombre[i] == caracterUsuario)
                    {
                        textBox.ForeColor = Color.Blue;
                    }

                    // Agregar el TextBox al formulario
                    Controls.Add(textBox);
                }

                lblNombre.Text = $"Nombre Seleccionado es. {nombre}";
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un único carácter válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarTextBoxes()
        {
            // Elimina los TextBoxes existentes del formulario, excepto txtCaracterUsuario
            foreach (var control in Controls.OfType<TextBox>().Where(c => c != txtCaracterUsuario).ToArray())
            {
                Controls.Remove(control);
                control.Dispose();
            }
        }



        private readonly List<Color> coloresDisponibles = new List<Color>
        {
            Color.LightGray,
            Color.LightBlue,
            Color.LightCoral,
            Color.LightGreen,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightSkyBlue
        };


        private void frmLecturaArchivos_Load(object sender, EventArgs e)
        {

        }

    }
}
    

