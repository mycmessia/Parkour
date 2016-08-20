using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TrackMove : MonoBehaviour {

	private float moveDist;
	private float moveSpeed;

	private Text gameInfo;

	public List<Transform> trackList = new List<Transform> ();

	private float TRACK_TOTAL_LENGTH;

	void Awake ()
	{
		moveDist = 0f;

		moveSpeed = Config.TRACK_SPEED;

		for (int i = 1; i < trackList.Count; i++)
		{
			ResetATrack (trackList[i]);
		}

		gameInfo = GameObject.Find ("GameInfo").GetComponent <Text> ();

		TRACK_TOTAL_LENGTH = (trackList.Count - 1) * Config.TRACK_LENGTH;
	}

	void OnEnable ()
	{
		ClearCureentTrackObs ();
	}

	// Game reset
	void OnDisable ()
	{
		moveDist = 0f;

		moveSpeed = Config.TRACK_SPEED;
	}

	void ClearCureentTrackObs ()
	{
		for (int i = 1; i < trackList.Count; i++)
		{
			if (trackList[i].localPosition.z >= -Config.TRACK_LENGTH / 2F &&
				trackList[i].localPosition.z <= Config.TRACK_LENGTH / 2F)
			{
				CLearObs (trackList[i].FindChild ("Obstacle"));
			}
		}
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
		int passerCountPerTrack = Random.Range (4, 7);

		for (int i = 0; i < passerCountPerTrack; i++)
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
			int zPos = i * Config.TRACK_LENGTH / passerCountPerTrack - 16;

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

	void MovePastTrack ()
	{
		for (int i = 0; i < trackList.Count; i++)
		{
			if (trackList[i].localPosition.z < -Config.TRACK_LENGTH)
			{
				float farestTrack = 0f;
				for (int j = 0; j < trackList.Count; j++)
				{
					if (trackList[j].localPosition.z > farestTrack)
					{
						farestTrack = trackList[j].localPosition.z;
					}
				}

				float newZPos = farestTrack + Config.TRACK_LENGTH;

				trackList[i].localPosition = new Vector3 (0f, 0f, newZPos);

				ResetATrack (trackList[i]);
			}
		}
	}

	void FixedUpdate ()
	{
		for (int i = 0; i < trackList.Count; i++)
		{
			trackList[i].localPosition -= new Vector3 (0f, 0f, moveSpeed * Time.deltaTime);
		}

		MovePastTrack ();

		moveDist += moveSpeed * Time.deltaTime;

		moveSpeed = Config.TRACK_SPEED +  (int) (moveDist / 50F);

		gameInfo.text = "Meters: " + ((int) moveDist).ToString ();
	}
}
