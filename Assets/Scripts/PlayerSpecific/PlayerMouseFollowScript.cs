﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseFollowScript : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5F;
	public float smoothing = 2F;
	public float upClamp = 90F;
	public float downClamp = 90F;

	GameObject character;

	void Start () {
		
		character = this.transform.parent.gameObject;
	}
	
	void Update () {

		Cursor.lockState = CursorLockMode.Locked;
		
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp(mouseLook.y, -downClamp, upClamp);

		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
	}
}
