/// <reference path="../Global/Global.js" />

var LocalLayout = {

    Init: function () {
        this.Events.Init();
        $("#wrapper").toggleClass("toggled");
    },

    Events: {

        Init: function () {
            this.OnClickToggleBtn();
            this.OnClickPDF();
            this.OnClickBloquear();
            this.OnClickMenu();
        },

        OnClickToggleBtn: function () {
            $("#menu-toggle-btn").click(function (e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });
        },

        OnClickPDF: function () {
            $('#js-a-pdf').off('click');
            $('#js-a-pdf').on('click', function () {
                window.location.href = GS_Path.GetUrl("/Menu/ManualOperacaoGSMPDF");
            });
        },

        OnClickBloquear: function () {
            $('#js-btn-bloquear').off('click');
            $('#js-btn-bloquear').on('click', function () {
                window.location.href = GS_Path.GetUrl('/Login/Desbloquear');
            });
        },

        OnClickMenu: function () {
            $('.js-li-fa').off('click');
            $('.js-li-fa').on('click', function () {
                if ($(this).find('.js-span-fa').hasClass('fa-chevron-right')) {
                    $(this).find('.js-span-fa').removeClass('fa-chevron-right').addClass('fa-chevron-down');
                }
                else {
                    $(this).find('.js-span-fa').removeClass('fa-chevron-down').addClass('fa-chevron-right');
                }
            });
        }
    }
}