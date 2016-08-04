using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum UserInput
{
	None, Left, Up, Right, Down
}

public class InputController : MonoBehaviour {

	public static InputController instance = null;

	Queue<UserInput> inputsQueue = new Queue<UserInput> ();

	bool isTouchStart = false;
	Vector2 startTouchPos = Vector2.zero;
	float minTouchMoveDis = 16F;

	Text inputText;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
		inputText = GameObject.Find ("InputText").GetComponent<Text> ();
	}

	public UserInput GetInput ()
	{
		if (inputsQueue.Count > 0)
		{
			return inputsQueue.Dequeue ();
		}

		return UserInput.None;
	}

	void GetTouchInput ()
	{
		if (Input.touchCount <= 0)
			return;

		if (Input.GetTouch (0).phase == TouchPhase.Began && isTouchStart == false)
		{
			isTouchStart = true;
			startTouchPos = Input.GetTouch (0).position;
		}

		if (Input.GetTouch (0).phase == TouchPhase.Ended && isTouchStart == true)
		{
			isTouchStart =false;

			Vector2 endTouchPos = Input.GetTouch (0).position;
			float xMoveDis = Mathf.Abs (endTouchPos.x - startTouchPos.x);
			float yMoveDis = Mathf.Abs (endTouchPos.y - startTouchPos.y);

			if (xMoveDis > minTouchMoveDis || yMoveDis > minTouchMoveDis)
			{
				// move left or right
				if (xMoveDis > yMoveDis)
				{
					if (endTouchPos.x - startTouchPos.x > 0)
					{
						// move right
						inputsQueue.Enqueue (UserInput.Right);
						inputText.text = "Right";
					}
					else
					{
						// move left
						inputsQueue.Enqueue (UserInput.Left);
						inputText.text = "Left";
					}
				}
				else	// move up or down
				{
					if (endTouchPos.y - startTouchPos.y > 0)
					{
						// move up
						inputsQueue.Enqueue (UserInput.Up);
						inputText.text = "Up";
					}
					else
					{
						// move down
						inputsQueue.Enqueue (UserInput.Down);
						inputText.text = "Down";
					}
				}
			}
		}
	}

	void GetKeyInput ()
	{

	}

	// Update is called once per frame
	void Update () {
		GetTouchInput ();
		GetKeyInput ();
	}
}