using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectManager : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;
    public ParticleSystem groundClick;

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

    private float CoolDownBuffer = .1f;
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        EventManager.StartListening(Events.DealDamageTrigger, DamageTextAnimation);
        EventManager.StartListening(Events.PlayerTargettedTrigger, HighlightUnit);
        EventManager.StartListening(Events.PlayerEndTargettedTrigger, DeHighlightUnit);
        EventManager.StartListening(Events.PlayerClick, moveIndicator);
    }

    void Update()
    {
        if (CoolDownBuffer > 0)
        {
            CoolDownBuffer -= Time.deltaTime;
        }
    }

    void moveIndicator(Dictionary<string, object> message)
    {
        if (CoolDownBuffer < 0)
        {
            CoolDownBuffer = .1f;
            var location = (Vector2)message["target"];
            GameObject.Instantiate(groundClick, location, Quaternion.identity);
        }
    }
    public void DamageTextAnimation(Dictionary<string, object> message)
    {
        var context = (DamageContext)message["damage"];
        GameObject DamageText = Instantiate(popUpText, context.target.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        TextMeshPro DamageTMPro = DamageText.GetComponent<TextMeshPro>();
        DamageTextProcedural DamageTextP = DamageText.GetComponent<DamageTextProcedural>();
        float size = 7;
        string text = ((int)context.baseDamage).ToString();
        DamageTextP.fadeDuration = 1.0f;
        DamageTextP.color_i = new Color(1, 1, 1, 1);
        DamageTextP.color_f = new Color(1, 1, 1, 0);
        if (context.isCrit)
            size += 3;
        DamageTextP.size_i = size;
        DamageTextP.size_f = 2;
        DamageTextP.initialOffset = context.target.transform.position + new Vector3(0, 1f, 0);
        DamageTextP.finalOffset = context.target.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 2f, 0);
        DamageTMPro.colorGradient = DamageTypeColor(context.type, context.isCrit);
        if (context.isCrit)
            text = text + "!";
        DamageTMPro.SetText(text);

    }

    public VertexGradient DamageTypeColor(DamageDealtType type, bool isCrit)
    {
        switch (type)
        {
            case DamageDealtType.Basic:
                if (isCrit)
                    return new VertexGradient(new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f));
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 1f));
            case DamageDealtType.Ability:
                if (isCrit)
                    return new VertexGradient(new Color(0.117f, 0.321f, 1f, 1f), new Color(0.117f, 0.321f, 1f, 1f), new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f));
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(0.098f, 0.321f, 1f, 1f), new Color(0.098f, 0.321f, 1f, 1f));
            case DamageDealtType.True:
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f));
            default:
                return new VertexGradient(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(1f, 0.023f, 0.239f, 1f), new Color(1f, 0.023f, 0.239f, 1f));
        }
    }

    public void HighlightUnit(Dictionary<string, object> message)
    {
        if (message["target"] != null)
        {
            var target = (GameObject)message["target"];
            if (target != null)
                target.GetComponent<SpriteRenderer>().material = highlightMaterial;
        }
    }

    public void DeHighlightUnit(Dictionary<string, object> message)
    {
        if (message["target"] != null)
        {
            var target = (GameObject)message["target"];
            if (target != null)
                target.GetComponent<SpriteRenderer>().material = defaultMaterial;
        }
    }
}
