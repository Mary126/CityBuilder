using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameCell
{
    public string type = null;
    public bool isBuildable = false;
    public bool isOccupied = false;
    public string color;
}
[System.Serializable]
public class GameField 
{ 
    public GameCell[,] field = new GameCell[10, 10];
    public GameField()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                field[i, j] = new GameCell();
            }
        }
    }
}


public class ModelInstances : MonoBehaviour
{
    public int swampCellCount = 5;
    public int waterCellCount = 5;
    public int buildingCount = 10;
    public GameController gameController;
    public FieldGenerator fieldGenerator;
    public GameObject cellPrefab;
    public GameObject buildingPrefab;
    public bool isPlacingBuilgigs = false;
    public bool isCooking = false;
}
