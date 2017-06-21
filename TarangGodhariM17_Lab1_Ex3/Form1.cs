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

namespace TarangGodhariM17_Lab1_Ex3
{
    public partial class Form1 : Form
    {
        private string openFileName;

        private bool fileOpened = false;

        public Form1()
        {
            InitializeComponent();


        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //Add Font Family to Combobox
            foreach (FontFamily fontfamilies in FontFamily.Families)
            {
                toolStripComboBoxFont.Items.Add(fontfamilies.Name);
            }
            toolStripComboBoxFont.Text = richTextBox1.Font.Name.ToString();

            //Add Font Size to ComboBox
            for (int i = 8; i <= 72; i++)
            {
                toolStripComboBoxFontSize.Items.Add(i);
            }
            int richfontfloor = (int)Math.Floor(richTextBox1.Font.Size);
            toolStripComboBoxFontSize.Text = richfontfloor.ToString();
            
            richTextBox1.Focus();
        }

        //Set text to Desired Font Size
        private void toolStripComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionLength>0)
            richTextBox1.SelectionFont = new Font(toolStripComboBoxFont.SelectedItem.ToString(), Convert.ToInt32(toolStripComboBoxFontSize.SelectedItem));
            else
            richTextBox1.Font = new Font(toolStripComboBoxFont.SelectedItem.ToString(), Convert.ToInt32(toolStripComboBoxFontSize.SelectedItem));

        }

        //Set text to Desired Font Family
        private void toolStripComboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fontsize = Convert.ToInt32(toolStripComboBoxFontSize.SelectedItem);
           
            if (0 < fontsize && fontsize < System.Single.MaxValue)
            {
                if (richTextBox1.SelectionLength > 0)
                    richTextBox1.SelectionFont = new Font(toolStripComboBoxFont.SelectedItem.ToString(), fontsize);
                else
                    richTextBox1.Font = new Font(toolStripComboBoxFont.SelectedItem.ToString(), fontsize);

                //   MessageBox.Show(toolStripComboBoxFont.SelectedItem.ToString());
            }
        }
   
        //Add File To RichTextArea
        private void toolStripButtonAddFile_Click(object sender, EventArgs e)
        {
            // Display the openFile dialog.
            DialogResult result = openFileDialog1.ShowDialog();

            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                openFileName = openFileDialog1.FileName;
                try
                {
                    // Output the requested file in richTextBox1.
                    Stream s = openFileDialog1.OpenFile();
                    richTextBox1.LoadFile(s, RichTextBoxStreamType.RichText);
                    s.Close();

                    fileOpened = true;

                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                    fileOpened = false;
                }
                Invalidate();

                //closeMenuItem.Enabled = fileOpened;
            }

            // Cancel button was pressed.
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        //Change Color of Text
        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {

            // Allows the user to get help. (The default is false.)
            colorDialog1.ShowHelp = true;
            // Sets the initial color select to the current text color.
            colorDialog1.Color = richTextBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                if (richTextBox1.SelectionLength > 0)
                    richTextBox1.SelectionColor = colorDialog1.Color;
                else
                    richTextBox1.ForeColor = colorDialog1.Color;
        }

        //Change RichTextBox BackColor
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Allows the user to get help. (The default is false.)
            colorDialog1.ShowHelp = true;
            // Sets the initial color select to the current text color.
            colorDialog1.Color = richTextBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.BackColor = colorDialog1.Color;
        }
        //Copy Text
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetDataObject(richTextBox1.SelectedText);
        }
        //Cut Text
        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetDataObject(richTextBox1.SelectedText);
            richTextBox1.SelectedText = "";
        }
        //Paste Text
        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

       
        //About Button Event
        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            Form f2 = new Form();
            f2.Text = "About";
            f2.Size = new Size(250, 150);
            f2.StartPosition = FormStartPosition.CenterScreen;
            f2.Icon = Icon.FromHandle(((Bitmap)imageList1.Images[0]).GetHicon());
            Label aboutLabel = new Label();
            aboutLabel.Size = new Size(200, 200);
            aboutLabel.Font = new Font("Verdana", 10, FontStyle.Bold);
            aboutLabel.Text = "Application Developer: Tarang Godhari \n\nIcons By: ";
            LinkLabel linklabel = new System.Windows.Forms.LinkLabel();

            linklabel.Location = new Point(0, 70);
            linklabel.Text = "Icons8";
            linklabel.Font = new Font("Verdana", 10, FontStyle.Bold);
            linklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);

            f2.Controls.Add(linklabel);
            f2.Controls.Add(aboutLabel);
            f2.Show();
        }
        private void linkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start("https://icons8.com/icon/13754/Message");
        }

        //Save File Button Event 
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savertf = new SaveFileDialog();
            savertf.DefaultExt = "*.rtf";
            savertf.Filter = "RTF Files|*.rtf";


            if (savertf.ShowDialog() == DialogResult.OK && savertf.FileName.Length > 0)
            {
                richTextBox1.SaveFile(savertf.FileName);
            }
        }

        //Key Down Events
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.S | Keys.Control))
            {
                MessageBox.Show("What the Ctrl+F");
                
            }
        }
        //Handling All the KeyDown Events with controls
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveFileDialog savertf = new SaveFileDialog();
                savertf.DefaultExt = "*.rtf";
                savertf.Filter = "RTF Files|*.rtf";


                if (savertf.ShowDialog() == DialogResult.OK && savertf.FileName.Length > 0)
                {
                    richTextBox1.SaveFile(savertf.FileName);
                }
                return true;
            }

            if(keyData == (Keys.Control | Keys.O))
            {
                DialogResult result = openFileDialog1.ShowDialog();

                // OK button was pressed.
                if (result == DialogResult.OK)
                {
                    openFileName = openFileDialog1.FileName;
                    try
                    {
                        // Output the requested file in richTextBox1.
                        Stream s = openFileDialog1.OpenFile();
                        richTextBox1.LoadFile(s, RichTextBoxStreamType.RichText);
                        s.Close();

                        fileOpened = true;

                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                        + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                        fileOpened = false;
                    }
                    //Invalidate();

                    //closeMenuItem.Enabled = fileOpened;
                }

                // Cancel button was pressed.
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

       
    }
}
