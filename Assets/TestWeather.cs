using UnityEngine;



public class TestWeather : MonoBehaviour
{
    public GameObject rainFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rainFX.SetActive(true);
        }
    }
}
