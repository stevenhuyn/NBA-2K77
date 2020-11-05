using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Canvas LevelSelect;
    public Canvas EscapeMenu;
    public Canvas FinishMenu;

    private GameObject Crosshair;
    private MouseLook mouseLookScript;

    public GameObject[] LevelButtons;

    public Text DoneText;

    public static bool isSandbox = false;

    public enum MenuState
    {
        None,
        EscapeMenu,
        Finish,
        LevelSelect
    }

    public static MenuState menuState;

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

            if (menuState == MenuState.None || menuState == MenuState.Finish) {
                Crosshair.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuState = MenuState.EscapeMenu;
                Time.timeScale = 0;
                FinishMenu.enabled = false;
            } else {
                Crosshair.SetActive(true);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                menuState = MenuState.None;
                Time.timeScale = 1;
            }
        }
    }

    void FixedUpdate() {
        if (DunkStats.BallsLeft() == 0 && LevelManager.level != 1) {
            StartCoroutine(FinishedGame());
        }
    }

    IEnumerator FinishedGame() {
        yield return new WaitForSeconds(3f);
        menuState = MenuState.Finish;
        LevelSelect.enabled = false;
        EscapeMenu.enabled = false;
        FinishMenu.enabled = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Crosshair.SetActive(false);
        DoneText.text = GenerateFinishText();
    }

    public void handleMenuPress() {
        SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
        isSandbox = false;
        menuState = MenuState.None;
        Time.timeScale = 1;
    }

    public void handleRestartPress() {
        LevelManager.ResetLevel();
        if (LevelManager.level != 1) isSandbox = false;
        EscapeMenu.enabled = false;
        menuState = MenuState.None;
        Time.timeScale = 1;
    }

    public void handleNextLevel() {
        LevelManager.NextLevel();
        EscapeMenu.enabled = false;
        menuState = MenuState.None;
        isSandbox = false;
        Time.timeScale = 1;
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
        Time.timeScale = 1;
    }

    public void handleLevelButton() {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int level = Int32.Parse(buttonName.Substring(3));
        LevelManager.ChangeLevel(level);
        isSandbox = false;
        menuState = MenuState.LevelSelect;
        Time.timeScale = 1;
    }

    public void handleDone() {
        menuState = MenuState.EscapeMenu;
        EscapeMenu.enabled = true;
        FinishMenu.enabled = false;
        Time.timeScale = 0;
    }

    private string GenerateFinishText() {
        int hoopBonus = DunkStats.NumberOfClearedHoops() == DunkStats.NumberOfHoops() ? DunkStats.NumberOfHoops() * 1000 : 0;
        int timerBonus = (int) TimerSystem.GetTime() * (DunkStats.NumberOfBalls() + 1) * 50;
        return string.Format(
@"All Dunk Bonus: {0}
TimeBonus: {1}
Score: {2}

FINAL SCORE

{3}
",
         hoopBonus,
         timerBonus,
         ScoreSystem.GetScore(),
         hoopBonus + timerBonus + ScoreSystem.GetScore()
         );
    }
}
