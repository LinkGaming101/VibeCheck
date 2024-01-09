using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rig;
    public bool inDialogue;

    [Header("Player Controls")]
    public KeyCode interactionKey;
    public float interactRange;

    [Header("Player Stats")]
    public int anxietyPoints;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();

        Animate();

        if(Input.GetKey(interactionKey))
        {
            Interact();
        }
    }

    void Move()
    {
        // basic movement system for the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;
    }

    void Animate()
    {
        //Animation code goes here if any
    }

    void Interact()
    {        
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, interactRange, 1 << 8);

        foreach (RaycastHit hit in hits)
        {
            //grab the NPCs dialogue function and run it
            hit.collider.GetComponent<NPCBehaviour>()?.Dialogue();

            // if interacting with item we want to grab the item
        }
    }

}
