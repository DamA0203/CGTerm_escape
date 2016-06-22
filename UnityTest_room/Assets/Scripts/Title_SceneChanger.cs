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
        Debug.Log("Title Scene.");
        window.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        helpPage.SetActive(false);
        windowOnCheck = false;
        helpOnCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Notification window about exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpOnCheck)
            {
                Debug.Log("[Escape]Close help page.");
                helpPage.SetActive(false);
                helpOnCheck = false;
            }
            else if (!windowOnCheck)
            {
                Debug.Log("[Escape]Open notification window.");
                window.SetActive(true);
                yesButton.SetActive(true);
                noButton.SetActive(true);
                windowOnCheck = true;
            }
            else
            {
                Debug.Log("[Escape]Close notification window.");
                window.SetActive(false);
                yesButton.SetActive(false);
                noButton.SetActive(false);
                windowOnCheck = false;
            }
        }
    }

    public void YesButton()
    {
        Debug.Log("[Yes]Exit game.");
        Application.Quit();
    }

    public void NoButton()
    {
        Debug.Log("[No]Close notification window.");
        window.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        windowOnCheck = false;
    }

    public void StartButton()
    {
        Debug.Log("[Start]Move to game.");
        SceneManager.LoadScene("room");
    }

    public void HelpButton()
    {
        Debug.Log("[Help]Open help page.");
        helpPage.SetActive(true);
        helpOnCheck = true;
    }

    public void HelpPage()
    {
        Debug.Log("[Touch]Close help page.");
        helpPage.SetActive(false);
        helpOnCheck = false;
    }
}
