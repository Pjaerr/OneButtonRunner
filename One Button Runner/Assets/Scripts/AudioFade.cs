using UnityEngine;
using System.Collections;

public class AudioFade : MonoBehaviour
{
    public AudioSource geoplexSolarRain;

    void Awake()
    {
        geoplexSolarRain.volume = 0f;
    }

    void Update()
    {
        for (int i = 0; i < 30; i++)
        {
            geoplexSolarRain.volume += 0.00001f;
        }
    }
}
