function sroll()
{
	var oDiv=document.getElementById('Activity_box');
	var oUl=oDiv.getElementsByTagName('ul')[0];
	var aLi=oUl.getElementsByTagName('li');
	var aA1=document.getElementById('pre_l');
	var aA2=document.getElementById('pre_r');
	var timer=null;
	var iSpeed=-1;
	
	oUl.innerHTML+=oUl.innerHTML;
	oUl.style.width=aLi.length*aLi[0].offsetWidth+'px';
	
    function fnMove(){
		if(oUl.offsetLeft<-oUl.offsetWidth/2)
		{
			oUl.style.left=0;
		}
		else if(oUl.offsetLeft>0)
		{
			oUl.style.left=-oUl.offsetWidth/2+'px';
		}
		oUl.style.left=oUl.offsetLeft+iSpeed+'px';
	}
	
	timer=setInterval(fnMove, 30);
	
	aA1.onclick=function ()
	{
		iSpeed=-1;
	};
	aA2.onclick=function ()
	{
		iSpeed=1;
	};
	
	oDiv.onmouseover=function ()
	{
		clearInterval(timer);
	};
	
	oDiv.onmouseout=function ()
	{
		timer=setInterval(fnMove, 30);
	};
};