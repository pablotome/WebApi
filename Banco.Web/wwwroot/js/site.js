// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    traerClientes();
    $('#modalCliente').on('shown.bs.modal', function () {
        $('#txtApellido').trigger('focus');
    });
});

function traerClientes() {
    $.getJSON("http://localhost:64186/api/cliente",
        function (data) {
            $("#tblClientes tbody").html("");
            $.each(data, function (key, val) {
                $("#tblClientes").append('<tbody><tr>' +
                    '<td>' + val.apellido + '</td>' +
                    '<td>' + val.nombre + '</td>' +
                    '<td>' + moment(val.fechaNacimiento).format("DD/MM/YYYY") + '</td>' +
                    '<td>' + moment(val.fechaAlta).format("DD/MM/YYYY") + '</td>' +
                    '<td><button type="button" class="btn btn-danger" onclick="eliminarCliente(' + val.id +')">Eliminar</button></td>' +
                    '</tr></tbody>');
            });
        });
}

function agregarCliente() {
    if ($("#txtApellido").val() === "")
        alert("El Apellido es obligatorio");
    else if ($("#txtNombre").val() === "")
        alert("El Nombre es obligatorio");
    else {
        var json = {
            apellido: $("#txtApellido").val(),
            nombre: $("#txtNombre").val(),
            fechaNacimiento: $("#txtFechaNacimiento").val()
        };
        
        $.ajax({
            type: 'POST',
            url: 'http://localhost:64186/api/cliente',
            data: JSON.stringify(json),
            success: function (data) {
                traerClientes();
                $('#modalCliente').modal('hide');
            },
            contentType: "application/json",
            dataType: 'json'
        });
    }
}

function eliminarCliente(id) {
    $.ajax({
        type: 'DELETE',
        url: 'http://localhost:64186/api/cliente/'+id,
        success: function (data) {
            traerClientes();
        },
        contentType: "application/json",
        dataType: 'json'
    });
}