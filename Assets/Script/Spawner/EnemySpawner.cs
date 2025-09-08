using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPooling enemyPool;

    [SerializeField] private List<string> enemyTag;

    [Header("Point")]
    [SerializeField] private Transform pointUpLeft;
    [SerializeField] private Transform pointDownLeft;
    [SerializeField] private Transform pointUpRight;
    [SerializeField] private Transform pointDownRight;

    public void Spawn(int countSpawn)
    {
        for (int i = 0; i < countSpawn; i++)
        {
            int indexEnemy = GetRamdomNum();
            Vector3 pos = GetRamdomPos();

            GameObject enemy = enemyPool.Get(enemyTag[indexEnemy]);
            enemy.transform.position = pos;
        }
    }

    private int GetRamdomNum()
    {
        int num = Random.Range(0, enemyTag.Count);
        return num;
    }
    private Vector3 GetRamdomPos()
    {
        Vector3 posLeft = Vector3.Lerp(pointDownLeft.position, pointUpLeft.position, Random.value);
        Vector3 posRight = Vector3.Lerp(pointDownRight.position, pointUpRight.position, Random.value);
        Vector3 posUp = Vector3.Lerp(pointUpLeft.position, pointUpRight.position, Random.value);
        Vector3 posDown = Vector3.Lerp(pointDownLeft.position, pointDownRight.position, Random.value);
        int num = Random.Range(0, 4);

        switch(num)
        {
            case 0: return posLeft;
            case 1: return posRight;
            case 2: return posUp;
            case 3: return posDown;
        }
        return Vector3.zero;
    }
    
}
