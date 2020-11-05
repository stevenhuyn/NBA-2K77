using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas LevelSelect;
    public Canvas EscapeMenu;
    public Canvas FinishMenu;

    private GameObject Crosshair;
    private MouseLook mouseLookScript;

    public GameObject[] LevelButtons;

    public static bool isSandbox = false;

    enum MenuState
    {
        None,
        EscapeMenu,
        Finish,
        LevelSelect
    }

    private MenuState menuState;

    private bool menuActive;

    void Start()
    {
        menuState = MenuState.None;
        LevelSelect.worldCamera = Camera.main;
        EscapeMenu.worldCamera = Camera.main;
        FinishMenu.worldCamera = Camera.main;
        LevelSelect.enabled = false;
        EscapeMenu.enabled = false;
        FinishMenu.enabled = false;
        Crosshair = GameObject.Find("Crosshair");
        mouseLookScript = Camera.main.GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            mouseLookScript.enabled = !mouseLookScript.enabled;
            EscapeMenu.enabled = !EscapeMenu.enabled;
            Crosshair.SetActive(!Crosshair.activeSelf);
            if (menuState == MenuState.None) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuState = MenuState.EscapeMenu;
            } else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                menuState = MenuState.None;
            }
        }
    }

    public void handleMenuPress() {
        SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
        isSandbox = false;
    }

    public void handleRestartPress() {
        LevelManager.ResetLevel();
        isSandbox = false;
        EscapeMenu.enabled = false;
        menuState = MenuState.None;
    }

    public void handleNextLevel() {
        LevelManager.NextLevel();
        EscapeMenu.enabled = false;
        menuState = MenuState.None;
        isSandbox = false;
    }

    public void handleLevelSelect() {
        menuState = MenuState.LevelSelect;
        LevelSelect.enabled = true;
        EscapeMenu.enabled = false;
    }

    public void handleBack() {
        menuState = MenuState.EscapeMenu;
        LevelSelect.enabled = false;
        EscapeMenu.enabled = true;
    }

    public void handleSandbox() {
        EscapeMenu.enabled = false;
        menuState = MenuState.None;
        isSandbox = true;
        LevelManager.ResetLevel();
    }

    // Handles all the nextLevel menu buttons
    public void handleLevelButton() {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        Debug.Log(buttonName.Substring(3));
        int level = Int32.Parse(buttonName.Substring(3));
        LevelManager.ChangeLevel(level);
        isSandbox = false;
    }
}
