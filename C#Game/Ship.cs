using System.Drawing;

namespace playerShip
{
    public class Ship
    {
        // Originally planned to have the sprites the other way around, but I liked being the monster
        public Image playerImage { get; set; } // image of the player
        public Point position { get; set; } // position of the player
        public int speed { get; set; } // speed of the player

        public int health { get; set; } // health of the player

        public Ship(Point startPos, int _speed, Image img, int hp)
        {
            position = startPos;
            speed = _speed;
            playerImage = img;
            health = hp;
        }

        public void Move(Point direction)
        {
            // moves the player in the direction
            position = new Point(position.X + direction.X * speed, position.Y + direction.Y * speed);
        }

        public void Hit()
        {
            //reduces the health by 1 when hit. 
            health -= 1;
        }
    }
}