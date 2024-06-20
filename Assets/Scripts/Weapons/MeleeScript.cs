using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeScript : MonoBehaviour {

	public GameObject unstableVoid;
	public GameObject recoilScriptGO;
	public Transform voidPoint;

	public float meleeDuration = 0.5f;
	public float knockback = 100;
	public float fireRate = 0.5f;
	private float nextFire;
	private float nextCharge;
	private bool runMelee = false;

	public LayerMask layerMask;

	public GameObject chargeSegments;
	public Sprite[] arrayOfSegments;
	public Slider chargeSlider;
	public Slider currentChargeSlider;
	public int maxCharge = 5;

	public float currentCharge;
	private float visualCharge;
	private float chargeSliderValue;
	private float currentChargeValue;

	void Start () {

		currentCharge = maxCharge;
		visualCharge = maxCharge;
	}

	void Update () {

		Image imageScript = chargeSegments.GetComponent("Image") as Image;
		imageScript.sprite = arrayOfSegments[maxCharge - 1];

		chargeSliderValue = visualCharge/maxCharge;
		chargeSlider.value = chargeSliderValue;

		currentChargeValue = currentCharge/maxCharge;
		currentChargeSlider.value = currentChargeValue;

		if (visualCharge < currentCharge)
			visualCharge += Time.deltaTime * meleeDuration;

		if (visualCharge > currentCharge)
			visualCharge -= Time.deltaTime * meleeDuration;

		if (Input.GetButtonDown("Melee") && Time.time > nextCharge && currentCharge > 0)
		{
			currentCharge -= 1;
			nextCharge = Time.time + meleeDuration;
		}

		if (Time.time > nextCharge)
		{
			runMelee = false;
		}
		else
		{
			runMelee = true;
		}

		if (runMelee == true && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			RecoilScript recoilScript = recoilScriptGO.GetComponent<RecoilScript>();
			recoilScript.Recoil();

			var unstableVoidInst = (GameObject)Instantiate(unstableVoid, voidPoint.position, voidPoint.rotation);
			
			unstableVoidInst.transform.SetParent(voidPoint.transform);

			Destroy(unstableVoidInst, 0.08f);
		}
	}
}
