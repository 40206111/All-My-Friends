using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Contract : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI Tag;
    [SerializeField]
    Image Icon;
    CanvasGroup CG;

    bool Showing = false;

    void Start()
    {
        Pedastal.OnItemPickUp += SpawnContract;
        CG = GetComponent<CanvasGroup>();
        CG.alpha = 0;
    }

    void SpawnContract(Item item)
    {
        StartCoroutine(WaitForContract(item));
    }

    IEnumerator WaitForContract(Item item)
    {
        if (Showing)
        {
            yield return null;
        }
        CG.alpha = 1;
        Showing = true;
        Name.text = item.Name;
        Tag.text = item.TagLine;
        Icon.sprite = item.PedastalIcon;
        Icon.color = item.Colour;
        Icon.SetNativeSize();
        yield return new WaitForSeconds(3f);
        CG.alpha = 0;
        Showing = false;
    }

    private void OnDestroy()
    {
        Pedastal.OnItemPickUp -= SpawnContract;
    }
}
