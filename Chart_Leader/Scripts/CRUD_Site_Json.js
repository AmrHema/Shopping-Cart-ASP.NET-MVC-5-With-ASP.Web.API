

// DrawTable
//function DrawTable() {
//    $('#tblBody').empty;
//    $.ajax({
//        type: "GET",
//        url: "http://localhost:60000/api/Products",
//        dataType: "JSON",
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            $('#tblBody').empty();
//            $("#DIV").html('');
//            var DIV = '';
//            $.each(data, function (i, item) {
//                var rows = "<tr>" +
//                    "<td >" + item.Product_Name + "</td>" +
//                    "<td >" + item.Product_Price + "</td>" +
//                    "<td >" + item.Product_QTY + "</td>" +
//                    "<td >" + item.Product_Writing_Date + "</td>" +
//                    "<td> <button  class='btn btn-outline-primary' id=" + item.Product_id + " onClick='AddEditProduct(" + item.Product_id + ")' data-togle='modal' data-target='#myModal' ><i class='fa fa-edit'></i>  Edit</button></td>" +
//                    "<td> <button  class='btn btn-outline-danger' id=" + item.Product_id + " onClick='Delete(" + item.Product_id + ")' data-togle='modal' data-target='#myModal' ><i class='fas fa-trash-restore'></i>  Delete</button></td>"

//                "</tr>";
//                $('#tblBody').append(rows);
//            }); //End of foreach Loop

//        }, //End of AJAX Success function

//        failure: function (data) {
//            alert(data.responseText);
//        }, //End of AJAX failure function
//        error: function (data) {
//            alert(data.responseText);
//        } //End of AJAX error function




//    });
//};



//Start Function Search




// Delete By ID
function Delete(id) {
    alertify.confirm('Web Api Leader Operations', 'Are You Sure to Delete this Record ?', function () {
        $.ajax({
            type: 'Delete',
            url: 'http://localhost:60000/api/Products/' + id,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alertify.success(data);
                table.ajax.reload();
                  // DrawTable();
            },
            error: function (error) {
                alert(error);
            }
        });
    }, null);
}




// AddEditProduct
var AddEditProduct = function (id) {
    var url = "/Admin/Products_Api_Jquery/ProductPartial?Product_id=" + id;
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $(".modal-body").html(data);
            $("#myModal").modal("show");
        },
        error: function () {
            alert("Error")
        },

    })
    return false;
}


// Reset Modal
var ResetModal = function () {
    $('#Product_Name').val('');
    $('#Product_Price').val('');
    $('#Product_QTY').val('');
    $('#Product_Writing_Date').val('');
    $('#Product_Description').val('');
    $('#Product_Image').val('');
}


//Add Product
var Add = function () {
    var _product = {
        Product_id: 0,
        Cat_id: $('#Cat_id').val(),
        Product_Name: $('#Product_Name').val(),
        Product_Price: $('#Product_Price').val(),
        Product_QTY: $('#Product_QTY').val(),
        Product_Writing_Date: $('#Product_Writing_Date').val(),
        Product_Description: $('#Product_Description').val(),
        Product_Image: $('#Product_Image').val(),
    };


    $.ajax({
        type: 'POST',
        url: 'http://localhost:60000/api/Products/product',
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(_product),
        success: function (data) {
            alertify.success(data);
            //     DrawTable();
            table.ajax.reload();
            ResetModal();
            $("#myModal").modal("hide");

        },
        error: function (error) {
            alert(error);
        }
    });
};


//Update Product
var Update = function () {
    var product = {
        Product_id: $('#Product_id').val(),
        Cat_id: $('#Cat_id').val(),
        Product_Name: $('#Product_Name').val(),
        Product_Price: $('#Product_Price').val(),
        Product_QTY: $('#Product_QTY').val(),
        Product_Writing_Date: $('#Product_Writing_Date').val(),
        Product_Description: $('#Product_Description').val(),
        Product_Image: $('#Product_Image').val(),
    };
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:60000/api/Products/' + product.Product_id,
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(product),
        success: function (data) {

            alertify.success(data);
           // DrawTable();
            table.ajax.reload();
            $("#myModal").modal("hide");
        },
        error: function (error) {
            alert(error);
        }
    });
};
