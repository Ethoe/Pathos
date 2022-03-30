using UnityEngine;

public class SkillshotController : MonoBehaviour
{
    // Public Vars
    protected Vector2 origin;
    protected SpriteRenderer sprite;
    public bool stopOnHit = true;
    public float range;
    public float speed;
    public GameObject owner;
    public StatBlock stats;

    // Private Vars
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = owner.GetComponent<StatBlockComponent>().stats;
        origin = owner.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(origin, gameObject.transform.position) >= range)
            EndSkillshot();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.CalculateDamage(new DamageContext(owner, other.gameObject, FlatDamage(), false, DamageDealtType.Ability));
        if (stopOnHit)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction)
    {
        rigidbody2d.AddForce(direction.normalized * speed);
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
