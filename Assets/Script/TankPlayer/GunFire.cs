using UnityEngine;
using UnityEngine.Events;

public class GunFire : MonoBehaviour
{
    [Header("ObjPool")]
    [SerializeField] private ObjectPooling bulletPool;
    [SerializeField] private string objTag;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private UnityEvent onFire;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private UnityEvent<float> onFireUI;

    [Header("Effect")]
    [SerializeField] private PlayEffect playEffect;

    private CharacterStat stat;

    private float time = 0;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        stat = GetComponent<CharacterStat>();
    }

    private void Update()
    {
        if (gameManager.state != GameState.Playing) { return; }
        Fire();
    }

    private void Fire()
    {
        time += Time.deltaTime;
        if (time < stat.atkSpeed.GetValue()) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = bulletPool.Get(objTag);
            Bullet sBullet = bullet.GetComponent<Bullet>();
            sBullet.Fire(firePoint, stat.atk.GetValue(), bulletSpeed, "Enemy", "Player");

            time = 0;

            //onFire.Invoke();
            playEffect.GetnPlay(firePoint, 0, 0);
            onFireUI.Invoke(stat.atkSpeed.GetValue());
        }
    }
}
