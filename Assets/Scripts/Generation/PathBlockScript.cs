using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlockScript : MonoBehaviour {

	private GameObject levelGen;

	public GameObject ZposWall;
	public GameObject ZnegWall;
	public GameObject XposWall;
	public GameObject XnegWall;

	public Transform ZposSensor;
	public Transform ZnegSensor;
	public Transform XposSensor;
	public Transform XnegSensor;

	public bool localSet = false;

	public GameObject pathFloor;
	public LayerMask floorLayer;
	public Color baseCanyonColor;

	[Header("For Collider Toggle")]
	public GameObject genCollider;
	public GameObject ceiling;
	public GameObject floor;

	void Start () {
		
		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void FixedUpdate () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (localSet == false)
		{
			Collider[] ZposArray = Physics.OverlapSphere(ZposSensor.position, 1);
			Collider[] ZnegArray = Physics.OverlapSphere(ZnegSensor.position, 1);
			Collider[] XposArray = Physics.OverlapSphere(XposSensor.position, 1);
			Collider[] XnegArray = Physics.OverlapSphere(XnegSensor.position, 1);
			
			if (ZposArray.Length != 0 && ZposWall != null)
			{
				Destroy(ZposWall);
			}

			if (ZnegArray.Length != 0 && ZnegWall != null)
			{
				Destroy(ZnegWall);
			}

			if (XposArray.Length != 0 && XposWall != null)
			{
				Destroy(XposWall);
			}

			if (XnegArray.Length != 0 && XnegWall != null)
			{
				Destroy(XnegWall);
			}

			if (levelGenScript.doneWallDraw == true)
			{
				genCollider.GetComponent<SphereCollider>().enabled = false;

				levelGenScript.pathBlocksSet += 1;
				localSet = true;
			}
		}

		if (levelGenScript.doneSettingFloors == true)
		{
			Collider[] checkForRoom = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z), 5, floorLayer);

			if (checkForRoom.Length != 0)
			{
				Renderer rend = pathFloor.GetComponent<Renderer>();
				//rend.material.shader = Shader.Find("Unlit/Color");
				rend.material.SetColor("_Color", baseCanyonColor);
			}

			GetComponent<PathBlockScript>().enabled = false;
		}
	}
}
