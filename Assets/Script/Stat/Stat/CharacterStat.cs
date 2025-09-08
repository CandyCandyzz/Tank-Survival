using UnityEngine;
using UnityEngine.Events;

public class CharacterStat : MonoBehaviour
{
    public EnemySO enemySO;

    public float currentHP;
    public Stat maxHP;
    public Stat atk;
    public Stat atkSpeed;

    private bool isDie = false;
    public UnityEvent onDie;

    [Header("TakeDMG")]
    public UnityEvent<float> onTakeDMG;
    public float takeDMGCooldown;
    private float nextTakeDMGTime = 0;

    private void Awake()
    {
        if (enemySO != null)
        {
            maxHP.SetValue(enemySO.maxHP);
            atk.SetValue(enemySO.atk);
            atkSpeed.SetValue(enemySO.atkSpeed);
        }
        currentHP = maxHP.GetValue();
    }

    public void TakeDamage(float damage)
    {
        if(isDie) { return; }

        if(Time.time < nextTakeDMGTime) { return; }
        nextTakeDMGTime = Time.time + takeDMGCooldown; 

        currentHP -= damage;
        onTakeDMG.Invoke(damage);

        if(currentHP <= 0)
        {
            isDie = true;
            onDie.Invoke();
        }
    }
}
