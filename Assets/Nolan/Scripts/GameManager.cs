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

    [HideInInspector]
    public PlayerController player;

    [Header("Deroulement partie")]
    public int roundToEnd;
    public int currentRound=0;
    public int dayEnd;
    public int currentDay = 0;
    public GameObject[] eachObject;

    [HideInInspector]
    public Object saveObj;

    public GameObject[] eachObjectRound2;
    public GameObject[] eachObjectRound3;
    public GameObject parentObject;

    public MiseArgent scriptMise;

    public GameObject arrow;

    public AudioSource yawn;
    public AudioSource roundSound;


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


        scriptMise.moneyValue = saveObj.price;
        scriptMise.prixDepart.text = "" + saveObj.defaultPrice.ToString();

        scriptMise.miseActuelle.text = "" + saveObj.price.ToString();
        scriptMise.moneySpend.text = "" + saveObj.price.ToString();

        if (obj.playerProperty)
        {
            arrow.SetActive(true);
        }
    }

    public void CloseCanvas()
    {
        objectToShow.gameObject.SetActive(false);
        saveObj = null;
        arrow.SetActive(false);
    }

    public void passRoundOrDay()
    {
        #region Day
        if (currentRound == roundToEnd-1)
        {
            for(int i = 0; i < eachObject.Length; i++)
            {
                if (eachObject[i].GetComponent<Object>().playerProperty)
                {
                    player.argent += eachObject[i].GetComponent<Object>().sellingPrice;
                    player.argent += eachObject[i].GetComponent<Object>().misePlayer = 0;
                }
                else
                {
                    player.argent += eachObject[i].GetComponent<Object>().misePlayer;
                    player.argent += eachObject[i].GetComponent<Object>().misePlayer = 0;
                }
            }
            player.miseTotaleT.text = "0";
            player.argentT.text = player.argent.ToString();

            if (currentDay == 0)
            {
                for (int i = 0; i < eachObject.Length; i++)
                {
                    var save = eachObject[i].gameObject;
                    eachObject[i] = Instantiate(eachObjectRound2[i].gameObject);
                    eachObject[i].gameObject.transform.position = save.transform.position;
                    save.gameObject.SetActive(false);
                    save = null;
                }
            }
            else if (currentDay == 1)
            {
                for (int i = 0; i < eachObject.Length; i++)
                {
                    Instantiate(eachObjectRound3[i].gameObject);
                    eachObjectRound3[i].transform.position = eachObject[i].transform.position;
                    eachObject[i].gameObject.SetActive(false);
                    eachObject[i] = eachObjectRound3[i].gameObject;
                }
            }
            else
            {
                End();
            }
            currentDay++;
            Debug.Log(currentDay);
            yawn.Play();
            currentRound = 0;
        }
        #endregion
        #region Round
        else
        {
            currentRound++;
            roundSound.Play();
            for (int i = 0; i < eachObject.Length; i++)
            {
                eachObject[i].GetComponent<Object>().UpdatePrice(currentRound);
            }
        }
        #endregion
    }


    private void End()
    {

    }
}
