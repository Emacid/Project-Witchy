using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLevelByTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ChangeLevelFazt();
        }
    }

    private IEnumerator ChangeLevelByTime() 
    {
        yield return new WaitForSeconds(70.2f);
        SceneManager.LoadScene(2);
    }

    private void ChangeLevelFazt() 
    {
        SceneManager.LoadScene(2);
    }

}
