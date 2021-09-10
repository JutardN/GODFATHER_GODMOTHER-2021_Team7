using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class MiseArgent : MonoBehaviour
{
    public GameManager manager;
    public Text moneySpend;
    public Text prixDepart;
    public Text miseActuelle;

    public int prixDepartMoney;
    public int miseActuelleMoney;
    public int coeffMoney;

    [HideInInspector]
    public int moneyValue;

    PlayerController controller;
    Player playerController;
    public bool inMenu;
    private bool betBool;
    private Coroutine currentCor;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        controller = manager.player;
        playerController = manager.player.playerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetButton("Action") && inMenu)
        {
            ValiderMoney();
        }
        if (playerController.GetButton("Cancel"))
        {
            AnnulerMoney();
        }

        if (playerController.GetAxis("Bet") > 0 && betBool)
        {
            addMoney();
            betBool = false;
            currentCor = null;
            currentCor = StartCoroutine("WaitBeforeNextBet");
        }
        else if (playerController.GetAxis("Bet") < 0 && betBool)
        {
            subMoney();
            betBool = false;
            currentCor = null;
            currentCor = StartCoroutine("WaitBeforeNextBet");
        }
    }

    IEnumerator WaitBeforeValidate()
    {
        yield return new WaitForSeconds(0.2f);
        inMenu = true;
        currentCor = null;
        currentCor = StartCoroutine("WaitBeforeNextBet");
    }

    IEnumerator WaitBeforeNextBet()
    {
        yield return new WaitForSeconds(0.3f);
        betBool=true;
    }

    public void addMoney()
    {
        if (moneyValue + coeffMoney <= controller.argent + manager.saveObj.misePlayer)
        {
            moneyValue += coeffMoney;
            moneySpend.text = "" + moneyValue;

        }

    }
    public void subMoney()
    {
        if (moneyValue - coeffMoney >= manager.saveObj.price)
        {
            moneyValue -= coeffMoney;
            moneySpend.text = "" + moneyValue;

        }

    }
#region final
    public void ValiderMoney()
    {
        if (miseActuelleMoney != moneyValue && controller.argent >= moneyValue - manager.saveObj.misePlayer)
        {
            miseActuelleMoney = moneyValue;

            moneySpend.text = "" + moneyValue;
            miseActuelle.text = "" + moneyValue;

            controller.argent -= moneyValue - manager.saveObj.misePlayer;
            controller.miseTotale = manager.saveObj.misePlayer + (moneyValue - manager.saveObj.misePlayer);

            controller.argentT.text = "" + controller.argent;

            manager.saveObj.playerProperty = true;
            manager.saveObj.price = moneyValue;
            manager.saveObj.misePlayer = moneyValue;

            controller.miseTotaleT.text = "" + (manager.eachObject[0].GetComponent<Object>().misePlayer + manager.eachObject[1].GetComponent<Object>().misePlayer + manager.eachObject[2].GetComponent<Object>().misePlayer).ToString();
            manager.arrow.SetActive(true);
            manager.CloseCanvas();
        }
    }

    public void AnnulerMoney()
    {

        if (moneyValue > prixDepartMoney)
        {
            controller.miseTotale -= manager.saveObj.misePlayer;
            controller.argent += manager.saveObj.misePlayer;

            manager.saveObj.misePlayer = 0;

            miseActuelleMoney = manager.saveObj.priceEachRound[manager.currentRound];
            moneyValue = miseActuelleMoney;

            moneySpend.text = "" + moneyValue;
            miseActuelle.text = "" + miseActuelleMoney;

            controller.argentT.text = "" + controller.argent;
            controller.miseTotaleT.text = "" + (manager.eachObject[0].GetComponent<Object>().misePlayer + manager.eachObject[1].GetComponent<Object>().misePlayer + manager.eachObject[2].GetComponent<Object>().misePlayer).ToString();

            manager.saveObj.playerProperty = false;
            manager.saveObj.price = moneyValue;

            manager.arrow.SetActive(false);
            manager.CloseCanvas();

        }
    }
#endregion
}
