using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarRatio : MonoBehaviour
{

    private MoodBarController moodBarController;
    public int moodValue = 50;

    // Start is called before the first frame update
    void Start()
    {
         moodBarController = GameObject.Find("MoodBar").GetComponent<MoodBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        moodBarController.moodValue = moodValue;
    }
}
