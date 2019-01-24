using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*Hangman implementation in C#
  Robert Kazirut
  CSI 3370 - Software Engineering and Practice
  Oakland University*/

namespace Hangman
{

    public partial class frmScreen1 : Form
    {
        Random r = new Random(); //initalize random
        Label[] letterLabels; //array for storing the letter labels on the GUI
        Button[] letters; //array for storing {A-Z} buttons
        string playWord; //the current word in play for hangman
        char[] characters; //the current word for hangman split by character into an array
        int tries; //number of guesses the user has attempted for the current word

        public frmScreen1()
        { 
            InitializeComponent();

            letterLabels = new Label[16] {lblC0, lblC1, lblC2, lblC3, lblC4, lblC5, lblC6,
            lblC7, lblC8, lblC9, lblC10, lblC11, lblC12, lblC13, lblC14, lblC15}; //populate the label array once the form has loaded

            letters = new Button[26] {btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ,
                btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ}; //populate button array once form has loaded
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            pnlButtons.Enabled = true; //enables the virtual keyboard for guesses
            btnStop.Visible = true; //shows the stop game button
            btnStart.Visible = false; //hides the start game button
            tries = 0; //sets inital value for tries
            lblTries.Visible = true; //shows the tries label
            lblTries.Text = tries.ToString() + " " + "tries"; //constructs inital string for tries label
            playGame(); //function handles the game set up
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            pnlButtons.Enabled = false; //disables the virtual keyboard for guesses
            enableButton(); //function ensures all buttons are enabled after previous play session
            btnStop.Visible = false; //hides the stop game button
            btnStart.Visible = true; //shows the start game button
            labelChange(0); //function handles the default display for the 16 letter labels
            lblTries.Visible = false; //hides the tries label
        }

        private void labelChange(int x) //method handles the letter labels
        {
            if (x == 0) //default, all sixteen labels are visible and display 'X'
            {
                for(int k = 0; k < letterLabels.Length; k++)
                {
                    letterLabels[k].Text = "X";
                    letterLabels[k].Visible = true;
                }
            }
            else //game has started, the required letter labels will display '-', else be hidden
            {
                for (int i = 0; i < letterLabels.Length; i++)
                {
                    if (i < x)
                    {
                        letterLabels[i].Text = "-";
                    }
                    else
                    {
                        letterLabels[i].Visible = false;
                    }
                }
            }

        }

        private void getClick(object sender, EventArgs e) //method handles all virtual keyboard clicks
        {
            Button btn = (Button)sender; //stores information for most recently clicked button
            char clicked = btn.Text.ToCharArray()[0]; /*extracts the text value associated with the key
                                                      stores in char array for later comparing*/
            if (playWord.Contains(clicked)) //if current word contains the char associated with the clicked button
            {
                tries++; //increment tries once for sucessful guess
                btn.Enabled = false;
                for (int i=0; i < playWord.Length; i++)
                {
                    if (characters[i] == clicked) //checks the char array associated with the current word against the clicked char
                    {
                        letterLabels[i].Text = clicked.ToString(); //if a letter label needs to be changed to the guessed char, it happens here
                    }
                }
            }
            else
            {
                tries++; //increment tries once for unsuccessful guess
                btn.Enabled = false;
            }
            lblTries.Text = tries.ToString() + " " + "tries"; //update tries label to reflect current number of guesses
        }

        private void playGame() //method sets up inital values for the game
        { 
            int random = r.Next(0, 10); //used to random select word from array
            string[] words = {"CODING", "PROJECT", "SPAGHETTI", "INITIALIZATION", "LUGUBRIOUS", "ADMINISTRATIVELY", "DIFFERENTIATIONS",
                "MAGICANS", "APPLICATION", "PURCHASES" }; //words that are available for game
            playWord = words[random]; //selects random word from array
            int length = playWord.Length; //extracts length of current word
            labelChange(length); //handles the display of required letter labels
            characters = playWord.ToCharArray(); //convents current word to char array for comparing
        }

        private void enableButton() //enables buttons that were disabled from the previous play session
        {
            for(int i = 0; i < letters.Length; i++)
            {
                letters[i].Enabled = true;
            }
        }
    }
}
