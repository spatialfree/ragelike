using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentGenScript : MonoBehaviour {

	private GameObject levelGen;
	public LayerMask floorLayer;
	public Transform contents;
	public GameObject voidBlock;
	public GameObject block;
	public GameObject pillar;

	public float floorHeight;

	private int xAxis;
	private int zAxis;
	private float xTransform;
	private float zTransform;

	private bool pickNewPos = true;
	private int pathLength;
	private int maxPathLength;
	private bool XorZ;

	[Header("Void x4")]
	public int minVoids = 1;
	public int maxVoids = 5;

	[Header("Room Objects x4")]
	public int minBlocks = 1;
	public int maxBlocks = 5;

	public int minPillars = 1;
	public int maxPillars = 5;

	[Header("Read Only")]
	public int widthX;
	public int lengthZ;
	public bool oddWidthX = false;
	public bool oddLengthZ = false;
	public int mirrorType;

	public int sizeMultiplier = 1;

	public int voidTotal;
	public int pillarTotal;
	public int blockTotal;

	public int genWhat;

	[Header("Load Sequence")]
	public bool mirrorTypeSet = false;

	public int voidBlocksDrawn;
	public bool voidDone = false;

	public int pillarsDrawn;
	public bool pillarsDone = false;

	public int blocksDrawn;
	public bool blocksDone = false;

	void Start () {

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void FixedUpdate () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneSettingBlocks == true)
		{
			RoomScript roomScript = GetComponentInParent<RoomScript>();

			widthX = (int)roomScript.roomWidthX;
			lengthZ = (int)roomScript.roomLengthZ;

			//Odd
			if (widthX % 2 == 1)
				oddWidthX = true;

			if (lengthZ % 2 == 1)
				oddLengthZ = true;

			//Set Mirror Type
			if (mirrorTypeSet == false)
			{
				if (oddWidthX == true && oddLengthZ == true)
					mirrorType = 0;

				if (oddWidthX == false && oddLengthZ == true)
					mirrorType = 1;

				if (oddWidthX == true && oddLengthZ == false)
					mirrorType = 2;

				if (oddWidthX == false && oddLengthZ == false)
					mirrorType = 3;

				//Number of generated items scales with room mirror type
				voidTotal = Random.Range(minVoids, maxVoids);
				blockTotal = Random.Range(minBlocks, maxBlocks);
				pillarTotal = Random.Range(minPillars, maxPillars);

				if (mirrorType == 0)
				{
					voidTotal *= 4;
					pillarTotal *= 4;
					blockTotal *= 4;
				}

				if (mirrorType == 1 || mirrorType == 2)
				{
					voidTotal *= 2;
					pillarTotal *= 2;
					blockTotal *= 2;
				}

				sizeMultiplier = (widthX * lengthZ)/100;

				voidTotal *= sizeMultiplier;
				pillarTotal *= sizeMultiplier;
				blockTotal *= sizeMultiplier;

				mirrorTypeSet = true;
			}

			//Start Drawing
			while (voidBlocksDrawn < voidTotal)
			{
				genWhat = 0;
				DrawCall();
			}

			while (pillarsDrawn < pillarTotal && voidBlocksDrawn >= voidTotal)
			{
				genWhat = 1;
				DrawCall();
			}

			while (blocksDrawn < blockTotal && pillarsDrawn >= pillarTotal)
			{
				genWhat = 2;
				DrawCall();
			}

			//END
			if (blocksDrawn >= blockTotal)
			{
				levelGenScript.roomsContentDrawn += 1;
				GetComponent<RoomContentGenScript>().enabled = false;
			}
		}
	}

	void DrawCall () {

		if (pathLength > maxPathLength)
		{
			pickNewPos = true;
			pathLength = 0;
			maxPathLength = Random.Range(4,9);
			XorZ ^= true;
		}

		if (mirrorType == 0)
		{
			if (genWhat < 2)
			{
				if (pickNewPos == true)
				{
					xAxis = Random.Range(0,widthX);
					zAxis = Random.Range(0,lengthZ);
					CoordinateConverter(); WhatToDraw();
					pickNewPos = false;
				}
				else
				{
					if (XorZ == true)
					{
						xAxis = Mathf.Clamp(xAxis-1, 0, widthX);
						CoordinateConverter(); WhatToDraw();
					}
					else
					{
						zAxis = Mathf.Clamp(zAxis-1, 0, lengthZ);
						CoordinateConverter(); WhatToDraw();
					}
				}
			}
			else
			{
				xAxis = Random.Range(0,widthX);
				zAxis = Random.Range(0,lengthZ);
				CoordinateConverter(); WhatToDraw();	
			}
		}

		if (mirrorType == 1)
		{
			if (genWhat < 2)
			{
				if (pickNewPos == true)
				{
					xAxis = Random.Range(0,widthX/2);
					zAxis = Random.Range(0,lengthZ);
					CoordinateConverter(); WhatToDraw();
					MirrorX(); WhatToDraw();	
					pickNewPos = false;
				}
				else
				{
					if (XorZ == true)
					{
						xAxis = Mathf.Clamp(xAxis-1, 0, widthX/2);
						CoordinateConverter(); WhatToDraw();
						MirrorX(); WhatToDraw();
					}
					else
					{
						zAxis = Mathf.Clamp(zAxis-1, 0, lengthZ);
						CoordinateConverter(); WhatToDraw();
						MirrorX(); WhatToDraw();
					}
				}
			}
			else
			{
				xAxis = Random.Range(0,widthX/2);
				zAxis = Random.Range(0,lengthZ);
				CoordinateConverter(); WhatToDraw();
				MirrorX(); WhatToDraw();	
			}
		}

		if (mirrorType == 2)
		{
			if (genWhat < 2)
			{
				if (pickNewPos == true)
				{
					xAxis = Random.Range(0,widthX);
					zAxis = Random.Range(0,lengthZ/2);
					CoordinateConverter(); WhatToDraw();
					MirrorZ(); WhatToDraw();
					pickNewPos = false;
				}
				else
				{
					if (XorZ == true)
					{
						xAxis = Mathf.Clamp(xAxis-1, 0, widthX);
						CoordinateConverter(); WhatToDraw();
						MirrorZ(); WhatToDraw();
					}
					else
					{
						zAxis = Mathf.Clamp(zAxis-1, 0, lengthZ/2);
						CoordinateConverter(); WhatToDraw();
						MirrorZ(); WhatToDraw();
					}
				}
			}
			else
			{
				xAxis = Random.Range(0,widthX);
				zAxis = Random.Range(0,lengthZ/2);
				CoordinateConverter(); WhatToDraw();
				MirrorZ(); WhatToDraw();
			}
		}

		if (mirrorType == 3)
		{
			if (genWhat < 2)
			{
				if (pickNewPos == true)
				{
					xAxis = Random.Range(0,widthX);
					zAxis = Random.Range(0,lengthZ);
					CoordinateConverter(); WhatToDraw();
					MirrorX(); WhatToDraw();
					MirrorZ(); WhatToDraw();
					MirrorX(); WhatToDraw();
					pickNewPos = false;
				}
				else
				{
					if (XorZ == true)
					{
						xAxis = Mathf.Clamp(xAxis-1, 0, widthX/2);
						CoordinateConverter(); WhatToDraw();
						MirrorX(); WhatToDraw();
						MirrorZ(); WhatToDraw();
						MirrorX(); WhatToDraw();
					}
					else
					{
						zAxis = Mathf.Clamp(zAxis-1, 0, lengthZ/2);
						CoordinateConverter(); WhatToDraw();
						MirrorX(); WhatToDraw();
						MirrorZ(); WhatToDraw();
						MirrorX(); WhatToDraw();
					}
				}
			}
			else
			{
				xAxis = Random.Range(0,widthX/2);
				zAxis = Random.Range(0,lengthZ/2);
				CoordinateConverter(); WhatToDraw();
				MirrorX(); WhatToDraw();
				MirrorZ(); WhatToDraw();
				MirrorX(); WhatToDraw();
			}
		}

		if (genWhat == 0)
			voidBlocksDrawn++;

		if (genWhat == 1)
			pillarsDrawn++;

		if (genWhat == 2)
			blocksDrawn++;

		pathLength++;
	}

	void MirrorX () {

		xAxis = (widthX - 1) - xAxis;
		CoordinateConverter();
	}

	void MirrorZ () {

		zAxis = (lengthZ - 1) - zAxis;
		CoordinateConverter();
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

	void WhatToDraw () {

		if (genWhat == 0)
			DrawVoidBlock();

		if (genWhat == 1)
			AddPillar();

		if (genWhat == 2)
			AddBlock();
	}

	void DrawVoidBlock () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		Collider[] checkForPath = Physics.OverlapSphere(new Vector3(transform.position.x, floorHeight + 2.5f, transform.position.z), 1, ~floorLayer);

		if (checkForPath.Length == 0)
		{
			var VoidBlock = (GameObject)Instantiate(voidBlock, new Vector3(transform.position.x, floorHeight, transform.position.z), transform.rotation);
			VoidBlock.transform.SetParent(contents);
			levelGenScript.roomFloorBlocks += 1;
		}
	}

	void AddPillar () {

		Collider[] checkBelowAndOn = Physics.OverlapSphere(new Vector3(transform.position.x, floorHeight + 2.5f, transform.position.z), 1, ~floorLayer);

		if (checkBelowAndOn.Length == 0)
		{
			var Pillar = (GameObject)Instantiate(pillar, new Vector3(transform.position.x, floorHeight + 5, transform.position.z), transform.rotation);
			Pillar.transform.SetParent(contents);
		}
	}

	void AddBlock () {

		Collider[] checkBelowAndOn = Physics.OverlapSphere(new Vector3(transform.position.x, floorHeight + 2.5f, transform.position.z), 1, ~floorLayer);

		if (checkBelowAndOn.Length == 0)
		{
			var Block = (GameObject)Instantiate(block, new Vector3(transform.position.x, floorHeight + 5, transform.position.z), transform.rotation);
			Block.transform.SetParent(contents);
		}
	}
}
