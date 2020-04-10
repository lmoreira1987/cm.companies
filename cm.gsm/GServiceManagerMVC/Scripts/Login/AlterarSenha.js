/// <reference path="../Global/Global.js" />

var LocalAPI = {

    Init: function () {
        $('#js-div-alterar-senha').show();
        $('.close').remove();

        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.OnClickConfirmar();
            this.OnClickCancelar()
        },

        OnClickConfirmar: function () {
            $('#js-btn-confirmar').off('click');
            $('#js-btn-confirmar').on('click', function () {
                var objeto = new Array();
                objeto.push($('#js-txt-novasenha1').val());
                objeto.push($('#js-txt-novasenha2').val());
                objeto.push($('#js-txt-login').val());
                objeto.push($('#js-txt-senha').val());

                GS_Ajax.Ajax(GS_Path.GetUrl('/Login/SalvarNovaSenha'), 'POST', 'json', objeto, function (resultado) {
                    if (resultado == 'ok') {
                        GS_Alert.SimplesCallBack("Senha alterada com sucesso!", function () {
                            window.location.href = GS_Path.GetUrl('/Login');
                        });
                    }
                    else {
                        GS_Alert.Simples(resultado);
                    }
                });
            });
        },

        OnClickCancelar: function () {
            $('#js-btn-cancelar').off('click');
            $('#js-btn-cancelar').on('click', function () {
                window.location.href = GS_Path.GetUrl('/Login');
            });
        }
    }
}