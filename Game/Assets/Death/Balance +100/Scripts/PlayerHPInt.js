@script ExecuteInEditMode;

//Yes
var HitsDeleting:float;
private var sumHits:float=100;
private var nawHits:float;
private var savHits:float;
private var FloatTrans:Vector2;
private var SavePosYes:int=0;
private var pusk:int=0;
var posHits:float;
var LevelReload:int;
var Information:GameObject;
var Player:GameObject;
var Hvost:GameObject;
var Explozion:GameObject;
var SpriteFon:SpriteRenderer;
var CameraEffects:GameObject;
var SoundDelHp:GameObject;

private var Pops:int=0;

function Start(){nawHits=sumHits;savHits=sumHits;
Pops=PlayerPrefs.GetFloat('Pops');
PlayerPrefs.SetFloat('Pops',Pops);
}


function Reload () {
Explozion.SetActive(true);
yield WaitForSeconds(0.3);
Hvost.SetActive(false);
}


function FixedUpdate () {
//Process Sound
if (nawHits<savHits){
savHits=nawHits;
SoundDelHp.SetActive(false);
SoundDelHp.SetActive(true);
}
//Process 1
nawHits=nawHits-HitsDeleting;
posHits=nawHits*4.3/100;
//Process 2
if (nawHits<=0) {nawHits=0;}
//Process 3


Information.transform.localScale.x=nawHits/100;
Information.transform.localPosition.x=-2.1+nawHits*2.1/100;

//Process 4
if (nawHits==0) {
if(SavePosYes==0){
FloatTrans.x=Player.transform.position.x;
FloatTrans.y=Player.transform.position.z;
SavePosYes=1;
minipusk();
Player.GetComponent(SphereCollider).enabled=false;
Reload();
}
Player.transform.position.x=FloatTrans.x;
Player.transform.position.z=FloatTrans.y;
}
if (SavePosYes==1){
if(pusk==1){
if (SpriteFon.color.a<1) {
SpriteFon.color.a=SpriteFon.color.a+0.037;
}else{
SavePosYes=-1;
Pops++;
PlayerPrefs.SetFloat('Pops',Pops);
Application.LoadLevel(LevelReload);
}
}
}
}

function minipusk(){
yield WaitForSeconds(1.7);
pusk=1;
}