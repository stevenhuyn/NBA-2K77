using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopupScript : MonoBehaviour
{

    public int lifeLength;

    private int timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        timeAlive++;
        if (timeAlive >= lifeLength) {
            Destroy(this.gameObject);
        }
    }
}
