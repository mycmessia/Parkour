using UnityEngine;
using System.Collections;

public class PasserRunningState : PasserState {

	Passer passer;
	Animator ani;

	float runningSpeed;

	void Awake ()
	{
		passer = GetComponent<Passer> ();
		ani = passer.GetAnimator ();

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
		transform.Translate (new Vector3 (0f, 0f, runningSpeed * Time.deltaTime));
	}
}
