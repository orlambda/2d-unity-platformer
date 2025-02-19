using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    public GameObject exit; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = exit.transform.position;
    }
}
