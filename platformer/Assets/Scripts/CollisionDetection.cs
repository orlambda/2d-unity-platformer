using UnityEngine;
using UnityEngine.Rendering;

public class CollisionDetection : MonoBehaviour
{
    private int collisionCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount += 1;
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount -= 1;        
    }
}
