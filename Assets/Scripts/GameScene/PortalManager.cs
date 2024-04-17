using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portal1;
    public GameObject portal2;
    public float minEnableTime = 5f;
    public float maxEnableTime = 10f;
    public float minDisableTime = 15f;
    public float maxDisableTime = 20f;

    private void Start()
    {
        DisablePortals();
        Invoke("EnablePortals", Random.Range(minEnableTime, maxEnableTime));
    }

    private void EnablePortals()
    {
        portal1.SetActive(true);
        portal2.SetActive(true);
        Invoke("DisablePortals", Random.Range(minDisableTime, maxDisableTime));
    }

    private void DisablePortals()
    {
        portal1.SetActive(false);
        portal2.SetActive(false);
        Invoke("EnablePortals", Random.Range(minEnableTime, maxEnableTime));
    }
}