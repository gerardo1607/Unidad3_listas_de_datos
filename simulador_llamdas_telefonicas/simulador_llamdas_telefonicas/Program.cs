using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorPilaLlamadas
{
    class Llamada
    {
        public int Id { get; set; }
        public string Funcion { get; set; }
        public string Estado { get; set; } // Pendiente / Atendida
        public DateTime Hora { get; set; }

        public override string ToString()
        {
            return $"{Funcion}  [Id: {Id} | {Estado} | {Hora:HH:mm:ss}]";
        }
    }

    class PilaLlamadas
    {
        private Stack<Llamada> pila = new Stack<Llamada>();
        private int contadorId = 1;

        public int Cantidad => pila.Count;

        
        public void Registrar(string funcion)
        {
            var llamada = new Llamada
            {
                Id = contadorId++,
                Funcion = funcion,
                Estado = "Pendiente",
                Hora = DateTime.Now
            };
            pila.Push(llamada);
            Console.WriteLine($"Llamada registrada: {llamada.Funcion}");
        }

        public void Atender()
        {
            if (pila.Count == 0) { Console.WriteLine("No hay llamadas en la pila."); return; }
            var tope = pila.Peek();
            if (tope.Estado == "Atendida")
            {
                Console.WriteLine($"La llamada {tope.Funcion} ya fue atendida.");
                return;
            }
            tope.Estado = "Atendida";
            Console.WriteLine($"Atendiendo: {tope.Funcion}");
        }

        public void Finalizar()
        {
            if (pila.Count == 0) { Console.WriteLine("No hay llamadas para finalizar."); return; }
            var finalizada = pila.Pop();
            Console.WriteLine("Al regresar:");
            Console.WriteLine($"Regresando de: {finalizada.Funcion}");
            if (pila.Count > 0)
                Console.WriteLine($"Ahora se continúa en: {pila.Peek().Funcion}");
            else
                Console.WriteLine("La pila quedó vacía.");
        }

        public void Mostrar()
        {
            if (pila.Count == 0) { Console.WriteLine("No hay llamadas activas."); return; }
            Console.WriteLine("\nPILA DE LLAMADAS");
            int i = 1;
            foreach (var l in pila.Reverse())
            {
                Console.WriteLine($"{i}. {l}");
                i++;
            }
            Console.WriteLine("(La última de la lista es el tope de la pila)");
        }

        public void RecuperarUltima()
        {
            if (pila.Count == 0) { Console.WriteLine("No hay llamadas en la pila."); return; }
            Console.WriteLine($"Última llamada: {pila.Peek()}");
        }

        public void Buscar(string criterio)
        {
            var encontradas = pila.Where(l =>
                l.Funcion.Equals(criterio, StringComparison.OrdinalIgnoreCase) ||
                l.Id.ToString() == criterio).ToList();

            if (encontradas.Count == 0)
            {
                Console.WriteLine("Llamada no encontrada.");
                return;
            }
            foreach (var l in encontradas)
                Console.WriteLine($"Encontrada: {l}");
        }

        public void Eliminar(string criterio)
        {
            var aux = new Stack<Llamada>();
            bool eliminada = false;

            while (pila.Count > 0)
            {
                var actual = pila.Pop();
                if (!eliminada &&
                    (actual.Funcion.Equals(criterio, StringComparison.OrdinalIgnoreCase) ||
                     actual.Id.ToString() == criterio))
                {
                    eliminada = true;
                    Console.WriteLine($"Llamada eliminada: {actual.Funcion}");
                }
                else
                {
                    aux.Push(actual);
                }
            }

            while (aux.Count > 0)
                pila.Push(aux.Pop());

            if (!eliminada)
                Console.WriteLine("Llamada no encontrada.");
        }
    }

    class Program
    {
        static void Main()
        {
            var pila = new PilaLlamadas();

            pila.Registrar("Main()");
            pila.Registrar("MenuPrincipal()");

            int opcion;
            do
            {
                Console.WriteLine("\n===== SIMULADOR DE PILA DE LLAMADAS =====");
                Console.WriteLine("1. Registrar llamada");
                Console.WriteLine("2. Atender llamada");
                Console.WriteLine("3. Finalizar llamada");
                Console.WriteLine("4. Mostrar llamadas activas");
                Console.WriteLine("5. Recuperar última llamada");
                Console.WriteLine("6. Buscar llamada");
                Console.WriteLine("7. Eliminar llamada");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    opcion = -1;
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Nombre de la función (ej. RegistrarCliente()): ");
                        string nombre = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombre))
                            Console.WriteLine("El nombre no puede estar vacío.");
                        else
                            pila.Registrar(nombre.Trim());
                        break;
                    case 2:
                        pila.Atender();
                        break;
                    case 3:
                        pila.Finalizar();
                        break;
                    case 4:
                        pila.Mostrar();
                        break;
                    case 5:
                        pila.RecuperarUltima();
                        break;
                    case 6:
                        Console.Write("Nombre o Id de la llamada: ");
                        pila.Buscar(Console.ReadLine().Trim());
                        break;
                    case 7:
                        Console.Write("Nombre o Id de la llamada a eliminar: ");
                        pila.Eliminar(Console.ReadLine().Trim());
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            } while (opcion != 0);
        }
    }
}