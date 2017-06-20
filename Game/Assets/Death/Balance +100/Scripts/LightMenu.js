@script ExecuteInEditMode;

var LightObj:GameObject;
var BigCircle:boolean=false;
private var BigLoop:int=0;

function FixedUpdate(){
if(BigCircle){
if(BigLoop==0){
if(LightObj.transform.position.x<3){
LightObj.transform.position.x=LightObj.transform.position.x+0.17;
}else{BigLoop=1;}
}
if(BigLoop==1){
if(LightObj.transform.position.y>-3){
LightObj.transform.position.y=LightObj.transform.position.y-0.17;
}else{BigLoop=2;}
}
if(BigLoop==2){
if(LightObj.transform.position.x>-3){
LightObj.transform.position.x=LightObj.transform.position.x-0.17;
}else{BigLoop=3;}
}
if(BigLoop==3){
if(LightObj.transform.position.y<3){
LightObj.transform.position.y=LightObj.transform.position.y+0.17;
}else{BigLoop=0;}
}
}
}


