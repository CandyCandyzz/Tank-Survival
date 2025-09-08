using UnityEngine;

public class JumpToTarget
{
    private Transform selfTransform;
    private Transform target;
    private float time = 0;

    private float timeDur;
    private float maxHeight;

    private Vector3 startPos;
    private Vector3 targetPos;

    public bool isEndJump = false;

    public JumpToTarget(Transform selfTransform, Transform target, float timeDur, float maxHeight)
    {
        this.selfTransform = selfTransform;
        this.target = target;
        this.timeDur = timeDur;
        this.maxHeight = maxHeight;
    }

    public void PreJump()
    {
        time = 0;
        startPos = selfTransform.position;
        targetPos = target.position;
    }

    public void PerformJump()
    {
        time += Time.deltaTime;
        if(time > timeDur)
        {
            isEndJump = true;
        }

        float t = time / timeDur;
        float newX = Mathf.Lerp(startPos.x, targetPos.x, t);
        float newZ = Mathf.Lerp(startPos.z, targetPos.z, t);
        float yOffset = -maxHeight * t * t + maxHeight * t;
        float newY = Mathf.Lerp(startPos.y, targetPos.y, t) + yOffset;

        Vector3 newPos = new Vector3(newX, newY, newZ);
        selfTransform.position = newPos;
    }

    public void EndJump()
    {
        time = 0;
        isEndJump = false;
        selfTransform.position = targetPos;
    }
}
