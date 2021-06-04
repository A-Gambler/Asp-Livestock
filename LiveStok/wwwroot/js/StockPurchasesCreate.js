function MarketBuySummaryTotals() {

    var ListMarketBuy = [];
    var CodeDesc = [];

    for (i = 0; i < $('#MarketBuy > tr').length; i++) {
        if ($('#MarketBuys_' + i + '__CodeDesc').val() !== "")
            CodeDesc[i] = SelectAnmial($('#MarketBuys_' + i + '__CodeDesc').val());
    }

    var Desc = CodeDesc.filter(function (item, index, array) {
        return array.indexOf(item) === index;
    });

    for (y = 0; y < Desc.length; y++) {
        var MarketBuy = new Object();
        MarketBuy.AgentID = "";
        MarketBuy.SumNumber = 0;
        MarketBuy.AvPrice = 0;
        MarketBuy.CodeDesc = "";
        MarketBuy.Cont = 0;

        for (i = 0; i < $('#MarketBuy > tr').length; i++) {

            if (Desc[y] === SelectAnmial($('#MarketBuys_' + i + '__CodeDesc').val())) {

                MarketBuy.AgentID = $('#MarketBuys_' + i + '__AgentID').val();
                MarketBuy.SumNumber = MarketBuy.SumNumber + parseInt($('#MarketBuys_' + i + '__Number').val());
                MarketBuy.AvPrice = parseFloat(MarketBuy.AvPrice + parseFloat($('#MarketBuys_' + i + '__Price').val()));
                MarketBuy.CodeDesc = $('#MarketBuys_' + i + '__CodeDesc').val();
                MarketBuy.Cont = MarketBuy.Cont + 1;
            }
        }
        ListMarketBuy[y] = MarketBuy;
    }

    if (ListMarketBuy.length > 0) {
        var thead = '<table class="table table-bordered" style="background-color: rgba(243, 228, 193, 0.71)">' +
            '<thead>' +
            '<tr>' +
            '<th class="text-center" style="width:150px">' +
            'TYPE' +
            '</th>' +
            '<th class="text-center">' +
            'Vendor' +
            '</th>' +
            '<th class="text-center">' +
            'NUMBER' +
            '</th>' +
            '<th class="text-center">' +
            'Av. PRICE' +
            '</th>' +
            '<th class="text-center">' +
            'FREIGHT' +
            '</th>' +
            '<th class="text-center">' +
            'Av. SKIN' +
            '</th>' +
            '<th class="text-center">' +
            'Av. WEIGHT' +
            '</th>' +
            '<th class="text-center">' +
            'COST per/kg' +
            '</th>' +
            '</tr>' +
            '</thead><tbody>';
        var tbody = '';
        for (i = 0; i < ListMarketBuy.length; i++) {
            tbody += '<tr id="tr' + i + '">' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__TypeOfAnimal_Name" name="StockPurchase.MarketBuySummaries[' + i + '].Name" value="' + SelectAnmial(ListMarketBuy[i].CodeDesc) + '" readonly>' +
                '<input type="hidden" id="StockPurchase_MarketBuySummaries_' + i + '__TypeOfAnimalID" name="MarketBuySummaries[' + i + '].TypeOfAnimalID" value="Animal" value="pruebaid"></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__Description" name="StockPurchase.MarketBuySummaries[' + i + '].Description" value="' + ListMarketBuy[i].CodeDesc + '" readonly></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__Number" name="StockPurchase.MarketBuySummaries[' + i + '].Number" value="' + ListMarketBuy[i].SumNumber + '" readonly></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__AvPrice" name="StockPurchase.MarketBuySummaries[' + i + '].AvPrice" value="' + ListMarketBuy[i].AvPrice / ListMarketBuy[i].Cont + '" readonly></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__Freight" name="StockPurchase.MarketBuySummaries[' + i + '].Freight" onchange="CostXKg(' + i + ')" value="0"></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__Skin" name="StockPurchase.MarketBuySummaries[' + i + '].Skin" onchange="CostXKg(' + i + ')"  value="0"></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__AvWeight" name="StockPurchase.MarketBuySummaries[' + i + '].AvWeight" onchange="CostXKg(' + i + ')" value="0"></td>' +
                '<td><input type="text" class="form-control" id="StockPurchase_MarketBuySummaries_' + i + '__CostXKg" name="StockPurchase.MarketBuySummaries[' + i + '].CostXKg" value="0" readonly></td>' +
                '</tr>';
        }

        html = thead + tbody + '</tbody></table>';
        $('#DivMakerBuySummaryTotals').empty();
        $('#DivMakerBuySummaryTotals').append(html);
    }
}

function CostXKg(i) {

    var AvPrice = $('#StockPurchase_MarketBuySummaries_' + i + '__AvPrice').val();
    var Freight = $('#StockPurchase_MarketBuySummaries_' + i + '__Freight').val();
    var Skin = $('#StockPurchase_MarketBuySummaries_' + i + '__Skin').val();
    var AvWeight = $('#StockPurchase_MarketBuySummaries_' + i + '__AvWeight').val();

    if (AvWeight > 0) {
        var total = ((parseFloat(AvPrice) + parseFloat(Freight) - parseFloat(Skin)) / parseFloat(AvWeight)).toFixed(2);
        $('#StockPurchase_MarketBuySummaries_' + i + '__CostXKg').val(total);
    }
}

function SelectType(Id) {
    $('.nav-tabs a[href="#' + Id + '"]').tab('show');

    $("#TypeOfAnimalID").removeAttr("style");
    $("#Number").removeAttr("style");
    $("#Price").removeAttr("style");
    $("#Freight").removeAttr("style");
    //$("#EstimatedWeight").removeAttr("style");

    //$("#TypeOfAnimalID").css("display", "none");
    //$("#Number").css("display", "none");
    //$("#Price").css("display", "none");
    //$("#Freight").css("display", "none");
    //$("#EstimatedWeight").css("display", "none");

    switch (Id) {
        case "29dec9c6-c2c3-4603-9c37-3256ab99215a"://Market Buy
            $("#TypeOfAnimalID").css("display", "none");
            $("#Number").css("display", "none");
            $("#Price").css("display", "none");
            $("#Freight").css("display", "none");
            //$("#EstimatedWeight").css("display", "none");
            break;
        case "d180ff28-1321-467a-a0de-d7955d463762"://Private Buy
            $("#Number").css("display", "none");
            $("#Price").css("display", "none");
            $("#Freight").css("display", "none");
            //$("#EstimatedWeight").css("display", "none");
            break;
        case "196d17bd-5320-47da-9d44-57ff705c18b9"://Hooks Buy
            $("#Price").css("display", "none");
            $("#Freight").css("display", "none");
            break;
        default:


        break;

    }
}

function SelectAnmial(CodeDesc) {

    var TypeOfAnimal = "";

    switch (CodeDesc) {
        case "1":
            TypeOfAnimal = "MUTTON 1";
            break;
        case "2":
            TypeOfAnimal = "MUTTON 2";
            break;
        case "5":
            TypeOfAnimal = "MUTTON 5";
            break;
        case "6":
            TypeOfAnimal = "MUTTON 6";
            break;
        case "3":
            TypeOfAnimal = "2 TOOTHS";
            break;
        case "8":
            TypeOfAnimal = "LAMBS";
            break;
        case "l":
        case "L":
            TypeOfAnimal = "LAMBS";
            break;
        case "r":
        case "R":
            TypeOfAnimal = "RAMS";
            break;
    }

    return TypeOfAnimal;
}

function FillSummaryTotals() {


    var i;
    var ListPricePerHeadBuy = [];

    for (i = 0; i < $('#PricePerHeadBuy > tr').length; i++) {
        var PricePerHeadBuy = new Object();

        PricePerHeadBuy.TypeOfAnimalID = $('#PricePerHeadBuys_' + i + '__TypeOfAnimalID').val();
        PricePerHeadBuy.HeadsBought = $('#PricePerHeadBuys_' + i + '__HeadsBought').val();
        PricePerHeadBuy.Price = $('#PricePerHeadBuys_' + i + '__Price').val();
        PricePerHeadBuy.Skin = $('#PricePerHeadBuys_' + i + '__Skin').val();
        PricePerHeadBuy.Weight = $('#PricePerHeadBuys_' + i + '__Weight').val();
        PricePerHeadBuy.Freight = $('#PricePerHeadBuys_' + i + '__Freight').val();

        if (PricePerHeadBuy.HeadsBought !== ""
            && PricePerHeadBuy.Price !== ""
            && PricePerHeadBuy.Skin !== ""
            && PricePerHeadBuy.Weight !== ""
            && PricePerHeadBuy.Freight !== "") {
            ListPricePerHeadBuy[i] = PricePerHeadBuy;
        }
    }

    if (ListPricePerHeadBuy.length >= 1) {
        $.ajax({
            type: "POST",
            url: "/StockPurchasesAPI/SummaryTotals",
            data: JSON.stringify(ListPricePerHeadBuy),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $('#SummaryTotals').empty();
                $('#SummaryTotals').append(response);
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
}