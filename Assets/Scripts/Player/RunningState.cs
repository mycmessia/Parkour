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
//		trackMove.enabled = false;
		anim.SetBool ("isRunning", false);
	}

	void HandleInput ()
	{
        if (Input.GetKeyUp (KeyCode.UpArrow) && model.localPosition.y <= 0f)
		{
			player.ChangeState (States.Jumping);
		}
		else if (Input.GetKeyUp (KeyCode.LeftArrow) && transform.position.x > -Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningLeft);
		} 
		else if (Input.GetKeyUp (KeyCode.RightArrow) && transform.position.x < Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningRight);
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow))
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
