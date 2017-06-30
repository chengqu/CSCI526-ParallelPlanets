@script ExecuteInEditMode;

private var DefaultBool:boolean=true;
private var TapRegistr:int=0;


var PlayGo:GameObject;
var Play:SpriteRenderer;

var MusicGo:GameObject;
var MusicOn:SpriteRenderer;
var MusicOff:SpriteRenderer;

var EffectsGo:GameObject;
var EffectsOn:SpriteRenderer;
var EffectsOff:SpriteRenderer;

var Language:GameObject;
var Rus:SpriteRenderer;
var Eng:SpriteRenderer;

var ExitGo:GameObject;
var Exit:SpriteRenderer;

var AssesNo:GameObject;
var AssesYes:GameObject;
var Asses:SpriteRenderer;

private var SaveEvaluate:int;
private var CoollFalse:int=0;


function Start(){
if (PlayerPrefs.HasKey("Evaluate")){
SaveEvaluate=(PlayerPrefs.GetFloat('Evaluate') );
}
else {SaveEvaluate=0;}
}


function OnMouseDown(){
if(Play.color.a>0.87){
if(DefaultBool){
SaveEvaluate=0;
CoollFalse=0;
TapRegistr=1;
}
}
}

function FixedUpdate(){
if(TapRegistr==1){
if(SaveEvaluate==0){
if(CoollFalse==0){
CoollFalse=1;
PlayGo.GetComponent(BoxCollider2D).enabled=false;
MusicGo.GetComponent(BoxCollider2D).enabled=false;
EffectsGo.GetComponent(BoxCollider2D).enabled=false;
ExitGo.GetComponent(BoxCollider2D).enabled=false;
Language.GetComponent(BoxCollider2D).enabled=false;
}
if(Exit.color.a>0){
Exit.color.a=Exit.color.a-0.1;
Play.color.a=Exit.color.a-0.1;
MusicOn.color.a=Exit.color.a-0.1;
MusicOff.color.a=Exit.color.a-0.1;
EffectsOn.color.a=Exit.color.a-0.1;
EffectsOff.color.a=Exit.color.a-0.1;
Rus.color.a=Exit.color.a-0.1;
Eng.color.a=Exit.color.a-0.1;
}
else{
if(Asses.color.a<1){
Asses.color.a=Asses.color.a+0.1;
}
else{
AssesNo.GetComponent(BoxCollider2D).enabled=true;
AssesYes.GetComponent(BoxCollider2D).enabled=true;
TapRegistr=0;
}
}
}
else{
if(CoollFalse==0){
CoollFalse=1;
PlayGo.GetComponent(BoxCollider2D).enabled=false;
MusicGo.GetComponent(BoxCollider2D).enabled=false;
ExitGo.GetComponent(BoxCollider2D).enabled=false;
EffectsGo.GetComponent(BoxCollider2D).enabled=false;
Language.GetComponent(BoxCollider2D).enabled=false;
}
if(Exit.color.a>0){
Exit.color.a=Exit.color.a-0.1;
Play.color.a=Exit.color.a-0.1;
MusicOn.color.a=Exit.color.a-0.1;
MusicOff.color.a=Exit.color.a-0.1;
EffectsOn.color.a=Exit.color.a-0.1;
EffectsOff.color.a=Exit.color.a-0.1;
Rus.color.a=Exit.color.a-0.1;
Eng.color.a=Exit.color.a-0.1;
}
else{
TapRegistr=0;
Application.Quit();
}
}
}
}