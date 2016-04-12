using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameForm
{
    /// <summary>
    /// A Unique game about dodging bricks that want to hit you in the face
    /// </summary>
    public partial class Form1 : Form
    {
        //instance of the character and all those bricks
        Character character;
        HashSet<Brick> bricks = new HashSet<Brick>();

        //Variables for winning time, change here to change everywhere
        private readonly int winTime = 30;
        private int theCountDown;

        //Win/Loss check case
        private int winOrLoss = 0;

        /// <summary>
        /// Initializes the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts timers and instances characters when the game loads up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Create Character
            character = new Character(this.DisplayRectangle);

            //Start timers
            tmrRefreshRate.Start();
            tmrSpawnBricks.Start();
            tmrBrickMove.Start();
            tmrWinTracker.Start();

            //SET THE COUUUUUNT DOWN!!!
            theCountDown = winTime;

            //Enable double buffer to stop the flashing
            DoubleBuffered = true;

            //Spawn the very first brick
            bricks.Add(new Brick(this.DisplayRectangle));
        }

        /// <summary>
        /// Draws the character and the bricks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            character.Draw(e.Graphics);

            //Draw each brick flying at me
            foreach (Brick brick in bricks)
            {
                brick.Draw(e.Graphics);
            }

            //Tracking that sweet sweet win condition
            Font displayFont = new Font("Arial", 14);
            SolidBrush brush = new SolidBrush(Color.White);
            string displayString = "Time left to win: {0}";

            e.Graphics.DrawString(String.Format(displayString, theCountDown.ToString()), displayFont, brush, new Point(10, 10));

            //Check if the player has won or lost, display message font -> Brush Script MT
            Font endGameFont = new Font("Algerian", 36);
            SolidBrush endBrush = new SolidBrush(Color.Black);
            
            //1 = winning, 2 = Loss
            if (winOrLoss == 1)
            {
                string youWon = "You're Savage, Good Job!";
                e.Graphics.DrawString(youWon, endGameFont, endBrush, new Point(10, 30));
            }
            else if (winOrLoss == 2)
            {
                string youLost = "Lol... You Got REKT";
                e.Graphics.DrawString(youLost, endGameFont, endBrush, new Point(100, 30));
            }
        }

        /// <summary>
        /// Takes care of the key presses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //react to left or right arrow keys

            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        //move the paddle to the Right
                        character.Move(Character.Direction.Right, this.DisplayRectangle);
                        break;
                    }
                case Keys.Left:
                    {
                        //move the paddle to the left
                        character.Move(Character.Direction.Left, this.DisplayRectangle);
                        break;
                    }
                case Keys.Up:
                    {
                        //move the paddle to the up
                        character.Move(Character.Direction.Up, this.DisplayRectangle);
                        break;
                    }
                case Keys.Down:
                    {
                        //move the paddle to the down
                        character.Move(Character.Direction.Down, this.DisplayRectangle);
                        break;
                    }
                case Keys.Escape:
                    {
                        //closes the game when the escape key is pressed
                        Application.Exit();
                        break;
                    }
            }
        }

        /// <summary>
        /// Tracks to see if there is a brick in your face.
        /// </summary>
        private void takeCareOfInjures()
        {

            //Removes failures from this world
            bricks.RemoveWhere(BrickLongGone);

            foreach (Brick brick in bricks)
            {
                //If a brick hit yo face, go get some help...
                if (brick.getBrikeShape.IntersectsWith(character.getCharShape))
                {
                    winOrLoss = 2;
                    endGame();
                    break;
                }
            }
        }

        /// <summary>
        /// If a brick goes off the screen, delete it from existance
        /// </summary>
        /// <param name="brick"></param>
        /// <returns></returns>
        private bool BrickLongGone(Brick brick)
        {
            return brick.getBrikeShape.X < 0;
        }

        /// <summary>
        /// Handles the code for when you lose or win.
        /// </summary>
        private void endGame()
        {
            //First stop all timers
            tmrBrickMove.Stop();
            tmrRefreshRate.Stop();
            tmrSpawnBricks.Stop();
            tmrWinTracker.Stop();

            //Next kill all active bricks
            bricks.Clear();

            //Show the end game message
            Invalidate();

            //Set a timer to restart game and close message after Five seconds
            tmrRestartGame.Start();
        }

        /// <summary>
        /// Tells the game to redraw every 0.01 seconds, i need the best fps NA.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRefreshRate_Tick(object sender, EventArgs e)
        {
            //Invalidate the form to force a redraw
            Invalidate();
        }

        /// <summary>
        /// Spawns a new flying deadly brick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrSpawnBricks_Tick(object sender, EventArgs e)
        {
            bricks.Add(new Brick(this.DisplayRectangle));
        }

        /// <summary>
        /// Tracks moving of bricks, and stops game if your hurt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrBrickMove_Tick(object sender, EventArgs e)
        {
            //check to see if your ok
            takeCareOfInjures();

            //Move all the bricks
            foreach (Brick brick in bricks)
            {
                brick.Move();
            }

            //Redraw
            Invalidate();
        }

        /// <summary>
        /// Timer used to restart game after delay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRestartGame_Tick(object sender, EventArgs e)
        {
            //Recreate characters and bricks
            character = new Character(this.DisplayRectangle);
            bricks.Add(new Brick(this.DisplayRectangle));
            theCountDown = winTime;
            winOrLoss = 0;

            //Restart other timers key to gameplay
            tmrRefreshRate.Start();
            tmrSpawnBricks.Start();
            tmrBrickMove.Start();
            tmrWinTracker.Start();

            //Stop timer after the first tick
            tmrRestartGame.Stop();
        }

        /// <summary>
        /// Timer used to track how long you have lived to see if you win
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrWinTracker_Tick(object sender, EventArgs e)
        {
            //Reduce time left by 1
            theCountDown = theCountDown - 1;

            //if you survive the time, you win
            if (theCountDown == 0)
            {
                winOrLoss = 1;
                endGame();
            }
        }
    }
}
