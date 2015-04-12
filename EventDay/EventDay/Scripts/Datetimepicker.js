﻿//funkcje wykorzystują parametr name:
//datetimerange : data i godzina od do
//datetime      : data i godzina
//daterange     : data od do
//date          : data
//timerange     : godzina od do (do poprawy)
//time          : godzina       (do poprawy)


$(function () {

    var now = new moment();
            
    var HBR = document.getElementById('HourBeginRegistration');
    if (HBR.hasAttribute('onclick') && HBR.value == "") HBR.value = now.format("HH:mm DD/MM/YYYY - HH:mm DD/MM/YYYY");

    var HB = document.getElementById('HourBegin');
    if (HB.hasAttribute('onclick') && HB.value == "") HB.value = now.format("HH:mm DD/MM/YYYY - HH:mm DD/MM/YYYY");

});

$(function () {
    $('[onclick="datetimerange()"]').daterangepicker({
        timePicker: true,
        format: 'HH:mm DD/MM/YYYY',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        startDate: moment(),
        endDate: moment(),
        showWeekNumbers: false,
        timePicker12Hour: false,
        showDropdowns: true,

        locale: {
            applyLabel: 'Ok',
            cancelLabel: 'Anuluj',
            fromLabel: 'start',
            toLabel: 'koniec',
            //customRangeLabel: 'Custom',
            daysOfWeek: ['Ndz', 'Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'Sob'],
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            firstDay: 1
        }
    });
});

$(function () {
    $('[onclick="datetime()"]').daterangepicker({
        timePicker: true,
        format: 'HH:mm DD/MM/YYYY',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        startDate: moment(),
        endDate: moment(),
        showWeekNumbers: false,
        timePicker12Hour: false,
        showDropdowns: true,
        singleDatePicker: true,

        locale: {
            applyLabel: 'Ok',
            cancelLabel: 'Anuluj',
            fromLabel: 'start',
            toLabel: 'koniec',
            //customRangeLabel: 'Custom',
            daysOfWeek: ['Ndz', 'Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'Sob'],
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            firstDay: 1
        }
    });
});

$(function () {
    $('[onclick="daterange()"]').daterangepicker({
        timePicker: false,
        format: 'DD/MM/YYYY',
        startDate: moment(),
        endDate: moment(),
        showWeekNumbers: false,
        showDropdowns: true,

        locale: {
            applyLabel: 'Ok',
            cancelLabel: 'Anuluj',
            fromLabel: 'start',
            toLabel: 'koniec',
            //customRangeLabel: 'Custom',
            daysOfWeek: ['Ndz', 'Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'Sob'],
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            firstDay: 1
        }
    });
});

$(function () {
    $('[onclick="date()"]').daterangepicker({
        timePicker: false,
        format: 'DD/MM/YYYY',
        startDate: moment(),
        showWeekNumbers: false,
        showDropdowns: true,
        singleDatePicker: true,

        locale: {
            daysOfWeek: ['Ndz', 'Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'Sob'],
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            firstDay: 1
        }
    });
});

$(function () {
    $('[onclick="timerange()"]').daterangepicker({
        timePicker: true,
        format: 'HH:mm',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        timePicker12Hour: false,
        showDropdowns: true,
        singleDatePicker: false
    });
});

$(function () {
    $('[onclick="time()"]').daterangepicker({
        timePicker: true,
        format: 'HH:mm',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        timePicker12Hour: false,
        showDropdowns: true,
        singleDatePicker: true
    });
});
