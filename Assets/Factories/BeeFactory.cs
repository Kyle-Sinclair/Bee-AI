using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BeeFactory : ScriptableObject
{
    [SerializeField]
    Bee[] prefabs;

    List<Bee>[] pools;


    void CreatePools() {
        pools = new List<Bee>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<Bee>();
        }
    }
    //standard worker bee
    public Bee Get(int BeeType) {
        return Instantiate(prefabs[BeeType]);
    }
}
