$(document).ready(function () {

    var dataSet = GetAllUser();

    LoadDataTable();

    InitialEditDialog();

});

function GetAllUser() {

    var result = {};

    $.ajax({
        type: "GET",
        url: "/User/GetUser",
        dataType: "json",
        async: false,
        success: function (response) {
            result = response;
        }
    });

    return result;
}

function LoadDataTable() {

    var dataSet = GetAllUser();

    $("#userTable").DataTable({
        data: dataSet,
        dom: 'lBrtip',
        columns: [
            { title: "UserId", data: "UserId" },
            { title: "UserAccount", data: "UserAccount" },
            { title: "UserName", data: "UserName" },
            { title: "Email", data: "Email" },
            {
                title: "Option", data: null,
                render: function (data, type, row) {
                    console.log(row);
                    return '<button type="button" class="EditBtn btn btn-warning btn-sm" value ="' + row.UserId + '">編輯</button> ' +
                        '<button type="button" class="DelBtn btn btn-danger btn-sm" value ="' + row.UserId + '" >刪除</button>'
                }
            }
        ]
    });

    $(".EditBtn").click(function () {

        var userData = GetUserById(this.value);

        $("#UserEditId").val(userData.UserId);
        $("#UserEditAccount").val(userData.UserAccount);
        $("#UserEditName").val(userData.UserName);
        $("#EditEmail").val(userData.Email);
    });

    $(".DelBtn").click(function () {
        console.log(this.value);
        DelUser(this.value);
    });
}

function EditUser(userData) {
    $.ajax({
        type: "Post",
        url: "/User/UpdateUser",
        dataType: "json",
        data: userData,
        async: false,
        success: function (response) {
            if (response == true) {
                alert("Update Success");
            }
            else {
                alert("Update Failed");
            }
        }
    });
}

function DelUser(userId) {

    if (confirm("Delete?"))
    {
        $.ajax({
            type: "Post",
            url: "/User/DeleteUser",
            dataType: "json",
            data: { userId: userId},
            async: false,
            success: function (response) {
                if (response == true) {
                    alert("Delete Success");
                }
                else {
                    alert("Delete Failed");
                }
            }
        });

        LoadDataTable();
    }
}

function GetUserById(userId) {

    var result = {};

    $.ajax({
        type: "GET",
        url: "/User/GetUserById/" + userId,
        dataType: "json",
        async: false,
        success: function (response) {
            result = response;
        }
    });

    return result;
}

function InitialEditDialog() {
    $("#EditDialog").dialog({
        autoOpen: false
    });

    $(".EditBtn").on("click", function () {
        $("#EditDialog").dialog("open");
    });

    $("#EditCancleBtn").on("click", function () {
        $("#EditDialog").dialog("close");
    });

    $("#EditSaveBtn").on("click", function () {
        var userData = {
            UserId: $("#UserEditId").val(),
            UserAccount: $("#UserEditAccount").val(),
            UserName: $("#UserEditName").val(),
            Email: $("#EditEmail").val(),
        }
        console.log(userData);
        $("#EditDialog").dialog("close");
    });
}
