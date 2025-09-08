using UnityEngine;

public class BasicFly
{
    private Transform target;
    private Rigidbody rb;
    private float flySpeed;
    private float maxHeight;

    public BasicFly(Transform target, Rigidbody rb, float flySpeed, float maxHeight)
    {
        this.target = target;
        this.rb = rb;
        this.flySpeed = flySpeed;
        this.maxHeight = maxHeight;
    }

    public void PerformFly()
    {
        Vector3 targetPos = new Vector3(target.position.x, rb.position.y, target.position.z);
        Vector3 dir = (targetPos - rb.position).normalized;

        Vector3 newPos = rb.position + dir * flySpeed * Time.fixedDeltaTime;
        float targetY = DefineGround();
        newPos.y = targetY;

        rb.MovePosition(newPos);
        rb.rotation = Quaternion.LookRotation(dir);
    }

    public float DefineGround()
    {
        bool isGround = Physics.Raycast(rb.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 20f);
        if (isGround)
        {
            return hitInfo.point.y + maxHeight;
        }
        return rb.position.y;
    }
}
