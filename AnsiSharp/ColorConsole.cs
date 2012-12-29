using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AnsiSharp
{
    public static class ColorConsole
    {
        // TODO skip Console.Color and go lower-level? http://code.google.com/p/roverdotnet/source/browse/trunk/ColorConsole/ConsoleClass/ColorConsole.cs?r=3
        // TODO integrate format-to-ANSI converter? http://www.codeproject.com/Articles/24753/Using-ANSI-Colors-within-NET

        public static void Write(object o)
        {
            var oldFgColor = Console.ForegroundColor;
            var oldBgColor = Console.BackgroundColor;

            var text = o.ToString();
            var segments = Regex.Split(text, Regex.Escape(EscapeCodes.EscapePrefix));

            foreach (var seg in segments)
            {
                var colorMatch = Regex.Match(seg, "^" + EscapeCodes.FormatEscape, RegexOptions.Singleline);
                if (colorMatch.Success)
                {
                    // Ignore the full match, start at 1
                    for (var i = 1; i < colorMatch.Groups.Count; i++)
                    {
                        foreach (Capture cap in colorMatch.Groups[i].Captures)
                        {
                            var code = Int32.Parse(cap.Value);
                            ConsoleColor parsedColor;

                            if (Enum.IsDefined(typeof (TextAttributes), code))
                            {
                                switch ((TextAttributes) code)
                                {
                                    case TextAttributes.Normal:
                                        Console.ForegroundColor = oldFgColor;
                                        Console.BackgroundColor = oldBgColor;
                                        break;

                                    case TextAttributes.Reverse:
                                    case TextAttributes.ReverseOff:
                                        var tmp = Console.ForegroundColor;
                                        Console.ForegroundColor = Console.BackgroundColor;
                                        Console.BackgroundColor = tmp;
                                        break;
                                }
                                // Do nothing otherwise... C# console can't bold/italic/underline
                            }
                            else if (EscapeCodes.ForegroundColor.TryGetValue(code, out parsedColor))
                            {
                                Console.ForegroundColor = parsedColor;
                            }
                            else if (EscapeCodes.BackgroundColor.TryGetValue(code, out parsedColor))
                            {
                                Console.BackgroundColor = parsedColor;
                            }
                        }
                    }
                    if (colorMatch.Length < seg.Length)
                    {
                        Console.Write(seg.Substring(colorMatch.Length));
                    }
                    continue;
                }

                // TODO Move cursor

                // TODO Save cursor position

                // TODO Return to cursor position

                // TODO Clear screen

                // TODO Clear to end of line

                Console.Write(seg);
            }
        }

        public static ConsoleColor ToConsoleColor(Color color)
        {
            ConsoleColor ret = 0;
            var delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : (n ?? "White")); // bug fix
                var t = Math.Pow(c.R - color.R, 2.0) + Math.Pow(c.G - color.G, 2.0) + Math.Pow(c.B - color.B, 2.0);
                if (Equals(t, 0.0))
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }

        public static void WriteLine(object o)
        {
            Write(o);
            Console.WriteLine();
        }

        public static string StripAnsi(string s)
        {
            throw new NotImplementedException();
        }
    }
}
