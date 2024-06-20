using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	private float nextESC;

	public GameObject mainPanel;
	public GameObject settingsPanel;
	public GameObject interactPrompt;

	public GameObject playerCamera;

	void Awake () {
	
		Application.targetFrameRate = 144;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 1f;
		playerCamera.GetComponent<PlayerMouseFollowScript>().enabled = false;
	}
	
	public void LoadByIndex (int sceneIndex) {

		SceneManager.LoadScene(sceneIndex);
	}

	public void TaskOnClick (int buttonClicked) {

		if (buttonClicked == 0)
		{
			settingsPanel.SetActive(true);

		}

		if (buttonClicked == 1)
		{
			settingsPanel.SetActive(false);
		}

		if (buttonClicked == 2)
		{
			mainPanel.SetActive(false);
		}
	}

	void Update () {
		
		if (Input.GetButtonDown("Cancel"))
		{
			if (settingsPanel.activeSelf == true && Time.time > nextESC)
			{
				nextESC = Time.time + 0.001f;
				settingsPanel.SetActive(false);
			}
			
			if (mainPanel.activeSelf == true && Time.time > nextESC)
			{
				nextESC = Time.time + 0.001f;
				mainPanel.SetActive(false);
			}

			if (mainPanel.activeSelf == false && settingsPanel.activeSelf == false && Time.time > nextESC)
			{
				nextESC = Time.time + 0.001f;
				mainPanel.SetActive(true);
			}
		}

		if (mainPanel.activeSelf == true)
		{
			Time.timeScale = 0.025f;
			playerCamera.GetComponent<PlayerMouseFollowScript>().enabled = false;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Time.timeScale = 1;
			playerCamera.GetComponent<PlayerMouseFollowScript>().enabled = true;
			Cursor.lockState = CursorLockMode.Locked;
		}

		PlayerInteract interactScript = playerCamera.GetComponentInParent<PlayerInteract>();

		if (interactScript.interactableObject == true)
		{
			interactPrompt.SetActive(true);
		}
		else
		{
			interactPrompt.SetActive(false);
		}
	}
}
