using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour {

	private GameObject playerHead;
	private GameObject healthGameObject;
	public LayerMask maskPlayer;
	public float pickUpSize = 1;
	public float magneticSize = 10;
	public float magneticPull = 1;
	private float distanceToPlayer;

	void Start () {

		playerHead = GameObject.FindGameObjectWithTag("PlayerHead");
		healthGameObject = GameObject.FindGameObjectWithTag("PlayerHealth");
	}

	void Update () {

		PlayerHealth healthScript = healthGameObject.GetComponent<PlayerHealth>();

		if (healthScript.playerHealth != healthScript.maxPlayerHealth)
		{
			Collider[] magneticWithPlayer = Physics.OverlapSphere(transform.position, magneticSize, maskPlayer);

			if (magneticWithPlayer.Length > 0)
			{
				Rigidbody rb = GetComponent<Rigidbody>();

				this.transform.LookAt(playerHead.transform);

				distanceToPlayer = Vector3.Distance(transform.position, playerHead.transform.position);

				rb.AddRelativeForce(Vector3.forward * (magneticPull / distanceToPlayer));
			}
				
			Collider[] collideWithPlayer = Physics.OverlapSphere(transform.position, pickUpSize, maskPlayer);

			if (collideWithPlayer.Length > 0)
			{
				if (healthScript.playerHealth != healthScript.maxPlayerHealth)
				{
					healthScript.playerHealth += 1;
					Destroy(this.gameObject);
				}
			}
		}
	}
}
