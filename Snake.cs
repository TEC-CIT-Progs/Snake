using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snakes
{
    /// <summary>
    /// Point class with constructor that creates point objects with x and y coordinates from int parameters. X and Y properties have get & set.
    /// </summary>
    class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; set; }
        public int y { get; set; }
    }


    class Snake
    {
        int snakeHeadX;
        int snakeHeadY;

        string direction;

        int delay;
        bool alive;

        Random rand;
        int appleX; // -1 means apple not set
        int appleY;
        int points;

        List<Point> tail;
     
        public Snake()
        {
            // Init windows size
            Console.WindowHeight = 10;
            Console.WindowWidth = 20;

            Console.CursorVisible = false;

            // Init variables

            snakeHeadX = Console.WindowWidth / 2;
            snakeHeadY = Console.WindowHeight / 2;

            direction = "right";
            delay = 500; // milliseconds

            alive = true;

            rand = new Random();
            appleX = -1; // -1 means apple not set
            appleY = -1;
            points = 0;

            tail = new List<Point>();
        }
        /// <summary>
        /// oprettelse af ascii border til at begrænse banen.
        /// størrelsen er bestemt ud fra vinduets brede og højde.
        /// </summary>
        public void ShowBorder()
        {
            // show border "╔═╗║╚╝"
            Console.Write("╔");
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            Console.WriteLine();

            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.Write("║");
                for (int j = 1; j < Console.WindowWidth - 1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");
                Console.WriteLine();
            }

            Console.Write("╚");
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
        }
        /// <summary>
        /// Vi giver æblet en ny position, og placere æblet.
        /// </summary>
        
        public void newApple() 
        {
            appleX = rand.Next(1, Console.WindowWidth - 1);
            appleY = rand.Next(1, Console.WindowHeight - 1);
            Console.SetCursorPosition(appleX, appleY);
            Console.Write("♥");
        }

        public void eatApple()
        {
            points += 1;
            appleX = -1;
            appleY = -1;
        }
        /// <summary>
        /// Det meste af spillet er placeret her
        /// det vil sige alive status, hovedets position, æbler og Game Over
        /// hvis man spiser et æble ændre hovedets position
        /// hvis man taber vil alive metoden returnere false og bryde vores while loop og vise Game Over
        /// </summary>
        public void MainLoop()
        {
            while (alive)
            {
                // apple "♥"
                if (appleX == -1)
                {
                    newApple();
                }

                // check if there is an apple, at the head of the snake
                if (snakeHeadX == appleX && snakeHeadY == appleY)
                {
                    eatApple();
                }

                ShowTail();

                MoveHead();

                // write snake head at new position
                Console.SetCursorPosition(snakeHeadX, snakeHeadY);
                Console.Write("█");
                Console.SetCursorPosition(snakeHeadX, snakeHeadY); // set cursor back at the snake head (it automatically moves one to the right)

                Thread.Sleep(delay);

                GetDirection();

                alive = IsAlive();

            }

            Console.WriteLine($"\n Game Over!\n You've eaned {points} points");

            // snake "□▪"

        }

        private bool IsAlive()
        {
            // check if we're dead
            if (snakeHeadX <= 0)
            {
                return false;
            }
            if (snakeHeadX >= Console.WindowWidth - 1)
            {
                return false;
            }
            if (snakeHeadY <= 0)
            {
                return false;
            }
            if (snakeHeadY >= Console.WindowHeight - 1)
            {
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// Get direction er metoden der modtager spiller inputs på pil tasterne
        /// og videre giver inputet til move head for at flytte hovedets retning i spillet.
        /// </summary>
        private void GetDirection()
        {
            // check keys
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                {
                    direction = "up";
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    direction = "down";
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    direction = "left";
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    direction = "right";
                }
            }
        }

        /// <summary>
        /// Skifter retning på hvor spilleren bevæger sig ud fra spillerens input
        /// Spillerens input kan være piletasterne på deres keyboard.
        /// </summary>
        private void MoveHead()
        {
            // moving right
            if (direction == "right")
            {
                snakeHeadX += 1;
            }
            // moving left
            else if (direction == "left")
            {
                snakeHeadX -= 1;
            }
            // moving up
            else if (direction == "up")
            {
                snakeHeadY -= 1;
            }
            // moving down
            else if (direction == "down")
            {
                snakeHeadY += 1;
            }
        }
        /// <summary>
        /// shows the tail by replaysing the old head and removeing the exesive tail end
        /// </summary>
        private void ShowTail()
        {
            // overwrite old snake head, with tail
            Console.SetCursorPosition(snakeHeadX, snakeHeadY);
            Console.Write("▒");
            tail.Add(new Point(snakeHeadX, snakeHeadY));
            // remove exesive tail end
            if (tail.Count > points)
            {
                Console.SetCursorPosition(tail[0].x, tail[0].y);
                Console.Write(" ");
                tail.RemoveAt(0);
            }
        }
    }
}
