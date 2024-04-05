using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    
    public float waitTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }

}
