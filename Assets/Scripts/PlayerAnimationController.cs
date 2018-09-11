using UnityEngine;


public class PlayerAnimationController:MonoBehaviour{
    [SerializeField] private PlayerActor p;
    
    private void Awake(){
        myAnimator = GetComponent<Animator>();
        animGrounded = Animator.StringToHash("ground");
        animDying = Animator.StringToHash("dying");        
    }

    private void Update(){        
        myAnimator.SetBool(animDying,p.isDying());
        myAnimator.SetBool(animGrounded,p.isGrounded());
    }


    private int animGrounded;
    private int animDying;



    private Animator myAnimator;
}
