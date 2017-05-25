
var userId = getCookie('userId');

function getCookie(name) {
    var regexp = new RegExp("(?:^" + name + "|;\s*" + name + ")=(.*?)(?:;|$)", "g");
    var result = regexp.exec(document.cookie);
    return (result === null) ? null : result[1];
}

function getUsers(event) {
    var expresion = $('#userSearch').val();
    var elemento;
    var problemDetailId = $('#ProblemDetailId').attr('id')

    if (event) {
        elemento = event.target.id
    }
     
    $.ajax({
        type: 'GET',
        url: '/Users/GetUsers',
        data: { userId: userId, exp: expresion },
        datatype: 'Json',
        cache: false,
        success: function (data) {
            console.log(data);
            var result = '<table class="table table-striped"><thead><tr><th>Nombre</th><th>Email</th><th> </th></tr></thead><tbody><tr>';

            if (elemento != 'asignar') {
                for (var i = 0; i < data.noFollowers.length; i++) {
                    result += '<td>' + data.noFollowers[i].Name + '</td><td>' + data.noFollowers[i].Email + '</td><td><a class = "btn btn-primary" href="javascript:addFollower(' + data.noFollowers[i].UserId + ',' + userId + ')" name="follow">Follow</a></td></tr>';
                }

                for (var i = 0; i < data.followers.length; i++) {
                    result += '<td>' + data.followers[i].Name + '</td><td>' + data.followers[i].Email + '</td><td><a class = "btn btn-danger"  href="javascript:unFollow(' + data.followers[i].UserId + ',' + userId + ')" name="unFollow">UnFollow</a></td></tr>';
                }
            } else {
        
                for (var i = 0; i < data.followers.length; i++) {
                    result += '<td>' + data.followers[i].Name + '</td><td>' + data.followers[i].Email + '</td><td><a class = "btn btn-danger"  href="javascript:Asignar(' + $('#ProblemDetailId').val() + ',' + data.followers[i].UserId + ')" >Asignar</a></td></tr>';
                }

                for (var i = 0; i < data.noFollowers.length; i++) {
                    result += '<td>' + data.noFollowers[i].Name + '</td><td>' + data.noFollowers[i].Email + '</td><td><a class = "btn btn-primary" href="javascript:Asignar(' + $('#ProblemDetailId').val() + ',' + data.noFollowers[i].UserId + ')" >Asignar</a></td></tr>';
                }
            }
            

            result += '</tr></tbody></table>';
            var a = document.getElementById('usersFound');
            console.log(a);
            document.getElementById('usersFound').innerHTML = result;

        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function addFollower(userId, followerId) {
    $.ajax({
        type: 'POST',
        url: '/Users/Follow',
        data: { userId: userId, followerId: followerId },
        cache: false,
        success: getUsers(),
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function unFollow(userId, followerId) {
    $.ajax({
        type: 'POST',
        url: '/Users/UnFollow',
        data: { userId: userId, followerId: followerId },
        cache: false,
        success: getUsers(),
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function Asignar(ProblemDetailId, UserId) {
    $.ajax({
        type: 'POST',
        url: '/ProblemDetails/Asignar',
        data: { ProblemDetailId: ProblemDetailId, UserId: UserId },
        cache: false,
        success: function (response) {
            location.reload();
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function notification() {
    $.ajax({
        type: 'GET',
        url: '/ProblemDetails/getFollowedEntries',
        data: { id: userId },
        cache: false,
        success: function (data) {
            if (data.length > 0) {
                var result = '';

                for (var i = 0; i < data.length; i++) {
                    result += '<dl class="dl-horizontal">';
                    result += '<dt>Problema</dt><dd>' + data[i].Problema + '</dd>';
                    result += '<dt>Titulo</dt><dd>' + data[i].Titulo + '</dd>';
                    result += '<dt>Departamento</dt><dd>' + data[i].Departamento + '</dd>';
                    result += '<dt>Creado Por</dt><dd>' + data[i].CreadoPor + '</dd>';
                    result += '<dt>Descripcion</dt><dd>' + data[i].Descripcion + '</dd>';
                    result += '</dl>';
                }
                document.getElementById('notification').innerHTML = result;

                $('#notification').fadeIn(2000);

                setTimeout(function () {
                    $('#notification').fadeOut(3000);
                }, 10000);
            }
        }
    });
}

function historicoSolicitudes() {
    $.ajax({
        type: 'GET',
        url: 'HistoricoSolicitudes',
        datatype: 'Json',
        data: { startDate: $('#startDate').val(), endDate: $('#endDate').val(), categoria: 4},
        cache: false,
        success: function (data) {
            if (data.length > 0) {
                var result = '';
                for (var i = 0; i < data.length; i++) {
                    result += '<tr><td>' + data[i].problemDetailId + '</td>';
                    result += '<td>' + data[i].problem + '</td>';
                    result += '<td>' + data[i].client + '</td>';
                    result += '<td>' + data[i].department + '</td>';
                    result += '<td>' + data[i].place + '</td>';
                    result += '<td>' + data[i].createdBy + '</td>';
                    result += '<td>' + data[i].description + '</td>';
                    result += '<td>' + data[i].date + '</td></tr>';               
                }

                document.getElementById('historicoSolicitudes').lastElementChild.innerHTML = result;
            }

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function searchSolution() {
    $.ajax({
        type: 'GET',
        url: 'SearchSolution',
        data: { exp: $('#searchSolution').val() },
        cache: false,
        success: function (data) {
            if (data.length > 0) {
                var result = '<tr><th>Problema</th><th>Titulo de la Solicitud</th><th>Nombre</th><th>Solucion</th><th>Fecha</th>';
                for (var i = 0; i < data.length; i++) {
                    result += '<tr><td>' + data[i].problem + '</td>';
                    result += '<td>' + data[i].title + '</td>';
                    result += '<td>' + data[i].name + '</td>';
                    result += '<td>' + data[i].solution + '</td>';
                    result += '<td>' + data[i].date + '</td>';
                    result += '<td><a href="Details/' + data[i].solutionId + '">Detalles</td></tr>'
                       
                }

                document.getElementById('solutionTable').innerHTML = result;
            }
        },
        error: function (error) {
            alert(error.responseText);
        }
    })
}


function loadManegentTime() {
    $.ajax({
        type: 'GET',
        url: 'ManagementTimeReport',
        data: { startDate: $('#startDate').val(), endDate: $('#endDate').val(), categoria: $('#categorias').val() },
        datatype: 'Json',
        cache: false,
        success: function (data) {
            if (data.length > 0 || data != 0) {
                var result = '<caption style="text-align:center">Tiempo total de gestion</caption>';
                if ($('#categorias').val() == 4) {
                    result += '<tr><td>Numero de soluciones dentro de 1 hora </td><td>' + data.hora1 + '</td><td>' + data.hora1 / data.total * 100 + '%</td></tr>';
                    result += '<tr><td>Numero de soluciones dentro de 4 horas </td><td>' + data.hora4 + '</td><td>' + data.hora4 / data.total * 100 + '%</td></tr>';
                    result += '<tr><td>Numero de soluciones dentro de 8 horas </td><td>' + data.hora8 + '</td><td>' + data.hora8 / data.total * 100 + '%</td></tr>';
                    result += '<tr><td>Numero de soluciones dentro de 24 horas </td><td>' + data.hora24 + '</td><td>' + data.hora24 / data.total * 100 + '%</td></tr>';
                    result += '<tr><td>Numero de soluciones mayor de 24 horas </td><td>' + data.mhora24 + '</td><td>' + data.mhora24 / data.total * 100 + '%</td></tr>';
                    result += '<tr><td>Total</td><td colspan="2">' + data.total + '</td></tr>';
                } else {

                    for (var i = 0; i < data.length; i++) {
                        result += '<tr><th colspan="3">' + data[i].category + '</th></tr>';
                        result += '<tr><td>Numero de soluciones dentro de 1 hora </td><td>' + data[i].hora1 + '</td><td>' + data[i].hora1 / data[i].total * 100 + '%</td></tr>';
                        result += '<tr><td>Numero de soluciones dentro de 4 horas </td><td>' + data[i].hora4 + '</td><td>' + data[i].hora4 / data[i].total * 100 + '%</td></tr>';
                        result += '<tr><td>Numero de soluciones dentro de 8 horas </td><td>' + data[i].hora8 + '</td><td>' + data[i].hora8 / data[i].total * 100 + '%</td></tr>';
                        result += '<tr><td>Numero de soluciones dentro de 24 horas </td><td>' + data[i].hora24 + '</td><td>' + data[i].hora24 / data[i].total * 100 + '%</td></tr>';
                        result += '<tr><td>Numero de soluciones mayor de 24 horas </td><td>' + data[i].mhora24 + '</td><td>' + data[i].mhora24 / data[i].total * 100 + '%</td></tr>';
                        result += '<tr><td>Total</td><td colspan="2">' + data[i].total + '</td></tr>';
                    }
                }
                
                

                document.getElementById('managementTimeTable').innerHTML = result;
            }

        },
        error: function (error) {
            console.log(error);
        }
    });
}

if (window.location.pathname == '/Reports/StaticsReport') {
    google.charts.load('current', { 'packages': ['corechart', 'table'] });
    google.charts.setOnLoadCallback(drawChart);

    //google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}


function loadGraphics() {

    
    $.ajax({
        type: 'GET',
        url: 'HistoricoSolicitudes',
        data: { startDate: $('#startDate').val(), endDate: $('#endDate').val(), categoria: $('#categorias').val() },
        cache: false,
        success: function (data) {
            console.log(data);
            var datosChart;

            if ($('#categorias').val() == 0) {
                datosChart = new Array(['Client', 'Quantity']);
            } else if ($('#categorias').val() == 1) {
                datosChart = new Array(['RequestType', 'Quantity']);
            } else if ($('#categorias').val() == 2) {
                datosChart = new Array(['Department', 'Quantity']);
            } else {
                datosChart = new Array(['Assigned', 'Quantity']);
            }

            var datosTable = new Array();
            var total = 0;
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var array = [data[i].categoria, data[i].quantity];
                    datosChart.push(array);
                    total += data[i].quantity;
                }

                for (var i = 0; i < data.length; i++) {
                    var array = [data[i].categoria, data[i].quantity, { v: data[i].quantity / total, f: ((data[i].quantity / total) * 100).toPrecision(4) + '%' }];
                    datosTable.push(array);
                }

               
                
                drawChart(datosChart, $('#categorias').val());
                drawTable(datosTable, $('#categorias').val());


            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function drawTable(datos, categoria) {
    var data = new google.visualization.DataTable();
    if (categoria == 0) {
        data.addColumn('string', 'Cliente');
    } else if (categoria == 1) {
        data.addColumn('string', 'Tipo de Solicitud');
    } else if (categoria == 2) {
        data.addColumn('string', 'Departamento');
    } else
    {
        data.addColumn('string', 'Tecnico asignado');
    }
    
    data.addColumn('number', 'Reportes');
    data.addColumn('number', 'Porcentaje');

    data.addRows(datos);

    var table = new google.visualization.Table(document.getElementById('table_div'));
    table.draw(data, { showRowNumber: true, width: '100%', heigth: '100%' });
}

function drawChart(datos, categoria) {
    var data = google.visualization.arrayToDataTable(datos);
    var options;
    if (categoria == 0) {
        options = {
            title: 'Volumen/Cliente'
        };
    } else if (categoria == 1) {
        options = {
            title: 'Volumen/Tipo de Solicitud'
        };
    } else if (categoria == 2) {
        options = {
            title: 'Volumen/Departamento'
        };
    } else {
        var options = {
            title: 'Volumen/Tecnico Asignado'
        };
    }
    
    

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
}
    
    //window.addEventListener('load', function () {
    //    search.addEventListener('focus', Search);
//})

window.onload = function () {
    $('#searchUsers').on('click', getUsers);
    $('#userSearch').on('keyup', getUsers);
    $('#asignar').on('click', getUsers);
    if (userId != null && userId != "") {
        setInterval(notification, 20000);
    }
    if (window.location.pathname == '/Reports/StaticsReport') {
        loadGraphics();
        $('#btnGraphisFilter').on('click', loadGraphics);
    }
    $('#btnBuscarHistorico').on('click', historicoSolicitudes);
    $('#searchSolution').on('change', searchSolution)

    if (window.location.pathname == '/Reports/ManagementTime') {
        loadManegentTime();
        $('#btnManagementFilter').on('click', loadManegentTime);
    }
}

    
