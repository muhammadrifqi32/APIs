table = $("#tbDepartment").DataTable({
    "paging": true,
    "responsive": true,
    "lengthChange": true,
    "searching": true,
    "ordering": true,
    "info": true,
    "autoWidth": false,
    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
    "ajax": {
        url: "https://localhost:7257/api/Departments",
        type: "GET",
        "datatype": "json",
        "dataSrc": "data",
        //success: function (result) {
        //    console.log(result)
        //}
    },
    columnDefs: [{
        "defaultContent": "-",
        "targets": "_all"
    }],
    "columns": [
        { "data": "dept_ID" },
        { "data": "dept_Initial" },
        { "data": "dept_Name" },
        {
            "render": function (data, type, row) {
                return '<button type="button" class="btn btn-sm btn-warning mr-1" data-bs-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editDepartment(\'' + row.dept_ID + '\')"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button type="button" class="btn btn-sm btn-danger" data-bs-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteDepartment(\'' + row.dept_ID + '\')"><i class="fas fa-trash"></i></button>';
            }
        }
    ]

}).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

$(document).ajaxComplete(function () {
    $('[data-bs-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

function ClearScreen() {
    $('#inputDeptId').val('');
    $('#inputDeptInitial').val('');
    $('#inputDeptName').val('');
    $('#Edit').hide();
    $('#Save').show();
}

function Save() {
    if ($('#inputDeptInitial').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Initial',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else if ($('inputDeptName').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Initial',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else {
        var department = new Object();
        department.dept_ID = $('#inputDeptId').val();
        department.dept_Initial = $('#inputDeptInitial').val();
        department.dept_Name = $('#inputDeptName').val();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7257/api/Departments',
            data: JSON.stringify(department), //must exist on Insert method
            contentType: "application/json; charset=utf-8", //must exist on Insert method
        }).then((result) => {
            if (result.status == 201 || result.status == 204 || result.status == 200) {
                Swal.fire({
                    icon: 'success',
                    title: result.message,
                });
                $("#tbDepartment").DataTable().ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Input', 'error');
                ClearScreen();
            }
        })
    }
}

function editDepartment(dept_ID) {
    $.ajax({
        url: "https://localhost:7257/api/Departments/" + dept_ID,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;
            $('#inputDeptId').val(obj.dept_ID);
            $('#inputDeptInitial').val(obj.dept_Initial);
            $('#inputDeptName').val(obj.dept_Name);
            $('#insertModal').modal('show');
            $('#Edit').show();
            $('#Save').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Edit() {
    if ($('#inputDeptInitial').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Initial',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else if ($('inputDeptName').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Initial',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else {
        var department = new Object();
        department.dept_ID = $('#inputDeptId').val();
        department.dept_Initial = $('#inputDeptInitial').val();
        department.dept_Name = $('#inputDeptName').val();
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:7257/api/Departments',
            data: JSON.stringify(department),
            contentType: "application/json; charset=utf-8",
        }).then((result) => {
            if (result.status == 201 || result.status == 204 || result.status == 200) {
                Swal.fire({
                    icon: 'success',
                    title: result.message,
                });
                $("#tbDepartment").DataTable().ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Input', 'error');
                ClearScreen();
            }
        })
    }
}

function deleteDepartment(dept_ID) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "https://localhost:7257/api/Departments/" + dept_ID,
                type: "DELETE",
                dataType: "json",
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire({
                        title: "Deleted!",
                        text: result.message,
                        icon: "success"
                    });
                    $("#tbDepartment").DataTable().ajax.reload();
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}