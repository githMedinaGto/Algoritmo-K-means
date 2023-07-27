<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PerspectivaCliente.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%: Page.Title %>Perspectivas de Clientes</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.13.5/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style>
        /* Estilo para hacer el icono más grande y cambiar el color */
        i.fa-file-excel-o {
            font-size: 30px; /* Tamaño del icono (puedes ajustarlo según tus preferencias) */
            color: green; /* Cambiar el color del icono a verde */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
            <div class="container">
                <a class="navbar-brand" runat="server" href="~/Index.aspx">Perspectiva de Clientes</a>
                <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Alternar navegación" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item"><a class="nav-link" runat="server" href="~/Index.aspx">Inicio</a></li>
                    </ul>
                </div>
            </div>
        </nav>
        <div class="container body-content">


            <div class="container">
                <div class="row">
                    <div class="col col-md-3">
                        <label for="cboEjemplo">Categorías:</label>
                        <select id="cboEjemplo" class="form-select select-multiple" name="states[]" multiple runat="server">
                            <option value="" disabled selected>Seleccione una categoría</option>
                        </select>
                    </div>
                    <div class="col col-md-3">
                        <label for="cboPreferencias">Preferencias por categoría:</label>
                        <select id="cboPreferencias" class="form-select select-multiple2" name="states[]" multiple runat="server">
                        </select>
                    </div>
                    <div class="col col-md-3">
                        <label for="cboRangoEdades">Rango de edades:</label>
                        <select id="cboRangoEdades" class="form-select select2" runat="server">
                            <option value="" disabled selected>Seleccione rango de edades</option>
                            <option value="0">Todas las edades</option>
                            <option value="18">Menores a 18 años</option>
                            <option value="18-24">18 a 24 años</option>
                            <option value="25-34">25 a 34 años</option>
                            <option value="25-34">25 a 34 años</option>                                                  
                            <option value="35-44">35 a 44 años</option>
                            <option value="45-54">45 a 54 años</option>
                            <option value="54">Mayores a 54 años</option>
                        </select>
                    </div>
                    <div class="col col-md-3">
                        <label for="cboPosibilidadesEco">Posibilidades económicas::</label>
                        <select id="cboPosibilidadesEco" class="form-select select2" runat="server">
                            <option value="" disabled selected>Seleccione posibilidades economicas</option>
                            <option value="0">Todos los rangos</option>
                            <option value="1">Baja</option>
                            <option value="2">Media</option>
                            <option value="3">Alta</option>
                        </select>
                    </div>
                    <div class="col col-md-12">
                        <br />
                        <%--<button id="btnBuscar" type="button" class="btn btn-primary" OnClick="btnBuscar_Click">Buscar</button>--%>
                        <button id="btnBuscar" type="button" class="btn btn-primary">Buscar</button>

                    </div>
                </div>
                <div class="row">
                    <div class="col col-md-1"></div>
                    <div class="col col-md-3">
                        <p><button type="button" id="buttonEliminar" class="btn btn-danger">Eliminar columna seleccionada</button></p>
                    </div>
                    <div class="col col-md-12">
                        <table id="example" class="display" width="100%"></table>
                    </div>
                </div>
            </div>



            <div id="divContainer" runat="server"></div>


            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %>Perspectivas de Clientes</p>
            </footer>
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>

        <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
        <script src="https://cdn.datatables.net/1.13.5/js/jquery.dataTables.min.js"></script>
        <script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
        <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>
        <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.colVis.min.js"></script>
        
        <script src="https://unpkg.com/xlsx/dist/xlsx.full.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2.0.5/FileSaver.min.js"></script>

        <!-- JavaScript -->
        <script src="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/alertify.min.js"></script>

        <!-- CSS -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/alertify.min.css"/>
        <!-- Default theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/default.min.css"/>
        <!-- Semantic UI theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/semantic.min.css"/>
        <!-- Bootstrap theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/bootstrap.min.css"/>

        <!-- 
            RTL version
        -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/alertify.rtl.min.css"/>
        <!-- Default theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/default.rtl.min.css"/>
        <!-- Semantic UI theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/semantic.rtl.min.css"/>
        <!-- Bootstrap theme -->
        <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.13.1/build/css/themes/bootstrap.rtl.min.css"/>

        <script src="Index.js"></script>
    </asp:PlaceHolder>
</body>
</html>
