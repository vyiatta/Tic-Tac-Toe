using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class MainForm : Form
    {
        private bool playerOneTurn;
        private int clicksButtonCount;
        private bool isBotEnabled;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutTicTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All copyright reserved with Anastasiia Bodnia", "About Tic Tac Toe",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            enableDisable(false);

            newGameToolStripMenuItem.Enabled = true;
            resetGameToolStripMenuItem.Enabled = false;

            isBotEnabled = false;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableDisable(true);
            newGameToolStripMenuItem.Enabled = false;
            resetGameToolStripMenuItem.Enabled = true;

            PlayerInfoEnableDisable(false);

            playerOneTurn = PlayerOneStartRadioButton.Checked;
            clicksButtonCount = 0;

            isBotEnabled = PlayerVsBotRadioButton.Checked;
        }

        private void enableDisable(bool enable)
        {
            ButtonOne.Enabled = enable;
            ButtonTwo.Enabled = enable;
            ButtonThree.Enabled = enable;
            ButtonFour.Enabled = enable;
            ButtonFive.Enabled = enable;
            ButtonSix.Enabled = enable;
            ButtonSeven.Enabled = enable;
            ButtonEight.Enabled = enable;
            ButtonNine.Enabled = enable;
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            enableDisable(false);
            newGameToolStripMenuItem.Enabled = true;
            resetGameToolStripMenuItem.Enabled = false;

            PlayerInfoEnableDisable(true);

            ClearButton();
        }

        private void ClearButton()
        {
            ButtonOne.Text = "";
            ButtonTwo.Text = "";
            ButtonThree.Text = "";
            ButtonFour.Text = "";
            ButtonFive.Text = "";
            ButtonSix.Text = "";
            ButtonSeven.Text = "";
            ButtonEight.Text = "";
            ButtonNine.Text = "";
        }

        private void PlayerInfoEnableDisable(bool enable)
        {
            PlayerNameGroupBox.Enabled = enable;
            FirstPlayerGroupBox.Enabled = enable;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (playerOneTurn)
            {
                if (PlayerOneStartRadioButton.Checked)
                {
                    button.Text = "X";
                }
                else
                {
                    button.Text = "O";
                }

                playerOneTurn = false;
                button.Enabled = false;
            }
            else
            {
                if (PlayerTwoStartRadioButton.Checked)
                {
                    button.Text = "X";
                }
                else
                {
                    button.Text = "O";
                }

                playerOneTurn = true;
                button.Enabled = false;
            }

            clicksButtonCount++;
            checkWinner();

            if (isBotEnabled && !playerOneTurn)
            {
                MakeBotMove();
            }
        }

        private void checkWinner()
        {
            bool isWinner = false;

            if (clicksButtonCount == 9)
            {
                MessageBox.Show("Game is Draw", "Game Over", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ResetGame();
            }
            else
            {
                if ((ButtonOne.Text == ButtonTwo.Text) && (ButtonTwo.Text == ButtonThree.Text) && (!ButtonOne.Enabled))
                    isWinner = true;
                else if ((ButtonFour.Text == ButtonFive.Text) && (ButtonFive.Text == ButtonSix.Text) && (!ButtonFour.Enabled))
                    isWinner = true;
                else if ((ButtonSeven.Text == ButtonEight.Text) && (ButtonEight.Text == ButtonNine.Text) && (!ButtonSeven.Enabled))
                    isWinner = true;
                else if ((ButtonOne.Text == ButtonFour.Text) && (ButtonFour.Text == ButtonSeven.Text) && (!ButtonOne.Enabled))
                    isWinner = true;
                else if ((ButtonTwo.Text == ButtonFive.Text) && (ButtonFive.Text == ButtonEight.Text) && (!ButtonTwo.Enabled))
                    isWinner = true;
                else if ((ButtonThree.Text == ButtonSix.Text) && (ButtonSix.Text == ButtonNine.Text) && (!ButtonThree.Enabled))
                    isWinner = true;
                else if ((ButtonOne.Text == ButtonFive.Text) && (ButtonFive.Text == ButtonNine.Text) && (!ButtonOne.Enabled))
                    isWinner = true;
                else if ((ButtonThree.Text == ButtonFive.Text) && (ButtonFive.Text == ButtonSeven.Text) && (!ButtonThree.Enabled))
                    isWinner = true;


                if (isWinner == true)
                {
                    if (playerOneTurn == false)
                    {
                        MessageBox.Show("Winner is " + PlayerOneNameTextBox.Text, "Game Over",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Winner is " + PlayerTwoNameTextBox.Text, "Game Over",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ResetGame();
                }
            }
        }

        private void MakeBotMove()
        {
            if (isBotEnabled)
            {
                foreach (Control control in Controls)
                {
                    if (control is Button button && button.Enabled)
                    {
                        button.Text = "O";
                        button.Enabled = false;
                        playerOneTurn = true;
                        clicksButtonCount++;
                        checkWinner();
                        break;
                    }
                }
            }
        }
      }
   }