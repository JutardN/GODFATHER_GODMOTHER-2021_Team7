using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiseArgent : MonoBehaviour
{
    public Text moneySpend;
    public Text prixDepart;
    public Text miseActuelle;

    public int prixDepartMoney;
    public int miseActuelleMoney;
    public int coeffMoney;
    int maMiseMoney;
    int maMoneyBack;


    int moneyValue;

    bool iSpendMoney = false;
    bool iRecoverMoney = false;
    bool iValid = false;
    bool iCancel = false;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        prixDepart.text = "" + prixDepartMoney;
        moneyValue = miseActuelleMoney;
        miseActuelle.text = "" + miseActuelleMoney;
        moneySpend.text = "" + miseActuelleMoney;
 
    }

    // Update is called once per frame
    void Update()
    {
        if(iSpendMoney == true && iCancel != true)
        {
            moneyValue += coeffMoney;
            moneySpend.text = "" + moneyValue;
            iSpendMoney = false;
            maMiseMoney = moneyValue;
        }
        else if (iRecoverMoney == true && moneyValue > miseActuelleMoney && iCancel != true)
        {
            moneyValue -= coeffMoney;
            moneySpend.text = "" + moneyValue;
            iRecoverMoney = false;
            maMiseMoney = moneyValue;

        }

        if(iValid == true && iCancel != true)
        {
            if (maMiseMoney < miseActuelleMoney)
            {
                miseActuelleMoney = moneyValue;
                moneySpend.text = "" + moneyValue;
                miseActuelle.text = "" + moneyValue;

                controller.argent += moneyValue;
                controller.miseTotale -= moneyValue;
                controller.argentT.text = "" + controller.argent;
                controller.miseTotaleT.text = "" + controller.miseTotale;

                iValid = false;
                iCancel = true;
            }
            else if(maMiseMoney > miseActuelleMoney)
            {
                miseActuelleMoney = moneyValue;
                moneySpend.text = "" + moneyValue;
                miseActuelle.text = "" + moneyValue;

                controller.argent -= moneyValue;
                controller.miseTotale += moneyValue;
                controller.argentT.text = "" + controller.argent;
                controller.miseTotaleT.text = "" + controller.miseTotale;

                iValid = false;
                iCancel = true;
            }
                
        }
    }

    public void addMoney()
    {
        iSpendMoney = true;
        iRecoverMoney = false;
    }
    public void subMoney()
    {
        iSpendMoney = false;
        iRecoverMoney = true;
    }

    public void ValiderMoney()
    {
        if (miseActuelleMoney != moneyValue)
        {
            iValid = true;
        }

    } 

    public void AnnulerMoney()
    {

        if (moneyValue > prixDepartMoney && iCancel == true)
        {
            controller.miseTotale -= moneyValue;
            controller.argent += moneyValue;

            miseActuelleMoney = prixDepartMoney;
            moneyValue = miseActuelleMoney;

            moneySpend.text = "" + moneyValue;
            miseActuelle.text = "" + miseActuelleMoney;
            controller.argentT.text = "" + controller.argent;
            controller.miseTotaleT.text = "" + controller.miseTotale;
            iCancel = false;
        }
    
    }
    public void ActiveMoney()
    {
        moneyValue = miseActuelleMoney;
    }
}
