using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Registro
{
    public class Persona : IDisposable
    {

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private short _data;
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public short Edad
        {
            get
            {
                return (short)(_data & 0b1111_1111);
            }

            set { _data |= value; }
        }
        public string Password { get; set; }

        //v6 properties (            ---------................-----------------    )
        public short sex
        {
            get
            {

                return (short)(_data & 0b1000 << 8);
            }
            set
            {
                _data |= value;
            }
        }

        public short EstadoCivil
        {
            get
            {
                return (short)(_data & 0b0100 << 8);
            }
            set
            {
                _data |= value;
            }
        }

        public short Grado
        {
            get
            {
                return (short)(_data & 0b0010 << 8);
            }
            set
            {
                _data |= value;
            }
        }

        public short Nacionalidad
        {
            get
            {
                return (short)(_data & 0b001 << 8);
            }
            set
            {
                _data |= value;
                _data = (short)(_data << 8);
            }
        }

        //persona(
        public Persona(string cedula, string nombre, string apellido, string password, short sex,
            short civil, short grado, short nacionalidad, short edad)
        {
            this.Cedula = cedula; //[0]
            this.Nombre = nombre; //[1]
            this.Apellido = apellido; //2
            this.Password = password; //3
            this.sex = sex; //4
            this.EstadoCivil = civil; //4
            this.Grado = grado;
            this.Nacionalidad = nacionalidad;
            this.Edad = edad;
        }

        public Persona(string c, string n, string a, string p, short d)
        {
            this.Cedula = c;
            this.Nombre = n;
            this.Apellido = a;
            this.Password = p;
            this._data = d;
        }



        public short GetData()
        {
            short data = _data;
            return data;
        }



        public override bool Equals(object obj)
        {
            if (obj is Persona other)
            {
                return this.Cedula.Equals(other.Cedula);

            }
            return false;

        }

        public override int GetHashCode()
        {

            return Apellido[0].GetHashCode();
        }

        public override string ToString()
        {

            return Cedula + "|" + Nombre + "|" + Apellido + "|" +
              "|" + (sex == (0b1000 << 8) ? "masculino" : "femenino") + "|"
              + ((EstadoCivil == (0b0100 << 8)) ? "Casado" : "Soltero") +
             "|" + ((Grado == (0b0010 << 8)) ? "Graduado" : "Dominicano") + "|" +
             "|" + ((Nacionalidad == (0b001 << 8)) ? "Dominicano" : "Extranjero") + "|"
               + Edad;


          

        }




        //Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }





        //Funcion autodestructiva, cumple su tarea y se destruye 'dejandome mas espacio en la memoria ;)'

    }
}
