using UnityEngine;

public class OnOffObj : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void TurnOn()
    {
        obj.SetActive(true);
    }
    public void TurnOff()
    {
        obj.SetActive(false);
    }
}
