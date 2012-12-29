using System;

namespace AnsiSharp
{
    internal static class EscapeCodes
    {
        // http://vt100.net/emu/dec_ansi_parser
        // http://www.ecma-international.org/publications/standards/Ecma-048.htm
        // http://bluesock.org/~willg/dev/ansi.html#ansicodes

        public const string EscapePrefix = "\x1b[";

        #region Colors

        public const string FormatEscape = @"(\d{1,2})(?:;(\d{1,2}))*m";

        public static readonly MirroredDict<ConsoleColor, int> ForegroundColor = new MirroredDict<ConsoleColor, int>
            {
                { ConsoleColor.Black , 30 },
                { ConsoleColor.Red, 31 },
                { ConsoleColor.Green, 32 },
                { ConsoleColor.Yellow, 33 },
                { ConsoleColor.Blue, 34 },
                { ConsoleColor.Magenta, 35 },
                { ConsoleColor.Cyan, 36 },
                { ConsoleColor.White, 37 }
            };

        public static readonly MirroredDict<ConsoleColor, int> BackgroundColor = new MirroredDict<ConsoleColor, int>
            {
                { ConsoleColor.Black, 40 },
                { ConsoleColor.Red, 41 },
                { ConsoleColor.Green, 42 },
                { ConsoleColor.Yellow, 43 },
                { ConsoleColor.Blue, 44 },
                { ConsoleColor.Magenta, 45 },
                { ConsoleColor.Cyan, 46 },
                { ConsoleColor.White, 47 }
            };

        #endregion

        #region Cursor Control

        public const string CursorMove = @"(?<row>\d+);(?<col>\d+)(H|f)";

        public const string CursorMoveUp = @"(?<row>\d+)A";

        public const string CursorMoveDown = @"(?<row>\d+)B";

        public const string CursorMoveLeft = @"(?<col>\d+)C";

        public const string CursorMoveRight = @"(?<col>\d+)D";

        #endregion

        #region Erase

        public const string ClearScreen = @"2J";

        public const string ClearLine = @"K";

        #endregion


    }
}
