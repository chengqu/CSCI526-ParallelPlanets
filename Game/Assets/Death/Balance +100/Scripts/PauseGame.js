@script ExecuteInEditMode;

private var Sound:int;
private var pusk:int=0;
private var coll:int=0;

var ThisScene:int;

var SpriteMiniMenu:SpriteRenderer;
var SpriteFon:SpriteRenderer;
var SpriteBlack:SpriteRenderer;

var ObjectPause:GameObject;
var ObjectContunie:GameObject;
var ObjectRestart:GameObject;
var ObjectOutput:GameObject;

var BoolPause:boolean=false;
var BoolContunie:boolean=false;
var BoolRestart:boolean=false;
var BoolOutPut:boolean=false;



function Start (){
if (PlayerPrefs.HasKey("SaveSound")){
Sound=(PlayerPrefs.GetFloat('SaveSound') );
}
else {Sound=1;}
}



function OnMouseDown(){
if(BoolPause){coll=0; pusk=1;}
if(BoolContunie){coll=0; pusk=-1;Time.timeScale=1;}
if(BoolRestart){coll=0; pusk=2;Time.timeScale=1;}
if(BoolOutPut){coll=0; pusk=2;Time.timeScale=1;}
}

function FixedUpdate(){
if(pusk==1){
if(coll==0){
coll=1;
ObjectPause.GetComponent(BoxCollider).enabled=false;
}
if(SpriteMiniMenu.color.a<1){
SpriteMiniMenu.color.a=SpriteMiniMenu.color.a+0.1;
SpriteFon.color.a=SpriteFon.color.a+0.1;
}else{
if(coll==1){
coll=0;
pusk=0;
ObjectContunie.GetComponent(BoxCollider).enabled=true;
ObjectRestart.GetComponent(BoxCollider).enabled=true;
ObjectOutput.GetComponent(BoxCollider).enabled=true;
if(Sound==1){AudioListener.pause=true;}
Time.timeScale=0;
}
}
}

if(pusk==-1){
if(coll==0){
coll=1;
ObjectContunie.GetComponent(BoxCollider).enabled=false;
ObjectRestart.GetComponent(BoxCollider).enabled=false;
ObjectOutput.GetComponent(BoxCollider).enabled=false;
}
if(SpriteMiniMenu.color.a>0){
SpriteMiniMenu.color.a=SpriteMiniMenu.color.a-0.1;
SpriteFon.color.a=SpriteFon.color.a-0.1;
}else{
if(coll==1){
Time.timeScale=1;
coll=0;
pusk=0;
if(Sound==1){AudioListener.pause=false;}
ObjectPause.GetComponent(BoxCollider).enabled=true;
}
}
}

if(pusk==2){
if(coll==0){
coll=1;
ObjectContunie.GetComponent(BoxCollider).enabled=false;
ObjectRestart.GetComponent(BoxCollider).enabled=false;
ObjectOutput.GetComponent(BoxCollider).enabled=false;
}
if(SpriteMiniMenu.color.a>0){
SpriteMiniMenu.color.a=SpriteMiniMenu.color.a-0.1;
SpriteFon.color.a=SpriteFon.color.a-0.1;
}else{
if(SpriteBlack.color.a<1){
SpriteBlack.color.a=SpriteBlack.color.a+0.1;
}else{
if(coll==1){
coll=0;
pusk=0;
if(Sound==1){AudioListener.pause=false;}
Application.LoadLevel(ThisScene);
}
}
}
}

if(pusk==3){
if(coll==0){
coll=1;
ObjectContunie.GetComponent(BoxCollider).enabled=false;
ObjectRestart.GetComponent(BoxCollider).enabled=false;
ObjectOutput.GetComponent(BoxCollider).enabled=false;
}
if(SpriteMiniMenu.color.a>0){
SpriteMiniMenu.color.a=SpriteMiniMenu.color.a-0.1;
SpriteFon.color.a=SpriteFon.color.a-0.1;
}else{
if(SpriteBlack.color.a<1){
SpriteBlack.color.a=SpriteBlack.color.a+0.1;
}else{
if(coll==1){
Time.timeScale=1;
coll=0;
pusk=0;
if(Sound==1){AudioListener.pause=false;}

Application.LoadLevel(0);
}
}
}
}

}

