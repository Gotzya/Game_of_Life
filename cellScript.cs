using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cellScript : MonoBehaviour
{
    public int neighbors;
    public bool active;
    public Material On;
    public Material Off;

    // Start is called before the first frame update
    void Start()
    {
        neighbors = 0;
        active = false;
        this.GetComponent<SpriteRenderer>().material = Off;
    }

    public void newNeighbors(int newNeighbors)
    {

        neighbors = newNeighbors;

    }

    public void doCycle()
    {
        if (active)
        {
            if (neighbors <= 1 || neighbors >= 4)
            {
                this.GetComponent<SpriteRenderer>().material = Off;
                active = false;
            }
        }

        if (!active)
        {
            if (neighbors == 3)
            {
                this.GetComponent<SpriteRenderer>().material = On;
                active = true;
            }
        }
    }

    public void clearCell()
    {
        active = false;
        this.GetComponent<SpriteRenderer>().material = Off;
    }

    public void OnMouseDown()
    {
        if (IsPointerOverUIObject()) return;

        if (active)
        {
            active = false;
            this.GetComponent<SpriteRenderer>().material = Off;
            Debug.Log("cell unfilled");

        }else if (!active)
        {
            active = true;
            this.GetComponent<SpriteRenderer>().material = On;
            Debug.Log("cell filled");
        }
    }

    public void OnMouseEnter()
    {
        if (IsPointerOverUIObject()) return;

        if (Input.GetMouseButton(0))
        {
            if (active)
            {
                active = false;
                this.GetComponent<SpriteRenderer>().material = Off;
                Debug.Log("cell unfilled");

            }
            else if (!active)
            {
                active = true;
                this.GetComponent<SpriteRenderer>().material = On;
                Debug.Log("cell filled");
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}