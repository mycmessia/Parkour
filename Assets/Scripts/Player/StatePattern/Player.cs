using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States {
	Preparing,
	TurningAround,
	Running,
	TurningLeft,
	TurningRight,
	Jumping,
	Sliding,
	Kicking,
	Walking,
	Away
};

public class Player : MonoBehaviour {

	public States m_currentState;

	private List<PlayerState> statesList = new List<PlayerState> ();
	private CharacterController controller;
    private Transform model;

    public float gravity = 0f;
	public float jumpForce = 0f;

	void Awake ()
	{
		statesList.Add (GetComponent<PreparingState> ());
		statesList.Add (GetComponent<TurningAroundState> ());
		statesList.Add (GetComponent<RunningState> ());
		statesList.Add (GetComponent<TurningLeftState> ());
		statesList.Add (GetComponent<TurningRightState> ());
		statesList.Add (GetComponent<JumpingState> ());
		statesList.Add (GetComponent<SlidingState> ());

		controller = GetComponent<CharacterController> ();
        model = transform.FindChild ("Model");

		jumpForce = 0f;
		gravity = 0f;
	}

	public States GetCurState ()
	{
		return m_currentState;
	}

	public void ChangeState (States state)
	{
		DisableAllStates ();

		m_currentState = state;

		switch (state) 
		{
			case States.Preparing:
				GetComponent<PreparingState> ().enabled = true;
				break;
			case States.TurningAround:
				GetComponent<TurningAroundState> ().enabled = true;
				break;
			case States.Running:
				GetComponent<RunningState> ().enabled = true;
				break;
			case States.TurningLeft:
				GetComponent<TurningLeftState> ().enabled = true;
				break;
			case States.TurningRight:
				GetComponent<TurningRightState> ().enabled = true;
				break;
			case States.Jumping:
				GetComponent<JumpingState> ().enabled = true;
				break;
			case States.Sliding:
				GetComponent<SlidingState> ().enabled = true;
				break;
		}
	}

	void DisableAllStates ()
	{
		for (int i = 0; i < statesList.Count; i++) 
		{
			statesList [i].enabled = false;		
		}
	}

	void Update ()
	{
        if (model.localPosition.y > 0f)
        {
			jumpForce -= Config.GRAVITY * Time.deltaTime;
			gravity = 0f;
        }
		else if (!controller.isGrounded)
        {
            gravity -= 1.5f * Time.deltaTime;
        }
	}

	public void Move ()
	{
		controller.Move (new Vector3 (0f, gravity, 0));

		model.localPosition += new Vector3 (0f, jumpForce * Time.deltaTime, 0f);

        if (model.localPosition.y <= 0f)
        {
            model.localPosition = Vector3.zero;
			jumpForce = 0F;
        }

        if (controller.isGrounded)
        {
            gravity = 0f;
        }
	}

	public Animator GetAnimator ()
	{
		return GetComponent <Animator> ();
	}
}
