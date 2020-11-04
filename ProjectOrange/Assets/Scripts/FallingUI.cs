using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallingUI : MonoBehaviour {
    public CharacterController character;

    public float heightToStartFade = -100;
    public float heightToReset = -300;

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update() {
        UpdateOpacity();
    }
    public void UpdateOpacity() {

        float characterHeight = character.gameObject.transform.position.y;

        if (characterHeight < heightToReset) {
            LevelManager.ResetLevel();
        }
        float opacityToSet = Mathf.Clamp(Mathf.InverseLerp(heightToStartFade, heightToReset, characterHeight), 0, 1);

        gameObject.GetComponent<CanvasGroup>().alpha = opacityToSet;
    }


}
