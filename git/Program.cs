using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        public static class Error
        {
            public const string ErrIncorrectInput = "Error! Incorrect input value.";
            public const string ErrIncorrectResult = "Error! Incorrect result value.";

            public static bool ErrFlag { get; private set; }
            public static string ErrMessage { get; private set; }

            public static void SetError(string errMessage)
            {
                ErrMessage = errMessage;
                ErrFlag = true;
            }

            public static void Refresh()
            {
                ErrFlag = false;
                ErrMessage = string.Empty;
            }
        }

        public static class Calculator
        {
            static string FirstNum { get; set; }
            static string SecondNum { get; set; }
            public static string Result { get; private set; }
            static char Sign { get; set; }

            const int maxDigits = 6;

            static ConsoleKeyInfo KeyInfoBuffer { get; set; }

            public static void ExecuteOp()
            {
                while (true)
                {
                    KeyInfoBuffer = Console.ReadKey();
                    if (IsInputError())
                    {
                        Error.SetError(Error.ErrIncorrectInput);
                        return;
                    }
                    if ((Char.IsDigit(KeyInfoBuffer.KeyChar)
                         || (KeyInfoBuffer.Key == ConsoleKey.Spacebar
                             && FirstNum != null))
                         && Char.IsControl(Sign))
                        FirstNum += KeyInfoBuffer.KeyChar;

                    else if (!Char.IsDigit(KeyInfoBuffer.KeyChar) && Char.IsControl(Sign)
                             && (KeyInfoBuffer.KeyChar.Equals('+') || KeyInfoBuffer.KeyChar.Equals('-')
                                 || KeyInfoBuffer.KeyChar.Equals('*') || KeyInfoBuffer.KeyChar.Equals('/')))
                        Sign = KeyInfoBuffer.KeyChar;

                    else if ((Char.IsDigit(KeyInfoBuffer.KeyChar)
                              || (KeyInfoBuffer.Key == ConsoleKey.Spacebar
                                  && SecondNum != null))
                              && !Char.IsControl(Sign))
                        SecondNum += KeyInfoBuffer.KeyChar;

                    else if (KeyInfoBuffer.KeyChar.Equals('=') && !Char.IsControl(Sign))
                    {
                        Calculate();
                        return;
                    }
                }
            }

            static void Calculate()
            {
                if (Error.ErrFlag.Equals(true))
                    return;
                switch (Sign)
                {
                    case '+':
                        Result = (Double.Parse(FirstNum) + Double.Parse(SecondNum)).ToString();
                        break;
                    case '-':
                        Result = (Double.Parse(FirstNum) - Double.Parse(SecondNum)).ToString();
                        break;
                    case '*':
                        Result = (Double.Parse(FirstNum) * Double.Parse(SecondNum)).ToString();
                        break;
                    case '/':
                        Result = (Double.Parse(FirstNum) / Double.Parse(SecondNum)).ToString();
                        break;
                }
                if (IsResultError())
                    Error.SetError(Error.ErrIncorrectResult);
            }

            static bool IsInputError()
            {
                if (FirstNum != null)
                {
                    if (Char.IsDigit(KeyInfoBuffer.KeyChar)
                        && FirstNum.Last().Equals(' ')
                        && Char.IsControl(Sign))
                        return true;
                }
                if (SecondNum != null)
                {
                    if (Char.IsDigit(KeyInfoBuffer.KeyChar)
                        && SecondNum.Last().Equals(' '))
                        return true;
                }
                if ((!Char.IsDigit(KeyInfoBuffer.KeyChar)
                     && KeyInfoBuffer.Key != ConsoleKey.Spacebar
                     && KeyInfoBuffer.KeyChar != '-'
                     && KeyInfoBuffer.KeyChar != '+'
                     && KeyInfoBuffer.KeyChar != '/'
                     && KeyInfoBuffer.KeyChar != '*'
                     && KeyInfoBuffer.KeyChar != '=')
                    || (KeyInfoBuffer.KeyChar.Equals('=')
                        && (Char.IsControl(Sign)
                            || SecondNum == null))
                    || (KeyInfoBuffer.Key == ConsoleKey.Spacebar
                        && FirstNum == null))
                    return true;
                return false;
            }

            static bool IsResultError()
            {
                return false;
            }

            public static void Refresh()
            {
                FirstNum = null;
                SecondNum = null;
                Result = null;
                Sign = '\0';
                KeyInfoBuffer = new ConsoleKeyInfo();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Scheme: {first number}[spacebars]{sign}[spacebars]{second number}[spacebars]{=})");
            while (true)
            {
                Calculator.ExecuteOp();
                if (Error.ErrFlag.Equals(true))
                {
                    Console.WriteLine('\n' + Error.ErrMessage + '\n');
                    Error.Refresh();
                }
                else
                    Console.WriteLine(Calculator.Result + "\n");
                Calculator.Refresh();
            }
        }
    }
}
