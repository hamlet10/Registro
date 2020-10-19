using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Registro
{
    public class CVSManager
    {
        /*"D:\\Hamlet\\Intec\\Registro\\Registro"*/
        private string _path =  Directory.GetCurrentDirectory();

        public CVSManager(string path)
        {
            if (path != null)
            {
                _path = _path + "\\" + path;
            }
            if (!File.Exists(_path))
            {
                Console.WriteLine($"{path} NO EXISTE... pero lo crearemos por ti ;)");
                using (StreamWriter sw = new StreamWriter(_path, true))
                {
                    sw.WriteLine("Cedula,Nombre,Apellido,Data");
                }


            }

        }


        //inserta una persona en el csv
        public void Create(Persona persona)
        {
            
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine($"{persona.Cedula},{persona.Nombre},{persona.Apellido},{persona.Password},{persona.GetData()}");
            }
        }

        public List<Persona> GetAll()
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                List<Persona> personas = new List<Persona>();
                string text = sr.ReadToEnd();
                string[] lines = text.Split("\r\n");
                for (int i = 1; i < lines.Length - 1; i++)
                {
                    string[] items = lines[i].Split(",");

                    //cedula   nombre    apellido  password  
                    Persona persona = new Persona(items[0], items[1], items[2], items[3], short.Parse(items[4]));

                    personas.Add(persona);

                }

                return personas;
            }

        }

        public void Editar(List<Persona> personas)
        {

            using (StreamWriter sw = new StreamWriter(_path, false))
            {
                sw.WriteLine("Cedula,Nombre,Apellido,Data");
            }
            foreach (var persona in personas)
            {
                using (StreamWriter sw = new StreamWriter(_path, true))
                {
                    sw.WriteLine($"{persona.Cedula},{persona.Nombre},{persona.Apellido},{persona.Password},{persona.GetData()}");
                }
            }
        }
    }
}
