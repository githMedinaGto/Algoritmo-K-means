using OfficeOpenXml;
using PerspectivaCliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Drawing.Printing;
using System.Drawing;
using System.Net;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;

namespace PerspectivaCliente
{
    public class IndexController
    {
        public string Categorias()
        {
            try
            {
                using (var db = new dbEjemploEntities())
                {
                    var categorias = db.CategoriasInteres.ToList();
                    var opciones = categorias.Select(c => $"{c.ID},{c.Categoria}").ToList();
                    return string.Join(";", opciones);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string PreferenciasPorCategoria(int[] categorias)
        {
            try
            {
                using (var db = new dbEjemploEntities())
                {
                    var categoriasList = categorias.ToList(); // Convertir el arreglo en una lista

                    var preferencias = db.Preferencias
                        .Where(p => categoriasList.Contains((int)p.CategoriaID))
                        .ToList();

                    var opciones = preferencias.Select(p => $"{p.ID},{p.Preferencia}").ToList();
                    return string.Join(";", opciones);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetTabla(string[] Preferencias, string RangoEdades, string PosEcononomicas)
        {
            try
            {
                using (var db = new dbEjemploEntities())
                {

                    var query = from tu in db.Usuarios
                                join tr in db.Region on tu.UbicacionID equals tr.ID
                                join tc in db.Ciudad on tr.CiudadID equals tc.ID
                                join tposEco in db.PosibilidadesEconomicas on tu.PosibilidadesEconomicasID equals tposEco.ID
                                join tp in db.Preferencias on tu.PreferenciasID equals tp.ID
                                join tci in db.CategoriasInteres on tp.CategoriaID equals tci.ID
                                select new
                                {
                                    tu.ID,
                                    tu.Nombre,
                                    tu.Edad,
                                    tu.Genero,
                                    Region = tr.Region1,
                                    Ciudad = tc.Ciudad1,
                                    PosibilidadesEconomicas = tposEco.Rango,
                                    PosibilidadesEconomicasID = tposEco.ID,
                                    Categoria = tci.Categoria,
                                    Preferencia = tp.Preferencia,
                                    PreferenciasID = tp.ID
                                };

                    //Filtrar por Preferencias si se proporciona
                    if (Preferencias != null && Preferencias.Length > 0)
                    {
                        int[] intArray = Array.ConvertAll(Preferencias, int.Parse);
                        query = query.Where(t => intArray.Contains(t.PreferenciasID));
                    }

                    // Filtrar por RangoEdades si se proporciona
                    if (!string.IsNullOrEmpty(RangoEdades) && PosEcononomicas != "0")
                    {
                        var rangoEdadesParts = RangoEdades.Split('-');
                        if (rangoEdadesParts.Length == 2 && int.TryParse(rangoEdadesParts[0], out int minAge) && int.TryParse(rangoEdadesParts[1], out int maxAge))
                        {
                            query = query.Where(t => t.Edad > minAge && t.Edad < maxAge);
                        }
                    }

                    // Filtrar por PosEcononomicas si se proporciona
                    if (!string.IsNullOrEmpty(PosEcononomicas) && PosEcononomicas != "0")
                    {
                        int iPos = int.Parse(PosEcononomicas);
                        query = query.Where(t => t.PosibilidadesEconomicasID == iPos);
                    }

                    var opciones = query.ToList().Select(c => $"{c.ID},{c.Nombre},{c.Edad},{c.Genero}," +
                    $"{c.Region},{c.Ciudad},{c.PosibilidadesEconomicas},{c.Categoria},{c.Preferencia}").ToList();
                    return string.Join(";", opciones);
                }
            }
            catch (Exception ex)
            {
                return ex.Message ;
            }
        }

        public string GenerarExcel(List<List<string>> aDatos)
        {
            string sDatosAg = "";
            try
            {
                // Configurar el contexto de licencia de EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Crear el archivo en la carpeta del proyecto
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelFiles");
                Directory.CreateDirectory(folderPath); // Asegurarse de que la carpeta exista

                // Nombre del archivo y ruta completa
                string fileName = "datos.xlsx";
                string filePath = Path.Combine(folderPath, fileName);

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Escribir los datos en el archivo Excel
                    for (int i = 0; i < aDatos.Count; i++)
                    {
                        List<string> row = aDatos[i];
                        for (int j = 0; j < row.Count; j++)
                        {
                            // Validar y transformar los valores numéricos a tipo entero
                            int intValue;
                            bool isNumeric = int.TryParse(row[j], out intValue);
                            if (isNumeric)
                            {
                                worksheet.Cells[i + 1, j + 1].Value = intValue;
                            }
                            else
                            {
                                worksheet.Cells[i + 1, j + 1].Value = row[j];
                            }
                        }
                    }

                    // Guardar el archivo Excel
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    sDatosAg = EnviarExcel(filePath);
                }
                return sDatosAg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected string EnviarExcel(string filePath)
        {
            try
            {
                string apiUrl = "http://127.0.0.1:8000/ObtenerExcel";

                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Leer el contenido del archivo como bytes
                        byte[] fileBytes = File.ReadAllBytes(filePath);

                        // Agregar el archivo al formulario
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        form.Add(fileContent, "excel", "file.xlsx");

                        // Realizar la solicitud POST a la URL protegida
                        var response = httpClient.PostAsync(apiUrl, form).GetAwaiter().GetResult();

                        // Manejar la respuesta del servidor
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            return getDatos(responseContent);
                        }
                        else
                        {
                            return getDatos($"<h1 class='text-center'>Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string getDatos(string respuesta)
        {
            
            try
            {
                if (respuesta.Contains("Error en la solicitud"))
                {
                    // Mostrar el mensaje de error en la página web
                    return respuesta;
                }
                else
                {
                    try
                    {
                        // Convertir la respuesta JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(respuesta);

                        // Obtener el objeto 'resultado'
                        JObject resultado = (JObject)jsonObject["resultado"];

                        // Obtener los archivos que tienen la terminación ".csv"
                        var archivosCsv = resultado.Properties()
                            .Where(kv => kv.Value.ToString().Contains(".csv"))
                            .ToDictionary(kv => kv.Name, kv => kv.Value["Archivo CSV"].ToString());

                        // Generar el código HTML para el panel de archivos
                        string panelHtml = "<div class=\"container\">";
                        panelHtml += "<div class=\"col col-md-12 text-center\"><h3>Archivos agrupados</h3></div>";
                        foreach (var archivo in archivosCsv)
                        {
                            panelHtml += "<div class=\"row mb-3\">";
                            panelHtml += "<div class=\"col-1\"><i class=\"fa fa-file-excel-o\"></i></div>";
                            panelHtml += $"<div class=\"col-9\">{archivo.Key}</div>";
                            panelHtml += $"<div class=\"col-2\"><button type=\"button\" class=\"btn btn-success\" href=\"{archivo.Value}\" target=\"_blank\" >Descargar</button></div>";
                            panelHtml += "</div>";
                        }
                        panelHtml += "</div>";

                        // Lista de claves de interés
                        var clavesInteres = new List<string>
                        {
                            "['Edad', 'Preferencia']",
                            "['Edad', 'Categoria', 'Preferencia']",
                            "['Genero', 'Categoria']",
                            "['Genero', 'Categoria', 'Preferencia']",
                            "['Posibilidades Economicas', 'Genero']",
                            "['Posibilidades Economicas', 'Genero', 'Categoria']",
                            "['Posibilidades Economicas', 'Genero', 'Preferencia']"
                        };

                        // Diccionario para almacenar las URLs de las imágenes por clave
                        Dictionary<string, List<string>> urlsPorClave = new Dictionary<string, List<string>>();

                        // Recorrer las claves y obtener las URLs de las imágenes
                        foreach (var clave in clavesInteres)
                        {
                            var imagenesDeClave = jsonObject["resultado"][clave];
                            List<string> imagenesUrls = new List<string>();

                            // Recorrer los tipos de imagen y obtener las URLs de las imágenes
                            foreach (var tipoImagen in imagenesDeClave.Children())
                            {
                                // Obtener el nombre del tipo de imagen
                                var nombreTipoImagen = ((JProperty)tipoImagen).Name;

                                // Verificar que no sea un archivo CSV (omitirlo)
                                if (!nombreTipoImagen.Contains("Archivo CSV"))
                                {
                                    string url = tipoImagen.First.ToString();
                                    imagenesUrls.Add(url);
                                }
                            }

                            // Almacenar las URLs en el diccionario
                            urlsPorClave.Add(clave, imagenesUrls);
                        }

                        // Generar el código HTML de las tarjetas con checkboxes
                        string html = GenerateCheckboxesHtml(clavesInteres, urlsPorClave);

                        return panelHtml + "\n" + html;
                    }
                    catch (System.Text.Json.JsonException)
                    {
                        // Mostrar el mensaje de error en la página web
                        return "Texto no válido como JSON";
                    }
                }
            }
            catch(Exception ex )
            {
                return ex.Message;
            }
        }

        public static string GenerateCheckboxesHtml(List<string> clavesInteres, Dictionary<string, List<string>> urlsPorClave)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // Iniciar el formulario
            htmlBuilder.AppendLine("<form>");
            htmlBuilder.AppendLine("<h3 class=\"text-center\">Graficas</h3>");

            // Generar las tarjetas con checkboxes
            foreach (var clave in clavesInteres)
            {
                htmlBuilder.AppendLine("<div class=\"container\">");
                htmlBuilder.AppendLine("<div class=\"row\">");
                htmlBuilder.AppendLine($"<div class=\"col col-md-12\"><h3>{clave}</h3></div>");

                // Agregar las imágenes y checkboxes para cada imagen
                var urls = urlsPorClave.ContainsKey(clave) ? urlsPorClave[clave] : new List<string>();
                foreach (var url in urls)
                {
                    htmlBuilder.AppendLine("<div class=\"col col-md-4\">");
                    htmlBuilder.AppendLine("<div class=\"form-check\">");
                    htmlBuilder.AppendLine($"<input class=\"form-check-input\" type=\"checkbox\" value=\"{url}\" id=\"{url.GetHashCode()}\">");
                    htmlBuilder.AppendLine($"<label class=\"form-check-label\" for=\"{url.GetHashCode()}\">");
                    htmlBuilder.AppendLine($"<img src=\"{url}\" alt=\"{url}\" style=\"max-width: 300px; max-height: 300px;\">");
                    htmlBuilder.AppendLine("</label>");
                    htmlBuilder.AppendLine("</div>");
                    htmlBuilder.AppendLine("</div>");
                }

                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</div>");
            }

            // Agregar un botón para descargar el PDF (puedes agregar más funcionalidad según tus necesidades)
            htmlBuilder.AppendLine("<button type=\"button\" class=\"btn btn-primary\" id=\"btnGenerarPDF\" onclick=\"fn_GenerarPDF()\">Generar PDF</button>");

            // Cerrar el formulario
            htmlBuilder.AppendLine("</form>");

            return htmlBuilder.ToString();
        }

        public string GenerarPDF(List<string> img)
        {
            try
            {
                // Crear el archivo en la carpeta del proyecto
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PdfFiles");
                Directory.CreateDirectory(folderPath); // Asegurarse de que la carpeta exista

                // Nombre del archivo y ruta completa
                string fileName = "Graficas.pdf";
                string outputPath = Path.Combine(folderPath, fileName);


                // Ordenar las imágenes según el estilo proporcionado en la URL
                img.Sort((url1, url2) =>
                {
                    string[] parts1 = url1.Split(new[] { "_['", "']_" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] parts2 = url2.Split(new[] { "_['", "']_" }, StringSplitOptions.RemoveEmptyEntries);
                    return string.Compare(parts1[1], parts2[1], StringComparison.OrdinalIgnoreCase);
                });

                // Crear el archivo PDF
                using (var stream = new FileStream(outputPath, FileMode.Create))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Agregar las imágenes al PDF
                    foreach (var imageUrl in img)
                    {
                        byte[] imageData;
                        using (WebClient client = new WebClient())
                        {
                            imageData = client.DownloadData(imageUrl);
                        }

                        using (MemoryStream imageStream = new MemoryStream(imageData))
                        {
                            Image image = Image.GetInstance(imageStream);
                            // Ajustar el tamaño de la imagen según tus necesidades
                            image.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
                            image.Alignment = Element.ALIGN_CENTER;

                            // Extraer el título desde la URL y usarlo como título de la imagen en el PDF
                            string[] parts = imageUrl.Split(new[] { "_['", "']_" }, StringSplitOptions.RemoveEmptyEntries);
                            string title = parts[1];
                            Paragraph paragraph = new Paragraph(title, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                            paragraph.Alignment = Element.ALIGN_CENTER;



                            document.Add(paragraph);
                            document.Add(image);
                        }
                    }

                    document.Close();

                    return "https://localhost:44359/PdfFiles/"+ fileName;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class CategoriaModel
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
    }
}