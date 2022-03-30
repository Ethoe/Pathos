using UnityEngine;

public class GroundAbilityController : MonoBehaviour
{
    // Public Vars
    protected Vector2 origin;
    protected SpriteRenderer sprite;
    public bool stopOnHit = false;
    public float duration;
    public GameObject owner;
    public StatBlock stats;

    // Private Vars
    private bool isActive = true;
    private Rigidbody2D rigidbody2d;
    private float timeAlive;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        timeAlive = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = owner.GetComponent<StatBlockComponent>().stats;
        origin = owner.transform.position;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            EndSkillshot();
            isActive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
            GameManager.Instance.CalculateDamage(new DamageContext(owner, other.gameObject, FlatDamage(), false, DamageDealtType.Ability));
        if (stopOnHit)
            Destroy(gameObject);
    }

    protected virtual void EndSkillshot()
    {
        Destroy(gameObject);
    }
    public virtual float FlatDamage()
    {
        return 0;
    }
}
