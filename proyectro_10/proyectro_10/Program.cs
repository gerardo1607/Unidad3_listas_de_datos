using System;
using System.Collections.Generic;
using System.Linq;
using TiendaOnline;

namespace TiendaOnline
{
    class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Existencias { get; set; }
        public string Categoria { get; set; }
    }

    class Pedido
    {
        public int Numero { get; set; }
        public string Cliente { get; set; }
        public double Total { get; set; }
        public string Estado { get; set; }
    }

    class Program
    {
        static LinkedList<Producto> inventario = new LinkedList<Producto>();
        static Queue<Pedido> pedidos = new Queue<Pedido>();
        static Stack<string> historial = new Stack<string>();

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== SISTEMA DE GESTION DE PEDIDOS ===");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Buscar producto");
                Console.WriteLine("3. Eliminar producto");
                Console.WriteLine("4. Registrar pedido");
                Console.WriteLine("5. Procesar pedido");
                Console.WriteLine("6. Mostrar pedidos pendientes");
                Console.WriteLine("7. Mostrar inventario");
                Console.WriteLine("8. Mostrar historial");
                Console.WriteLine("9. Salir");

                Console.Write("\nOpcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarProducto();
                        break;

                    case 2:
                        BuscarProducto();
                        break;

                    case 3:
                        EliminarProducto();
                        break;

                    case 4:
                        RegistrarPedido();
                        break;

                    case 5:
                        ProcesarPedido();
                        break;

                    case 6:
                        MostrarPedidos();
                        break;

                    case 7:
                        MostrarInventario();
                        break;

                    case 8:
                        MostrarHistorial();
                        break;
                }

            } while (opcion != 9);
        }

        static void RegistrarProducto()
        {
            Producto p = new Producto();

            Console.Write("ID: ");
            p.Id = int.Parse(Console.ReadLine());

            Console.Write("Nombre: ");
            p.Nombre = Console.ReadLine();

            Console.Write("Precio: ");
            p.Precio = double.Parse(Console.ReadLine());

            Console.Write("Existencias: ");
            p.Existencias = int.Parse(Console.ReadLine());

            Console.Write("Categoria: ");
            p.Categoria = Console.ReadLine();

            inventario.AddLast(p);

            historial.Push("Producto registrado: " + p.Nombre);

            Console.WriteLine("Producto agregado.");
            Console.ReadKey();
        }

        static void BuscarProducto()
        {
            Console.Write("ID a buscar: ");
            int id = int.Parse(Console.ReadLine());

            var producto = inventario.FirstOrDefault(x => x.Id == id);

            if (producto != null)
            {
                Console.WriteLine("\nProducto encontrado");
                Console.WriteLine("Nombre: " + producto.Nombre);
                Console.WriteLine("Precio: " + producto.Precio);
                Console.WriteLine("Stock: " + producto.Existencias);
            }
            else
            {
                Console.WriteLine("No existe.");
            }

            Console.ReadKey();
        }

        static void EliminarProducto()
        {
            Console.Write("ID a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            var producto = inventario.FirstOrDefault(x => x.Id == id);

            if (producto != null)
            {
                inventario.Remove(producto);
                historial.Push("Producto eliminado: " + producto.Nombre);
                Console.WriteLine("Eliminado.");
            }
            else
            {
                Console.WriteLine("No encontrado.");
            }

            Console.ReadKey();
        }

        static void RegistrarPedido()
        {
            Pedido p = new Pedido();

            Console.Write("Numero de pedido: ");
            p.Numero = int.Parse(Console.ReadLine());

            Console.Write("Cliente: ");
            p.Cliente = Console.ReadLine();

            Console.Write("Total: ");
            p.Total = double.Parse(Console.ReadLine());

            p.Estado = "Pendiente";

            pedidos.Enqueue(p);

            historial.Push("Pedido registrado: " + p.Numero);

            Console.WriteLine("Pedido agregado.");
            Console.ReadKey();
        }

        static void ProcesarPedido()
        {
            if (pedidos.Count > 0)
            {
                Pedido p = pedidos.Dequeue();

                p.Estado = "Procesado";

                historial.Push("Pedido procesado: " + p.Numero);

                Console.WriteLine("Pedido procesado.");
            }
            else
            {
                Console.WriteLine("No hay pedidos.");
            }

            Console.ReadKey();
        }

        static void MostrarPedidos()
        {
            Console.WriteLine("\nPEDIDOS PENDIENTES\n");

            foreach (Pedido p in pedidos)
            {
                Console.WriteLine($"Pedido: {p.Numero} | Cliente: {p.Cliente} | Total: {p.Total}");
            }

            Console.ReadKey();
        }

        static void MostrarInventario()
        {
            Console.WriteLine("\nINVENTARIO\n");

            foreach (Producto p in inventario)
            {
                Console.WriteLine(
                    $"ID:{p.Id} | {p.Nombre} | ${p.Precio} | Stock:{p.Existencias}");
            }

            Console.ReadKey();
        }

        static void MostrarHistorial()
        {
            Console.WriteLine("\nHISTORIAL\n");

            foreach (string accion in historial)
            {
                Console.WriteLine(accion);
            }

            Console.ReadKey();
        }
    }
}