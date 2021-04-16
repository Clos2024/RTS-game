using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoomSpeed, panSpeed;
    public float zoomSpeedReg, panSpeedReg,zoomSpeedSprint,panSpeedSprint;
    private float zoomInClamp, zoomOutClamp;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            panSpeed = panSpeedSprint;
            zoomSpeed = zoomSpeedSprint;
        }
        else
        {
            panSpeed = panSpeedReg;
            zoomSpeed = zoomSpeedReg;
        }
        //Camera Zoom
        transform.position += new Vector3(Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed * -1, Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed);
        //WASD camera panning
        transform.position += new Vector3(Input.GetAxis("Vertical") * panSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * panSpeed * Time.deltaTime);
        transform.position += new Vector3(Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime, 0, Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime *-1);


    }
}
