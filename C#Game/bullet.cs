using System.Drawing;

namespace bullet
{
    public class Bullet
    {
        public Image bulletImage { get; set; }
        public Point position { get; set; }
        public int speed { get; set; }

        public int health { get; set; }

        public int lifeSpan{ get; set; }

        public Point target { get; set; }
        public Bullet(Point startPos, int _speed, Image img, int _lifespan, int hp, Point _target)
        {
            position = startPos;
            speed = _speed;
            bulletImage = img; 
            lifeSpan = _lifespan;
            health = hp;
            target = _target;
        }
        public void Hit()
        {
            health -= 1;
        }

        public void MoveTowards()
        {
            double deltaX = target.X - position.X;
            double deltaY = target.Y - position.Y;
            double len = System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            // Normalizes the vector, so the enemy doesn't move too fast
            if (len != 0)
            {
                double normX = deltaX / len;
                double normY = deltaY / len;
                // Move toward the target
                position = new Point((int)(position.X + normX * speed), (int)(position.Y + normY * speed));
            }
            lifeSpan--;
        }
    }
}