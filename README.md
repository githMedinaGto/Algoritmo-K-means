# Algoritmo-K-means
Se hace un ejemplo del uso del algoritmo de k-means con una aplicación web y una api en python

Breve descripción del proyecto.

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
