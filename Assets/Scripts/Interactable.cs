using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Interactable: MonoBehaviour{



    [SerializeField]protected Collider2D mySizeTrigger2D;





    void setupRigid(){
        
        
        
        //kinematic rigidbody setup
        myRigidbody2D.isKinematic = myRigidbody2D.useFullKinematicContacts = true;
        

        
        //dynamic rigidbody setup
//        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
//        myRigidbody2D.gravityScale = 0;
        
        
        //interpolation setup
        myRigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        
    }
    protected virtual void Awake(){           
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
        
        Assert.IsNotNull(mySizeTrigger2D,"add a size trigger volume");
        Assert.IsTrue(mySizeTrigger2D.isTrigger,"enable trigger on collider");  
        setupRigid();
        

    }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        
    }


    public virtual Vector2 getTopRight(){
        return mySizeTrigger2D.bounds.max;           
    }
    

    public virtual Vector2 getbottomLeft(){
        return mySizeTrigger2D.bounds.min;
    }


    public virtual float getBottomDistance(){

        return myTransform.position.y-mySizeTrigger2D.bounds.min.y;
       
    }
    
    

    public virtual float getTopDistance(){
        return mySizeTrigger2D.bounds.max.y-myTransform.position.y;

    }

    
    
    public virtual Vector2 getSize(){
        return mySizeTrigger2D.bounds.size;
      
    }
    
        
    public virtual Vector2 getCenter(){
        return mySizeTrigger2D.bounds.center;
      
    }
    
    public virtual void move(float move){      
      
//        myTransform.position+=new Vector3(move,0,0);
        
      
        
        //if else here for tesitng differnt rigidbody configurations
        
        if (myRigidbody2D.bodyType == RigidbodyType2D.Dynamic){
            myRigidbody2D.AddForce(new Vector2(move/Time.fixedDeltaTime-myRigidbody2D.velocity.x,0),ForceMode2D.Impulse);
        }
        
        else if (myRigidbody2D.bodyType == RigidbodyType2D.Kinematic){
        
            
            myRigidbody2D.MovePosition(myRigidbody2D.position+new Vector2(move,0));

        }
                                                                                                                                                       
  
    }




    protected Rigidbody2D myRigidbody2D;
    protected Transform myTransform;

}

