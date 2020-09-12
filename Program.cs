using System;
using System.ComponentModel;
using System.IO;
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
            while (state)
            {
                string answer;
                bool resulato;
                PropertyInfo[] properties = typeof(Persona).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Console.Write($"\t{property.Name}: ");
                    string myValue = Console.ReadLine();
                    if(property.Name == "Edad")
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
            while(answer[0] != 'y' && answer[0] != 'n')
            {
                Console.WriteLine("Valor no valido");
                Console.WriteLine(erroHandler);
                answer = Console.ReadLine();
            }

            if( answer[0] == 'y')
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
