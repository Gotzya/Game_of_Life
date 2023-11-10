using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridHandler : MonoBehaviour
{
    [SerializeField] public Text generationCounter;
    [SerializeField] public Text generationSpeedCounter;
    public Slider generationSpeedSlider;
    public float generationSpeedMulitier;
    public int generation;
    public GameObject cellPrefab;
    public Transform gridFolder;
    public cellScript[,] grid;
    int width = 100;
    int height = 100;
    public bool simulating = false;

    // Start is called before the first frame update
    void Start()
    {
        generationSpeedMulitier = 1; 

        grid = new cellScript[width, height];

        for (int xCord = 0; xCord < width; xCord++)
        {
            // run through all y values
            for (int yCord = 0; yCord < height; yCord++)
            {
                //draws the lines of cell
/*                Debug.DrawLine(new Vector2(xCord, yCord), new Vector2(xCord + 1, yCord), Color.gray, 1000);
                Debug.DrawLine(new Vector2(xCord, yCord), new Vector2(xCord, yCord + 1), Color.gray, 1000);*/

                GameObject cell = Instantiate(cellPrefab, new Vector2(xCord + 0.5f, yCord + 0.5f), Quaternion.Euler(0, 0, 0));
                cell.name = string.Format("cell: {0}, {1}", xCord, yCord);
                grid[xCord, yCord] = cell.GetComponent<cellScript>();
                // grid[xCord, yCord].parent = 
            }
        }

    }

    public void oneGeneration()
    {
        if (simulating == false)
        {
            Debug.Log("Forward 1 generation");
            generationCounter.text = "Generation: " + generation;
            generationAlg();
        }
        else
        {
            Debug.Log("Please stop the simulation first");
        }
    }

    public void repeatedGen()
    {
        simulating = !simulating;

        if (simulating)
        {
            Debug.Log("Starting Generation");
            CancelInvoke("generationAlg");
            InvokeRepeating("generationAlg", 0, 1 / generationSpeedMulitier);
        }
        else
        {
            Debug.Log("Ending Generation");
            CancelInvoke("generationAlg");
        }
    }

    public void generationSpeed()
    {
        generationSpeedCounter.text = Mathf.Round(generationSpeedSlider.value) + "x";
        generationSpeedMulitier = generationSpeedSlider.value;

        if (simulating)
        {
            Debug.Log("Speed Changed");
            CancelInvoke("generationAlg");
            InvokeRepeating("generationAlg", 0.4f, 1 / generationSpeedMulitier);
        }
    }

    public void clearGrid()
    {
        for (int xCord = 0; xCord < width; xCord++)
        {
            for (int yCord = 0; yCord < height; yCord++)
            {
                grid[xCord, yCord].GetComponent<cellScript>().clearCell();
            }
        }

        generation = 0;
        generationCounter.text = "Generation: " + generation;
    }

    public void generationAlg()
    {
        generation++;
        generationCounter.text = "Generation: " + generation;

        for (int xCord = 0; xCord < width; xCord++)
        {
            for (int yCord = 0; yCord < height; yCord++)
            {
                int adjecent = 0;

                try
                {
                    if (grid[xCord, yCord + 1].GetComponent<cellScript>().active == true) adjecent++; // Front
                    if (grid[xCord, yCord - 1].GetComponent<cellScript>().active == true) adjecent++; // Back
                    if (grid[xCord + 1, yCord].GetComponent<cellScript>().active == true) adjecent++; // Right
                    if (grid[xCord - 1, yCord].GetComponent<cellScript>().active == true) adjecent++; // Left

                    if (grid[xCord + 1, yCord + 1].GetComponent<cellScript>().active == true) adjecent++; // Top Right
                    if (grid[xCord + 1, yCord - 1].GetComponent<cellScript>().active == true) adjecent++; // Bottom Right 
                    if (grid[xCord - 1, yCord - 1].GetComponent<cellScript>().active == true) adjecent++; // Bottom Left
                    if (grid[xCord - 1, yCord + 1].GetComponent<cellScript>().active == true)  adjecent++; // Top Left
                }
                catch (System.IndexOutOfRangeException)
                {
                    // skip errors
                }

                grid[xCord, yCord].GetComponent<cellScript>().newNeighbors(adjecent);
            }
        }

        for (int xCord = 0; xCord < width; xCord++)
        {
            // run through all y values
            for (int yCord = 0; yCord < height; yCord++)
            {
                grid[xCord, yCord].GetComponent<cellScript>().doCycle();
            }
        }
    }
}
