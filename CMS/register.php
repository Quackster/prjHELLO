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
<form method="post" action="register_sub.php">
          Name :  <input name="name" type="text" id="name" size="27" maxlength="12"> ( max length : 12 )
          Pass :  <input name="password" type="password" id="password" size="27" maxlength="7"> ( max length : 7 )
         Email :  <input name="email" type="text" id="email" size="27" maxlength="30" /> ( max length : 30 )
         Motto :  <input name="mission" value="Lulz!" type="text" id="mission" size="27" maxlength="15" /> ( max length : 15 )
      Birthday :  <input name="birthday" value="01.01.1990" type="text" id="birthday" size="27" maxlength="10" /> 
<br><br>
<input name="enter" type="submit" id="enter" value="Continuer >>">
</form>
</pre>

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