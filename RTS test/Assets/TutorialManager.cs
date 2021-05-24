using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private bool tutorial = true;
    public GameObject cameraObj;
    public GameObject dialogueBubble;
    public CameraController cameraController;
    public Text dialogueText;
    private int count = 0;
    private int stage = 0;
    private int video = 0;
    public string[] tutorialDialogue;
    public Transform[] cameraPositions;
    public GameObject videoTutorials;

    private void Awake()
    {
        cameraController.camEnabled = false;
        dialogueText.text = tutorialDialogue[count];
        cameraObj.transform.position = cameraPositions[0].position;

        if (dialogueBubble.activeInHierarchy == false)
        {
            dialogueBubble.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial)
        {
            if (stage == 0)
            {
                if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                {
                    count++;
                    if (count < tutorialDialogue.Length - 1)
                    {
                        dialogueText.text = tutorialDialogue[count];
                        if(count == 3)
                        {
                            stage++;
                        }
                    }
                }
            }
            if(stage == 1)
            {
                cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, cameraPositions[1].transform.position,1.0f*Time.deltaTime);
                cameraObj.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cameraObj.GetComponent<Camera>().orthographicSize, cameraPositions[1].GetComponent<cameraLenseSettings>().zoom,1.0f*Time.deltaTime);

                if(Vector3.Distance(cameraObj.transform.position, cameraPositions[1].transform.position) <= 1.0f)
                {
                    if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                    {
                        dialogueBubble.SetActive(false);
                        stage++;
                    }
                }
            }
            else if(stage == 2)
            {
                cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, cameraPositions[0].transform.position, 1.0f * Time.deltaTime);
                cameraObj.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cameraObj.GetComponent<Camera>().orthographicSize, cameraPositions[0].GetComponent<cameraLenseSettings>().zoom, 1.0f * Time.deltaTime);
                
                if (Vector3.Distance(cameraObj.transform.position, cameraPositions[0].transform.position) <= 1.0f)
                {
                    videoTutorials.transform.GetChild(video).gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Escape))
                    {
                        videoTutorials.transform.GetChild(video).gameObject.SetActive(false);
                        video++;
                        if(video == 3)
                        {
                            stage++;
                        }
                    }
                }
            }
            else if(stage == 3)
            {
                videoTutorials.SetActive(false);
                tutorial = false;
            }
        }
        else
        {
            cameraController.camEnabled = true;
        }
    }
}
