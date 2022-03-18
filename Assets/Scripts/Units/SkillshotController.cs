using UnityEngine;

public class SkillshotController : MonoBehaviour
{
    // Public Vars
    protected float lifeSpan = 1.0f;
    public float coolDown;
    public GameObject owner;
    public StatBlock stats;

    // Private Vars
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = owner.GetComponent<StatBlockComponent>().stats;
    }

    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.CalculateDamage(owner, other.gameObject, gameObject, false);
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction.normalized * force);
    }

    public virtual float FlatDamage()
    {
        return 0;
    }
}
