using Newtonsoft.Json;
using OfficeOpenXml;
using PerspectivaCliente.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PerspectivaCliente
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Categoria();
            }

        }

        public void Categoria()
        {
            IndexController indexController = new IndexController();
            string opciones = indexController.Categorias();
            cboEjemplo.Items.Clear();
            //cboEjemplo.Items.Add(new ListItem("Todas las categorías", "0"));
            if (!string.IsNullOrEmpty(opciones))
            {
                string[] opcionesArray = opciones.Split(';');
                foreach (string opcion in opcionesArray)
                {
                    string[] valores = opcion.Split(',');
                    if (valores.Length == 2)
                    {

                        cboEjemplo.Items.Add(new ListItem(valores[1], valores[0]));
                    }
                }
            }
        }


        [WebMethod]
        public static string ObtenerResultado(string cadena)
        {
            int[] numeros = ConvertirStringAIntArray(cadena);
            string resultado = "";
            IndexController indexController = new IndexController();
            var result = indexController.PreferenciasPorCategoria(numeros);

            if (!string.IsNullOrEmpty(result))
            {

                Index index = (Index)HttpContext.Current.Handler;
                List<Dictionary<string, string>> opciones = index.SelectPreferencias(result);
                foreach (Dictionary<string, string> opcion in opciones)
                {
                    resultado += "<option value=\""+ opcion["Value"] + "\">"+opcion["Text"]+ "</option>";
                }
            }
            else
            {
                resultado = "No se encontraron preferencias";
            }
            return resultado;
        }

        protected List<Dictionary<string, string>> SelectPreferencias(string opciones)
        {
            return opciones.Split(';')
            .Select(opcionString => opcionString.Split(','))
            .Where(opcionSeparada => opcionSeparada.Length == 2)
            .Select(opcionSeparada => new Dictionary<string, string>
            {
                { "Value", opcionSeparada[0] },
                { "Text", opcionSeparada[1] }
            })
            .ToList();
        }

        [WebMethod]
        public static string GenerarTabla(string[] Preferencias, string RangoEdades, string PosEcononomicas)
        {
            IndexController indexController = new IndexController();
            return indexController.GetTabla(Preferencias, RangoEdades, PosEcononomicas);
        }

        protected static int[] ConvertirStringAIntArray(string input)
        {
            string[] numeros = input.Split(',');

            int[] arregloNumeros = new int[numeros.Length];

            for (int i = 0; i < numeros.Length; i++)
            {
                if (int.TryParse(numeros[i], out int numero))
                {
                    arregloNumeros[i] = numero;
                }
                else
                {
                    arregloNumeros[i] = 0;
                }
            }

            return arregloNumeros;
        }

        [WebMethod]
        public static string ObtenerDatos(string datos)
        {
            // Remover los corchetes exteriores
            datos = datos.Trim('[', ']');

            // Dividir el string en sublistas
            string[] sublists = datos.Split(new string[] { "],[" }, StringSplitOptions.None);

            // Crear una lista de listas de strings para almacenar los elementos
            List<List<string>> resultList = new List<List<string>>();

            foreach (string sublist in sublists)
            {
                // Dividir cada sublista en elementos individuales
                string[] elements = sublist.Split(new char[] { ',' });

                // Crear una lista de strings para almacenar los elementos de la sublista
                List<string> subList = new List<string>();

                foreach (string element in elements)
                {
                    // Remover las comillas dobles alrededor de cada elemento
                    string trimmedElement = element.Trim('\"');

                    // Agregar el elemento a la sublista
                    subList.Add(trimmedElement);
                }

                // Agregar la sublista a la lista principal
                resultList.Add(subList);
            }

            IndexController indexController = new IndexController();
            return indexController.GenerarExcel(resultList);
        }

        [WebMethod]
        public static string GenerarPDF(List<string> urls)
        {
            IndexController indexController = new IndexController();
            return indexController.GenerarPDF(urls);
        }

    }
}