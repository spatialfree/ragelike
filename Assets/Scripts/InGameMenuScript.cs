using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuScript : MonoBehaviour {

	private GameObject levelGen;
	public GameObject player;
	public GameObject mapPlayer;
	public bool playerSpawned;
	public Transform lobby;
	public GameObject idleCamera;
	public GameObject LoadingScreenBG;
	public GameObject Menu;
	public GameObject MapCamera;
	public GameObject playerCamera;
	public GameObject healthGameObject;
	public GameObject gameOverPanel;
	public GameObject gunObject;
	//public GameObject crosshair;

	private Vector4 loadingColor;
	private float alphaLoadingBG = 1;

	void Awake () {
	
		Cursor.lockState = CursorLockMode.Locked;
		//Application.targetFrameRate = 144;
		Time.timeScale = 1;
		LoadingScreenBG.SetActive(true);
		loadingColor = LoadingScreenBG.GetComponent<Image>().color;
		player.SetActive(false);
		mapPlayer.SetActive(false);
		playerSpawned = false;
		idleCamera.SetActive(true);
		MapCamera.GetComponent<Camera>().enabled = false;
	}
	
	public void LoadByIndex(int sceneIndex) {

		SceneManager.LoadScene(sceneIndex);
	}

	public void TaskOnClick() {

		Menu.SetActive(false);
		EnablePlayer();
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

	void Start () {

		levelGen = GameObject.FindGameObjectWithTag("LevelGen");
	}

	void Update () {

		LevelGenScript levelGenScript = levelGen.GetComponent<LevelGenScript>();

		if (levelGenScript.doneGenScene == true)
		{
			if (alphaLoadingBG > 0)
			{
				loadingColor = new Vector4(loadingColor.x, loadingColor.y, loadingColor.z, alphaLoadingBG);
				LoadingScreenBG.GetComponent<Image>().color = loadingColor;
				alphaLoadingBG -= Time.deltaTime;

				SpawnPlayer();
			}
			else
			{
				LoadingScreenBG.SetActive(false);

				if (Input.GetButtonDown("Map"))
				{
					if (MapCamera.GetComponent<Camera>().enabled == true)
					{
						//crosshair.SetActive(true);
						MapCamera.GetComponent<Camera>().enabled = false;
						Cursor.lockState = CursorLockMode.Locked;
					}
					else
					{
						//crosshair.SetActive(false);
						MapCamera.GetComponent<Camera>().enabled = true;
						Cursor.lockState = CursorLockMode.None;
					}
				}

				if (Input.GetButtonDown("Cancel"))
				{
					if (Menu.activeSelf)
					{
						Menu.SetActive(false);
						EnablePlayer();
						Cursor.lockState = CursorLockMode.Locked;
						Time.timeScale = 1;
					}
					else
					{
						Menu.SetActive(true);
						DisablePlayer();
						Cursor.lockState = CursorLockMode.None;
						//Time.timeScale = 0;
					}
				}

				PlayerHealth healthScript = healthGameObject.GetComponent<PlayerHealth>();

				if (healthScript.playerHealth <= 0)
				{
					gameOverPanel.SetActive(true);
					KillPlayer();
					Cursor.lockState = CursorLockMode.None;
					Time.timeScale = 1;
				}
			}
		}
		else
		{
			DisablePlayer();
		}
	}

	void SpawnPlayer () {

		if (playerSpawned == false)
		{
			playerCamera.GetComponent<PlayerMouseFollowScript>().enabled = true;
			gunObject.SetActive(true);
			player.transform.position = new Vector3(lobby.position.x, 1.5f, lobby.position.z);
			mapPlayer.transform.position = new Vector3(lobby.position.x, mapPlayer.transform.position.y, lobby.position.z);
			idleCamera.SetActive(false);
			player.SetActive(true);
			mapPlayer.SetActive(true);
			playerSpawned = true;
		}
	}

	void KillPlayer () {

		idleCamera.SetActive(true);
		idleCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z);
		idleCamera.transform.localEulerAngles = new Vector3(90,0,0);
		player.SetActive(false);
		mapPlayer.SetActive(false);
	}

	void DisablePlayer () {

		idleCamera.SetActive(true);
		idleCamera.transform.position = playerCamera.transform.position;
		idleCamera.transform.rotation = playerCamera.transform.rotation;
		player.SetActive(false);
	}

	void EnablePlayer () {

		idleCamera.SetActive(false);
		player.SetActive(true);

		// playerCamera.GetComponent<PlayerMouseFollowScript>().enabled = true;
		// gunObject.SetActive(true);
	}
}
