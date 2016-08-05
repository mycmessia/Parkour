using UnityEngine;
using System.Collections;

public class RunningState : PlayerState {

	private Player player;
    private Transform model;
	private Animator anim;
	private TrackMove trackMove;

	void Awake ()
	{
		player = GetComponent<Player> ();
        model = transform.FindChild ("Model");
		anim = player.GetAnimator ();
		trackMove = GameObject.Find ("TrackController").GetComponent<TrackMove> ();
	}

	void OnEnable ()
	{
		trackMove.enabled = true;
		anim.SetBool ("isRunning", true);
	}

	void OnDisable ()
	{
		anim.SetBool ("isRunning", false);
	}

	void HandleInput ()
	{
		UserInput uesrInput = InputController.instance.GetInput ();

		if (uesrInput == UserInput.Up && model.localPosition.y <= 0f)
		{
			player.ChangeState (States.Jumping);
		}
		else if (uesrInput == UserInput.Left && transform.position.x > -Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningLeft);
		} 
		else if (uesrInput == UserInput.Right && transform.position.x < Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningRight);
		}
		else if (uesrInput == UserInput.Down)
		{
			player.ChangeState (States.Sliding);
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
}
