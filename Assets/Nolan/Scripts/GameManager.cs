using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Canvas objectToShow;
    public Image imageObj;
    public TextMeshProUGUI nameObj;
    public TextMeshProUGUI descObj;
    public TextMeshProUGUI priceObj;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCanvas(Object obj)
    {
        objectToShow.gameObject.SetActive(true);
        imageObj.sprite = obj.image;
        nameObj.text = obj.name;
        if (player.levelKnowledge >= 2 && obj.maxLvlKnowledge==2)
        {
            descObj.text = obj.descriptionlvl3;
        }
        else if (player.levelKnowledge >= 1 && obj.maxLvlKnowledge >= 1)
        {
            descObj.text = obj.descriptionlvl2;
        }
        else
        {
            descObj.text = obj.descriptionlvl1;
        }

        priceObj.text = obj.price;
    }

    public void CloseCanvas()
    {
        objectToShow.gameObject.SetActive(false);
    }
}
