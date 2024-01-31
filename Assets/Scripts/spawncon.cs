using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawncon : MonoBehaviour
{
    public Transform[] fruitObj;
    static public string spawnedYet = "n";
    static public Vector2 spawnXPos;
    static public Vector2 fruitPos;
    static public string newfruit = "n";
    static public int whichFruit = 0;


    void Start()
    {
        whichFruit = 0;
        newfruit = "n";
        spawnedYet = "n";
    }


    private Vector2 touchPosition;
    public float speed = 0.1f;
    public float minX = -5.0f;
    public float maxX = 5.0f;

    void Update()
    {
        spawnFruit();
        replaceFruit();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Get the touch position
            touchPosition = touch.position;

            // Convert the touch position to world coordinates
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10));

            // Set the object's x-position based on the touch position
            Vector3 newPosition = new Vector3(Mathf.Clamp(touchWorldPosition.x, minX, maxX), transform.position.y, transform.position.z);

            // Check for collisions with walls before updating the position
            if (!CollidesWithWalls(newPosition))
            {
                transform.position = newPosition;
                spawnXPos = transform.position;
            }
        }
    }

    bool CollidesWithWalls(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a wall or obstacle layer
            if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                return true; // Collision with wall detected
            }
        }

        return false; // No collision with walls
    }
    
    
    void spawnFruit()
    {
        if(spawnedYet == "n")
        {
            StartCoroutine(spawnTimer());
            spawnedYet = "y";
        }
    }

    void replaceFruit()
    {
        if (newfruit == "y")
        {
            newfruit = "n";
            if ((whichFruit + 1) < fruitObj.Length)
            {
                Vector2 adjustedSpawnPos = AdjustSpawnPosition(fruitPos);

                // Check for obstacles in the direction of the adjusted spawn position
                Vector2 direction = adjustedSpawnPos - fruitPos;
                RaycastHit2D hit = Physics2D.Raycast(fruitPos, direction, direction.magnitude, obstacleLayer);

                if (hit.collider == null)
                {
                    // No obstacles detected, safe to spawn
                    Instantiate(fruitObj[whichFruit + 1], adjustedSpawnPos, fruitObj[0].rotation);
                }
                else
                {
                    // Adjust the spawn position based on the hit information
                    Vector2 adjustedPosition = hit.point + hit.normal * 0.1f;
                    Instantiate(fruitObj[whichFruit + 1], adjustedPosition, fruitObj[0].rotation);
                }
            }
        }
    }

    
    public LayerMask obstacleLayer;
    
    Vector2 AdjustSpawnPosition(Vector2 desiredPosition)
    {
        // Check if the desired position is inside a wall
        Collider2D hitCollider = Physics2D.OverlapPoint(desiredPosition, obstacleLayer);

        if (hitCollider != null)
        {
            // Adjust the position based on the normal of the wall
            Vector2 adjustedPosition = desiredPosition - (Vector2)hitCollider.bounds.ClosestPoint(desiredPosition);

            return adjustedPosition;
        }

        // If not inside a wall, return the original position
        return desiredPosition;
    }


    IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(fruitObj[Random.Range(0,6)], transform.position, fruitObj[0].rotation);
    }

}

