using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

    public List<string> scenes;
    public string current_scene;
    public bool randomScenes = false;

    // Use this for initialization
    void Start () {


        //si queremos que las escenas salgan de forma aleatoria
        if (randomScenes == true)
        {
            Shufflescenes(scenes);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Shufflescenes(List<string> scenes)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            string temp = scenes[i];
            int randomindex = Random.Range(0, scenes.Count);
            scenes[i] = scenes[randomindex];
            scenes[randomindex] = temp;
        }
    }

    public void nextscene()
    {
            current_scene = scenes[0];
            scenes.Remove(current_scene);
            SceneManager.LoadScene(current_scene);
    }
}
