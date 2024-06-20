using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	private GameObject globalLockDownOBJ;
	public GameObject forceField;
	public LayerMask playerLayerMask;
	public bool enemysSpawned;
	public GameObject baseFlyingEnemy;
	public GameObject baseWalkingEnemy;

	private float spawnWidth;
	private float spawnLength;
	private float spawnNumber;
	private float spawnRandomNumber;
	private int spawnedNumber;
	private Vector3 spawnPosition;
	private Vector3 sensorPosition;
	private int whatToSpawn;

	void Start () {

		globalLockDownOBJ = GameObject.FindGameObjectWithTag("GlobalLockDown");

		spawnRandomNumber = Random.Range(0,4);
	}
	
	void FixedUpdate () {

		GlobalLockDownScript globalLockDownScript = globalLockDownOBJ.GetComponent<GlobalLockDownScript>();
		
		RoomScript roomScript = GetComponent<RoomScript>();

		spawnWidth = roomScript.roomWidthX*2.5f;

		spawnLength = roomScript.roomLengthZ*2.5f;

		spawnNumber = ((roomScript.roomWidthX*roomScript.roomLengthZ/100) + spawnRandomNumber)/2;

		sensorPosition = new Vector3(roomScript.roomWidthX*2.5f, 100, roomScript.roomLengthZ*2.5f);

		Collider[] playerDetection = Physics.OverlapBox(transform.position, sensorPosition, this.transform.rotation, playerLayerMask);
		
		if (playerDetection.Length >= 1 && enemysSpawned == false)
		{
			if (spawnedNumber < spawnNumber)
			{
				spawnPosition = new Vector3(transform.position.x + Random.Range(-spawnWidth,spawnWidth), Random.Range(3,4), transform.position.z + Random.Range(-spawnLength,spawnLength));
				
				whatToSpawn = Random.Range(0,2);

				Collider[] checkForSpace = Physics.OverlapSphere(spawnPosition, 1);

				if (checkForSpace.Length == 0)
				{
					if (whatToSpawn == 0)
						Instantiate(baseFlyingEnemy, spawnPosition, transform.rotation);

					if (whatToSpawn == 1)
						Instantiate(baseWalkingEnemy, spawnPosition, transform.rotation);

					globalLockDownScript.enemyNumber += 1;
					spawnedNumber += 1;
				}
			}
			else
			{
				enemysSpawned = true;
			}
		}


		forceField.transform.localScale = new Vector3(roomScript.roomWidthX*5 + 2.5f, 25, roomScript.roomLengthZ*5 + 2.5f);

		if (globalLockDownScript.globalLockDown == true)
		{
			forceField.SetActive(true);
		}
		else
		{
			forceField.SetActive(false);
		}
	}
}