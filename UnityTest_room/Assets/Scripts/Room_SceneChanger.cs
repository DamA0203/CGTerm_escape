using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room_SceneChanger : MonoBehaviour {
    public GameObject menuWindow;
    public GameObject menuTitleButton;
    public GameObject menuExitButton;
    public GameObject menuCancelButton;

    public GameObject titleWindow;
    public GameObject titleYesButton;
    public GameObject titleNoButton;

    public GameObject exitWindow;
    public GameObject exitYesButton;
    public GameObject exitNoButton;

    bool menuWindowOnCheck;
    bool titleWindowOnCheck;
    bool exitWindowOnCheck;

    // Use this for initialization
    void Start () {
        Debug.Log("Game start.");

        menuWindow.SetActive(false);
        menuTitleButton.SetActive(false);
        menuExitButton.SetActive(false);
        menuCancelButton.SetActive(false);
        menuWindowOnCheck = false;

        titleWindow.SetActive(false);
        titleYesButton.SetActive(false);
        titleNoButton.SetActive(false);
        titleWindowOnCheck = false;

        exitWindow.SetActive(false);
        exitYesButton.SetActive(false);
        exitNoButton.SetActive(false);
        exitWindowOnCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Notification window
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuWindowOnCheck && !titleWindowOnCheck && !exitWindowOnCheck)
            {
                Debug.Log("[Escape]Open menu window.");
                menuWindowOnCheck = true;
            }
            else if (menuWindowOnCheck)
            {
                Debug.Log("[Escape]Close menu window.");
                menuWindowOnCheck = false;
            }
            else if (titleWindowOnCheck)
            {
                Debug.Log("[Escape]Go back to menu window.");
                menuWindowOnCheck = true;
                titleWindowOnCheck = false;
            }
            else if (exitWindowOnCheck)
            {
                Debug.Log("[Escape]Go back to menu window.");
                menuWindowOnCheck = true;
                exitWindowOnCheck = false;
            }
        }

        if (menuWindowOnCheck)
        {
            menuWindow.SetActive(true);
            menuTitleButton.SetActive(true);
            menuExitButton.SetActive(true);
            menuCancelButton.SetActive(true);
        }
        else
        {
            menuWindow.SetActive(false);
            menuTitleButton.SetActive(false);
            menuExitButton.SetActive(false);
            menuCancelButton.SetActive(false);
        }

        if (titleWindowOnCheck)
        {
            titleWindow.SetActive(true);
            titleYesButton.SetActive(true);
            titleNoButton.SetActive(true);
        }
        else
        {
            titleWindow.SetActive(false);
            titleYesButton.SetActive(false);
            titleNoButton.SetActive(false);
        }

        if (exitWindowOnCheck)
        {
            exitWindow.SetActive(true);
            exitYesButton.SetActive(true);
            exitNoButton.SetActive(true);
        }
        else
        {
            exitWindow.SetActive(false);
            exitYesButton.SetActive(false);
            exitNoButton.SetActive(false);
        }
    }

    public void TitleButton()
    {
        Debug.Log("[Title]Open title notification window.");
        menuWindowOnCheck = false;
        titleWindowOnCheck = true;
    }

    public void ExitButton()
    {
        Debug.Log("[Exit]Open exit notification window.");
        menuWindowOnCheck = false;
        exitWindowOnCheck = true;
    }

    public void CancelButton()
    {
        Debug.Log("[Cancel]Close menu window.");
        menuWindowOnCheck = false;
    }

    public void TitleYesButton()
    {
        Debug.Log("[Yes]Go back to the title.");
        SceneManager.LoadScene("title");
    }

    public void TitleNoButton()
    {
        Debug.Log("[No]Go back to menu window.");
        menuWindowOnCheck = true;
        titleWindowOnCheck = false;
    }

    public void ExitYesButton()
    {
        Debug.Log("[Exit]Exit game.");
        Application.Quit();
    }

    public void ExitNoButton()
    {
        Debug.Log("[Exit]Go back to menu window.");
        menuWindowOnCheck = true;
        exitWindowOnCheck = false;
    }
}
