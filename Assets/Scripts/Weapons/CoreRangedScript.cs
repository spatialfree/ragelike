using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRangedScript : MonoBehaviour {

	public float fireRate = 0.25f;
	public GameObject recoilScriptGO;

	private AudioSource gunAudio;
	private float nextFire;

	private ParticleSystem rangedParticleSystem;

	private GameObject weapon;

	void Start () {

		weapon = GameObject.FindGameObjectWithTag("Weapon");

		gunAudio = GetComponent<AudioSource>();

		rangedParticleSystem = GetComponent<ParticleSystem>();
	}

	void OnParticleCollision (GameObject other) {

		EnemyHealthScript health = other.GetComponent<EnemyHealthScript>();

		if (health != null)
		{
			health.Damage(1);
		}
	}

	void Update () {

		if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.activeSelf)
		{
			nextFire = Time.time + fireRate;

			gunAudio.Play();

			RecoilScript recoilScript = recoilScriptGO.GetComponent<RecoilScript>();
			recoilScript.Recoil();

			rangedParticleSystem.Emit(1);
		}
	}

	/*public float fireRate = 0.25f;
	public Transform gunEnd;
	public GameObject voidShard;
	public GameObject recoilScriptGO;

	private AudioSource gunAudio;
	private float nextFire;

	void Start () {

		gunAudio = GetComponent<AudioSource>();
	}

	void Update () {

		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			gunAudio.Play();

			RecoilScript recoilScript = recoilScriptGO.GetComponent<RecoilScript>();
			recoilScript.Recoil();

			var voidShardInst = (GameObject)Instantiate(voidShard, gunEnd.position, gunEnd.rotation);
			Destroy(voidShardInst, 10.0f);
		}
	}*/
}
