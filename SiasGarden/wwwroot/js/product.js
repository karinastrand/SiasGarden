var dataTable
$(document).ready(function () {
    loadDataTable();
    
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        language: {
            search: "S\u00F6k:",
            lengthMenu: " _MENU_ per sida",
            info: "Visar _START_ - _END_ av _TOTAL_ poster",
            infoFiltered: "(filtrerat fr\u00E5n _MAX_ poster totalt)"
        },
        scrollY:450,
       "ajax": { url: '/admin/product/getall' },

        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'latinName', "width": "25%" },
            { data: 'price', "width": "10%" },
            { data: 'subCategory.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/product/upsert?id=${data}" class="btn btn-success mx-2 rounded"><i class="bi bi-pencil-square"></i>Redigera</a>
                     <a onclick=Delete('/admin/product/delete?id=${data}') class="btn btn-danger mx-2 rounded"><i class="bi bi-trash-fill"></i>Ta bort</a>
                    </div>`
                },
               
                "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "\u00C4r du s\u00E4ker?",
        text: "Du kommer inte att kunna \u00E5ngra det!!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "Avbryt",
        confirmButtonText: "Ja, ta bort den!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
            
        }
    });
}
