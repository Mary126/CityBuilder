using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ModelInstances modelInstances;
    public ViewInstances viewInstances;

    void SaveScene()
    {
        modelInstances.gameController.SaveScene();
    }
    void LoadScene()
    {
        modelInstances.gameController.LoadScene();
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
            default: modelInstances.placingBuildingSize = 0; break;
        }
        modelInstances.isPlacingBuilding = true;
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
    private void Start()
    {
        viewInstances.saveButton.onClick.AddListener(SaveScene);
        viewInstances.loadButton.onClick.AddListener(LoadScene);
        viewInstances.buildButton.onClick.AddListener(ShowBuildingWindow);
        viewInstances.cookButton.onClick.AddListener(Cook);
        viewInstances.button1x1.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button1x1.tag); });
        viewInstances.button2x2.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button2x2.tag); });
        viewInstances.button3x3.onClick.AddListener(delegate { PlaceBuildingButton(viewInstances.button3x3.tag); });
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
                viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
        }
    }
}
