using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBabyAnimator : MonoBehaviour
{
    public Sprite Idle;
    public Sprite Attack;

    public EntityHub Hub;
    ScareyFolBrain Brain;
    SpriteRenderer Sprite;

    eEntityActions LastAction = eEntityActions.none;
    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();

        Hub = GetComponentInParent<EntityHub>();

        StartCoroutine(GetBrain());
    }

    IEnumerator GetBrain()
    {
        while (Hub.Brain == null)
        {
            yield return null;
        }

        if (Hub.Brain is ScareyFolBrain)
        {
            Brain = Hub.Brain as ScareyFolBrain;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Brain == null)
        {
            return;
        }

        if (LastAction != Brain.Action)
        {
            if (Brain.Action == eEntityActions.idle)
            {
                Sprite.sprite = Idle;
            }
            else if (Brain.Action == eEntityActions.attacking)
            {
                Sprite.sprite = Attack;
            }
            LastAction = Brain.Action;

        }
    }
}
