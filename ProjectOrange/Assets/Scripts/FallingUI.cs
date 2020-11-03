using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallingUI : MonoBehaviour {
    static public FallingUI instance;
    void Awake() {
        instance = this;
    }

    public static FallingUI Get() {
        return instance;
    }

    public void UpdateOpacity(float n) {
        instance.GetComponent<CanvasGroup>().alpha = n;
    }


}