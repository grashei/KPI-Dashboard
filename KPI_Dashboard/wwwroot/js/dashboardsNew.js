/* Überprüft, ob das Feld Name leer ist */
$("#name").on("keyup", function () {
    if ($('#title').val().length == 0 || $('#name').val().length == 0) {
        $(".input-warning").css("display", "inline");
        $("#add").css("opacity", "0.5");
        $("#add").attr("disabled", true);
    } else {
        $(".input-warning").css("display", "none");
        $("#add").css("opacity", "1");
        $("#add").attr("disabled", false);
    }
});

/* Überprüft, ob das Feld Titel leer ist */
$("#title").on("keyup", function () {
    if ($('#title').val().length == 0 || $('#name').val().length == 0) {
        $(".input-warning").css("display", "inline");
        $("#add").css("opacity", "0.5");
        $("#add").attr("disabled", true);
    } else {
        $(".input-warning").css("display", "none");
        $("#add").css("opacity", "1");
        $("#add").attr("disabled", false);
    }
});

/* Suche nach einer Produktionszelle */
$("#search").on("keyup", function () {
    var value = $(this).val().toLowerCase();

    $("table tr").each(function (index) {
        if (index !== 0) {
            $row = $(this);
            var id = $row.find("td:first").text().toLowerCase();

            if (id.indexOf(value) < 0) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

$("#add").css("opacity", "0.5");
$("#add").attr("disabled", true);
