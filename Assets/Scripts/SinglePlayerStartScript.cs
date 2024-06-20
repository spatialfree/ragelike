using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerStartScript : MonoBehaviour {

	void Update () {
		
		InteractableScript interactScript = gameObject.GetComponent<InteractableScript>();

		if (interactScript.interactedWith == true)
		{
			SceneManager.LoadScene(1);
		}
	}
}
