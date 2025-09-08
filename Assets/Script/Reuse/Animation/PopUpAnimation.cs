using UnityEngine;

public class PopUpAnimation : MonoBehaviour
{
    private ObjectPooling objPool;
    [SerializeField] private string objTag;

    public AnimationCurve scaleCurve;
    private float time = 0;

    private void Start()
    {
        objPool = transform.parent.GetComponent<ObjectPooling>();
    }

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time <= scaleCurve.keys[scaleCurve.length - 1].time)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        }
        else
        {
            objPool.Return(objTag, gameObject);
        }    
    }
}
