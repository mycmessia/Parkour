using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackMove : MonoBehaviour {

	private float moveSpeed;

	public List<Transform> trackList = new List<Transform> ();

	void Awake ()
	{
		moveSpeed = Config.TRACK_SPEED;
	}

	void OnEnable ()
	{
		
	}

	void OnDisEnable ()
	{
		
	}

	void Reset (Transform track)
	{
		if (track.FindChild ("Model").FindChild ("Stage") != null)
		{
			track.FindChild ("Model").FindChild ("Stage").gameObject.SetActive (false);
		}
	}

	void FixedUpdate ()
	{
		for (int i = 0; i < trackList.Count; i++)
		{
			trackList[i].localPosition -= new Vector3 (0f, 0f, moveSpeed * Time.deltaTime);

			if (trackList[i].localPosition.z < -Config.TRACK_LENGTH)
			{
				trackList[i].localPosition = new Vector3 (0f, 0f, (trackList.Count - 1) * Config.TRACK_LENGTH);

				Reset (trackList[i]);
			}
		}
	}
}
