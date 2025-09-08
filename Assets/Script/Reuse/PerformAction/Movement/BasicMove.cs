using UnityEngine;

public class BasicMove
{
    private Transform target;
    private Rigidbody rb;
    private float moveSpeed;

    public BasicMove(Transform target, Rigidbody rb, float moveSpeed)
    {
        this.target = target;
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    //public void PerformMove(Vector3 target)
    //{
    //    //Vector3 targetPos = new Vector3(target.position.x, rb.position.y, target.position.z);
    //    Vector3 targetPos = new Vector3(target.x, rb.position.y, target.z);
    //    Vector3 dir = (targetPos - rb.position).normalized;
    //    rb.linearVelocity = dir * moveSpeed;
    //    rb.rotation = Quaternion.LookRotation(dir);
    //}

    public void PerformMove()
    {
        Vector3 targetPos = new Vector3(target.position.x, rb.position.y, target.position.z);
        Vector3 dir = (targetPos - rb.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
        rb.rotation = Quaternion.LookRotation(dir);
    }
}
