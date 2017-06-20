@script ExecuteInEditMode;

var Rus:GameObject;
var Eng:GameObject;
private var SaveLanguages:float;

function Start () {
if (PlayerPrefs.HasKey("SaveLanguages")){
SaveLanguages=(PlayerPrefs.GetFloat('SaveLanguages') );
}else {SaveLanguages=0;}
SaveInform ();
GlobalControl();
}

function OnMouseDown () {
if (SaveLanguages==0) {SaveLanguages=1;}
else {SaveLanguages=0;}
SaveInform ();
GlobalControl();
}

function SaveInform () {
PlayerPrefs.SetFloat("SaveLanguages",SaveLanguages);
}


function GlobalControl () {
if (SaveLanguages==1){
Eng.SetActive(false);
Rus.SetActive(true);
}
else {
Eng.SetActive(true);
Rus.SetActive(false);
}
}