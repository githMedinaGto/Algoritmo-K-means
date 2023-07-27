import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import os
from sklearn.cluster import KMeans
from sklearn.metrics import silhouette_samples, silhouette_score

def k_means(df, filename):
    # Lista de combinaciones de columnas que deseas utilizar
    column_combinations = [
        ["Edad", "Preferencia"],
        ["Edad", "Categoria", "Preferencia"],
        ["Genero", "Categoria"],
        ["Genero", "Categoria", "Preferencia"],
        ["Posibilidades Economicas", "Genero"],
        ["Posibilidades Economicas", "Genero", "Categoria"],
        ["Posibilidades Economicas", "Genero", "Preferencia"]
    ]

    # Número de clusters que deseas obtener (puedes ajustarlo según tus necesidades)
    num_clusters = 2

    # Crear una carpeta para guardar las imágenes y los datos
    output_folder = 'output'
    # URL de la carpeta para guardar las imágenes y los datos
    base_url = "http://127.0.0.1:8000/output/"
    os.makedirs(output_folder, exist_ok=True)

    # Diccionario para almacenar los resultados
    result = {}

    # Lista de tipos de gráficas
    graph_types = ["Gráfico de dispersión", "Gráfico de barras agrupadas", "Gráfico de heatmap", "Gráfico de sectores", "Gráfico de burbujas", "Gráfico de líneas"]

    for columns in column_combinations:
        # Seleccionar las columnas para la combinación actual y crear una copia del DataFrame
        df_seleccionado = df[columns].copy()

        # Crear el modelo K-Means
        kmeans = KMeans(n_clusters=num_clusters, n_init=10, random_state=42)

        # Entrenar el modelo con los datos transformados
        kmeans.fit(df_seleccionado)

        # Obtener las etiquetas de los clusters asignadas a cada fila
        cluster_labels = kmeans.labels_

        # Agregar una columna al DataFrame para mostrar a qué cluster pertenece cada fila
        df_seleccionado['Cluster'] = cluster_labels

        # Diccionario para almacenar los resultados de esta combinación de columnas
        column_result = {}

        # Restaurar las etiquetas originales para la columna 'Preferencia' (opcional, solo para visualización)
        preferencia_labels = {
            1: 'Estilo casual',
            2: 'Estilo formal',
            3: 'Ropa deportiva',
            4: 'Accesorios',
            5: 'Ropa para ocasiones especiales',
            6: 'Teléfonos móviles',
            7: 'Computadoras y laptops',
            8: 'Dispositivos de audio',
            9: 'Dispositivos de entretenimiento',
            10: 'Electrodomésticos inteligentes',
            11: 'Dispositivos de fotografía y videografía',
            12: 'Comida rápida',
            13: 'Comida saludable',
            14: 'Postres y dulces',
            15: 'Bebidas alcohólicas',
            16: 'Bebidas sin alcohol',
            17: 'Productos orgánicos y naturales',
            18: 'Suplementos alimenticios',
            19: 'Equipamiento para hacer ejercicio en casa',
            20: 'Productos para el cuidado de la piel',
            21: 'Productos para el cuidado del cabello',
            22: 'Servicios de bienestar',
            23: 'Muebles',
            24: 'Decoración de interiores',
            25: 'Electrodomésticos',
            26: 'Ropa de cama y baño',
            27: 'Utensilios de cocina',
            28: 'Productos de limpieza y organización',
            29: 'Fútbol',
            30: 'Baloncesto',
            31: 'Tenis',
            32: 'Natación',
            33: 'Ciclismo',
            34: 'Senderismo y camping',
            35: 'Gadgets y dispositivos inteligentes',
            36: 'Software y aplicaciones',
            37: 'Componentes de computadoras',
            38: 'Periféricos',
            39: 'Impresoras y escáneres',
            40: 'Equipos de redes y conectividad',
            41: 'Playa',
            42: 'Montaña',
            43: 'Ciudades históricas',
            44: 'Aventura y deportes extremos',
            45: 'Ecoturismo',
            46: 'Cruceros',
            47: 'Pintura',
            48: 'Escultura',
            49: 'Fotografía',
            50: 'Música',
            51: 'Teatro y espectáculos en vivo',
            52: 'Literatura'
        }
        # Restaurar las etiquetas originales para la columna 'Preferencia' (opcional, solo para visualización)
        if 'Preferencia' in df_seleccionado.columns:
            df_seleccionado['Preferencia'] = df_seleccionado['Preferencia'].map(preferencia_labels)

        # Verificar si la columna "Preferencia" existe en el DataFrame
        if 'Preferencia' in df_seleccionado.columns:
            df_seleccionado['Preferencia'] = df_seleccionado['Preferencia'].astype('category')

        # Guardar los datos transformados en un archivo CSV
        output_file = os.path.join(output_folder, f"{filename}_{str(columns)}.csv")
        df_seleccionado.to_csv(output_file, index=False)

        # Agregar el archivo CSV al diccionario de resultados
        column_result['Archivo CSV'] = os.path.join(base_url, f"{filename}_{str(columns)}.csv")

        for graph_type in graph_types:
            plt.figure(figsize=(8, 6))

            if graph_type == "Gráfico de dispersión":
                sns.scatterplot(x=columns[0], y=columns[1], hue='Cluster', data=df_seleccionado, palette='Set1')
                plt.title(f"{graph_type} - {columns[0]} vs {columns[1]}")

            elif graph_type == "Gráfico de barras agrupadas":
                sns.countplot(x=columns[0], hue='Cluster', data=df_seleccionado, palette='Set1')
                plt.title(f"{graph_type} - Distribución de {columns[0]} según {columns[1]}")

            elif graph_type == "Gráfico de heatmap":
                sns.heatmap(df_seleccionado.pivot_table(index=columns[0], columns=columns[1], values='Cluster', aggfunc='count'), cmap='YlGnBu')
                plt.title(f"{graph_type} - Relación entre {columns[0]}, {columns[1]} y Cluster")

            elif graph_type == "Gráfico de sectores":
                plt.pie(df_seleccionado['Cluster'].value_counts(), autopct='%1.1f%%', colors=sns.color_palette('Set1'))
                plt.title(f"{graph_type} - Proporción de Cluster")

            elif graph_type == "Gráfico de burbujas":
                # Verificar si la columna "Preferencia" existe en el DataFrame
                if 'Preferencia' in df_seleccionado.columns:
                    sns.scatterplot(x=columns[0], y=columns[1], size='Cluster', hue='Preferencia', data=df_seleccionado, palette='Set1')
                    plt.title(f"Gráfico de burbujas - Relación entre {columns[0]}, {columns[1]} y Preferencia")
                    # Generar el nombre de archivo para la imagen
                    image_name = f"{filename}_{str(columns)}_Gráfico_de_burbujas.png"
                    # Agregar la ruta de la imagen al diccionario
                    column_result["Gráfico de burbujas"] = os.path.join(output_folder, image_name)
                    # Guardar la imagen en la carpeta "output"
                    plt.savefig(os.path.join(output_folder, image_name))
                    # Cerrar la figura para liberar memoria
                    plt.close()

            elif graph_type == "Gráfico de líneas":
                df_seleccionado_grouped = df_seleccionado.groupby([columns[0], columns[1]])['Cluster'].mean().reset_index()
                sns.lineplot(x=columns[0], y='Cluster', hue=columns[1], data=df_seleccionado_grouped, palette='Set1')
                plt.title(f"{graph_type} - Evolución del Cluster según {columns[0]} y {columns[1]}")

            # Generar el nombre de archivo para la imagen
            image_name = f"{filename}_{str(columns)}_{graph_type}.png"

            # Agregar la ruta de la imagen al diccionario
            column_result[graph_type] = os.path.join(base_url, image_name)

            # Guardar la imagen en la carpeta "output"
            plt.savefig(os.path.join(output_folder, image_name))

            # Cerrar la figura para liberar memoria
            plt.close()

        # Agregar el diccionario de resultados de esta combinación de columnas al resultado general
        result[str(columns)] = column_result

    return result
