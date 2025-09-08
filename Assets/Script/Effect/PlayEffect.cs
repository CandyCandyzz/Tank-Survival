using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public ObjectPooling effectPool;
    public List<string> objTag;
}

public class PlayEffect : MonoBehaviour
{
    [SerializeField] private List<Effect> effects;
   
    public void GetnPlay(Transform pos, int poolIndex, int tagIndex)
    {
        ObjectPooling effectPool = effects[poolIndex].effectPool;
        string objTag = effects[poolIndex].objTag[tagIndex];

        EffectController controller = effectPool.Get(objTag).GetComponent<EffectController>();
        controller.PlayEffect(pos);
    }   
}
