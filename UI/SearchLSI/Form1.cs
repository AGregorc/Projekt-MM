using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchLSI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;
               
                Properties.Settings.Default.folderPATH = sPath + "/";

            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = Properties.Settings.Default.octavePATH;
            string potDoGenerate = Properties.Settings.Default.generatePATH;
            string potdo = potDoGenerate.Replace("generate.m", "");//@"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\

            //_process.StartInfo.Arguments = @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\generate.m" + " 10 "+ @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\"+" "+ @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\classic\";
            
            _process.StartInfo.Arguments = Properties.Settings.Default.generatePATH +" "+ potdo + " "+ Properties.Settings.Default.folderPATH;


            if (!_process.Start())
            {
                Console.WriteLine("Error starting");
                //     MessageBox.Show("Error starting");

            }
            string izhod = "";
            while (!_process.StandardOutput.EndOfStream)
            {
                string line = _process.StandardOutput.ReadLine();
                izhod += line + "\n";

                // do something with line
            }

            _process.Close();
            textBoxResult.Text = izhod;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = Properties.Settings.Default.octavePATH;
            int stBesed = textBoxSearch.Text.Split(null).Length;//split by whitespaces and count
            string potDoGenerate = Properties.Settings.Default.generatePATH;
            string potdo = potDoGenerate.Replace("generate.m", "");//@"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\
            _process.StartInfo.Arguments = Properties.Settings.Default.searchPATH + " " +potdo + " " + textBoxSearch.Text;

            if (!_process.Start())
            {
                Console.WriteLine("Error starting");
                //     MessageBox.Show("Error starting");

            }
            string izhod = "";
            textBoxResult.Text = "";
            while (!_process.StandardOutput.EndOfStream)
            {
                string line = _process.StandardOutput.ReadLine();
                izhod += line + "\n";


                // do something with line
            }
            textBoxResult.Text = izhod;
            _process.Close();

      //      textBoxVsebina.Text = System.IO.File.ReadAllText("C:\Users\main computer\Desktop\vb test\gyn-obs-D.txt")

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.octavePATH = Microsoft.VisualBasic.Interaction.InputBox("Vnesi pot do octava", "PATH", Properties.Settings.Default.octavePATH, -1, -1);
            Properties.Settings.Default.generatePATH = Microsoft.VisualBasic.Interaction.InputBox("Vnesi pot do generate.m", "PATH", Properties.Settings.Default.generatePATH, -1, -1);
            Properties.Settings.Default.searchPATH = Microsoft.VisualBasic.Interaction.InputBox("Vnesi pot do search.m", "PATH", Properties.Settings.Default.searchPATH, -1, -1);

            //  MessageBox.Show(Properties.Settings.Default.potDoGenerate);
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //FileDialog openFileDialog1 = new FileDialog();
            //textBoxVsebina.Text = File.ReadAllText(openFileDialog1.FileName);
        }
    }
}
