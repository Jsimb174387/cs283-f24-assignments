using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using playerShip;
using mob;
using bullet;
public class Game
{
    private Ship player;
    private HashSet<Keys> pressedKeys = new HashSet<Keys>();
    private HashSet<Enemy> enemies = new HashSet<Enemy>();
    private HashSet<Bullet> bullets = new HashSet<Bullet>();
    private bool playerDead = false;
    private bool displayCredits = false;
    public void Setup()
    {
        Image pImg = Image.FromFile("player.png"); 
        player = new Ship(new Point(Window.width/2, Window.height/2), 5, pImg, 3);
    }

    public void Update(float dt)
    {
        if (playerDead)
        {
            return;
        }

        // Handles Player Movement.
        Movement();

        // Handles Enemies.
        var enemiesToRemove = new List<Enemy>();
        foreach (var enemy in enemies)
        {
            if (enemy.health <= 0)
            {
                enemiesToRemove.Add(enemy);
            }
            else
            {
                enemy.MoveTowards(playerCenter());
            }
        }

        // Remove enemies with health <= 0
        foreach (var enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
        }

        // Handles Bullets.
        var bulletsToRemove = new List<Bullet>();
        foreach (var bullet in bullets)
        {
            if (bullet.lifeSpan <= 0 || bullet.health <= 0)
            {
                bulletsToRemove.Add(bullet);
            }
            else
            {
                bullet.MoveTowards();
            }
        }

        // Check for collision with enemies and bullets
        var enemiesKilled = new HashSet<Enemy>();
        foreach (var enemy in enemies)
        {
            foreach (var bullet in bullets)
            {

            
                if (bullet.position.X >= enemy.position.X && bullet.position.X <= enemy.position.X + enemy.enemyImage.Width &&
                    bullet.position.Y >= enemy.position.Y && bullet.position.Y <= enemy.position.Y + enemy.enemyImage.Height &&
                    bullet.health > 0 && bullet.lifeSpan > 0)
                {
                    enemy.Hit();
                    bullet.Hit();
                    if (enemy.health <= 0)
                    {
                        enemiesKilled.Add(enemy);
                    }
                }
            }
            // checks if player and enemy collide 
            if (player.position.X >= enemy.position.X && player.position.X <= enemy.position.X + enemy.enemyImage.Width &&
                player.position.Y >= enemy.position.Y && player.position.Y <= enemy.position.Y + enemy.enemyImage.Height)
            {
                player.Hit();
                enemy.Hit();
                if (enemy.health <= 0)
                {
                    enemiesKilled.Add(enemy);
                }
            }
        }

            


        // Remove enemies with health <= 0
        foreach (var enemy in enemiesKilled)
        {
            enemies.Remove(enemy);
        }

        // Removes bullets with health <= 0 or lifeSpan <= 0

        foreach (var bullet in bulletsToRemove)
        {
            bullets.Remove(bullet);
        }

        // Spawns Enemies
        // This makes each iteration of Update have a 5% chance of spawning an enemy
        if (Window.RandomRange(0, 100) < 5)
        {
            Spawner();
        }

        // Handles Player Death

        if (player.health <= 0)
        {
            playerDead = true;
        }
    }

    public void Draw(Graphics g)
    {
        g.Clear(Color.Black);
        // centers the image on the player
        g.DrawImage(player.playerImage, playerCenter());

        if (displayCredits)
        {
            DrawCredits(g);
        }

        foreach (var enemy in enemies)
        {
            g.DrawImage(enemy.enemyImage, enemy.position);
        }

        foreach (var bullet in bullets)
        {
            g.DrawImage(bullet.bulletImage, bullet.position);
        }

        if (playerDead)
        {
            g.DrawString("Game Over", new Font("Arial", 32), Brushes.White, new Point(Window.width / 2 - 100, Window.height / 2));
        }
    }

    public void DrawCredits(Graphics g)
    {
        string name = "James Simbolon";
        string classYear = "2025";
        string gameTitle = "Space Shooter?";
        int boxWidth = 200;
        int boxHeight = 85;
        int x = 10;
        int y = 10;
        g.FillRectangle(Brushes.Gray, x, y, boxWidth, boxHeight);

        g.DrawString(name, new Font("Arial", 16), Brushes.White, new Point(x + 10, y + 10));
        g.DrawString(classYear, new Font("Arial", 16), Brushes.White, new Point(x + 10, y + 30));
        g.DrawString(gameTitle, new Font("Arial", 16), Brushes.White, new Point(x + 10, y + 50));
    }

    public Point playerCenter()
    {
        return new Point(player.position.X - player.playerImage.Width / 2, player.position.Y - player.playerImage.Height / 2);
    }

    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            Shoot(mouse.Location);
        }
    }

    public void Shoot(Point target)
    {
        if (playerDead)
        {
            return;
        }
        Image bImg = Image.FromFile("bullet.png");
        Bullet newBullet = new Bullet(player.position, 20, bImg, 20, 1, target);
        bullets.Add(newBullet);
    }

    public void KeyDown(KeyEventArgs key)
    {
        pressedKeys.Add(key.KeyCode);

        if (key.KeyCode == Keys.Oemplus)
        {
            displayCredits = !displayCredits;
        }
    } 
    public void KeyUp(KeyEventArgs key)
    {
        pressedKeys.Remove(key.KeyCode);

    }
    public void Movement()
    {
        // Handles Player Movement.
        Point direction = new Point(0, 0);

        if (pressedKeys.Contains(Keys.D) || pressedKeys.Contains(Keys.Right))
        {
            direction.X += 1;
        }
        if (pressedKeys.Contains(Keys.A) || pressedKeys.Contains(Keys.Left))
        {
            direction.X -= 1;
        }
        if (pressedKeys.Contains(Keys.W) || pressedKeys.Contains(Keys.Up))
        {
            direction.Y -= 1;
        }
        if (pressedKeys.Contains(Keys.S) || pressedKeys.Contains(Keys.Down))
        {
            direction.Y += 1;
        }        
        if (direction.X != 0 || direction.Y != 0)
        {
            player.Move(direction);
        }
    }

    public void Spawner()
    {
        // Spawns an enemy at a random location in the window. Then adds it to the enemy set. 
        Image eImg = Image.FromFile("combatant.png");
        Enemy newEnemy = new Enemy(new Point(Window.RandomRange(0, Window.width), Window.RandomRange(0, Window.height)), 3, eImg, 1);
        enemies.Add(newEnemy);
    }
}