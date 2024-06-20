using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenScript : MonoBehaviour {

	private GameObject levelGen;

	public GameObject wallBlock;

	private float roomLength;
	private float roomWidth;

	private int xFirst = 0;
	private int zFirst = 0;
	private int xLast = 0;
	private int zLast = 0;

	void Start () {

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
		
		RoomScript roomScript = GetComponentInParent<RoomScript>();
		roomLength = roomScript.roomLengthZ;
		roomWidth = roomScript.roomWidthX;

		transform.position = new Vector3(transform.position.x - 5*((roomWidth/2)+0.5f), 2.5f, transform.position.z - 5*((roomLength/2)+0.5f));
	}

	void Update () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		while (zLast < (roomLength + 1))
		{
			if (xFirst < (roomWidth + 1))
			{
				transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
				AddWall();
				xFirst += 1;
			}
			else
			{
				if (zFirst < (roomLength + 1))
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
					AddWall();
					zFirst += 1;
				}
				else
				{
					if (xLast < (roomWidth + 1))
					{
						transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
						AddWall();
						xLast += 1;
					}
					else
					{
						if (zLast < (roomLength + 1))
						{
							transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
							AddWall();
							zLast += 1;
						}
					}
				}
			}
		}

		levelGenScript.wallBlocksDrawn += 1;
		GetComponent<WallGenScript>().enabled = false;
	}

	void AddWall () {

		Collider[] emptySpace = Physics.OverlapSphere(transform.position, 1);

		if (emptySpace.Length == 0)
		{
			var WallBlock = (GameObject)Instantiate(wallBlock, transform.position, Quaternion.identity);
			WallBlock.transform.SetParent(transform.parent);
		}
	}
}
