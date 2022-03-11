using UnityEngine;
using TMPro;

public class DamageTextProcedural : MonoBehaviour
{
    public Color color_i, color_f;
    public Vector2 initialOffset, finalOffset; //position to drift to, relative to the gameObject's local origin
    public float size_i, size_f;
    public float fadeDuration;
    private float fadeStartTime;
    private TextMeshPro damageText;
    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<TextMeshPro>();
        fadeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;
        if (progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            transform.localPosition = Vector2.Lerp(initialOffset, finalOffset, progress);
            damageText.color = Color.Lerp(color_i, color_f, progress);
            damageText.fontSize = Mathf.Lerp(size_i, size_f, progress);
        }
        else Destroy(gameObject);
    }
}