using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// 禁用不是本客户端的脚本
public class PlayerNetworkSetup : NetworkBehaviour  {

	public Behaviour[] componentsToDisable;

	void Start ()
	{
		if (isLocalPlayer == false) 
		{
			DisableComponent ();
		}
	}

	void DisableComponent ()
	{
		foreach (Behaviour component in componentsToDisable)
		{
			component.enabled = false;
		}
	}
}
