using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title_SceneChanger : MonoBehaviour {
    public GameObject window;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject helpPage;

    bool windowOnCheck;
    bool helpOnCheck;

	// Use this for initialization
	void Start () {
        Debug.Log("Now title Page.");
        window.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        windowOnCheck = false;

        helpPage.SetActive(false);
        helpOnCheck = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!windowOnCheck && !helpOnCheck)
            {
                Debug.Log("Escape-button. Open notification window.");
                windowOnCheck = true;
            }
            else if (windowOnCheck)
            {
                Debug.Log("Escape-button. Close notification window.");
                windowOnCheck = false;
            }
            else if (helpOnCheck)
            {
                Debug.Log("Escape-Button. Close help Page.");
                helpOnCheck = false;
            }
        }

        if (windowOnCheck)
        {
            window.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
        else
        {
            window.SetActive(false);
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }

        if (helpOnCheck)
        {
            helpPage.SetActive(true);
        }
        else
        {
            helpPage.SetActive(false);
        }
    }

    public void YesButton()
    {
        Debug.Log("Yes-button. Exit game.");
        Application.Quit();
    }

    public void NoButton()
    {
        Debug.Log("No-button. Close notification window.");
        windowOnCheck = false;
    }

    public void StartButton()
    {
        Debug.Log("Start button. Move to game.");
        SceneManager.LoadScene("room_1");
    }

    public void HelpButton()
    {
        Debug.Log("Help button. Open help page.");
        helpOnCheck = true;
    }

    public void HelpPage()
    {
        Debug.Log("Touch. Close help page.");
        helpOnCheck = false;
    }
}
