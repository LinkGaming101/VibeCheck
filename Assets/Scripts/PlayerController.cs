using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rig;
    public bool inDialogue;

    [Header("Player Controls")]
    public KeyCode interactionKey;
    public float interactRange;
    public int interactLayer;

    [Header("Player Stats")]
    public int anxietyPoints;

    [Header("Player Stats")]
    public string playerName;
    public GameObject player;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        player = this.gameObject;

        interactLayer = LayerMask.NameToLayer("Interactables");
    }

    void Update()
    {
        if (inDialogue == false)
        {
            Move();
        }

        Animate();

        if(Input.GetKeyDown(interactionKey))
        {
            if (!inDialogue)
            {
                Interact();
            }
            else if (inDialogue)
            {
                print("exit dialogue");
                inDialogue = false;  
            }
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

        foreach (RaycastHit hit in hits.Where(hit => hit.transform.gameObject.layer == interactLayer))
        {
            //grab the NPCs dialogue function and run it
            hit.collider.GetComponent<NPCBehaviour>()?.Dialogue(player);

            // if interacting with item we want to grab the item
        }
    }

    public void exitDialogue()
    {
        inDialogue = false;
    }
}
