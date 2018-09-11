using UnityEngine;


public class Scroller:MonoBehaviour{



    [SerializeField] private PlayerSpeed ps;
    
    [Range(0,1)]
    [SerializeField] private float controlFactor;

    private void Awake(){
        repeatLength = ((GetComponent<SpriteRenderer>().sprite.bounds.max -
                        GetComponent<SpriteRenderer>().sprite.bounds.min).x)*transform.localScale.x;


        initPosition = transform.localPosition;
        


    }

    private void Update(){
        distance += ps.getSpeedX()*controlFactor*Time.deltaTime;
        //this keeps precision in check
        distance = Mathf.Repeat(distance,repeatLength);

        //purely visual so using transform to move it in Update.
        transform.localPosition = initPosition+transform.right*-distance;
    }
    
    
    private Vector3 initPosition;
    private float distance;
    private float repeatLength;
    
    
}