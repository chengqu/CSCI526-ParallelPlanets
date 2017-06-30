@script ExecuteInEditMode;

var SoundsYes:GameObject;
var SoundsNo:GameObject;
private var SaveSound:float;


function Start () {
if (PlayerPrefs.HasKey("SaveSound")){
SaveSound=(PlayerPrefs.GetFloat('SaveSound') );
}else {SaveSound=1;}
SaveInform ();

GlobalControl();
if(SaveSound==1){
AudioListener.pause=false;
}
}

function OnMouseDown () {
if (SaveSound==0) {SaveSound=1;}
else {SaveSound=0;}
SaveInform ();
GlobalControl();
}

function SaveInform () {
PlayerPrefs.SetFloat("SaveSound",SaveSound);
}



function GlobalControl () {
if (SaveSound==0){
AudioListener.pause=true;
SoundsYes.SetActive(false);
SoundsNo.SetActive(true);
}
else {
AudioListener.pause=false;
SoundsYes.SetActive(true);
SoundsNo.SetActive(false);
}
}