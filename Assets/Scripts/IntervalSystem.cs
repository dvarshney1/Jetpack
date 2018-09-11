using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;


public class IntervalSystem : MonoBehaviour{


	[Header("Interval Parameters")] 
	[SerializeField]private float initialDistance = 0;
	//distance from player position here
	[SerializeField]private float minDistance = -1;
	[SerializeField]private float maxDistance = -1;


	[Header("Dependencies")]
	[SerializeField]private PlayerActor player;
	[SerializeField]private CameraActor cam;
	[SerializeField]private CeilingActor topCeilingActor;
	[SerializeField]private CeilingActor bootomCeilingActor;



	
	


	[Serializable]
	private struct InteractableEntity{
		[SerializeField]public Interactable actor;
		[SerializeField]public float probability;
	}
	
	[Header("Entities")]
	[SerializeField] private InteractableEntity[] interactableEntities;


	private void OnValidate(){
		//check if probabilities sum to one
		if (interactableEntities.Length > 0 && !probabilitySumIsOne())
			Debug.LogWarning("make sure probability sum to 1");

	}


	private void Awake(){
		Assert.IsTrue(interactableEntities!=null && interactableEntities.Length > 0);
		Assert.IsTrue(player!=null,"link player");
		Assert.IsTrue(minDistance >=0 && maxDistance > 0,"Setup interval Parameters Correctly");
				
		myTransform = transform;
		
		//list from array,requires dot net 4.5
		obstacleActors = new List<Interactable>();
		
		
	}

	
	
	
	private float topCeiling;
	private float bottomCeiling;

	private void Start(){
		
		//stuff dempendent on differnt gameobjects in start
		topCeiling = topCeilingActor.getBottomLeft().y;
		bottomCeiling = bootomCeilingActor.getTopRight().y;
		s = State.StartDelay;
		
	}





	enum State{
		StartDelay,Spwaning,Inactive
		
	}

	private State s = State.Inactive;


	private void moveObstacles(float amount){

		for (int i = 0; i < obstacleActors.Count; ++i){
			obstacleActors[i].move(amount);
		}
		
	}
	private int getPendingObstacles(){
		
		
		int pendingObstacles = 0;
			
		//check if any pending obstacles
		for (int i = obstacleActors.Count - 1; i >= 0; --i){
				
			float posX = obstacleActors[i].getbottomLeft().x;
			float headRoom = 2;
			if (cam.isOffScreenRight(posX-headRoom)){
				++pendingObstacles;
			}

		}

		return pendingObstacles;

	}



	private bool probabilitySumIsOne(){


		float sum = 0;
		for (int i = 0; i < interactableEntities.Length; i++){
			sum += interactableEntities[i].probability;
			
		}

		return Mathf.Approximately(sum, 1);
		
	}


	

	Interactable chooseObstacle(){
		Assert.IsTrue(probabilitySumIsOne());
		float prob = Random.Range(0.0f,1.0f);
		float sum = 0;
		for (int i = 0; i < interactableEntities.Length; ++i){			
			sum += interactableEntities[i].probability;		
			if (prob <= sum){

				return interactableEntities[i].actor;

			}

		}
		Assert.IsTrue(false,"Cannot reach here");
		return null;
	}
	private void spawnObstacle(){




		Interactable chosen = chooseObstacle();

		Vector3 pos = Vector3.zero;

		Interactable newObstacle = Instantiate(chosen,pos, chosen.transform.rotation,myTransform);									

		//no obstacles currently,spawn off screen
		if (obstacleActors.Count == 0){
			pos.x = cam.getMaxX()+Random.Range(minDistance,maxDistance);					
		}
		//else spawn ahead of latest obstacle
		else{
	
			Interactable latest = obstacleActors[obstacleActors.Count - 1];
			float dist = Random.Range(minDistance, maxDistance);
			float latestPosThisFrame  = latest.getTopRight().x;
			pos.x = latestPosThisFrame + dist;

		}
				
		//set y position randomly
		{
			float bottom = newObstacle.getBottomDistance();
			float top = newObstacle.getTopDistance();
			float easing = 0.1f;
			pos.y = Random.Range(bottomCeiling + bottom + easing, topCeiling - top - easing);
		}
				
		newObstacle.transform.position = pos;
		obstacleActors.Add(newObstacle);

	}
	private void destoryOffScreenObstacles(){
					
		for (int i = obstacleActors.Count-1; i >=0; --i){
			float posX = obstacleActors[i].getTopRight().x;
				
				
			float headRoom = 0;
			if (cam.isOffScreenLeft(posX + headRoom)){
				var obs = obstacleActors[i];
				//save to remove since reverse interation
				obstacleActors.Remove(obs);
				Destroy(obs.gameObject);
			}
		}

	}



	private float displacement;
	
	private void FixedUpdate(){
		
		if(s == State.Inactive)return;
		float playerSpeed = player.getSpeedX() * Time.deltaTime;
	
		if (s == State.StartDelay){		
			//initial delay
			if(displacement < initialDistance){

				displacement+=playerSpeed;
			}
			else{
				displacement = 0;
				s = State.Spwaning;
				
			}
		}
		else if (s == State.Spwaning){		
			moveObstacles(-playerSpeed);
			int pendingObstacles = getPendingObstacles();
			if (pendingObstacles < 1){
				spawnObstacle();
			}
			destoryOffScreenObstacles();
			
		}
		
		
	}


	private void OnDrawGizmos(){
		
		
		
		
		//edtior and play mode 
		Gizmos.color = Color.red;
		
		Gizmos.DrawWireCube(new Vector3(player.transform.position.x+minDistance,0,0), Vector3.one);
		Gizmos.DrawWireCube(new Vector3(player.transform.position.x+maxDistance,0,0), Vector3.one);


		
		if (!Application.isPlaying)return;
		
		Debug.Log("here in play mode here ");
		//play mode only drawing
		Gizmos.color = Color.blue;
		for (int i = 0; i < obstacleActors.Count; ++i){
			Gizmos.DrawWireCube(obstacleActors[i].getCenter(), obstacleActors[i].getSize());

		}


	}


	private List<Interactable> obstacleActors;	
	private Transform myTransform;
	
}
