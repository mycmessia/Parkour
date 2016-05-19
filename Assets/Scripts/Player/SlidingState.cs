using UnityEngine;
using System.Collections;

public class SlidingState : PlayerState {

	private Player player;
	private Animator anim;

	void Awake ()
	{
		player = GetComponent<Player> ();
		anim = player.GetAnimator ();
	}

	void OnEnable ()
	{
		StartCoroutine ("StopSliding");

		anim.SetBool ("isSliding", true);

        if (player.moveYspeed != 0f)
            player.moveYspeed = -Config.JUMP_SPEED * 2f;
	}

	void OnDisable ()
	{
		StopAllCoroutines ();

		anim.SetBool ("isSliding", false);

        player.moveYspeed = 0f;
	}

	void HandleInput ()
	{
		if (Input.GetKeyUp (KeyCode.LeftArrow) && transform.position.x > -Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningLeft);
		} 
		else if (Input.GetKeyUp (KeyCode.RightArrow) && transform.position.x < Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningRight);
		}
		else if (Input.GetKeyUp (KeyCode.UpArrow))
		{
			player.ChangeState (States.Jumping);
		}
	}

	void Update ()
	{
		HandleInput ();
	}

	void FixedUpdate ()
	{
		player.Move ();
	}

	IEnumerator StopSliding ()
	{
		yield return new WaitForSeconds (0.4f);

		player.ChangeState (States.Running);
	}
}
