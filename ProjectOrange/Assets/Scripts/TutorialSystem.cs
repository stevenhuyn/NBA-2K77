using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSystem : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    private GameObject player;

    enum Step {
        Moving,
        Jumping,
        BallCollection,
        Swinging,
        Scoring
    }

    private Step step; 

    // Start is called before the first frame update
    void Start() {   
        instructionText = this.GetComponent<TextMeshProUGUI>();
        instructionText.alignment = TextAlignmentOptions.Center;
        player = GameObject.FindWithTag("Player");
        step = Step.Moving;
        UpdateInstruction();
    }

    // Update is called once per frame
    void Update() {
        detectProgress();
    }

    private void detectProgress() {
        switch(step) {
            case Step.Moving: {
                Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
                if (playerVelocity.x > 0 || playerVelocity.z > 0) {
                    step = Step.Jumping;
                    UpdateInstruction();
                }
                break;
            }
            case Step.Jumping:
            {
                Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
                if (playerVelocity.y > 0) {
                    step = Step.BallCollection;
                    UpdateInstruction();
                }
                break;
            }
        }  
    }

    private void UpdateInstruction()
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
                instructionText.text = "Walk to balls to pick them up";
                break;
            }
        }
    }
}
