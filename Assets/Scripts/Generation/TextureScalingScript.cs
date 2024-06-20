using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScalingScript : MonoBehaviour {

		private GameObject levelGen;
		private Renderer rend;

		public bool widthNotLength = true;
		public bool tilingY = true;
		public int tileScale = 1;

	void Start () {

		rend = GetComponent<Renderer>();

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void Update () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneDrawingFloors == true)
		{
			RoomScript roomScript = GetComponentInParent<RoomScript>();

			if (roomScript != null)
			{
				if (tilingY == true)
				{
					rend.material.mainTextureScale = new Vector2(roomScript.roomWidthX * tileScale, roomScript.roomLengthZ * tileScale);
				}
				else
				{
					if (widthNotLength == true)
					{
						rend.material.mainTextureScale = new Vector2(roomScript.roomWidthX * tileScale, tileScale);
					}
					else
					{
						rend.material.mainTextureScale = new Vector2(roomScript.roomLengthZ * tileScale, tileScale);
					}
				}

			}
			GetComponent<TextureScalingScript>().enabled = false;
		}
	}
}
