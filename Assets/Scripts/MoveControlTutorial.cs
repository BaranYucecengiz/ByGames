using System;
using UnityEngine;

public class MoveControlTutorial : MonoBehaviour
{
    //FOV
    public Camera cam;
    private float Speed = 0.0125f;
    bool zoomActive = false;
   
    //TEXT
    public TextMesh tutorialText;
    public string myText;
    
    //FOV
    private void Start()
    {
        cam = Camera.main;
    }
    private void LateUpdate()
    {
       if(zoomActive == true)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, Speed);
        }
       else if (zoomActive == false)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, Speed);
        }
    }
    //TEXT
    private void OnTriggerEnter2D(Collider2D collision) //Nesneyle temas
    {
        zoomActive = true;
        tutorialText.text = myText;
        

    }
    private void OnTriggerExit2D(Collider2D collision) //Nesneyle temas son
    {
        zoomActive = false;
        tutorialText.text = null;
       
    }
}
