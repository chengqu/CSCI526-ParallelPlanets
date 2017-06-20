@script ExecuteInEditMode;

var ThisBazaNumber:int=1;//Position Object;
private var ThisBazaPosition:Vector3;
private var RegistrationBool:int=0;
private var SavePos:int;
private var EndX:int=0;
private var EndZ:int=0;
var Loop:int=0;


var ObjectTransform:GameObject;
var Speed:float;

var Baza1:Vector3;
var Baza2:Vector3;
var Baza3:Vector3;
var Baza4:Vector3;
var Baza5:Vector3;
var Baza6:Vector3;
var Baza7:Vector3;
var Baza8:Vector3;
var Baza9:Vector3;
var Baza0:Vector3;


function Start (){
ControllBazaGo();
}


function FixedUpdate() {

//Zero Faza
if (RegistrationBool==0){
if (ObjectTransform.transform.position.x>ThisBazaPosition.x && 
ObjectTransform.transform.position.z>ThisBazaPosition.z) {
SavePos=1;Deactiv();
}
//2
if (ObjectTransform.transform.position.x>ThisBazaPosition.x && 
ObjectTransform.transform.position.z<ThisBazaPosition.z) {
SavePos=2;Deactiv();
}
//3
if (ObjectTransform.transform.position.x<ThisBazaPosition.x && 
ObjectTransform.transform.position.z<ThisBazaPosition.z) {
SavePos=3;Deactiv();
}
//4
if (ObjectTransform.transform.position.x<ThisBazaPosition.x && 
ObjectTransform.transform.position.z>ThisBazaPosition.z) {
SavePos=4;Deactiv();
}
}




//1
if (SavePos==1){
if (ObjectTransform.transform.position.x>ThisBazaPosition.x && 
ObjectTransform.transform.position.z>ThisBazaPosition.z) {
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x-Speed;
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z-Speed;
}
else {
if (ObjectTransform.transform.position.x>ThisBazaPosition.x){
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x-Speed;
}else {EndX=1;RegistrationEnd();}
if (ObjectTransform.transform.position.z>ThisBazaPosition.z) {
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z-Speed;
}else {EndZ=1;RegistrationEnd();}
}
}
//2
if (SavePos==2){
if (ObjectTransform.transform.position.x>ThisBazaPosition.x && 
ObjectTransform.transform.position.z<ThisBazaPosition.z) {
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x-Speed;
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z+Speed;
}
else {
if (ObjectTransform.transform.position.x>ThisBazaPosition.x){
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x-Speed;
}else {EndX=1;RegistrationEnd();}
if (ObjectTransform.transform.position.z<ThisBazaPosition.z) {
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z+Speed;
}else {EndZ=1;RegistrationEnd();}
}
}
//3
if (SavePos==3){
if (ObjectTransform.transform.position.x<ThisBazaPosition.x && 
ObjectTransform.transform.position.z<ThisBazaPosition.z) {
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x+Speed;
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z+Speed;
}
else {
if (ObjectTransform.transform.position.x<ThisBazaPosition.x){
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x+Speed;
}else {EndX=1;RegistrationEnd();}
if (ObjectTransform.transform.position.z<ThisBazaPosition.z) {
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z+Speed;
}else {EndZ=1;RegistrationEnd();}
}
}
//4
if (SavePos==4){
if (ObjectTransform.transform.position.x<ThisBazaPosition.x && 
ObjectTransform.transform.position.z>ThisBazaPosition.z) {
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x+Speed;
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z-Speed;
}
else {
if (ObjectTransform.transform.position.x<ThisBazaPosition.x){
ObjectTransform.transform.position.x=ObjectTransform.transform.position.x+Speed;
}else {EndX=1;RegistrationEnd();}
if (ObjectTransform.transform.position.z>ThisBazaPosition.z) {
ObjectTransform.transform.position.z=ObjectTransform.transform.position.z-Speed;
}else {EndZ=1;RegistrationEnd();}
}
}

}

function ControllBazaGo(){

if (ThisBazaNumber==0){
ThisBazaPosition.x=Baza1.x;
ThisBazaPosition.z=Baza1.z;
}
if (ThisBazaNumber==1){
ThisBazaPosition.x=Baza2.x;
ThisBazaPosition.z=Baza2.z;
}
if (ThisBazaNumber==2){
ThisBazaPosition.x=Baza3.x;
ThisBazaPosition.z=Baza3.z;
}
if (ThisBazaNumber==3){
ThisBazaPosition.x=Baza4.x;
ThisBazaPosition.z=Baza4.z;
}
if (ThisBazaNumber==4){
ThisBazaPosition.x=Baza5.x;
ThisBazaPosition.z=Baza5.z;
}
if (ThisBazaNumber==5){
ThisBazaPosition.x=Baza6.x;
ThisBazaPosition.z=Baza6.z;
}
if (ThisBazaNumber==6){
ThisBazaPosition.x=Baza7.x;
ThisBazaPosition.z=Baza7.z;
}
if (ThisBazaNumber==7){
ThisBazaPosition.x=Baza8.x;
ThisBazaPosition.z=Baza8.z;
}
if (ThisBazaNumber==8){
ThisBazaPosition.x=Baza9.x;
ThisBazaPosition.z=Baza9.z;
}
if (ThisBazaNumber==9){
ThisBazaPosition.x=Baza0.x;
ThisBazaPosition.z=Baza0.z;
}

}

function RegistrationEnd(){
if (EndX==1&&EndZ==1){
Deactiv();
SavePos=0;
RegistrationBool=0;
Loop=0;

if (ThisBazaNumber==9){ThisBazaNumber=0;Loop=1;}
if (ThisBazaNumber==8){ThisBazaNumber=9;}
if (ThisBazaNumber==7){ThisBazaNumber=8;}
if (ThisBazaNumber==6){ThisBazaNumber=7;}
if (ThisBazaNumber==5){ThisBazaNumber=6;}
if (ThisBazaNumber==4){ThisBazaNumber=5;}
if (ThisBazaNumber==3){ThisBazaNumber=4;}
if (ThisBazaNumber==2){ThisBazaNumber=3;}
if (ThisBazaNumber==1){ThisBazaNumber=2;}
if (ThisBazaNumber==0&&Loop==0){ThisBazaNumber=1;}









ControllBazaGo();
}
}


function Deactiv() {
RegistrationBool=1;
EndX=0;
EndZ=0;
}

