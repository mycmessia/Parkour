using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSyncPosition : NetworkBehaviour {

    [SyncVar (hook = "SyncPositionValues")]   // 这个符号代表这个变量Server要不断向Client同步
	private Vector3 syncPos;
	[SyncVar (hook = "SyncModelLocalPositionValues")]
	private Vector3 syncModelLocalPos;

	[SerializeField] Transform myTransform;
	[SerializeField] Transform modelTransform;
	private float lerpRate;
    private float normalLerpRate = 15;
    private float fastLerpRate = 25;

    // 这是一个对Command方法的优化，当玩家移动超过这个值后才更新syncPos
    // 每一个联入Server的Client都会调用主机端的Command方法，这样随着联入的机器增加会给Server端很大压力
    private Vector3 lastPos;
	private Vector3 lastModelLocalPos;
    private float threshold = 0.1f;

    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    private List<Vector3> syncPosList = new List<Vector3> ();
	private List<Vector3> syncModelLocalPoslist = new List<Vector3> ();
    [SerializeField] bool useHistoricalLerping = false;
    private float closeEnough = 0.2f;

    void Start ()
    {
        nClient = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ().client;
        latencyText = GameObject.Find ("LatencyText").GetComponent<Text> ();

        lerpRate = normalLerpRate;
    }

    void Update ()
    {
        // 视频老师认为把lerp函数放在update里执行可以弥合不同机器update执行频率不同带来的有的快有的慢的问题
        LerpPosition ();
        ShowLatency();
    }

	void FixedUpdate ()
	{
        TransmitPosition ();
	}

    void LerpPosition ()
    {
        if (!isLocalPlayer)
        {
            if (useHistoricalLerping)
            {
                HistoricalLerping ();
            }
            else
            {
                OrdinaryLerping ();
            }
        }
    }

    [Command]   // 这个方法在Server执行但是由Client调用，调用过程中Client可以向Server传参
    void CmdProvidePositionToServer (Vector3 pos)
    {
        syncPos = pos;
    }

	[Command]
	void CmdProvideModelLocalPosToServer (Vector3 pos)
	{
		syncModelLocalPos = pos;
	}

    [ClientCallback]
    void TransmitPosition ()
    {
        if (isLocalPlayer && Vector3.Distance (myTransform.position, lastPos) > threshold)
        {
            CmdProvidePositionToServer (myTransform.position);
            lastPos = myTransform.position;
        }

		if (isLocalPlayer && Vector3.Distance (modelTransform.localPosition, lastModelLocalPos) > threshold) 
		{
			CmdProvideModelLocalPosToServer (modelTransform.localPosition);
			lastModelLocalPos = modelTransform.localPosition;
		}
    }

    [Client]
    void SyncPositionValues (Vector3 latestPos)
    {
        syncPos = latestPos;
        syncPosList.Add (syncPos);
    }

	[Client]
	void SyncModelLocalPositionValues (Vector3 latestPos)
	{
		syncModelLocalPos = latestPos;
		syncModelLocalPoslist.Add (syncModelLocalPos);
	}

    void ShowLatency ()
    {
        if (isLocalPlayer)
        {
            latency = nClient.GetRTT();
            latencyText.text = latency.ToString();
        }
    }

    void OrdinaryLerping ()
    {
        myTransform.position = Vector3.Lerp (myTransform.position, syncPos, lerpRate * Time.deltaTime);
		modelTransform.localPosition = Vector3.Lerp (modelTransform.localPosition, syncModelLocalPos, lerpRate * Time.deltaTime);
    }

    void HistoricalLerping ()
    {
        if (syncPosList.Count > 0)
        {
            myTransform.position = Vector3.Lerp (myTransform.position, syncPosList[0], lerpRate * Time.deltaTime);
        
            if (Vector3.Distance (myTransform.position, syncPosList[0]) < closeEnough)
            {
                syncPosList.RemoveAt (0);
            }

            if (syncPosList.Count > 10)
            {
                lerpRate = fastLerpRate;
            }
            else
            {
                lerpRate = normalLerpRate;
            }
        }

		if (syncModelLocalPoslist.Count > 0) 
		{
			modelTransform.localPosition = Vector3.Lerp (
				modelTransform.localPosition, 
				syncModelLocalPoslist[0],
				lerpRate * Time.deltaTime
			);

			if (Vector3.Distance (modelTransform.localPosition, syncModelLocalPoslist[0]) < closeEnough) 
			{
				syncModelLocalPoslist.RemoveAt (0);			
			}

			if (syncModelLocalPoslist.Count > 10) 
			{
				lerpRate = fastLerpRate;
			} 
			else 
			{
				lerpRate = normalLerpRate;
			}
		}	
    }
}
