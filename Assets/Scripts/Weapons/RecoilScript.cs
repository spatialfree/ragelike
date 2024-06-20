using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilScript : MonoBehaviour {

	[Header("Recoil")]
	public float recoilDistance = 0.1f;
	public float distanceDecel = 12;
	public float recoilRotation = 10;
	public float rotationDecel = 12;

	[Header("do not change")]
	public float recoilLerp;
	public float angleRecoilLerp;

	void Update () {

		recoilLerp = Mathf.Lerp(recoilLerp, 0, 0.5f * Time.deltaTime * distanceDecel);
		angleRecoilLerp = Mathf.Lerp(angleRecoilLerp, 0, 0.5f * Time.deltaTime * rotationDecel);
	}

	public void Recoil () {

		recoilLerp = -recoilDistance;
		angleRecoilLerp = -recoilRotation;
	}
}
