using static TextRPG_Team20.ConsoleUI;
using System.Runtime.InteropServices;

namespace TextRPG_Team20
{
    internal class Program
    {
        struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        static void Main(string[] args)
        {
            /*            // Import the necessary functions from user32.dll
                        [DllImport("user32.dll")]
                        static extern IntPtr GetForegroundWindow();
                        [DllImport("user32.dll")]
                        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
                        [DllImport("user32.dll")]
                        static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
                        [DllImport("user32.dll")]
                        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
                        // Constants for the ShowWindow function
                        const int SW_MAXIMIZE = 3;
                        // Get the handle of the console window
                        IntPtr consoleWindowHandle = GetForegroundWindow();
                        // Maximize the console window
                        ShowWindow(consoleWindowHandle, SW_MAXIMIZE);
                        // Get the screen size
                        Rect screenRect;
                        GetWindowRect(consoleWindowHandle, out screenRect);
                        // Resize and reposition the console window to fill the screen
                        int width = screenRect.Right - screenRect.Left;
                        int height = screenRect.Bottom - screenRect.Top;

                        MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);*/

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll", SetLastError = true)]
            static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            Console.SetWindowSize(210, 55);
            Console.SetBufferSize(210, 55);

            int screenWidth = 1920;
            int screenHeight = 1080;

            int fontWidth = 8;
            int fontHeight = 16;

            int consoleWidth = 210 * fontWidth;
            int consoleHeight = 55 * fontHeight;

            int posX = (screenWidth - consoleWidth) / 2;
            int posY = (screenHeight - consoleHeight) / 2;

            IntPtr consoleWnd = GetConsoleWindow();
            MoveWindow(consoleWnd, posX, posY, consoleWidth, consoleHeight, true);

            Console.CursorVisible = false;
            Game game = new Game();


        }
    }
}
