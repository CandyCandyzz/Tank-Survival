using UnityEngine;
using UnityEngine.Events;

public class EffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] effects;

    private ObjectPooling objPool;
    [SerializeField] private string objTag;

    [SerializeField] private UnityEvent onPlay;

    private void Start()
    {
        objPool = transform.parent.GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        if(EffectsDone())
        {
            gameObject.SetActive(false);
            objPool.Return(objTag, gameObject);
        }
    }

    public void PlayEffect(Transform pos)
    {
        transform.position = pos.position;
        transform.rotation = pos.rotation;
        foreach (var effect in effects)
        {
            effect.Play();
        }

        onPlay.Invoke();
    }

    public bool EffectsDone()
    {
        foreach (var effect in effects)
        {
            if (effect.IsAlive(true))
            {
                return false;
            }
        }
        return true;
    }
}
