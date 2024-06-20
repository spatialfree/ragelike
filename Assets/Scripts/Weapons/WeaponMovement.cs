using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour {

	private float extRecoilLerp;
	private float extAngleRecoilLerp;

	private float horizontalLerp;
	private float angleHorizontalLerp;
	private float verticalLerp;
	private float stepLerp;

	private float bobActualHeight;

	public CharacterController playerController;
	private float startTime;
	public float duration = 0.08f;
	private bool counterStarted = false;

	[Header("Horizontal Linear Drift")]
	public float horizontalDistance = 0.03f;
	public float horizontalAccel = 24;
	public float horizontalDecel = 6;

	[Header("Jump/Fall")]
	public float jumpDistance = 0.25f;
	public float jumpAccel = 12;
	public float jumpFall = 0.1f;
	public float fallAccel = 6;
	public float jumpDecel = 48;

	[Header("Strafe Tilt")]
	public float strafeTilt = 8;
	public float strafeTiltDecel = 16;

	[Header("Bobbing")]
	public float bobHeight = 0.1f;
	public float bobFreq = 10;
	public float bobDecel = 10;

	void Start () {

		bobActualHeight = bobHeight;
	}

	void Update () {

		//Horizontal linear drift
		if (Input.GetAxis("Horizontal") > 0)
		{
			horizontalLerp = Mathf.Lerp(horizontalLerp, -horizontalDistance, 0.5f * Time.deltaTime * horizontalAccel);
			angleHorizontalLerp = Mathf.Lerp(angleHorizontalLerp, -strafeTilt, 0.5f * Time.deltaTime * strafeTiltDecel);
		}

		if (Input.GetAxis("Horizontal") < 0)
		{
			horizontalLerp = Mathf.Lerp(horizontalLerp, horizontalDistance, 0.5f * Time.deltaTime * horizontalAccel);
			angleHorizontalLerp = Mathf.Lerp(angleHorizontalLerp, strafeTilt, 0.5f * Time.deltaTime * strafeTiltDecel);
		}

		if (Input.GetAxis("Horizontal") == 0)
		{
			horizontalLerp = Mathf.Lerp(horizontalLerp, 0f, 0.5f * Time.deltaTime * horizontalDecel);
			angleHorizontalLerp = Mathf.Lerp(angleHorizontalLerp, 0, 0.5f * Time.deltaTime * strafeTiltDecel);
		}

		//GunBob
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && IsGroundedByCController() == true)
		{
			stepLerp = Mathf.SmoothStep(stepLerp, bobActualHeight, 0.5f * Time.deltaTime * bobFreq);
		}

		if (stepLerp > (bobHeight - (bobHeight / 10)) || stepLerp < (-bobHeight + (bobHeight / 10)))
		{
			bobActualHeight *= -1;
		}

		if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
		{
			stepLerp = Mathf.Lerp(stepLerp, 0, 0.5f * Time.deltaTime * bobDecel);
		}

		//Jump
		if (Input.GetButton("Jump") && IsGroundedByCController() == true)
		{
			verticalLerp = Mathf.Lerp(verticalLerp, -jumpDistance, 0.5f * Time.deltaTime * jumpAccel);
		}
		else if (IsGroundedByCController() == true)
		{
			verticalLerp = Mathf.Lerp(verticalLerp, 0, 0.5f * Time.deltaTime * jumpDecel);
		}
		
		if (IsGroundedByCController() == false)
		{
			verticalLerp = Mathf.Lerp(verticalLerp, jumpFall, 0.5f * Time.deltaTime * fallAccel);
		}

		if (gameObject.GetComponent<RecoilScript>() != null)
		{
			RecoilScript recoilScript = gameObject.GetComponent<RecoilScript>();
			extRecoilLerp = recoilScript.recoilLerp;
			extAngleRecoilLerp = recoilScript.angleRecoilLerp;
		}

		transform.localPosition = new Vector3(horizontalLerp, verticalLerp + (stepLerp * Mathf.Clamp(Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")), 0, 1)), extRecoilLerp);

		transform.localRotation = Quaternion.Euler(extAngleRecoilLerp, 0, angleHorizontalLerp);
	}

	public float CountTime () {

		return Time.time - startTime;
	}

	public bool IsGroundedByCController () {

		if (playerController.isGrounded == false)
		{
			if (counterStarted == false)
			{
				startTime = Time.time;
				counterStarted = true;
			}
		}
		else counterStarted = false;

		if (CountTime() > duration)
		{
			return false;
		}

		return true;
	}
}
