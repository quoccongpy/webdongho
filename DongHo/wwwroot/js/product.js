var dataThable;
$(document).ready(function () {
    loadData();
});

function loadData() {
    dataThable = $('#tblData').DataTable({
        "ajax": {
            "url" : "/admin/product/getall"
        },
        "columns": [
            {"data" : "name","width" :"20%"},
            {"data" : "price","width" :"20%"},
            {"data" : "category.name","width" :"20%"},
            {"data" : "brand.name","width" :"20%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <a class="btn btn-primary" href="/Admin/Product/Update?id=${data}">
                       <i class="bi bi-pencil"></i>
                    </a>
                    <a class="btn btn-danger" onClick=Delete('/Admin/Product/Delete/${data}')>
                       <i class="bi bi-trash"></i>
                    </a>
                           `
                },

                "width": "20%"
            },
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