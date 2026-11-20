using System.Text;

namespace MesaPartes
{
    internal class Program
    {
        public static string[] Tipos = { "Solicitud", "Carta", "Oficio", "Reclamo" };
        public static string[] Estados = { "Registrado", "En proceso", "Atendido", "Rechazado" };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int cantidad = 0;
            Expediente[] expedientes = new Expediente[5];

            int op = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("=== SISTEMA MESA DE PARTES - EF ===");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Listar expedientes");
                Console.WriteLine("0. Salir");
                Console.WriteLine("3. Buscar expediente por código");
                Console.WriteLine("4. Ordenar expedientes (burbuja)");
                Console.WriteLine("5. Modificar estado de un expediente");
                Console.WriteLine("6. Eliminar expediente");
                do
                {
                } while (!LeerEntero(0, out op, "Seleccione una opción, valor mínimo:"));

                switch (op)
                {
                    case 0:
                        Console.WriteLine("Saliendo ....");
                        break;
                    case 1:
                        clsCatalogo.RegistrarExpediente(ref expedientes, ref cantidad);
                        break;
                    case 2:
                        clsCatalogo.VisualizarCatalogo(expedientes, cantidad);
                        break;
                    case 3:
                        clsCatalogo.BuscarCodigo(expedientes, cantidad);
                        break;
                    case 4:
                        clsCatalogo.OrdenarPorCodigo(expedientes, cantidad);
                        break;
                    case 5:
                        clsCatalogo.ModificarExpediente(expedientes, cantidad);
                        break;
                    case 6:
                        clsCatalogo.EliminarExpediente(expedientes, ref cantidad);
                        break;
                    default:
                        Console.WriteLine("Ingrese una opción valida ....");
                        break;
                }
            } while (op != 0);
        }

        public static bool LeerEntero(int minimo, out int valor, string mensaje)
        {
            Console.WriteLine($"{mensaje} {minimo}");
            if (!int.TryParse(Console.ReadLine(), out valor) || valor < minimo)
            {
                Console.WriteLine("Dato no válido.");
                return false;
            }
            return true;
        }

        public static string Leer(string mensaje)
        {
            Console.WriteLine(mensaje);
            string texto = Console.ReadLine();
            if (texto == null)
            {
                texto = "";
            }
            return texto.Trim();
        }
        public static bool Confirmar(string mensaje)
        {
            string respuesta = Leer(mensaje + " (S/N)");
            return respuesta.ToUpper() == "S";
        }
        public static string LeerCodigo()
        {
            string codigo = Leer("Ingrese el código único (formato EXP-001)").ToUpper();

            if (codigo.Length == 0)
            {
                Console.WriteLine("Error: El código no puede estar vacío.");
                return null;
            }
            if (!CodigoValido(codigo))
            {
                Console.WriteLine("Error: El código debe tener el formato EXP-001.");
                return null;
            }
            return codigo;
        }
        public static string LeerCodigoBuscar(string mensaje)
        {
            string codigo = Leer(mensaje).ToUpper();

            if (codigo.Length == 0)
            {
                Console.WriteLine("Error: El código no puede estar vacío.");
                return null;
            }
            return codigo;
        }
        public static string LeerTexto(string mensaje, string campo)
        {
            string texto = Leer(mensaje);

            if (texto.Length == 0)
            {
                Console.WriteLine($"Error: {campo} es obligatorio.");
                return null;
            }
            return texto.Replace(";", ",");
        }

        public static string LeerSolicitante()
        {
            return LeerTexto("Ingrese el solicitante ficticio/anonimizado", "El solicitante");
        }

        public static string LeerAsunto()
        {
            return LeerTexto("Ingrese el asunto", "El asunto");
        }

        public static string LeerTipo()
        {
            string texto = Leer("Ingrese el tipo de documento (" + string.Join(", ", Tipos) + ")");

            if (texto.Length == 0)
            {
                Console.WriteLine("Error: El tipo de documento es obligatorio.");
                return null;
            }

            int pos = IndiceDe(Tipos, texto);
            if (pos == -1)
            {
                Console.WriteLine("Error: Fecha no válida. Use el formato DD/MM/AAAA (día 01-31, mes 01-12).");
                return null;
            }
            return Tipos[pos];
        }

        public static string LeerFecha()
        {
            string texto = Leer("Ingrese la fecha de recepción (DD/MM/AAAA)");

            if (texto.Length == 0)
            {
                Console.WriteLine("Error: La fecha de recepción es obligatoria.");
                return null;
            }
            if (!FechaValida(texto))
            {
                Console.WriteLine("Error: Fecha no válida. Use DD/MM/AAAA con una fecha real.");
                return null;
            }
            return texto;
        }

        public static string LeerEstado()
        {
            string texto = Leer("Ingrese el estado (" + string.Join(", ", Estados) + ")");

            if (texto.Length == 0)
            {
                Console.WriteLine("Error: El estado es obligatorio.");
                return null;
            }

            int pos = IndiceDe(Estados, texto);
            if (pos == -1)
            {
                Console.WriteLine("Error: Estado no válido. Opciones: " + string.Join(", ", Estados));
                return null;
            }
            return Estados[pos];
        }

        public static int IndiceDe(string[] lista, string valor)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (string.Compare(lista[i], valor, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool CodigoValido(string codigo)
        {
            if (codigo.Length != 7)
            {
                return false;
            }
            if (codigo.IndexOf("EXP-", StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false;
            }
            for (int i = 4; i < 7; i++)
            {
                if (codigo[i] < '0' || codigo[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool FechaValida(string fecha)
        {
            string[] partes = fecha.Split("/");
            if (partes.Length != 3)
            {
                return false;
            }
            if (partes[0].Length != 2 || partes[1].Length != 2 || partes[2].Length != 4)
            {
                return false;
            }

            int dia = 0;
            int mes = 0;
            int año = 0;
            if (!int.TryParse(partes[0], out dia) || !int.TryParse(partes[1], out mes)
                || !int.TryParse(partes[2], out año))
            {
                return false;
            }

            if (mes < 1 || mes > 12)
            {
                return false;
            }
            if (dia < 1 || dia > 31)
            {
                return false;
            }
            return true;
        }
    }
}