/* Ruft die aktuellen Einstellungen des Dashboards von der API ab */
function getDashboardData() {
    var pageURL = window.location.href;
    var url = '/api/dashboard/' + pageURL.substr(pageURL.lastIndexOf('/') + 1);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: url,
            success: function (result) {
                load(result);
            }
        });
}

/* Trägt die aktuellen Einstellungen in die Ansicht ein */
function load(data) {
    document.getElementById("name").value = data.name;
    document.getElementById("title").value = data.title;
    var i;
    for (i = 0; i < data.cells.length; i++) {
        document.getElementById(data.cells[i]).checked = true;
    }
}

/* Überprüft, ob das Feld Name leer ist */
$("#name").on("keyup", function () {
    if ($('#title').val().length == 0 || $('#name').val().length == 0) {
        $(".input-warning").css("display", "inline");
        $("#save").css("opacity", "0.5");
        $("#save").attr("disabled", true);
    } else {
        $(".input-warning").css("display", "none");
        $("#save").css("opacity", "1");
        $("#save").attr("disabled", false);
    }
});

/* Überprüft, ob das Feld Titel leer ist */
$("#title").on("keyup", function () {
    if ($('#title').val().length == 0 || $('#name').val().length == 0) {
        $(".input-warning").css("display", "inline");
        $("#save").css("opacity", "0.5");
        $("#save").attr("disabled", true);
    } else {
        $("#save").css("opacity", "1");
        $(".input-warning").css("display", "none");
        $("#save").attr("disabled", false);
    }
});

/* Suche nach Produktionszellen */
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

getDashboardData();
