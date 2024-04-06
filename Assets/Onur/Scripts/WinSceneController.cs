using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // E�er R tu�una bas�l�rsa
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 2 numaral� sahneyi y�kle
            SceneManager.LoadScene(2);
        }

        // E�er ESC tu�una bas�l�rsa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Oyunu kapat
            Application.Quit();
        }
    }
}
