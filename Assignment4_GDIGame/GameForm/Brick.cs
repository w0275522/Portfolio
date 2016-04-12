using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameForm
{
    class Brick
    {
        //Travel speed for bricks
        private int xVelocity;

        //brick properties  
        Rectangle brickShape;
        Image brickImage;

        //Random value for travel speed
        Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Brick constructor, generates random speed
        /// </summary>
        /// <param name="gameplayArea"></param>
        public Brick(Rectangle gameplayArea)
        {
            //randomly set the x and y velocities
            xVelocity = random.Next(15, 30);

            //Form the size of the deadly brick
            brickShape.Height = 40;
            brickShape.Width = 20;
            brickShape.X = gameplayArea.Right - brickShape.Width;
            brickShape.Y = random.Next(gameplayArea.Top, (gameplayArea.Bottom - brickShape.Height));

            // Create image.
            Image newImage = Image.FromFile("E:\\C#\\Assignment4_GDIGame\\GameForm\\concrete-brick-unit.png");
            brickImage = ScaleImage(newImage, brickShape.Width, brickShape.Height);
        }

        /// <summary>
        /// Draws the flying deadly brick
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            // Draw image to screen.
            graphics.DrawImage(brickImage, new Point(brickShape.X, brickShape.Y));
        }

        /// <summary>
        /// Method used to scale an image down to the brick size, ty Caines 
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
        /// Moves the brick 
        /// </summary>
        public void Move()
        {
            //change x position according to the velocity
            brickShape.X -= xVelocity;
        }




        public Rectangle getBrikeShape
        {
            get
            {
                return brickShape;
            }
        }

    }
}
