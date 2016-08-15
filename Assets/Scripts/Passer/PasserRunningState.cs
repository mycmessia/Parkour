using UnityEngine;
using System.Collections;

public class PasserRunningState : PasserState {

	Passer passer;
	Animator ani;

//	private TrackMove trackMove;

	float runningSpeed;

	void Awake ()
	{
		passer = GetComponent<Passer> ();
		ani = passer.GetAnimator ();

//		trackMove = GameObject.Find ("TrackController").GetComponent<TrackMove> ();

		runningSpeed = -2f;
	}

	void OnEnable ()
	{
		ani.SetBool ("isRunning", true);
	}

	void OnDisable ()
	{
		ani.SetBool ("isRunning", false);
	}

	void FixedUpdate ()
	{
//		if (trackMove.enabled)
//		{
			transform.Translate (new Vector3 (0f, 0f, runningSpeed * Time.deltaTime));	
//		}
	}
}
