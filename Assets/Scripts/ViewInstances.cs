using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInstances : MonoBehaviour
{
    public CellInstances[,] cells = new CellInstances[100, 100];
    public List<GameObject> buildings = new List<GameObject>();
    public Camera cam;
    public Button saveButton;
    public Button loadButton;
    public Button buildButton;
    public Button cookButton;
    public GameObject field;
}
