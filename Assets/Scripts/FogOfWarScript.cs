using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour {

	private Vector3 sensorPos;

	void FixedUpdate () {

		sensorPos = new Vector3(transform.position.x, -40, transform.position.z);
		
		Collider[] roomCheck = Physics.OverlapSphere(sensorPos, 1);

		if (roomCheck.Length > 0)
		{
			MapRoomScript mapRoomScript = roomCheck[0].GetComponent<MapRoomScript>();

			if (mapRoomScript != null)
			{
				if (mapRoomScript.discovered == false)
					mapRoomScript.SetAsDiscovered();
			}
		} 
	}
}
