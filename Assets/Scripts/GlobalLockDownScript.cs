using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLockDownScript : MonoBehaviour {

	public GameObject meleeWeapon;
	public bool globalLockDown;
	public int enemyNumber;

	private Vector3 sensorPos;
	private bool roomClear;

	void Update () {

		if (enemyNumber > 0)
		{
			globalLockDown = true;
			roomClear = true;
		}
		else
		{
			if (roomClear == true)
			{
				MeleeScript meleeScript = meleeWeapon.GetComponent<MeleeScript>();

				if (meleeScript.currentCharge < meleeScript.maxCharge)
					meleeScript.currentCharge += 1;
					
				roomClear = false;
			}

			globalLockDown = false;
		}
	}
}
