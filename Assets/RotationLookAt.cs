using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLookAt : MonoBehaviour {


    GameObject target;
	// Use this for initialization
	void Start () {

        target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);

    }
}
