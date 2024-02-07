using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            Random randomnummer = new Random();
            int score = 5;
            int gameover = 0;
            pixel snakePosition = new pixel();
            snakePosition.xpos = screenWidth/2;
            snakePosition.ypos = screenHeight/2;
            snakePosition.snakeHeadeColor = ConsoleColor.Red;
            string currentlMovementDirectio = "RIGHT";
            List<int> xposlijf = new List<int>();
            List<int> yposlijf = new List<int>();
            int berryx = randomnummer.Next(1, screenWidth);
            int berryy = randomnummer.Next(0, screenHeight);
            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            while (true)
            {
                Console.Clear();
                if (snakePosition.xpos == screenWidth-1 || snakePosition.xpos == 0 ||snakePosition.ypos == screenHeight-1 || snakePosition.ypos == 0)
                { 
                    gameover = 1;
                }
                for (int i = 0;i< screenWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, screenHeight -1);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(screenWidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryx == snakePosition.xpos && berryy == snakePosition.ypos)
                {
                    score++;
                    berryx = randomnummer.Next(1, screenWidth-2);
                    berryy = randomnummer.Next(1, screenHeight-2);
                } 
                for (int i = 0; i < xposlijf.Count(); i++)
                {
                    Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                    Console.Write("■");
                    if (xposlijf[i] == snakePosition.xpos && yposlijf[i] == snakePosition.ypos)
                    {
                        gameover = 1;
                    }
                }
                if (gameover == 1)
                {
                    break;
                }
                Console.SetCursorPosition(snakePosition.xpos, snakePosition.ypos);
                Console.ForegroundColor = snakePosition.snakeHeadeColor;
                Console.Write("■");
                Console.SetCursorPosition(berryx, berryy);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                        //Console.WriteLine(pressedKey.Key.ToString());
                        if (pressedKey.Key.Equals(ConsoleKey.UpArrow))
                        {
                            currentlMovementDirectio = "UP";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.DownArrow))
                        {
                            currentlMovementDirectio = "DOWN";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.LeftArrow))
                        {
                            currentlMovementDirectio = "LEFT";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.RightArrow))
                        {
                            currentlMovementDirectio = "RIGHT";
                        }
                    }
                }
                xposlijf.Add(snakePosition.xpos);
                yposlijf.Add(snakePosition.ypos);
                switch (currentlMovementDirectio)
                {
                    case "UP":
                        snakePosition.ypos--;
                        break;
                    case "DOWN":
                        snakePosition.ypos++;
                        break;
                    case "LEFT":
                        snakePosition.xpos--;
                        break;
                    case "RIGHT":
                        snakePosition.xpos++;
                        break;
                }
                if (xposlijf.Count() > score)
                {
                    xposlijf.RemoveAt(0);
                    yposlijf.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 +1);
        }
        class pixel
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor snakeHeadeColor { get; set; }
        }
    }
}
//¦