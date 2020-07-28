/* Aktualisiert regelmäßig die angezeigten Leistungsdaten */
$(document).ready(function () {
    getKpi();
    setInterval(getKpi, 30000);
});

/* Ruft die aktuellen Leistungsdaten von der API ab */
function getKpi() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/api/Kpi',
        success: function (result) {
            load(result);
        }
    });
}

/* Aktualisiert die angezeigten Daten der Ansicht */
function load(data) {
    var yellow = "#ffed42";
    var green = "green";
    var red = "#dd0000";
    var styleValues = "transition: background 1s, color 1s; opacity:0.85; text-align:center; width:70%; margin: 0 15%; padding-top:5px; padding-bottom:5px; font-size: 3.3em;";
    var styleValuesOee = "transition: background 1s, color 1s; opacity:0.85; text-align:center; width:80%; margin: 0 15%; padding-top:5px; padding-bottom:5px; font-size: 4em;";
    var styleText = "transition: none; margin: 0 15%; font-size: 4em; opacity:0.75; text-align: center; white-space: nowrap;";
    var performance, quality, availability, oee;
    var i, j = 1;
    var changeOverRemaining;

    for (i = 0; i < data.length; i++) {
        if (document.getElementById(data[i].cellDescription) != null) {
            if (data[i].runStatus == null) {
                document.getElementById("performance" + (j)).style.cssText = styleText;
                document.getElementById("quality" + (j)).style.cssText = "display: none";
                document.getElementById("availability" + (j)).style.cssText = "display: none";
                document.getElementById("oee" + (j)).style.cssText = "display: none";
                document.getElementById("performance" + (j)).innerHTML = "Aus";
                document.getElementById("performance" + (j)).style.color = "black";
            } else if (data[i].runStatus == "CHANGEOVER") {
                document.getElementById("performance" + (j)).style.cssText = styleText;
                document.getElementById("quality" + (j)).style.cssText = "display: none";
                document.getElementById("availability" + (j)).style.cssText = "display: none";
                document.getElementById("oee" + (j)).style.cssText = "display: none";
                changeOverRemaining = data[i].changeOverTargetDuration - data[i].changeOverDuration;
                document.getElementById("performance" + (j)).innerHTML = "Rüsten: " + changeOverRemaining + " Minuten";
                if (changeOverRemaining >= 0) {
                    document.getElementById("performance" + (j)).style.color = "black";
                } else {
                    document.getElementById("performance" + (j)).style.color = "red";
                }
            } else {
                performance = data[i].rate.toFixed(1);
                document.getElementById("performance" + (j) + "-col").classList.add("col");
                document.getElementById("performance" + (j)).style.cssText = styleValues;
                if (performance > 80) {
                    document.getElementById("performance" + (j)).style.backgroundColor = green;
                    document.getElementById("performance" + (j)).style.color = "white";
                } else if (performance >= 10) {
                    document.getElementById("performance" + (j)).style.backgroundColor = yellow;
                    document.getElementById("performance" + (j)).style.color = "black";
                } else {
                    document.getElementById("performance" + (j)).style.backgroundColor = red;
                    document.getElementById("performance" + (j)).style.color = "black";
                }
                document.getElementById("performance" + (j)).innerHTML = performance + "%";

                quality = data[i].yield.toFixed(1);
                document.getElementById("quality" + (j)).style.cssText = styleValues;
                if (quality > 95) {
                    document.getElementById("quality" + (j)).style.backgroundColor = green;
                    document.getElementById("quality" + (j)).style.color = "white";
                } else if (quality >= 10) {
                    document.getElementById("quality" + (j)).style.backgroundColor = yellow;
                    document.getElementById("quality" + (j)).style.color = "black";
                } else {
                    document.getElementById("quality" + (j)).style.backgroundColor = red;
                    document.getElementById("quality" + (j)).style.color = "black";
                }
                document.getElementById("quality" + (j)).innerHTML = quality + "%";

                availability = data[i].util.toFixed(1);
                document.getElementById("availability" + (j)).style.cssText = styleValues;
                if (availability > 70) {
                    document.getElementById("availability" + (j)).style.backgroundColor = green;
                    document.getElementById("availability" + (j)).style.color = "white";
                } else if (availability >= 10) {
                    document.getElementById("availability" + (j)).style.backgroundColor = yellow;
                    document.getElementById("availability" + (j)).style.color = "black";
                } else {
                    document.getElementById("availability" + (j)).style.backgroundColor = red;
                    document.getElementById("availability" + (j)).style.color = "black";
                }
                document.getElementById("availability" + (j)).innerHTML = availability + "%";

                oee = data[i].oee.toFixed(1);
                document.getElementById("oee" + (j)).style.cssText = styleValuesOee;
                if (oee > 54) {
                    document.getElementById("oee" + (j)).style.backgroundColor = green;
                    document.getElementById("oee" + (j)).style.color = "white";
                } else if (oee >= 10) {
                    document.getElementById("oee" + (j)).style.backgroundColor = yellow;
                    document.getElementById("oee" + (j)).style.color = "black";
                } else {
                    document.getElementById("oee" + (j)).style.backgroundColor = red;
                    document.getElementById("oee" + (j)).style.color = "black";
                }
                document.getElementById("oee" + (j)).innerHTML = oee + "%";
            }
            j++;
        }
    }
}
