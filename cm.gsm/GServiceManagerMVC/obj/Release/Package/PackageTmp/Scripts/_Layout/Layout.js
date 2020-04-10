var LocalAPI = {

    Init: function () {
        this.Events.Init();
        $("#wrapper").toggleClass("toggled");
    },

    Events: {

        Init: function () {
            this.OnClickToggle();
        },

        OnClickToggle: function () {
            $("#menu-toggle").click(function (e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");

                if ($("#wrapper").hasClass("toggled")) {
                    $('#menu-toggle > i').removeClass("fa-chevron--left");
                    $('#menu-toggle > i').addClass("fa-chevron-right");
                }
                else {
                    $('#menu-toggle > i').removeClass("fa-chevron-right");
                    $('#menu-toggle > i').addClass("fa-chevron-left");
                }
            });
        }
    },

    Methods: {
        
    },

    Modals: {
        
    }
}