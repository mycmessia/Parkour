using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackMove : MonoBehaviour {

	private float moveSpeed;

	public List<Transform> trackList = new List<Transform> ();

	void Awake ()
	{
		moveSpeed = Config.TRACK_SPEED;

		for (int i = 1; i < trackList.Count; i++)
		{
			ResetATrack (trackList[i]);
		}
	}

	void OnEnable ()
	{

	}

	void OnDisEnable ()
	{
		
	}

	void CLearObs (Transform obs)
	{
		int obsCount = obs.childCount;

		for (int i = obsCount - 1; i >= 0; i--)
		{
			Destroy (obs.GetChild (i).gameObject);
		}
	}

	void CreateObs (Transform obs)
	{
		for (int i = 0; i < 4; i++)
		{
			int ran = Random.Range (0, 2);
			string passerName = "Passer";
			if (ran == 0)
				passerName = "Passer02";

			GameObject passer = Instantiate (
				Resources.Load ("Prefabs/" + passerName, typeof (GameObject))
			) as GameObject;

			passer.transform.parent = obs;

			int randomLine = Random.Range (0, 3) - 1;
			int zPos = i * 8 - 16;

			passer.transform.localPosition = new Vector3 (randomLine * Config.TRACK_WIDTh, 0f, zPos);

			passer.GetComponent<Passer> ().RandomPasserType ();
		}
	}

	void ResetATrack (Transform track)
	{
		if (track.FindChild ("Model").FindChild ("Stage") != null)
		{
			track.FindChild ("Model").FindChild ("Stage").gameObject.SetActive (false);
		}

		Transform obs = track.FindChild ("Obstacle");

		if (obs.childCount > 0)
			CLearObs (obs);

		CreateObs (obs);
	}

	void FixedUpdate ()
	{
		for (int i = 0; i < trackList.Count; i++)
		{
			trackList[i].localPosition -= new Vector3 (0f, 0f, moveSpeed * Time.deltaTime);

			if (trackList[i].localPosition.z < -Config.TRACK_LENGTH)
			{
				trackList[i].localPosition = new Vector3 (0f, 0f, (trackList.Count - 1) * Config.TRACK_LENGTH);

				ResetATrack (trackList[i]);
			}
		}
	}
}
