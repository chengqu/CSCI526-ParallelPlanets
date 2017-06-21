@script ExecuteInEditMode;

var PointParticle:ParticleSystem;
var SoundDinamic:GameObject;
private var RegistrTrue:float=0;
private var pusk:int=0;
var ObjectCreatFinish:GameObject;



function OnTriggerEnter(other:Collider) {
if (other.gameObject.name=="Player"){
StartRegistration();
}
}


function StartRegistration () {
if (RegistrTrue==0) {
SoundDinamic.SetActive(false);
SoundDinamic.SetActive(true);
pusk=1;
RegistrTrue=1;
}
}



function FixedUpdate(){
if(pusk==1){
if(PointParticle.startColor.b>0){
PointParticle.startColor.b=PointParticle.startColor.b-0.077;
}else{
pusk=0;
var Fin:NextLevels=ObjectCreatFinish.GetComponent(NextLevels);
Fin.NawSave++;
}
}
}