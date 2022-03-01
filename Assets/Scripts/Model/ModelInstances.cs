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
public class Building
{
    public int size = 0;
    public int x;
    public int y;

    public Building(int x, int y, int size)
    {
        this.x = x;
        this.y = y;
        this.size = size;
    }
}
[System.Serializable]
public class GameField 
{ 
    public GameCell[,] field = new GameCell[100, 100];
    public List<Building> buildings = new List<Building>();
    public GameField()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                field[i, j] = new GameCell();
            }
        }
    }
}


public class ModelInstances : MonoBehaviour
{
    public int swampCellCount = 500;
    public int waterCellCount = 500;
    public int buildingsCellCount = 100;
    public GameController gameController;
    public FieldGenerator fieldGenerator;
    public SceneLoader sceneLoader;
    public UIController uIController;
    public GameObject cellPrefab;
    public GameObject buildingPrefab;
    public bool isPlacingBuilding = false;
    public int placingBuildingSize = 0;
    public bool isCooking = false;
    public bool isShowingBuildingWindow = false;
    public Vector3 camMoveDirection;
}
