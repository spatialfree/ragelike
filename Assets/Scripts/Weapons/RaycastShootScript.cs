using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShootScript : MonoBehaviour {

	public int gunDamage = 1;
	public float fireRate = 0.25f;
	public float weaponRange = 50f;
	public float hitForce = 100f;
	public Transform gunEnd;
	public GameObject standardDecal;
	public GameObject standardParticle;
	public GameObject recoilScriptGO;

	private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
	private AudioSource gunAudio;
	private LineRenderer laserLine;
	private float nextFire;

	void Start () {
		
		laserLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		fpsCam = GetComponentInParent<Camera>();
	}

	void Update () {

		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			StartCoroutine(ShotEffect());

			RecoilScript recoilScript = recoilScriptGO.GetComponent<RecoilScript>();
			recoilScript.Recoil();

			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
			RaycastHit hit;

			laserLine.SetPosition(0, gunEnd.position);

			if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit, weaponRange))
			{
				laserLine.SetPosition(1, hit.point);

				var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				var impactDecal = (GameObject)Instantiate(standardDecal, hit.point, hitRotation);
				impactDecal.transform.SetParent(hit.transform);
				Destroy(impactDecal, 10.0f);

				var impactParticle = (GameObject)Instantiate(standardParticle, hit.point, hitRotation);
				Destroy(impactParticle, 5.0f);

				EnemyHealthScript health = hit.collider.GetComponent<EnemyHealthScript>();

				if (health != null)
				{
					health.Damage(gunDamage);
				}

				if (hit.rigidbody != null)
				{
					hit.rigidbody.AddForce(-hit.normal * hitForce);
					FlyingEnemyScript stunScript = hit.collider.GetComponent<FlyingEnemyScript>();

					if (stunScript != null)
					{
						stunScript.stun = 0;
					}
				}

			}
			else
			{
				laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
			}
		}
	}

	private IEnumerator ShotEffect() {

		gunAudio.Play();
		laserLine.enabled = false;
		yield return shotDuration;
		laserLine.enabled = false;
	}
}
