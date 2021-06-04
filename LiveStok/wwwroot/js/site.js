// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function RefreshStockPurchaseGrid(SchValues, CurrentPage) {
    $.ajax({
        type: "POST",
        url: "/StockPurchasesAPI/RefreshStockPurchaseGrid?searchString=" + SchValues +"&pageNumber=" + CurrentPage,
        data: SchValues,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log("pase por aqui" + response);
            $('#ListStockPurchaseBody').empty();
            $('#ListStockPurchaseBody').append(response);

        },
        failure: function (response) {
            alert(response);
        }
    });
}