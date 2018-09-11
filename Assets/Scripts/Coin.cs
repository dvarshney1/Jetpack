using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;




public class Coin:MonoBehaviour{
    
    
    
    private void Awake(){
        
        myAudioSource = GetComponent<AudioSource>();
        
    }



    IEnumerator onDeath(){
        
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        myAudioSource.Play();
        //using wait for seconds for efficieny
        yield return new WaitForSeconds(myAudioSource.clip.length);
        myAudioSource.Stop();
        
 
        Destroy(gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        
        Assert.IsTrue(other.gameObject.CompareTag("Player"));
        Debug.Log("other guy is "+other.gameObject.name);


        StartCoroutine(onDeath());
    }

    private AudioSource myAudioSource;
}
