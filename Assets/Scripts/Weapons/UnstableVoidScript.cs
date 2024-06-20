using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableVoidScript : MonoBehaviour {

	public int meleeDamage = 1;
	public GameObject healthPickUp;
	public LayerMask enemyLayer;

	void Start () {
		
		Collider[] checkInside = Physics.OverlapSphere(transform.position, 0.4f, enemyLayer);

		if (checkInside.Length > 0)
		{
			EnemyHealthScript health = checkInside[0].GetComponent<EnemyHealthScript>();

			if (health != null)
			{
				if (health.currentHealth <= meleeDamage)
					Instantiate(healthPickUp, checkInside[0].transform.position, checkInside[0].transform.rotation);

				health.Damage(meleeDamage);
			}

			BlockScript blockScript = checkInside[0].GetComponent<BlockScript>();

			if (blockScript != null)
				blockScript.Damage(meleeDamage);
		}	
	}

	void Update () {

		
	}
}
