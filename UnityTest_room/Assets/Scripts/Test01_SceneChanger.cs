using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Test01_SceneChanger : MonoBehaviour {
    public GameObject menuBox;
    public GameObject menuTitleButton;
    public GameObject menuExitButton;
    public GameObject menuCancelButton;

    public GameObject titleBox;
    public GameObject titleYesButton;
    public GameObject titleNoButton;

    public GameObject exitBox;
    public GameObject exitYesButton;
    public GameObject exitNoButton;

    bool menuBoxOnCheck;
    bool titleBoxOnCheck;
    bool exitBoxOnCheck;

    // Use this for initialization
    void Start () {
        Debug.Log("Now game start.");

        menuBox.SetActive(false);
        menuTitleButton.SetActive(false);
        menuExitButton.SetActive(false);
        menuCancelButton.SetActive(false);
        menuBoxOnCheck = false;

        titleBox.SetActive(false);
        titleYesButton.SetActive(false);
        titleNoButton.SetActive(false);
        titleBoxOnCheck = false;

        exitBox.SetActive(false);
        exitYesButton.SetActive(false);
        exitNoButton.SetActive(false);
        exitBoxOnCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Notification window about exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuBoxOnCheck && !titleBoxOnCheck && !exitBoxOnCheck)
            {
                Debug.Log("Escape-button. Open menu window.");
                menuBoxOnCheck = true;
            }
            else if (menuBoxOnCheck)
            {
                Debug.Log("Escape-button. Close menu window.");
                menuBoxOnCheck = false;
            }
            else if (titleBoxOnCheck)
            {
                Debug.Log("Escape-button. Go back to menu window.");
                menuBoxOnCheck = true;
                titleBoxOnCheck = false;
            }
            else if (exitBoxOnCheck)
            {
                Debug.Log("Escape-button. Go back to menu window.");
                menuBoxOnCheck = true;
                exitBoxOnCheck = false;
            }
        }

        if (menuBoxOnCheck)
        {
            menuBox.SetActive(true);
            menuTitleButton.SetActive(true);
            menuExitButton.SetActive(true);
            menuCancelButton.SetActive(true);
        }
        else
        {
            menuBox.SetActive(false);
            menuTitleButton.SetActive(false);
            menuExitButton.SetActive(false);
            menuCancelButton.SetActive(false);
        }

        if (titleBoxOnCheck)
        {
            titleBox.SetActive(true);
            titleYesButton.SetActive(true);
            titleNoButton.SetActive(true);
        }
        else
        {
            titleBox.SetActive(false);
            titleYesButton.SetActive(false);
            titleNoButton.SetActive(false);
        }

        if (exitBoxOnCheck)
        {
            exitBox.SetActive(true);
            exitYesButton.SetActive(true);
            exitNoButton.SetActive(true);
        }
        else
        {
            exitBox.SetActive(false);
            exitYesButton.SetActive(false);
            exitNoButton.SetActive(false);
        }
    }

    public void TitleButton()
    {
        Debug.Log("Title-button. Open title notification window.");
        menuBoxOnCheck = false;
        titleBoxOnCheck = true;
    }

    public void ExitButton()
    {
        Debug.Log("Exit-button. Open exit notification window.");
        menuBoxOnCheck = false;
        exitBoxOnCheck = true;
    }

    public void CancelButton()
    {
        Debug.Log("Cancel-button. Close menu window.");
        menuBoxOnCheck = false;
    }

    public void TitleYesButton()
    {
        Debug.Log("Yes-button. Go back to the title.");
        SceneManager.LoadScene("title");
    }

    public void TitleNoButton()
    {
        Debug.Log("No-button. Go back to menu window.");
        menuBoxOnCheck = true;
        titleBoxOnCheck = false;
    }

    public void ExitYesButton()
    {
        Debug.Log("Exit-button. Exit game.");
        Application.Quit();
    }

    public void ExitNoButton()
    {
        Debug.Log("Exit-button. Go back to menu window.");
        menuBoxOnCheck = true;
        exitBoxOnCheck = false;
    }
}
