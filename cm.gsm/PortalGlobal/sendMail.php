<?php

$out = array("msg" => "Preenchimento incorreto, não foi possível enviar sua mensagem.");

//verificando se contém todos os campos
if( isset($_POST['name']) && isset($_POST['email']) && isset($_POST['message']) )
{
	$_POST['name']  =utf8_decode($_POST['name']);
	$_POST['message']  =utf8_decode($_POST['message']);

	require 'PHPMailer-master/PHPMailerAutoload.php';

	$mail = new PHPMailer;

	// $mail->SMTPDebug = 2;                               // Enable verbose debug output

	// $mail->isSMTP();                                      // Set mailer to use SMTP
	// $mail->Host = 'smtp.office365.com';  // Specify main and backup SMTP servers
	// $mail->SMTPAuth = true;                               // Enable SMTP authentication
	// $mail->Username = 'marketing@gsoftware.com.br';                 // SMTP username
	// $mail->Password = 'Samcezar10';                           // SMTP password
	// $mail->SMTPSecure = 'tls';                            // Enable TLS encryption, `ssl` also accepted
	// $mail->Port = 587;                                    // TCP port to connect to

	$mail->isSMTP();                                      // Set mailer to use SMTP
	$mail->Host = 'smtp.gmail.com';  // Specify main and backup SMTP servers
	$mail->SMTPAuth = true;                               // Enable SMTP authentication
	$mail->Username = 'dev.globalsoftware@gmail.com';                 // SMTP username
	$mail->Password = 'Global@123';                           // SMTP password
	$mail->SMTPSecure = 'tls';                            // Enable TLS encryption, `ssl` also accepted
	$mail->Port = 587;                                    // TCP port to connect to

	$mail->From = 'contato@gsoftware.com.br';
	$mail->FromName = 'Contato - Global';
	$mail->addAddress('samuel.cezario@gsoftware.com.br', 'Samuel Cezário');     // Add a recipient
	$mail->addAddress('adriano.silva@gsoftware.com.br', 'Adriano Maciel');     // Add a recipient

	// $mail->addAddress('contato@gsoftware.com.br', 'Global');     // Add a recipient
	                                                                           
	$mail->addReplyTo($_POST['email'], $_POST['name']);
	// $mail->addCC('cc@example.com');
	// $mail->addBCC('bcc@example.com');

	// $mail->addAttachment('/var/tmp/file.tar.gz');         // Add attachments
	// $mail->addAttachment('/tmp/image.jpg', 'new.jpg');    // Optional name
	// $mail->isHTML(true);                                  // Set email format to HTML

	$mail->Subject = 'Contato pelo site';
	$mail->Body    = $_POST['message'];
	// $mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	if(!$mail->send()) {
	    $out = array("msg" => "Não foi possível enviar sua mensagem, tente novamente em breve.");
	    // echo 'Mailer Error: ' . $mail->ErrorInfo;
	} else {
	    $out = array("msg" => "Mensagem enviada com sucesso!");
	}
}

echo json_encode($out);

?>