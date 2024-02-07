using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static int screenWidth = Console.WindowWidth;
        static int screenHeight = Console.WindowHeight;
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            
            Random randomNumber = new Random();
            int score = 5;
            bool isGameover = false;
            pixel snakePosition = new pixel();
            snakePosition.horizontalPosition = screenWidth/2;
            snakePosition.verticalPosition = screenHeight/2;
            snakePosition.snakeHeadeColor = ConsoleColor.Red;
            string currentlMovementDirectio = "RIGHT";
            List<int> horizontalPositionSnakeList = new List<int>();
            List<int> verticalPositionSnakeList = new List<int>();
            int horizontalBerrySpawnPosition = randomNumber.Next(0, screenWidth);
            int verticalBerrySpawnPostion = randomNumber.Next(0, screenHeight);
            
            DateTime currentTime = DateTime.Now;
            DateTime updateTime = DateTime.Now;
            while (true)
            {
                Console.Clear();

                isGameover = IsOutsideGameScreen(snakePosition);
                CreateBorder();

                Console.ForegroundColor = ConsoleColor.Green;
                if (horizontalBerrySpawnPosition == snakePosition.horizontalPosition && verticalBerrySpawnPostion == snakePosition.verticalPosition)
                {
                    score++;
                    horizontalBerrySpawnPosition = randomNumber.Next(1, screenWidth - 2);
                    verticalBerrySpawnPostion = randomNumber.Next(1, screenHeight - 2);
                }
                for (int i = 0; i < horizontalPositionSnakeList.Count(); i++)
                {
                    Console.SetCursorPosition(horizontalPositionSnakeList[i], verticalPositionSnakeList[i]);
                    Console.Write("■");
                    if (horizontalPositionSnakeList[i] == snakePosition.horizontalPosition && verticalPositionSnakeList[i] == snakePosition.verticalPosition)
                    {
                        isGameover = true;
                    }
                }
                if (isGameover == true)
                {
                    break;
                }
                Console.SetCursorPosition(snakePosition.horizontalPosition, snakePosition.verticalPosition);
                Console.ForegroundColor = snakePosition.snakeHeadeColor;
                Console.Write("■");
                Console.SetCursorPosition(horizontalBerrySpawnPosition, verticalBerrySpawnPostion);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                currentTime = DateTime.Now;
                while (true)
                {
                    updateTime = DateTime.Now;
                    if (updateTime.Subtract(currentTime).TotalMilliseconds > 500) { break; }
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
                horizontalPositionSnakeList.Add(snakePosition.horizontalPosition);
                verticalPositionSnakeList.Add(snakePosition.verticalPosition);
                switch (currentlMovementDirectio)
                {
                    case "UP":
                        snakePosition.verticalPosition--;
                        break;
                    case "DOWN":
                        snakePosition.verticalPosition++;
                        break;
                    case "LEFT":
                        snakePosition.horizontalPosition--;
                        break;
                    case "RIGHT":
                        snakePosition.horizontalPosition++;
                        break;
                }
                if (horizontalPositionSnakeList.Count() > score)
                {
                    horizontalPositionSnakeList.RemoveAt(0);
                    verticalPositionSnakeList.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 +1);
        }

        private static void CreateBorder()
        {
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        class pixel
        {
            public int horizontalPosition { get; set; }
            public int verticalPosition { get; set; }
            public ConsoleColor snakeHeadeColor { get; set; }
        }

        static bool IsOutsideGameScreen(pixel snakePosition)
        {
            if (snakePosition.horizontalPosition == screenWidth - 1 || snakePosition.horizontalPosition == 0 || snakePosition.verticalPosition == screenHeight - 1 || snakePosition.verticalPosition == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
//¦