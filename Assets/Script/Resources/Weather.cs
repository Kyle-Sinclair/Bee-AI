using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weather : MonoBehaviour
{
    public delegate void weatherChangeDelegate(Weather weather);
    public event weatherChangeDelegate weatherChangeEvent;

    private float weatherDuration;
    private float elapsedTime;

    private int temperature;

    // Start is called before the first frame update
    void Start() {
        weatherDuration = Random.Range(25f, 50f);
    }

    // Update is called once per frame
    void Update() {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= weatherDuration) {
            elapsedTime = 0;
            weatherDuration = Random.Range(25f, 50f);
            ChangeWeather();
        }
    }
    

    private void ChangeWeather() {
        temperature = Random.Range(0, 45);
        weatherChangeEvent(this);
    }
}

