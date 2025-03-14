using Unity.VisualScripting;
using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    public GameObject exit; 
    public GameObject player;
    public GameObject playerBody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetInstanceID() == playerBody.GetInstanceID()) {
            player.transform.position = exit.transform.position; }
    }
}
