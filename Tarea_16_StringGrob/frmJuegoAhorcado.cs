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
        private int errores = 0;
        private int puntoError = 1;
        private int puntoAcierto = 1;
        TextBox[] palabras;
        private PictureBox[] pictureBoxes;//para poder usar las imágenes para el muñequito de ahorcado

        private string nombreJugador = ""; // Variable para almacenar el nombre del jugador
        private TextBox playerNameTextBox;

        public frmJuegoAhorcado()
        {
            InitializeComponent();
            pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };//Para realizar y acomodar los picture box, esta actividad supongo que es para la siguiente clase

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//cierra todas las ventanas abiertas
        }
       

        private void LeerArchivoTexto(string nombreArchivo)//Lee archivos de texto ingresado por selecciona archivo
        {
            arrayPalabras = new string[MAX];
            int conteo = 0;
            totalElementos = 0; // Restablecer el contador, este es importante, ya que sin este se crean vacíos en la ejecución  

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
        private void MuestraCategoria()
        {
            this.lblCategoria.Text = arrayPalabras[0];
        }

        private void nombresPropiosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SeleccionarArchivo();//selecciona el archivo correspondiente
            MuestraCategoria();
           
        }

        private void animalesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           
            SeleccionarArchivo();//selecciona el archivo correspondiente
            MuestraCategoria();
            
        }

        private void ciudadesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            
            SeleccionarArchivo();//selecciona el archivo correspondiente
            MuestraCategoria();
            
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
                FilterIndex = 1,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivos = openFileDialog1.FileName;

                // Check if the selected file name is valid
                if (EsArchivoValido(nombreArchivos))
                {
                    LeerArchivoTexto(nombreArchivos);
                }
                else
                {
                    MessageBox.Show("El archivo no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
       
        private bool EsArchivoValido(string nombreArchivo)
        {
            // nombres específicos de archivos permitidos
            string[] archivosPermitidos = { "NombresPropios.txt", "Ciudades.txt", "Animales.txt" };

            // tiene solo el archivo 
            string nombreArchivoSeleccionado = Path.GetFileName(nombreArchivo);

            //verifica si el archivo es permitido
            return archivosPermitidos.Contains(nombreArchivoSeleccionado);
        }
        private int GeneraAleatorios(int totalElementos)//genera los aleatorios que posteriormente será la palabra que hya que adivinar
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var numeroAleatorio = random.Next(1, totalElementos);
            return numeroAleatorio;
        }



        private void nuevoJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimpiarJuegoAnterior();


            if (totalElementos > 0)
            {
                int indiceAleatorio = GeneraAleatorios(totalElementos);
                this.lblPalabra.Text = arrayPalabras[indiceAleatorio];
                MustraFrase(this.lblPalabra.Text);
                this.groupBoxFraseAdivinar.Text = "Frase a adivinar:";

                // Guardar información del juego en un archivo de texto
                GuardarInformacionJuego();
            }
            else
            {
                MessageBox.Show("No hay palabras cargadas. Abra una categoría primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool infoAutorasMostrada = false; // Variable para controlar si se ha mostrado la información de las autoras
        private void GuardarInformacionJuego()
        {
            string rutaArchivo = "InformacionJuego.txt";

            try
            {
                // Abre o crea el archivo y agrega la información del juego
                using (StreamWriter sw = File.AppendText(rutaArchivo))
                {
                    // Muestra la información de las autoras solo una vez
                    if (!infoAutorasMostrada)
                    {
                        sw.WriteLine("Autoras: Eliana Lucas y Amelie Grob");
                        sw.WriteLine($"Fecha: {DateTime.Now}");
                        sw.WriteLine("Juego ahorcado 2.0 Tarea 18");
                        sw.WriteLine(new string('=', 60)); // Línea de separación
                        infoAutorasMostrada = true;
                    }

                    // Agrega la información del juego al archivo
                    sw.WriteLine($"Jugador(a): {nombreJugador}");
                    sw.WriteLine($"Partidas ganadas: {puntoAcierto - 1}"); // Resta 1 porque este mensaje se muestra antes de la primera partida
                    sw.WriteLine($"Partidas perdidas: {puntoError - 1}"); // Resta 1 porque este mensaje se muestra antes de la primera partida
                    sw.WriteLine($"Fecha de la partida: {DateTime.Now}");
                    sw.WriteLine(new string('=', 70)); // Línea de separación
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la información del juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MustraFrase(string frase)
        {

            this.groupBoxFraseAdivinar.Controls.Clear();
            palabras = new TextBox[frase.Length];
            int cont = 0, x = 15, y = 27;

            foreach (char c in frase)
            {
                palabras[cont] = new TextBox();
                palabras[cont].Size = new Size(80, 80);//tamaño del textbox
                palabras[cont].TextAlign = HorizontalAlignment.Center;
                palabras[cont].MaxLength = 1;
                palabras[cont].Multiline = true;
                palabras[cont].ReadOnly = true;
                Font fuente = new Font("Segoe Script", 20);//Fuente de la letra para el texbox
                palabras[cont].Font = fuente;
                palabras[cont].Text = "";
                palabras[cont].Tag = c.ToString();//letra que guarda para adivinar, muestra datos adicionales
                palabras[cont].Location = new Point(x, y);//localización, donde se va a ubicar el textbox, x y
                x += 82;
                this.groupBoxFraseAdivinar.Controls.Add(palabras[cont]);
                cont++;
            }
        }
        private void LimpiarJuegoAnterior()
        {
            // Restablecer variables y limpiar controles aquí
            this.lblPalabra.Text = string.Empty;
            errores = 0;
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Image = Properties.Resources.Ahorcado_vacio; // La imagen por defecto
            }

        }

        private void MostrarImagenAhorcado()
        {
            if (errores < pictureBoxes.Length)
            {
                pictureBoxes[errores].Image = Properties.Resources.ResourceManager.GetObject($"Ahorcado-00{errores + 1}") as Image;
                errores++;

                if (errores == pictureBoxes.Length)
                {
                    MessageBox.Show($"¡Perdiste :( la palabra correcta era : {this.lblPalabra.Text}", "Perdedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtErrores.Text = $"{puntoError++}";
                    GuardarInformacionJuego();
                }
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (this.txtLetra.Text.Length > 0)
            {
                bool letraEncontrada = false;

                for (int i = 0; i < palabras.Length; i++)
                {
                    if (palabras[i].Tag.ToString().ToUpper() == this.txtLetra.Text.ToString().ToUpper())
                    {
                        palabras[i].Text = this.txtLetra.Text.ToUpper();
                        letraEncontrada = true;
                        MessageBox.Show("Has encontrado una letra de la palabra :)");
                    }
                }

                if (!letraEncontrada)
                {
                    // La letra no está en la palabra, mostrar imagen del ahorcado
                    MostrarImagenAhorcado();
                }


                Ganador(); // verifica si la palabra ha sido completada
                this.txtLetra.Clear();
            }
        }
        private void Ganador()
        {
            bool palabraAdivinada = true;


            foreach (var textBox in palabras)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    palabraAdivinada = false;
                    break;
                }
            }

            if (palabraAdivinada)
            {
                MessageBox.Show("¡Felicidades, Ganaste!", "Ganador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAciertos.Text = $"{puntoAcierto++}";
                GuardarInformacionJuego();
            }
        }

        private void frmJuegoAhorcado_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            PedirNombreJugador();
            // Ocultar el control GroupBox3 al cargar el formulario
            groupBox3.Visible = false;

        }
        private void PedirNombreJugador()
        {
            using (var inputForm = new Form())
            {
                inputForm.Text = "Nombre del Jugador";
                inputForm.Size = new Size(300, 150);
                inputForm.StartPosition = FormStartPosition.CenterScreen;

                var prompt = new Label() { Left = 50, Top = 20, Text = "Ingrese su nombre:" };
                var textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
                var submitButton = new Button() { Text = "Aceptar", Left = 50, Width = 80, Top = 80 };

                submitButton.Click += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        nombreJugador = textBox.Text;
                        inputForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un nombre válido.");
                    }
                };

                inputForm.Controls.Add(prompt);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(submitButton);

                inputForm.ShowDialog();
            }

            // Check if the user entered a name
            if (string.IsNullOrEmpty(nombreJugador))
            {
                MessageBox.Show("No se ingresó un nombre. El juego se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


        }

        private void puntuacionesDelJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarPuntuaciones();
        }
        private void MostrarPuntuaciones()
        {
            string rutaArchivo = "InformacionJuego.txt";

            try
            {
                // Check if the file exists
                if (File.Exists(rutaArchivo))
                {
                    string contenido = File.ReadAllText(rutaArchivo);
                    MessageBox.Show(contenido, "Puntuaciones del Juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay información de juego guardada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer la información del juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblPalabra_Click(object sender, EventArgs e)
        {

        }
    }
        
}
