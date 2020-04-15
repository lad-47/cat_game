using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    
    Camera _camera;
    void Start() {
        _camera = GetComponent<Camera>();
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update() {
        float unitsPerPixel = 1;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        GetComponent<Camera>().orthographicSize = desiredHalfHeight;
        Camera.main.transform.position = new Vector2 (Screen.width/2, Screen.height/2);
    }
}
