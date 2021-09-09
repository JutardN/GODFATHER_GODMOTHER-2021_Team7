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

    [Header("Deroulement partie")]
    public int roundToEnd;
    private int currentRound=0;
    public int dayEnd;
    public int currentDay = 0;
    public GameObject[] eachObject;
    private Object saveObj;
    public GameObject[] eachObjectRound2;
    public GameObject[] eachObjectRound3;
    public GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OpenCanvas(Object obj)
    {
        objectToShow.gameObject.SetActive(true);
        imageObj.sprite = obj.image;
        nameObj.text = obj._name;
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

        priceObj.text = obj.price.ToString();
        saveObj = obj;
    }

    public void CloseCanvas()
    {
        objectToShow.gameObject.SetActive(false);
        saveObj = null;

    }

    public void passRoundOrDay()
    {
        if(currentRound == roundToEnd-1)
        {
            if (currentDay == 0)
            {
                for (int i = 0; i < eachObject.Length; i++)
                {
                    Instantiate(eachObjectRound2[i].gameObject);
                    eachObjectRound2[i].transform.position = eachObject[i].transform.position;
                    eachObject[i].gameObject.SetActive(false);
                    eachObject[i] = eachObjectRound2[i].gameObject;
                }
            }
            else if (currentDay == 1)
            {
                for (int i = 0; i < eachObject.Length; i++)
                {
                    eachObject[i] = eachObjectRound3[i].gameObject;
                }
            }
            else
            {
                End();
            }
            currentDay++;
            currentRound = 0;
        }
        else
        {
            currentRound++;
            for (int i = 0; i < eachObject.Length; i++)
            {
                eachObject[i].GetComponent<Object>().UpdatePrice(currentRound);
            }
        }
    }

    public void addMise(int money)
    {
        saveObj.mise = money;
        saveObj.playerProperty = true;
    }
    public void cancelMise()
    {
        saveObj.mise = 0;
        saveObj.playerProperty = false;
    }

    private void End()
    {

    }
}
