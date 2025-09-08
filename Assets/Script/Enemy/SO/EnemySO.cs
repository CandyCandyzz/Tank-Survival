using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    //Stat
    public float maxHP;
    public float atk;
    public float atkSpeed;

    //Score
    public int score;
}
