using System;
using System.Collections;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;
    private bool canMove = true;
    public LayerMask collisionLayer;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void ResetSprite()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        spriteRend.flipX = false;
    }

    public void Move(Vector2 direction)
    {
        if(!canMove) return;

        canMove = false;
        Game.Instance.hasMadeFirstMove = true;

        StartCoroutine(SmoothMove(direction));
    }

    private IEnumerator SmoothMove(Vector2 direction)
    {
        FaceCorrectDirection(direction);

        float elapsedTime = 0f;
        Vector2 startingPos = rb.position;
        Vector2 targetPos = CalculateTargetPosition(startingPos, direction);

        // Calculate the distance and duration
        float distance = Vector2.Distance(startingPos, targetPos);
        float duration = distance / GameParameters.HamsterMoveSpeed;

        while(elapsedTime < duration) {
            rb.position = Vector2.Lerp(startingPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPos;
        canMove = true;
    }

    private Vector2 CalculateTargetPosition(Vector2 startingPos, Vector2 direction)
    {
        Vector2 targetPos = startingPos + direction * 100f; // The 100 increases the length of the raycast

        // Cast a ray in the direction of movement
        Ray2D ray = new Ray2D(startingPos, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f, collisionLayer);

        if (hit.collider != null)
        {
            // If the ray hits a wall, adjust the target position to one grid space away from the wall
            targetPos = hit.point - direction * 0.5f;

            // Debug crosshair
            Debug.DrawLine(hit.point + Vector2.up * 0.5f, hit.point - Vector2.up * 0.5f, Color.yellow, 2f);
            Debug.DrawLine(hit.point + Vector2.right * 0.5f, hit.point - Vector2.right * 0.5f, Color.yellow, 2f);
        }

        return targetPos;
    }

    private void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face right
        } 
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180); // Face left
        }
        else if (direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90); // Face up
        }
        else if (direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90); // Face down
        }
    }

    public void StopHamsterVelocity()
    {
        rb.velocity = Vector2.zero;
    }
}