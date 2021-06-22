
using UnityEngine;

public class TargetPlayerCamera : MonoBehaviour
{
    public GameObject target;  // Hedef
    public Vector3 cameraoffset;    // Hedeften ne kadar farkl� bir konumda duracak
    public Vector3 targetedposition;// kameran�n ge�i� h�z�
                                    //De�erlerle oynayarak bakabilirsin
    
    public float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;
    void Update()
    {
        targetedposition = target.transform.position + cameraoffset;
        if (targetedposition.x < 61.57309 )
        {

            transform.position = Vector3.SmoothDamp(transform.position, targetedposition, ref velocity, smoothTime);

        }
        
    }
    
}
