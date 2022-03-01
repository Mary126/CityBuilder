using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    public void MoveCamera()
    {

    }
    Color GetColor(string color)
    {
        int a = color.IndexOf('(') + 1;
        int b = color.Length - 1 - a;

        string[] values = color.Substring(a, b).Split(',');

        var culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        return new Color(
            float.Parse(values[0], culture),
            float.Parse(values[1], culture),
            float.Parse(values[2], culture),
            float.Parse(values[3], culture));
    }
    public void LoadScene()
    {
        string filepath = "save.dat";
        GameField savedGame;
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            savedGame = (GameField)loadedData;
        }
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (viewInstances.cells[i, j].building != null) Destroy(viewInstances.cells[i, j].building);
                Destroy(viewInstances.cells[i, j].gameObject);
                modelInstances.fieldGenerator.GenerateCell(i, j, savedGame.field[i, j].type, 
                    savedGame.field[i, j].isBuildable, savedGame.field[i, j].isOccupied, GetColor(savedGame.field[i,j].color));
                if (viewInstances.cells[i, j].isOccupied == true)
                {
                    modelInstances.fieldGenerator.GenerateBuilding(i, j, (int)viewInstances.cells[i, j].transform.localScale.x);
                }
            }
        }
        Debug.Log(savedGame.field[0, 0].color);
    }
    public void SaveScene()
    {
        string filepath = "save.dat";
        GameField saveGame = new GameField();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                saveGame.field[i, j].type = viewInstances.cells[i, j].type;
                saveGame.field[i, j].isBuildable = viewInstances.cells[i, j].isBuildable;
                saveGame.field[i, j].isOccupied = viewInstances.cells[i, j].isOccupied;
                saveGame.field[i, j].color = viewInstances.cells[i, j].color;
            }
        }
        using (FileStream file = File.Create(filepath))
        {
            new BinaryFormatter().Serialize(file, saveGame);
        }
    }
    public void BuildBuilding(int x, int y, int size)
    {
        if (viewInstances.cells[x, y].isBuildable == true && viewInstances.cells[x, y].isOccupied == false)
        {
            modelInstances.fieldGenerator.GenerateBuilding(x, y, size);
        }
    }
    public void ChangeCellType(string type, int x, int y)
    {
        if (type == "water")
        {
            viewInstances.cells[x, y].type = "swamp";
            viewInstances.cells[x, y].gameObject.GetComponent<Renderer>().material.color = Color.grey;
        }
        if (type == "swamp")
        {
            viewInstances.cells[x, y].type = "sand";
            viewInstances.cells[x, y].isBuildable = true;
            viewInstances.cells[x, y].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            viewInstances.cam.transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            viewInstances.cam.transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            viewInstances.cam.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            viewInstances.cam.transform.Translate(Vector3.back * 5f * Time.deltaTime);
        }
    }
}
