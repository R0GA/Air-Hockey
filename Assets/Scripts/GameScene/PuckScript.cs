using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{

    public ScoreScript ScoreScriptInstance;

    public static bool WasGoal {  get; private set; }

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    Rigidbody2D rb;

    public Transform portal1;
    public Transform portal2;
    //public float teleportOffset = 0.5f; // Adjust this offset as needed
    private bool canTeleport = true;
    public float teleportCooldown = 1f;

    void Start()
    {
        
        WasGoal = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!WasGoal)
        {
            
            if (other.tag == "AiGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(false));
            }
            else if (other.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(true));
            }

        }

        if(other.tag == "Arrow")
        {
           // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);

            Debug.Log("Bonk");

        }

        if (canTeleport && (other.transform == portal1 || other.transform == portal2))
        {
            // Teleport the puck to the opposite portal
            Transform targetPortal = other.transform == portal1 ? portal2 : portal1;
            transform.position = targetPortal.position;

            // Invert the y velocity
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

            // Start cooldown timer
            canTeleport = false;
            Invoke("ResetTeleportCooldown", teleportCooldown);
        }

    }

    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0, 0);

        if(didAiScore)
        {
            rb.position = new Vector2(-1, 0);
        }
        else
        {
            rb.position = new Vector2(1, 0);
        }

    }

    public void CenterPuck()
    {

        rb.position = new Vector2(0, 0);

    }

    private void FixedUpdate()
    {
      
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

    }

    public void TeleportToOtherPortal(GameObject portal)
    {
        // Calculate the position to teleport to (opposite portal's position)
        Vector3 destination = portal.transform.position;

        // Teleport the puck to the destination
        transform.position = destination;
    }

    // Method to invert the y velocity of the puck
    public void InvertYVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
    }

    private void ResetTeleportCooldown()
    {
        canTeleport = true;
    }

}
