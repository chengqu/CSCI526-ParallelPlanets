@script ExecuteInEditMode;

var Click_1:GameObject;
var Click_2:GameObject;
var Click_3:GameObject;
var Click_4:GameObject;
var Click_5:GameObject;
var Click_6:GameObject;
var Click_7:GameObject;
var Click_8:GameObject;
var Click_9:GameObject;
var Click_10:GameObject;
var Click_11:GameObject;
var Click_12:GameObject;
var Click_13:GameObject;
var Click_14:GameObject;
var Click_15:GameObject;
var Click_16:GameObject;
var Click_17:GameObject;
var Click_18:GameObject;
var Click_19:GameObject;
var Click_20:GameObject;
var Click_21:GameObject;
var Click_22:GameObject;
var Click_23:GameObject;
var Click_24:GameObject;
var Click_25:GameObject;
var Click_26:GameObject;
var Click_27:GameObject;
var Click_28:GameObject;
var Click_29:GameObject;
var Click_30:GameObject;
var Click_31:GameObject;
var Click_32:GameObject;
var Click_33:GameObject;
var Click_34:GameObject;
var Click_35:GameObject;
var Click_36:GameObject;
var Click_37:GameObject;
var Click_38:GameObject;
var Click_39:GameObject;
var Click_40:GameObject;
var Click_41:GameObject;
var Click_42:GameObject;
var Click_43:GameObject;
var Click_44:GameObject;
var Click_45:GameObject;
var Click_46:GameObject;
var Click_47:GameObject;
var Click_48:GameObject;
var Click_49:GameObject;
var Click_50:GameObject;
var Click_51:GameObject;
var Click_52:GameObject;
var Click_53:GameObject;
var Click_54:GameObject;
var Click_55:GameObject;
var Click_56:GameObject;
var Click_57:GameObject;
var Click_58:GameObject;
var Click_59:GameObject;
var Click_60:GameObject;
var Click_61:GameObject;
var Click_62:GameObject;
var Click_63:GameObject;
var Click_64:GameObject;

private var Loadnumber:int;


function Start(){
if (PlayerPrefs.HasKey("LoadLevels")){
Loadnumber=(PlayerPrefs.GetFloat('LoadLevels') );
}else {Loadnumber=1;}
Open();
}

function Open(){
if(Loadnumber>=1){Click_1.SetActive(true);}else{Click_1.SetActive(false);}
if(Loadnumber>=2){Click_2.SetActive(true);}else{Click_2.SetActive(false);}
if(Loadnumber>=3){Click_3.SetActive(true);}else{Click_3.SetActive(false);}
if(Loadnumber>=4){Click_4.SetActive(true);}else{Click_4.SetActive(false);}
if(Loadnumber>=5){Click_5.SetActive(true);}else{Click_5.SetActive(false);}
if(Loadnumber>=6){Click_6.SetActive(true);}else{Click_6.SetActive(false);}
if(Loadnumber>=7){Click_7.SetActive(true);}else{Click_7.SetActive(false);}
if(Loadnumber>=8){Click_8.SetActive(true);}else{Click_8.SetActive(false);}
if(Loadnumber>=9){Click_9.SetActive(true);}else{Click_9.SetActive(false);}
if(Loadnumber>=10){Click_10.SetActive(true);}else{Click_10.SetActive(false);}
if(Loadnumber>=11){Click_11.SetActive(true);}else{Click_11.SetActive(false);}
if(Loadnumber>=12){Click_12.SetActive(true);}else{Click_12.SetActive(false);}
if(Loadnumber>=13){Click_13.SetActive(true);}else{Click_13.SetActive(false);}
if(Loadnumber>=14){Click_14.SetActive(true);}else{Click_14.SetActive(false);}
if(Loadnumber>=15){Click_15.SetActive(true);}else{Click_15.SetActive(false);}
if(Loadnumber>=16){Click_16.SetActive(true);}else{Click_16.SetActive(false);}
if(Loadnumber>=17){Click_17.SetActive(true);}else{Click_17.SetActive(false);}
if(Loadnumber>=18){Click_18.SetActive(true);}else{Click_18.SetActive(false);}
if(Loadnumber>=19){Click_19.SetActive(true);}else{Click_19.SetActive(false);}
if(Loadnumber>=20){Click_20.SetActive(true);}else{Click_20.SetActive(false);}
if(Loadnumber>=21){Click_21.SetActive(true);}else{Click_21.SetActive(false);}
if(Loadnumber>=22){Click_22.SetActive(true);}else{Click_22.SetActive(false);}
if(Loadnumber>=23){Click_23.SetActive(true);}else{Click_23.SetActive(false);}
if(Loadnumber>=24){Click_24.SetActive(true);}else{Click_24.SetActive(false);}
if(Loadnumber>=25){Click_25.SetActive(true);}else{Click_25.SetActive(false);}
if(Loadnumber>=26){Click_26.SetActive(true);}else{Click_26.SetActive(false);}
if(Loadnumber>=27){Click_27.SetActive(true);}else{Click_27.SetActive(false);}
if(Loadnumber>=28){Click_28.SetActive(true);}else{Click_28.SetActive(false);}
if(Loadnumber>=29){Click_29.SetActive(true);}else{Click_29.SetActive(false);}
if(Loadnumber>=30){Click_30.SetActive(true);}else{Click_30.SetActive(false);}
if(Loadnumber>=31){Click_31.SetActive(true);}else{Click_31.SetActive(false);}
if(Loadnumber>=32){Click_32.SetActive(true);}else{Click_32.SetActive(false);}
if(Loadnumber>=33){Click_33.SetActive(true);}else{Click_33.SetActive(false);}
if(Loadnumber>=34){Click_34.SetActive(true);}else{Click_34.SetActive(false);}
if(Loadnumber>=35){Click_35.SetActive(true);}else{Click_35.SetActive(false);}
if(Loadnumber>=36){Click_36.SetActive(true);}else{Click_36.SetActive(false);}
if(Loadnumber>=37){Click_37.SetActive(true);}else{Click_37.SetActive(false);}
if(Loadnumber>=38){Click_38.SetActive(true);}else{Click_38.SetActive(false);}
if(Loadnumber>=39){Click_39.SetActive(true);}else{Click_39.SetActive(false);}
if(Loadnumber>=40){Click_40.SetActive(true);}else{Click_40.SetActive(false);}
if(Loadnumber>=41){Click_41.SetActive(true);}else{Click_41.SetActive(false);}
if(Loadnumber>=42){Click_42.SetActive(true);}else{Click_42.SetActive(false);}
if(Loadnumber>=43){Click_43.SetActive(true);}else{Click_43.SetActive(false);}
if(Loadnumber>=44){Click_44.SetActive(true);}else{Click_44.SetActive(false);}
if(Loadnumber>=45){Click_45.SetActive(true);}else{Click_45.SetActive(false);}
if(Loadnumber>=46){Click_46.SetActive(true);}else{Click_46.SetActive(false);}
if(Loadnumber>=47){Click_47.SetActive(true);}else{Click_47.SetActive(false);}
if(Loadnumber>=48){Click_48.SetActive(true);}else{Click_48.SetActive(false);}
if(Loadnumber>=49){Click_49.SetActive(true);}else{Click_49.SetActive(false);}
if(Loadnumber>=50){Click_50.SetActive(true);}else{Click_50.SetActive(false);}
if(Loadnumber>=51){Click_51.SetActive(true);}else{Click_51.SetActive(false);}
if(Loadnumber>=52){Click_52.SetActive(true);}else{Click_52.SetActive(false);}
if(Loadnumber>=53){Click_53.SetActive(true);}else{Click_53.SetActive(false);}
if(Loadnumber>=54){Click_54.SetActive(true);}else{Click_54.SetActive(false);}
if(Loadnumber>=55){Click_55.SetActive(true);}else{Click_55.SetActive(false);}
if(Loadnumber>=56){Click_56.SetActive(true);}else{Click_56.SetActive(false);}
if(Loadnumber>=57){Click_57.SetActive(true);}else{Click_57.SetActive(false);}
if(Loadnumber>=58){Click_58.SetActive(true);}else{Click_58.SetActive(false);}
if(Loadnumber>=59){Click_59.SetActive(true);}else{Click_59.SetActive(false);}
if(Loadnumber>=60){Click_60.SetActive(true);}else{Click_60.SetActive(false);}
if(Loadnumber>=61){Click_61.SetActive(true);}else{Click_61.SetActive(false);}
if(Loadnumber>=62){Click_62.SetActive(true);}else{Click_62.SetActive(false);}
if(Loadnumber>=63){Click_63.SetActive(true);}else{Click_63.SetActive(false);}
if(Loadnumber>=64){Click_64.SetActive(true);}else{Click_64.SetActive(false);}
}

