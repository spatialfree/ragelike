using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public int playerLayer;
	public int fallingPlayerLayer;
	public LayerMask voidLayer;
	public LayerMask pathLayer;

	public float speed = 6.0F;
	public float airSpeed = 3.0F;
	public float jumpSpeed = 8.0F;
	public float accelRate = 1;
	public float gravity = 20.0F;

	public Vector3 moveDirection = Vector3.zero;
	private Vector3 moveAirDirection = Vector3.zero;
	private Vector3 moveFinal = Vector3.zero;

	public float forwardClamp = 1F;
	public float backwardClamp = 1F;
	public float strafeClamp = 1F;
	private float originalSpeed;
	private float originalAirSpeed;

	CharacterController controller;

	private Vector3 sensorPos;
	public float topSpeed;

	void Start() {
		
		controller = GetComponent<CharacterController>();

		topSpeed = speed;
		//originalAirSpeed = airSpeed;
	}

	void Update() {

		topSpeed = Input.GetAxis("Run") * 10 + 10;

		originalSpeed = topSpeed;
		originalAirSpeed = topSpeed;
		
		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Mathf.Clamp(Input.GetAxis("Horizontal"), -strafeClamp, strafeClamp), 0, Mathf.Clamp(Input.GetAxis("Vertical"), -backwardClamp, forwardClamp));
			moveDirection = transform.TransformDirection(moveDirection);

			if ((Input.GetAxis("Horizontal") != 0) && (Input.GetAxis("Vertical") != 0))
			{
				speed = originalSpeed * 0.7f;
			}
			else
			{
				speed = originalSpeed;
			}

			moveDirection *= speed;
			moveFinal = moveDirection;

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		else
		{
			moveAirDirection = new Vector3(Mathf.Clamp(Input.GetAxis("Horizontal"), -strafeClamp, strafeClamp), 0, Mathf.Clamp(Input.GetAxis("Vertical"), -backwardClamp, forwardClamp));
			moveAirDirection = transform.TransformDirection(moveAirDirection);

			if ((Input.GetAxis("Horizontal") != 0) && (Input.GetAxis("Vertical") != 0))
			{
				airSpeed = originalAirSpeed * 0.7f;
			}
			else
			{
				airSpeed = originalAirSpeed;
			}

			moveAirDirection *= airSpeed;
			moveFinal = moveAirDirection;
			moveFinal.y = moveDirection.y;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveFinal * Time.deltaTime);
	}
}