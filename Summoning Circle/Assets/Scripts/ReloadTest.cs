using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadTest : MonoBehaviour
{
    bool wereAlive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!wereAlive && PlayerHub.Instance != null)
        {
            wereAlive = true;
        }
        if(wereAlive && PlayerHub.Instance == null){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
