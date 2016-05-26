using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        //exit game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        //move next scene
        if (Input.touchCount > 0) {
            Application.LoadLevel("test01");
        }
	}
}
