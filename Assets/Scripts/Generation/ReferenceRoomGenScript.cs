using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceRoomGenScript : MonoBehaviour {
/*
	public GameObject forwardDoor;
	public GameObject backwardDoor;
	public GameObject rightDoor;
	public GameObject leftDoor;

	public GameObject standardRoom;

	public bool floorDecayFinished = false;
	public bool pathwayFinished = false;
	public bool blocksWallsFinished = false;

	public bool roomGenFinished = false;

	public LayerMask floorLayer; // ~floorLayer for everything else
	public float floorHeight;

	private int xAxis;
	private int zAxis;
	private float xTransform;
	private float zTransform;

	[Header("Decay")]
	public int numberOf = 3;

	[Header("Room Objects")]
	public GameObject block;
	public int maxBlocks = 5;
	public GameObject solidPillar;
	public int maxSolidPillars = 5;

	private float delay = 2;

	private int floorMirror;
	private int blockMirror;
	private int pillarMirror;

	void Start () {

		floorMirror = Random.Range(0,4);
		blockMirror = Random.Range(0,4);
		pillarMirror = Random.Range(0,4);
	}

	void Update () {

		if (roomGenFinished == true)
		{
			GetComponent<RoomGenScript>().enabled = false;
		}
		
		RoomDoorScript roomDoorScript = standardRoom.GetComponent<RoomDoorScript>();
		
		if (roomDoorScript.doorGenFinished == true && floorDecayFinished == true && pathwayFinished == false)
		{
			if (forwardDoor.activeSelf == true)
			{
				xAxis = 5; zAxis = 7; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 8; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 9; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 10; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 11; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 7; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 8; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 9; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 10; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 11; CoordinateConverter(); AddFloor();
			}

			if (backwardDoor.activeSelf == true)
			{
				xAxis = 5; zAxis = 4; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 3; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 2; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 1; CoordinateConverter(); AddFloor();
				xAxis = 5; zAxis = 0; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 4; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 3; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 2; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 1; CoordinateConverter(); AddFloor();
				xAxis = 6; zAxis = 0; CoordinateConverter(); AddFloor();
			}

			if (rightDoor.activeSelf == true)
			{
				xAxis = 11; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 10; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 9; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 8; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 7; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 11; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 10; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 9; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 8; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 7; zAxis = 6; CoordinateConverter(); AddFloor();
			}

			if (leftDoor.activeSelf == true)
			{
				xAxis = 0; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 1; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 2; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 3; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 4; zAxis = 5; CoordinateConverter(); AddFloor();
				xAxis = 0; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 1; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 2; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 3; zAxis = 6; CoordinateConverter(); AddFloor();
				xAxis = 4; zAxis = 6; CoordinateConverter(); AddFloor();
			}

			xAxis = 5; zAxis = 5; CoordinateConverter(); AddFloor();
			xAxis = 5; zAxis = 6; CoordinateConverter(); AddFloor();
			xAxis = 6; zAxis = 5; CoordinateConverter(); AddFloor();
			xAxis = 6; zAxis = 6; CoordinateConverter(); AddFloor();

			pathwayFinished = true;
		}

		if (numberOf > 0)
		{
			if (floorMirror == 0)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); RemoveFloor();

				numberOf -= 1;
			}

			if (floorMirror == 1)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); RemoveFloor();
				MirrorX(); RemoveFloor();

				numberOf -= 1;
			}

			if (floorMirror == 2)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); RemoveFloor();
				MirrorZ(); RemoveFloor();

				numberOf -= 1;
			}

			if (floorMirror == 3)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); RemoveFloor();
				MirrorX(); RemoveFloor();
				MirrorZ(); RemoveFloor();
				MirrorX(); RemoveFloor();

				numberOf -= 1;
			}
		}
		else
		{
			floorDecayFinished = true;	
		}

		if (pathwayFinished == true && delay > 0)
		{
			delay -= Time.deltaTime;
		}

		if (pathwayFinished == true && maxSolidPillars > 0 && delay < 1)
		{
			if (pillarMirror == 0)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); AddPillar();

				maxSolidPillars -= 1;
			}

			if (pillarMirror == 1)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); AddPillar();
				MirrorX(); AddPillar();

				maxSolidPillars -= 1;
			}

			if (pillarMirror == 2)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); AddPillar();
				MirrorZ(); AddPillar();

				maxSolidPillars -= 1;
			}

			if (pillarMirror == 3)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); AddPillar();
				MirrorX(); AddPillar();
				MirrorZ(); AddPillar();
				MirrorX(); AddPillar();

				maxSolidPillars -= 1;
			}
		}

		if (pathwayFinished == true && maxBlocks > 0 && maxSolidPillars == 0)
		{
			if (blockMirror == 0)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); AddBlock();

				maxBlocks -= 1;
			}

			if (blockMirror == 1)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,12);
				CoordinateConverter(); AddBlock();
				MirrorX(); AddBlock();

				maxBlocks -= 1;
			}

			if (blockMirror == 2)
			{
				xAxis = Random.Range(0,12);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); AddBlock();
				MirrorZ(); AddBlock();

				maxBlocks -= 1;
			}

			if (blockMirror == 3)
			{
				xAxis = Random.Range(0,6);
				zAxis = Random.Range(0,6);
				CoordinateConverter(); AddBlock();
				MirrorX(); AddBlock();
				MirrorZ(); AddBlock();
				MirrorX(); AddBlock();

				maxBlocks -= 1;
			}
		}

		if (maxBlocks == 0 && maxSolidPillars == 0 && blocksWallsFinished == false)
		{
			if (forwardDoor.activeSelf == true)
			{
				xAxis = 5; zAxis = 7; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 8; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 9; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 10; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 11; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 7; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 8; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 9; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 10; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 11; CoordinateConverter(); RemoveBlocksAndWalls();
			}

			if (backwardDoor.activeSelf == true)
			{
				xAxis = 5; zAxis = 4; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 3; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 2; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 1; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 5; zAxis = 0; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 4; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 3; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 2; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 1; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 6; zAxis = 0; CoordinateConverter(); RemoveBlocksAndWalls();
			}

			if (rightDoor.activeSelf == true)
			{
				xAxis = 11; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 10; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 9; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 8; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 7; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 11; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 10; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 9; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 8; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 7; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
			}

			if (leftDoor.activeSelf == true)
			{
				xAxis = 0; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 1; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 2; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 3; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 4; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 0; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 1; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 2; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 3; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
				xAxis = 4; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
			}

			xAxis = 5; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
			xAxis = 5; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();
			xAxis = 6; zAxis = 5; CoordinateConverter(); RemoveBlocksAndWalls();
			xAxis = 6; zAxis = 6; CoordinateConverter(); RemoveBlocksAndWalls();

			blocksWallsFinished = true;

			roomGenFinished = true;
		}

	}

	void MirrorX () {

		xAxis = 11 - xAxis;
		CoordinateConverter();
	}

	void MirrorZ () {

		zAxis = 11 - zAxis;
		CoordinateConverter();
	}

	void CoordinateConverter () {

		if (xAxis < 6)
		{
			xTransform = ((xAxis - 5) * 5) - 2.5f;
		}
		else
		{
			xTransform = ((xAxis - 6) * 5) + 2.5f;
		}

		if (zAxis < 6)
		{
			zTransform = ((zAxis - 5) * 5) - 2.5f;
		}
		else
		{
			zTransform = ((zAxis - 6) * 5) + 2.5f;
		}

		this.transform.localPosition = new Vector3(xTransform, floorHeight, zTransform); 		
	}

	void RemoveFloor () {

		Collider[] floorCheck = Physics.OverlapSphere(this.transform.position, 1, floorLayer);

		if (floorCheck.Length > 0)
		{
			FloorActiveScript floorActiveScript = floorCheck[0].GetComponent<FloorActiveScript>();
			floorActiveScript.active = false;
		}
	}

	void AddFloor () {

		Collider[] floorCheck = Physics.OverlapSphere(this.transform.position, 1, floorLayer);

		if (floorCheck.Length > 0)
		{
			FloorActiveScript floorActiveScript = floorCheck[0].GetComponent<FloorActiveScript>();
			floorActiveScript.active = true;
		}
	}

	void AddBlock () {

		Collider[] checkBelow = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1, floorLayer);
		Collider[] checkOn = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1, ~floorLayer);

		if (checkBelow.Length > 0 && checkOn.Length == 0)
		{
			Instantiate(block, new Vector3(transform.position.x, 2.5f, transform.position.z), transform.rotation);
		}
	}

	void AddPillar () {

		Collider[] checkBelow = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1, floorLayer);
		Collider[] checkOn = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1, ~floorLayer);

		if (checkBelow.Length > 0 && checkOn.Length == 0)
		{
			Instantiate(solidPillar, new Vector3(transform.position.x, 2.5f, transform.position.z), transform.rotation);
		}
	}

	void RemoveBlocksAndWalls () {

		Collider[] checkForOther = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1, ~floorLayer);

		if (checkForOther.Length > 0)
		{
			Destroy (checkForOther[0].gameObject);
		}		
	}
	*/
}
