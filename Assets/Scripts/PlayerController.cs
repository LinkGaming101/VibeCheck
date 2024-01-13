using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public int interactLayer;

    public KeyCode animCheer;
    public KeyCode animCheck;
    public KeyCode animHappy;

    [Header("Player Stats")]
    public int anxietyPoints;

    [Header("Player Stats")]
    public string playerName;
    public GameObject player;

    public List<string> playerDialogue = new List<string>();
    void Start()
    {
        rig = GetComponent<Rigidbody>();

        player = this.gameObject;

        interactLayer = LayerMask.NameToLayer("Interactables");

        inDialogue = false;
    }

    void FixedUpdate()
    {
        if (inDialogue == false)
        {
            Move();
        }
    }


    void Update()
    {

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
    }

    void Interact()
    {
        Debug.Log("Try Interact");
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, interactRange, 1 << 8);

        foreach (RaycastHit hit in hits.Where(hit => hit.transform.gameObject.layer == interactLayer))
        {
            //grab the NPCs dialogue function and run it
            Debug.Log("Try Dialogue");
            hit.collider.GetComponent<NPCBehaviour>()?.Dialogue(player);

            // if interacting with item we want to grab the item
        }
    }

    public void exitDialogue()
    {
        inDialogue = false;
    }
}
