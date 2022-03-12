using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSmash : MonoBehaviour
{
    public SpriteRenderer Indicator;
    public Transform Tentacle;

    public int Damage = 10;

    public eActionState State = eActionState.none;

    public void NewTarget(Vector2 pos, bool startImmediate = true)
    {
        transform.position = pos;
        if (startImmediate)
        {
            StartCoroutine(FlashIndicator());
        }
    }

    private IEnumerator FlashIndicator()
    {
        State = eActionState.windUp;
        Indicator.gameObject.SetActive(true);
        float elapsed = Time.deltaTime;
        while (State == eActionState.windUp)
        {
            var cos = Mathf.Cos(MathUtils.Tau * elapsed / 0.15f);
            var movedCos = 0.5f * cos + 0.5f;
            Indicator.color = Color.Lerp(Color.red, 0.3f * Color.red, 1f - movedCos);

            yield return null;
            elapsed += Time.deltaTime;
        }
        Indicator.gameObject.SetActive(false);
    }

    public void DoAttack()
    {
        State = eActionState.inProgress;
        Tentacle.gameObject.SetActive(true);
        StartCoroutine(TentacleScale());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (State == eActionState.inProgress)
        {
            ContactPoint2D[] colls = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(colls);
            foreach (var c in colls)
            {
                EntityHub eh = c.collider.GetComponent<EntityHub>();
                if (eh != null && eh.Health != null)
                {
                    eh.Health.Damage(Damage);
                }
            }
        }
    }

    private IEnumerator TentacleScale()
    {
        float fullTime = 0.2f;
        float elapsed = Time.deltaTime;
        while (elapsed <= fullTime)
        {
            float progress = Mathf.Clamp01(elapsed / fullTime);
            Tentacle.localScale = new Vector3(progress, progress, 1f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        while (State == eActionState.inProgress)
        {
            yield return null;
        }
        elapsed = 0f;
        while (elapsed <= fullTime)
        {
            float progress = 1f - Mathf.Clamp01(elapsed / fullTime);
            Tentacle.localScale = new Vector3(progress, progress, 1f);
            yield return null;
            elapsed += Time.deltaTime;
        }

        Tentacle.gameObject.SetActive(false);
    }
}
