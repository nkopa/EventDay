//funkcje wykorzystują parametr name:
//datetimerange : data i godzina od do
//datetime      : data i godzina
//daterange     : data od do
//date          : data
//timerange     : godzina od do (do poprawy)
//time          : godzina       (do poprawy)


$(function () {
    $('[onclick="f4()"]').daterangepicker({
        timePicker: true,
        format: 'H:mm DD/MM/YYYY',
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
    $('[name="datetime"]').daterangepicker({
        timePicker: true,
        format: 'H:mm DD/MM/YYYY',
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
    $('input[name="daterange"]').daterangepicker({
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
    $('input[name="date"]').daterangepicker({
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
    $('input[name="timerange"]').daterangepicker({
        timePicker: true,
        format: 'H:mm',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        timePicker12Hour: false,
        showDropdowns: true,
        singleDatePicker: false
    });
});

$(function () {
    $('input[name="time"]').daterangepicker({
        timePicker: true,
        format: 'H:mm',
        timePickerIncrement: 1,
        timePickerSeconds: false,
        timePicker12Hour: false,
        showDropdowns: true,
        singleDatePicker: true
    });
});
