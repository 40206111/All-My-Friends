using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDamage : MonoBehaviour
{
    Color StartColour;
    Color DamagedColour = Color.red;
    SpriteRenderer Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        StartColour = Sprite.color;
    }

    public void DisplayDamaged(float time)
    {
        StartCoroutine(DamagedCoroutine(time));
    }

    private IEnumerator DamagedCoroutine(float time)
    {
        float elapsed = Time.deltaTime;
        while(elapsed < time)
        {
            var cos = Mathf.Cos(MathUtils.Tau * elapsed / 0.15f);
            var movedCos = 0.5f * cos + 0.5f;
            Sprite.color = Color.Lerp(StartColour, DamagedColour, 1f - movedCos);

            yield return null;
            elapsed += Time.deltaTime;
        }
        Sprite.color = StartColour;
    }
}
