<html>
<head>
<?php include('./data/config.php'); ?>

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
<table><tr>

<td class="under_box">


<?php

    include('./data/config.php');
    mysql_connect("$db_host", "$db_user", "$db_pass") or die(mysql_error());
    mysql_select_db("$db_db") or die(mysql_error());

    $myusername= mysql_real_escape_string($_POST[name]);
    $mypassword= mysql_real_escape_string($_POST[password]);
    
    $sql="SELECT * FROM members WHERE username='$myusername' and password='$mypassword'";
    $result=mysql_query($sql);
    $count=mysql_num_rows($result);

    if($count==1){
    $check = mysql_query("SELECT * FROM members WHERE username='$myusername'");
    $info = mysql_fetch_array( $check );
    $habbo = $info['name'];
    $rand1=mt_rand(100000,999999) ;
    $rand2=mt_rand(10000,99999) ;
    $rand3=mt_rand(10000,99999) ;
    $rand4=mt_rand(10000,99999) ;
    $rand5=mt_rand(10000,99999) ;
    $rand6=mt_rand(0,9) ;
    $ticket="ST-".$rand1."-".$rand2.$rand3.$rand4.$rand5."-es-fe".$rand6 ;
    //mysql_query("INSERT INTO SSO (name,ticket) VALUES ('$habbo','$ticket')") ;    

    echo '
<OBJECT id=gf_entry codeBase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=8,0,0,0 
height=540 width=720 classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000>
<PARAM NAME="src" VALUE="dcr_v1_r0910/loader.dcr">
<PARAM NAME="swRemote" VALUE="swSaveEnabled="true" 
swVolume="true" 
swRestart="false" 
swPausePlay="false" 
swFastForward="false" 
swTitle="Habbo Hotel" 
swContextMenu="true" ">
<PARAM NAME="swStretchStyle" VALUE="none">
<PARAM NAME="swText" VALUE='.$server_ip.':'.$server_port.'">
<PARAM NAME="bgColor" VALUE="#000000">
<embed src="dcr_v1_r0910/loader.dcr" bgcolor="#000000" 
swremote="swSaveEnabled="true" 
swVolume="true" swRestart="false" 
swPausePlay="false" 
swFastForward="false" 
swTitle="Habbo Hotel" 
swContextMenu="true" " 
swstretchstyle="none"

swtext='.$server_ip.':'.$server_port.'" 
<?php echo '
type="application/x-director" 
pluginspage="http://www.macromedia.com/shockwave/download/" height="540" 
width="720">
</embed></OBJECT>  ';
    

    
} else {
print '

<pre><br><br>
  Nom ou pass invalide !<br><br>
  <a href="index.php"><< Retour</a>
</pre>
;

}

?>


</td></tr><tr>
<!-- MENU -->
<td class="menu_box">
<a href="index.php"><input type="button" value="Login"></a>&nbsp;
<a href="news.php"><input type="button" value="News"></a>&nbsp;
</td></tr></table>
<small>Copyright &copy; 2010 - 2011 Wirbelwind</small>
</center>
</body></html>