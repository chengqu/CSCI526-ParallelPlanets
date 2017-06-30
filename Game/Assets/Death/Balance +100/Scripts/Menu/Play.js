@script ExecuteInEditMode;



private var ThisPlay:boolean=true;


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

var Levels:GameObject;
var LevelsSpr:SpriteRenderer;
var LevelsSprGrid:SpriteRenderer;

var Anl_1:SpriteRenderer;
var Anl_2:SpriteRenderer;
var Anl_3:SpriteRenderer;
var Anl_4:SpriteRenderer;
var Anl_5:SpriteRenderer;
var Anl_6:SpriteRenderer;
var Anl_7:SpriteRenderer;
var Anl_8:SpriteRenderer;
var Anl_9:SpriteRenderer;
var Anl_10:SpriteRenderer;
var Anl_11:SpriteRenderer;
var Anl_12:SpriteRenderer;
var Anl_13:SpriteRenderer;
var Anl_14:SpriteRenderer;
var Anl_15:SpriteRenderer;
var Anl_16:SpriteRenderer;

var Back:GameObject;
var BackSpr:SpriteRenderer;

var Next:GameObject;
var NextSpr:SpriteRenderer;

private var SaveEvaluate:int;
private var CoollFalse:int=0;
private var TapRegistr:int=0;


function OnMouseDown(){
if(Play.color.a>0.87){
if(ThisPlay){
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
Levels.SetActive(true);
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
}else{
if(LevelsSpr.color.a<1){
LevelsSpr.color.a=LevelsSpr.color.a+0.1;
LevelsSprGrid.color.a=LevelsSprGrid.color.a+0.1;
BackSpr.color.a=BackSpr.color.a+0.1;
NextSpr.color.a=NextSpr.color.a+0.1;
Anl_1.color.a=Anl_1.color.a+0.1;
Anl_2.color.a=Anl_2.color.a+0.1;
Anl_3.color.a=Anl_3.color.a+0.1;
Anl_4.color.a=Anl_4.color.a+0.1;
Anl_5.color.a=Anl_5.color.a+0.1;
Anl_6.color.a=Anl_6.color.a+0.1;
Anl_7.color.a=Anl_7.color.a+0.1;
Anl_8.color.a=Anl_8.color.a+0.1;
Anl_9.color.a=Anl_9.color.a+0.1;
Anl_10.color.a=Anl_10.color.a+0.1;
Anl_11.color.a=Anl_11.color.a+0.1;
Anl_12.color.a=Anl_12.color.a+0.1;
Anl_13.color.a=Anl_13.color.a+0.1;
Anl_14.color.a=Anl_14.color.a+0.1;
Anl_15.color.a=Anl_15.color.a+0.1;
Anl_16.color.a=Anl_16.color.a+0.1;
}
else{
Back.GetComponent(BoxCollider2D).enabled=true;
Next.GetComponent(BoxCollider2D).enabled=true;
TapRegistr=0;
}
}
}
}
}