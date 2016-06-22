using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title_SceneChanger : MonoBehaviour {
    public GameObject box;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject helpPage;

    bool boxOnCheck;
    bool helpOnCheck;

    // Use this for initialization
    void Start () {
        Debug.Log("Now title Page.");
        box.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        helpPage.SetActive(false);
        boxOnCheck = false;
        helpOnCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Notification window about exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpOnCheck)
            {
                Debug.Log("Escape key. Close help page.");
                helpPage.SetActive(false);
                helpOnCheck = false;
            }
            else if (!boxOnCheck)
            {
                Debug.Log("Escape key. Open notification window.");
                box.SetActive(true);
                yesButton.SetActive(true);
                noButton.SetActive(true);
                boxOnCheck = true;
            }
            else
            {
                Debug.Log("Escape key. Close notification window.");
                box.SetActive(false);
                yesButton.SetActive(false);
                noButton.SetActive(false);
                boxOnCheck = false;
            }
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
        box.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        boxOnCheck = false;
    }

    public void StartButton()
    {
        Debug.Log("Start button. Move to game.");
        SceneManager.LoadScene("room_1");
    }

    public void HelpButton()
    {
        Debug.Log("Help button. Open help page.");
        helpPage.SetActive(true);
        helpOnCheck = true;
    }

    public void HelpPage()
    {
        Debug.Log("Touch. Close help page.");
        helpPage.SetActive(false);
        helpOnCheck = false;
    }
}
