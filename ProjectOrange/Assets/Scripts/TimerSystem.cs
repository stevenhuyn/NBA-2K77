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

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = startingTime;
        InvokeRepeating("Decement", 0, granularity);
    }

    void FixedUpdate() {
        gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:0.00}", time);
        if (time <= redThresholdTime) {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (time <= 0) {
            CancelInvoke();
            time = 0;
        }
    }

    void Decement() {
        time -= granularity;
    }
}
