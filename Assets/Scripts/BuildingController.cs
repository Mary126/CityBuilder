using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public ModelInstances modelInstances;
    private void OnMouseDown()
    {
        modelInstances.uIController.ShowBuildingInfoWindow((int)(transform.localScale.x + 0.2), (int)transform.position.x, (int)transform.position.z);
    }
}
