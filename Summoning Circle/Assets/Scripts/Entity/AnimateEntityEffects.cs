using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEntityEffects : MonoBehaviour
{
    Color StartColour;
    Color DamagedColour = Color.red;
    Color FrozenColour = Color.grey;
    SpriteRenderer Sprite;

    bool IsFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        StartColour = Sprite.color;
    }

    public void DisplayFrozen(EntityHub hub)
    {
        StartCoroutine(FrozenCoroutine(hub));
    }

    private IEnumerator FrozenCoroutine(EntityHub hub)
    {
        Sprite.color = FrozenColour;
        IsFrozen = true;
        while (hub.FrozenMovement > 0)
        {
            yield return null;
        }
        IsFrozen = false;
        Sprite.color = StartColour;
    }

    public void DisplayDamaged(float time)
    {
        StartCoroutine(DamagedCoroutine(time));
    }

    private IEnumerator DamagedCoroutine(float time)
    {
        float elapsed = Time.deltaTime;
        while (elapsed < time)
        {
            var cos = Mathf.Cos(MathUtils.Tau * elapsed / 0.15f);
            var movedCos = 0.5f * cos + 0.5f;
            Sprite.color = Color.Lerp(StartColour, DamagedColour, 1f - movedCos);

            yield return null;
            elapsed += Time.deltaTime;
        }
        Sprite.color = IsFrozen ? FrozenColour : StartColour;
    }
}
