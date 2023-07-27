$(document).ready(function () {
    var selectElement = document.getElementById('cboPreferencias');
    selectElement.disabled = true;
    $('.select-multiple').select2({
        placeholder: "Seleccione una categoría",
        allowClear: true,
        tags: true,
        tokenSeparators: [',', ' ']
    });
    $('.select-multiple2').select2({
        placeholder: "Seleccione preferencias",
        allowClear: true,
        tags: true,
        tokenSeparators: [',', ' ']
    });

    $('.select-multiple').on('change', function () {
        var selectedValues = $(this).val();
        //console.log(selectedValues);
        var cadena = selectedValues.join(", ");
        // Llamar al método AJAX y pasar el resultado
        obtenerResultado(cadena);
    });
});

function obtenerResultado(cadena) {
    $.ajax({
        type: "POST",
        url: "Index.aspx/ObtenerResultado",
        data: JSON.stringify({ cadena: cadena }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization": "Bearer <token>"
        },
        success: function (data) {
            // Manejar la respuesta del servidor aquí
            // Obtener el elemento select por su ID
            var selectElement = document.getElementById("cboPreferencias");
            selectElement.disabled = false;
            // Limpiar el contenido actual del select
            selectElement.innerHTML = "";
            selectElement.innerHTML = data.d;
        },
        error: function (xhr, status, error) {
            // Manejar errores aquí
            alertify.error(status);
        }
    });
}

document.getElementById("btnBuscar").addEventListener("click", function () {

    // Obtener los valores seleccionados del selector "cboPreferencias"
    var cboPreferencias = document.getElementById("cboPreferencias");
    var preferenciasSeleccionadas = Array.from(cboPreferencias.selectedOptions).map(option => option.value);

    // Obtener el valor seleccionado del selector "cboRangoEdades"
    var cboRangoEdades = document.getElementById("cboRangoEdades");
    var rangoEdadesSeleccionado = cboRangoEdades.value;

    // Obtener el valor seleccionado del selector "cboPosibilidadesEco"
    var cboPosibilidadesEco = document.getElementById("cboPosibilidadesEco");
    var posibilidadesEcoSeleccionadas = cboPosibilidadesEco.value;


    $.ajax({
        type: "POST",
        url: "Index.aspx/GenerarTabla",
        data: JSON.stringify({ Preferencias: preferenciasSeleccionadas, RangoEdades: rangoEdadesSeleccionado, PosEcononomicas: posibilidadesEcoSeleccionadas }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization": "Bearer <token>"
        },
        success: function (data) {
            var result = data.d.split(';').map(function (obj) {
                return obj.split(',');
            });
            //console.log(result);

            const table = new DataTable('#example', {
                columns: [
                    { title: 'ID' },
                    { title: 'Nombre' },
                    { title: 'Edad' },
                    { title: 'Genero' },
                    { title: 'Region' },
                    { title: 'Ciudad' },
                    { title: 'Posibilidades Economicas' },
                    { title: 'Categoria' },
                    { title: 'Preferencia' },
                ],
                data: result,
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-MX.json'
                },
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: ':visible'
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];

                            // Loop over the cells in column `C`
                            $('row c[r^="C"]', sheet).each(function () {
                                // Get the value
                                if ($('is t', this).text() == 'New York') {
                                    $(this).attr('s', '20');
                                }
                            });
                        },
                        action: function (e, dt, button, config) {
                            // Obtener los nombres de las columnas de la tabla
                            var columnas = dt.columns().header().toArray().map(function (columna) {
                                return columna.innerText;
                            });

                            // Obtener los datos de la tabla
                            var datos = dt.data().toArray();

                            // Agregar los nombres de las columnas como la primera fila de datos
                            datos.unshift(columnas);

                            // Convertir el arreglo en formato JSON
                            var jsonData = JSON.stringify(datos);

                            alertify.success("Se esta trabajando en segundo plano el agrupamiento, porfavor espere");

                            // Enviar la solicitud AJAX al método web
                            $.ajax({
                                type: "POST",
                                url: "Index.aspx/ObtenerDatos",
                                data: JSON.stringify({ datos: jsonData }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var divDatos = document.getElementById("divContainer");
                                    divDatos.disabled = false;
                                    // Limpiar el contenido
                                    divDatos.innerHTML = "";
                                    divDatos.innerHTML = data.d;
                                },
                                error: function (xhr, status, error) {
                                    // Manejar errores aquí
                                    alertify.error(status);
                                }
                            });

                        }
                    },
                    //'colvis'
                ]
            });         



            table.on('click', 'tbody tr', (e) => {
                let classList = e.currentTarget.classList;

                if (classList.contains('selected')) {
                    classList.remove('selected');
                }
                else {
                    table.rows('.selected').nodes().each((row) => row.classList.remove('selected'));
                    classList.add('selected');
                }
            });

            document.querySelector('#buttonEliminar').addEventListener('click', function () {
                table.row('.selected').remove().draw(false);
            });
        },
        error: function (xhr, status, error) {
            // Manejar errores aquí
            alertify.error(status);
        }
    });
});

function fn_GenerarPDF() {

    const checkboxes = document.querySelectorAll('.form-check-input');

    // Crear un arreglo para almacenar los valores seleccionados
    const valoresSeleccionados = [];

    // Recorrer los checkboxes para verificar cuáles están seleccionados
    checkboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            // Agregar el valor del checkbox (en este caso, la URL de la imagen) al arreglo
            valoresSeleccionados.push(checkbox.value);
        }
    });

    if (valoresSeleccionados.length > 0) {
        // Enviar la solicitud AJAX al método web
        $.ajax({
            type: "POST",
            url: "Index.aspx/GenerarPDF",
            data: JSON.stringify({ urls: valoresSeleccionados }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                // Abrir una nueva pestaña con la URL del PDF
                window.open(data.d, "_blank");
            },
            error: function (xhr, status, error) {
                // Manejar errores aquí
                alertify.error(status);
            }
        });

    } else {
        alertify.warning('No se ha seleccionado ninguna imagen.');
    }
};

