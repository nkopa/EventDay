function rokPrzestepny(rok) {
    return ((rok % 4 == 0) && ((rok % 100 != 0) || (rok % 400 == 0)));
}
function wyświetlKalendarz(r, m, d, el) {
    var data = new Date(r, m, d);
    if (data == "Invalid date") data = new Date();

    var rok = data.getFullYear();
    var miesiac = data.getMonth() + 1;
    var dzienTygodnia = data.getDay();
    var dzienMiesiaca = data.getDate();

    var tempDate = new Date(rok, miesiac - 1, 1);
    var pierwszyDzienMiesiaca = tempDate.getDay();

    if (dzienTygodnia == 0) dzienTygodnia = 7;
    if (pierwszyDzienMiesiaca == 0) pierwszyDzienMiesiaca = 7;


    var monthNames = new Array(
    "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
    "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
    );
    var monthDays = new Array(31, rokPrzestepny(rok) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    var shortDayNames = new Array("Nd", "Pn", "Wt", "Śr", "Cz", "Pt", "So");

    nazwaMiesiaca = monthNames[miesiac - 1];
    dniWMiesiacu = monthDays[miesiac - 1];

    var str = "";

    str += "<table>";
    str += "<tr>";
    str += "<th class='tdNaglowek' colspan='7'>";
    str += "<span class='clickable' onclick='wyświetlKalendarz(";
    str += rok + "," + (miesiac - 2) + "," + dzienMiesiaca + ",\"";
    str += el + "\");'>&lt;&lt;<\/span>&nbsp;&nbsp;";
    str += nazwaMiesiaca + " " + rok + "&nbsp;&nbsp;";
    str += "<span class='clickable' onclick='wyświetlKalendarz(";
    str += rok + "," + (miesiac) + "," + dzienMiesiaca + ",\"";
    str += el + "\");'>&gt;&gt;<\/span>";
    str += "<\/th>";
    str += "</tr>";

    var j = dniWMiesiacu + pierwszyDzienMiesiaca - 1;

    for (var i = 0; i < j; i++) {
        if (i < pierwszyDzienMiesiaca - 1) {
            continue;
        }

        str += "<tr>";

        var klasa = "tdDzien";
        var dzien = i - pierwszyDzienMiesiaca + 2;
        var name;

        name = shortDayNames[(i + 1) % 7];

        if (dzien == dzienMiesiaca &&
             (new Date()).toDateString() == data.toDateString()) {
            klasa = "tdBiezacyDzien";
        }

        //str += "<td class='" + klasa + "' onclick='ustawDate(";
        //str += rok + "," + miesiac + "," + dzien + ");'>";
        str += "<td class='" + klasa + "'>";
        str += dzien;
        str += "</br>";
        str += name;
        str += "<\/td>";

        str += "<td id=\"" + rok + "," + miesiac + "," + dzien + "\">";
        str += "<\/td>";

    }
    str += "<\/tr><\/table>";


    var el = document.getElementById(el);
    if (el) el.innerHTML = str;

    document.getElementById('2015,2,3').innerHTML = "Hello Day.";

}


function ustawDate(r, m, d) {
    var tfData = document.getElementById('tfData');
    if (tfData) {
        m = m < 10 ? "0" + m : "" + m;
        d = d < 10 ? "0" + d : "" + d;
        tfData.value = r + "/" + m + "/" + d;
    }
}

//  <div>
//    Podaj datę (RRRR/MM/DD):<br />
//    <input type="text" id="tfData" />
//  </div>

