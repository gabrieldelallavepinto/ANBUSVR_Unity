using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticScene : MonoBehaviour {
    private static GameObject _isCreated;

    private void Awake()
    {
        //si es la escena principal lo guardamos, si no se destruye porque ya existe
        if (_isCreated == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _isCreated = this.gameObject;
        }
        else
        {
            if (_isCreated != this.gameObject)
            {
                Debug.Log("destruimos la copia");
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
