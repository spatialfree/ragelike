using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PlayerDamageScript : MonoBehaviour {

	public LayerMask maskOneDmg;
	public LayerMask playerLayer;

	private Vector3 startCapsule;
	private Vector3 endCapsule;

	public GameObject healthGameObject;
	private float iFrames;

	private float abberationIntensity = 0.25f;
	public float delayAb = 2;
	private float noiseIntensity = 0.15f;
	public float delayNoise = 2;
	public GameObject playerCamera;

	public float bounceOutHeight = 20;
	public float fallDistance = 2;
	public int fallDamage = 1;

	void Update () {

		startCapsule = new Vector3(this.transform.position.x, this.transform.position.y + 0.375f, this.transform.position.z);
		endCapsule = new Vector3(this.transform.position.x, this.transform.position.y - 0.375f, this.transform.position.z);

		Collider[] oneDamageCollider = Physics.OverlapCapsule(startCapsule, endCapsule, 0.6f, maskOneDmg);

		if (oneDamageCollider.Length > 0 && iFrames <= 0)
		{
			PlayerHealth healthScript = healthGameObject.GetComponent<PlayerHealth>();
			healthScript.playerHealth -= 1;
			iFrames = 1;
			abberationIntensity = 30;
			noiseIntensity = 10f;
		}

		Collider[] fallCollider = Physics.OverlapSphere(new Vector3(transform.position.x, -fallDistance, transform.position.z), 1, playerLayer);

		if (fallCollider.Length > 0 && iFrames <= 0)
		{
			PlayerHealth healthScript = healthGameObject.GetComponent<PlayerHealth>();
			healthScript.playerHealth -= fallDamage;
			iFrames = 1;
			abberationIntensity = 30;
			noiseIntensity = 10f;
			PlayerMovementScript playerMovementScript = gameObject.GetComponent<PlayerMovementScript>();
			playerMovementScript.moveDirection.y = bounceOutHeight;
		}

		if (iFrames > 0)
		{
			iFrames -= Time.deltaTime;
		}

		VignetteAndChromaticAberration chromaticScript = playerCamera.GetComponent<VignetteAndChromaticAberration>();
		chromaticScript.chromaticAberration = abberationIntensity;

		if (abberationIntensity > 0.25f)
		{
			abberationIntensity -= Time.deltaTime * delayAb;
		}
		else
		{
			abberationIntensity = 0.25f;
		}

		NoiseAndGrain noiseScript = playerCamera.GetComponent<NoiseAndGrain>();
		noiseScript.intensityMultiplier = noiseIntensity;

		if (noiseIntensity > 0.25f)
		{
			noiseIntensity -= Time.deltaTime * delayNoise;
		}
		else
		{
			noiseIntensity = 0.25f;
		}
	}
}
