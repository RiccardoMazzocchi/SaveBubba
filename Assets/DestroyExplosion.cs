using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour {

    Animation explosion;
    float timer;

	// Use this for initialization
	void Start () {
        explosion = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if ( timer >= explosion.clip.length)
        {
            Destroy(gameObject);
        }
	}
}
