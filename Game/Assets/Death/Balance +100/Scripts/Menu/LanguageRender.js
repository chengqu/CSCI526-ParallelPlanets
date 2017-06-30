@script ExecuteInEditMode;

private var SaveLanguages:int;
var Loop:boolean=false;
var Spriter:boolean=false;
var SpriteLoop:boolean=false;

var ObjectEng:GameObject;
var ObjectRus:GameObject;

var GameSprite:SpriteRenderer;
var SprEng:Sprite;
var SprRus:Sprite;


function Start(){
SaveLanguages=(PlayerPrefs.GetFloat('SaveLanguages') );
if(Spriter==false){
if(SaveLanguages==0){
ObjectRus.layer=8;
ObjectEng.layer=0;
}else{
ObjectRus.layer=0;
ObjectEng.layer=8;
}
}

if(Spriter){
if(SaveLanguages==0){
GameSprite.sprite=SprEng;
}else{
GameSprite.sprite=SprRus;
}
}
}



function Update(){
if(Loop){
SaveLanguages=(PlayerPrefs.GetFloat('SaveLanguages') );
if(SaveLanguages==0){
ObjectRus.layer=8;
ObjectEng.layer=0;
}else{
ObjectRus.layer=0;
ObjectEng.layer=8;
}
}
if(SpriteLoop){
SaveLanguages=(PlayerPrefs.GetFloat('SaveLanguages') );
if(SaveLanguages==0){
GameSprite.sprite=SprEng;
}else{
GameSprite.sprite=SprRus;
}
}
}