using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FormSerialisation;

namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Task task1 = Task.Factory.StartNew(()=> DirectoryCopy(@"F:\T2", @"F:\T1", true));
           Task.WaitAll(task1);
           MessageBox.Show("Done");

        }

        private void textfield_click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = ((TextBox)sender).Text;
            DialogResult resault = folderBrowserDialog1.ShowDialog();
            if (resault == DialogResult.OK)
            {
                // the code here will be executed if the user presses Open in
                // the dialog.
                string name = ((TextBox)sender).Text = folderBrowserDialog1.SelectedPath.ToString();
            }
            //textBox1.Text = "test";
           // string name = ((TextBox)sender).Name;
            //MessageBox.Show(name);
        }

        private void checkbox_click(object sender, EventArgs e)
        {
            
            bool chec = ((CheckBox)sender).Checked;
            int nr = Convert.ToInt16(((CheckBox)sender).AccessibleName);
          //  var txtbox = this.Controls.Find(String.Format("textBox{0}",((CheckBox)sender).AccessibleName), true);
           // if(txtbox is TextBox)
            if (chec)
            {
                switch (nr)
                {
                    case 1: textBox1.BackColor = Color.Green; break;
                    case 2: textBox2.BackColor = Color.Green; break;
                    case 3: textBox3.BackColor = Color.Green; break;
                    case 4: textBox4.BackColor = Color.Green; break;
                    case 5: textBox5.BackColor = Color.Green; break;
                    case 6: textBox6.BackColor = Color.Green; break;
                    case 7: textBox7.BackColor = Color.Green; break;
                    case 8: textBox8.BackColor = Color.Green; break;
                    case 9: textBox9.BackColor = Color.Green; break;
                    case 10: textBox11.BackColor = Color.Green; break;
                }
           }
            else
            {
                switch (nr)
                {
                    case 1: textBox1.BackColor = Color.White; break;
                    case 2: textBox2.BackColor = Color.White; break;
                    case 3: textBox3.BackColor = Color.White; break;
                    case 4: textBox4.BackColor = Color.White; break;
                    case 5: textBox5.BackColor = Color.White; break;
                    case 6: textBox6.BackColor = Color.White; break;
                    case 7: textBox7.BackColor = Color.White; break;
                    case 8: textBox8.BackColor = Color.White; break;
                    case 9: textBox9.BackColor = Color.White; break;
                    case 10: textBox11.BackColor = Color.White; break;
                }
                // textBox1.BackColor = Brushes.Red;
            }
        }



        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }


        //static?
        private  void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }
        
        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            FileInfo destFile = new FileInfo(Path.Combine(destDirName, file.Name));
            if(destFile.Exists)
            {
                if (file.LastWriteTime > destFile.LastWriteTime)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    try
                    {
                        file.CopyTo(temppath, false);
                    }

                    catch (IOException)
                    {
                        throw;
                    }
                }

            }
         
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSerialisor.Serialise(this, Application.StartupPath + @"\serialise.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormSerialisor.Deserialise(this, Application.StartupPath + @"\serialise.xml");
        }

    }
}
