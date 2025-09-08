using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave")]
    private WaveManager waveManager;

    [Header("DMGPopUp")]
    private ObjectPooling dmgPopUpPool;
    [SerializeField] private string tagDMGPopUP;

    [Header("Score")]
    [SerializeField] private CharacterStat stat;
    private ScoreManager scoreManager;

    private void Start()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        dmgPopUpPool = GameObject.Find("DMGPopUpPool").GetComponent<ObjectPooling>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    public void ChecknSpawn()
    {
        waveManager.CheckAllDied();
    }

    public void DMGPopUP(float dmg)
    {
        DamagePopUp dmgPopUp = dmgPopUpPool.Get(tagDMGPopUP).GetComponent<DamagePopUp>();
        dmgPopUp.SetInfo(transform.position, dmg);
    }

    public void IncreaseScore()
    {
        scoreManager.AddScore(stat.enemySO.score);
    }
}
