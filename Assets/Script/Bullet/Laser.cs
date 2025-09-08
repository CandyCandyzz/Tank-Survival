using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private ObjectPooling objPool;
    [SerializeField] private string objTag;

    [SerializeField] private float timeDelay;
    [SerializeField] private float laserLifeTime;

    [SerializeField] private ParticleSystem efffect;
    [SerializeField] private LineRenderer lineRenderer;

    public DamageInfo damageInfo;

    private void Start()
    {
        objPool = transform.parent.GetComponent<ObjectPooling>();
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
    }

    public void Shoot(float maxLaserDist,float dmg, string tagCheck)
    {
        Vector3 endPos = new Vector3(0, 0, maxLaserDist);
        lineRenderer.SetPosition(1, endPos);

        efffect.Play();
        StartCoroutine(BeginShoot(maxLaserDist,dmg, tagCheck));
    }

    private IEnumerator BeginShoot(float maxLaserDist, float dmg, string tagCheck)
    {
        yield return new WaitForSeconds(timeDelay);
        lineRenderer.enabled = true;

        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxLaserDist);
        if(hitInfo.collider != null && hitInfo.transform.CompareTag(tagCheck))
        {
            hitInfo.collider.GetComponent<CharacterStat>().TakeDamage(dmg);
        }
        
        StartCoroutine(EndShoot());
    }

    private IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(laserLifeTime);
        lineRenderer.enabled = false;
        yield return new WaitUntil(() => !efffect.IsAlive(true));
        objPool.Return(objTag, gameObject);
    }
}
