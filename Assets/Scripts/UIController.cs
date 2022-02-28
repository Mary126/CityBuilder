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
    void Build()
    {
        if (!modelInstances.isCooking)
        {
            modelInstances.isPlacingBuilgigs = !modelInstances.isPlacingBuilgigs;
            if (modelInstances.isPlacingBuilgigs)
            {
                viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }
            else
            {
                viewInstances.buildButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
        }
    }
    void Cook()
    {
        if (!modelInstances.isPlacingBuilgigs)
        {
            modelInstances.isCooking = !modelInstances.isCooking;
            if (modelInstances.isCooking)
            {
                viewInstances.cookButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }
            else
            {
                viewInstances.cookButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
        }
    }
    private void Start()
    {
        viewInstances.saveButton.onClick.AddListener(SaveScene);
        viewInstances.loadButton.onClick.AddListener(LoadScene);
        viewInstances.buildButton.onClick.AddListener(Build);
        viewInstances.cookButton.onClick.AddListener(Cook);
    }
}
