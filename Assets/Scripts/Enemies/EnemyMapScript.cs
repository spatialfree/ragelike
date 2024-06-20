using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMapScript : MonoBehaviour {

	public GameObject enemyMap;
	public bool isMapPrefab = false;

	private Transform parentEnemy;

	void Start () {

		if (isMapPrefab != true)
		{
			var EnemyMap = (GameObject)Instantiate(enemyMap);
			EnemyMap.transform.SetParent(gameObject.transform);
		}
		else
		{
			parentEnemy = transform.parent;
		}
	}

	void Update () {

		if (isMapPrefab == true)
		{
			transform.rotation = Quaternion.identity;
			transform.position = new Vector3(parentEnemy.transform.position.x, -38, parentEnemy.transform.position.z);
		}
		else
		{
			GetComponent<EnemyMapScript>().enabled = false;
		}
	}
}
