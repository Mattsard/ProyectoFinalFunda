using System;
using System.Collections.Generic;
using System.Text;

namespace MesaPartes
{
    public class clsCatalogo
    {
        public static void AgrandarArreglo(ref Expediente[] expedientes, int cantidad)
        {
            if (cantidad == expedientes.Length)
            {
                int incremento = (int)Math.Ceiling(expedientes.Length * 0.5);
                Array.Resize(ref expedientes, cantidad + incremento);
            }
        }

        public static int BuscarIndice(Expediente[] expedientes, int cantidad, string codigo)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (expedientes[i].Codigo == codigo)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void AgregarExpediente(ref Expediente[] expedientes, ref int cantidad, Expediente nuevo)
        {
            AgrandarArreglo(ref expedientes, cantidad);
            expedientes[cantidad] = nuevo;
            cantidad++;
        }

        public static void RegistrarExpediente(ref Expediente[] expedientes, ref int cantidad)
        {
            Console.WriteLine("--- REGISTRAR NUEVO EXPEDIENTE ---");

            string codigo = Program.LeerCodigo();
            if (codigo == null) return;

            if (BuscarIndice(expedientes, cantidad, codigo) != -1)
            {
                Console.WriteLine($"Error: Ya existe un expediente con el código '{codigo}'.");
                return;
            }

            string solicitante = Program.LeerSolicitante();
            if (solicitante == null) return;

            string tipo = Program.LeerTipo();
            if (tipo == null) return;

            string asunto = Program.LeerAsunto();
            if (asunto == null) return;

            string fecha = Program.LeerFecha();
            if (fecha == null) return;

            string estado = Program.LeerEstado();
            if (estado == null) return;

            Expediente nuevo = new Expediente(codigo, solicitante, tipo, asunto, fecha, estado);
            AgregarExpediente(ref expedientes, ref cantidad, nuevo);
            Console.WriteLine("¡Expediente registrado con éxito!");
        }

        public static void VisualizarCatalogo(Expediente[] expedientes, int cantidad)
        {
            Console.WriteLine("--- LISTADO DE EXPEDIENTES ---");
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados en el sistema.");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"{expedientes[i].ToString()}");
            }
        }
        public static int BuscarCodigo(Expediente[] expedientes, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
                return -1;
            }

            string codigo = Program.LeerCodigoBuscar("Ingrese el código del expediente");
            if (codigo == null) return -1;

            int indice = BuscarIndice(expedientes, cantidad, codigo);
            if (indice == -1)
            {
                Console.WriteLine($"No se encontró ningún expediente con el código '{codigo}'.");
                return -1;
            }

            Console.WriteLine("¡Expediente encontrado!");
            Console.WriteLine($"{expedientes[indice].ToString()}");
            return indice;
        }
        public static int BuscarRecursivo(Expediente[] expedientes, int cantidad, string codigo, int indice)
        {
            if (indice >= cantidad)
            {
                return -1;
            }

            if (expedientes[indice].Codigo == codigo)
            {
                return indice;
            }

            return BuscarRecursivo(expedientes, cantidad, codigo, indice + 1);
        }

        public static void BuscarCodigoRecursivo(Expediente[] expedientes, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
                return;
            }

            string codigo = Program.LeerCodigoBuscar("Ingrese el código del expediente");
            if (codigo == null) return;

            int indice = BuscarRecursivo(expedientes, cantidad, codigo, 0);
            if (indice == -1)
            {
                Console.WriteLine($"No se encontró ningún expediente con el código '{codigo}'.");
                return;
            }

            Console.WriteLine($"¡Expediente encontrado en la posición {indice + 1}!");
            Console.WriteLine($"{expedientes[indice].ToString()}");
        }

        public static void ModificarExpediente(Expediente[] expedientes, int cantidad)
        {
            Console.WriteLine("--- MODIFICAR ESTADO DE UN EXPEDIENTE ---");

            int indice = BuscarCodigo(expedientes, cantidad);
            if (indice == -1) return;

            string nuevoEstado = Program.LeerEstado();
            if (nuevoEstado == null) return;

            if (!Program.Confirmar("¿Confirma la modificación?"))
            {
                Console.WriteLine("Modificación cancelada.");
                return;
            }

            expedientes[indice].CambiarEstado(nuevoEstado);
            Console.WriteLine("¡Estado modificado con éxito!");
        }

        public static void EliminarExpediente(Expediente[] expedientes, ref int cantidad)
        {
            Console.WriteLine("--- ELIMINAR EXPEDIENTE ---");

            int indice = BuscarCodigo(expedientes, cantidad);
            if (indice == -1) return;

            if (!Program.Confirmar("¿Está seguro de eliminar este expediente?"))
            {
                Console.WriteLine("Eliminación cancelada.");
                return;
            }

            for (int k = indice; k < cantidad - 1; k++)
            {
                expedientes[k] = expedientes[k + 1];
            }
            expedientes[cantidad - 1] = null;
            cantidad--;

            Console.WriteLine("¡Expediente eliminado con éxito!");
        }
        public static void OrdenarPorCodigo(Expediente[] expedientes, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes para ordenar.");
                return;
            }

            for (int i = 0; i < cantidad - 1; i++)
            {
                for (int j = 0; j < cantidad - i - 1; j++)
                {
                    int Comparacion = string.Compare(expedientes[j].Codigo, expedientes[j + 1].Codigo, StringComparison.OrdinalIgnoreCase);
                    if (Comparacion > 0)
                    {
                        Expediente Aux = expedientes[j];
                        expedientes[j] = expedientes[j + 1];
                        expedientes[j + 1] = Aux;
                    }
                }
            }

            Console.WriteLine("¡Expedientes ordenados por código con éxito!");
        }
        public static void MostrarResumen(Expediente[] expedientes, int cantidad)
        {
            int[,] matriz = new int[Program.Tipos.Length, Program.Estados.Length];

            for (int i = 0; i < cantidad; i++)
            {
                int fila = Program.IndiceDe(Program.Tipos, expedientes[i].TipoDocumento);
                int columna = Program.IndiceDe(Program.Estados, expedientes[i].Estado);

                if (fila != -1 && columna != -1)
                {
                    matriz[fila, columna]++;
                }
            }

            Console.WriteLine("--- RESUMEN: TIPO DE DOCUMENTO POR ESTADO ---");

            for (int f = 0; f < Program.Tipos.Length; f++)
            {
                Console.Write(Program.Tipos[f] + " -> ");

                for (int c = 0; c < Program.Estados.Length; c++)
                {
                    Console.Write($"{Program.Estados[c]}: {matriz[f, c]}");
                    if (c < Program.Estados.Length - 1)
                    {
                        Console.Write(" | ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}