using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenScript : MonoBehaviour {

	public GameObject standardRoom;
	public int maxNumberOfRooms;
	public int minNumberOfRooms;
	private int spawnTotal;
	private int spawnCurrent;

	public GameObject gravelSystem;

	[Header("Load Sequence")]
	public int numberOfRooms;
	public int roomsDoneMoving;
	public float timeDelay = 1;
	private float timer;
	public bool doneMoving = false;

	public int roomsDoneInflating;
	public bool doneInflating = false;

	public int pathBlocksDrawn;
	public bool donePathDraw = false;

	public int wallBlocksDrawn;
	public bool doneWallDraw = false;

	public int pathBlocksSet;
	public bool doneSettingBlocks = false;

	public int roomsContentDrawn;
	public bool doneDrawingContent = false;

	public int roomFloorsDrawn;
	public bool doneDrawingFloors = false;

	public int placeholdersDrawn;
	public int placeholdersRemoved;
	public bool doneRemovingPlaceholders = false;

	public int roomFloorBlocks;
	public int roomFloorsSet;
	public bool doneSettingFloors = false;

	public float startDelay;
	public bool doneGenScene = false;

	private int randomDirection;

	void Awake () {

		//Random.InitState(42);
	}

	void Start () {

		spawnTotal = Random.Range(minNumberOfRooms, maxNumberOfRooms);

		while (spawnCurrent < spawnTotal)
		{
			Instantiate(standardRoom, transform.position, Quaternion.identity);
			spawnCurrent ++;
		}
	}

	void Update () {

		if (roomsDoneMoving == numberOfRooms)
		{
			if (timer > timeDelay)
			{
				doneMoving = true;
			}
			else
			{
				timer += Time.deltaTime;
			}
		}

		if (roomsDoneInflating == numberOfRooms)
			doneInflating = true;

		if (wallBlocksDrawn == numberOfRooms)
			doneWallDraw = true;

		if (pathBlocksSet == pathBlocksDrawn && donePathDraw == true)
		{
			doneSettingBlocks = true;
			//SceneManager.LoadScene(0);
		}

		if (roomsContentDrawn == numberOfRooms -1 && doneSettingBlocks == true)
			doneDrawingContent = true;

		if (roomFloorsDrawn == numberOfRooms && doneDrawingContent == true)
			doneDrawingFloors = true;

		if (placeholdersRemoved == placeholdersDrawn && doneDrawingFloors == true)
			doneRemovingPlaceholders = true;

		if (roomFloorsSet == roomFloorBlocks && doneDrawingFloors == true)
			doneSettingFloors = true;

		if (doneSettingFloors == true)
		{
			if (startDelay > 2)
			{
				doneGenScene = true;
				Instantiate(gravelSystem);
				GetComponent<LevelGenScript>().enabled = false;
			}
			else
			{
				startDelay += Time.deltaTime;
			}
		}
	}
}
