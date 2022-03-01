using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    void SaveScene()
    {
        modelInstances.sceneLoader.SaveScene();
    }
    void LoadScene()
    {
        modelInstances.sceneLoader.LoadScene();
    }
    void ShowBuildingWindow()
    {
        viewInstances.buildWindow.SetActive(true);
        modelInstances.isShowingBuildingWindow = true;
        viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        modelInstances.isCooking = false;
    }
    void PlaceBuildingButton(string tag)
    {
        switch (tag)
        {
            case "1x1": modelInstances.placingBuildingSize = 1; break;
            case "2x2": modelInstances.placingBuildingSize = 2; break;
            case "3x3": modelInstances.placingBuildingSize = 3; break;
            default: Debug.Log("Error wrong button"); break;
        }
        modelInstances.isPlacingBuilding = true;
        modelInstances.isShowingBuildingWindow = false;
        modelInstances.gameController.ShowPlacesForBuildings();
        viewInstances.buildWindow.SetActive(false);
    }
    void Cook()
    {
        if (!modelInstances.isShowingBuildingWindow)
        {
            modelInstances.isCooking = true;
            viewInstances.cookButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
    }
    public void ShowBuildingInfoWindow(int size, int x, int y)
    {
        viewInstances.deleteWindow.SetActive(true);
        viewInstances.sizeInfo.text = size + "x" + size;
        Debug.Log(y);
        viewInstances.xCoordinate.text = x.ToString();
        viewInstances.yCoordinate.text = y.ToString();
    }
    void DeleteBuilding()
    {
        int x = int.Parse(viewInstances.xCoordinate.text);
        int y = int.Parse(viewInstances.yCoordinate.text);
        float size = viewInstances.cells[x, y].building.transform.localScale.x + 0.2f;
        for (int stepX = x - (int)(size - 1); stepX <= x + (int)(size - 1); stepX++)
        {
            for (int stepY = y - (int)(size - 1); stepY <= y + (int)(size - 1); stepY++)
            {
                viewInstances.cells[stepX, stepY].isOccupied = false;
            }
        }
        viewInstances.buildings.Remove(viewInstances.cells[x, y].building);
        Destroy(viewInstances.cells[x, y].building);
        viewInstances.cells[x, y].isOccupied = false;
        viewInstances.deleteWindow.SetActive(false);
    }
    private void Start()
    {
        viewInstances.saveButton.onClick.AddListener(SaveScene);
        viewInstances.loadButton.onClick.AddListener(LoadScene);
        viewInstances.buildButton.onClick.AddListener(ShowBuildingWindow);
        viewInstances.cookButton.onClick.AddListener(Cook);
        viewInstances.button1x1.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button1x1.tag); });
        viewInstances.button2x2.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button2x2.tag); });
        viewInstances.button3x3.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button3x3.tag); });
        viewInstances.delete.onClick.AddListener(DeleteBuilding);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && modelInstances.isShowingBuildingWindow)
        {
            RaycastHit hit;
            Ray ray = viewInstances.cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit, 10f))
            {
                viewInstances.buildWindow.SetActive(false);
                modelInstances.isShowingBuildingWindow = false;
                viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
        }
    }
}
