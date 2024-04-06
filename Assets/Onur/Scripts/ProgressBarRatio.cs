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

        if (moodValue <= 0)
        {
            LostTheGame();
        }

        else if (moodValue >= 100)
        {
            WonTheGame();
        }

        if (Input.GetKeyDown(KeyCode.F10) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            wonObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F11) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            lostObject.SetActive(true);
        }

    }

    private void LostTheGame()
    {
        lostObject.SetActive(true);
    }

    private void WonTheGame()
    {
        wonObject.SetActive(true);
    }


}
