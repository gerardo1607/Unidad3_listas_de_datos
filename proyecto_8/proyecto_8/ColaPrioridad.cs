using System;
using System.Runtime.Remoting.Messaging;

namespace proyecto_8
{
    public class ColaPrioridad
    {
        private Nodo frente;
        private class Nodo
        {
            public cliente Datos;
            public Nodo Siguiente;
            public Nodo(cliente cliente)
            {
                Datos = cliente;
            }
        }
        public void insertar(cliente cliente )
        {
            Nodo nuevo = new Nodo(cliente);
            if (frente ==null || cliente.Prioridad < frente.Datos.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
                return;
            }
            Nodo actual = frente;
            while (actual.Siguiente!=null&& actual.Siguiente.Datos.Prioridad<= cliente.Prioridad)
            {
                actual = actual.Siguiente;
            }
            nuevo.Siguiente = actual.Siguiente;
            actual.Siguiente = nuevo;
        }
        public cliente Atender()
        {
            if (frente == null)
                return null;
            cliente cliente = frente.Datos;
            frente = frente.Siguiente;
            return cliente;
        }
        public void Mostrar()
        {
            Nodo actual = frente;
            while(actual!=null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }
    }
}
