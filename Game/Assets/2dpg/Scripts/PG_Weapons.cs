using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	[System.Serializable]
	public class PG_Weapons {

		public string vName = "";
		public Sprite vWeaponIcon = null;						//show the right weapon at the bottom
		public Sprite vSprite = null;							//show the weapon on the character. Will Flip when facing Left

		public GameObject vProjectile = null;
		public bool UseGravity = true;							//if true, the projectile will go straight and follow the planet. If not, you can aim with the weapon as you want.
		public int vDmgValue = 1;								//configure the dmg here for current projectile.
	}
}