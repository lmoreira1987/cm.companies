/// <reference path="../Global/Global.js" />

var LocalAPI = {

    Init: function () {
        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.OnSelectRelatorio();
        },

        OnSelectRelatorio: function () {
            $('#js-slc-relatorios').off('change');
            $('#js-slc-relatorios').on('change', function () {
                
                var id = $(this).val();
                var url = '';

                if (id == '1') {
                    url = '/Relatorio/RelatorioSemanaColaborador';
                    $('#js-span-titulo').text(' Relatório de Horas / Semana');
                }
                else if (id == '2') {
                    url = '/Relatorio/RelatorioMesColaborador';
                    $('#js-span-titulo').text(' Relatório de Horas / Mês');
                }
                else if (id == '3') {
                    url = '/Relatorio/RelatorioAtividadesColaborador';
                    $('#js-span-titulo').text(' Relatório de Atividades Concluídas');
                }
                else if (id == '4') {
                    url = '/Relatorio/RelatorioAtividadesGrupoColaborador';
                    $('#js-span-titulo').text(' Relatório de Atividades Concluídas Grupo');
                }

                if (id != '0') {
                    GS_Ajax.AjaxVoid(GS_Path.GetUrl(url), 'POST', 'html', function (resultado) {
                        $('#js-div-parcial').empty().html(resultado);
                        
                        LocalAPI.Init();
                    });
                }
            });
        }
    }
}