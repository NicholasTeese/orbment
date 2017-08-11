using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioSource collectableAudio;

	public AudioClip orbPickUp;
	public AudioClip dashAudio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OrbPickUp(){
		collectableAudio.PlayOneShot (orbPickUp, 0.7f);
		collectableAudio.pitch = 1 + Random.Range (-0.3f, 0.3f);
	}
	public void DashSound(){
		collectableAudio.PlayOneShot (dashAudio, 0.7f);
		collectableAudio.pitch = 1 + Random.Range (-0.3f, 0.3f);
	}
}
