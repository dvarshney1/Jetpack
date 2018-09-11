using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Rotator : MonoBehaviour{


	
	
	[SerializeField] private float rotationSpeed;

	private void Awake(){
		
		myRigidbody2D = GetComponent<Rigidbody2D>();
		Assert.IsTrue(myRigidbody2D.bodyType == RigidbodyType2D.Kinematic);

	}


	private void FixedUpdate(){
		
		
		myRigidbody2D.MoveRotation(myRigidbody2D.rotation+rotationSpeed*Time.deltaTime);
	}


	private Rigidbody2D myRigidbody2D;

}
