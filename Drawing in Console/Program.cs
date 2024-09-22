using System;
using System.Collections.Generic;
using System.Threading;

class Program
{


    static int x = 10, y = 10;
    static ConsoleColor currentColor = ConsoleColor.White;
    static List<(int x, int y, ConsoleColor color)> trail = new List<(int, int, ConsoleColor)>();
    static string inputText = "";

    static void Main()
    {
        Console.CursorVisible = false;
        inputText = Console.ReadLine();



        while (true)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);


            switch (input.Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.W:
                    if (y > 0) y--;
                    break;
                case ConsoleKey.DownArrow or ConsoleKey.S:
                    if (y < Console.WindowHeight - 1) y++;
                    break;
                case ConsoleKey.LeftArrow or ConsoleKey.A:
                    if (x > 0) x--;
                    break;
                case ConsoleKey.RightArrow or ConsoleKey.D:
                    if (x < Console.WindowWidth - 1) x++;
                    break;
                case ConsoleKey.Spacebar:
                    ChangeColor();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            trail.Add((x, y, currentColor));
            DrawTrail();
        }
    }

    static void DrawTrail()
    {
        Console.Clear();

        foreach (var (posX, posY, color) in trail)
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = color;
            Console.Write(inputText);
        }

        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = currentColor;
        Console.Write(inputText);
    }

    static void ChangeColor()
    {
        var colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        Random random = new Random();
        currentColor = colors[random.Next(colors.Length)];
    }
}