using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour{

	
	
	
	private void Awake(){
		myAudioSource = GetComponent<AudioSource>();
		
	}

	public void stopAudio(){
		myAudioSource.Stop();
	}
	
	
	
	private AudioSource myAudioSource;

}
