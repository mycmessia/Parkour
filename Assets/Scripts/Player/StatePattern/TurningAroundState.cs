using UnityEngine;
using System.Collections;

public class TurningAroundState : PlayerState {

	private Player player;
	private Animator anim;

	// Use this for initialization
	void Awake () 
	{
		player = GetComponent<Player> ();
		anim = player.GetAnimator ();
	}
	
	void OnEnable ()
	{
		anim.SetBool ("isTurningAround", true);

		StartCoroutine ("StartRun");
	}

	void OnDisable ()
	{
		anim.SetBool ("isTurningAround", false);

		StopAllCoroutines ();
	}

	IEnumerator StartRun ()
	{
		yield return new WaitForSeconds (0.46f);

		player.ChangeState (States.Running);
	}
}
