using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveNotifyUI : MonoBehaviour
{
    private TextMeshProUGUI textNotify;
    [SerializeField] private Image imageBG;

    [SerializeField] private float timeAppear;
    [SerializeField] private float timeWait;
    [SerializeField] private float timeDisappear;

    private Color originBGColor;
    private Color originTextColor;

    private void Start()
    {
        textNotify = GetComponentInChildren<TextMeshProUGUI>();
        originBGColor = imageBG.color;
        originTextColor = textNotify.color;
    }

    public void Notify(int numWave)
    {
        textNotify.text = "Wave " + numWave.ToString();
        StartCoroutine(ShowAndHide());
    }

    private IEnumerator ShowAndHide()
    {
        StartCoroutine(Appear());
        yield return new WaitForSeconds(timeAppear + timeWait);
        StartCoroutine(Disappear());
    }

    private IEnumerator Appear()
    {
        float time = 0;
        Color startBGColor = originBGColor;
        startBGColor.a = 0;
        Color startTextColor = originTextColor;
        startTextColor.a = 0;

        while (time < timeAppear)
        {
            time += Time.deltaTime;
            float t = time/timeAppear;

            imageBG.color = Color.Lerp(startBGColor, originBGColor, t);
            textNotify.color = Color.Lerp(startTextColor, originTextColor, t);
            yield return null;
        }
    }

    private IEnumerator Disappear()
    {
        float time = 0;

        Color targetBGColor = originBGColor;
        targetBGColor.a = 0;
        Color targetTextColor = originTextColor;
        targetTextColor.a = 0;

        while (time < timeDisappear)
        {
            time += Time.deltaTime;
            float t = time / timeDisappear;

            imageBG.color = Color.Lerp(originBGColor, targetBGColor, t);
            textNotify.color = Color.Lerp(originTextColor, targetTextColor, t);
            yield return null;
        }
    }
}
