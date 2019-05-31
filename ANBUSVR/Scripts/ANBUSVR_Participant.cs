using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ANBUSVR_Participant : MonoBehaviour {

    //[HideInInspector]
    public ANBUSVR_API.Participant participant;

    // Use this for initialization
    void Start () {
        participant = new ANBUSVR_API.Participant();
    }

    // Update is called once per frame
    void Update () {
		
	}

}
