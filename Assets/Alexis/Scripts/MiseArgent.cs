using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiseArgent : MonoBehaviour
{

    public Text moneySpend;

    public Text prixDepart;
    public Text miseActuelle;
    public Text maMise;

    public int prixDepartMoney;
    public int miseActuelleMoney;
    public int maMiseMoney;

    public int coeffMoney;
    int moneyValue;

    // Start is called before the first frame update
    void Start()
    {
        prixDepart.text = "" + prixDepartMoney;
        miseActuelle.text = "" + miseActuelleMoney;
        maMise.text = "" + maMiseMoney;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addMoney()
    {

        moneyValue += coeffMoney;
        moneySpend.text = "" + moneyValue;

    }
    public void subMoney()
    {
        if (moneyValue > 0)
        {
            moneyValue -= coeffMoney;
            moneySpend.text = "" + moneyValue;
        }
    }

    public void ValiderMoney()
    {
        maMiseMoney = moneyValue;
        moneyValue = 0;
        miseActuelleMoney += maMiseMoney;

        moneySpend.text = "" + moneyValue;
        maMise.text = "" + maMiseMoney;
        miseActuelle.text = "" + miseActuelleMoney;
    }
    public void AnnulerMoney()
    {
        maMiseMoney = 0;
        moneyValue = 0;
        maMise.text = "" + maMiseMoney;
        moneySpend.text = "" + moneyValue;
    }
}
