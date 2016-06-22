using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room1_SceneChanger : MonoBehaviour {
    public GameObject menuWindow;
    public GameObject menuExitButton;
    public GameObject menuTitleButton;
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
        Debug.Log("Now game start.");

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuWindowOnCheck && !titleWindowOnCheck && !exitWindowOnCheck)
            {
                Debug.Log("Escape-button. Open menu window.");
                menuWindowOnCheck = true;
            }
            else if (menuWindowOnCheck)
            {
                Debug.Log("Escape-button. Close menu window.");
                menuWindowOnCheck = false;
            }
            else if (titleWindowOnCheck)
            {
                Debug.Log("Escape-button. Go back to menu window.");
                menuWindowOnCheck = true;
                titleWindowOnCheck = false;
            }
            else if (exitWindowOnCheck)
            {
                Debug.Log("Escape-button. Go back to menu window.");
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

    public void ExitButton()
    {
        Debug.Log("Exit-button. Open exit notification window.");
        menuWindowOnCheck = false;
        exitWindowOnCheck = true;
    }

    public void TitleButton()
    {
        Debug.Log("Title-button. Open title notification window.");
        menuWindowOnCheck = false;
        titleWindowOnCheck = true;
    }
    
    public void CancelButton()
    {
        Debug.Log("Cancel-button. Close menu window.");
        menuWindowOnCheck = false;
    }

    public void TitleYesButton()
    {
        Debug.Log("Yes-button. Go back to the title.");
        SceneManager.LoadScene("title");
    }

    public void TitleNoButton()
    {
        Debug.Log("No-button. Go back to menu window.");
        menuWindowOnCheck = true;
        titleWindowOnCheck = false;
    }

    public void ExitYesButton()
    {
        Debug.Log("Exit-button. Exit game.");
        Application.Quit();
    }

    public void ExitNoButton()
    {
        Debug.Log("Exit-button. Go back to menu window.");
        menuWindowOnCheck = true;
        exitWindowOnCheck = false;
    }
}
