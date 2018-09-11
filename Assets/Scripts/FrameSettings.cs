using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DisallowMultipleComponent]
public class FrameSettings : MonoBehaviour {
	
	


	[SerializeField]
	private bool enableTargetFrameRate;

	
	private void Awake(){
		Application.targetFrameRate = enableTargetFrameRate?60:-1;
		

	}




}
