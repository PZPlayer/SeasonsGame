using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    [SerializeField] private Color damageColor;
    [SerializeField] private Color healColor;

    public float speed = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void FlashDamage()
    {
        StartCoroutine(FadeToRed(damageColor));
    }

    public void FlashHeal()
    {
        StartCoroutine(FadeToRed(healColor));
    }

    private IEnumerator FadeToRed(Color color)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            spriteRenderer.color = Color.Lerp(originalColor, color, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null; 
        }
        RestoreColor();
    }

    private void RestoreColor()
    {
        spriteRenderer.color = originalColor;
    }
}
