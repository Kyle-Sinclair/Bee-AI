using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellSensor : MonoBehaviour
{
    int maxColliders = 10;
    Collider[] smeltFlowers;
    public GameObject flowerTarget;
    Color gizmoColor = Color.magenta;
    public int smellRadius = 2;
    private int layerMask;
    public bool targetAcquired = false;
    // Start is called before the first frame update
    void Start()
    {
        smeltFlowers = new Collider[maxColliders];
        layerMask = 1 << Layers.PollenLayer;
    }
    public  Flower  UpdateSensor(Flower foundFlower) {

        if(foundFlower != null) {
            //check if flower is still in smelling range
            if(Vector3.Distance(transform.position, foundFlower.transform.position) > smellRadius) {
                foundFlower = null;
            }
        }
        if (foundFlower == null) {
            int numSmeltFlowers = Physics.OverlapSphereNonAlloc(transform.position, smellRadius, smeltFlowers, layerMask);

            if (numSmeltFlowers > 0) {
                return smeltFlowers[Random.Range(0, numSmeltFlowers)].gameObject.GetComponent<Flower>();
            }
           
        }
        return foundFlower;

    }
    void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, smellRadius);
    }
}

//Maintain a refererce to aa smelled flower until distance is greater than radius
//If further away, start scanning again

