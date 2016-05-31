using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title_SceneChanger : MonoBehaviour {

    public GUIStyle btStyleStart;
    public GUIStyle btStyleHelp;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    //exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width * 2 / 5, Screen.height * 3 / 5, Screen.width / 5, Screen.height / 10), "Start", btStyleStart))
        {
            SceneManager.LoadScene("room_1");
        }
        if (GUI.Button(new Rect(Screen.width * 2 / 5, Screen.height * 3 / 4, Screen.width / 5, Screen.height / 10), "Help", btStyleHelp))
        {
            SceneManager.LoadScene("help");
        }
    }
}
