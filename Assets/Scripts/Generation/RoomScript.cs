using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {

	public GameObject roomMesh;
	public GameObject roomFloor;
	public GameObject wallSlice;
	public GameObject wallSkirt;
	public GameObject roomCeiling;

	public GameObject wallGen;
	private GameObject levelGen;
	public Transform walls;
	public GameObject roomMAP;

	//public GameObject testCubeZpos;
	//public GameObject testCubeZneg;
	//public GameObject testCubeXpos;
	//public GameObject testCubeXneg;

	[Header("Spacing Between Rooms")]
	public float roomSpacing = 2;
	private float sensorOffset;

	[Header("Height")]
	public int minRoomHeight = 3;
	public int maxRoomHeight = 6;
	private float heightOffset;
	private int numberOfWallSlices;
	private float sliceHeight = 7.5f;
	private bool wallSkirtDone = false;
	private bool ceilingInstalled = false;

	[Header("Scale")]
	public int minRoomScale = 3;
	public int maxRoomScale = 12;
	public int maxInflate = 2;

	[Header("Read Only")]
	public float roomLengthZ;
	public float roomWidthX;
	public float roomHeightY;

	public int stuckDirection;

	private Vector3 zPosV3;
	private Vector3 zNegV3;
	private Vector3 xPosV3;
	private Vector3 xNegV3;

	private Vector3 xScale;
	private Vector3 zScale;

	private Collider[] Xpositive;
	private Collider[] Xnegative;
	private Collider[] Zpositive;
	private Collider[] Znegative;
	private Collider[] inside;

	private bool localDoneMoving = false;
	private bool localDoneInflating = false;
	private bool localDone = false;

	private float inflatePosZ;
	private float inflateNegZ;
	private float inflatePosX;
	private float inflateNegX;

	private bool doneInflatePosX;
	private bool doneInflateNegX;
	private bool doneInflatePosZ;
	private bool doneInflateNegZ;

	//[Header("+# = -Chance")]
	//public int changeDirection = 4;
	public float friction = 4;

	private float timeToChangeDir;

	void Start () {

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		levelGenScript.numberOfRooms += 1;
		
		roomLengthZ = Random.Range(minRoomScale, maxRoomScale);
		roomWidthX = Random.Range(minRoomScale, maxRoomScale);
		roomHeightY = Random.Range(minRoomHeight, maxRoomHeight);

		heightOffset = roomHeightY;

		if (roomLengthZ % 2 == 1)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.5f);
		}

		if (roomWidthX % 2 == 1)
		{
			transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
		}

		roomMesh.transform.localScale = new Vector3(roomWidthX, roomHeightY, roomLengthZ);

		if (roomHeightY % 2 == 1)
		{
			heightOffset -= 1;
			roomMesh.transform.localPosition = new Vector3(0, roomMesh.transform.localPosition.y + 2.5f, 0);
		}

		roomMesh.transform.localPosition = new Vector3(0, roomMesh.transform.localPosition.y + ((heightOffset - 2)/2)*5, 0);

		sensorOffset = roomSpacing * 2;

		stuckDirection = Random.Range(0,5);

		friction = (roomLengthZ * roomWidthX)/10;
	}
	
	void FixedUpdate () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneMoving == false)
		{
			if (localDoneMoving == true)
			{
				if (Random.Range(0, friction) < 1)
				{
					XSensors();	

					if (Xpositive.Length != 1 && Xnegative.Length == 1)
					{
						transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
					}

					if (Xpositive.Length == 1 && Xnegative.Length != 1)
					{
						transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
					}

					ZSensors();

					if (Zpositive.Length != 1 && Znegative.Length == 1)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
					}

					if (Zpositive.Length == 1 && Znegative.Length != 1)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
					}
				}
			}
			else
			{
				XSensors();	

				if (Xpositive.Length != 1 && Xnegative.Length == 1)
				{
					transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
				}

				if (Xpositive.Length == 1 && Xnegative.Length != 1)
				{
					transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
				}

				ZSensors();

				if (Zpositive.Length != 1 && Znegative.Length == 1)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
				}

				if (Zpositive.Length == 1 && Znegative.Length != 1)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
				}
			}

			//Stuck
			XSensors();
			ZSensors();
			InsideSensor();

			if (timeToChangeDir > 240)
			{
				stuckDirection = Random.Range(0,5);
				timeToChangeDir = 0;
			}
			else
			{
				timeToChangeDir += Random.Range(1, 3);	
			}

			if (inside.Length != 1)
			{
				RoomScript roomScript = inside[1].gameObject.GetComponentInParent<RoomScript>();

				if (localDoneMoving == true)
				{
					levelGenScript.roomsDoneMoving -= 1;
					localDoneMoving = false;
				}

				if (roomScript != null)
				{
					if (roomScript.stuckDirection == stuckDirection)
					{
						stuckDirection = Random.Range(0,5);
						//Debug.Log("Same!");
						timeToChangeDir = 0;
					}
					/*
					if (timeToChangeDir > 40)
					{
						stuckDirection = Random.Range(0,5);
						Debug.Log("OHHHH Boy Here I go");
						timeToChangeDir = 0;
					}
					else
					{
						timeToChangeDir += Random.Range(0, 10);	
					}
					*/
				}
			}

			if (Xpositive.Length != 1 && Xnegative.Length != 1 && Zpositive.Length != 1 && Znegative.Length != 1)
			{
				UnStuck();
			}
			else
			{
				if (Xpositive.Length != 1 && Xnegative.Length != 1)
				{
					UnStuck();
				}

				if (Zpositive.Length != 1 && Znegative.Length != 1)
				{
					UnStuck();
				}
			}

			//Done Moving?
			if (Xpositive.Length == 1 && Xnegative.Length == 1 && Zpositive.Length == 1 && Znegative.Length == 1)
			{
				InsideSensor();

				if (inside.Length > 1 )
				{
					UnStuck();
				}
				else
				{
					if (localDoneMoving == false)
					{
						levelGenScript.roomsDoneMoving += 1;
						localDoneMoving = true;
					}
				}
			}
		}

		if (levelGenScript.doneMoving == true && localDoneInflating == false)
		{
			sensorOffset = roomSpacing;
			XSensors();

			if (Xpositive.Length == 1 && inflatePosX < maxInflate)
			{
				doneInflatePosX = false;
				transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
				roomWidthX += 1;
				roomMesh.transform.localScale = new Vector3(roomWidthX, roomHeightY, roomLengthZ);
				inflatePosX += 1;
			}
			else
			{
				doneInflatePosX = true;
			}

			if (Xnegative.Length == 1 && inflateNegX < maxInflate)
			{
				doneInflateNegX = false;
				transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
				roomWidthX += 1;
				roomMesh.transform.localScale = new Vector3(roomWidthX, roomHeightY, roomLengthZ);
				inflateNegX += 1;
			}
			else
			{
				doneInflateNegX = true;
			}

			ZSensors();

			if (Zpositive.Length == 1 && inflatePosZ < maxInflate)
			{
				doneInflatePosZ = false;
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.5f);
				roomLengthZ += 1;
				roomMesh.transform.localScale = new Vector3(roomWidthX, roomHeightY, roomLengthZ);
				inflatePosZ += 1;
			}
			else
			{
				doneInflatePosZ = true;
			}

			if (Znegative.Length == 1 && inflateNegZ < maxInflate)
			{
				doneInflateNegZ = false;
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.5f);
				roomLengthZ += 1;
				roomMesh.transform.localScale = new Vector3(roomWidthX, roomHeightY, roomLengthZ);
				inflateNegZ += 1;
			}
			else
			{
				doneInflateNegZ = true;
			}

			//Done Inflating?
			if (doneInflatePosX == true && doneInflateNegX == true && doneInflatePosZ == true && doneInflateNegZ == true)
			{
				levelGenScript.roomsDoneInflating += 1;
				localDoneInflating = true;
			}
		}

		if (levelGenScript.doneInflating == true)
		{
			if (ceilingInstalled == false)
			{
				// var Ceiling = (GameObject)Instantiate(roomCeiling, new Vector3(transform.position.x, transform.position.y + (roomHeightY * 5) - 7.5f, transform.position.z), Quaternion.identity);
				// Ceiling.transform.localScale = new Vector3(roomWidthX, 1, roomLengthZ);
				// Ceiling.transform.SetParent(transform);
				ceilingInstalled = true;
			}

			if (wallSkirtDone == false)
			{
				var WallSkirt = (GameObject)Instantiate(wallSkirt, new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z), Quaternion.identity);
				WallSkirt.transform.localScale = new Vector3(roomWidthX, 1, roomLengthZ);
				WallSkirt.transform.SetParent(transform);
				wallSkirtDone = true;
			}

			if (numberOfWallSlices < roomHeightY - 2)
			{
				var WallSlice = (GameObject)Instantiate(wallSlice, new Vector3(transform.position.x, transform.position.y + sliceHeight, transform.position.z), Quaternion.identity);
				WallSlice.transform.localScale = new Vector3(roomWidthX, 1, roomLengthZ);
				WallSlice.transform.SetParent(transform);
				sliceHeight += 5;
				numberOfWallSlices += 1;
			}
			else
			{
				if (levelGenScript.donePathDraw == true)
				{
					if (localDone == false)
					{
						var WallGen = (GameObject)Instantiate(wallGen, transform.position, Quaternion.identity);
						WallGen.transform.SetParent(walls);
						localDone = true;
					}
				}

				if (levelGenScript.doneSettingBlocks == true)
				{
					roomMesh.SetActive(false);

					/*var RoomFloor = (GameObject)Instantiate(roomFloor);
					RoomFloor.transform.SetParent(transform);
					RoomFloor.transform.localScale = new Vector3(roomMesh.transform.localScale.x, 1, roomMesh.transform.localScale.z);
					RoomFloor.transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
					levelGenScript.roomFloorsDrawn += 1;*/
					
					//Instantiate room(map) Gameobject
					var RoomMap = (GameObject)Instantiate(roomMAP);
					RoomMap.transform.SetParent(transform);
					RoomMap.transform.localScale = new Vector3(roomMesh.transform.localScale.x, 1, roomMesh.transform.localScale.z);
					RoomMap.transform.position = new Vector3(transform.position.x, -40, transform.position.z);

					//End Script
					GetComponent<RoomScript>().enabled = false;
				}
			}
		}
	}

	public void XSensors () {

		xPosV3 = new Vector3(transform.position.x + ((roomWidthX/2) + sensorOffset/2)*5, transform.position.y - 2.5f, transform.position.z);
		xNegV3 = new Vector3(transform.position.x - ((roomWidthX/2) + sensorOffset/2)*5, transform.position.y - 2.5f, transform.position.z);

		xScale = new Vector3(sensorOffset * 2.5f, 1 * 2.5f, (sensorOffset * 2 + roomLengthZ) * 2.5f);

		Xpositive = Physics.OverlapBox(xPosV3, xScale);
		Xnegative = Physics.OverlapBox(xNegV3, xScale);

		//testCubeXpos.transform.position = xPosV3;
		//testCubeXneg.transform.position = xNegV3;
		//testCubeXpos.transform.localScale = new Vector3(xScale.x*2, xScale.y*2, xScale.z*2);
		//testCubeXneg.transform.localScale = new Vector3(xScale.x*2, xScale.y*2, xScale.z*2);
	}

	public void ZSensors () {

		zPosV3 = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z + ((roomLengthZ/2) + sensorOffset/2)*5);
		zNegV3 = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z - ((roomLengthZ/2) + sensorOffset/2)*5);

		zScale = new Vector3((sensorOffset * 2 + roomWidthX) * 2.5f, 1 * 2.5f, sensorOffset * 2.5f);

		Zpositive = Physics.OverlapBox(zPosV3, zScale);
		Znegative = Physics.OverlapBox(zNegV3, zScale);

		//testCubeZpos.transform.position = zPosV3;
		//testCubeZneg.transform.position = zNegV3;
		//testCubeZpos.transform.localScale = new Vector3(zScale.x*2, zScale.y*2, zScale.z*2);
		//testCubeZneg.transform.localScale = new Vector3(zScale.x*2, zScale.y*2, zScale.z*2);
	}

	public void InsideSensor () {

		inside = Physics.OverlapBox(transform.position, new Vector3(roomWidthX * 2.5f, 2 * 2.5f, roomLengthZ * 2.5f));
	}

	public void UnStuck () {

		if (localDoneMoving == true)
		{
			if (Random.Range(0, friction) < 1)
			{
				if (stuckDirection == 0)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
				}

				if (stuckDirection == 1)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
				}

				if (stuckDirection == 2)
				{
					transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
				}

				if (stuckDirection == 3)
				{
					transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
				}
			}
		}
		else
		{
			if (stuckDirection == 0)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
			}

			if (stuckDirection == 1)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
			}

			if (stuckDirection == 2)
			{
				transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
			}

			if (stuckDirection == 3)
			{
				transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
			}
		}
	}
}
