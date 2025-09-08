using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform tower;
    [SerializeField] private Transform canon;

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        Vector3 dirCanon = (MousePos() - canon.position);
        Vector3 dirTower = (MousePos() - tower.position);
        dirTower.y = 0;

        tower.rotation = Quaternion.LookRotation(dirTower.normalized);

        if(dirCanon.magnitude > 2.5f)
        {
            canon.rotation = Quaternion.LookRotation(dirCanon.normalized);
        }
    }
    private Vector3 MousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        return hitInfo.point;
    }
}
