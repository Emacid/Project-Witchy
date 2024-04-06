using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ProgressBarRatio : MonoBehaviour
{

    private MoodBarController moodBarController;
    public int moodValue = 50;

    public GameObject wonObject;
    public GameObject lostObject;

    // Start is called before the first frame update
    void Start()
    {
         moodBarController = GameObject.Find("MoodBar").GetComponent<MoodBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        moodBarController.moodValue = moodValue;

        if(moodValue <= 0) 
        {
            LostTheGame();
        }

        else if(moodValue >= 100) 
        {
            WonTheGame();
        }
    }

    private void LostTheGame() 
    {

    }

    private void WonTheGame() 
    {

    }


}
