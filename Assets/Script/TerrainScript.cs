using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour{

    public FlowerFactory flowerFactory;

    List<Flower> flowers;
    private int flowerCount;
    public int maxFlower = 100;
    public static int scale = 5;
    // Start is called before the first frame update
    void Awake() {
        this.transform.localScale *= 5;
    }
    void Start(){
        flowers = new List<Flower>(maxFlower);

        ScatterFlowers();
    }

    void OnEnable() {

    }


    public void ScatterFlowers() {
        Vector3 randomVector; 
        while (flowers.Count < maxFlower) {
            Flower instance = flowerFactory.Get(0);
            Transform t = instance.transform;
            randomVector = new Vector3(Random.Range(-5f* scale, 5f * scale), 0, Random.Range(-5f * scale, 5f * scale));
            t.localPosition = randomVector;
            flowers.Add(instance);
        }
    }
}
