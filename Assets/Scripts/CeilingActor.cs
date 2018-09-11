using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class CeilingActor : MonoBehaviour {
	private void Awake(){



		myTransform = transform;
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		
	
	}



	
	


	
	public Vector2 getBottomLeft(){


		//can use these instead of transform point since the ceiling are aligned with x-y axis and not rotated.
		return myBoxCollider2D.bounds.min;

	}
	

	public Vector2 getTopRight(){


		return myBoxCollider2D.bounds.max;
		
		
	}


	
	
	private void OnDrawGizmos(){

		myTransform = transform;
		myBoxCollider2D = GetComponent<BoxCollider2D>();
				
		
		Gizmos.color = Color.gray;
		Gizmos.DrawWireCube(Vector3.zero,new Vector3(myBoxCollider2D.bounds.size.x,myBoxCollider2D.bounds.size.y,10));

	
	}



	private Transform myTransform;
	private BoxCollider2D myBoxCollider2D;
}
