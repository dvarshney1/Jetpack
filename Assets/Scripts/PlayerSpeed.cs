using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class PlayerSpeed : ScriptableObject {
	
	public void registerPlayer(PlayerActor p){
		Assert.IsNull(player,"player already registered");
		player = p;
		


	}

	public void unregisterPlayer(PlayerActor p){
		Assert.IsTrue(player == p,"player not registered");
		player = null;
		
	}

	
	public float getSpeedX(){
		return player != null? player.getSpeedX():testSpeed;
		
		
	}

	[SerializeField] private PlayerActor player;
	[SerializeField] private float testSpeed;
}
