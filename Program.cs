using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;

namespace Registro
{
    class Program
    {
        static string fileName;
        static Persona persona = new Persona();
        //static bool resultado;
        static void Main(string[] args)
        {
            fileName = args[0];
            ValidateFile(fileName);
            ProgramEngine(true);
        }

        private static void ProgramEngine(bool state)
        {
            string answer;
            while (state)
            {
                Console.WriteLine("**************************");
                Console.WriteLine("Bienvenidos a la Registro_v2");
                Console.WriteLine("**************************\n");
                Console.WriteLine("(C)rear");
                Console.WriteLine("(L)lista");
                Console.WriteLine("(B)Buscar");
                Console.WriteLine("(D)elete");
                Console.WriteLine("(S)alir");
                Console.Write("Introduzca una opcion: ");
                answer = Console.ReadLine();
                switch (answer[0])
                {
                    case 'c':
                        Create(true);
                        break;
                    case 'l':
                        GetList(true);
                        break;
                    case 'b':
                        Console.WriteLine("Introduzca cedula de la persona: ");
                        string cedula = Console.ReadLine();
                        SearchPerson(true, cedula);
                        break;
                    case 'd':
                        Console.WriteLine("Introduzca cedula de la persona: ");
                        string cedulaBorrar = Console.ReadLine();
                        DeletePerson(cedulaBorrar);
                        break;
                    case 's':
                        state = false;
                        break;
                    default:
                        while (answer[0] != 'c' && answer[0] != 'l' && answer[0] != 'b' && answer[0] != 'd'
                            && answer[0] != 's')
                        {
                            Console.WriteLine("valor invalido");
                            Console.Write("Introduzca unan obcion valida: ");
                            answer = Console.ReadLine();
                            break;
                        }
                        break;
                }


            }


        }

        private static void DeletePerson(string cedulaBorrar)
        {
            throw new NotImplementedException();
        }

        private static void SearchPerson(bool status, string cedula)
        {
            string answer;
            List<Persona> myList = new List<Persona>();
            string text = System.IO.File.ReadAllText(fileName);
            string[] line = text.Split("\n");
            for (int i = 1; i < line.Length - 1; i++)
            {
                Persona persona = new Persona();
                string[] personaValue = line[i].Split(",");
                persona.Cedula = personaValue[0];
                persona.Nombre = personaValue[1];
                persona.Apellido = personaValue[2];
                persona.Edad = int.Parse(personaValue[3]);
                myList.Add(persona);
            }
            
            while (status)
            {
                if (myList.Exists(p => p.Cedula == cedula))
                {
                    persona = myList.Find(p => p.Cedula == cedula);
                    
                    Console.WriteLine($"{persona.Cedula}, {persona.Nombre}, {persona.Apellido}, {persona.Edad}");
                    Console.WriteLine("Desea Continuar y/n");
                    answer = Console.ReadLine();
                    status = ValidateAnswer(answer, "Desea Continuar y/n");
                    break;
                }
                else
                {
                    Console.WriteLine("La persona no existe");
                    Console.WriteLine("Desea volver a intentar y/n:");
                    answer = Console.ReadLine();
                    if(answer[0] == 'y')
                    {
                        Console.Write("Intruzca la cedula: ");
                        cedula = Console.ReadLine();
                    }
                    else
                    {
                        status = false;
                    }

                }
                
            }
           






        }

        private static void GetList(bool state)
        {
            string answer;
            List<Persona> myList = new List<Persona>();
            while (state)
            {
                string text = System.IO.File.ReadAllText(fileName);
                string[] line = text.Split("\n");
                for (int i = 1; i < line.Length - 1; i++)
                {
                    Persona persona = new Persona();
                    string[] personaValue = line[i].Split(",");
                    persona.Cedula = personaValue[0];
                    persona.Nombre = personaValue[1];
                    persona.Apellido = personaValue[2];
                    persona.Edad = int.Parse(personaValue[3]);
                    myList.Add(persona);
                }
                foreach (var persona in myList)
                {
                    Console.WriteLine($"{persona.Cedula}, {persona.Nombre}, {persona.Apellido}, {persona.Edad}");
                }
                Console.WriteLine("Desea continuar y/n");
                answer = Console.ReadLine();
                state = ValidateAnswer(answer, "Desea continuar y/n");
            }

        }

        private static void Create(bool state)
        {
            while (state)
            {
                string answer;
                bool resulato;
                PropertyInfo[] properties = typeof(Persona).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Console.Write($"\t{property.Name}: ");
                    string myValue = Console.ReadLine();
                    if (property.Name == "Edad")
                    {
                        int myValueToInt = int.Parse(myValue);
                        property.SetValue(persona, myValueToInt);
                    }
                    else
                    {
                        property.SetValue(persona, myValue);
                    }
                }
                Console.WriteLine("Desea guardar: y/n?");
                answer = Console.ReadLine();
                resulato = ValidateAnswer(answer, "Desea guardar: y/n?");
                if (resulato)
                {
                    SavePersonToCsv(persona);

                }
                Console.WriteLine("Desea continuar... y/n: ");
                answer = Console.ReadLine();
                state = ValidateAnswer(answer, "Desea continuar... y/n: ");
            }
        }

        private static void SavePersonToCsv(Persona persona)
        {
            using (var fileStrem = new FileStream(fileName, FileMode.Append))
            {
                AddText(fileStrem, persona.Cedula + ",");
                AddText(fileStrem, persona.Nombre + ",");
                AddText(fileStrem, persona.Apellido + ",");
                AddText(fileStrem, persona.Edad.ToString() + ",\n");
            }
        }

        private static bool ValidateAnswer(string answer, string erroHandler)
        {
            while (answer[0] != 'y' && answer[0] != 'n')
            {
                Console.WriteLine("Valor no valido");
                Console.WriteLine(erroHandler);
                answer = Console.ReadLine();
            }

            if (answer[0] == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ValidateFile(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                Console.WriteLine($"{fileName } no existe pero lo crearemos ;)");
                using (var fileStream = new FileStream(fileName, FileMode.Append))
                {
                    AddText(fileStream, "Cedula,Nombre,Apellido,Edad\n");
                }
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

    }
}
