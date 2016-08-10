using UnityEngine;
using System.Collections;

public class PasserWalkingState : PasserState {

	Passer passer;
	Animator ani;

	float walkingSpeed;
	float walkingDistance;
	float distanceCounter;

	const float WALKING_SPEED = 0.5f;

	void Awake ()
	{
		passer = GetComponent<Passer> ();
		ani = passer.GetAnimator ();
	}

	void OnEnable ()
	{
		walkingDistance = 2f * Config.TRACK_WIDTh;

		if (transform.localRotation == Quaternion.Euler (0, 90f, 0))
			walkingSpeed = -WALKING_SPEED;
		else
			walkingSpeed = WALKING_SPEED;

		if (transform.localPosition.x == 0f)
			walkingDistance = Config.TRACK_WIDTh;

		distanceCounter = walkingDistance;

		ani.SetBool ("isWalking", true);
	}

	void OnDisable ()
	{
		ani.SetBool ("isWalking", false);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.localPosition += new Vector3 (walkingSpeed * Time.deltaTime, 0f, 0f);

		distanceCounter -= WALKING_SPEED * Time.deltaTime;
		if (distanceCounter <= 0f)
		{
			walkingSpeed *= -1f;
			distanceCounter = walkingDistance;

			if (transform.localRotation == Quaternion.Euler (0, 90f, 0))
				transform.localRotation = Quaternion.Euler (0, 270f, 0);
			else
				transform.localRotation = Quaternion.Euler (0, 90f, 0);
		}
	}
}
