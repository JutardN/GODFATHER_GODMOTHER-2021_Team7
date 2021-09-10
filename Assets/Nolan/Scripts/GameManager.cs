using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public Animator fade;
    public GameObject fondTransi;
    public GameObject transiFade;
    public float timeTransi = 5f;

    private Text textSell;
    private GameObject image;

    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        textSell = fondTransi.transform.GetChild(3).GetComponent<Text>();
        image = fondTransi.transform.GetChild(1).gameObject;
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
        scriptMise.StartCoroutine("WaitBeforeValidate");
        player.stopPlayer = true;
        player.rb.velocity = new Vector2(0,0);
        player.anim.SetBool("Walking", false);
        player.footStep.Stop();
        player.inBet = true;
    }

    public void CloseCanvas()
    {
        objectToShow.gameObject.SetActive(false);
        //saveObj = null;
        arrow.SetActive(false);
        scriptMise.StopAllCoroutines();
        scriptMise.inMenu = false;
        player.stopPlayer = false;
        player.inBet = false;
        player.canvasOpen = false;
    }

    public void passRoundOrDay()
    {
        #region Day
        if (currentRound == roundToEnd-1)
        {
            image.SetActive(true);
            textSell.gameObject.SetActive(true);
            textSell.text = "";
            for(int i = 0; i < eachObject.Length; i++)
            {
                //////// Player obtain object /////////////
                if (eachObject[i].GetComponent<Object>().playerProperty)
                {
                    player.argent += eachObject[i].GetComponent<Object>().sellingPrice;
                    player.argent += eachObject[i].GetComponent<Object>().misePlayer = 0;
                    textSell.text += "Congratulations, you managed to get " + eachObject[i].GetComponent<Object>()._name + " and resell it for " + eachObject[i].GetComponent<Object>().sellingPrice + "\n";
                }
                //////  Player didn't obtain object /////////
                else if (!eachObject[i].GetComponent<Object>().playerProperty && eachObject[i].GetComponent<Object>().misePlayer>0)
                {

                    player.argent += eachObject[i].GetComponent<Object>().misePlayer;
                    player.argent += eachObject[i].GetComponent<Object>().misePlayer = 0;
                    textSell.text += "Too bad, you didn't manage to get " + eachObject[i].GetComponent<Object>()._name + "\n";
                }
                else if(!eachObject[i].GetComponent<Object>().playerProperty && eachObject[i].GetComponent<Object>().misePlayer == 0)
                {

                    textSell.text += eachObject[i].GetComponent<Object>()._name + " was sold for " + eachObject[i].GetComponent<Object>().sellingPrice + "\n";
                }
                if(currentDay == 2 && eachObject[i].GetComponent<Object>()._name == "Gilded box" && eachObject[i].GetComponent<Object>().playerProperty)
                {
                    win = true;
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
                    var save = eachObject[i].gameObject;
                    eachObject[i] = Instantiate(eachObjectRound3[i].gameObject);
                    eachObject[i].gameObject.transform.position = save.transform.position;
                    save.gameObject.SetActive(false);
                    save = null;
                }
            }
            else
            {
                End();
            }
            if (currentDay != 2)
            {
                currentDay++;
                yawn.Play();
                currentRound = 0;

                timeTransi = 5;

                transiFade.SetActive(true);
                fade.SetTrigger("Start");
                StartCoroutine(Fade());
                player.stopPlayer = true;
            }
        }
        #endregion
        #region Round
        else
        {
            timeTransi = 2;

            image.SetActive(false);
            textSell.gameObject.SetActive(false);
            currentRound++;

            roundSound.Play();

            
            transiFade.SetActive(true);
            fade.SetTrigger("Start");
            StartCoroutine(Fade());
            player.stopPlayer = true;

            for (int i = 0; i < eachObject.Length; i++)
            {
                eachObject[i].GetComponent<Object>().UpdatePrice(currentRound);
            }
        }
        #endregion
    }


    private void End()
    {
        transiFade.SetActive(true);
        fade.SetTrigger("Start");
        StartCoroutine(FadeEnd());
    }

    IEnumerator Fade()
    {
        fade.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        transiFade.SetActive(false);
        fondTransi.SetActive(true);

        fade.ResetTrigger("Start");
        

        yield return new WaitForSeconds(timeTransi);
        fondTransi.SetActive(false);
        transiFade.SetActive(true);
        fade.SetTrigger("End");
        fondTransi.SetActive(false);

        yield return new WaitForSeconds(1);
        transiFade.SetActive(false);
        fade.ResetTrigger("End");
        player.stopPlayer = false;

    }
    
    IEnumerator FadeEnd()
    {
        fade.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        transiFade.SetActive(false);
        fondTransi.SetActive(true);

        fade.ResetTrigger("Start");
        
        yield return new WaitForSeconds(timeTransi);

        if (win)
        {
            fondTransi.transform.GetChild(4).gameObject.SetActive(true);
            textSell.text = "Curious to know what the chest contains, you open it and find a letter inside: My dear son, I hope this letter will find you someday, as I must tell you something you would never know otherwise: I am a spy for the French Liberation Army." +
                " If I may have one piece of advice to give you, after the many lives that I had to watch, it is this one :  always manage your expenses.Enjoy your life, but do not empoverish yourself." +
                " I look forward to see you again... For Terence, your Mother.";
            yield return new WaitForSeconds(15);

        }
        else
        {
            textSell.text = "Unfortunately, Terence did not spotted the fraud and got scammed. Out of money, he is unable to keep on bidding, and so he goes back to his poor sailor life... ";
            yield return new WaitForSeconds(10);
        }

        SceneManager.LoadScene(0);
    }
}
