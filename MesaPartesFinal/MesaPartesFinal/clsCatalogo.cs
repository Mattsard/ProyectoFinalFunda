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
    }
}