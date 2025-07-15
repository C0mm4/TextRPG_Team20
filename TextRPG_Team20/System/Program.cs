using static TextRPG_Team20.ConsoleUI;
using System.Runtime.InteropServices;

namespace TextRPG_Team20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 160;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;
            Game game = new Game();
        }
    }
}
