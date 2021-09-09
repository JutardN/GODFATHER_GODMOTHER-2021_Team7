using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public int argent = 1000;
    public int miseTotale;
    public TextMeshProUGUI miseTotaleT;
    public TextMeshProUGUI argentT;

    public Canvas interact;
    private bool canvasOpen;

    public TextMeshProUGUI levelText;

    public MiseArgent checkMoney;

    private bool onCouch;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        argentT.text = "" + argent;
        if (!checkMoney)
        {
            checkMoney = FindObjectOfType<MiseArgent>();
        }
        miseTotaleT.text = "0";
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            UpgradeLevelKnowledge();
        }

        if(onCouch && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.passRoundOrDay();
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
        }
        if (collision.tag == "Couch")
        {
            interact.transform.position = collision.transform.position;
            interact.gameObject.SetActive(true);
            onCouch = true;
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
        if (collision.tag == "Couch")
        {
            interact.gameObject.SetActive(false);
            onCouch = false;
        }
    }

    void UpgradeLevelKnowledge()
    {
        levelKnowledge++;
        levelText.text = levelKnowledge.ToString();
    }
}
