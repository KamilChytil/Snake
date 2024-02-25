using System;
using static Snake.Program;
using static System.Formats.Asn1.AsnWriter;

namespace Snake
{
    static class Program
    {


        // Constants for screen dimensions
        const int _screenWidth = 32;
        const int _screenHeight = 16;
        public static int screenWidth {  get { return _screenWidth; } }
        public static int screenHeight { get { return _screenHeight; } }

        public static int score = 5;
        public static bool isNotGameOver = true;
        public static string currentlMovementDirectio = "RIGHT";
        public static Random random = new Random();
        public static ConsoleColor UIcolor = ConsoleColor.Green;
        public class SnakeBody
        {
            public int horizontalPosition { get; set; }
            public int verticalPosition { get; set; }
            public ConsoleColor snakeHeadeColor { get; set; }
        }

        public static List<int> horizontalPositionSnakeList = new List<int>();
        public static List<int> verticalPositionSnakeList = new List<int>();
        public static int horizontalBerrySpawnPosition;
        public static int verticalBerrySpawnPosition;

        public static DateTime currentTime;
        public static DateTime updateTime;

        static void Main(string[] args)
        {
            Console.WindowHeight = screenHeight;
            Console.WindowWidth = screenWidth;


            SnakeBody snakePosition = new SnakeBody();
            SnakeLogic.initializeSnake(snakePosition);
            SnakeLogic.chooseBerryPozition();
            SnakeLogic.startTimer();
            while (isNotGameOver)
            {
                Console.Clear();
                SnakeLogic.isSnakeOutsideBorder(snakePosition);
                SnakeUI.drawBorder();

                Console.ForegroundColor = UIcolor;
                if (SnakeLogic.isSnakeOnBarry(snakePosition))
                {
                    score++;
                    SnakeLogic.chooseBerryPozition();
                }
                SnakeUI.drawSnake(snakePosition);
                if (isNotGameOver == false)
                {
                    SnakeUI.drawGameOverText();
                    break;
                }
                SnakeUI.drawSnakeHead(snakePosition);
                SnakeUI.drawBerry();
                SnakeLogic.selectSnakeDirection();
                SnakeUI.addSnakeBodyPosition(snakePosition);
                SnakeLogic.moveSnake(snakePosition);
            }

        }
    }

    static class SnakeUI
    {
        public static void drawBorder()
        {
            for (int i = 0; i < Program.screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, Program.screenHeight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < Program.screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(Program.screenWidth - 1, i);
                Console.Write("■");
            }
        }

        public static void drawSnake(SnakeBody snakePosition)
        {
            for (int currentSnakeSquare = 0; currentSnakeSquare < Program.horizontalPositionSnakeList.Count(); currentSnakeSquare++)
            {
                Console.SetCursorPosition(Program.horizontalPositionSnakeList[currentSnakeSquare], Program.verticalPositionSnakeList[currentSnakeSquare]);
                Console.Write("■");
                if (SnakeLogic.isSnakeTouchingItSelf(snakePosition, currentSnakeSquare))
                {
                    Program.isNotGameOver = false;
                }
            }
        }

        public static void drawSnakeHead(SnakeBody snakePosition)
        {
            Console.SetCursorPosition(snakePosition.horizontalPosition, snakePosition.verticalPosition);
            Console.ForegroundColor = snakePosition.snakeHeadeColor;
            Console.Write("■");
        }

        public static void addSnakeBodyPosition(SnakeBody snakePosition)
        {
            Program.horizontalPositionSnakeList.Add(snakePosition.horizontalPosition);
            Program.verticalPositionSnakeList.Add(snakePosition.verticalPosition);
        }

        public static void drawBerry()
        {
            Console.SetCursorPosition(Program.horizontalBerrySpawnPosition, Program.verticalBerrySpawnPosition);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("■");
        }

        public static void drawGameOverText()
        {
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + Program.score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }

    }

    static class SnakeLogic
    {
        public static void startTimer()
        {
            Program.currentTime = DateTime.Now;
            Program.updateTime = DateTime.Now;
        }

        public static void chooseBerryPozition()
        {
            int spawnBerryMargin = 2;
            horizontalBerrySpawnPosition = random.Next(0, screenWidth - spawnBerryMargin);
            verticalBerrySpawnPosition = random.Next(0, screenHeight - spawnBerryMargin);
        }


        public static void initializeSnake(SnakeBody snakePosition)
        {
            snakePosition.horizontalPosition = screenWidth / 2;
            snakePosition.verticalPosition = screenHeight / 2;
            snakePosition.snakeHeadeColor = ConsoleColor.Red;
        }


        public static void selectSnakeDirection()
        {
            Program.currentTime = DateTime.Now;
            while (true)
            {
                Program.updateTime = DateTime.Now;
                if (Program.updateTime.Subtract(Program.currentTime).TotalMilliseconds > 500) { break; }
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
        }

        public static void moveSnake(SnakeBody snakePosition)
        {
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
        public static bool isSnakeOutsideBorder(SnakeBody snakePosition)
        {
            if (snakePosition.horizontalPosition == screenWidth - 1 || snakePosition.horizontalPosition == 0
                || snakePosition.verticalPosition == screenHeight - 1 || snakePosition.verticalPosition == 0)
            {
                isNotGameOver = false;
                return false;
            }
            else
            {
                isNotGameOver = true;
                return true;
            }
        }

        public static bool isSnakeOnBarry(SnakeBody snakePosition)
        {
            return horizontalBerrySpawnPosition == snakePosition.horizontalPosition && verticalBerrySpawnPosition == snakePosition.verticalPosition;
        }

        public static bool isSnakeTouchingItSelf(SnakeBody snakePosition, int i)
        {
            return horizontalPositionSnakeList[i] == snakePosition.horizontalPosition && verticalPositionSnakeList[i] == snakePosition.verticalPosition;
        }

    }

}
