using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise5
{
    // Mutasem Alhariri 03/20/2020
    public partial class Form1 : Form
    {
        char[,] matrix = new char[5, 5];
        string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // Contains all alphabet characters except the letter 'J'.
        public Form1()
        {
            InitializeComponent();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            secretWord.Text = "";
            input.Text = "";
            output.Text = "";
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void secretWord_TextChanged(object sender, EventArgs e)
        {
            secretWord.BackColor = Color.White;
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            input.BackColor = Color.White;
        }
        private void translateBtn_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty())
            {
                if (IsValidWord())
                {

                    FillMatrix(PrepareChars(secretWord.Text.ToUpper()));
                    output.Text = Translate(input.Text.ToUpper());

                }

            }
        }
        // Checks if any of the required texts is empty

        private bool IsNotEmpty()
        {
            if (secretWord.Text == "")
            {
                secretWord.BackColor = Color.Red;
                MessageBox.Show("A secret word is required.", "Input Left Empty");
                return false;
            }
            else if (input.Text == "")
            {
                input.BackColor = Color.Red;
                MessageBox.Show("Please enter text you wish to translate.", "Input Left Empty");
                return false;
            }
            else
            {
                return true;
            }
        }
        // Checks if the secret word is valid
        private bool IsValidWord()
        {
            Regex re = new Regex(@"^[a-zA-Z]+$");

            if (re.IsMatch(secretWord.Text))
            {
                return true;
            }
            else
            {
                secretWord.BackColor = Color.Red;
                MessageBox.Show("Secret Words cannot contain spaces, numbers or punctuations.", "Invalid Word");
                return false;
            }
        }
        // Takes a string that contains chars from the secret word and the remaining chars in the alphapet
        // and fills the matrix.
        private void FillMatrix(string matrixString)
        {
            int x = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = matrixString[x];
                    x++;
                }
            }

        }
        // Prepares a string that contains secret word chars and the remaining chars in alpha
        // Returns a string with no duplicates
        private string PrepareChars(string word)
        {
            word = word.Replace('J', 'I');
            string allChars = "";

            foreach (char c in word)
            {
                if (!allChars.Contains(c))
                {
                    allChars += c;
                }
            }
            foreach (char c in alpha)
            {
                if (!allChars.Contains(c))
                {
                    allChars += c;
                }
            }
            return allChars;
        }
        // Takes the string we want to encrypt 
        // Returns the encrypted string
        private string Translate(string phrase)
        {
            string result = "";

            foreach (char c in phrase)
            {
                if (Char.IsLetter(c))
                {
                    result += EncodeLetter(c);
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
        // Takes a character to encode
        // Returns the encoded char.
        private char EncodeLetter(char c)
        {
            if (c.Equals('J')) c = 'I';

            char letter = '\0'; // the encoded char to return
            bool found = false; 

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (matrix[x, y].Equals(c))
                    {
                        letter = matrix[y, x];
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }
            return letter;
        }
    }
}