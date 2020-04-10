jQuery(function ($) {
    $('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR"
    });

    $('.datepicker').datepicker();
    $('.datepicker').datepicker('update');

    $('.mask-date').mask("99/99/9999");

    $(".mask-number").keypress(function (e) {
        if (String.fromCharCode(e.keyCode).match(/[^0-9]/g))
            return false;
    });
});

var LocalMask = {
    Init: function () {
        $('.datepicker').datepicker({
            format: "dd/mm/yyyy",
            language: "pt-BR"
        });

        $('.datepicker').datepicker();
        $('.datepicker').datepicker('update');

        $('.mask-date').mask("99/99/9999");

        $(".mask-number").keypress(function (e) {
            if (String.fromCharCode(e.keyCode).match(/[^0-9]/g))
                return false;
        });
    }
}