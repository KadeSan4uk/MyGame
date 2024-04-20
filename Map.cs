using System;
using System.IO;

namespace MyGame
{
    public class Map
    {
        public void DrawMap()
        {
            char[,] map = ReadMap("map1.txt");
            Console.CursorVisible = false;

            int playerX = 1;
            int playerY = 9;

            while (true)
            {
                DrawMap(map);
                DrawPlayer(playerX, playerY);


                Console.SetCursorPosition(map.GetLength(0) + 2, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Player Position: ({playerX}, {playerY})");

                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                HandleInput(pressedKey, ref playerX, ref playerY, map);
            }
        }

        private static char[,] ReadMap(string path)
        {
            string mapPath = @"D:\CsharpCourse\MyGame\bin\Debug\net8.0\map1.txt";
            List<string> lines = new List<string>();
            
            using (StreamReader reader = new StreamReader(mapPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            
            int maxWidth = GetMaxLengthOfLine(lines.ToArray());
            char[,] map = new char[maxWidth, lines.Count];

            Random random = new Random();
            
            for (int y = 0; y < lines.Count; y++)
            {
                string currentLine = lines[y];
                for (int x = 0; x < currentLine.Length; x++)
                {
                    if (currentLine[x] == '#')
                    {
                        map[x, y] = '#';
                    }
                    else if (currentLine[x] == '@')
                    {
                        map[x, y] = '@';
                    }
                    else
                    {
                        if (!HasNeighborO(map, x, y))
                        {
                            map[x, y] = random.Next(10) < 2 ? 'm' : ' ';
                        }
                        else
                        {
                            map[x, y] = ' ';
                        }
                    }
                }
            }

            return map;
        }

        private static bool HasNeighborO(char[,] map, int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = x + i;
                    int neighborY = y + j;

                    if (neighborX >= 0 && neighborX < map.GetLength(0) &&
                        neighborY >= 0 && neighborY < map.GetLength(1))
                    {
                        if (map[neighborX, neighborY] == 'm')
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static void DrawMap(char[,] map)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    switch (map[x, y])
                    {
                        case '#':
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case 'm':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                    }
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static void DrawPlayer(int playerX, int playerY)
        {
            Console.SetCursorPosition(playerX, playerY);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@");
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int playerX, ref int playerY, char[,] map)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPlayerPositionX = playerX + direction[0];
            int nextPlayerPositionY = playerY + direction[1];
            char nextCell = map[nextPlayerPositionX, nextPlayerPositionY];

            if (nextCell == ' ' || nextCell == 'm')
            {
                playerX = nextPlayerPositionX;
                playerY = nextPlayerPositionY;
                if (nextCell == 'm')
                {
                    map[nextPlayerPositionX, nextPlayerPositionY] = ' ';
                }
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
                direction[1] -= 1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1] += 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0] -= 1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                direction[0] += 1;
            return direction;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (string line in lines)
            {
                if (line.Length > maxLength)
                    maxLength = line.Length;
            }
            return maxLength;
        }
    }
}


