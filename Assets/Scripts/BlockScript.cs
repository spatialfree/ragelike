using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

	public int currentDurability = 2;

	[Header("Last Hit Indicator")]
	public bool indicator = true;
	public string shaderName = "Standard";
	public string colorVar = "_Color";

	private bool colorSet = false;

	void Update () {
		
		if (indicator == true)
		{
			if (currentDurability == 1 && colorSet == false)
			{
				Renderer rend = GetComponent<Renderer>();
				rend.material.shader = Shader.Find(shaderName);
				rend.material.SetColor(colorVar, Color.red);
				colorSet = true;
			}
		}
	}

	public void Damage (int damageAmount) {

		currentDurability -= damageAmount;
		if (currentDurability <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
