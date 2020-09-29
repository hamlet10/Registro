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
            fileName = "Datos.csv";
            //fileName = args[0];
            ValidateFile(fileName);
            ProgramEngine(true);
        }

        private static void ProgramEngine(bool state)
        {
            string answer;
            while (state)
            {
                Console.WriteLine("***************************");
                Console.WriteLine("Bienvenidos a la Registro_v3");
                Console.WriteLine("***************************\n");
                Console.WriteLine("(C)rear");
                Console.WriteLine("(L)lista");
                Console.WriteLine("(B)Buscar");
                Console.WriteLine("(E)ditar");
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
                    case 'e':
                        Editar();
                        break;
                    case 'd':
                        DeletePerson();
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
        private static void Editar()
        {

            bool resultado;
            string answer;
            Console.WriteLine("Introduzca la cédula de la persona que desea editar: ");
            string cedulaEditar = Console.ReadLine();
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
            int editingIndex = myList.FindIndex(p => p.Cedula == cedulaEditar);
            persona = myList.Find(p => p.Cedula == cedulaEditar);
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

            Console.Write("Desea guardar, y/n? ");
            answer = Console.ReadLine();
            resultado = ValidateAnswer(answer, "Desea guardar y/n? ");

            if (resultado)
            {
                myList[editingIndex] = persona;
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    AddText(fileStream, "Cedula,");
                    AddText(fileStream, "Nombre,");
                    AddText(fileStream, "Apellido,");
                    AddText(fileStream, "Edad\n");
                }
                foreach (Persona persona in myList)
                {
                    SavePersonToCsv(persona);
                }
            }
        }

        private static void DeletePerson()
        {
            string answer;
            bool state = true;
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
            while (state)
            {
                Console.Write("Introduzca cédula de la persona que desea borra: ");
                string cedulaBorrar = Console.ReadLine();
                if (myList.Exists(p => p.Cedula == cedulaBorrar))
                {
                    persona = myList.Find(p => p.Cedula == cedulaBorrar);
                    Console.WriteLine("esta seguro que desea borrar a");
                    Console.WriteLine($"{persona.Cedula}, {persona.Nombre}, {persona.Apellido}, {persona.Edad}");
                    answer = Console.ReadLine();
                    if (answer[0] == 'y')
                    {
                        myList.Remove(persona);
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            AddText(fileStream, "Cedula,");
                            AddText(fileStream, "Nombre,");
                            AddText(fileStream, "Apellido,");
                            AddText(fileStream, "Edad,\n");
                        }
                        foreach (var persona in myList)
                        {
                            SavePersonToCsv(persona);
                        }
                        Console.WriteLine("Proceso de borrado completado exitosamente");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontro {cedulaBorrar}");
                    Console.WriteLine("Desea volver a intentar y/n");
                    answer = Console.ReadLine();
                    state = ValidateAnswer(answer, "Desea continuar y/n?");
                }
            }

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
                    if (answer[0] == 'y')
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
                    persona.Password = personaValue[4];
                    myList.Add(persona);
                }
                Console.WriteLine("Cedula, Nombre, Apellido, Edad, Contraseña");
                foreach (var persona in myList)
                {
                    Console.WriteLine($"{persona.Cedula}, {persona.Nombre}, {persona.Apellido}, {persona.Edad}, {persona.Password}");
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
                    if (property.Name == "Cedula")
                    {
                        Console.Write($"\t{property.Name}: ");
                        property.SetValue(persona, Validators.cedula());
                        Console.Write("\n");
                    }
                    //string myValue = Console.ReadLine();
                    if (property.Name == "Nombre")
                    {
                        Console.Write($"\t{property.Name}: ");
                        property.SetValue(persona, Validators.name());
                        Console.Write("\n");
                    }
                    if (property.Name == "Apellido")
                    {
                        Console.Write($"\t{property.Name}: ");
                        property.SetValue(persona, Validators.apellido());
                        Console.Write("\n");
                    }
                    if (property.Name == "Edad")
                    {
                        Console.Write($"\t{property.Name}: ");
                        int myValueToInt = int.Parse(Validators.edad());
                        property.SetValue(persona, myValueToInt);
                        Console.Write("\n");
                    }
                    if (property.Name == "Password")
                    {
                        Console.Write("\tDigite la contraseña (solo números del 0 al 9 y letras del abecedario (mayúsculas o minúsculas)): ");
                        string pass1 = Validators.password();
                        Console.Write("\nVuelva y digite la contraseña (verificación de igualdad): ");
                        string pass2 = Validators.password();
                        if (pass1 == pass2)
                        {
                            property.SetValue(persona, pass1);
                            Console.Write("\nCotraseña exitosa\n");
                        }
                        else
                        {
                            Console.Error.WriteLine("\nLas contraseñas son diferentes. ");
                            Console.Write("\nVuelva y digite la contraseña (verificación de igualdad): ");
                            pass2 = Validators.password();
                            if (pass1 == pass2)
                            {
                                property.SetValue(persona, pass1);
                                Console.Write("\nCotraseña exitosa\n");
                            }


                            else
                            {
                                Console.WriteLine("\nNo logró igualar las contraseñas. Intente mañana");
                            }
                        }

                    }

                    //else
                    //{
                    //    property.SetValue(persona, myValue);
                    //}
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
                AddText(fileStrem, persona.Edad.ToString() + ",");
                AddText(fileStrem, persona.Password + ",\n");
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
