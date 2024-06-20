using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour {

	private GameObject playerHead;
	public GameObject dropShadow;
	
	public LayerMask maskPlayer;

	private float SummoningSickness;
	private float randomSickness;

	public float forwardSpeed = 1;
	public float randomSpeed = 1;

	public float stunDuration = 1;
	public float stun;

	private Quaternion randDirection;
	private float timeDelay;
	private float localDelay;

	private bool switchDirection;

	void Start () {

		playerHead = GameObject.FindGameObjectWithTag("PlayerHead");

		randomSickness = Random.Range(0.5f, 3.5f);

		localDelay = Random.Range(0.1f, 3);
	}

	void Update () {

		if (timeDelay > 3)
		{
			if (switchDirection != false)
			{
				randDirection = Random.rotation;
				switchDirection = false;
			}
			else
			{
				randDirection = Quaternion.identity;
				switchDirection = true;
			}
			timeDelay = 0;
		}
		else
		{
			timeDelay += Time.deltaTime * localDelay;
		}

		if (SummoningSickness < randomSickness)
		{
			SummoningSickness += Time.deltaTime;
		}

		if (SummoningSickness > randomSickness)
		{
			Collider[] collideWithPlayer = Physics.OverlapSphere(transform.position, 0.5f, maskPlayer);

			Rigidbody rb = GetComponent<Rigidbody>();

			if (collideWithPlayer.Length > 0)
			{
				rb.AddForce(Vector3.forward * -50);
			}

			if (stun > stunDuration)
			{
				this.transform.LookAt(playerHead.transform);
				rb.AddRelativeForce(Vector3.forward * forwardSpeed);
				if (switchDirection == false)
				{
					rb.AddRelativeForce(randDirection * Vector3.forward * randomSpeed);
				}
			}
			else
			{
				stun += Time.deltaTime;	
			}
		}

		dropShadow.transform.rotation = Quaternion.identity;
	}
}
