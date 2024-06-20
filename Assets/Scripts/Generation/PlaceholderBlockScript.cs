using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderBlockScript : MonoBehaviour {

	private GameObject levelGen;

	void Start () {

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}
	
	void Update () {
		
		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneDrawingFloors == true)
		{
			levelGenScript.placeholdersRemoved += 1;
			Destroy(gameObject);
		}
	}
}
