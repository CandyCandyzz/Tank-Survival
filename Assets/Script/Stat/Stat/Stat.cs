using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    private List<StatModifier> modifiers = new();

    public float GetValue()
    {
        return baseValue;
    }
    public void SetValue(float value)
    {
        baseValue = value;
    }

    public void AddModifier(StatModifier statModifier)
    {
        modifiers.Add(statModifier);
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        modifiers.Remove(statModifier);
    }
}
