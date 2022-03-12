using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour
{
    public List<Image> Hearts = new List<Image>();

    public Sprite Full;
    public Sprite Half;
    public Sprite Empty;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnHealthUpdate += UpdateHealthUI;
    }


    void UpdateHealthUI(PlayerHealth health)
    {
        for(int i = 0; i < Hearts.Count; ++i)
        {
            int twoI = i * 2;
            Hearts[i].gameObject.SetActive(twoI < health.MaxHealth);

            if(health.CurrentHealth - twoI > 1)
            {
                Hearts[i].sprite = Full;
            }
            else if(health.CurrentHealth - twoI == 1)
            {
                Hearts[i].sprite = Half;
            }
            else
            {
                Hearts[i].sprite = Empty;
            }
        }
    }
}
