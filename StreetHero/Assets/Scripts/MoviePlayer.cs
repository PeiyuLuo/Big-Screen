using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class MoviePlayer : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audio;
    
    private bool startPlaying;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
        startPlaying = true;

	}
	
	// Update is called once per frame
	void Update () {
 
        
    
	}
}
