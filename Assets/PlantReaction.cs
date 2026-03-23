using UnityEngine;

public class PlantReaction : MonoBehaviour
{
    public void OnRain()
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void OnSun()
    {
        transform.localScale = Vector3.one;
    }
}