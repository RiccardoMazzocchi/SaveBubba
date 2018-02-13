using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    static MusicManager instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            print("Duplicate music player removed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
