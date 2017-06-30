@script ExecuteInEditMode;



//Yes
private var TransformZero:float=0;
private var OneChance:int=0;
private var Load:int;
var LittlePlayer:ParticleSystem;
var InformatorLoad:int;
var LevelNext:int;
var DinamisFinish:GameObject;
var BlackFon:SpriteRenderer;
var FinishStart:GameObject;
var PlayerObject:GameObject;

function OnTriggerEnter(other:Collider) {
if (other.gameObject.name=="Player"){
if (FinishStart.layer==0){
if (OneChance==0){
OneChance=1;
DinamisFinish.SetActive(false);
DinamisFinish.SetActive(true);
PlayerObject.GetComponent(SphereCollider).enabled=false;
TransformZero=1;
}
}
}
}



function FixedUpdate () {
if (InformatorLoad==1) {
InformatorLoad=-1;
FinishStart.layer=0;
}

if (TransformZero==1) {
PlayerObject.transform.position.x=FinishStart.transform.position.x;
PlayerObject.transform.position.z=FinishStart.transform.position.z;
if(LittlePlayer.startColor.r>0){
LittlePlayer.startColor.r=LittlePlayer.startColor.r-0.03;
}
if(LittlePlayer.startColor.g>0){
LittlePlayer.startColor.g=LittlePlayer.startColor.g-0.03;
}
if(LittlePlayer.startColor.b>0){
LittlePlayer.startColor.b=LittlePlayer.startColor.b-0.03;
}else{
if(BlackFon.color.a<1){
BlackFon.color.a=BlackFon.color.a+0.05;
}else{
LoadNewLewel();
}
}
}
}


function  LoadNewLewel() {
Load=PlayerPrefs.GetFloat('LoadLevels');
if(Load<LevelNext){PlayerPrefs.SetFloat('LoadLevels',LevelNext);}
Application.LoadLevel(LevelNext);
}