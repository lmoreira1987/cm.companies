function sendMail(){
	//validando campos
	error = false;

	//limpando espaços
	$("#email-name").val( $.trim($("#email-name").val()) );
	$("#email-mail").val( $.trim($("#email-mail").val()) );
	$("#message").val( $.trim($("#message").val()) );
	
	if( $("#email-name").val().length < 0 )
	{
		alert("Preenchimento, incorreto. O campo Nome é obrigatório.");
		error = true;
	}
	else if( $("#email-mail").val().length < 0 )
	{
		alert("Preenchimento, incorreto. O campo E-mail é obrigatório.");
		error = true;
	}
	else if( $("#email-name").val().length < 0 )
	{
		alert("Preenchimento, incorreto. O campo Mensagem é obrigatório.");
		error = true;
	}

	//caso não tenha erros
	if( error == false )
	{
		$.ajax({
			url: "sendMail.php",
			type: "post",
			data: $("#contact-form").serialize(),
			dataType: "json",
			success: function(data){
				alert(data.msg);
			},
			error: function(){
				alert("Falha ao enviar a mensagem, tente novamente em breve.");
			}
		});
	}

	//evitar submit padrão
	return false;
}