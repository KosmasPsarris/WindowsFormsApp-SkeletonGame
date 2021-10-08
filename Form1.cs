using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int score = 0;
        string name;
        int time;
        Random r = new Random();
        int X, Y;
        WMPLib.WindowsMediaPlayer wplayer;
        int counter = 1;
        int w, h;
        string save;
        string TopScorer;
        private bool SameScore = false;
        string level;
        public Form1()
        {
            InitializeComponent();
        }
        //method to maintain the username. Textbox1.Text can be changed from form2
        public string TextBoxValue
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        //displays difficulties only after a username was given
        private void StartTrue()
        {
            Easy.Enabled = true;
            Easy.Visible = true;
            Medium.Enabled = true;
            Medium.Visible = true;
            Hard.Enabled = true;
            Hard.Visible = true;
            UltraHard.Enabled = true;
            UltraHard.Visible = true;
        }
        //resetting the standard values of some variables for a successful functionality
        private void Aftergame()
        {
            Easy.Enabled = true;
            Easy.Visible = true;
            Medium.Enabled = true;
            Medium.Visible = true;
            Hard.Enabled = true;
            Hard.Visible = true;
            UltraHard.Enabled = true;
            UltraHard.Visible = true;
            Username.Visible = true;
            textBox1.Enabled = true;
            textBox1.Visible = true;
            Start.Enabled = true;
            Start.Visible = true;
            TopScore.Enabled = true;
            TopScore.Visible = true;
            Info.Enabled = true;
            Info.Visible = true;
            Exit.Enabled = true;
            Exit.Visible = true;
            pic1.Visible = true;
            Skeleton.Enabled = false;
            Skeleton.Visible = false;
            Skeleton2.Enabled = false;
            Skeleton2.Visible = false;
            Skeleton3.Enabled = false;
            Skeleton3.Visible = false;
            ScoreLabel.Visible = false;
            score = 0;
            ScoreLabel.Text = "0";
            this.Skeleton.Size = new System.Drawing.Size(262, 207);
            counter = 1;
            this.BackColor = Color.Black;
            this.CenterToScreen();
            this.Size = new Size(w, h);
            SameScore = false;
            String top_scorer = File.ReadAllText("Top Score.txt");
            String[] topscorer_temp = top_scorer.Split(null);
            TopScorer = topscorer_temp[0];
        }
        // making the form more game-friendly removing annoying buttons and such
        private void Beforegame()
        {
            Easy.Enabled = false;
            Easy.Visible = false;
            Medium.Enabled = false;
            Medium.Visible = false;
            Hard.Enabled = false;
            Hard.Visible = false;
            UltraHard.Enabled = false;
            UltraHard.Visible = false;
            Username.Visible = false;
            textBox1.Enabled = false;
            textBox1.Visible = false;
            Start.Enabled = false;
            Start.Visible = false;
            TopScore.Enabled = false;
            TopScore.Visible = false;
            Info.Enabled = false;
            Info.Visible = false;
            Exit.Enabled = false;
            Exit.Visible = false;
            pic1.Visible = false;
            Skeleton.Enabled = true;
            Skeleton.Visible = true;
            ScoreLabel.Visible = true;
        }
        //remove access to levels if there is no username
        private void StartFalse()
        {
            Easy.Enabled = false;
            Easy.Visible = false;
            Medium.Enabled = false;
            Medium.Visible = false;
            Hard.Enabled = false;
            Hard.Visible = false;
            UltraHard.Enabled = false;
            UltraHard.Visible = false;
        }

        private void SameUser(string name,int score, string level)
        {
            {
                //with level the program knows the correct file to change
                // if there is a high score check if it is the same user
                if (!(new FileInfo("Top Score"+level+".txt").Length == 0))
                {
                    //Put the contents of Top score.txt to Top then split where is whitespace
                    //making Top_temp[0] the name and Top_temo[1] the score
                    String Top = File.ReadAllText("Top Score" + level + ".txt");
                    String[] Top_temp = Top.Split(null);
                    //if it is the same user
                    if (Top_temp[0] == name)
                    {
                        //if the user's new score is better we want to update it , so we delete his entry
                        //from the scoreboard and then re-add it with the help of method NewScore
                        if (Int32.Parse(Top_temp[1]) < score)
                        {
                            //This way we overwrite Top Score.txt by first clearing it and then with
                            //NewScore writing in the same user and the updated score
                            File.Create("Top Score" + level + ".txt").Close();
                        }
                    }
                }
                //if there is at least one score check if it is the same user
                if (!(new FileInfo("Scores" + level + ".txt").Length == 0))
                {
                    //splitting each user and their score from the other like this:
                    //Score_temp[0] = name1 Score_temp[1] = score1 Score_temp[2] = " " 
                    //Score_temp[3] = name2 Score_temp[4] = score2 Score_temp[5] = " "  etc.
                    String Scores = File.ReadAllText("Scores" + level + ".txt");
                    String[] Score_temp = Scores.Split(null);
                    int k = 0;
                    //so we can find where user is located in the txt file
                    int place = -1;
                    while (k < Score_temp.Length)
                    {
                        if (Score_temp[k] == name)
                        {
                            place = k;
                            break;
                        }
                        k = k + 3;
                    }
                    //if same user found
                    if (!(place == -1))
                    {
                        //if the user's score is better, their entry is deleted
                        if (Int32.Parse(Score_temp[place + 1]) < score)
                        {

                            //create a temporary file in order to swap info.
                            var TempFile = Path.GetTempFileName();
                            //Read all lines from Score.txt except of the one we wish to remove
                            var UpdatedFile = File.ReadLines("Scores" + level + ".txt").Where(l => l != Score_temp[place] + " " + Score_temp[place + 1]);
                            //copy the contents we want from the variable UpdatedFile to TempFile
                            File.WriteAllLines(TempFile, UpdatedFile);
                            //delete the scoreboard with the not updated user
                            File.Delete("Scores" + level + ".txt");
                            //copy the contents of TempFile to Scores.txt as if overwriting it with the updated user info
                            File.Move(TempFile, "Scores" + level + ".txt");
                        }
                        else
                        {
                            //if it is the same user but with same or worse score
                            SameScore = true;
                        }
                    }
                }
            }
        }

        private void SortScores(string name, int score, string level)
        {
            if (!(new FileInfo("Scores" + level + ".txt").Length == 0))
            {
                String Scores = File.ReadAllText("Scores" + level + ".txt");
                String[] Score_temp = Scores.Split(null);
                //if there is only one entry there is no need to sort
                if (!(Score_temp.Length == 4))
                {
                    //creating temporary arrays that contain each entry , so we can sort them
                    string[] Temp_names = new string[( (Score_temp.Length - 1) / 3)];
                    string[] Temp_scores = new string[( (Score_temp.Length - 1) / 3)];
                    int t = 0;
                    //separating name from score
                    for (int i = 0; i < Score_temp.Length-1; i += 3)
                    {
                        Temp_names[t] = Score_temp[i];
                        Temp_scores[t] = Score_temp[i+1];
                        t++;
                    }
                    //sorting arrays in ascending order by comparing each user's score and
                    //changing the usernames accordingly
                    Array.Sort(Temp_scores, Temp_names);
                    //sorting arrays in descending order
                    Array.Reverse(Temp_names);
                    Array.Reverse(Temp_scores);
                    //clearing Scores.txt
                    File.Create("Scores" + level + ".txt").Close();
                    //Writing in Scores.txt the same entries but with descending order
                    for (int i = 0; i < Temp_names.Length; i++)
                    {
                        if (new FileInfo("Scores" + level + ".txt").Length == 0)
                        {
                            File.WriteAllText("Scores" + level + ".txt", Temp_names[i] + " " + Temp_scores[i]);
                        }
                        else
                        {
                            File.AppendAllText("Scores" + level + ".txt", "\r\n" + Temp_names[i] + " " + Temp_scores[i]);
                        }
                    }
                }
            }
        }

        private void NewScore(string name,int score, string level)
        {
            //find out if the user made it to the scoreboard before
            SameUser(name, score,level);
            //compare with high score 
            //if there is no high score yet just grant the title
            if (new FileInfo("Top Score" + level + ".txt").Length == 0)
            {
                File.WriteAllText("Top Score" + level + ".txt", name + " " + score);
            }
            else
            {
                String Top = File.ReadAllText("Top Score" + level + ".txt");
                String[] Top_temp = Top.Split(null);
                //string to int
                if (Int32.Parse(Top_temp[1]) < score)
                {
                    //new high score
                    File.WriteAllText("Top Score" + level + ".txt", name + " " + score);
                    //old high score becomes just a score
                    if (new FileInfo("Scores" + level + ".txt").Length == 0)
                    {
                        File.AppendAllText("Scores" + level + ".txt", Top_temp[0] + " " + Top_temp[1]);
                    }
                    else
                    {
                        File.AppendAllText("Scores" + level + ".txt", "\r\n" + Top_temp[0] + " " + Top_temp[1]);
                    }
                }
                else
                {
                    //if user is not the top scorer and their new score is  better than their old one
                    //add them as new entry, otherwise if it is not better, there is no need to add them
                    if ((!(TopScorer == name)) && (SameScore == false))
                    {
                        //new score
                        if (new FileInfo("Scores" + level + ".txt").Length == 0)
                        {
                            File.WriteAllText("Scores" + level + ".txt", name + " " + score);
                        }
                        else
                        {
                            File.AppendAllText("Scores" + level + ".txt", "\r\n" + name + " " + score);
                        }
                    }
                }
            }
            //wherever there is a blank line in Scores.txt we "remove" it so the users are displayed
            //one on top of the other without blank lines
            var lines = File.ReadAllLines("Scores" + level + ".txt").Where(x => !string.IsNullOrWhiteSpace(x));
            File.WriteAllLines("Scores" + level + ".txt", lines);
            SortScores(name,score,level);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //make sure there is a username in order to proceed
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill in a username.");
                this.StartFalse();
            }
            else if (textBox1.Text.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show("Username must not contain whitespace.");
                this.StartFalse();
            }
            else
            {
                this.StartTrue();
            }
        }

        //Difficulties
        private void Easy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill in a username.");
                this.StartFalse();
            }
            else if (textBox1.Text.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show("Username must not contain whitespace.");
                this.StartFalse();
            }
            else
            {
                level = null;
                name = textBox1.Text;
                Beforegame();
                wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = "Original.mp3";
                wplayer.controls.play();
                this.Skeleton.Size = new System.Drawing.Size(262 - 45, 207 - 45);
                time = 42;      //with different interval, the int time has to take different value in order 
                timer.Start(); //to roughly represent one minute.It should be also noted that instead of
                               //60 seconds the project functions with 63 seconds, so it matches the song
                               //in easy,medium and hard difficulty.So  63 / 1.5(interval) = 42. 
            }
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill in a username.");
                this.StartFalse();
            }
            else if (textBox1.Text.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show("Username must not contain whitespace.");
                this.StartFalse();
            }
            else
            {
                level = "1";
                name = textBox1.Text;
                Beforegame();
                wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = "Original.mp3";
                wplayer.controls.play();
                time = 63; // 63 / 1(interval)
                this.Skeleton.Size = new System.Drawing.Size(262 - 55, 207 - 55);
                timer1.Start();
            }
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill in a username.");
                this.StartFalse();
            }
            else if (textBox1.Text.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show("Username must not contain whitespace.");
                this.StartFalse();
            }
            else
            {
                level = "2";
                name = textBox1.Text;
                Beforegame();
                wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = "Original.mp3";
                wplayer.controls.play();
                time = 84; //63 / 0.75(interval) 
                this.Skeleton.Size = new System.Drawing.Size(262 - 65, 207 - 65);
                timer2.Start();
            }
        }

        //mainly for fun difficulty
        private void UltraHard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill in a username.");
                this.StartFalse();
            }
            else if (textBox1.Text.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show("Username must not contain whitespace.");
                this.StartFalse();
            }
            else
            {
                //Asks for user validation
                string message = "It is highly recommended  that you practice in an easier difficulty and eat breakfast before proceeding!!!" + "\n" + "Do you wish to continue?" + "\n" + "\n" + "Duration: 3:50 minutes.";
                string title = "God have mercy.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    level = "3";
                    name = textBox1.Text;
                    Beforegame();
                    wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = "Remix.mp3";
                    wplayer.controls.play();
                    this.Skeleton.Size = new System.Drawing.Size(200, 160);
                    time = 480; //(about) 225 / 0,455(interval) this is full duration of the song, not representing one minute.
                    timer3.Start();
                }
            }

        }

        private void TopScore_Click(object sender, EventArgs e)
        {
            //passes the username to form 2 and displays it
            save = textBox1.Text;
            this.Visible = false;
            Form2 form2 = new Form2(save);
            form2.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            //Closes the game
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create necessary files if they don't exist.
            if (!(File.Exists("Top Score.txt")))
            {
                File.Create("Top Score.txt");
            }
            if (!(File.Exists("Scores.txt")))
            {
                File.Create("Scores.txt");
            }
            if (!(File.Exists("Top Score1.txt")))
            {
                File.Create("Top Score1.txt");
            }
            if (!(File.Exists("Scores1.txt")))
            {
                File.Create("Scores1.txt");
            }
            if (!(File.Exists("Top Score2.txt")))
            {
                File.Create("Top Score2.txt");
            }
            if (!(File.Exists("Scores2.txt")))
            {
                File.Create("Scores2.txt");
            }
            if (!(File.Exists("Top Score3.txt")))
            {
                File.Create("Top Score3.txt");
            }
            if (!(File.Exists("Scores3.txt")))
            {
                File.Create("Scores3.txt");
            }
            //Keeping the name of top scorer known as it comes in handy
            if (!(new FileInfo("Top Score.txt").Length == 0))
            {
                String top_scorer = File.ReadAllText("Top Score.txt");
                String[] topscorer_temp = top_scorer.Split(null);
                TopScorer = topscorer_temp[0];
            }
            //so do the width and height of form1
            w = this.Width;
            h = this.Height;
        }

        private void Skeleton_Click(object sender, EventArgs e)
        {
            //score raises everytime the user clicks on the skeleton
            score += 10;
            ScoreLabel.Text = score.ToString();
        }

        //Medium
        private void timer1_Tick(object sender, EventArgs e)
        {
            //with every tick the time is subtracted by 1.Also the skeleton moves randomly
            //within form1 not exceeding the form's boundaries
            time--;
            X = r.Next(this.Width - Skeleton.Width);
            Y = r.Next(this.Height - Skeleton.Height);
            Skeleton.Location = new Point(X, Y);
            //when time reaches 0 music stops,score is calculated and user becomes an entry to the
            //scoreboard (unless they did worse than their previous playthrough) and
            //user is returned back to the menu
            if (time == 0)
            {
                timer1.Stop();
                wplayer.controls.stop();
                NewScore(name, score,level);
                Aftergame();
            }
        }
        //Hard
        private void timer2_Tick(object sender, EventArgs e)
        {
            time--;
            X = r.Next(this.Width - Skeleton.Width);
            Y = r.Next(this.Height - Skeleton.Height);
            Skeleton.Location = new Point(X, Y);
            if (time == 0)
            {
                timer2.Stop();
                wplayer.controls.stop();
                NewScore(name, score, level);
                Aftergame();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //pressing escape stops the running timer and returns the user to the menu 
            if(e.KeyCode == Keys.Escape)
            {
                if (timer.Enabled == true) { timer.Stop(); }
                if (timer1.Enabled == true) { timer1.Stop(); }
                if (timer2.Enabled == true) { timer2.Stop(); }
                if (timer3.Enabled == true) { timer3.Stop(); }
                wplayer.controls.stop();
                //remove the "//" from below for easier entry testing
                //NewScore(name, score, level);
                Aftergame();
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press the Escape key during the game in order to return to the menu."+"\n"+"However,this way you won't be able to make it to the scoreboard!");
        }


        // Ultra Hard
        private void timer3_Tick(object sender, EventArgs e)
        {
            time--;
            //chooses a random color
            Color RandomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            if (counter >= 96 && counter <= 159) // drop 1
            {
                //alternate every tick between two colors
                if(counter % 2 == 0)
                {
                    this.BackColor = Color.Teal;
                }
                else
                {
                    this.BackColor = Color.DarkViolet;
                }
                //randomly shrink the skeleton
                this.Skeleton.Size = new System.Drawing.Size(r.Next(Skeleton.Width - 2,Skeleton.Width), r.Next(Skeleton.Height - 2, Skeleton.Height));
            }
            //a small reset between drops
            if (counter == 160) // drop 1 - drop 2 
            {
                this.BackColor = Color.Black;
                this.Skeleton.Size = new System.Drawing.Size(200 - 20, 160 - 20);
            }
            if (counter >= 175 && counter <= 237) // drop 2
            {
                //alternate every tick between randomly increase and decrease the size of form1
                //making the user think that the form is trembling
                if (counter % 2 == 0)
                {
                    this.Size = new System.Drawing.Size(r.Next(this.Width - 10, this.Width - 10), r.Next(this.Height - 10, this.Height - 10));
                    this.CenterToScreen();
                    this.BackColor = Color.Blue;
                }
                else
                {
                    this.Size = new System.Drawing.Size(r.Next(this.Width, this.Width + 35), r.Next(this.Height, this.Height + 35));
                    this.CenterToScreen();
                    this.BackColor = Color.Red;
                }
                this.Skeleton.Size = new System.Drawing.Size(r.Next(Skeleton.Width - 2, Skeleton.Width), r.Next(Skeleton.Height - 2, Skeleton.Height));
            }   
            if (counter == 238) // drop 2 - drop 3 
            {
                //restore form1 size
                this.Size = new Size(w, h);
                this.CenterToScreen();
                this.BackColor = Color.Black;
                this.Skeleton.Size = new System.Drawing.Size(200 - 35, 160 - 35);
            }
            if (counter >= 318 && counter <= 382) // drop 3
            {
                //summoning 2 new spooky skeletons to dance
                Skeleton2.Visible = true;
                Skeleton2.Enabled = true;
                Skeleton3.Visible = true;
                Skeleton3.Enabled = true;
                this.Size = new System.Drawing.Size(r.Next(this.Width, this.Width + 5), r.Next(this.Height, this.Height + 5));
                this.CenterToScreen();
                Skeleton2.Location = new Point(r.Next(this.Width - Skeleton2.Width), r.Next(this.Height - Skeleton2.Height));
                Skeleton3.Location = new Point(r.Next(this.Width - Skeleton3.Width), r.Next(this.Height - Skeleton3.Height));
            }
            if (counter == 383) // drop 3 - drop 4 
            {
                //bye bye skeltons
                Skeleton2.Visible = false;
                Skeleton2.Enabled = false;
                Skeleton3.Visible = false;
                Skeleton3.Enabled = false;
                this.BackColor = Color.Black;
                this.Skeleton.Size = new System.Drawing.Size(200 - 50, 160 - 50);
            }
            if (counter >= 398 && counter <= 461) // drop 4
            {
                //just joking, they are back
                Skeleton2.Visible = true;
                Skeleton2.Enabled = true;
                Skeleton3.Visible = true;
                Skeleton3.Enabled = true;
                this.Size = new System.Drawing.Size(r.Next(this.Width, this.Width + 5), r.Next(this.Height, this.Height + 5));
                this.CenterToScreen();
                Skeleton2.Location = new Point(r.Next(this.Width - Skeleton2.Width), r.Next(this.Height - Skeleton2.Height));
                Skeleton3.Location = new Point(r.Next(this.Width - Skeleton3.Width), r.Next(this.Height - Skeleton3.Height));
                //random color every tick
                this.BackColor = RandomColor;
            }
            if (counter == 462) // End of song
            {
                Skeleton2.Visible = false;
                Skeleton2.Enabled = false;
                Skeleton3.Visible = false;
                Skeleton3.Enabled = false;
                this.Size = new Size(w, h);
                this.CenterToScreen();
                this.BackColor = Color.Black;
                this.Skeleton.Size = new System.Drawing.Size(200 - 65, 160 - 65);
            }
            X = r.Next(this.Width - Skeleton.Width);
            Y = r.Next(this.Height - Skeleton.Height);
            Skeleton.Location = new Point(X, Y);
            if (time == 0)
            {
                timer3.Stop();
                wplayer.controls.stop();
                NewScore(name, score, level);
                Aftergame();
            }
            //raises with each tick,helps to determine the timing of the drops 
            //so the cool effects can happen
            counter++;
        }

        //Easy
        private void timer_Tick(object sender, EventArgs e)
        {
            time--;
            X = r.Next(this.Width - Skeleton.Width);
            Y = r.Next(this.Height - Skeleton.Height);
            Skeleton.Location = new Point(X, Y);
            if (time == 0)
            {
                timer.Stop();
                wplayer.controls.stop();
                NewScore(name, score, level);
                Aftergame();
            }
        }
    }
}
