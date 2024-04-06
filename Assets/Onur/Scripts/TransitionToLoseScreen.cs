using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToLoseScreen : MonoBehaviour
{

    public MusicController musicController;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToLoseScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ToLoseScreen() 
    {
        musicController.endTheScene = true;
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(2);
    }

}
