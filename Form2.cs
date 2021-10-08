using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp3
{   
    public partial class Form2 : Form
    {
        WMPLib.WindowsMediaPlayer wplayer;
        string save2;
        public Form2(string save)
        {  
            //passing the username from form1
            save2 = save;
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
            Doot.Visible = false;
            Doot.Enabled = false;
            var form1 = new Form1();
            //changing the value of textbox1 so it can maintain the username
            form1.TextBoxValue = save2;
            form1.Visible = true;
        }


        private void buttop_Click(object sender, EventArgs e)
        {
            rich1.SelectAll();
            rich1.SelectionAlignment = HorizontalAlignment.Center;
            //display the Top score
            //depending on the selected category
            if (Easy.Checked == true)
            {
                rich1.LoadFile("Top Score.txt", RichTextBoxStreamType.PlainText);
                if (!(new FileInfo("Top Score.txt").Length == 0))
                {
                    Doot.Visible = true;
                    Doot.Enabled = true;
                }
            }
            else if (Medium.Checked == true)
            {
                rich1.LoadFile("Top Score1.txt", RichTextBoxStreamType.PlainText);
                if (!(new FileInfo("Top Score1.txt").Length == 0))
                {
                    Doot.Visible = true;
                    Doot.Enabled = true;
                }
            }
            else if (Hard.Checked == true)
            {
                rich1.LoadFile("Top Score2.txt", RichTextBoxStreamType.PlainText);
                if (!(new FileInfo("Top Score2.txt").Length == 0))
                {
                    Doot.Visible = true;
                    Doot.Enabled = true;
                }
            }
            else if (Ultra_Hard.Checked == true)
            {
                rich1.LoadFile("Top Score3.txt", RichTextBoxStreamType.PlainText);
                if (!(new FileInfo("Top Score3.txt").Length == 0))
                {
                    Doot.Visible = true;
                    Doot.Enabled = true;
                }
            }
        }

        private void butscore_Click(object sender, EventArgs e)
        {
            Doot.Visible = false;
            Doot.Enabled = false;
            rich1.SelectAll();
            rich1.SelectionAlignment = HorizontalAlignment.Center;
            //display the Scores
            //depending on the selected category
            if(Easy.Checked == true)
            {
                rich1.LoadFile("Scores.txt", RichTextBoxStreamType.PlainText);
            }
            else if(Medium.Checked == true)
            {
                rich1.LoadFile("Scores1.txt", RichTextBoxStreamType.PlainText);
            }
            else if (Hard.Checked == true)
            {
                rich1.LoadFile("Scores2.txt", RichTextBoxStreamType.PlainText);
            }
            else if (Ultra_Hard.Checked == true)
            {
                rich1.LoadFile("Scores3.txt", RichTextBoxStreamType.PlainText);
            }
        }

        private void Doot_Click(object sender, EventArgs e)
        {
            //click the skeleton with the trumpet!
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "SKULL TRUMPET.mp3";
            wplayer.controls.play();
        }

        //Toggle buttons
        private void Easy_MouseClick(object sender, MouseEventArgs e)
        {
            rich1.Clear();
            Doot.Visible = false;
            Doot.Enabled = false;

            Medium.Checked = false;
            Medium.BackColor = Color.White;
            Medium.ForeColor = Color.Black;

            Hard.Checked = false;
            Hard.BackColor = Color.White;
            Hard.ForeColor = Color.Black;

            Ultra_Hard.Checked = false;
            Ultra_Hard.BackColor = Color.White;
            Ultra_Hard.ForeColor = Color.Black;

            Easy.Checked = true;
            Easy.BackColor = Color.Black;
            Easy.ForeColor = Color.White;
        }

        private void Medium_MouseClick(object sender, MouseEventArgs e)
        {
            rich1.Clear();
            Doot.Visible = false;
            Doot.Enabled = false;

            Medium.Checked = true;
            Medium.BackColor = Color.Black;
            Medium.ForeColor = Color.White;

            Hard.Checked = false;
            Hard.BackColor = Color.White;
            Hard.ForeColor = Color.Black;

            Ultra_Hard.Checked = false;
            Ultra_Hard.BackColor = Color.White;
            Ultra_Hard.ForeColor = Color.Black;

            Easy.Checked = false;
            Easy.BackColor = Color.White;
            Easy.ForeColor = Color.Black;
        }

        private void Hard_MouseClick(object sender, MouseEventArgs e)
        {
            rich1.Clear();
            Doot.Visible = false;
            Doot.Enabled = false;

            Medium.Checked = false;
            Medium.BackColor = Color.White;
            Medium.ForeColor = Color.Black;

            Hard.Checked = true;
            Hard.BackColor = Color.Black;
            Hard.ForeColor = Color.White;

            Ultra_Hard.Checked = false;
            Ultra_Hard.BackColor = Color.White;
            Ultra_Hard.ForeColor = Color.Black;

            Easy.Checked = false;
            Easy.BackColor = Color.White;
            Easy.ForeColor = Color.Black;
        }

        private void Ultra_Hard_MouseClick(object sender, MouseEventArgs e)
        {
            rich1.Clear();
            Doot.Visible = false;
            Doot.Enabled = false;

            Medium.Checked = false;
            Medium.BackColor = Color.White;
            Medium.ForeColor = Color.Black;

            Hard.Checked = false;
            Hard.BackColor = Color.White;
            Hard.ForeColor = Color.Black;

            Ultra_Hard.Checked = true;
            Ultra_Hard.BackColor = Color.Black;
            Ultra_Hard.ForeColor = Color.White;

            Easy.Checked = false;
            Easy.BackColor = Color.White;
            Easy.ForeColor = Color.Black;
        }
    }
}
