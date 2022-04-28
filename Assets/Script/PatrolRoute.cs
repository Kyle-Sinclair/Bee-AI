using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public GameObject[] patrolPoints;

    public int numPatrolPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        numPatrolPoints = patrolPoints.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0.5f, 0);
    }
}
