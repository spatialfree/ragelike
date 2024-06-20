using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	private Camera fpsCam;

	public float interactRange;
	public bool interactableObject = false;


	void Start () {
		
		fpsCam = GetComponentInChildren<Camera>();
	}

	void Update () {
		
		Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit, interactRange))
		{
			if (hit.collider.GetComponent<InteractableScript>() != null)
			{
				interactableObject = true;
				if (Input.GetButtonDown("Interact"))
				{
					InteractableScript intScript = hit.collider.GetComponent<InteractableScript>();
					intScript.interactedWith = true;
				}
			}
			else
			{
				interactableObject = false;
			}
		}
		else
		{
			interactableObject = false;
		}
	}
}
