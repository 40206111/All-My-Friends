using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadTest : MonoBehaviour
{
    bool wereAlive = false;

    // Update is called once per frame
    void Update()
    {
        if(!wereAlive && PlayerHub.Instance != null)
        {
            wereAlive = true;
        }
        if(wereAlive && PlayerHub.Instance == null){
            StartCoroutine(GoToDeathScene());
        }
    }

    private IEnumerator GoToDeathScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("YouDiedScene");
    }
}
