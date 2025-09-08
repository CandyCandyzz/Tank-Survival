using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private CharacterStat characterStat;
    [SerializeField] private string targetTag;

    [SerializeField] private Collider hitboxCollider;

    private void OnEnable()
    {
        OnCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStat targetStat = other.GetComponent<CharacterStat>();
        if (targetStat != null && other.CompareTag(targetTag))
        {
            targetStat.TakeDamage(characterStat.atk.GetValue());
        }
    }

    public void OnCollider()
    {
        hitboxCollider.enabled = true;
    }

    public void OffCollider()
    {
        hitboxCollider.enabled = false;
    }    
}
