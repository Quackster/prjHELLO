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
<pre>
<form method="post" action="client.php">
     Name :  <input name="name" value="..." type="text" id="name" maxlength="20">
     Pass :  <input name="password" value="..." type="password" id="password" maxlength="20"><br>
	     <input name="enter" type="submit" id="enter" value="Enter !">
		 
<br><a href="register.php"><br />Create an account >></a>
</form>
</pre>

</td>
</tr>

<tr>

<!-- MENU -->
<td class="menu_box">
<a href="index.php"><input type="button" value="Login"></a>&nbsp;
<a href="register.php"><input type="button" value="Register"></a>&nbsp;
<a href="news.php"><input type="button" value="News"></a>&nbsp;
</td>

</tr>
</table>
<small>Copyright &copy; 2010 - 2011 Wirbelwind</small>

</center>
</body>
</html>