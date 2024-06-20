using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMakerScript : MonoBehaviour {

	private GameObject levelGen;
	public GameObject pathBlock;
	public GameObject roomPathBlock;
	public Transform path;
	public GameObject pathMAP;
	public float pathMapHeight = -35;
	public Transform mapPath;

	public Transform behindTransform;

	private GameObject[] roomsArray;
	public int roomNumber;
	public Vector3 roomOriginalPos;
	public Vector3 roomPosition;
	public Vector3 roomAdjustment;
	public Vector3 pathDirection;

	public bool moveLR = false;
	public bool moveFB = false;

	public LayerMask pathBlockMask;
	public LayerMask roomMask;

	public bool start = true;

	private Vector3 leftSensorPosition;
	private Vector3 rightSensorPosition;
	private Vector3 frontSensorPosition;
	private Vector3 backSensorPosition;

	private Vector3 leftBlockSensorPosition;
	private Vector3 rightBlockSensorPosition;
	private Vector3 frontBlockSensorPosition;
	private Vector3 backBlockSensorPosition;

	//private bool parallelLR = false;
	//private bool parallelFB = false;//

	public bool XorZ = false;

	void Start () {
		
		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void FixedUpdate () {
		
		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneInflating == true)
		{
			roomsArray = GameObject.FindGameObjectsWithTag("Rooms");

			roomOriginalPos = roomsArray[roomNumber].transform.position;

			if (Mathf.Abs(roomOriginalPos.x) % 5 == 0)
			{
				roomOriginalPos.x += 2.5f;
			}

			if (Mathf.Abs(roomOriginalPos.z) % 5 == 0)
			{
				roomOriginalPos.z += 2.5f;
			}

			if (start == true)
			{
				transform.position = new Vector3(roomOriginalPos.x, transform.position.y, roomOriginalPos.z);
				start = false;
			}

			roomPosition = roomOriginalPos + roomAdjustment;

			if (transform.position.x != roomPosition.x || transform.position.z != roomPosition.z) 
			{
				if (XorZ != true)
				{
					while (transform.position.x != roomPosition.x)
					{
						if (transform.position.x > roomPosition.x)
						{
							transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
							pathDirection.y = -90;
						}

						if (transform.position.x < roomPosition.x)
						{
							transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
							pathDirection.y = 90;
						}
						AddBlock();
						CheckFB();
						CheckFB();
						AddBlock();
					}

					while (transform.position.z != roomPosition.z)
					{
						if (transform.position.z > roomPosition.z)
						{
							transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
							pathDirection.y = 180;
						}

						if (transform.position.z < roomPosition.z)
						{
							transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
							pathDirection.y = 0;
						}
						AddBlock();
						CheckLR();
						CheckLR();
						AddBlock();
					}
				}
				else
				{
					while (transform.position.z != roomPosition.z)
					{
						if (transform.position.z > roomPosition.z)
						{
							transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
							pathDirection.y = 180;
						}

						if (transform.position.z < roomPosition.z)
						{
							transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
							pathDirection.y = 0;
						}
						AddBlock();
						CheckLR();
						CheckLR();
						AddBlock();
					}

					while (transform.position.x != roomPosition.x)
					{
						if (transform.position.x > roomPosition.x)
						{
							transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
							pathDirection.y = -90;
						}

						if (transform.position.x < roomPosition.x)
						{
							transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
							pathDirection.y = 90;
						}
						AddBlock();
						CheckFB();
						CheckFB();
						AddBlock();
					}
				}
			}
			else
			{
				roomNumber += 1;

				if (XorZ == false)
				{
					XorZ = true;
				}
				else
				{
					XorZ = false;
				}

				roomAdjustment = Vector3.zero;

				if (roomNumber == roomsArray.Length)
				{
					levelGenScript.donePathDraw = true;
					GetComponent<PathMakerScript>().enabled = false;
				}
			}
		}
	}

	void CheckLR () {

		leftSensorPosition = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
		rightSensorPosition = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);

		Collider[] leftSensor = Physics.OverlapSphere(leftSensorPosition, 2, ~pathBlockMask);
		Collider[] rightSensor = Physics.OverlapSphere(rightSensorPosition, 2, ~pathBlockMask);

		if (leftSensor.Length != 0 && rightSensor.Length == 0)
		{
			roomAdjustment.x -= 5;
			transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
		}

		if (rightSensor.Length != 0 && leftSensor.Length == 0)
		{
			roomAdjustment.x += 5;
			transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		}

		Collider[] insideSensor = Physics.OverlapSphere(transform.position, 1, ~pathBlockMask);

		if (leftSensor.Length != 0 && rightSensor.Length != 0 && insideSensor.Length == 0)
		{
			roomAdjustment.x -= 5;
			transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
		}

		AddBlock();
	}

	void CheckFB () {

		frontSensorPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
		backSensorPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);

		Collider[] frontSensor = Physics.OverlapSphere(frontSensorPosition, 2, ~pathBlockMask);
		Collider[] backSensor = Physics.OverlapSphere(backSensorPosition, 2, ~pathBlockMask);

		if (frontSensor.Length != 0 && backSensor.Length == 0)
		{
			roomAdjustment.z += 5;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
		}

		if (backSensor.Length != 0 && frontSensor.Length == 0)
		{
			roomAdjustment.z -= 5;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
		}

		Collider[] insideSensor = Physics.OverlapSphere(transform.position, 1, ~pathBlockMask);

		if (frontSensor.Length != 0 && backSensor.Length != 0 && insideSensor.Length == 0)
		{
			roomAdjustment.z += 5;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
		}
		
		AddBlock();
	}

	void AddBlock () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		transform.localEulerAngles = pathDirection;

		//Collider[] checkForBlock = Physics.OverlapBox(transform.position, new Vector3(5,1,1), transform.rotation, pathBlockMask);
		Collider[] outsideRoom = Physics.OverlapSphere(behindTransform.position, 1);
		Collider[] insideRoom = Physics.OverlapSphere(behindTransform.position, 1, pathBlockMask);

		if (outsideRoom.Length == 0)
		{
			var newPathBlock = (GameObject)Instantiate(pathBlock, behindTransform.position, Quaternion.identity);
			newPathBlock.transform.SetParent(path);
			var PathMAP = (GameObject)Instantiate(pathMAP, new Vector3(behindTransform.position.x, pathMapHeight, behindTransform.position.z), Quaternion.identity);
			PathMAP.transform.SetParent(mapPath);
			levelGenScript.pathBlocksDrawn += 1;
		}
		else
		{
			if (insideRoom.Length == 0)
			{
				var newRoomPathBlock = (GameObject)Instantiate(roomPathBlock, behindTransform.position, Quaternion.identity);
				newRoomPathBlock.transform.SetParent(path);
				levelGenScript.placeholdersDrawn += 1;
			}
		}
	}
}
