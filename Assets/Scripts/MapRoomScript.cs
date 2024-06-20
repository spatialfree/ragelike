using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoomScript : MonoBehaviour {

	public bool discovered = false;

	public Color discovColor;

	public string shaderName = "Standard";
	public string colorVar = "_Color";

	public void SetAsDiscovered () {

		discovered = true;

		Renderer rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find(shaderName);
		rend.material.SetColor(colorVar, discovColor);
	}
}
