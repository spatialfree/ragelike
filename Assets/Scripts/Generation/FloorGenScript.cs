using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenScript : MonoBehaviour {

	private GameObject levelGen;
	public Transform floor;
	public GameObject floorBlock;
	public float floorHeight;

	private int widthX;
	private int lengthZ;
	private bool oddWidthX;
	private bool oddLengthZ;

	private int xAxis = 0;
	private int zAxis = 0;
	private float xTransform;
	private float zTransform;

	void Start () {
		
		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void Update () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneDrawingContent == true)
		{
			RoomScript roomScript = GetComponentInParent<RoomScript>();

			widthX = (int)roomScript.roomWidthX;
			lengthZ = (int)roomScript.roomLengthZ;

			//xAxis = 0;
			//zAxis = 0;
			CoordinateConverter();

			while (zAxis < lengthZ)
			{
				if (xAxis < widthX - 1)
				{
					CoordinateConverter();
					DrawFloor();
					xAxis += 1;
					CoordinateConverter();
					DrawFloor();
				}
				else
				{
					zAxis += 1;
					xAxis = 0;
				}
			}

				levelGenScript.roomFloorsDrawn += 1;
				GetComponent<FloorGenScript>().enabled = false;		

		}
	}

	void CoordinateConverter () {

		RoomScript roomScript = GetComponentInParent<RoomScript>();

		if (xAxis < (roomScript.roomWidthX/2))
		{
			xTransform = (xAxis - ((roomScript.roomWidthX / 2) - 1)) - 0.5f;
		}
		else
		{
			xTransform = (xAxis - (roomScript.roomWidthX / 2)) + 0.5f;
		}

		if (zAxis < (roomScript.roomLengthZ/2))
		{
			zTransform = (zAxis - ((roomScript.roomLengthZ / 2) - 1)) - 0.5f;
		}
		else
		{
			zTransform = (zAxis - (roomScript.roomLengthZ / 2)) + 0.5f;
		}

		this.transform.localPosition = new Vector3(xTransform * 5, floorHeight, zTransform * 5); 
	}

	void DrawFloor () {

		//LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		Collider[] emptyCheck = Physics.OverlapSphere(transform.position, 1);

		if (emptyCheck.Length == 0)
		{
			var FloorBlock = (GameObject)Instantiate(floorBlock, transform.position, transform.rotation);
			FloorBlock.transform.SetParent(floor);
			//levelGenScript.roomFloorBlocks += 1;
		}
	}
}
