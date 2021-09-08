using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiseArgent : MonoBehaviour
{
    //scoreVideo = FindObjectOfType<VideosManager>();
    public Text moneySpend;

    public Text prixDepart;
    public Text miseActuelle;

    public int prixDepartMoney;
    public int miseActuelleMoney;
    public int maMiseMoney;

    bool iSpendMoney = false;
    bool iRecoverMoney = false;
    public int coeffMoney;
    int moneyValue;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        prixDepart.text = "" + prixDepartMoney;
        miseActuelle.text = "" + miseActuelleMoney;
        moneySpend.text = "" + miseActuelleMoney;
        moneyValue = miseActuelleMoney;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void addMoney()
    {
        moneyValue += coeffMoney;
        moneySpend.text = "" + moneyValue;

        iSpendMoney = true;
        iRecoverMoney = false;
    }
    public void subMoney()
    {
        if (moneyValue > prixDepartMoney)
        {
            moneyValue -= coeffMoney;
            moneySpend.text = "" + moneyValue;

            iSpendMoney = false;
            iRecoverMoney = true;
        }
    }

    public void ValiderMoney()
    {
        if (iSpendMoney == true)
        {
            miseActuelleMoney = moneyValue;
            moneySpend.text = "" + miseActuelleMoney;
            miseActuelle.text = "" + miseActuelleMoney;

            controller.argent -= moneyValue;
            controller.miseTotale += moneyValue;
            controller.argentT.text = "" + controller.argent;
            controller.miseTotaleT.text = "" + controller.miseTotale;

            iSpendMoney = false;
        }
        else if (iRecoverMoney == true && miseActuelleMoney != prixDepartMoney)
        {
            miseActuelleMoney = moneyValue;
            moneySpend.text = "" + miseActuelleMoney;
            miseActuelle.text = "" + miseActuelleMoney;
            
            controller.argent += moneyValue;
            controller.miseTotale -= moneyValue;
            controller.argentT.text = "" + controller.argent;
            controller.miseTotaleT.text = "" + controller.miseTotale;
            iRecoverMoney = false;
        }

    }

    /*public void AnnulerMoney()
    {
        moneyValue = prixDepart;
        moneySpend.text = "" + moneyValue;
    }*/
    public void ActiveMoney()
    {
        moneyValue = miseActuelleMoney;
    }
}
