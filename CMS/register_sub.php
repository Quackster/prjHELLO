<html>
<head>
<? include('./data/config.php'); ?>

<body oncontextmenu="return false" onselectstart="return false" ondragstart="return false">
<title><?php echo $site_title ?></title>
<link rel="shortcut icon" type=image/vnd.microsoft.icon href="data/favicon.ico"></link>

<style>
@import "data/style.css";
</style>
<script type="text/javascript" language="JavaScript" src="data/ajax.js"></script>
<script>
function show(id) {
document.getElementById(id).setAttribute("class", "show");
}
function hide(id) {
document.getElementById(id).setAttribute("class", "hide");
}
</script>
</head>
<body isFront="1">

<center>
<img src="data/logo.gif"><br><br>
<table>
<tr>


<td class="under_box">



<?

	include('./data/config.php');
	mysql_connect("$db_host", "$db_user", "$db_pass") or die(mysql_error());
	mysql_select_db("$db_db") or die(mysql_error());

$name = $_POST[name];
$password = $_POST[password];
$email = $_POST[email];
$birthday = $_POST[birthday];
$mission = $_POST[mission];
	
	
	$result = mysql_query("SELECT * FROM users");
	$lol = "UPDATE members SET id = (id+0) WHERE username='$name'; "; 
	$date = date('d').'/'.date('m').'/'.date('Y');
	
$result = mysql_query("SELECT username FROM members WHERE username='$name'");
$users_name = mysql_fetch_row($result);
$users_name = $users_name[0];
$users_name_check = strtolower($users_name);
$habbo_check = strtolower($name);
	if ( $habbo_check == $users_name_check ) {
    exit('<pre><br><br>Name already used ! <a href="register.php"><br><br><< Back</a></pre>');
	
	
	}
	
$registeruser = mysql_query("INSERT INTO members (id, username, password, role, email, coins, motto, console_motto, figure, birthday, country, sex, badge, had_read_agreement) VALUES ('', '$name', '$password', '1', '$email', '$user_coins', '$mission', 'HELLO_Project', '$user_figure', '$birthday', 'FR', 'M', '0', '1')") or die (mysql_error());

//$newid = mysql_query("UPDATE id SET id = id+1") or die (mysql_error());

echo '<pre><b>Sucessful !</b><br>
	( You can use your name in the client )<br><br><br><a href="index.php">Login >></a></pre>';



?>

	</td></tr><tr>
	<!-- MENU -->
	<td class="menu_box">
	<a href="index.php"><input type="button" value="Login"></a>&nbsp;
	<a href="register.php"><input type="button" value="Register"></a>&nbsp;
	<a href="news.php"><input type="button" value="News"></a>&nbsp;
	</td></tr></table>
	<small>Copyright &copy; 2010 - 2011 Wirbelwind</small></center>
	</body></html>