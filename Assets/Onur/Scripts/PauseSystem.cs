using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject moodBar;
    private GameObject timer;
    public GameObject pausePanel;
    private FirstPersonLook firstPersonLook;
    private GameObject date;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("TimerControl");
        firstPersonLook = GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>();
        date = GameObject.Find("DatePanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
        {
            Pause();
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) 
        {
            StopPause();
        }
    }

    private void Pause() 
    {
        Time.timeScale = 0;
        timer.SetActive(false);
        moodBar.SetActive(false);
        pausePanel.SetActive(true);
        date.SetActive(false);
        isPaused = true;
        firstPersonLook.enabled = false;
    }

    private void StopPause()
    {
        Time.timeScale = 1;
        timer.SetActive(true);
        moodBar.SetActive(true);
        date.SetActive(true);
        pausePanel.SetActive(false);
        isPaused = false;
        firstPersonLook.enabled = true;
    }

}
