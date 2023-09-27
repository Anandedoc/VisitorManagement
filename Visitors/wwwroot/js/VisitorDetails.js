var dataTable;

$(document).ready(function () {
    var url = window.location.search;

    if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("cancelled")) {
        loadDataTable("cancelled");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
     
    } else {
        loadDataTable("all")
    }

})
console.log(status);
function loadDataTable(status) {
    dataTable = $('#tblData').DataTable(
        {
            "ajax": {
                "url": "/Customer/Home/GetAll?status=" + status
            },
            "columns": [
                { "data": "name", "width": "15%" },
                { "data": "toUserName", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "phone", "width": "15%" },
                { "data": "email", "width": "15%" },
                { "data": "time", "width": "15%" },
                { "data": "inTime", "width": "15%" },
                { "data": "outTime", "width": "15%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                             <div class="w-75 btn-group" role="group">
                            <a href="/Admin/VisitorDetails/Upsert?id=${data}"
                            class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Details</a>
                           
                       </div>
`
                    },
                    "width": "15%"

                }


            ]
        });
}

