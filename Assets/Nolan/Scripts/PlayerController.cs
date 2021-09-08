using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    public int speed;
    private float hz;
    private Rigidbody2D rb;
    private List<Object> inventory;
    public int levelKnowledge = 0;

    private bool objCollision;
    private Object saveObj;

    public Canvas interact;
    private bool canvasOpen;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hz = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(hz * speed, 0, 0);
    }

    private void Update()
    {
        if (objCollision)
        {
            if (Input.GetKeyDown(KeyCode.E) && !canvasOpen)
            {
                gameManager.OpenCanvas(saveObj);
                canvasOpen = true;
                interact.gameObject.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.E) && canvasOpen)
            {
                interact.gameObject.SetActive(true);
                gameManager.CloseCanvas();
                canvasOpen = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Object")
        {
            saveObj = collision.GetComponent<Object>();
            objCollision = true;
            interact.transform.position = collision.transform.position;
            interact.gameObject.SetActive(true);
            //gameManager.OpenCanvas(collision.GetComponent<Object>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            canvasOpen = false;
            saveObj = null;
            objCollision = false;
            gameManager.CloseCanvas();
            interact.gameObject.SetActive(false);
        }
    }
}
