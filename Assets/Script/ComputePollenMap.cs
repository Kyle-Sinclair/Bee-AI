using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputePollenMap : MonoBehaviour
{

    public ComputeShader computePollenMap;
    public RenderTexture renderTexture;
    // Start is called before the first frame update
    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        computePollenMap.SetTexture(0, "Result", renderTexture);
        computePollenMap.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
