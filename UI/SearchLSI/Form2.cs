using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchLSI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            label2.Text = "generatePATH:";
            label3.Text = "docsDIR:";
            label4.Text = "dataLoadPATH:";
            label5.Text = "octavePATH:";
            label6.Text = "dataSavePATH";
            textBox3.Text = "searchPATH";


            textBox1.Text = Properties.Settings.Default.octavePATH;
            textBox2.Text = Properties.Settings.Default.generatePATH;
            textBox3.Text = Properties.Settings.Default.searchPATH;
            textBox4.Text = Properties.Settings.Default.docsDIR;
            textBox5.Text = Properties.Settings.Default.dataLoadPATH;
            textBox6.Text = Properties.Settings.Default.dataSavePATH;
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.octavePATH = textBox1.Text;
            Properties.Settings.Default.generatePATH = textBox2.Text;
            Properties.Settings.Default.searchPATH = textBox3.Text;
            Properties.Settings.Default.docsDIR = textBox4.Text;
            Properties.Settings.Default.dataLoadPATH = textBox5.Text;
            Properties.Settings.Default.dataSavePATH = textBox6.Text;

            this.Close();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();




            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = fileDialog.FileName.ToString().Replace("/", "\\"); ;

                Properties.Settings.Default.searchPATH = sPath;
                textBox3.Text = sPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderDialog.SelectedPath = Properties.Settings.Default.docsDIR;
            folderDialog.ShowNewFolderButton = false;

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath+ "\\";

                Properties.Settings.Default.docsDIR = sPath;
                textBox4.Text = sPath;
            }
        }

        private void button7_Click(object sender, EventArgs e) //path tocno do matrike
        {
            OpenFileDialog fileDialog = new OpenFileDialog();




            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = fileDialog.FileName.ToString();

                Properties.Settings.Default.dataLoadPATH = sPath.Replace("/", "\\"); ;
                textBox5.Text = sPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
           
           
        

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = fileDialog.FileName.ToString().Replace("/", "\\");

                Properties.Settings.Default.octavePATH = sPath;
                textBox1.Text = sPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();




            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = fileDialog.FileName.ToString().Replace("/", "\\"); 

                Properties.Settings.Default.generatePATH = sPath;
                textBox2.Text = sPath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();




            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                String sPath = fileDialog.FileName.ToString();

                Properties.Settings.Default.dataSavePATH = sPath.Replace("/", "\\"); 
                textBox6.Text = sPath;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
