using System.Drawing;

namespace playerShip
{
    public class Ship
    {
        public Image playerImage { get; set; }
        public Point position { get; set; }
        public int speed { get; set; }

        public int health { get; set; }

        public Ship(Point startPos, int _speed, Image img, int hp)
        {
            position = startPos;
            speed = _speed;
            playerImage = img;
            health = hp;
        }

        public void Move(Point direction)
        {
            position = new Point(position.X + direction.X * speed, position.Y + direction.Y * speed);
        }

        public void Hit()
        {
            health -= 1;
        }
    }
}