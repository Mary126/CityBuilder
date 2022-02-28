using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    private void OnMouseDown()
    {
        if (modelInstances.isPlacingBuilgigs == true)
        {
            modelInstances.gameController.BuildBuilding((int)transform.position.x, (int)transform.position.z);
        }
        if (modelInstances.isCooking == true)
        {
            modelInstances.gameController.ChangeCellType(GetComponent<CellInstances>().type, (int)transform.position.x, (int)transform.position.z);
        }
    }
}
