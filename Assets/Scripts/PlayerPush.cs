using System.Collections;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    float direction;
    GameObject box;
    public Animator animator;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Burada scale i ayarlayamadýðým için , raycast içindeki direction kýsmýný kontrl etmek için rotation üzerinden deðer deðiþtiriyorum(objem rotation a göre dönüyor)
        if(transform.rotation.y == -1 || transform.rotation.y == 1)
        {
            direction = -1;

        }
        else if(transform.rotation.y == 0)
        {
            direction = 1;
        }

        

        //Physics2D.queriesStartInColliders = false; 
        //Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit =   Physics2D.Raycast(transform.position, Vector2.right * direction, distance, boxMask); // Raycast2d Objemizden ýþýn yollayan bir fonk. bu sayede uzaktaki bir
        
        // cisimle temas edip kontrol edebiliriz.

        


        if (hit.collider != null && hit.collider.gameObject.tag == "Blocks" && Input.GetKeyDown(KeyCode.H) && !animator.GetBool("IsJump")) // Eðer raycastimiz bir cisme çarparsa ve e tuþunua basýldýysa
        {
            box = hit.collider.gameObject;
            
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            box.GetComponent<FixedJoint2D>().autoConfigureConnectedAnchor = false;
            
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<FixedJoint2D>().autoConfigureConnectedAnchor = true;
            
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * direction * distance); // ýþýnýmýza gizmos üzerinden çizgi ekliyoruz
    }
}
