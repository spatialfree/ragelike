using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidShardScript : MonoBehaviour {

	public float speed = 1;
	public float decelRate = 1;
	public float gravityStrength = 1;
	private float gravity;

	public LayerMask layerMask;


	void Start () {

		transform.Rotate(Vector3.forward, Random.Range(-90, 90));
	}

	void Update () {

		speed = Mathf.Clamp(speed - Time.deltaTime * decelRate, 0, 10);
		
		transform.Translate(Vector3.forward * speed);

		gravity -= Time.deltaTime * gravityStrength;

		transform.Translate(Vector3.up * gravity, Space.World);

		Collider[] checkForSpace = Physics.OverlapSphere(transform.position, 0.2f, layerMask);

		if (checkForSpace.Length > 0)
		{
			speed = 0;
			gravity = 0;

			EnemyHealthScript health = checkForSpace[0].GetComponent<EnemyHealthScript>();

			if (health != null)
			{
				health.Damage(1);
			}

			Destroy(gameObject);

			//GetComponent<VoidShardScript>().enabled = false;
			//transform.SetParent(checkForSpace[0].transform);
		}
	}
}
