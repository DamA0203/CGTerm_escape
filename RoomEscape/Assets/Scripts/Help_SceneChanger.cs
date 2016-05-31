using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Help_SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //return start scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("start");
        }
    }
}
