using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] private Image imageCD;
    [SerializeField] private TextMeshProUGUI textCD;

    private float cdTimer;
    private float cdTime;

    private void Start()
    {
        imageCD.fillAmount = 0;
    }

    private void Update()
    {
        CoolDown();
    }

    public void SetCoolDown(float cdTime)
    {
        textCD.gameObject.SetActive(true);
        imageCD.gameObject.SetActive(true);

        this.cdTime = cdTime;
        cdTimer = cdTime;
    }    

    private void CoolDown()
    {
        cdTimer -= Time.deltaTime;
        if(cdTimer <= 0)
        {
            textCD.gameObject.SetActive(false);
            imageCD.gameObject.SetActive(false);
        }
        else
        {
            textCD.text = Mathf.RoundToInt(cdTimer).ToString();
            imageCD.fillAmount = cdTimer/cdTime;
        }
    }
}
