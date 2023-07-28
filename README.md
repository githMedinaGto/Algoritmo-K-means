# Algoritmo-K-means
Se hace un ejemplo del uso del algoritmo de k-means con una aplicación web y una api en python

## Proyecto K-Means

Este proyecto consiste en una API y una aplicación web que implementan el algoritmo de agrupamiento K-Means. La API está desarrollada en Python utilizando el framework FastAPI, mientras que la aplicación web está desarrollada en ASP.NET.

La API proporciona endpoints(ver Anexo1) para cargar archivos Excel, procesar los datos utilizando el algoritmo K-Means y obtener los resultados de los grupos generados. Los datos del archivo Excel se transforman y se utilizan diferentes combinaciones de columnas para realizar el agrupamiento. Los resultados se devuelven en forma de archivos CSV y gráficos interactivos.

La aplicación web permite a los usuarios interactuar con la API de manera intuitiva a través de una interfaz gráfica. Los usuarios pueden cargar archivos Excel, seleccionar las columnas para el agrupamiento y visualizar los resultados en forma de gráficos de dispersión, gráficos de barras agrupadas, mapas de calor, gráficos de sectores, gráficos de burbujas y gráficos de líneas.

El proyecto utiliza bibliotecas populares como pandas, seaborn y matplotlib para el procesamiento de datos y la visualización. Además, se implementa la función de transformación de variables para ajustar los datos antes de aplicar el algoritmo K-Means.

Este proyecto es ideal para aquellos interesados en el análisis de datos y la implementación de algoritmos de aprendizaje automático. Proporciona una solución completa para realizar agrupamientos utilizando el algoritmo K-Means con diferentes combinaciones de columnas y visualizar los resultados de manera interactiva.

## Estructura del Proyecto

El proyecto está organizado en dos carpetas principales:

1. **API_Kmeans**: Esta carpeta contiene una API programada en Python utilizando el framework FastAPI.
2. **Aplicación Web**: Esta carpeta contiene una aplicación web desarrollada en ASP.NET.

## Ejecución de la API

Sigue los siguientes pasos para ejecutar la API:

1. Abre la carpeta `API_Kmeans` en Visual Studio Code.
2. Instala los siguientes paquetes ejecutando los siguientes comandos en el terminal:
   ```
   pip install fastapi uvicorn
   ```
3. Inicia el servidor ejecutando el siguiente comando en el terminal:
   ```
   api-kmeans\Scripts\activate
   ```
   Esto activará el entorno virtual y mostrará `(api-kmeans)` antes de la ruta del proyecto en la terminal.
4. Verifica si tienes instalados los siguientes paquetes:
   ```
   pip install numpy pandas matplotlib scikit-learn seaborn python-multipart
   ```
5. Asegúrate de tener una carpeta llamada `output` en la raíz del proyecto, donde se encuentran los archivos `.py` como `main`, `leerExcel` y `kMeans`.
6. Para iniciar el servidor de la API, ejecuta el siguiente comando en el terminal:
   ```
   uvicorn main:app --reload
   ```
   Esto mostrará una URL que puedes abrir en tu navegador (haz clic con Ctrl para abrir) para probar la API. Agrega `/docs` al final de la URL para acceder a la documentación de la API.

## Ejecución de la Aplicación Web

Sigue los siguientes pasos para ejecutar la aplicación web:

1. Asegúrate de tener una base de datos en SQL Server. Puedes utilizar el archivo `QuerydbEjemplo.sql` para crear la base de datos con algunos ejemplos.
2. Asegúrate de tener un usuario con al menos privilegios de lectura en la base de datos `dbEjemplo`.
3. Abre el archivo `PerspectivaCliente.sln` en Visual Studio 2022 desde la carpeta `PerspectivaCliente`.
4. Configura la conexión a la base de datos en el archivo `web.config`. Realiza los siguientes cambios en la sección `<connectionStrings>`:
   ```
   <connectionStrings>
       <add name="dbEjemploEntities" connectionString="metadata=res://*/Models.DB.csdl|res://*/Models.DB.ssdl|res://*/Models.DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=ConexionServidor;initial catalog=dbEjemplo;user id=User;password=****;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
   </connectionStrings>
   ```
   - `data source`: Es la conexión al servidor.
   - `initial catalog`: Es el nombre de la base de datos.
   - `user id`: Es el usuario con los privilegios necesarios.
   - `password`: Es la contraseña del usuario.

5. Ejecuta la aplicación web desde Visual Studio 2022.

## Anexo

### Anexo 1
<b>Endpoints :</b> son direcciones URL específicas, a las que se pueden enviar solicitudes HTTP para interactuar con el sistema. Cada endpoint está asociado con una operación o función específica, como cargar datos, ejecutar el algoritmo K-Means y obtener los resultados de los grupos generados.

Estos endpoints permiten a los usuarios enviar y recibir datos de manera estructurada, lo que facilita la comunicación entre la aplicación web y la API. Al utilizar estos endpoints, los usuarios pueden realizar diversas acciones y obtener los resultados deseados de manera eficiente y segura.
