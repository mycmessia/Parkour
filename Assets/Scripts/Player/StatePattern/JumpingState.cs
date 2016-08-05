using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {

	private Player player;
    private Transform model;
	private Animator anim;
	private int aniRamdom;

	void Awake ()
	{
		player = GetComponent<Player> ();
        model = transform.FindChild ("Model");
		anim = player.GetAnimator ();
	}

	void OnEnable ()
	{
		aniRamdom = Random.Range (0, 2);

        if (aniRamdom == 0)
        {
            anim.SetBool("isJumping1", true);
        }
        else
        {
            anim.SetBool("isJumping2", true);
        }

		player.jumpForce = Config.Player.JUMP_SPEED;
	}

	void OnDisable ()
	{
		if (aniRamdom == 0) 
		{
			anim.SetBool ("isJumping1", false);
		} 
		else 
		{
			anim.SetBool ("isJumping2", false);
		}
	}

	void HandleInput ()
	{
		UserInput uesrInput = InputController.instance.GetInput ();

		if (uesrInput == UserInput.Right && transform.position.x < Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningRight);
		}
		else if (uesrInput == UserInput.Left && transform.position.x > -Config.TRACK_WIDTh) 
		{
			player.ChangeState (States.TurningLeft);
		} 
		else if (uesrInput == UserInput.Down)
		{
			player.ChangeState (States.Sliding);
		}
	}

	void Update ()
	{
        HandleInput();
	}

	void FixedUpdate ()
	{
		player.Move ();

        if (model.localPosition.y <= 0f) 
		{
			model.localPosition = Vector3.zero;
			player.jumpForce = 0f;

			player.ChangeState (States.Running);
		}
	}
}
