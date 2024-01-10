using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float rotSpeed = 20f;

    bool isWalking;

    private Rigidbody rig;
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    public bool inDialogue;

    public Animator anim;

    [Header("Player Controls")]
    public KeyCode interactionKey;
    public float interactRange;
    public KeyCode animCheer;
    public KeyCode animCheck;
    public KeyCode animHappy;

    [Header("Player Stats")]
    public int anxietyPoints;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(interactionKey))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // basic movement system for the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool xInputMade = !Mathf.Approximately(x, 0f); //sets true if horizontal input is approx 0
        bool zInputMade = !Mathf.Approximately(z, 0f);
        isWalking = xInputMade || zInputMade;

        movement.Set(x, 0f, z);
        movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, rotSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
        rig.MoveRotation(rotation);

        Animate();

    }

    void Animate()
    {
        if (isWalking)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKey(animCheck))
        {
            anim.SetTrigger("Check");
        }
        if (Input.GetKey(animCheer))
        {
            anim.SetTrigger("Cheer");
        }
        if (Input.GetKey(animHappy))
        {
            anim.SetTrigger("Happy");
        }
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
