using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dmgText;

    private void Start()
    {
        dmgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetInfo(Vector3 pos, float dmg)
    {
        transform.position = pos;
        dmgText.text = dmg.ToString();
    }
}
