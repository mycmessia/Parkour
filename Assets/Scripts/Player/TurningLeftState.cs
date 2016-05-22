using UnityEngine;
using System.Collections;

public class TurningLeftState : PlayerState {

	private Player player;
	private Animator anim;

	private float moveDistance;
	private float movePerFrame;
	private int moveFrames;

	void Awake ()
	{
		player = GetComponent<Player> ();
		anim = player.GetAnimator ();
	}

	void OnEnable ()
	{
		if (transform.position.x == 0 || transform.position.x == 3) 
		{
			moveDistance = -Config.TRACK_WIDTh;
		} 
		else if (transform.position.x > 0) 
		{
			moveDistance = -transform.position.x;
		} 
		else 
		{
			moveDistance = -Config.TRACK_WIDTh - transform.position.x;
		}

		moveFrames = Config.Player.TURN_LR_FRAMES;
		movePerFrame = moveDistance / moveFrames;

		anim.SetBool ("isTurningLeft", true);
	}

	void OnDisable ()
	{
		anim.SetBool ("isTurningLeft", false);
	}

	void HandleInput ()	
	{
		if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{
			player.ChangeState (States.TurningRight);
		}
	}

	void Update () 
	{
		HandleInput ();
	}

	void FixedUpdate ()
	{
		player.Move ();

		transform.position = new Vector3 (
			transform.position.x + movePerFrame,
			transform.position.y,
			transform.position.z
		);
		moveDistance -= movePerFrame;

		if (Mathf.Abs (moveDistance) < 0.01f)
		{
			transform.position = new Vector3 (
				transform.position.x + moveDistance, 
				transform.position.y, 
				transform.position.z
			);

			player.ChangeState (States.Running);
		}
	}
}
