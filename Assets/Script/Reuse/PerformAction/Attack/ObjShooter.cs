using UnityEngine;

public class ObjShooter
{
    private ObjectPooling objPool;
    private int count;
    private Transform firePoint;
    private float shotAngle;
    private float objSpeed;
    private float damage;
    private string objTag;

    public ObjShooter(ObjectPooling objPool, int count, Transform firePoint, float shotAngle, float objSpeed, float damage, string objTag)
    {
        this.objPool = objPool;
        this.count = count;
        this.firePoint = firePoint;
        this.shotAngle = shotAngle;
        this.objSpeed = objSpeed;
        this.damage = damage;
        this.objTag = objTag;
    }

    public void Shot(Vector3 target, string tagTargetCheck, string tagNotCheck)
    {
        float offset = shotAngle/(count - 1);
        float startAngle = -shotAngle / 2;
        Vector3 dir = (target - firePoint.position).normalized;

        if(count == 0) { return; }
        if(count == 1)
        {
            GameObject obj = objPool.Get(objTag);
            firePoint.rotation = Quaternion.LookRotation(dir);

            obj.GetComponent<Bullet>().Fire(firePoint, damage, objSpeed, tagTargetCheck, tagNotCheck);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + i * offset;
            Quaternion rotationAngle = Quaternion.Euler(0, angle, 0);
            Vector3 objDir = rotationAngle * dir;
            
            GameObject obj = objPool.Get(objTag);
            firePoint.rotation = Quaternion.LookRotation(objDir);

            obj.GetComponent<Bullet>().Fire(firePoint, damage, objSpeed, tagTargetCheck, tagNotCheck);
        }
    }
}
