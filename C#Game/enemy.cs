using System.Drawing;

namespace mob
{
    public class Enemy
    {
        public Image enemyImage { get; set; }
        public Point position { get; set; }
        public int speed { get; set; }
        public int health { get; set; }

        public Enemy(Point startPos, int _speed, Image img, int hp)
        {
            position = startPos;
            speed = _speed;
            enemyImage = img;
            health = hp;
        }

        public void Hit()
        {
            health -= 1;
        }

        public void MoveTowards(Point target)
        {
            double deltaX = target.X - position.X;
            double deltaY = target.Y - position.Y;
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