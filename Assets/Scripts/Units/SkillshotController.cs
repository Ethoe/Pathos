using UnityEngine;

public class SkillshotController : MonoBehaviour
{
    // Public Vars
    protected Vector2 origin;
    public float range;
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
        origin = owner.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(origin, gameObject.transform.position) >= range)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.CalculateDamage(owner, other.gameObject, FlatDamage(), false);
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
