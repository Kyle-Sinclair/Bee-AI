using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Sensor : MonoBehaviour
{
    public float detectionRate = 1.0f;
    float elapsedTime = 0.0f;
    protected virtual void Initialize() { }
    public virtual bool UpdateSensor() { return false; }
    // Use this for initialization 
    //void Start () {    elapsedTime = 0.0f;    Initialize();  } 
    // Update is called once per frame

}