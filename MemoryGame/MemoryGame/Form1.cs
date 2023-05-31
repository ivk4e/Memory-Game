using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        //Adding the letters which are going to be converted because I am using Webdings font
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k", "b", "b", "v", "v", "w", "w", "z", "z"
        };

        //Creating to labels. We will click two cells every time.
        Label firstClicked, secondClicked;

        private int cellsPairClicked = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeIconsToSquares();
        }

        private void InitializeIconsToSquares()
        {
            Label label;
            int randomNumber;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                //Checks if control i of tableLayoutPanel1 is the type of the Label
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    //then label assigns the tableLayoutPanel1 Control
                    label = (Label)tableLayoutPanel1.Controls[i];
                }

                //if it is not it goes to the next control
                else
                {
                    continue;
                }

                //and after that we create a random icon in this label
                randomNumber = random.Next(0, icons.Count());
                label.Text = icons[randomNumber];

                //at the end we remove the icon which was added in the cell
                icons.RemoveAt(randomNumber);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
            {
                return;
            }

            Label clickedLabel = sender as Label;

            if (clickedLabel == null)
            {
                return;
            }

            if (clickedLabel.ForeColor == Color.Black)
            {
                return;
            }

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;


            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;

                cellsPairClicked++;
                int progress = (int)((cellsPairClicked / 8.0) * 100);
                progressBar1.Value = progress;
            }
            else
            {
                timer1.Start();
            }
            CheckForWinner();

        }

        private void CheckForWinner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor) 
                {
                    return;
                }
            }

            MessageBox.Show("Winner!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
