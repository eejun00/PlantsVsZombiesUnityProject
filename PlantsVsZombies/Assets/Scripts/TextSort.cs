using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSort : MonoBehaviour
{
    public string sortingLayerName = default;
    public int sortingOrder = 15;

    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
