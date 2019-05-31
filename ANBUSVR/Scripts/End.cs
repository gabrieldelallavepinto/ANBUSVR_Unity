using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class End : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(end());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator end()
    {
        yield return new WaitForSeconds(3);

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
