using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour {

    public GameObject follow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - follow.transform.position), 5*Time.deltaTime);
	}
}
