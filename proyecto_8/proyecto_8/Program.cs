using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_8
{
   public class cliente
    {
        public int Turno { get; set;}
        public string Nombre { get; set;}
        public int Prioridad { get; set;}
        public cliente(int turno, string nombre, int prioridad)
        {
            Turno = turno;
            Nombre = nombre;
            Prioridad = prioridad;
        }
        public override string ToString()
        {
            return $"Turno{Turno}|{Nombre}|Prioridad{Prioridad}";
        }
    }
}
