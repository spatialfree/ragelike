using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour {

	private GameObject globalLockDownOBJ;
	public int currentHealth = 3;

	[Header("Last Hit Indicator")]
	public bool indicator = true;
	public string shaderName = "Unlit/Color";
	public string colorVar = "_Color";

	void Start () {

		globalLockDownOBJ = GameObject.FindGameObjectWithTag("GlobalLockDown");
	}

	void Update () {

		if (indicator == true)
		{
			Renderer rend = GetComponent<Renderer>();
	        rend.material.shader = Shader.Find(shaderName);

			if (currentHealth == 1)
			{
				rend.material.SetColor(colorVar, Color.red);
			}
		}

		if (transform.position.y < -25)
		{
			Damage(1);
		}
	}

	public void Damage(int damageAmount) {

		GlobalLockDownScript globalLockDownScript = globalLockDownOBJ.GetComponent<GlobalLockDownScript>();

		currentHealth -= damageAmount;
		if (currentHealth <= 0)
		{
			//gameObject.SetActive(false);
			globalLockDownScript.enemyNumber -= 1;
			Destroy(gameObject);
		}
	}
}