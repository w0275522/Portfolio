using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameForm
{
    /// <summary>
    /// A Class used to spawn and control a player
    /// </summary>
    class Character
    {
        //Player objects
        Rectangle CharShape;
        Image PlayerFace;
        //Moveable directions

        //Character Specs, Change here to change the size of the player piece
        private readonly int moveNum = 20;
        private readonly int width = 40;
        private readonly int height = 40;

        /// <summary>
        /// Enum used for direction reference
        /// </summary>
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        /// <summary>
        /// Creates a shape and places it in the middle of the screen
        /// </summary>
        /// <param name="GamePlayArea"></param>
        public Character(Rectangle GamePlayArea)
        {
            CharShape.Height = height;
            CharShape.Width = width;
            CharShape.X = GamePlayArea.Width / 2 - width / 2;
            CharShape.Y = GamePlayArea.Height / 2 - height / 2;

            // Create image.
            Image newImage = Image.FromFile("E:\\C#\\Assignment4_GDIGame\\GameForm\\Player_Kappa.png");
            PlayerFace = ScaleImage(newImage, CharShape.Width, CharShape.Height);
        }

        /// <summary>
        /// Draws the players shape/face image onto the screen
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            //SolidBrush brush = new SolidBrush(Color.White);
            //graphics.FillEllipse(brush, CharShape);

            // Draw image to screen.
            graphics.DrawImage(PlayerFace, new Point(CharShape.X, CharShape.Y));

        }

        /// <summary>
        /// Method required to scale an image down to player size
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        /// <summary>
        /// Moves the players face based on which key was pressed
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="gameplayArea"></param>
        public void Move(Direction direction, Rectangle gameplayArea)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        //Move left if not to far left
                        if (CharShape.X - moveNum < gameplayArea.Left)
                            CharShape.X = 0;
                        else
                            CharShape.X -= moveNum;
                        break;
                    }
                case Direction.Right:
                    {
                        //Move right
                        if (CharShape.X + width < gameplayArea.Width)
                            CharShape.X += moveNum;
                        else
                            CharShape.X = gameplayArea.Right - CharShape.Width;
                        break;
                    }
                case Direction.Up:
                    {
                        //Move up
                        if (CharShape.Y - moveNum < gameplayArea.Top)
                            CharShape.Y = 0;
                        else
                            CharShape.Y -= moveNum;
                        break;
                    }
                case Direction.Down:
                    {
                        //Move right
                        if (CharShape.Y + height < gameplayArea.Bottom)
                            CharShape.Y += moveNum;
                        else
                            CharShape.Y = gameplayArea.Bottom - CharShape.Height;
                        break;
                    }
            }
        }

        /// <summary>
        /// Getter for the player
        /// </summary>
        public Rectangle getCharShape
        {
            get
            {
                return CharShape;
            }
        }
    }
}
