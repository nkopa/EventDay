
    //budowa kalendarza
    function rokPrzestepny(rok) {
        return ((rok % 4 == 0) && ((rok % 100 != 0) || (rok % 400 == 0)));
    }
function wyświetlKalendarz(r, m, d, el, ev) {
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

    str += "<table class='table table-hover'>";
    str += "<tr>";
    str += "<th class='tdNaglowek' colspan='7'>";
    str += "<span class='clickable' onclick='wyświetlKalendarz(" + rok + "," + (miesiac - 2) + "," + dzienMiesiaca + ",\"" + el + "\" );'>";
    str += "&lt;&lt;<\/span>&nbsp;&nbsp;" + nazwaMiesiaca + " " + rok + "&nbsp;&nbsp;";

    str += "<span class='clickable' onclick='wyświetlKalendarz(" + rok + "," + (miesiac) + "," + dzienMiesiaca + ",\"" + el + "\" );'>&gt;&gt;<\/span>";
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

        str += "<td class='" + klasa + "'>";
        str += dzien;
        str += "</br>";
        str += name;
        str += "<\/td>";

        str += "<td id=\"" + rok + "-" + miesiac + "-" + dzien + "\">";

        str += c(events, rok, miesiac, dzien);

        str += "<\/td>";

    }
    str += "<\/tr><\/table>";

    document.getElementById(el).innerHTML = str;

}

//zawartosc kalendarza
function c(ev, rok, miesiac, dzien) {
    var text = "";
    // var data = rok + "-" + miesiac + "-" + dzien + " ";

    var m = miesiac < 10 ? "0" + miesiac : "" + miesiac;
    var d = dzien < 10 ? "0" + dzien : "" + dzien;
    //var data = m + "-" + d + "-" + rok;
    var data = m + "/" + d + "/" + rok;

    for (i = 0; i < ev.length; i++) {
        if (ev[i].DateBegin.indexOf(data) === 0) {             
               
            if (ev[i].Username === usrName) {
                text += '<div class="event eventOwner">';
            }
            else {
                text += '<div class="event eventMember">';
            }
                
            text += '<a href="Events/Details/' + ev[i].EventId + '">';
            text += '<div>';
            text += '<img id="Image" src="' + image(ev[i].ProfileImage) + '"/>'; 
            text += '<p id="Title">' + ev[i].Title + '</p>';
            text += '<p id="Time">' + hour(ev[i].HourBegin) + '</p>';
            text += '<p id="Description">' + ev[i].Description + '</p>';
            text += '</div>';

            text += '</a>';

            text += '</div>';
        }
    }
    return text;
}

function image(ProfileImage) {
    if (ProfileImage !== null && ProfileImage.length > 0) {
        var url = "/Content/Uploads/" + ProfileImage;
        return url;
    }
    else return "/Content/no-img.png";
}

function hour(myDate) {
    var getdate = new Date(myDate);
    var minutes = getdate.getMinutes() < 10 ? "0" + getdate.getMinutes() : "" + getdate.getMinutes();
    var gethour = getdate.getHours() + ":" + minutes;
    return gethour;
}
