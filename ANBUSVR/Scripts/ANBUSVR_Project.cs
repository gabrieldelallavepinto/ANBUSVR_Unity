using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ANBUSVR_Project : MonoBehaviour {

    public ANBUSVR_API.Project project;

    // Use this for initialization
    void Start()
    {
        //definimos el nuevo proyecto
        project = new ANBUSVR_API.Project();

        var ANBUSVR = GameObject.Find("ANBUSVR");
        var ANBUSVR_api = ANBUSVR.GetComponent<ANBUSVR_API>();

        StartCoroutine(ANBUSVR_api.GetProject(this));
    }

}
