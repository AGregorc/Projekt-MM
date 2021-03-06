﻿using System;
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
using System.Collections;

namespace SearchLSI
{
    public partial class Form1 : Form
    {
        object[] dokumenti;
        int startInd = 0;
        public Form1()
        {
            InitializeComponent();
        }

        bool settingsForMe = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!radioButtonGlob.Checked)
            {
                radioButtonFrek.Checked = true;
            }
            double trackVal = trackBar1.Value;
            trackVal = trackVal / 100;
            label1.Text = "cos: "+trackVal.ToString();
            if (!settingsForMe)
            {
                /*
                Properties.Settings.Default.generatePATH = @"..\..\..\..\generate.m";
                Properties.Settings.Default.searchPATH = @"..\..\..\..\search.m";
                Properties.Settings.Default.docsDIR = @"..\..\..\..\classic1\";
                Properties.Settings.Default.dataLoadPATH = @"..\..\..\..\data.mat";
                Properties.Settings.Default.dataSavePATH = @"..\..\..\..\data.mat";
                */
            }



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderDialog.SelectedPath = Properties.Settings.Default.docsDIR;
            folderDialog.ShowNewFolderButton = false;
            
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;
               
                Properties.Settings.Default.docsDIR = sPath + " /";

            }
         
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBoxResult.Text = "Generating ... ";
            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = Properties.Settings.Default.octavePATH;
           

            //_process.StartInfo.Arguments = @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\generate.m" + " 10 "+ @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\"+" "+ @"C:\Users\Cyws\Dropbox\2.letnik\MatematicnoModeliranje\1.projekt\classic\";
            /*
             * 
             *
            docs_dir = args{1};
            mode = args{2};
            save_path = args{3};
             * */
            _process.StartInfo.Arguments = Properties.Settings.Default.generatePATH +" "+ Properties.Settings.Default.docsDIR + " "+ (radioButtonFrek.Checked ? "f" : "a") + " "+ Properties.Settings.Default.dataSavePATH;
           
            Boolean napaka = false;
            if (!_process.Start())
            {
              
                //     MessageBox.Show("Error starting");
                napaka = true;
            }
            else
            {
                napaka = false;
                
            }
            string izhod = "";
            
            while (!_process.StandardOutput.EndOfStream)
            {
                string line = _process.StandardOutput.ReadLine();
                izhod += line + "\n";
                textBoxResult.AppendText(line);
                // do something with line
               
            }
          
            if (!napaka) textBoxResult.AppendText("\nSuccesfully generated");
            else textBoxResult.AppendText("\nerror while generating");

            _process.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            textBoxVsebina.Text = "";
            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = Properties.Settings.Default.octavePATH;
            int stBesed = textBoxSearch.Text.Split(null).Length;//split by whitespaces and count
          


            /*
             * 
             * 
             * 
             * za search pa 
             load_path = args{1};
             min_cos = str2num(args{2})/100;
             od tle naprej pa vse besede za search
             * */

            _process.StartInfo.Arguments = Properties.Settings.Default.searchPATH + " " + Properties.Settings.Default.dataLoadPATH + " "+ trackBar1.Value + " " + textBoxSearch.Text;

            if (!_process.Start())
            {
                Console.WriteLine("Error starting");
                //     MessageBox.Show("Error starting");

            }
            string izhod = "";
            textBoxResult.Text = "";
            ArrayList list = new ArrayList();
            Boolean ansCur = true;
            while (!_process.StandardOutput.EndOfStream)
            {
                string line = _process.StandardOutput.ReadLine();
                if (!ansCur)
                {
                    string[] words = line.Split(null);
                    if (words.Length > 1)
                    {
                        list.Add(words[1]);
                    }
                }
                ansCur = false;
                izhod += line + "\n";


                // do something with line
            }


            textBoxResult.Text = izhod;
            _process.Close();
            dokumenti = list.ToArray();
            //MessageBox.Show(dokumenti.Length.ToString());
            startInd = 0;
            if (dokumenti.Length > 0)
            {
                
                    textBoxVsebina.Text = System.IO.File.ReadAllText(Properties.Settings.Default.docsDIR + dokumenti[Math.Abs(startInd) % dokumenti.Length]);
                
               
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 forma2 = new Form2();
            forma2.ShowDialog();
            
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxVsebina.Text = System.IO.File.ReadAllText(Properties.Settings.Default.docsDIR + dokumenti[Math.Abs(++startInd) % dokumenti.Length]);
            }
            catch
            {

            }
            
            
            //FileDialog openFileDialog1 = new FileDialog();
            //textBoxVsebina.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxVsebina.Text = System.IO.File.ReadAllText(Properties.Settings.Default.docsDIR + dokumenti[Math.Abs(--startInd) % dokumenti.Length]);
            }
            catch
            {

            }
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double trackval = trackBar1.Value;
            trackval = trackval / 100;
            label1.Text = "cos: "+trackval.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderDialog.SelectedPath = Properties.Settings.Default.dataLoadPATH;
            folderDialog.ShowNewFolderButton = false;
           
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;

                Properties.Settings.Default.dataLoadPATH = sPath + "/";

            }
        
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
