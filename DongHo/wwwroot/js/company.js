var dataTable;
$(document).ready(function () {
    loadData();
});

function loadData() {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url": "/admin/company/getall" },
        "columns": [
            { "data": "name","width":"15%"},
            { "data": "streetAddress","width":"15%"},
            { "data": "district","width":"15%"},
            { "data": "province","width":"15%"},
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                           
                             <a href="/Admin/Company/Update?id=${data}" class="btn btn-primary">
                                <i class="bi bi-pencil"></i>
                             </a>
                             <a class="btn btn-danger" onClick=Delete('/Admin/Company/Delete/${data}')>
                                <i class="bi bi-trash"></i>
                             </a>
                          
                           `
                }
                ,"width" :"15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (response) {
                    if (response.success) {
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message)
                    }
                }
            });
        }
    })
}