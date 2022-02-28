using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    void GenerateCell(int x, int y, string type, bool isBuildable, bool isOccupied, Color color)
    {
        GameObject cell = Instantiate(modelInstances.cellPrefab);
        cell.GetComponent<Renderer>().material.color = color;
        cell.transform.position = new Vector3(x, 0, y);
        cell.transform.parent = modelInstances.field.transform;
        cell.GetComponent<CellInstances>().type = type;
        cell.GetComponent<CellInstances>().isBuildable = isBuildable;
        cell.GetComponent<CellInstances>().isOccupied = isOccupied;
        viewInstances.cells[x, y] = cell.GetComponent<CellInstances>();
    }
    void GenerateCells(string type, bool isBuildable, bool isOccupied, Color color, int count)
    {
        for (int i = 0; i < count; i++)
        {
            bool isPlaced = false;
            while (isPlaced == false)
            {
                int x = Random.Range(0, 10);
                int y = Random.Range(0, 10);
                if (viewInstances.cells[x, y] == null)
                {
                    GenerateCell(x, y, type, isBuildable, isOccupied, color);
                    isPlaced = true;
                }
            }
        }
    }
    void GenerateBuildings()
    {
        for (int i = 0; i < modelInstances.buildingCount; i++)
        {
            bool isPlaced = false;
            while (isPlaced == false)
            {
                int x = Random.Range(0, 10);
                int y = Random.Range(0, 10);
                if (viewInstances.cells[x, y].isBuildable == true && viewInstances.cells[x, y].building == null)
                {
                    GameObject building = Instantiate(modelInstances.buildingPrefab);
                    building.transform.position = new Vector3(x, 0.5f, y);
                    viewInstances.cells[x,y].building = building;
                    isPlaced = true;
                }
            }
        }
    }
    void GenerateField()
    {
        GenerateCells("water", false, false, Color.blue, modelInstances.waterCellCount);
        GenerateCells("swamp", false, false, Color.grey, modelInstances.swampCellCount);
        int grassCount = Random.Range(10, 79);
        int sandCount = 90 - grassCount;
        GenerateCells("grass", true, false, Color.green, grassCount);
        GenerateCells("sand", true, false, Color.yellow, sandCount);
    }
    private void Start()
    {
        GenerateField();
        GenerateBuildings();
    }
}
