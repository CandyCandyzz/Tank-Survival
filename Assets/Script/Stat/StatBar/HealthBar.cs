using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private CharacterStat characterStat;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider subHPSlider;
    [SerializeField] private float speedLerp;

    private void Start()
    {
        hpSlider.maxValue = characterStat.maxHP.GetValue();
        subHPSlider.maxValue = characterStat.maxHP.GetValue();

        hpSlider.value = characterStat.currentHP;
    }

    private void Update()
    {
        hpSlider.value = characterStat.currentHP;

        if(subHPSlider.value > hpSlider.value)
        {
            subHPSlider.value = Mathf.Lerp(subHPSlider.value, hpSlider.value, speedLerp * Time.deltaTime);
        }
        else
        {
            subHPSlider.value = hpSlider.value;
        }
    }
}
