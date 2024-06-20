using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	private GameObject lockDown;
	public GameObject weapon;

	void Start () {
		
		lockDown = GameObject.FindGameObjectWithTag("GlobalLockDown");
	}

	void Update () {
		
		GlobalLockDownScript lockDownScript = lockDown.GetComponent<GlobalLockDownScript>();

		if (lockDownScript.enemyNumber < 1)
		{
			weapon.SetActive(false);
		}
		else
		{
			if (Input.GetButton("Run"))
			{
				weapon.SetActive(false);
			}
			else
			{
				weapon.SetActive(true);
			}
		}
	}
}
