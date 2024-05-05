$(document).ready(function () {
    $("#tbDepartment").DataTable({
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
                    return '<a href="#" onclick="editDepartment(' + row.dept_ID + '); return false;">Edit</a> | <a href="#" onclick="deleteDepartment(' + row.dept_ID + '); return false;">Delete</a>';
                }
            }
        ]

    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
})

function ClearScreen() {
    $('#inputDeptId').val('');
    $('#inputDeptInitial').val('');
    $('#inputDeptName').val('');
}

function Save() {
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
        console.log(result);
        debugger;
        if (result >= 1) {
            alert("Data Succesfully Inserted")
        }
        else {
            alert("Failed to Add Data")
        }
    })
}

