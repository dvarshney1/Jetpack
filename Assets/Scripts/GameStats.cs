using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;


[DisallowMultipleComponent]
public class GameStats:MonoBehaviour{
    
        
    [Range(0,100)][SerializeField]private int framesPerSec;
    private float frequency = 1.0f;
    private string fps;


    private void Start(){
        
        StartCoroutine(_fps());
        
    }

    private IEnumerator _fps() {
        for(;;){
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
        
            fps = string.Format("FPS: {0}" , Mathf.RoundToInt(frameCount / timeSpan));
        }
    }
    
    
    
    void OnGUI(){



        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 20;
        GUI.Label(new Rect(Screen.width/2.0f,Screen.height/2.0f,300,200), fps);
        
        
    }

    
}