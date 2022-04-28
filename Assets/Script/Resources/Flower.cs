using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    int pollenStore;
    float replenishmentTime;
    float depletionTime = 0;
    bool depleted = false;
    // Start is called before the first frame update
    void Start()
    {
        pollenStore = Random.Range(5, 25);
        replenishmentTime = Random.Range(5,25);
    }
    public void Update() {
        if(depleted == true) {
            depletionTime += Time.deltaTime;
            if(depletionTime >= replenishmentTime) {
                Replenished();
            }

        }
    }

    public int TakePollen() {
        int pollenLoad = Random.Range(1, 6);
        pollenLoad = Mathf.Min(pollenLoad, pollenStore);
        pollenStore -= pollenLoad;
        if(pollenStore == 0 && depleted == false) {
            Depleted();
        }
        return pollenLoad;
    }

    public void Depleted() {
        depleted = true;
        this.gameObject.layer = LayerMask.NameToLayer("DepletedPollen");
        this.transform.localScale *= 0.5f;
    }
    public void Replenished() {
        depleted = false;
        this.gameObject.layer = LayerMask.NameToLayer("Pollen");
        this.transform.localScale *= 2f;
        pollenStore = Random.Range(5, 25);
        depletionTime = 0;
        depleted = false;
    }
}
