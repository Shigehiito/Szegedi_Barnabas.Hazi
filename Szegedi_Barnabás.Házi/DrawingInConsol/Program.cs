using System;
using System.Collections.Generic;

class Program
{
    static int x = 10, y = 10;
    static int prevX = 10, prevY = 10;
    static ConsoleColor currentColor = ConsoleColor.White;
    static char currentChar = '█';
    static char prevChar = ' ';

    static List<(int x, int y, char character, ConsoleColor color)> trail = new List<(int, int, char, ConsoleColor)>();


    static Dictionary<ConsoleKey, char> charMapping = new Dictionary<ConsoleKey, char>
    {
        { ConsoleKey.F1, '█' },
        { ConsoleKey.F2, '▓' },
        { ConsoleKey.F3, '▒' },
        { ConsoleKey.F4, '░' }
    };

    static Dictionary<ConsoleKey, ConsoleColor> colorMapping = new Dictionary<ConsoleKey, ConsoleColor>
    {
        { ConsoleKey.D1, ConsoleColor.Red },
        { ConsoleKey.D2, ConsoleColor.Green },
        { ConsoleKey.D3, ConsoleColor.Blue },
        { ConsoleKey.D4, ConsoleColor.Yellow },
        { ConsoleKey.D5, ConsoleColor.Cyan },
        { ConsoleKey.D6, ConsoleColor.Magenta },
        { ConsoleKey.D7, ConsoleColor.White },
        { ConsoleKey.D8, ConsoleColor.Gray }
    };

    static void Main()
    {
        Console.CursorVisible = false;
        ConsoleKeyInfo input;
        DrawCursor();
        do
        {
            input = Console.ReadKey(true);

            bool positionChanged = false;

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    //if(0<Console.CursorTop) Console.CursorTop -= 1;
                    if (y > 0) { prevY = y; prevX = x; y--; positionChanged = true; }
                    break;
                case ConsoleKey.DownArrow:
                    if (y < Console.WindowHeight - 2) { prevY = y; prevX = x; y++; positionChanged = true; }
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 0) { prevY = y; prevX = x; x--; positionChanged = true; }
                    break;
                case ConsoleKey.RightArrow:
                    if (x < Console.WindowWidth - 1) { prevY = y; prevX = x; x++; positionChanged = true; }
                    break;
                case ConsoleKey.Spacebar:
                    trail.Add((x, y, currentChar, currentColor));
                    DrawCharacter(x, y, currentChar, currentColor);
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                    Console.ForegroundColor = colorMapping[input.Key];
                    break;
            }

            if (colorMapping.ContainsKey(input.Key))
            {
                currentColor = colorMapping[input.Key];
            }

            if (charMapping.ContainsKey(input.Key))
            {
                currentChar = charMapping[input.Key];
            }

            if (positionChanged)
            {
                ClearCursorArea(prevX, prevY);
                DrawCursor();
            }
        }
        while (input.Key != ConsoleKey.Escape);
    }

    static void DrawCharacter(int posX, int posY, char character, ConsoleColor color)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = color;
        Console.Write(character);
    }

    static void DrawCursor()
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = currentColor;
        Console.Write('_');

        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Kurzor pozíció: ({x}, {y}) | Prev. Kurzor pozíció: ({prevX}, {prevY}) | Szín: {currentColor} | Karakter: {currentChar} ");
    }

    static void ClearCursorArea(int posX, int posY)
    {
        var existingTrail = trail.Find(t => t.x == posX && t.y == posY);
        if (existingTrail != default)
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = existingTrail.color;
            Console.Write(existingTrail.character);
        }
        else
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(' ');
        }
    }
}
