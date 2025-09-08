using UnityEngine;

public class LaserShooter
{
    private ObjectPooling objPool;
    private string objTag;

    private Transform[] firePoint;
    private float maxLaserDist;
    private float damage;

    public LaserShooter(ObjectPooling objPool, string objTag, float maxLaserDist, Transform[] firePoint, float damage)
    {
        this.objPool = objPool;
        this.objTag = objTag;
        this.maxLaserDist = maxLaserDist;
        this.firePoint = firePoint;
        this.damage = damage;
    }

    public void Shoot(Vector3 target, string tagCheck)
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            Vector3 dir = (target - firePoint[i].position).normalized;

            GameObject laser = objPool.Get(objTag);
            laser.transform.position = firePoint[i].position;
            laser.transform.rotation = Quaternion.LookRotation(dir);

            laser.GetComponent<Laser>().Shoot(maxLaserDist, damage, tagCheck);
        }
    }
}
