@script ExecuteInEditMode;

var SpeedZero:float;
private var SpeedPlayerX:float;
private var SpeedPlayerZ:float;
private var GetAcselX:float;
private var GetAcselZ:float;
var PlayerCamera:GameObject;
var PlayerParticle:ParticleSystem;
private var LimCameraMin:float=-0.5;
private var LimCameraMax:float=7.3;




function FixedUpdate () {

GetAcselX=Input.acceleration.x;
GetAcselZ=Input.acceleration.y;
SpeedPlayerX=GetAcselX/3.07;
SpeedPlayerZ=GetAcselZ/3.07;



if (gameObject.transform.position.z>LimCameraMin && gameObject.transform.position.z<LimCameraMax) {
PlayerCamera.transform.position.z = gameObject.transform.position.z;
}
if (SpeedZero==0){
gameObject.transform.position.x=gameObject.transform.position.x+SpeedPlayerX;
gameObject.transform.position.z=gameObject.transform.position.z+SpeedPlayerZ;
}else{
gameObject.transform.position.x=gameObject.transform.position.x+SpeedPlayerX/2.07;
gameObject.transform.position.z=gameObject.transform.position.z+SpeedPlayerZ/2.07;
if(PlayerParticle.startColor.r>0.17){
PlayerParticle.startColor.r=PlayerParticle.startColor.r-0.037;
}
if(PlayerParticle.startColor.g>0.17){
PlayerParticle.startColor.g=PlayerParticle.startColor.g-0.037;
}
if(PlayerParticle.startColor.b>0.17){
PlayerParticle.startColor.b=PlayerParticle.startColor.b-0.037;
}
}
}