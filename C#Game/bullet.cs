using System.Drawing;

namespace bullet
{
    public class Bullet
    {
        public Image bulletImage { get; set; } // The image of the bullet
        public Point position { get; set; } // The position of the bullet
        public int speed { get; set; } // The speed of the bullet

        public int health { get; set; } // The health of the bullet

        public int lifeSpan{ get; set; } // The lifespan of the bullet. When the lifespan reaches 0, the bullet is removed from the game

        public Point target { get; set; } // The target of the bullet
        public Bullet(Point startPos, int _speed, Image img, int _lifespan, int hp, Point _target)
        {
            position = startPos;
            speed = _speed;
            bulletImage = img; 
            lifeSpan = _lifespan;
            health = hp;
            target = _target;
        }

        // Reduces the health by 1 when hit
        public void Hit()
        {
            health -= 1;
        }
        // Moves the bullet towards the target designated on creation. 
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