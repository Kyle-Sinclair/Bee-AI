using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Renderer mr;

    public int terrainScaleFactor;
    private int temperature;
   //add a time to spend foraging count
   //vary it's decay by the map's temperature rating
   //make temperature accessible t hrough a static facade

    AgentState state = AgentState.GUARD;
    private float forageTriggerPoint;
    public SmellSensor smellSensor;
    Flower foundFlower;

    Vector3 patrolTarget;
    int patrolNode = 0;
    public HiveManager homeHive;
    public Vector3 hiveLocation;
    public int collectedPollen;

    Vector3 position;
    Vector3 velocity;
    Vector3 desiredDirection;

    public float maxSpeed;
    public float steerStrength = 3;
    public float wanderStrength = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<Renderer>();

        position = transform.localPosition;
        FindObjectOfType<Weather>().weatherChangeEvent += UpdateWeatherInformation;
        patrolTarget = homeHive.patrolRoute.patrolPoints[patrolNode].transform.position;

        forageTriggerPoint = Random.Range(30f, 90f);
        smellSensor.smellRadius = 2;
    }

    public void Update() {

        UpdateSmellSensor();

        switch (state) {
            case AgentState.FORAGE:
                Forage();
                break;
            case AgentState.SEEK:
                Seek();
                break;
            case AgentState.DANCING:
                Dance();
                break;
            case AgentState.GUARD:
                Guard();
                break;
            case AgentState.VENTILATE:
                Ventilate();
                break;
            case AgentState.RETURNINGHOME:
                ReturnHome();
                break;
        }
  
    }

    public void Forage() {
        mr.material.color = Color.blue;
        desiredDirection = (desiredDirection  + (Random.insideUnitSphere * wanderStrength)).normalized;

        Vector3 desiredVelocity = desiredDirection * maxSpeed;
        Vector3 desiredSteeringForce = (desiredVelocity - velocity) ;
        Vector3 acceleration = Vector3.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector3.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;
        position = ClampPosition(position);


        transform.position = position;
    }
    public void ReturnHome() {
       
        mr.material.color = Color.white;
        desiredDirection = (desiredDirection + (hiveLocation -  position) + (Random.insideUnitSphere * wanderStrength / 3)).normalized;

        Vector3 desiredVelocity = desiredDirection * maxSpeed;
        Vector3 desiredSteeringForce = (desiredVelocity - velocity) * 5;
        Vector3 acceleration = Vector3.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector3.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;
        position = ClampPosition(position);


        transform.position = position;
        if (Vector3.Distance(hiveLocation, position) < 0.5) {
            DepositPollen();
            if (homeHive.foodRatio > forageTriggerPoint) {
                //Debug.Log("Bee starting to guard");

                state = AgentState.GUARD;
            }
        }
    }
    public void Seek() {
        
        mr.material.color = Color.red;

        desiredDirection =  ((foundFlower.transform.position - position) + (Random.insideUnitSphere * wanderStrength)).normalized;

        Vector3 desiredVelocity = desiredDirection * maxSpeed;
        Vector3 desiredSteeringForce = (desiredVelocity - velocity) * 5;
        Vector3 acceleration = Vector3.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector3.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;
        position = ClampPosition(position);

        transform.position = position;
        if (Vector3.Distance(foundFlower.transform.position, position) < 0.2) {
            TakePollen();
        }

    }

    public void Dance() {

    }

    public void Guard() {
        //if (dangerPresent)
        //    Attack();
        //if(impressed == true) {
        //    state = AgentState.FOLLOW;
        //}
        //if(homeHive.temperature > triggerTemp) {
        //    Ventilate();
        //}
        if(homeHive.foodRatio < forageTriggerPoint) {
            state = AgentState.FORAGE;
        }
        desiredDirection = ((patrolTarget - position) + (Random.insideUnitSphere * wanderStrength)).normalized;

        Vector3 desiredVelocity = desiredDirection * maxSpeed;
        Vector3 desiredSteeringForce = (desiredVelocity - velocity) * 5;
        Vector3 acceleration = Vector3.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector3.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;
        position = ClampPosition(position);

        transform.position = position;
        if (Vector3.Distance(patrolTarget, position) < 0.1) {
            if (patrolNode == 3) {
                patrolNode = 0;
            }
            else {
                patrolNode++;
            }
            patrolTarget = homeHive.patrolRoute.patrolPoints[patrolNode].transform.position;
        }
    }

    public void Ventilate() {

    }

    public void UpdateSmellSensor() {
        if (state == AgentState.FORAGE) {
            foundFlower = smellSensor.UpdateSensor(foundFlower);
            if (foundFlower != null) {
                state = AgentState.SEEK;
            }
        }
     
        
    }


    private void DepositPollen() {
        homeHive.DepositPollen(collectedPollen);
        collectedPollen = 0;
        state = AgentState.FORAGE;
    }

    private void TakePollen() {
        //Debug.Log("pollen collected");
        collectedPollen = foundFlower.TakePollen();
        if (collectedPollen > 0) {
            state = AgentState.RETURNINGHOME;
        }
        else if(collectedPollen == 0) {
            state = AgentState.FORAGE;
            foundFlower = null;
        }
    }
    private Vector3 ClampPosition(Vector3 position) {
        position.y = Mathf.Clamp(position.y, 0.1f, 2.0f);
        position.z = Mathf.Clamp(position.z, -5f * terrainScaleFactor, 5f * (float)terrainScaleFactor);
        position.x = Mathf.Clamp(position.x, -5f * terrainScaleFactor, 5f * (float)terrainScaleFactor);
        return position;
    }

    private void UpdateWeatherInformation(Weather weather) {
        //temperature = newTemp;
        Debug.Log("Event Triggered");
    }
    void OnDrawGizmosSelected() {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = desiredDirection * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
