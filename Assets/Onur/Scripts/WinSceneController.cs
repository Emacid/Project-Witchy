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
        // Eðer R tuþuna basýlýrsa
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 2 numaralý sahneyi yükle
            SceneManager.LoadScene(2);
        }

        // Eðer ESC tuþuna basýlýrsa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Oyunu kapat
            Application.Quit();
        }
    }
}
