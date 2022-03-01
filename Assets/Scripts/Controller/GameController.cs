using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    public Color GetColor(string color)
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
    public void BuildBuilding(int x, int y, int size)
    {
        if (modelInstances.fieldGenerator.CanPlaceBuilding(x, y, size))
        {
            modelInstances.fieldGenerator.GenerateBuilding(x, y, size);
            modelInstances.isPlacingBuilding = false;
            modelInstances.placingBuildingSize = 0;
            viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            HidePlacesForBuildings();
        }
    }
    public void ChangeCellType(string type, int x, int y)
    {
        if (type == "water")
        {
            viewInstances.cells[x, y].type = "swamp";
            viewInstances.cells[x, y].gameObject.GetComponent<Renderer>().material.color = modelInstances.gameController.GetColor(" RGBA(0.140, 0.139, 0.038, 1.000)");
            viewInstances.cells[x, y].color = " RGBA(0.140, 0.139, 0.038, 1.000)";
            modelInstances.isCooking = false;
            viewInstances.cookButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }
        if (type == "swamp")
        {
            viewInstances.cells[x, y].type = "sand";
            viewInstances.cells[x, y].isBuildable = true;
            viewInstances.cells[x, y].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            viewInstances.cells[x, y].color = Color.yellow.ToString();
            modelInstances.isCooking = false;
            viewInstances.cookButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }
    }
    public void ShowPlacesForBuildings()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (viewInstances.cells[i, j].isOccupied == false && viewInstances.cells[i, j].isBuildable == true)
                {
                    viewInstances.cells[i, j].GetComponent<Renderer>().material.color = Color.magenta;
                }
            }
        }
    }
    public void HidePlacesForBuildings()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                viewInstances.cells[i, j].GetComponent<Renderer>().material.color = GetColor(viewInstances.cells[i, j].color);
            }
        }
    } 
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            viewInstances.cameraBody.transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            viewInstances.cameraBody.transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            viewInstances.cameraBody.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            viewInstances.cameraBody.transform.Translate(Vector3.back * 5f * Time.deltaTime);
        }
    }
}
