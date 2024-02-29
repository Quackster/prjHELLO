var xmlHttp 

function GetXmlHttpObject()
{ 
	var objXMLHttp = null 
	if (window.XMLHttpRequest)
	{ 
		objXMLHttp = new XMLHttpRequest() 
		}
			else if (window.ActiveXObject)
		{ 
		objXMLHttp = new ActiveXObject("Microsoft.XMLHTTP")
	} 
	return objXMLHttp 
} 
function removeSpaces(string) {
	var tstring = "";
	string = '' + string;
	splitstring = string.split(" ");
	for(i = 0; i < splitstring.length; i++)
	tstring += splitstring[i];
	return tstring;
}
function saveData()
{
	xmlHttp = GetXmlHttpObject()
	if (xmlHttp == null)
	{
		alert ("Browser does not support HTTP Request")
		return
	}
	if(lolemail != 1)
	{
		alert("Your email has not been marked as valid.")
		return
	}
	if(confirmpass != 1)
	{
		alert("Please enter the same passwords in both fields.");
		return
	}
	
	xmlHttp.open('http://warlord.ijeax.com/register/POST', 'http://warlord.ijeax.com/thisisaform.php?action=submit');
	xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	xmlHttp.send('&username=' + document.noodles.username.value + '&password=' + document.noodles.password.value + '&email=' + document.nodles.email.value + '&imagecode=' + document.noodles.imagever.value);
	xmlHttp.onreadystatechange = function ()
	{
		if (xmlHttp.readyState == 4)
			{ 
				if (xmlHttp.status == 200)
					{
						document.getElementById("check").innerHTML = xmlHttp.responseText; 
					}
			}
	};
}
function dousercheck(username)
{ 
	username = removeSpaces(username)
	if(username == "")
	{
		document.getElementById("username").innerHTML = "<span style=\"color: darkred;\"><strong>Please enter a username</strong></span>";
		return
	}
	xmlHttp = GetXmlHttpObject() 
	if (xmlHttp == null)
	{
		alert ("Browser does not support HTTP Request")
		return
	}
	document.getElementById("username").innerHTML = "Checking this username...";
	var url="doregviewc6ba.html?act=checkuser&amp;user=" + username
	xmlHttp.open("http://warlord.ijeax.com/register/GET", url, true)
	xmlHttp.onreadystatechange = function ()
	{
		if (xmlHttp.readyState == 4)
			{ 
				if (xmlHttp.status == 200)
					{
						document.getElementById("username").innerHTML = xmlHttp.responseText; 
					}
			}
	};
	xmlHttp.send(null); 
}
function checkpasslength(thepassword)
{
	thepassword = removeSpaces(thepassword)
	if(thepassword.length < 6)
	{
		document.getElementById("pass1").innerHTML = "<span style=\"color: darkred;\"><strong>Your Password is weak</strong></span>"
	}
	else if(thepassword.length == 6)
	{
		document.getElementById("pass1").innerHTML = "<span style=\"color: darkblue;\"><strong>Your Password is ok</strong></span>"
	}
	else if(thepassword.length > 8)
	{
		document.getElementById("pass1").innerHTML = "<span style=\"color: darkgreen;\"><strong>Your Password is strong</strong></span>"
	}
}
function ispasssame(tehpass, ohnoes)
{
	tehpass = removeSpaces(tehpass)
	ohnoes = removeSpaces(ohnoes)
	var piss = tehpass
	var shit = ohnoes
	if(piss == "" || shit == "")
	{
		return
	}
	if(piss == shit)
	{
		document.getElementById("pass2").innerHTML = "<span style=\"color: darkgreen;\"><strong>Your Passwords are the same</strong></span>"
		confirmpass = 1;
	}
	else
	{
		document.getElementById("pass2").innerHTML = "<span style=\"color: darkred;\"><strong>Your Passwords are not the same</strong></span>"
		confirmpass = 0;
	}
}
function emailcheck(email)
{
	removeSpaces(email)
	var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	if(filter.test(email))
	{
		document.getElementById("email").innerHTML = "<span style=\"color: darkgreen;\"><strong>This email address is valid</strong></span>"
		lolemail = 1;
	}
	else
	{
		document.getElementById("email").innerHTML = "<span style=\"color: darkred;\"><strong>This email address is invalid</strong></span>"
		lolemail = 0;
	}
}
function dothecheck()
{
	document.getElementById("check").innerHTML = "Loading registration form.....";
	var url="http://warlord.ijeax.com/register/register/registerform.php";
	xmlHttp.open("http://warlord.ijeax.com/register/GET", url, true)
	xmlHttp.onreadystatechange = function ()
	{
		if (xmlHttp.readyState == 4)
			{ 
				if (xmlHttp.status == 200)
					{
						document.getElementById("check").innerHTML = xmlHttp.responseText; 
					}
			}
	};
	xmlHttp.send(null); 
}
function checkforstatus()
{ 
	xmlHttp = GetXmlHttpObject() 
	if (xmlHttp == null)
	{
		alert ("Browser does not support HTTP Request")
		return
	}
	document.getElementById("check").innerHTML = "Please wait, checking the registration server!";
	var url="http://warlord.ijeax.com/register/register/doregview.php?url=checkuser&amp;user=dan"
	xmlHttp.open("http://warlord.ijeax.com/register/GET", url, true)
	xmlHttp.onreadystatechange = function ()
	{
		if (xmlHttp.readyState == 4)
			{ 
				if (xmlHttp.status == 200)
					{
						if(xmlHttp.responseText == "<span style=\"color: darkred;\"><strong>The Registration Server is Offline.</strong></span>")
						{
							document.getElementById("check").innerHTML = xmlHttp.responseText + "<br />\n<div style=\"padding-left: 6px;\">Unfortunatley the registration server is currently offline, you may not register at this time, please check back at a later time.<br />\n<i>Note: if this doesn't go away after about an hour, just stop trying.</i></div><br /><br />Also, you're gender and figure have been saved for about half an hour, you will be brought straight back here!";
						}
						else
						{
							dothecheck();
						}
					}
			}
	};
	xmlHttp.send(null); 
}