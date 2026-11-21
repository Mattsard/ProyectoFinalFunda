using System;
using System.Collections.Generic;
using System.Text;

namespace MesaPartes
{
    public class clsArchivos
    {
        public static void Guardar(string ruta, Expediente[] expedientes, int cantidad)
        {
            string carpeta = Path.GetDirectoryName(ruta);
            Directory.CreateDirectory(carpeta);
            using (StreamWriter archivo = new StreamWriter(ruta))
            {
                for (int i = 0; i < cantidad; i++)
                {
                    archivo.WriteLine(expedientes[i].Codigo
                        + ";" + expedientes[i].Solicitante
                        + ";" + expedientes[i].TipoDocumento
                        + ";" + expedientes[i].Asunto
                        + ";" + expedientes[i].Fecha
                        + ";" + expedientes[i].Estado);
                }
            }
        }

        public static void LeerArchivo(string ruta, ref Expediente[] expedientes, ref int cantidad)
        {
            if (!File.Exists(ruta))
                return;

            Array.Clear(expedientes, 0, expedientes.Length);
            cantidad = 0;

            using (StreamReader archivo = new StreamReader(ruta))
            {
                while (!archivo.EndOfStream)
                {
                    string linea = archivo.ReadLine();
                    if (linea.Trim().Length == 0)
                    {
                        continue;
                    }

                    string[] datos = linea.Split(";");
                    if (datos.Length != 6)
                    {
                        continue;
                    }

                    string codigo = datos[0].Trim().ToUpper();
                    string solicitante = datos[1].Trim();
                    int posTipo = Program.IndiceDe(Program.Tipos, datos[2].Trim());
                    string asunto = datos[3].Trim();
                    string fecha = datos[4].Trim();
                    int posEstado = Program.IndiceDe(Program.Estados, datos[5].Trim());

                    if (!Program.CodigoValido(codigo) || solicitante.Length == 0 || asunto.Length == 0
                        || posTipo == -1 || posEstado == -1 || !Program.FechaValida(fecha))
                    {
                        continue;
                    }

                    if (clsCatalogo.BuscarIndice(expedientes, cantidad, codigo) != -1)
                    {
                        continue;
                    }

                    Expediente e = new Expediente(codigo, solicitante, Program.Tipos[posTipo],
                                                  asunto, fecha, Program.Estados[posEstado]);
                    clsCatalogo.AgregarExpediente(ref expedientes, ref cantidad, e);
                }
            }
        }
    }
}