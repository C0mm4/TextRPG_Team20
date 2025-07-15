using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    public class ConsoleUI
    {
        private static ConsoleUI? _instance;
        public static ConsoleUI Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ConsoleUI();
                }
                return _instance;
            }
        }

        public struct Rect
        {
            public int x;
            public int y;
            public int width;
            public int height;

            public int currentX;
            public int currentY;

            public List<string> lines;

            public Rect(int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;

                this.currentX = x;
                this.currentY = y;

                this.lines = new List<string>();
            }

            public void ResetCursor()
            {
                currentX = x;
                currentY = y;
            }

            public void ClearBuffer()
            {
                lines.Clear();
                ResetCursor();
            }
        }

        public static Rect mainView, logView, info1View, info2View, inputView;

        private ConsoleUI() 
        {
            mainView = new Rect(2, 1, 110, 29);
            logView = new Rect(116, 1, 42, 45);

            info1View = new Rect(2, 32, 52, 7);
            info2View = new Rect(60, 32, 52, 7);

            inputView = new Rect(2, 41, 110, 5);
        }

        /// <summary>
        /// 지정된 영역 안에 텍스트를 자동 줄바꿈하며 출력합니다.
        /// </summary>
        public void DrawTextInBox(string text, ref ConsoleUI.Rect rect)
        {
            string[] words = text.Split(' ');
            string line = "";

            foreach (var word in words)
            {
                if (GetDisplayWidth(line + word) > rect.width)
                {
                    if (rect.lines.Count >= rect.height) break;

                    rect.lines.Add(line.TrimEnd());
                    line = word + " ";
                }
                else
                {
                    line += word + " ";
                }
            }

            if (rect.lines.Count < rect.height && !string.IsNullOrWhiteSpace(line))
            {
                rect.lines.Add(line.TrimEnd());
            }
        }

        private void ClearLine(int x, int y, int width)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', width));
        }

        /// <summary>
        /// 전각 문자의 출력 폭을 고려하여 문자열 너비를 계산합니다.
        /// </summary>
        private int GetDisplayWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                width += IsFullWidth(c) ? 2 : 1;
            }
            return width;
        }

        /// <summary>
        /// 한글 및 전각 기호의 여부를 판단합니다.
        /// </summary>
        private bool IsFullWidth(char c)
        {
            return c >= 0x2E80 && c <= 0x9FFF || // 한자, CJK
                   c >= 0xFF01 && c <= 0xFF60 || // 전각 기호
                   c >= 0xAC00 && c <= 0xD7AF;   // 한글
        }

        /// <summary>
        /// 전각/반각을 고려해 우측 공백을 채워 출력 폭을 맞춥니다.
        /// </summary>
        private string PadRightDisplay(string text, int width)
        {
            int displayWidth = GetDisplayWidth(text);
            return text + new string(' ', Math.Max(0, width - displayWidth));
        }
        public void PrintView(Rect rect, string horizontalAlign = "left", string verticalAlign = "top")
        {
            ClearView(rect); // 먼저 지움

            int startY = rect.y;

            // 상하 정렬: 출력 시작 위치 조정
            if (verticalAlign == "middle")
            {
                int linesToPrint = Math.Min(rect.lines.Count, rect.height);
                int verticalPadding = (rect.height - linesToPrint) / 2;
                startY = rect.y + verticalPadding;
            }

            for (int i = 0; i < rect.height; i++)
            {
                Console.SetCursorPosition(rect.x, rect.y + i);
                Console.Write(new string(' ', rect.width)); // safety wipe
            }

            for (int i = 0; i < rect.lines.Count && i < rect.height; i++)
            {
                string line = rect.lines[i];
                int dx = 0;

                switch (horizontalAlign.ToLower())
                {
                    case "left":
                        dx = 0;
                        break;
                    case "right":
                        dx = rect.width - GetDisplayWidth(line);
                        break;
                    case "center":
                    default:
                        dx = (rect.width - GetDisplayWidth(line)) / 2;
                        break;
                }

                dx = Math.Max(0, dx); // underflow 방지

                Console.SetCursorPosition(rect.x + dx, startY + i);
                Console.Write(PadRightDisplay(line, rect.width - dx));
            }
        }

        public static void ClearView(Rect rect)
        {
            for (int i = 0; i < rect.height; i++)
            {
                Console.SetCursorPosition(rect.x, rect.y + i);
                Console.Write(new string(' ', rect.width));
            }
        }
    }
}
