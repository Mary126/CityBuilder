using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public ModelInstances modelInstances;
    private void OnMouseDown()
    {
        Debug.Log(transform.localScale.x);
        float size = transform.localScale.x + 0.2f;
        Debug.Log(size);
        modelInstances.uIController.ShowBuildingInfoWindow((int)(size), (int)transform.position.x, (int)transform.position.z);
    }
}
