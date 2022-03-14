using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedSceneScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > 1.0f)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("GameplayScene");
            }
        }
    }
}
