/// <reference path="../Global/Global.js" />

var LocalAPI = {

    ObjetoFile: new Object(),
    Files: new Array(),
    FilesSend: new Array(),
    ControleFile: 0,
    ControleFileSend: 2,
    ObjetoSend: new Object(),
    SalvarOrEdit: true,

    Init: function () {
        this.Events.Init();
    },

    Events: {

        Init: function () {
            this.OnClickFiltro();
            this.OnClickLimpar();
            this.OnClickAdionarAtividade();
            this.OnClickToggleAtividade();
            this.OnClickAtualizarAtividades();
        },

        OnClickFiltro: function () {
            $('#js-btn-pesquisar').off('click');
            $('#js-btn-pesquisar').on('click', function () {
                var objeto = LocalAPI.Methods.PreencherObjetoModal();
                var mensagem = LocalAPI.Methods.VerificarObjetoModal(objeto);

                if (mensagem == '') {
                    GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/PesquisarAtividade'), 'POST', 'html', objeto, function (resultado) {
                        $('#js-div-tabela-atividades').empty().html(resultado);
                        $('#js-div-modal-filtro').modal('hide');

                        LocalMask.Init();
                        LocalAPI.Init();
                        LocalAPI.Events.PaginarAtividade();
                        LocalAPI.Events.OnClickCancelarAtividade();
                        LocalAPI.Events.OnClickEditarAtividade();

                        var count = 0;
                        $('.panel').each(function () {
                            ++count;
                        });
                        if (count > 1)
                            $('#js-i-adicionar').off('click');

                        if (!$('.js-div-panel-atividade:eq(0)').is(':visible')) {
                            $('.js-div-panel-atividade:eq(0)').toggle('display');
                        }
                    });
                }
                else {
                    GS_Alert.Simples(mensagem);
                }
            });
        },

        OnClickLimpar: function () {
            $('#js-btn-limpar').off('click');
            $('#js-btn-limpar').on('click', function () {
                $('#js-slc-modal-projeto').val('0');
                $('#js-slc-intervalo-atividades').val('5');
                $('#js-txt-modal-de').val('');
                $('#js-txt-modal-ate').val('');
                $('#js-txt-modal-nome').val('');
                $('#js-txt-modal-os').val('');
            });
        },

        PaginarAtividade: function () {
            var count = $('.js-p-paginar-atividade').attr('id-count');

            $('.js-p-paginar-atividade').bootpag({
                total: count,
                maxVisible: 10
            }).on("page", function (event, num) {
                var objeto = num;

                GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/PaginarAtividades'), 'POST', 'html', objeto, function (resultado) {
                    $("#js-div-tabela-atividades-conteudo").empty().html(resultado);

                    LocalAPI.Events.OnClickCancelarAtividade();
                    LocalAPI.Events.OnClickEditarAtividade();
                });
            });
        },

        OnClickAdionarAtividade: function () {
            $('#js-i-adicionar').off('click');
            $('#js-i-adicionar').on('click', function () {
                if ($('.js-div-panel-atividade:eq(0)').is(':visible')) {
                    $('.js-div-panel-atividade:eq(0)').toggle('toggle');
                }

                $("#js-div-partial-edit-atividade").empty();

                GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/CriarAtividade'), 'POST', 'html', function (resultado) {
                    $('#js-div-partial-criar-atividade').empty().html(resultado);

                    LocalAPI.Events.OnClickFiltro();
                    LocalAPI.Events.OnClickLimpar();
                    LocalAPI.Events.OnClickToggleAtividade();
                    LocalAPI.Events.OnTypeAHead();
                    LocalAPI.Events.OnClickLimparOS();
                    LocalAPI.Events.OnClickSelecionarOS();
                    LocalAPI.Events.OnClickCancelarOS();

                    $('#js-i-adicionar').off('click');
                });
            });
        },

        OnClickToggleAtividade: function () {
            $('.js-i-toggle-atividade').off('click');
            $('.js-i-toggle-atividade').on('click', function (e) {
                e.preventDefault();
                $(this).parent().parent().find('.js-div-panel-atividade').toggle('toggle');
            });
        },

        OnTypeAHead: function () {
            $('#js-txt-typeahead').typeahead({
                ajax: {
                    url: GS_Path.GetUrl('/Fabrica/TypeAHeadOS')
                }
            });
        },

        OnClickLimparOS: function () {
            $('#js-btn-limpar-os').off('click');
            $('#js-btn-limpar-os').on('click', function () {
                $('#js-div-partial-criar-atividade-conteudo').empty();
                $('#js-div-partial-criar-atividade-os').empty();
                $('#js-div-partial-criar-atividade-conteudo-anexo').empty();
                $('#js-txt-typeahead').val('');
            });
        },

        OnClickSelecionarOS: function () {
            $('#js-btn-selecionar-os').off('click');
            $('#js-btn-selecionar-os').on('click', function () {
                var objeto = $('#js-txt-typeahead').val();

                if (objeto.trim() != '') {
                    $.loader.open();

                    GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/VerificaOS'), 'POST', 'json', objeto, function (resultado) {
                        if (resultado) {
                            GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/CriarAtividadeOS'), 'POST', 'html', function (resultado) {
                                $('#js-div-partial-criar-atividade-os').empty().html(resultado);

                                LocalAPI.Events.OnClickToggleAtividade();

                                $('.js-div-panel-atividade:eq(1)').toggle('toggle');
                            });

                            GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/CriarAtividadeConteudo'), 'POST', 'html', function (resultado) {
                                $('#js-div-partial-criar-atividade-conteudo').empty().html(resultado);

                                GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/CriarAtividadeConteudoAnexo'), 'POST', 'html', function (resultado) {
                                    $('#js-div-partial-criar-atividade-conteudo-anexo').empty().html(resultado);

                                    LocalMask.Init();
                                    LocalAPI.Events.OnClickPropagar();
                                    LocalAPI.Events.OnClickToggleAtividade();
                                    LocalAPI.Events.OnClickFileUpload();
                                    LocalAPI.Events.OnClickAdicionarUpload();
                                    LocalAPI.Events.OnClickAdicionarDownload();
                                    LocalAPI.Events.OnClickSalvar();

                                    $('.js-div-panel-atividade:eq(3)').toggle('toggle');

                                    $.loader.close();
                                });
                            });

                            if ($('.js-div-panel-atividade:eq(0)').is(':visible')) {
                                $('.js-div-panel-atividade:eq(0)').toggle('toggle');
                            }
                        }
                        else {
                            $.loader.close();

                            GS_Alert.Simples('OS não encontrada.');
                        }
                    });
                }
                else {
                    GS_Alert.Simples('O campo de pesquisar OS é obrigatório.');
                }
            });
        },

        OnClickCancelarOS: function () {
            $('#js-btn-cancelar-os').off('click');
            $('#js-btn-cancelar-os').on('click', function () {
                $('#js-div-partial-criar-atividade').empty();
                $('#js-div-partial-criar-atividade-os').empty();
                $('#js-div-partial-criar-atividade-conteudo-anexo').empty();

                if (!$('.js-div-panel-atividade:eq(0)').is(':visible')) {
                    $('.js-div-panel-atividade:eq(0)').toggle('toggle');
                }

                LocalAPI.Events.OnClickAdionarAtividade();
            });
        },

        OnClickPropagar: function () {
            $('#js-ckb-propagar').off('click');
            $('#js-ckb-propagar').on('click', function () {
                if ($(this).is(':checked')) {
                    $('#js-hdn-calendar').addClass('date-multi');

                    $('.date-multi').datepicker({
                        multidate: true,
                        format: "dd/mm/yyyy",
                        language: "pt-BR"
                    });

                    $('.date-multi').datepicker('setDates');

                    $('.date-multi').datepicker('show');
                }
                else {
                    $('.date-multi').removeClass('date-multi');
                    $('.date-multi').datepicker('hide');
                }
            });
        },

        OnClickFileUpload: function () {
            $('#js-btn-file-anexo').fileupload({
                limitMultiFileUploads: 1,
                singleFileUploads: true,
                progressInterval: 10,
                dataType: 'json',
                url: GS_Path.GetUrl('/Fabrica/FileUpload'),
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
                        if (LocalAPI.SalvarOrEdit) {
                            LocalAPI.Methods.SalvarAtividades(LocalAPI.ObjetoSend);
                        }
                        else {
                            LocalAPI.Methods.SalvarAtividadesEdit(LocalAPI.ObjetoSend);
                        }
                        
                        LocalAPI.ControleFileSend = 2;
                    }
                },
                fail: function (e, data) {
                    GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/DeleteFiles'), 'POST', 'json', function () {
                        $.loader.close();
                        GS_Alert.Simples('Erro ao salvar o arquivo: ' + data.files[0].name + '! <br/> Por favor, entre em contato com a equipe técnica responsável.');
                        LocalAPI.ControleFileSend = 2;
                    });
                }
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

                    GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/GetDate'), 'POST', 'json', function (resultado) {
                        var linha = LocalAPI.Methods.CriarLinhaEvidencia(objeto.id, descricao, objeto.file.files[0].name, resultado);

                        $('#js-tbl-evidencia > tbody:last').append(linha);

                        var count = 1;
                        $('#js-tbl-evidencia > tbody > tr').each(function () {
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
                        if ($(linha).hasClass('upload')) {
                            var id = $(linha).attr('id');
                            
                            for (var i = 0; i < LocalAPI.Files.length; i++) {
                                if (LocalAPI.Files[i].id == id)
                                    LocalAPI.Files[i].id = '0';
                            }
                        }
                        else {
                            $('#js-tbl-evidencia-anterior > tbody > tr').each(function () {
                                if ($(this).attr('id') == $(linha).attr('id-controle')) {
                                    $(this).show();
                                }
                            });
                        }

                        $(linha).remove();

                        var count = 1;
                        $('#js-tbl-evidencia > tbody > tr').each(function () {
                            $(this).find('td:eq(0)').text(count);
                            ++count;
                        });
                    }
                });
            });
        },

        OnClickAdicionarDownload: function () {
            $('#js-i-adicionar-download').off('click');
            $('#js-i-adicionar-download').on('click', function () {
                $('#js-tbl-evidencia-anterior > tbody > tr').each(function () {
                    if ($(this).find('.js-ckb-anexo').is(':checked')) {
                        var linha = LocalAPI.Methods.CriarLinhaEvidenciaAnterior($(this).attr('id'), $(this).find('td:eq(1)').text(), $(this).find('td:eq(2)').text(), $(this).find('td:eq(3)').text())

                        $('#js-tbl-evidencia > tbody:last').append(linha);

                        var count = 1;
                        $('#js-tbl-evidencia > tbody > tr').each(function () {
                            $(this).find('td:eq(0)').text(count);
                            ++count;
                        });

                        $(this).hide();
                        $(this).find('.js-ckb-anexo').prop('checked', false);

                        LocalAPI.Events.OnClickDeletarUpload();
                    }
                });
            });
        },

        OnClickSalvar: function () {
            $('#js-btn-confirmar-ati').off('click');
            $('#js-btn-confirmar-ati').on('click', function () {
                LocalAPI.ObjetoSend = LocalAPI.Methods.PreencherObjeto();
                LocalAPI.SalvarOrEdit = true;
                var mensagem = LocalAPI.Methods.VerificaObjeto(LocalAPI.ObjetoSend);

                if (mensagem == '') {
                    $.loader.open();

                    GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/VerificaOS'), 'POST', 'json', LocalAPI.ObjetoSend.id, function (resultado) {
                        if (resultado) {
                            var count = 0;
                            $('#js-tbl-evidencia > tbody > tr.upload').each(function () {
                                ++count;
                            });

                            if (count > 0) {
                                LocalAPI.FilesSend = new Array();

                                for (var i = 0; i < LocalAPI.Files.length; i++) {
                                    if (LocalAPI.Files[i].id != '0') {
                                        LocalAPI.FilesSend.push(LocalAPI.Files[i]);
                                    }
                                }
                                LocalAPI.FilesSend[0].file.submit();
                            }
                            else {
                                LocalAPI.Methods.SalvarAtividades(LocalAPI.ObjetoSend);
                            }
                        }
                        else {
                            $.loader.close();

                            GS_Alert.Simples('OS não encontrada.');
                        }
                    });
                }
                else {
                    GS_Alert.Simples(mensagem);
                }
            });
        },

        OnClickCancelarAtividade: function () {
            $('.js-i-cancelar-ati').off('click');
            $('.js-i-cancelar-ati').on('click', function () {
                var objeto = $(this).parent().parent().attr('id');
                var linha = $(this);

                GS_Alert.Dialogo("Deseja cancelar esta atividade?", function (resultado) {
                    if (resultado) {
                        GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/CancelarAtividade'), 'POST', 'json', objeto, function (resultado2) {
                            if (resultado2) {
                                $(linha).parent().parent().remove();
                            }
                            else {
                                GS_Alert.Simples("Ocorreu um erro ao tentar cancelar a atividade! <br/>Por favor, entre em contato com a equipe técnica responsável.");
                            }
                        });
                    }
                });
            });
        },

        OnClickEditarAtividade: function () {
            $('.js-td-editar-ati').off('click');
            $('.js-td-editar-ati').on('click', function () {
                $.loader.open();

                var objeto = $(this).parent().attr('id');

                if (!$('.js-div-panel-atividade:eq(0)').is(':visible')) {
                    $('.js-div-panel-atividade:eq(0)').toggle('display');
                }
                else {
                    $('.js-div-panel-atividade:eq(0)').toggle('toggle');
                }

                GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/EditAtividade'), 'POST', 'html', objeto, function (resultado) {
                    $("#js-div-partial-edit-atividade").empty().html(resultado);
                    $("#js-div-partial-criar-atividade").empty();
                    $("#js-div-partial-criar-atividade-os").empty();

                    LocalAPI.Events.OnClickToggleAtividade();
                    LocalAPI.Events.OnClickCancelarOSEdit();
                    LocalAPI.Events.OnClickLimparOSEdit();

                    LocalMask.Init();
                    LocalAPI.Events.OnClickPropagar();
                    LocalAPI.Events.OnClickToggleAtividade();
                    LocalAPI.Events.OnClickFileUpload();
                    LocalAPI.Events.OnClickAdicionarUpload();
                    LocalAPI.Events.OnClickAdicionarDownload();
                    LocalAPI.Events.OnClickSalvarEdit();
                    LocalAPI.Events.OnClickDeletarUploadBanco();

                    $('.js-div-panel-atividade:eq(1)').toggle('toggle');
                    $('.js-div-panel-atividade:eq(3)').toggle('toggle');

                    $('#js-i-adicionar').off('click');
                    $.loader.close();
                });
            });
        },

        OnClickCancelarOSEdit: function () {
            $('#js-btn-cancelar-os').off('click');
            $('#js-btn-cancelar-os').on('click', function () {
                $("#js-div-partial-edit-atividade").empty();

                LocalAPI.Events.OnClickAdionarAtividade();
            });
        },

        OnClickLimparOSEdit: function () {
            $('#js-btn-limpar-os').off('click');
            $('#js-btn-limpar-os').on('click', function () {
                $('#js-slc-tipo-ati').val($('#js-slc-tipo-ati').attr('id-valor'));
                $('#js-txt-ati-nome').val($('#js-txt-ati-nome').attr('id-valor'));
                $('#js-txtarea-descricao').val($('#js-txtarea-descricao').attr('id-valor'));
                $('#js-txt-ati-ini').val($('#js-txt-ati-ini').attr('id-valor'));
                $('#js-txt-ati-fim').val($('#js-txt-ati-fim').attr('id-valor'));
                $('#js-txt-ati-prazo').val($('#js-txt-ati-prazo').attr('id-valor'));
                $('#js-hdn-calendar').val('');
                $('#js-ckb-propagar').prop('checked', false);

                $('#js-tbl-evidencia > tbody > tr.upload').each(function () {
                    $(this).remove();
                });

                $('#js-tbl-evidencia > tbody > tr.download').each(function () {
                    $(this).remove();
                });

                LocalAPI.ObjetoFile = new Object();
                LocalAPI.Files = new Array();
                LocalAPI.FilesSend = new Array();
                LocalAPI.ControleFile = 0;
                LocalAPI.ControleFileSend = 2;
                LocalAPI.ObjetoSend = new Object();
            });
        },

        OnClickSalvarEdit: function () {
            $('#js-btn-confirmar-ati').off('click');
            $('#js-btn-confirmar-ati').on('click', function () {
                LocalAPI.ObjetoSend = LocalAPI.Methods.PreencherObjetoEdit();
                LocalAPI.SalvarOrEdit = false;
                var mensagem = LocalAPI.Methods.VerificaObjetoEdit(LocalAPI.ObjetoSend);

                if (mensagem == '') {
                    $.loader.open();

                    var count = 0;
                    $('#js-tbl-evidencia > tbody > tr.upload').each(function () {
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
                    }
                    else {
                        LocalAPI.Methods.SalvarAtividadesEdit(LocalAPI.ObjetoSend);
                    }
                }
                else {
                    GS_Alert.Simples(mensagem);
                }
            });
        },

        OnClickDeletarUploadBanco: function () {
            $('.js-i-deletar-linha-edit').off('click');
            $('.js-i-deletar-linha-edit').on('click', function () {
                var td = $(this);

                GS_Alert.Dialogo('Deseja realmente excluir?', function (resultado) {
                    if (resultado) {
                        $(td).parent().parent().removeClass('notDelete');
                        $(td).parent().parent().addClass('delete');

                        $(td).removeClass('fa-trash-o');
                        $(td).removeClass('js-i-deletar-linha-edit');
                        $(td).addClass('fa-times');
                        
                        $(td).css('color', '#d9534f');

                        $(td).off();

                        LocalAPI.Events.OnClickDeletarUploadBanco();
                    }
                });
            });
        },

        OnClickAtualizarAtividades: function () {
            $('#js-i-atualizar').off('click');
            $('#js-i-atualizar').on('click', function () {
                var count = 0;
                $('#js-tbl-ati-conteudo > tbody > tr').each(function () {
                    ++count;
                });

                if (count > 0 || $('.js-p-paginar-atividade').attr('id-count') != '0') {
                    GS_Ajax.AjaxVoid(GS_Path.GetUrl('/Fabrica/PesquisarAtividadeAtualizar'), 'POST', 'html', function (resultado) {
                        $('#js-div-tabela-atividades').empty().html(resultado);

                        LocalMask.Init();
                        LocalAPI.Init();
                        LocalAPI.Events.PaginarAtividade();
                        LocalAPI.Events.OnClickEditarAtividade();
                        LocalAPI.Events.OnClickCancelarAtividade();
                    });
                }
            });
        }
    },

    Methods: {
        PreencherObjetoModal: function () {
            var objeto = new Object();

            objeto.projeto = $('#js-slc-modal-projeto > option:selected').val();
            objeto.de = $('#js-txt-modal-de').val();
            objeto.ate = $('#js-txt-modal-ate').val();
            objeto.nome = $('#js-txt-modal-nome').val();
            objeto.os = $('#js-txt-modal-os').val();
            objeto.intervalo = $('#js-slc-intervalo-atividades > option:selected').val();
            objeto.pagina = '1';

            return objeto;
        },

        VerificarObjetoModal: function (objeto) {
            var mensagem = '';

            if (objeto.projeto == '0' && objeto.de.trim() == '' && objeto.ate.trim() == '' && objeto.nome.trim() == '' && objeto.os.trim() == '')
                mensagem += 'É necessário selecionar ao menos um campo.</br>';

            var reg = new RegExp(/[0-9]/g);

            if (reg.test(objeto.os) === false && objeto.os.trim() != '')
                mensagem += 'O campo OS deve ser numérico.';

            return mensagem;
        },

        CriarLinhaEvidencia: function (objeto, descricao, arquivo, data) {
            var date = new Date();

            var linha = '';
            linha += '<tr id="' + objeto + '" class="upload">';
            linha += '<td></td>';
            linha += '<td>' + descricao + '</td>';
            linha += '<td>' + arquivo + '</td>';
            linha += '<td>' + data + '</td>';
            linha += '<td class="css-td"><i class="fa fa-trash-o fa-1x css-i-atividade-toggle js-i-deletar-linha" title="Deletar Arquivo"></i></td>';
            linha += '</tr>';

            return linha;
        },

        CriarLinhaEvidenciaAnterior: function (objeto, descricao, arquivo, data) {
            var date = new Date();

            var linha = '';
            linha += '<tr id-controle="' + objeto + '" class="download">';
            linha += '<td></td>';
            linha += '<td>' + descricao + '</td>';
            linha += '<td>' + arquivo + '</td>';
            linha += '<td>' + data + '</td>';
            linha += '<td class="css-td"><i class="fa fa-trash-o fa-1x css-i-atividade-toggle js-i-deletar-linha" title="Deletar Arquivo"></i></td>';
            linha += '</tr>';

            return linha;
        },

        PreencherObjeto: function () {
            var objeto = new Object();
            objeto.id = $('#js-txt-typeahead').val();
            objeto.tipoAti = $('#js-slc-tipo-ati').val();
            objeto.atividades = $('#js-txt-ati-nome').val();
            objeto.descricao = $('#js-txtarea-descricao').val();

            if ($('#js-ckb-propagar').is(':checked')) {
                objeto.datas = $('#js-hdn-calendar').val();
            }

            objeto.dataIni = $('#js-txt-ati-ini').val();
            objeto.dataFim = $('#js-txt-ati-fim').val();
            objeto.prazo = $('#js-txt-ati-prazo').val();
            objeto.recurso = $('#js-slc-recurso-ati').val();
            objeto.recursoTec = $('#js-slc-recurso-ati').find('option:selected').attr('id-tec');

            objeto.uploads = new Array();

            $('#js-tbl-evidencia > tbody > tr.upload').each(function () {
                var upload = new Object();
                upload.descricao = $(this).find('td:eq(1)').text();
                upload.anexo = $(this).find('td:eq(2)').text();
                upload.dtUpload = $(this).find('td:eq(3)').text();

                objeto.uploads.push(upload);
            });

            objeto.downloads = new Array();

            $('#js-tbl-evidencia > tbody > tr.download').each(function () {
                var download = new Object();
                download.id = $(this).attr('id-controle');
                download.descricao = $(this).find('td:eq(1)').text();
                download.anexo = $(this).find('td:eq(2)').text();
                download.dtUpload = $(this).find('td:eq(3)').text();

                objeto.downloads.push(download);
            });

            return objeto;
        },

        VerificaObjeto: function (objeto) {
            var mensagem = '';

            if (objeto.id == '') {
                mensagem += 'O campo de pesquisar OS é obrigatório!<br/>';
                $('#js-txt-typeahead').parent().addClass('has-error');
            }
            else {
                $('#js-txt-typeahead').parent().removeClass('has-error');
            }

            if (objeto.tipoAti == '0') {
                mensagem += 'O campo Tipo de Atividade é obrigatório!<br/>';
                $('#js-slc-tipo-ati').parent().addClass('has-error');
            }
            else {
                $('#js-slc-tipo-ati').parent().removeClass('has-error');
            }

            if (objeto.atividades == '') {
                mensagem += 'O campo Atividade é obrigatório!<br/>';
                $('#js-txt-ati-nome').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-nome').parent().removeClass('has-error');
            }

            if (objeto.descricao == '') {
                mensagem += 'O campo Descrição é obrigatório!<br/>';
                $('#js-txtarea-descricao').parent().addClass('has-error');
            }
            else {
                $('#js-txtarea-descricao').parent().removeClass('has-error');
            }

            if (objeto.dataIni == '') {
                mensagem += 'O campo Início é obrigatório!<br/>';
                $('#js-txt-ati-ini').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-ini').parent().removeClass('has-error');
            }

            if (objeto.dataFim == '') {
                mensagem += 'O campo Fim é obrigatório!<br/>';
                $('#js-txt-ati-fim').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-fim').parent().removeClass('has-error');
            }

            if (objeto.prazo == '') {
                mensagem += 'O campo Prazo é obrigatório!<br/>';
                $('#js-txt-ati-prazo').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-prazo').parent().removeClass('has-error');
            }

            if (objeto.recurso == '0') {
                mensagem += 'O campo Recurso é obrigatório!<br/>';
                $('#js-slc-recurso-ati').parent().addClass('has-error');
            }
            else {
                $('#js-slc-recurso-ati').parent().removeClass('has-error');
            }

            return mensagem;
        },

        SalvarAtividades: function (objeto) {
            GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/SalvarAtividade'), 'POST', 'json', objeto, function (resultado) {
                if (resultado) {
                    GS_Alert.SimplesCallBack('Atividade salva com sucesso!', function () {
                        $('#js-div-partial-criar-atividade').empty();
                        $('#js-div-partial-criar-atividade-os').empty();
                        $('#js-div-partial-criar-atividade-conteudo-anexo').empty();

                        $('.js-div-panel-atividade:first').toggle('toggle');

                        LocalAPI.Events.OnClickAdionarAtividade();
                    });
                }
                else {
                    GS_Alert.Simples('Ocorreu um erro ao tentar salvar a atividade! <br/> Por favor, entre em contato com a equipe técnica responsável.');
                }

                $.loader.close();
            });
        },

        SalvarAtividadesEdit: function (objeto) {
            GS_Ajax.Ajax(GS_Path.GetUrl('/Fabrica/EditarAtividade'), 'POST', 'json', objeto, function (resultado) {
                if (resultado) {
                    GS_Alert.SimplesCallBack('Atividade atualizada com sucesso!', function () {
                        $('.js-div-panel-atividade:first').toggle('toggle');

                        $("#js-div-partial-edit-atividade").empty();

                        LocalAPI.Events.OnClickAdionarAtividade();
                    });
                }
                else {
                    GS_Alert.Simples('Ocorreu um erro ao tentar atualizar a atividade! <br/> Por favor, entre em contato com a equipe técnica responsável.');
                }

                $.loader.close();
            });
        },

        PreencherObjetoEdit: function () {
            var objeto = new Object();
            objeto.id = $('#js-txt-typeahead-edit').val();
            objeto.idAti = $('#js-btn-confirmar-ati').attr('id-valor');
            objeto.tipoAti = $('#js-slc-tipo-ati').val();
            objeto.atividade = $('#js-txt-ati-nome').val();
            objeto.descricao = $('#js-txtarea-descricao').val();
            
            if ($('#js-ckb-propagar').is(':checked')) {
                objeto.datas = $('#js-hdn-calendar').val();
            }

            objeto.dataIni = $('#js-txt-ati-ini').val();
            objeto.dataFim = $('#js-txt-ati-fim').val();
            objeto.prazo = $('#js-txt-ati-prazo').val();
            objeto.recursoTec = $('#js-slc-recurso-ati').find('option:selected').attr('id-tec');

            objeto.uploads = new Array();

            $('#js-tbl-evidencia > tbody > tr.upload').each(function () {
                var upload = new Object();
                upload.descricao = $(this).find('td:eq(1)').text();
                upload.anexo = $(this).find('td:eq(2)').text();
                upload.dtUpload = $(this).find('td:eq(3)').text();

                objeto.uploads.push(upload);
            });

            objeto.downloads = new Array();

            $('#js-tbl-evidencia > tbody > tr.download').each(function () {
                var download = new Object();
                download.id = $(this).attr('id-controle');
                download.descricao = $(this).find('td:eq(1)').text();
                download.anexo = $(this).find('td:eq(2)').text();
                download.dtUpload = $(this).find('td:eq(3)').text();

                objeto.downloads.push(download);
            });

            objeto.deletar = new Array();

            $('#js-tbl-evidencia > tbody > tr.delete').each(function () {
                var id = $(this).attr('id-controle');

                objeto.deletar.push(id);
            });

            return objeto;
        },

        VerificaObjetoEdit: function (objeto) {
            var mensagem = '';

            if (objeto.tipoAti == '0') {
                mensagem += 'O campo Tipo de Atividade é obrigatório!<br/>';
                $('#js-slc-tipo-ati').parent().addClass('has-error');
            }
            else {
                $('#js-slc-tipo-ati').parent().removeClass('has-error');
            }

            if (objeto.atividade == '') {
                mensagem += 'O campo Atividade é obrigatório!<br/>';
                $('#js-txt-ati-nome').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-nome').parent().removeClass('has-error');
            }

            if (objeto.descricao == '') {
                mensagem += 'O campo Descrição é obrigatório!<br/>';
                $('#js-txtarea-descricao').parent().addClass('has-error');
            }
            else {
                $('#js-txtarea-descricao').parent().removeClass('has-error');
            }

            if (objeto.dataIni == '') {
                mensagem += 'O campo Início é obrigatório!<br/>';
                $('#js-txt-ati-ini').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-ini').parent().removeClass('has-error');
            }

            if (objeto.dataFim == '') {
                mensagem += 'O campo Fim é obrigatório!<br/>';
                $('#js-txt-ati-fim').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-fim').parent().removeClass('has-error');
            }

            if (objeto.prazo == '') {
                mensagem += 'O campo Prazo é obrigatório!<br/>';
                $('#js-txt-ati-prazo').parent().addClass('has-error');
            }
            else {
                $('#js-txt-ati-prazo').parent().removeClass('has-error');
            }

            return mensagem;
        },
    }
}