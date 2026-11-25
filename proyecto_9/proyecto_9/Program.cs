using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaProcesos
{
    class Proceso
    {
        public int PID { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Estado { get; set; }
        public int TiempoEjecucion { get; set; }
    }

    class Program
    {
        static LinkedList<Proceso> listaProcesos =
            new LinkedList<Proceso>();

        static Queue<Proceso> colaProcesos =
            new Queue<Proceso>();

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("===== SISTEMA DE CONTROL DE PROCESOS =====");
                Console.WriteLine("1. Crear proceso");
                Console.WriteLine("2. Eliminar proceso");
                Console.WriteLine("3. Ejecutar proceso");
                Console.WriteLine("4. Bloquear proceso");
                Console.WriteLine("5. Desbloquear proceso");
                Console.WriteLine("6. Mostrar procesos");
                Console.WriteLine("7. Buscar proceso");
                Console.WriteLine("8. Mostrar cola de procesos");
                Console.WriteLine("9. Salir");

                Console.Write("\nSeleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        CrearProceso();
                        break;

                    case 2:
                        EliminarProceso();
                        break;

                    case 3:
                        EjecutarProceso();
                        break;

                    case 4:
                        BloquearProceso();
                        break;

                    case 5:
                        DesbloquearProceso();
                        break;

                    case 6:
                        MostrarProcesos();
                        break;

                    case 7:
                        BuscarProceso();
                        break;

                    case 8:
                        MostrarCola();
                        break;
                }

            } while (opcion != 9);
        }

        static void CrearProceso()
        {
            Proceso p = new Proceso();

            Console.Write("PID: ");
            p.PID = int.Parse(Console.ReadLine());

            Console.Write("Nombre: ");
            p.Nombre = Console.ReadLine();

            Console.Write("Prioridad (1-10): ");
            p.Prioridad = int.Parse(Console.ReadLine());

            Console.Write("Tiempo de ejecución: ");
            p.TiempoEjecucion = int.Parse(Console.ReadLine());

            p.Estado = "NUEVO";

            listaProcesos.AddLast(p);
            colaProcesos.Enqueue(p);

            Console.WriteLine("Proceso creado correctamente.");
            Console.ReadKey();
        }

        static void EliminarProceso()
        {
            Console.Write("PID a eliminar: ");
            int pid = int.Parse(Console.ReadLine());

            var proceso = listaProcesos.FirstOrDefault(x => x.PID == pid);

            if (proceso != null)
            {
                listaProcesos.Remove(proceso);
                Console.WriteLine("Proceso eliminado.");
            }
            else
            {
                Console.WriteLine("Proceso no encontrado.");
            }

            Console.ReadKey();
        }

        static void EjecutarProceso()
        {
            if (colaProcesos.Count > 0)
            {
                Proceso p = colaProcesos.Dequeue();

                p.Estado = "EJECUTANDO";

                Console.WriteLine($"Ejecutando: {p.Nombre}");

                p.Estado = "TERMINADO";

                Console.WriteLine("Proceso terminado.");
            }
            else
            {
                Console.WriteLine("No hay procesos en cola.");
            }

            Console.ReadKey();
        }

        static void BloquearProceso()
        {
            Console.Write("PID: ");
            int pid = int.Parse(Console.ReadLine());

            var proceso = listaProcesos.FirstOrDefault(x => x.PID == pid);

            if (proceso != null)
            {
                proceso.Estado = "BLOQUEADO";
                Console.WriteLine("Proceso bloqueado.");
            }
            else
            {
                Console.WriteLine("No encontrado.");
            }

            Console.ReadKey();
        }

        static void DesbloquearProceso()
        {
            Console.Write("PID: ");
            int pid = int.Parse(Console.ReadLine());

            var proceso = listaProcesos.FirstOrDefault(x => x.PID == pid);

            if (proceso != null)
            {
                proceso.Estado = "LISTO";
                colaProcesos.Enqueue(proceso);

                Console.WriteLine("Proceso desbloqueado.");
            }
            else
            {
                Console.WriteLine("No encontrado.");
            }

            Console.ReadKey();
        }

        static void MostrarProcesos()
        {
            Console.WriteLine("\nLISTA DE PROCESOS\n");

            foreach (var p in listaProcesos)
            {
                Console.WriteLine(
                    $"PID:{p.PID} | {p.Nombre} | Prioridad:{p.Prioridad} | Estado:{p.Estado}");
            }

            Console.ReadKey();
        }

        static void BuscarProceso()
        {
            Console.Write("PID a buscar: ");
            int pid = int.Parse(Console.ReadLine());

            var proceso = listaProcesos.FirstOrDefault(x => x.PID == pid);

            if (proceso != null)
            {
                Console.WriteLine($"PID: {proceso.PID}");
                Console.WriteLine($"Nombre: {proceso.Nombre}");
                Console.WriteLine($"Prioridad: {proceso.Prioridad}");
                Console.WriteLine($"Estado: {proceso.Estado}");
                Console.WriteLine($"Tiempo: {proceso.TiempoEjecucion}");
            }
            else
            {
                Console.WriteLine("Proceso no encontrado.");
            }

            Console.ReadKey();
        }

        static void MostrarCola()
        {
            Console.WriteLine("\nCOLA DE PROCESOS\n");

            foreach (var p in colaProcesos)
            {
                Console.WriteLine($"PID:{p.PID} - {p.Nombre}");
            }

            Console.ReadKey();
        }
    }
}