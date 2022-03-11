using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;

    public GameObject popUpText;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Effect Manager is dead");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {

    }

    public void DamageTextAnimation(string text, DamageDealtType damagetype, bool isCrit, float size, GameObject target)
    {
        GameObject DamageText = Instantiate(popUpText, target.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        TextMeshPro DamageTMPro = DamageText.GetComponent<TextMeshPro>();
        DamageTextProcedural DamageTextP = DamageText.GetComponent<DamageTextProcedural>();
        DamageTextP.fadeDuration = 1.0f;
        DamageTextP.color_i = new Color(1, 1, 1, 1);
        DamageTextP.color_f = new Color(1, 1, 1, 0);
        if (isCrit)
            size += 2;
        DamageTextP.size_i = size;
        DamageTextP.size_f = 2;
        DamageTextP.initialOffset = target.transform.position + new Vector3(0, 1.5f, 0);
        DamageTextP.finalOffset = target.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 2.5f, 0);
        DamageTMPro.colorGradient = DamageTypeColor(damagetype, isCrit);
        DamageTMPro.SetText(text);

    }

    public VertexGradient DamageTypeColor(DamageDealtType type, bool isCrit)
    {
        switch (type)
        {
            case DamageDealtType.Physical:
                if (isCrit)
                    return new VertexGradient(new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f));
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 1f));
            case DamageDealtType.Magic:
                if (isCrit)
                    return new VertexGradient(new Color(0.117f, 0.321f, 1f, 1f), new Color(0.117f, 0.321f, 1f, 1f), new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f));
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(0.098f, 0.321f, 1f, 1f), new Color(0.098f, 0.321f, 1f, 1f));
            case DamageDealtType.True:
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f));
            default:
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 0.023f, 0.239f, 1f), new Color(1f, 0.023f, 0.239f, 1f));
        }
    }
}
