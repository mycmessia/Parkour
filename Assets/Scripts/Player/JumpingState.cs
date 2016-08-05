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
		if (Input.GetKeyUp (KeyCode.LeftArrow) && transform.position.x > -Config.TRACK_WIDTh) 
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
