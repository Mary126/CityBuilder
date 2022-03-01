using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    public void GenerateCell(int x, int y, string type, bool isBuildable, bool isOccupied, Color color)
    {
        GameObject cell = Instantiate(modelInstances.cellPrefab);
        cell.GetComponent<Renderer>().material.color = color;
        cell.transform.position = new Vector3(x, 0, y);
        cell.transform.parent = viewInstances.field.transform;
        cell.GetComponent<CellInstances>().type = type;
        cell.GetComponent<CellInstances>().isBuildable = isBuildable;
        cell.GetComponent<CellInstances>().isOccupied = isOccupied;
        cell.GetComponent<CellInstances>().color = color.ToString();
        cell.GetComponent<CellController>().modelInstances = modelInstances;
        cell.GetComponent<CellController>().viewInstances = viewInstances;
        viewInstances.cells[x, y] = cell.GetComponent<CellInstances>();
    }
    void GenerateCells(string type, bool isBuildable, bool isOccupied, Color color, int count)
    {
        for (int i = 0; i < count; i++)
        {
            bool isPlaced = false;
            while (isPlaced == false)
            {
                int x = Random.Range(0, 100);
                int y = Random.Range(0, 100);
                if (viewInstances.cells[x, y] == null)
                {
                    GenerateCell(x, y, type, isBuildable, isOccupied, color);
                    isPlaced = true;
                }
            }
        }
    }
    public void GenerateBuilding(int x, int y, int size)
    {
        if (x - (size - 1) >= 0 && y - (size - 1) >= 0 && x + (size - 1) < 100 && y + (size - 1) < 100)
        {
            GameObject building = Instantiate(modelInstances.buildingPrefab);
            building.transform.position = new Vector3(x, 0.5f, y);
            building.transform.parent = viewInstances.field.transform;
            building.transform.localScale = new Vector3(size - 0.2f, building.transform.localScale.y, size - 0.2f);
            building.GetComponent<BuildingController>().modelInstances = modelInstances;
            viewInstances.cells[x, y].building = building;
            for (int stepX = x - (size - 1); stepX <= x + (size - 1); stepX++)
            {
                for (int stepY = y - (size - 1); stepY <= y + (size - 1); stepY++)
                {
                    viewInstances.cells[stepX, stepY].isOccupied = true;
                }
            }
        }
    }
    public bool CanPlaceBuilding(int x, int y, int size)
    {
        int places = 0;
        int squareArea = -1;
        if (x - (size - 1) >= 0 && y - (size - 1) >= 0 && x + (size - 1) < 100 && y + (size - 1) < 100)
        {
            squareArea = 0;
            for (int stepX = x - (size - 1); stepX <= x + (size - 1); stepX++)
            {
                for (int stepY = y - (size - 1); stepY <= y + (size - 1); stepY++)
                {
                    if (viewInstances.cells[stepX, stepY].isBuildable == true && viewInstances.cells[stepX, stepY].isOccupied == false)
                    {
                        places++;
                    }
                    else
                    {
                        places--;
                    }
                    squareArea++;
                }
            }
        }
        if (places == squareArea)
        {
            return true;
        }
        return false;
    }
    void GenerateBuildingsType(int size, int count)
    {
        for (int i = 0; i < count; i++)
        {
            bool isPlaced = false;
            int iteratorCheck = 0;
            while (isPlaced == false)
            {
                int x = Random.Range(0, 100);
                int y = Random.Range(0, 100);
                if (CanPlaceBuilding(x, y, size))
                {
                    GenerateBuilding(x, y, size);
                    isPlaced = true;
                }
                iteratorCheck++;
                if (iteratorCheck == 10000)
                {
                    Debug.Log("Can't place building");
                    isPlaced = true;
                }
            }
        }
    }
    void GenerateBuildings()
    {
        int i = 0;
        int smallBuildingsCount = 0;
        int middleBuildingsCount = 0;
        int largeBuildingsCount = 0;
        //рассчитать примерное количество каждого типа здания
        while (i < modelInstances.buildingsCellCount)
        {
            int size = Random.Range(1, 4);
            switch (size)
            {
                case 1: smallBuildingsCount++; break;
                case 2: middleBuildingsCount++; break;
                case 3: largeBuildingsCount++; break;
                default: break;
            }
            i += size;
        }
        GenerateBuildingsType(1, smallBuildingsCount);
        GenerateBuildingsType(2, middleBuildingsCount);
        GenerateBuildingsType(3, largeBuildingsCount);
    }
    void GenerateField()
    {
        GenerateCells("water", false, false, Color.blue, modelInstances.waterCellCount);
        GenerateCells("swamp", false, false, Color.grey, modelInstances.swampCellCount);
        int grassCount = Random.Range(1000, 9000);
        int sandCount = 9000 - grassCount;
        GenerateCells("grass", true, false, Color.green, grassCount);
        GenerateCells("sand", true, false, Color.yellow, sandCount);
    }
    private void Start()
    {
        GenerateField();
        GenerateBuildings();
    }
}
