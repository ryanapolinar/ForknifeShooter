using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    AudioSource music;

	// Use this for initialization
	void Start () {
        music = GetComponent<AudioSource>();
        music.Play();
	}

}
