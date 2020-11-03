using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSystem : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public const float defaultUpdateDelay = 0.8f;
    private float updateDelayRemaining = 0.0f;
    private bool updateDelayed = false;
    private GameObject player;

    enum Step {
        Moving,
        Jumping,
        BallCollection,
        BallGrappling,
        
    }

    private Step step; 

    // Start is called before the first frame update
    void Start() {   
        instructionText = this.GetComponent<TextMeshProUGUI>();
        instructionText.alignment = TextAlignmentOptions.Center;
        player = GameObject.FindWithTag("Player");
        step = Step.Moving;
        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate() {
        updateDelayRemaining = Mathf.Max(0.0f, updateDelayRemaining -= Time.deltaTime);
        if (updateDelayRemaining == 0.0f && updateDelayed) UpdateInstruction();
        print(updateDelayRemaining);
        DetectProgress();
    }

    private void DetectProgress() {
        if (updateDelayRemaining > 0.0f) return;

        switch(step) {
            case Step.Moving: {
                Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
                if (playerVelocity.x != 0 || playerVelocity.z != 0) {
                    BeginInstructionUpdate(defaultUpdateDelay);
                }
                break;
            }
            case Step.Jumping:
            {
                Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
                if (playerVelocity.y > 0) {
                    BeginInstructionUpdate(defaultUpdateDelay);
                }
                break;
            }
        }  
    }

    private void BeginInstructionUpdate(float delay) {
        updateDelayRemaining = delay;
        updateDelayed = true;
    }

    private void UpdateInstruction() {
        updateDelayed = false;
        switch(step) {
            case Step.Moving:
                step = Step.Jumping;
                break;
            case Step.Jumping:
                step = Step.BallCollection;
                break;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        switch(step) {
            case Step.Moving: {
                instructionText.text = "WASD to move";
                break;
            }
            case Step.Jumping: {
                instructionText.text = "Space to jump";
                break;
            }
            case Step.BallCollection: {
                instructionText.text = "Touch balls to pick them up";
                break;
            }
        }
    }
}
