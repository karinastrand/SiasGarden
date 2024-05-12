var dataTable
$(document).ready(function () {
    loadDataTable();
    
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        language: {
            search: "S\u00F6k:",
            lengthMenu: " _MENU_ per sida",
            info: "Visar _START_ till _END_ av _TOTAL_ poster"
        },
        scrollY:450,
       "ajax": { url: '/admin/user/getall' },

        "columns": [
        { data: 'firstName', "width": "20%" },
            { data: 'lastName', "width": "25%" },
            { data: 'email', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/user/edit?userId=${data}" class="btn btn-success mx-2"><i class="bi bi-pencil-square"></i>Redigera</a>
                    </div>`
                },
               
                "width": "30%"
            }
    ]
    });
}


