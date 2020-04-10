var GS_Alert = {
    Simples: function (mensagem) {
        bootbox.dialog({
            message: mensagem,
            closeButton: false,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                    }
                }
            }
        });
    },
    SimplesCallBack: function (mensagem, callback) {
        bootbox.dialog({
            message: mensagem,
            closeButton: false,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        callback(true);
                    }
                }
            }
        });
    },
    Dialogo: function (mensagem, callback) {
        bootbox.dialog({
            message: mensagem,
            closeButton: false,
            buttons: {
                nao: {
                    label: "Não",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                        callback(false);
                    }
                },
                sim: {
                    label: "Sim",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        callback(true);
                    }
                }
            }
        });
    }
}

var GS_Path = {
    GetUrl: function (url) {
        var gsm = window.location.pathname;

        if (gsm.indexOf("GSM_MVC") > -1) {
            return '/GSM_MVC' + url;
        }
        else {
            return url;
        }
    }
}

var GS_Ajax = {
    Ajax: function (url, type, dataType, objeto, callback) {
        $.ajax({
            url: url,
            type: type,
            contentType: "application/json; charset=utf-8",
            dataType: dataType,
            data: JSON.stringify({ 'objeto': objeto })
        })
        .done(function (resultado) {
            callback(resultado);
        })
        .fail(function () {
            GS_Alert.SimplesCallBack("Sua sessão expirou! Você será redirecionado!", function () {
                window.location.href = GS_Path.GetUrl('/Login/Desbloquear');
            });
        });
    },

    AjaxVoid: function (url, type, dataType, callback) {
        $.ajax({
            url: url,
            type: type,
            contentType: "application/json; charset=utf-8",
            dataType: dataType
        })
        .done(function (resultado) {
            callback(resultado);
        })
        .fail(function () {
            GS_Alert.SimplesCallBack("Sua sessão expirou! Você será redirecionado!", function () {
                window.location.href = GS_Path.GetUrl('/Login/Desbloquear');
            });
        });
    }
}