using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToWin : MonoBehaviour
{
    public MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToWinScreen());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ToWinScreen()
    {
        musicController.endTheScene = true;
        yield return new WaitForSeconds(8.5f);
        SceneManager.LoadScene(5);
    }

}
