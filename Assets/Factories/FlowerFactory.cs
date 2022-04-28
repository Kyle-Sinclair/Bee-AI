using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlowerFactory : ScriptableObject
{
    [SerializeField]
    Flower[] prefabs;

    List<Flower>[] pools;


    void CreatePools() {
        pools = new List<Flower>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<Flower>();
        }
    }
    //standard worker bee
    public Flower Get(int flowerType) {
        return Instantiate(prefabs[flowerType]);
    }
}