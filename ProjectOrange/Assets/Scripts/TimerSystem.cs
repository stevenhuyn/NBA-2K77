using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    static public TimerSystem instance;

    private float time;

    private float granularity = 0.01f;

    public float startingTime = 30;

    public float redThresholdTime = 10;

    private bool hasSeenZero;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = MenuScript.isSandbox ? 0.01f : startingTime;
        InvokeRepeating("Decement", 0, granularity);
        hasSeenZero = false;
    }

    void FixedUpdate() {
        gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:0.00}", time);
        if (time <= redThresholdTime && !MenuScript.isSandbox) {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (time <= 0) {
            if (hasSeenZero == false) {
                hasSeenZero = true;
                ScoreSystem.ResetMultiplier();
                ScoreSystem.UpdateMultiplier(-1);
            }
            CancelInvoke();
            time = 0;
        }
    }

    void Decement() {
        if (MenuScript.isSandbox) {
            time += granularity;
        } else {
            time -= granularity;
        }
    }

    public static float GetTime() {
        return instance.time;
    }
}
