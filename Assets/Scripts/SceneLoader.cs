using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;
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
                    savedGame.field[i, j].isBuildable, savedGame.field[i, j].isOccupied, modelInstances.gameController.GetColor(savedGame.field[i, j].color));
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
}
