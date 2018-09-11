using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class PlayerActor : MonoBehaviour{






	
	



	
	
	
		

	[SerializeField] private PlayerSpeed ps;

	[SerializeField] private float jumpFactor = 4.5f;
	
	[Range(0,100)]
	[SerializeField]private float speedX = 0;
	[SerializeField]private float deathTime = 0;






	[SerializeField]UnityEvent deathEvent;
	


	[SerializeField] private float gameTimeScale = 1;
	
	
	private void OnGUI(){
		GUIStyle myStyle = new GUIStyle();        
		myStyle.fontSize = 20;
		myStyle.normal.textColor = Color.black;
		
		GUILayout.BeginVertical();
		GUILayout.Label("Distance "+Mathf.FloorToInt(distance).ToString()+" m",myStyle);	
		GUILayout.Label("Coins "+coinsCollected.ToString(),myStyle);
		GUILayout.Label("Highest Distance "+prevHighScore,myStyle);
		GUILayout.EndVertical();
		
	}
	
	private enum PlayerState{
		Alive,Dying,Dead
		
	}



	private PlayerState myState;



	public bool isGrounded(){
		return ground;
	
	}
	public bool isDying(){
		return myState == PlayerState.Dying;
	}
	
	public float getSpeedX(){

		if (myState == PlayerState.Alive) return speedX;
		if (myState == PlayerState.Dying) return Mathf.Lerp(speedX,0,Utils.smoothStop4(deathTimer/deathTime));
		return 0;
		
	}



	private int prevHighScore;
	private void Awake(){
		
		myRigidbody = GetComponent<Rigidbody2D>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myState = PlayerState.Alive;
		prevHighScore = PlayerPrefs.GetInt(highScoreKey);

		Time.timeScale = gameTimeScale;

	}

	private void OnEnable(){
		if(ps!=null)ps.registerPlayer(this);
		
	}

	private void OnDisable(){
		if(ps!=null)ps.unregisterPlayer(this);
	}


	
	private void FixedUpdate(){


		if(myState == PlayerState.Dead)return;
			
		
		
		
	
		float speed = getSpeedX() * Time.deltaTime;
		distance += speed;

	
		if(myState!=PlayerState.Alive)return;

	
		

		//true for one frame
		//on jump begin
		if (!prevJumping && jumping){


			//falling down,correct quick
			if (myRigidbody.velocity.y < 0){
				myRigidbody.AddForce(-myRigidbody.velocity,ForceMode2D.Impulse);
			}
			
		}
		//on jump end
		else if (prevJumping && !jumping){
			
			
			myRigidbody.AddForce(-0.5f*myRigidbody.velocity,ForceMode2D.Impulse);
			
		}
		//on jumping
		else if (prevJumping && jumping){

			//still correction needs to be applied
			if (myRigidbody.velocity.y < 0){
				myRigidbody.AddForce(-myRigidbody.velocity,ForceMode2D.Impulse);
			}
			
			Vector2 currentForce = Physics2D.gravity*myRigidbody.gravityScale *-jumpFactor;	
			myRigidbody.AddForce(currentForce, ForceMode2D.Force);

		}
		
		prevJumping = jumping;

	}


	private void OnCollisionEnter2D(Collision2D other){
		
		if(myState==PlayerState.Dead)return;
		if (other.gameObject.CompareTag("TopCeiling")){
			Debug.Log("collided with top ceiling");
			
		}
		else if (other.gameObject.CompareTag("BottomCeiling")){
			Debug.Log("collided with bottom ceiling");
			ground = true;
		}
		
		
	}
	
	private void OnCollisionExit2D(Collision2D other){
		
		if(myState==PlayerState.Dead)return;
		
		if (other.gameObject.CompareTag("TopCeiling")){
			Debug.Log("collided with top ceiling");
			
		}
		
		
		else if (other.gameObject.CompareTag("BottomCeiling")){
			ground = false;
		}
		
		
	}


	

	private void OnTriggerEnter2D(Collider2D other){
		
		
		if(myState!=PlayerState.Alive)return;
		
	

		if (other.gameObject.CompareTag("Coin")){
			Debug.Log("collided with coin");
			collectCoin();
			
		}
		else if (other.gameObject.CompareTag("Obstacle")){
		
			myState = PlayerState.Dying;
			deathEvent.Invoke();


		}
		

	}

	private void collectCoin(){
		
		if(myState!=PlayerState.Alive)return;
		++coinsCollected;

	}





	private IEnumerator playerDeath(){
		
		int prevScore = PlayerPrefs.GetInt(highScoreKey);
		PlayerPrefs.SetInt(highScoreKey, Mathf.FloorToInt(Mathf.Max(distance, prevScore)));
		


		//relaod current scene on death complete
		yield return null;
		//invoke all the death events
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
		
	}

	private void Update(){


		if(myState == PlayerState.Dead)return;
		
		
		if (myState == PlayerState.Dying ){

			if (deathTimer < deathTime){
				deathTimer += Time.deltaTime;
				
			}
			else{
				

				
				
				StartCoroutine(playerDeath());

				deathTimer = 0;
				
				
				myState = PlayerState.Dead;
			}
		}
		
		if(myState != PlayerState.Alive)return;
		
		if (Input.GetKeyUp(KeyCode.Space) || (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)){
			jumping = false;
			
		}
		else if (Input.GetKeyDown(KeyCode.Space) || (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)){
			jumping = true;
		}
	
	}

	public Vector2 getMinPoint(){
		
		return myBoxCollider2D.bounds.min;
		
	}
	
	
	public Vector2 getPosition(){
		return myRigidbody.position;
		

	}


	private Rigidbody2D myRigidbody;
	private BoxCollider2D myBoxCollider2D;
	private const string highScoreKey = "HighScore";

	
	
	//alive state variables
	private int coinsCollected = 0;
	private float distance = 0;
	private bool ground;
	
	//alive state book keeping variables
	private bool jumping;	
	private bool prevJumping;
	
	private int fallFrames;


	


	//dying state variables
	private float deathTimer;

	



}
