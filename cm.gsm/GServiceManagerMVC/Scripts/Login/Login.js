/// <reference path="../Global/Global.js" />

var LocalAPI = {

    Init: function () {
        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.OnClickLogin();
            this.OnClickEsqueceuSenha();
            this.OnEnterLogin();
        },

        OnClickLogin: function () {
            $('#btn-login').off('click');
            $('#btn-login').on('click', function () {
                LocalAPI.Methods.Enviar();
            });
        },

        OnClickEsqueceuSenha: function () {
            $('#btn-reset').off('click');
            $('#btn-reset').on('click', function () {
                $.loader.open({ title: 'Enviando email de recuperação de senha ...', fontColor: 'black', });

                if ($('#txt-login').val() != '') {
                    GS_Ajax.Ajax(GS_Path.GetUrl('/Login/EsqueceuSenha'), 'POST', 'json', $('#txt-login').val(), function (resultado) {
                        if (resultado) {
                            GS_Alert.Simples('Verifique seu e-mail!');
                        }
                        else {
                            GS_Alert.Simples('Login não encontrado!');
                        }
                        $.loader.close();
                    });
                }
                else {
                    GS_Alert.Simples('O campo Login é obrigatório!');
                    $.loader.close();
                }
            });
        },

        OnEnterLogin: function () {
            $('#txt-login, #txt-senha').keypress(function (e) {
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
            var login = $('#txt-login').val();
            var senha = $('#txt-senha').val();

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