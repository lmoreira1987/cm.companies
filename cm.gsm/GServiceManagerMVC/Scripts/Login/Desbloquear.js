/// <reference path="../Global/Global.js" />

var LocalAPI = {

    Init: function () {
        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.onClickDesbloquear();
            this.onClickNaoVoce();
            this.OnEnterDesbloquear();
        },

        onClickDesbloquear: function () {
            $('#js-btn-desbloquear').off('click');
            $('#js-btn-desbloquear').on('click', function () {
                LocalAPI.Methods.Enviar();
            });
        },

        onClickNaoVoce: function () {
            $('#js-btn-naoVoce').on('click', function () {
                window.location.href = GS_Path.GetUrl('/Login');
            });
        },

        OnEnterDesbloquear: function () {
            $('#js-txt-senha').keypress(function (e) {
                var code = e.keyCode || e.which;

                if (code === 13) {
                    e.preventDefault();

                    LocalAPI.Methods.Enviar();
                }
            })
        }
    },

    Methods: {
        Enviar: function () {
            var login = $('#js-h3-login').attr('js-login');
            var senha = $('#js-txt-senha').val();

            $.loader.open();

            $.ajax({
                url: GS_Path.GetUrl('/Login/Logar'),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify({ 'login': login, 'senha': senha })
            })
            .done(function (resultado) {
                if (resultado) {
                    window.location.href = GS_Path.GetUrl('/Dashboard');
                }
                else {
                    $.loader.close();
                    GS_Alert.Simples('Login ou Senha Inválidos!');
                }
            });
        }
    }
}