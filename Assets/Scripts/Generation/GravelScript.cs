using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravelScript : MonoBehaviour {

	private ParticleSystem gravelSystem;

	void Start () {

		gravelSystem = GetComponent<ParticleSystem>();
		
		GameObject[] gravelSpawnPos = GameObject.FindGameObjectsWithTag("GravelSpawnPosition");

		for (int i = 0; i < gravelSpawnPos.Length; i++)
		{
			transform.position = new Vector3(gravelSpawnPos[i].transform.position.x, gravelSpawnPos[i].transform.position.y - 2.5f, gravelSpawnPos[i].transform.position.z);
			gravelSystem.Emit(Random.Range(8,16));
		}

		GameObject[] pathSpawnPos = GameObject.FindGameObjectsWithTag("PathSpawnPosition");

		for (int ii = 0; ii < pathSpawnPos.Length; ii++)
		{
			//transform.position = new Vector3(pathSpawnPos[ii].transform.position.x, pathSpawnPos[ii].transform.position.y - 2.5f, pathSpawnPos[ii].transform.position.z);

			var sh = gravelSystem.shape;
			sh.shapeType = ParticleSystemShapeType.BoxShell;

			//gravelSystem.Emit(Random.Range(2,4));

			transform.position = new Vector3(pathSpawnPos[ii].transform.position.x, pathSpawnPos[ii].transform.position.y + 2.5f, pathSpawnPos[ii].transform.position.z);

			gravelSystem.Emit(Random.Range(4,8));
		}

		GameObject[] wallSpawnPos = GameObject.FindGameObjectsWithTag("WallSpawnPosition");

		for (int a = 0; a < wallSpawnPos.Length; a++)
		{
			transform.position = new Vector3(wallSpawnPos[a].transform.position.x, wallSpawnPos[a].transform.position.y - 2.5f, wallSpawnPos[a].transform.position.z);
			
			var ma = gravelSystem.main;
			ma.startSize = 14;

			var sh = gravelSystem.shape;
			sh.scale = new Vector3(0.5f,0,0.5f);

			gravelSystem.Emit(Random.Range(2,4));
		}
	}
}
