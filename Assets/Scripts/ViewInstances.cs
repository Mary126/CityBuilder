using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInstances : MonoBehaviour
{
    public CellInstances[,] cells = new CellInstances[10, 10];
    public GameObject[,] buildings = new GameObject[10, 10];
    public Camera cam;
    public Button saveButton;
    public Button loadButton;
    public Button buildButton;
    public Button cookButton;
    public GameObject field;
}
