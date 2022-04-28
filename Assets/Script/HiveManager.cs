using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveManager : MonoBehaviour
{
    public float foodStocks = 100;
    private float foodStorage = 100;
    public PatrolRoute patrolRoute;
    public float foodRatio;
    public int initialBeeCount = 50;
    List<Bee> bees;
    public BeeFactory beeFactory;
    public static Vector3 hiveLocation;
    private int terrainScaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        this.terrainScaleFactor = TerrainScript.scale; 
        hiveLocation = this.transform.localPosition;
        bees = new List<Bee>();
        while (bees.Count < initialBeeCount) {
            CreateBee();
        }
       
    }

    // Update is called once per frame


        //Really actually all bees are already spawned,
        //they disable their rendering when inside hive
        //they need access to foodStock, hive temp(only when inside)
        //
    void Update(){
        
        foodStocks -= Time.deltaTime * 2f;
        Mathf.Max(foodStocks, 0);
        foodRatio = foodStocks / foodStorage * 100f;
     }

    private void CreateBee() {
        Bee instance = beeFactory.Get(0);
        instance.hiveLocation = this.transform.localPosition;
        Transform t = instance.transform;
        t.localPosition = this.transform.localPosition;
        instance.terrainScaleFactor = this.terrainScaleFactor;
        instance.homeHive = this;
        bees.Add(instance);
    }


    public void DepositPollen(int pollenCount) {
        foodStocks += pollenCount;
        foodStocks = Mathf.Min(foodStorage, foodStocks);
    }
}
