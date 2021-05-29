using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoomSpeed, panSpeed;
    public float zoomSpeedReg, panSpeedReg,zoomSpeedSprint,panSpeedSprint;
    public int minCap, maxCap;
    public bool camEnabled;
    public GameObject cameraHolder;
    private float rotation;

    private void Start()
    {
        rotation = cameraHolder.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (camEnabled)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                panSpeed = panSpeedSprint;
                zoomSpeed = zoomSpeedSprint;
            }
            else
            {
                panSpeed = panSpeedReg;
                zoomSpeed = zoomSpeedReg;
            }
            //if(Input.GetKeyDown(KeyCode.C))
            //{
            //    rotation -= 45;
            //}
            //Camera Zoom
            if (!Camera.main.orthographic)
                transform.position += new Vector3(Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed * -1, Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed);
            else
            {
                Camera.main.orthographicSize += Input.mouseScrollDelta.y * 0.5f;
                Camera.main.nearClipPlane = -200;
            }

            if (Camera.main.orthographicSize < minCap)
            {
                Camera.main.orthographicSize = minCap;
            }
            else if (Camera.main.orthographicSize > maxCap)
            {
                Camera.main.orthographicSize = maxCap;
            }

            //Quaternion startRot = cameraHolder.transform.rotation;
            //Quaternion endRot = Quaternion.Euler(cameraHolder.transform.rotation.eulerAngles.x, rotation, 0);
            //cameraHolder.transform.rotation = Quaternion.Lerp(startRot, endRot, 10 * Time.deltaTime);

            //WASD camera panning
            transform.position += new Vector3(Input.GetAxis("Vertical") * panSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * panSpeed * Time.deltaTime);
            transform.position += new Vector3(Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime, 0, Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime * -1);
        }
    }
}
