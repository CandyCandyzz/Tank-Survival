using System.Collections;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;

    private void OnEnable()
    {
        StartCoroutine(ResetTrail());
    }

    private IEnumerator ResetTrail()
    {
        trail.enabled = false;
        yield return new WaitForSeconds(0.03f);

        trail.Clear();              
        trail.enabled = true;       
    }

    private void OnDisable()
    {
        trail.Clear();
        trail.enabled = false;
    }
}
