/// <reference path="../Global/Global.js" />
/// <reference path="../_JQuery/jquery-1.11.2.intellisense.js" />
/// <reference path="../_JQuery/_references.js" />

var LocalAPI = {
    ObjetoFile: new Object(),
    Files: new Array(),
    FilesSend: new Array(),
    ControleFile: 0,
    ControleFileSend: 2,
    ObjetoSend: new Object(),

    Init: function () {
        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.OnClickPDF();
            this.OnClickBloco();
            this.OnClickTr();//click da tabela minhas atividades
            this.OnClickIcon();
            this.OnClickCancelarLog();
            this.OnClickIniciar();
            this.OnClickConfirmarLog();
        },

        OnClickPDF: function () {
            $('#js-a-block-pdf').off('click');
            $('#js-a-block-pdf').on('click', function () {
                window.location.href = GS_Path.GetUrl("/Menu/ManualOperacaoGSMPDF");
            });
        },

        OnClickBloco: function () {
            $('.js-a-blocos').off('click');
            $('.js-a-blocos').on('click', function () {
                var objeto = $(this).attr('id-valor');

                GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/SetTempData/'), 'POST', 'json', objeto, function (resultado) {;
                    if (resultado == 'ok') {
                        window.location.href = GS_Path.GetUrl("/Relatorio");
                    }
                });
            });
        },

        OnClickTr: function () {
            $('.js-td-click').off('click');
            $('.js-td-click').on('click', function () {
                var Id = $(this).parent().attr('js-id-atividade');
                window.location.href = GS_Path.GetUrl('/Dashboard/AtividadeLog/') + Id;
            });
        },

        OnClickCancelarLog: function () {
            $('#js-btn-cancelar-log').off('click');
            $('#js-btn-cancelar-log').on('click', function () {

                window.location.href = GS_Path.GetUrl('/Dashboard');
            });
        },

        OnClickConfirmarLog: function () {
            $('#js-btn-confirmar-log').off('click');
            $('#js-btn-confirmar-log').on('click', function () {

                console.log('click');

                var objeto = new Object();

                objeto.atividadeId = $('#js-atividade-id').attr('js-atividade-id');
                objeto.tempoEfetivoConsumido = $('#js-txt-horas-trabalhadas').val();
                objeto.apontamento = $('#js-txtarea-parecer').val();
                objeto.logInicialStatus = "A";

                GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/logarAtividade'), 'POST', 'json', objeto, function (resultado) {
                    if (resultado) {

                        window.location.href = GS_Path.GetUrl('/Dashboard');

                    } else {

                        GS_Alert.Simples('Ocorreu um erro ao tentar salvar o log da atividade!</br>Por favor, entre em contato com a equipe técnica responsável.');

                    }
                });
            });
        },

        OnClickIcon: function () {
            $('.js-log-click').off('click');
            $('.js-log-click').on('click', function () {

                var objeto = $(this).parent().attr('js-id-atividade');

                GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/LogWorkAtividade'), 'POST', 'html', objeto, function (resultado) {
                    $('#js-div-painel-minhas-atividades').html(resultado);
                    LocalAPI.Events.OnClickCancelarLog();
                    LocalAPI.Events.OnClickCancelarLog();
                    LocalAPI.Events.OnClickConfirmarLog();
                    LocalAPI.Events.OnClickAdicionarUpload();
                    LocalAPI.Events.OnClickDeletarUpload();
                    LocalAPI.Events.OnClickFileUpload();
                    LocalAPI.Events.OnClickSalvar();


                });
            });
        },

        OnClickIniciar: function () {
            $('.js-btn-Iniciar').off('click');
            $('.js-btn-Iniciar').on('click', function () {

                var objeto = new Object();
                objeto.id = this.id;
                objeto.nome = 2;

                GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/PegarAtividadeGrupo'), 'POST', 'json', objeto, function (resultado) {
                    $('#js-div-painel-atividade').html(resultado);

                    if (resultado == true) {
                        window.location.href = GS_Path.GetUrl('/Dashboard');
                    } else {
                        GS_Alert.Simples('Ocorreu um erro ao tentar transferir a atividade!</br>Por favor, entre em contato com a equipe técnica responsável.');
                    }
                });
            });
        },

        OnClickAdicionarUpload: function () {
            $('#js-i-adicionar-upload').off('click');
            $('#js-i-adicionar-upload').on('click', function () {
                var descricao = $('#js-txt-file-descricao').val();
                var nome = $('#js-txt-file-nome').val();

                if (descricao.trim() != '' && nome.trim() != '') {
                    LocalAPI.ControleFile = LocalAPI.ControleFile + 1;

                    var objeto = new Object();
                    objeto.id = LocalAPI.ControleFile;
                    objeto.file = LocalAPI.ObjetoFile;

                    LocalAPI.Files.push(objeto);

                    GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Dashboard/GetDate'), 'POST', 'json', function (resultado) {

                        var linha = LocalAPI.Methods.CriarLinhaEvidencia(objeto.id, descricao, objeto.file.files[0].name, resultado);

                        $('#js-tbl-anexo > tbody:last').append(linha);

                        var count = 1;
                        $('#js-tbl-anexo > tbody > tr').each(function () {
                            $(this).find('td:eq(0)').text(count);
                            ++count;
                        });

                        $('#js-txt-file-descricao').val('');
                        $('#js-txt-file-nome').val('');

                        LocalAPI.Events.OnClickDeletarUpload();
                    });
                }
                else {
                    GS_Alert.Simples('Os campos Descrição e Anexo são obrigatórios!');
                }
            });
        },

        OnClickDeletarUpload: function () {
            $('.js-i-deletar-linha').off('click');
            $('.js-i-deletar-linha').on('click', function () {
                var linha = $(this).parent().parent();

                GS_Alert.Dialogo('Deseja realmente excluir?', function (resultado) {
                    if (resultado) {

                        var id = $(linha).attr('id');

                        for (var i = 0; i < LocalAPI.Files.length; i++) {
                            if (LocalAPI.Files[i].id == id)
                                LocalAPI.Files[i] = null;
                        }

                        $(linha).remove();

                        var count = 1;
                        $('#js-tbl-anexo > tbody > tr').each(function () {
                            $(this).find('td:eq(0)').text(count);
                            ++count;
                        });
                    }
                });
            });
        },

        OnClickFileUpload: function () {
            $('#js-btn-file-anexo').fileupload({
                limitMultiFileUploads: 1,
                singleFileUploads: true,
                progressInterval: 10,
                dataType: 'json',
                url: GS_Path.GetUrl("/Dashboard/FileUpload"),
                add: function (e, data) {
                    $('#js-txt-file-nome').val(data.files[0].name);
                    LocalAPI.ObjetoFile = data;
                },
                done: function (e, data) {
                    var salvarAtividade = true;

                    for (var i = 1; i < LocalAPI.FilesSend.length; i++) {
                        var objeto = LocalAPI.FilesSend[i];

                        if (objeto.id == LocalAPI.ControleFileSend) {
                            salvarAtividade = false;
                            ++LocalAPI.ControleFileSend;
                            objeto.file.submit();
                            break;
                        }
                    }

                    if (salvarAtividade) {
                        LocalAPI.Methods.SalvarAtividades(LocalAPI.ObjetoSend);
                        LocalAPI.ControleFileSend = 2;
                    }
                },
                fail: function (e, data) {
                    GS_Ajax.AjaxVoid(GS_Path.GetUrl("/Dashboard/DeleteFiles"), 'POST', 'json', function () {
                        $.loader.close();
                        GS_Alert.Simples('Erro ao salvar o arquivo: ' + data.files[0].name + '! <br/> Por favor, entre em contato com a equipe técnica responsável.');
                        LocalAPI.ControleFileSend = 2;
                    });
                }
            });
        },

        OnClickSalvar: function () {
            $('#js-btn-confirmar-log').off('click');
            $('#js-btn-confirmar-log').on('click', function () {
                LocalAPI.ObjetoSend = LocalAPI.Methods.PreencherObjeto();
                var mensagem = LocalAPI.Methods.VerificaObjeto(LocalAPI.ObjetoSend);

                var objeto = LocalAPI.ObjetoSend.log;

                console.log(objeto);

                if (mensagem == '') {
                    $.loader.open();

                    GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/logarAtividade'), 'POST', 'json', objeto, function (resultado) {
                        if (resultado) {

                            var count = 0;
                            $('#js-tbl-anexo > tbody > tr.upload').each(function () {
                                ++count;
                            });

                            if (count > 0) {
                                LocalAPI.FilesSend = new Array();

                                for (var i = 0; i <= LocalAPI.Files.length; i++) {
                                    if (LocalAPI.Files[i] != null) {
                                        LocalAPI.FilesSend.push(LocalAPI.Files[i]);
                                    }
                                }

                                LocalAPI.FilesSend[0].file.submit();
                                $.loader.close();
                            }
                            else {
                                console.log('');
                                console.log('');
                                LocalAPI.Methods.SalvarAtividades(LocalAPI.ObjetoSend);
                            }

                            Id = $('#js-atividade').attr('js-atividade-id');
                            window.location.href = GS_Path.GetUrl('/Dashboard');

                        } else {
                            GS_Alert.Simples('Ocorreu um erro ao tentar salvar o log da atividade!</br>Por favor, entre em contato com a equipe técnica responsável.');
                            $.loader.close();
                        }
                    });
                }
                else {
                    $.loader.close();
                    GS_Alert.Simples(mensagem);
                }
            });
        }
    },

    Methods: {
        RetornarPaineis: function (objeto) {

            GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/CancelarLogWorkAtividade'), 'POST', 'html', objeto, function (resultado) {
                $('#js-div-painel-atividade').html(resultado);
            });

            GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/RetornarPainelApontamento'), 'POST', 'html', objeto, function (resultado) {
                $('#js-div-painel-apontamentos').html(resultado);
            });

            GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/RetornarPainelAnexos'), 'POST', 'html', objeto, function (resultado) {
                $('#js-div-painel-anexos').html(resultado);

                LocalAPI.Events.OnClickLogWork();
                LocalAPI.Events.OnClickIniciar();
                LocalAPI.Events.OnClickImpedimento();
                LocalAPI.Events.OnClickConcluir();
                LocalAPI.Events.OnClickToggleAnexos();
                LocalAPI.Events.OnClickToggleApontamentos();
                LocalAPI.Events.OnClickToggleAtividade();

            });
        },

        CriarLinhaEvidencia: function (objeto, descricao, arquivo, data) {

            var linha = '';
            linha += '<tr id="' + objeto + '" class="upload">';
            linha += '<td></td>';
            linha += '<td class="css-td">' + descricao + '</td>';
            linha += '<td class="css-td">' + arquivo + '</td>';
            linha += '<td class="css-td">' + data + '</td>';
            linha += '<td class="css-td"><i class="fa fa-trash-o fa-1x css-i-atividade-toggle js-i-deletar-linha" title="Deletar Arquivo"></i></td>';
            linha += '</tr>';

            return linha;
        },

        PreencherObjeto: function () {

            var objeto = new Object();
            objeto.atividadeId = $('#js-atividade-id').attr('js-atividade-id');
            objeto.uploads = new Array();

            $('#js-tbl-anexo > tbody > tr.upload').each(function () {
                var upload = new Object();
                upload.descricao = $(this).find('td:eq(1)').text();
                upload.anexo = $(this).find('td:eq(2)').text();
                upload.dtUpload = $(this).find('td:eq(3)').text();

                objeto.uploads.push(upload);
            });

            objeto.log = new Object();
            var log = new Object();

            log.atividadeId = $('#js-atividade-id').attr('js-atividade-id');
            log.tempoEfetivoConsumido = $('#js-txt-horas-trabalhadas').val();
            log.apontamento = $('#js-txtarea-parecer').val();
            log.logInicialStatus = "A";

            objeto.log = log;

            return objeto;
        },

        VerificaObjeto: function (objeto) {
            var mensagem = '';

            if (objeto.log.tempoEfetivoConsumido == '') {
                mensagem += 'O campo Horas Trabalhadas é obrigatório!<br/>';
                $('#js-txt-horas-trabalhadas').parent().addClass('has-error');
            }
            else {
                $('#js-txt-horas-trabalhadas').parent().removeClass('has-error');
            }

            if (objeto.log.apontamento == '') {
                mensagem += 'O campo Parecer é obrigatório!<br/>';
                $('#js-txtarea-parecer').parent().addClass('has-error');
            }
            else {
                $('#js-txtarea-parecer').parent().removeClass('has-error');
            }

            if (objeto.descricao == '') {
                mensagem += 'O campo Descrição é obrigatório!<br/>';
                $('#js-txtarea-descricao').parent().addClass('has-error');
            }
            else {
                $('#js-txtarea-descricao').parent().removeClass('has-error');
            }

            return mensagem;
        },

        SalvarAtividades: function (objeto) {
            GS_Ajax.Ajax(GS_Path.GetUrl('/Dashboard/SalvarAtividade'), 'POST', 'json', objeto, function (resultado) {
                if (resultado) {
                    GS_Alert.SimplesCallBack('Log salvo com sucesso!', function () {

                    });
                }
                else {
                    GS_Alert.Simples('Ocorreu um erro ao tentar salvar a atividade! <br/> Por favor, entre em contato com a equipe técnica responsável.');
                }

                $.loader.close();
            });
        }

    }

}