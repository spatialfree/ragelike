using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraScript : MonoBehaviour {

	private GameObject player;
	public GameObject dust;
	public GameObject playerMAP;
	public float cameraHeight = 250;
	public float playerHeight = -30;
	public float dustHeight = 2;

	void Update () {
		
		if (player != null)
		{
			//transform.position = new Vector3(player.transform.position.x, cameraHeight, player.transform.position.z);
			playerMAP.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
			playerMAP.transform.position = new Vector3(player.transform.position.x, cameraHeight + playerHeight, player.transform.position.z);
		
			dust.transform.position = new Vector3(player.transform.position.x, dustHeight, player.transform.position.z);
		}
		else
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
}
