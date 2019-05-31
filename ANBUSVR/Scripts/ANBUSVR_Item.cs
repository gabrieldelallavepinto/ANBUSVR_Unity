using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANBUSVR_Item : MonoBehaviour {

    public ANBUSVR_API.Item item;

    // Use this for initialization
    void Start()
    {
        var ANBUSVR = GameObject.Find("ANBUSVR");
        var ANBUSVR_api = ANBUSVR.GetComponent<ANBUSVR_API>();

        //definimos un nuevo item
        item = new ANBUSVR_API.Item();
        item.name = gameObject.name;

        StartCoroutine(ANBUSVR_api.PostItem(this));

    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
