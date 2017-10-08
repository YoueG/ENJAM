using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playImageWhite;
    public GameObject playImageBlack;

    private bool  imageState;
    private float elapsedTime;
	
	void Start ()
    {
        imageState  = true;
        elapsedTime = 0.0f;
        mainCamera.transform.position = new Vector3(0.96f, 6.3f, -1.94f);
    }
	
	void Update ()
    {
        elapsedTime += Time.deltaTime;
		if(elapsedTime >= 1.0f)
        {
            if(imageState)
            {
                imageState = false;
                playImageWhite.SetActive(false);
                playImageBlack.SetActive(true);
            }
            else
            {
                imageState = true;
                playImageWhite.SetActive(true);
                playImageBlack.SetActive(false);
            }

            elapsedTime = 0.0f;
        }
	}
}
