@script ExecuteInEditMode;

var EffectsYes:GameObject;
var EffectsNo:GameObject;
var RotationSpace:GameObject;
private var SaveEffects:float;


function Start () {
if (PlayerPrefs.HasKey("SaveEffects")){
SaveEffects=(PlayerPrefs.GetFloat('SaveEffects') );
}else {SaveEffects=1;}
SaveInform ();
GlobalControl();
}

function OnMouseDown () {
if (SaveEffects==0) {SaveEffects=1;}
else {SaveEffects=0;}
SaveInform ();
GlobalControl();
}

function SaveInform () {
PlayerPrefs.SetFloat("SaveEffects",SaveEffects);
}


function GlobalControl () {
if (SaveEffects==0){
EffectsYes.SetActive(false);
EffectsNo.SetActive(true);
RotationSpace.GetComponent(FonRotation).enabled=false;
}
else {
EffectsYes.SetActive(true);
EffectsNo.SetActive(false);
RotationSpace.GetComponent(FonRotation).enabled=true;
}
}