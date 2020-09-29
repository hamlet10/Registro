using System;
using System.Collections.Generic;
using System.Text;

namespace Registro
{
    public static class Validators
    {
        public static string name()
        {
            char[] valor = new char[50];
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 65 && a <= 122)
                {
                    valor[x] = a;
                    ++x;
                    Console.Write(a);
                }

                if (a == 13)
                {
                    break;

                }

                if (a == 8 && x >= 1)
                {
                    Console.Write("\b \b");
                    --x;
                }

            }
            return new string(valor);
        }

        public static string apellido()
        {
            char[] valor = new char[50];
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 65 && a <= 122)
                {
                    valor[x] = a;
                    ++x;
                    Console.Write(a);
                }

                if (a == 13)
                {
                    break;

                }

                if (a == 8 && x >= 1)
                {
                    Console.Write("\b \b");
                    --x;
                }
            }
            return new string(valor);
        }

        public static string edad()
        {
            char[] valor = new char[10];
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 57)
                {
                    valor[x] = a;
                    ++x;
                    Console.Write(a);
                }

                if (a == 13)
                {
                    break;

                }

                if (a == 8 && x >= 1)
                {
                    Console.Write("\b \b");
                    --x;
                }
            }

            return new string(valor);
        }

        public static string cedula()
        {
            char[] valor = new char[15];
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 57)
                {
                    valor[x] = a;
                    ++x;
                    Console.Write(a);
                }

                if (a == 13)
                {
                    valor[x] = '\0';
                    break;
                }

                if (a == 8 && x >= 1)
                {
                    Console.Write("\b \b");
                    --x;
                }
            }
            return new string(valor);
        }
        public static string password()
        {
            char[] valor = new char[100];
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 122)
                {
                    valor[x] = a;
                    ++x;
                    Console.Write("*");
                }

                if (a == 13)
                {
                    break;
                }

                if (a == 8 && x >= 1)
                {
                    Console.Write("\b \b");
                    --x;
                }
            }
            return new string(valor);

        }
    }
}