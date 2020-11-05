using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int level = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static string getLevelName() {
        return String.Format("lvl{0}", level);
    }
    public static void ResetLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    public static void BackLevel() {
        level--;
        SceneManager.LoadScene(getLevelName(), LoadSceneMode.Single);
    }

    public static void NextLevel() {
        level++;
        SceneManager.LoadScene(getLevelName(), LoadSceneMode.Single);
    }

    public static void ChangeLevel(int newLevel) {
        level = newLevel;
        SceneManager.LoadScene(getLevelName(), LoadSceneMode.Single);

        if (level == 1) {
            MenuScript.isSandbox = true;
        }
    }
}
