using UnityEngine;

public class StatModifier
{
    public float value;
    public StatType statType;

    public void AddValue(float value, StatType type)
    {
        this.value = value;
        this.statType = type;
    }
}

public enum StatType
{
    Flat,
    Percent
}
