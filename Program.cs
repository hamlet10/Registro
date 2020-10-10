using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Registro
{
    class Program
    {
        static string fileName = "Datos.csv";
        private static CVSManager manager = new CVSManager(fileName);
        //static Persona persona = new Persona();
        //static bool resultado;

        static void Main(string[] args)

        {
           
            ProgramEngine(true);
        }

        private static void ProgramEngine(bool state)
        {
            char answer;
            while (state)
            {
                Console.WriteLine("\n\n******************************");
                Console.WriteLine("Bienvenidos a la Registro_v6");
                Console.WriteLine("******************************\n");
                Console.WriteLine("(C)rear");
                Console.WriteLine("(L)ista");
                Console.WriteLine("(B)uscar");
                Console.WriteLine("(E)ditar");
                Console.WriteLine("(D)elete");
                Console.WriteLine("(S)alir");
                Console.Write("Introduzca una opcion: ");
                answer = (char)Console.ReadKey().KeyChar;
                switch (answer)
                {
                    case 'c':
                        Create();
                        break;
                    case 's':
                        state = !state;
                        break;
                    case ('l'):
                        GetList();
                        break;
                    case 'e':
                        Editar();
                        break;
                    case 'd':
                        Eliminar();
                        break;
                    default:
                        Console.WriteLine("we're bussy here, so se you later:");
                        state = !state;
                        break;

                }
            }
        }
        static void Create()
        {
            bool state = true;
            while (state)
            {
                Console.Write("\n\n\tCedula: ");
                string cedula = ValidateCedula();
                Console.Write("\n\tNombre: ");
                string nombre = ValidateName();
                Console.Write("\n\tApellido: ");
                string apellido = ValidateLastName();
                Console.Write("\n\tPassword: ");
                string password1 = ValidatePassword();
                Console.Write("\n\tConfirme su Contraseña: ");
                string password2 = ValidatePassword();
                if (!SamePasswordValidataor(password1, password2))
                {
                    state = false;

                }

                Persona persona = new Persona(cedula, nombre, apellido, password1, ValidateSex(), ValidateEstadoCivil(), GradoAcademico(), Nacionalidad(), Age());


                if (ValidateAnswer("\nDesea guardar y/n: ", 'y', 'n')) 
                {
                    
                    manager.Create(persona);
                    persona.Dispose();
                }
                if (ValidateAnswer("\nDesea Salir y/n: ", 'y', 'n'))
                {
                    state = false;
                }
                else
                {
                    state = true;
                }


            }

        }

        static void GetList()
        {
            List<Persona> personas = manager.GetAll();
            Console.Write("\n\n\t|Reg.No|Cedular|Nombre|Apellido|Edad|Contraseña");

            for (int i = 0; i < personas.Count(); i++)
            {
                int index = i + 1;
                Console.Write(

                    $"\n\t----------------------------------------------------\n" +
                 index + "|" + personas[i].ToString());
            }


            personas = null;
        }

        static void Editar()
        {
            bool state = true;
            while (state)
            {
                Console.Write("\n\tIntroduzca la cedula de la persona que desea editar: ");
                string cedula = ValidateCedula();
                List<Persona> personas = manager.GetAll();
                var persona = personas.Find(x => x.Cedula == cedula);
                while (persona == null)
                {
                    Console.Write("\n\tNo se pudo encontra a la persona, vuelva a intentar: ");
                    cedula = ValidateCedula();
                    persona = personas.Find(x => x.Cedula == cedula);
                }
                Console.Write("\n\n\tCedula: ");
                string Newcedula = ValidateCedula();
                Console.Write("\n\tNombre: ");
                string nombre = ValidateName();
                Console.Write("\n\tApellido: ");
                string apellido = ValidateLastName();
                Console.Write("\n\tEdad: ");
                short edad = ValidateAge();
                Console.Write("\n\tPassword: ");
                string password1 = ValidatePassword();
                int index = personas.IndexOf(persona);
                personas[index].Cedula = Newcedula;
                personas[index].Nombre = nombre;
                personas[index].Apellido = apellido;
                personas[index].Edad = edad;
                personas[index].Password = password1;
                if (ValidateAnswer("\nDesea guardar y/n: ", 'y', 'n'))
                {
                    manager.Editar(personas);
                }

                if (ValidateAnswer("\nDesea Salir y/n: ", 'y', 'n'))
                {
                    state = false;
                }
                else
                {
                    state = true;
                }

            }


        }

        static void Eliminar()
        {
            bool state = true;
            while (state)
            {
                Console.Write("\n\tIntroduzca la cedula de la persona que desea Eliminar: ");
                string cedula = ValidateCedula();
                List<Persona> personas = manager.GetAll();
                var persona = personas.Find(x => x.Cedula == cedula);
                while (persona == null)
                {
                    Console.Write("\n\tNo se pudo encontra a la persona, vuelva a intentar: ");
                    cedula = ValidateCedula();
                    persona = personas.Find(x => x.Cedula == cedula);
                }
                personas.Remove(persona);

                if (ValidateAnswer("\nDesea guardar y/n: ", 'y', 'n'))
                {
                    if (personas.Count == 0)
                    {
                        manager.Editar(personas);
                    }
                    manager.Editar(personas);
                }

                if (ValidateAnswer("\nDesea Salir y/n: ", 'y', 'n'))
                {
                    state = false;
                }
                else
                {
                    state = true;
                }

            }
        }


















        //Validations
        static bool ValidateAnswer(string q = "your binary Question y/n", char valueTrue = 'y', char valueFalse = 'n')
        {
            Console.Write("\n" + q);
            char answer = (char)Console.ReadKey().KeyChar;

            while (answer != valueFalse && answer != valueTrue)
            {
                Console.WriteLine("Valor Invalido, vuelva a intentar " + Console.Error);
                Console.Write(q);
                answer = (char)Console.ReadKey().KeyChar;
            }


            if (answer == valueFalse)
            {
                return false;
            }
            else
            {
                return true;

            }

        }

        private static string ValidateName()
        {
            List<char> valor = new List<char>();
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 65 && a <= 122)
                {
                    valor.Add(a);
                    //++x;
                    Console.Write(a);
                }

                if (a == 13 && valor.Count >= 1)
                {
                    break;

                }

                if (a == 8 && valor.Count >= 1)
                {
                    Console.Write("\b \b");
                    valor.RemoveAt(x);
                    //--x;
                }


            }

            return new string(valor.ToArray());
        }
        private static string ValidateCedula()
        {
            List<char> valor = new List<char>();
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 57)
                {
                    valor.Add(a);
                    //++x;
                    Console.Write(a);
                }

                if (a == 13 && valor.Count >= 1)
                {
                    break;

                }

                if (a == 8 && valor.Count >= 1)
                {
                    Console.Write("\b \b");
                    valor.RemoveAt(x);
                    //--x;
                }


            }

            return new string(valor.ToArray());
        }
        private static string ValidateLastName()
        {
            List<char> valor = new List<char>();
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 65 && a <= 122)
                {
                    valor.Add(a);
                    //++x;
                    Console.Write(a);
                }

                if (a == 13 && valor.Count >= 1)
                {
                    break;

                }

                if (a == 8 && valor.Count >= 1)
                {
                    Console.Write("\b \b");
                    valor.RemoveAt(x);
                    //--x;
                }


            }

            return new string(valor.ToArray());
        }
        private static short ValidateAge()
        {
            List<char> valor = new List<char>();
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 57)
                {
                    valor.Add(a);
                    //++x;
                    Console.Write(a);
                }

                if (a == 13 && valor.Count >= 1)
                {
                    break;

                }

                if (a == 8 && valor.Count >= 1)
                {
                    Console.Write("\b \b");
                    valor.RemoveAt(x);
                    //--x;
                }


            }

            return Int16.Parse(new string(valor.ToArray()));
        }
        private static string ValidatePassword()
        {
            List<char> valor = new List<char>();
            char a;
            for (int x = 0; ;)
            {
                a = Console.ReadKey(true).KeyChar;
                if (a >= 48 && a <= 122)
                {
                    valor.Add(a);
                    //++x;
                    Console.Write("*");
                }

                if (a == 13 && valor.Count >= 1)
                {
                    break;

                }

                if (a == 8 && valor.Count >= 1)
                {
                    Console.Write("\b \b");
                    valor.RemoveAt(x);
                    //--x;
                }


            }

            return new string(valor.ToArray());
        }
        private static bool SamePasswordValidataor(string pass1, string pass2)
        {
            if (pass1 == pass2)
            {
                Console.Write("\n\tContraseña exitosa");
                return true;
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta, vuelva a intentar");
                Console.Write("contraseña");
                pass2 = ValidatePassword();
                if (pass1 == pass2)
                {
                    Console.Write("Contraseña exitosa");
                    return true;
                }
            }
            return false;
        }
        private static short ValidateSex()
        {
            if (ValidateAnswer("Sexo (M)asculino/(F)emenino: ", 'm', 'f'))
            {
                return (short)(0b1000);
            }
            else
            {
                return (short)(0);
            }
        }
        private static short ValidateEstadoCivil()
        {
            if (ValidateAnswer("Estado Civil (C)asado/(S)oltero: ", 'c', 's'))
            {
                return (short)(0b0100);
            }
            else
            {
                return (short)(0);
            }
        }
        private static short GradoAcademico()
        {
            if (ValidateAnswer("Grado Academico (G)raduado/(E)studiante ", 'g', 'e'))
            {
              
                return (short)(0b0010);
            }
            else
            {
                return (short)(0);
            }
        }
        private static short Nacionalidad()
        {
            if (ValidateAnswer("Nacionalidad (D)ominicano/(E)xtranjero ", 'd', 'e'))
            {
                return (short)(0b0001);
            }
            else
            {
                return (short)(0);
            }
        }
        private static short Age()
        {

            Console.Write("\n\tEdad: ");
            short edad = ValidateAge();
            //while (edad > 120 || edad < 0)
            //{
            //    Console.Write("edad invalida, vuelve a intentar: ");
            //    edad = ValidateAge();
            //}
            return edad;
        }
    }


}








