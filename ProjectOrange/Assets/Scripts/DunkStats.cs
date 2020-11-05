using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DunkStats : MonoBehaviour
{
    static DunkStats instance;

    private HoopController[] allHoops;
    private int numBalls;
    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }

    void Start() {
        Ball[] balls = UnityEngine.Object.FindObjectsOfType<Ball>();
        numBalls = Array.FindAll<Ball>(balls, h => h.gameObject.activeInHierarchy).Length;
        allHoops = UnityEngine.Object.FindObjectsOfType<HoopController>();
    }

    // Gives number of hoops in scene
    public static int NumberOfHoops() {
        return instance.allHoops.Length;
    }

    // Gives number of hoops in scene that were cleared
    public static int NumberOfClearedHoops() {
        return Array.FindAll<HoopController>(instance.allHoops,
            h => h.disabled && h.gameObject.activeInHierarchy).Length;
    }

    // Gives number of balls that existed in the scene at Start() invocation
    public static int NumberOfBalls() {
        return instance.numBalls;
    }

    public static int BallsLeft() {
        Ball[] balls = UnityEngine.Object.FindObjectsOfType<Ball>();
        return  Array.FindAll<Ball>(balls, h => h.gameObject.activeInHierarchy).Length;
    }
}
