using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Registro
{
    public static class OtherInfoValidator
    {
        private static Int16 bwresut;
        static Int16 ValidateAnswerS()
        {
            Console.Write("\tSexo (M)asculino/(F)emenino: ");
            string answer = Console.ReadLine();
            while (answer[0] != 'm' && answer[0] != 'f')
            {
                Console.Write("\tValor invalido vuelva a intentar");
                Console.WriteLine("\tSexo (M)asculino/(F)emenino: ");
                answer = Console.ReadLine();
                break;
            }
            if (answer[0] == 'm')
            {
                return Convert.ToInt16(8);
            }
            else
            {
                return Convert.ToInt16(0);
            }

        }

        static Int16 ValidateAnswerE()
        {
            Console.Write("\tEstado civil (S)oltero/(C)asado: ");
            string answer = Console.ReadLine();
            while (answer[0] != 'c' && answer[0] != 's')
            {
                Console.Write("\tValor invalido vuelva a intentar");
                Console.Write("tEstado civil (S)oltero/(C)asado: ");
                answer = Console.ReadLine();
                break;
            }
            if (answer[0] == 'c')
            {
                return Convert.ToInt16(4);
            }
            else
            {
                return Convert.ToInt16(0);
            }


            
        }
        static Int16 ValidateAnswerG()
        {
            Console.Write("\t(E)studiante/(G)raduado: ");
            string answer = Console.ReadLine();
            while (answer[0] != 'g' && answer[0] != 'e')
            {
                Console.Write("\tValor invalido vuelva a intentar");
                Console.Write("\t(E)studiante/(G)raduado: ");
                answer = Console.ReadLine();
                break;
            }
            if (answer[0] == 'g')
            {
                return Convert.ToInt16(2);
            }
            else
            {
                return Convert.ToInt16(0);
            }

        }

        static Int16 ValidateAnswerA()
        {
            Console.Write("\t(D)ominicano/(E)extranjero: ");
            string answer = Console.ReadLine();
            while (answer[0] != 'd' && answer[0] != 'e')
            {
                Console.Write("\tValor invalido vuelva a intentar");
                Console.Write("\t(D)ominicano/(E)extranjero: ");
                answer = Console.ReadLine();
                break;
            }
            if (answer[0] == 'd')
            {
                return Convert.ToInt16(1);
            }
            else
            {
                return Convert.ToInt16(0);
            }

           
        }

        private static short GetAge()
        {
            Console.Write("\tEdad: ");
            short age = Convert.ToInt16(Console.ReadLine());
            return age;
        }



        public static Int16 bitsWiseOperationResult()
        {
            bwresut = Convert.ToInt16( ValidateAnswerS() | ValidateAnswerE() | ValidateAnswerG() | ValidateAnswerA());
            bwresut = Convert.ToInt16(bwresut << 8);
            bwresut = Convert.ToInt16(bwresut | GetAge());
            return bwresut;
        }

        
    }
}
