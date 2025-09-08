using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    private ObjectPooling objPool;
    [SerializeField] private string objTag;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifeTime;
    private float time = 0;

    private float damage;
    private string tagTargetCheck;
    private string tagNotCheck;

    private void Start()
    {
        objPool = transform.parent.GetComponent<ObjectPooling>();
    }

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            objPool.Return(objTag, gameObject);
        }
    }

    public void Fire(Transform firePoint, float dmg, float bulletSpeed, string tagTargetCheck, string tagNotCheck)
    {
        damage = dmg;
        this.tagTargetCheck = tagTargetCheck;
        this.tagNotCheck = tagNotCheck;
        rb.position = firePoint.position;
        rb.rotation = firePoint.rotation;
        rb.linearVelocity = firePoint.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag(tagNotCheck)) { return; }

        CharacterStat objStat = other.GetComponent<CharacterStat>();

        if (objStat != null && other.CompareTag(tagTargetCheck))
        {
            objStat.TakeDamage(damage);
        }

        objPool.Return(objTag, gameObject);
    }
}
