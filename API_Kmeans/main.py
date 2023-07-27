import pandas as pd
from fastapi import FastAPI, UploadFile, File
from fastapi.staticfiles import StaticFiles
from pydantic import BaseModel
from typing import Optional
from leerExcel import leer_archivo_excel
import os
import json

app = FastAPI()

class Excel(BaseModel):
    ID: Optional[int]
    Nombre: Optional[str]
    Edad: Optional[int]
    Genero: Optional[str]
    Region: Optional[str]
    Ciudad: Optional[str]
    PosibilidadesEconomicas: Optional[str]
    Categoria: Optional[str]
    Preferencia: Optional[str]

def get_file_data(filename: str) -> dict:
    # Cargar el archivo JSON si existe, de lo contrario, crear un nuevo diccionario
    if os.path.exists("file_data.json"):
        with open("file_data.json", "r") as f:
            data = json.load(f)
    else:
        data = {}
    return data.get(filename, {})

def save_file_data(filename: str, data: dict):
    # Crear el archivo "file_data.json" si no existe
    if not os.path.exists("file_data.json"):
        with open("file_data.json", "w") as f:
            json.dump({}, f)

    with open("file_data.json", "r") as f:
        file_data = json.load(f)
    file_data[filename] = data
    with open("file_data.json", "w") as f:
        json.dump(file_data, f)

@app.get("/")
def index():
    return {"message": "Bienvenido!!"}

@app.post("/ObtenerExcel")
async def obtener_Excel(excel: UploadFile = File(...)):
   # Guardar el archivo en el sistema de archivos
    with open(excel.filename, "wb") as f:
        f.write(excel.file.read())

    # Obtener la ruta completa del archivo
    ruta_archivo = os.path.abspath(excel.filename)

    # Verificar si el archivo y los datos ya han sido procesados previamente
    file_data = get_file_data(ruta_archivo)
    if file_data:
        return {"message": f"El archivo {excel.filename} ya ha sido procesado previamente", "resultado": file_data}

    # Llamar al método leer_archivo_excel con la ruta del archivo
    resultado = leer_archivo_excel(ruta_archivo, excel.filename)

    # Guardar los resultados en el registro
    save_file_data(ruta_archivo, resultado)

    return {"message": f"Excel recibido: {excel.filename}", "resultado": resultado}


app.mount("/output", StaticFiles(directory="output"), name="output")