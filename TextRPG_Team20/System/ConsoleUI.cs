using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            public void DrawRect()
            {
                int border = 0;
                char text = '=';
                for (int j = y - 1; j <= y + height; j++)
                {
                    if (j > y - 1 && j < y + height)
                        border = 2;
                    else 
                        border = 0;
                    for (int i = x - 2; i <= x + width; i ++)
                    {
                        switch (border)
                        {
                            case 2:
                                if (i > x-2 && i < x + width)
                                {
                                    i = x + width-1;
                                    continue;
                                }
                                else
                                {
                                    text = '│';
                                }
                                break;
                            default:
                                text = '=';
                                break;
                        }
                        Console.SetCursorPosition(i, j);
                        Console.Write(text);
                    }
                
                }
            }
        }

        public static Rect mainView, logView, info1View, info2View, inputView;

        private ConsoleUI() 
        {
            mainView = new Rect(2, 1, 110, 32);
            logView = new Rect(116, 1, 80, 48);

            info1View = new Rect(2, 35, 52, 7);
            info2View = new Rect(60, 35, 52, 7);

            inputView = new Rect(2, 44, 110, 5);
        }

        public void InsertTextInBox(List<string> strings, ref Rect rect)
        {
            rect.lines.AddRange(strings);
        }


        public void DrawTextInBox(string text, ref Rect rect)
        {
            if (rect.lines == null)
                rect.lines = new List<string>();

            // 새로 추가될 줄들을 계산
            List<string> newLines = new List<string>();

            if (string.IsNullOrEmpty(text))
            {
                newLines.Add("");
            }
            else
            {
                string[] words = text.Split(' ');
                string line = "";

                foreach (var word in words)
                {
                    if (GetDisplayWidth(line + word) > rect.width)
                    {
                        newLines.Add(line.TrimEnd());
                        line = word + " ";
                    }
                    else
                    {
                        line += word + " ";
                    }
                }

                if (!string.IsNullOrWhiteSpace(line))
                    newLines.Add(line.TrimEnd());
            }

            // 전체 줄 수가 height를 초과할 경우 → 오래된 줄부터 제거
            int total = rect.lines.Count + newLines.Count;
            int overflow = total - rect.height;

            if (overflow > 0)
            {
                rect.lines.RemoveRange(0, overflow);
            }

            rect.lines.AddRange(newLines);
        }


        public static int GetDisplayWidth(string text)
        {
            // ANSI 코드 제거 (\u001b[...m)
            string noAnsi = Regex.Replace(text, @"\x1B\[[0-9;]*m", "");

            // 전각/반각 글자 계산
            int width = 0;
            foreach (var ch in noAnsi)
            {
                // 유니코드 범위 기준으로 전각 여부 판별
                width += IsFullWidth(ch) ? 2 : 1;
            }
            return width;
        }

        public static bool IsFullWidth(char ch)
        {
            // CJK, 일본어, 한글, 전각 특수문자 등
            return ch >= 0x1100 && (
                ch <= 0x115F || // Hangul Jamo
                ch == 0x2329 || ch == 0x232A ||
                (ch >= 0x2E80 && ch <= 0xA4CF) ||
                (ch >= 0xAC00 && ch <= 0xD7A3) || // Hangul syllables
                (ch >= 0xF900 && ch <= 0xFAFF) ||
                (ch >= 0xFE10 && ch <= 0xFE6F) ||
                (ch >= 0xFF00 && ch <= 0xFF60) ||
                (ch >= 0xFFE0 && ch <= 0xFFE6));
        }

        /// <summary>
        /// 전각/반각을 고려해 우측 공백을 채워 출력 폭을 맞춥니다.
        /// </summary>
        public static string PadRightDisplay(string text, int targetWidth)
        {
            int currentWidth = GetDisplayWidth(text);
            int padding = Math.Max(0, targetWidth - currentWidth);
            return text + new string(' ', padding);
        }

        public static string PadLeftDisplay(string text, int targetWidth)
        {
            return "";
        }

        public void PrintView(ref Rect rect, string hAlign = "left", string vAlign = "top")
        {
            ClearView(rect);

            int linesToPrint = Math.Min(rect.lines.Count, rect.height);

            int startY = rect.y;
            if (vAlign == "middle")
                startY = rect.y + (rect.height - linesToPrint) / 2;

            int lastPrintedY = rect.y;
            int lastCursorX = rect.x;

            for (int i = 0; i < linesToPrint; i++)
            {
                string line = rect.lines[i];
                int lineWidth = GetDisplayWidth(line);
                int dx = 0;

                if (hAlign == "center")
                    dx = (rect.width - lineWidth) / 2;
                else if (hAlign == "right")
                    dx = rect.width - lineWidth;

                dx = Math.Max(0, dx);

                int cursorX = rect.x + dx;
                int cursorY = startY + i;

                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(line);  // ❗ PadRightDisplay 제거

                // 마지막 커서 위치 기록
                lastPrintedY = cursorY;
                lastCursorX = cursorX + lineWidth;
            }

            // ✅ 출력 후 위치 업데이트
            rect.currentY = (lastPrintedY - rect.y) + 1;
            rect.currentX = lastCursorX;

        }

        public static void ClearView(Rect rect)
        {
            for (int i = 0; i < rect.height; i++)
            {
                Console.SetCursorPosition(rect.x, rect.y + i);
                Console.Write(new string(' ', rect.width));
            }
        }
        public static string Read(ref Rect rect)
        {
            // 입력할 위치: rect.x + currentX + 1 (한 칸 미뤄서 공백)
            int inputStartX = rect.x + rect.currentX - 1;

            // 입력할 y 좌표: currentY (현재 출력 다음 줄 위치)
            int inputY = rect.currentY + rect.y - 1;

            // 자리 확보용 공백 출력
            Console.SetCursorPosition(inputStartX, inputY);
            Console.Write(" ");

            // 커서 위치 재설정
            Console.SetCursorPosition(inputStartX, inputY);

            Console.CursorVisible = true;
            string input = Console.ReadLine() ?? " "; 
            input = " " + input;  // 앞에 공백 추가
            Console.CursorVisible = false;

            if (rect.lines != null && rect.lines.Count > 0)
            {
                rect.lines[rect.lines.Count - 1] = rect.lines.Last() + input;
            }
            else
            {
                rect.lines = new List<string> { input };
            }

            return input;
        }

        public static void SplitRectHorizontal(Rect original, out Rect left, out Rect right)
        {
            int halfWidth = original.width / 2;

            left = new Rect
            {
                x = original.x,
                y = original.y,
                width = halfWidth,
                height = original.height,
                currentY = original.y,
                lines = new List<string>()
            };

            right = new Rect
            {
                x = original.x + halfWidth,
                y = original.y,
                width = original.width - halfWidth, // 보정
                height = original.height,
                currentY = original.y,
                lines = new List<string>()
            };
        }
    }
}
