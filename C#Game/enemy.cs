using System.Drawing;

namespace mob
{
    public class Enemy
    {
        public Image enemyImage { get; set; } // The image of the enemy
        public Point position { get; set; } // The position of the enemy
        public int speed { get; set; } // The speed of the enemy
        public int health { get; set; } // The health of the enemy

        public Enemy(Point startPos, int _speed, Image img, int hp)
        {
            position = startPos;
            speed = _speed;
            enemyImage = img;
            health = hp;
        }

        public void Hit()
        {
            // Reduces the health by 1 when hit
            health -= 1;
        }

        public void MoveTowards(Point target)
        {
            // Calculate the difference in x and y coordinates between the target and the current position
            double deltaX = target.X - position.X;
            double deltaY = target.Y - position.Y;
            // calculates the distance to the target 
            double len = System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            // Normalizes the vector, so the enemy doesn't move too fast
            if (len != 0)
            {
                double normX = deltaX / len;
                double normY = deltaY / len;
                // Moves the enemy
                position = new Point((int)(position.X + normX * speed), (int)(position.Y + normY * speed));
            }
        }
    }
}