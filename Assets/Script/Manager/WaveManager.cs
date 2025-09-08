using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    private int currentWave;

    [SerializeField] private int countIncrease;
    private int curCount;
    private int enemyAlive;

    [SerializeField] private UnityEvent<int> onNextWave;

    private void Start()
    {
        NextWave();
    }

    public void CheckAllDied()
    {
        enemyAlive--;
        if(enemyAlive <= 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        curCount += countIncrease;
        enemyAlive = curCount;
        enemySpawner.Spawn(curCount);

        currentWave++;
        onNextWave.Invoke(currentWave);
    }
}
