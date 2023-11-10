using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Transform camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Camera cameraObj;

    float mouseSens = 0.0f;
    public float maxFOV = 20;
    public float minFOV = 2;
    Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        mouseSens = cameraObj.orthographicSize * 0.03f;
    }


    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cameraObj.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cameraObj.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position += difference;
        }

    }

    private void LateUpdate()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (cameraObj.orthographicSize <= maxFOV && cameraObj.orthographicSize >= minFOV)
            {
                cameraObj.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel");
            }

            if (cameraObj.orthographicSize > maxFOV)
            {
                cameraObj.orthographicSize = maxFOV;
            }

            if (cameraObj.orthographicSize < minFOV)
            {
                cameraObj.orthographicSize = minFOV;
            }
        }
    }
}
