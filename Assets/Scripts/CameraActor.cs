using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class CameraActor : MonoBehaviour{


	private void Awake(){
		myTransform = transform;
		myCamera = GetComponent<Camera>();


	}

	
	

	public float getMinX(){
		return myTransform.position.x - myCamera.aspect * myCamera.orthographicSize;
		
	}



	public float getMaxX(){
		return myTransform.position.x + myCamera.aspect * myCamera.orthographicSize;
		
	}
	
	public bool isOffScreenLeft(float worldX ){
		return worldX < myTransform.position.x -myCamera.aspect * myCamera.orthographicSize;
	}
	
	public bool isOffScreenRight(float worldX ){
		return worldX > myTransform.position.x +myCamera.aspect * myCamera.orthographicSize;
	}
	private Transform myTransform;
	private Camera myCamera;
}
