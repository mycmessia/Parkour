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

		if (player.jumpForce != 0f)
			player.jumpForce = -Config.Player.JUMP_SPEED * 2f;
	}

	void OnDisable ()
	{
		StopAllCoroutines ();

		anim.SetBool ("isSliding", false);

		player.jumpForce = 0f;
	}

	void HandleInput ()
	{
		UserInput uesrInput = InputController.instance.GetInput ();

		if (uesrInput == UserInput.Left && transform.position.x > -Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningLeft);
		} 
		else if (uesrInput == UserInput.Right && transform.position.x < Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningRight);
		}
		else if (uesrInput == UserInput.Up)
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
