import pandas as pd
from kMeans import k_means

def leer_archivo_excel(nombre_archivo: str, filename) -> str:
    # Leer el archivo Excel
    df = pd.read_excel(nombre_archivo)
    # Eliminar las columnas "ID" y "Nombre"
    df = df.drop(["ID", "Nombre"], axis=1)
    #Eliminar las columnas "Region" y "Ciudad"
    df = df.drop(["Region", "Ciudad"], axis=1)
    # Llamar a la función transformar_variables para transformar las variables
    df_transformado = transformar_variables(df, filename)
    return df_transformado

def transformar_variables(df: str, filename) -> dict:
    # Transformar la variable Edad
    df['Edad'] = df['Edad'].apply(lambda x: 0 if x < 18 else 
                                  (1 if 18 <= x <= 24 else 
                                   (2 if 25 <= x <= 34 else 
                                    (3 if 35 <= x <= 44 else 
                                     (4 if 45 <= x <= 54 else 5)))))
    
    #Transformar la variable Genero
    df['Genero'] = df['Genero'].replace({
        'Masculino': 1,
        'Femenino' : 0
    })

    # Transformar la variable Preferencia
    df['Categoria'] = df['Categoria'].replace({
        'Ropa y moda': 1,
        'Electrónica': 2,
        'Alimentos y bebidas': 3,
        'Salud y bienestar': 4,
        'Hogar y decoración': 5,
        'Deportes y actividades al aire libre': 6,
        'Tecnología': 7,
        'Viajes y turismo': 8,
        'Arte y cultura': 9,
        'Otros': 10
    })

    # Asignar valores específicos a la columna "Categoria"
    df['Posibilidades Economicas'] = df['Posibilidades Economicas'].replace({
        'Bajas': 1,
        'Medias': 2,
        'Altas': 3
    })

    # Asignar valores específicos a la columna "Preferencia"
    df['Preferencia'] = df['Preferencia'].replace({
        'Estilo casual' : 1,
        'Estilo formal' : 2,
        'Ropa deportiva' : 3,
        'Accesorios' : 4,
        'Ropa para ocasiones especiales' : 5,
        'Teléfonos móviles' : 6, 
        'Computadoras y laptops' : 7,
        'Dispositivos de audio' : 8,
        'Dispositivos de entretenimiento' : 9,
        'Electrodomésticos inteligentes' : 10,
        'Dispositivos de fotografía y videografía': 11,
        'Comida rápida' : 12,
        'Comida saludable': 13,
        'Postres y dulces' : 14,
        'Bebidas alcohólicas' : 15,
        'Bebidas sin alcohol' : 16,
        'Productos orgánicos y naturales' : 17,
        'Suplementos alimenticios' : 18,
        'Equipamiento para hacer ejercicio en casa' : 19,
        'Productos para el cuidado de la piel' : 20,
        'Productos para el cuidado del cabello' : 21,
        'Servicios de bienestar' : 22,
        'Muebles' : 23,
        'Decoración de interiores' : 24,
        'Electrodomésticos' : 25,
        'Ropa de cama y baño' : 26,
        'Utensilios de cocina' : 27,
        'Productos de limpieza y organización' : 28,
        'Fútbol' : 29,
        'Baloncesto' : 30,
        'Tenis' : 31,
        'Natación' : 32,
        'Ciclismo' : 33,
        'Senderismo y camping' : 34,
        'Gadgets y dispositivos inteligentes' : 35,
        'Software y aplicaciones' : 36,
        'Componentes de computadoras' : 37,
        'Periféricos' : 38,
        'Impresoras y escáneres' : 39,
        'Equipos de redes y conectividad' : 40,
        'Playa' : 41,
        'Montaña' : 42,
        'Ciudades históricas' : 43,
        'Aventura y deportes extremos' : 44,
        'Ecoturismo' : 45,
        'Cruceros' : 46,
        'Pintura' : 47,
        'Escultura' : 48,
        'Fotografía' : 49,
        'Música' : 50,
        'Teatro y espectáculos en vivo' : 51,
        'Literatura' : 52
    })

    # Asignar valores específicos a la columna "Ubicacion"
    # Crear columnas binarias para las variables categóricas

    # Llamar al método k_means con los datos transformados
    resultado = k_means(df, filename)
    
    return resultado
