using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlockScript : MonoBehaviour {

	private GameObject levelGen;

	public LayerMask floorBlockMask;
	private Vector3 sensorPos;
	private Collider[] checkForFloor;

	public GameObject frontMesh;
	public GameObject backMesh;
	public GameObject rightMesh;
	public GameObject leftMesh;

	// public Transform ZposSensor;
	// public Transform ZnegSensor;
	// public Transform XposSensor;
	// public Transform XnegSensor;

	void Start () {
		
		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void Update () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneRemovingPlaceholders == true)
		{
			GetComponent<BoxCollider>().enabled = false;

			levelGenScript.roomFloorsSet += 1;

			if (levelGenScript.doneSettingFloors == true)
			{
				sensorPos = transform.position + new Vector3(0,0,5);
				CheckForFloor();

				if (checkForFloor.Length == 0 && frontMesh != null)
					Destroy(frontMesh);


				sensorPos = transform.position + new Vector3(0,0,-5);
				CheckForFloor();

				if (checkForFloor.Length == 0 && backMesh != null)
					Destroy(backMesh);


				sensorPos = transform.position + new Vector3(5,0,0);
				CheckForFloor();

				if (checkForFloor.Length == 0 && rightMesh != null)
					Destroy(rightMesh);


				sensorPos = transform.position + new Vector3(-5,0,0);
				CheckForFloor();

				if (checkForFloor.Length == 0 && leftMesh != null)
					Destroy(leftMesh);



				GetComponent<FloorBlockScript>().enabled = false;
			}
		}
	}

	void CheckForFloor () {

		checkForFloor = Physics.OverlapSphere(sensorPos, 1, floorBlockMask);
	}
}
