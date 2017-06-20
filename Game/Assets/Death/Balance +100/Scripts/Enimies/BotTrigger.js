
//In Trigger Zona

//Objects
var Player:GameObject;
var BotOsnova:GameObject;
var BotZona:GameObject;

//Pablik
var SpeedMax:float;
var SpeedUpdate:float;

//Private
private var Speed:float;
private var StartBack:int=0;
private var SpeedUp:int=0;

private var StartPositionX:float;
private var StartPositionZ:float;




function Start () {
StartPositionX=BotZona.transform.position.x;
StartPositionZ=BotZona.transform.position.z;
}





function FixedUpdate () {
if (StartBack==1 ) {
StopAttak();
}

if (StartBack==-1 ) {
PuskAttak () ;

}

if (SpeedUp==1) {
Speedeing();
}



}

function Speedeing() {
if (SpeedUp==1) {
if (Speed<SpeedMax) {
Speed=Speed+SpeedUpdate;
}
}
if (SpeedUp==-1) {
if (Speed>0) {
Speed=Speed-SpeedUpdate;
}
else { SpeedUp=0; }
}

}



//Open


function OnTriggerEnter(other : Collider) {
if (other.gameObject.name=="Player") {
StartBack=-1;
SpeedUp=1;
}
}

function PuskAttak () {

//1
if (BotOsnova.transform.position.x>Player.transform.position.x
&&BotOsnova.transform.position.z>Player.transform.position.z) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x-Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z-Speed;
} 
//2
if (BotOsnova.transform.position.x>Player.transform.position.x
&&BotOsnova.transform.position.z<Player.transform.position.z) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x-Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z+Speed;
}
//3
if (BotOsnova.transform.position.x<Player.transform.position.x
&&BotOsnova.transform.position.z<Player.transform.position.z)  {
BotOsnova.transform.position.x=BotOsnova.transform.position.x+Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z+Speed;
}
//4
if (BotOsnova.transform.position.x<Player.transform.position.x
&&BotOsnova.transform.position.z>Player.transform.position.z) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x+Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z-Speed;
}
}


//Exit


function OnTriggerExit(other : Collider) {
if (other.gameObject.name=="Player") {
StartBack=1; Speed=0;
}
}

function StopAttak () {

//1
if (BotOsnova.transform.position.x>StartPositionX
&&BotOsnova.transform.position.z<StartPositionZ) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x-Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z+Speed;
} 
//2
if (BotOsnova.transform.position.x>StartPositionX
&&BotOsnova.transform.position.z>StartPositionZ) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x-Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z-Speed;
}
 //3
if (BotOsnova.transform.position.x<StartPositionX
&&BotOsnova.transform.position.z>StartPositionZ) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x+Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z-Speed;
}
 //4
if (BotOsnova.transform.position.x<StartPositionX
&&BotOsnova.transform.position.z<StartPositionZ) {
BotOsnova.transform.position.x=BotOsnova.transform.position.x+Speed;
BotOsnova.transform.position.z=BotOsnova.transform.position.z+Speed;
}
//5
if (BotOsnova.transform.position.x>StartPositionX-SpeedMax*2
&&BotOsnova.transform.position.x<StartPositionX+SpeedMax*2
&&BotOsnova.transform.position.z>StartPositionZ-SpeedMax*2
&&BotOsnova.transform.position.z<StartPositionZ+SpeedMax*2 ) {

StartBack=0;
SpeedUp=-1;
Normalized();
}


}

function Normalized() {
BotOsnova.transform.position.x=BotZona.transform.position.x;
BotOsnova.transform.position.z=BotZona.transform.position.z;
}









