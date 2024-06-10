using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileOperationsApp
{
    public partial class Form1 : Form
    {
        private string filePath = "D:\\Universidad Siglo 21\\18 Seminario de actualización en sistemas colaborativos\\VINF04394-TP3-Sistemas-Colaborativos\\archivo.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void ExecuteGitCommand(string command)
        {
            var processInfo = new ProcessStartInfo("git", command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = "D:\\Universidad Siglo 21\\18 Seminario de actualización en sistemas colaborativos\\VINF04394-TP3-Sistemas-Colaborativos"
            };

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    string message = $"Git command failed:\nCommand: git {command}\nError: {error}\nOutput: {output}";
                    MessageBox.Show(message);
                    throw new InvalidOperationException(message);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Escribir el contenido por defecto en el archivo con la fecha y hora de creación
                string content = $"Este es el contenido inicial del archivo. Creado el {DateTime.Now}\n";
                File.WriteAllText(filePath, content);
                MessageBox.Show("Texto añadido al archivo."); // Mensaje de confirmación
                ShowFileContent();

                // Ejecutar comandos Git
                ExecuteGitCommand("add ."); // Agregar todos los archivos modificados y no rastreados
                ExecuteGitCommand("commit -m \"Añadir texto por defecto a archivo.txt desde la aplicación\"");
                ExecuteGitCommand("push origin main");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Eliminar todo el contenido del archivo y agregar la fecha de eliminación
                if (File.Exists(filePath))
                {
                    string content = $"Eliminado el {DateTime.Now}";
                    File.WriteAllText(filePath, content);
                    MessageBox.Show("Contenido del archivo eliminado."); // Mensaje de confirmación
                    ShowFileContent(); // Asegurarse de actualizar la pantalla

                    // Ejecutar comandos Git
                    ExecuteGitCommand("add ."); // Agregar todos los archivos modificados y no rastreados
                    ExecuteGitCommand("commit -m \"Eliminar contenido de archivo.txt desde la aplicación\"");
                    ExecuteGitCommand("push origin main");
                }
                else
                {
                    MessageBox.Show("El archivo no existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar el contenido del cuadro de texto en el archivo con la fecha y hora de modificación
                string content = $"{txtContent.Text}\nModificado el {DateTime.Now}";
                File.WriteAllText(filePath, content);
                MessageBox.Show("Archivo guardado."); // Mensaje de confirmación
                ShowFileContent();

                // Ejecutar comandos Git
                ExecuteGitCommand("add ."); // Agregar todos los archivos modificados y no rastreados
                ExecuteGitCommand("commit -m \"Guardar cambios en archivo.txt desde la aplicación\"");
                ExecuteGitCommand("push origin main");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ShowFileContent()
        {
            // Mostrar el contenido del archivo en el cuadro de texto
            if (File.Exists(filePath))
            {
                txtContent.Text = File.ReadAllText(filePath);
            }
            else
            {
                txtContent.Text = string.Empty;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFileContent();
        }
    }
}
