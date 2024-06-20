using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Sprite[] arrayOfSegments;
	public Slider healthSlider;
	public Slider currentHealthSlider;
	public int maxPlayerHealth = 3;

	public float playerHealth;
	private float visualHealth;
	private float healthSliderValue;
	private float currentHealthValue;

	void Start () {

		playerHealth = maxPlayerHealth;
		visualHealth = maxPlayerHealth;
	}
	
	void Update () {
		
		Image imageScript = GetComponent("Image") as Image;
		imageScript.sprite = arrayOfSegments[maxPlayerHealth - 1];

		healthSliderValue = visualHealth/maxPlayerHealth;
		healthSlider.value = healthSliderValue;

		currentHealthValue = playerHealth/maxPlayerHealth;
		currentHealthSlider.value = currentHealthValue;

		if (visualHealth < playerHealth)
		{
			visualHealth += Time.deltaTime;
		}

		if (visualHealth > playerHealth)
		{
			visualHealth -= Time.deltaTime;
		}
	}
}
