using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Change name to player controller;

    [SerializeField] private float speed;
    private bool canMove = true;
    private Animator animator;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject statusScreen;
    [SerializeField] float interactionRange;
    [SerializeField] KeyCode interactKey;
    [SerializeField] LayerMask interactLayer;
    private bool inInventory = false;
    private bool inStatusScreen = false;
    private bool inPause = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
          RunMoveComands();
        }       

        Animate();

        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.I) && inStatusScreen == false && inPause == false)
        {
            inInventory = !inInventory;
            OpenCloseUI(inventoryUI);          

        }

        if (Input.GetKeyDown(KeyCode.Return) && inInventory == false && inPause == false)
        {
            inStatusScreen = !inStatusScreen;
            OpenCloseUI(statusScreen);            
        }        
    }

    private void RunMoveComands()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        transform.position += movement;
    }

    private void Animate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetTrigger("walkingUp");
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetTrigger("walkingDown");
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("walkingLeft");
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("walkingRight");
        }        
    }

    private void Interact()
    {
        Collider2D[] interactable = new Collider2D[1];
        interactable[0] = (Physics2D.OverlapCircle(transform.position, interactionRange, interactLayer));

        try
        {
            if (interactable[0] != null)
            {
                if (interactable[0].TryGetComponent<IInteractable>(out IInteractable interactTarget))
                {
                    interactTarget.Interacted(this.gameObject);
                }
            }
        }
        catch { };
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    private void OpenCloseUI(GameObject uiWindow)
    {
        if (uiWindow.activeSelf ==false)
        {
            canMove = false;
        }
        else if (uiWindow.activeSelf == true)
        {
            canMove = true;
        }

        uiWindow.SetActive(!uiWindow.activeSelf);
        
    }
}
