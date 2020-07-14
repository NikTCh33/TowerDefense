using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cozarm : BaseObject
{

    public List<Minion> minions;

    public int MaxLvlUpgrade = 5;

    public int CurrentPriceMaxMinions = 150;
    public int UpLvlMaxMinions = 1;
    public int MaxMinions = 5;

    public int CurrentPriceHealth = 50;
    public int CurrentUpHealth = 1000;
    public int UpLvlHealth = 1;

    public int CurrentPriceDamage = 100;
    public int CurrentUpDamage = 100;
    public int UpLvlDamage = 1;

    public GameObject Select;
    private static int DefaultPrice = 300;
    void Start()
    {
        transform.SetParent(GlobalData.GameTrans);
        GlobalData.cozarms.Add(this);
    }

    public void UpgradeParametr(ref string text, string type)
    {
        if(type == "Health")
        {
            if (MayBuyUpgrade(type))
            {

                text = "Здоровье минионов увеличено (+" + (UpLvlHealth * 100) + ")";
                BuyUpgrade(type);
            }
            else
                text = "Недостаточно денег";
        }
        else if (type == "Damage")
        {
            if (MayBuyUpgrade(type))
            {
                text = "Урон минионов увеличен (+" + (UpLvlDamage * 100) + ")";
                BuyUpgrade(type);
            }
            else
                text = "Недостаточно денег";
        }
        else if (type == "MaxMinions")
        {
            if (MayBuyUpgrade(type))
            {
                text = "Казарма расширена (+" + UpLvlMaxMinions + ")";
                BuyUpgrade(type);
            }
            else
                text = "Недостаточно денег";
        }
    }

    public bool MayBuyUpgrade(string type)
    {
        if (type == "Health")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        else if (type == "Damage")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        else if (type == "MaxMinions")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        return false;
    }

    public void BuyUpgrade(string type)
    {
        if (type == "Health")
        {
            GlobalData.Money -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
            CurrentPriceHealth *= 2;
            CurrentUpHealth += UpLvlHealth * 100;
            UpLvlHealth++;
        }
        else if (type == "Damage")
        {
            GlobalData.Money -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
            CurrentPriceDamage *= 2;
            CurrentUpDamage += UpLvlDamage * 100;
            UpLvlDamage++;
        }
        else if (type == "MaxMinions")
        {
            GlobalData.Money -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
            CurrentPriceMaxMinions *= 2;
            MaxMinions += UpLvlMaxMinions;
            UpLvlMaxMinions++;
        }
    }

    public int ShowUpgradeParametr(string type)
    {
        if (type == "Health")
        {
            return UpLvlHealth * 100;
        }
        else if (type == "Damage")
        {
            return UpLvlDamage * 100;
        }
        else if (type == "MaxMinions")
        {
            return UpLvlMaxMinions;
        }
        return 0;
    }

    public void GoTo(int x, int y)
    {
        foreach (Minion minion in minions)
            minion.GoTo(x, y, this);
    }

    public override void EventMouseDown()
    {
        
    }

    public override void EventMouseUp()
    {
        ShopClick.Cancel();
        if (ShopMove.Meta.type != null)
            ShopMove.Meta.OffLastObject();
        ShopMove.Meta.type = "Kazarma";
        ShopMove.Meta.RunInfo = Info;
        ShopMove.Meta.RunSell = Sell;
        ShopMove.Meta.OffLastObject = Off;
        ShopMove.Meta.gameObject = gameObject;
        KazarmUpMenu.TempCozarm = this;
        MenuGUI.SetMenuSell(true);
        MenuKazarm.ShowMenu();
        Select.SetActive(true);
    }

    public void Off()
    {
        KazarmUpMenu.Close();
        if (KazarmUpMenu.TempMinion != null)
            KazarmUpMenu.TempMinion.Select = false;
        ShopMove.Selecteditem = 0;
        Select.SetActive(false);
    }

    int AngleInstant = 0;
    public void MayAddMinion(ref string text, string type)
    {
        text = "";
        if (type == "Mechnik")
        {
            if (minions.Count != MaxMinions)
            {
                if (MayBuyMinion(type))
                {
                    BuyMinion(type);
                    Minion m = Instantiate(Resources.Load("Object/GAME/Minions/Minion1") as GameObject, gameObject.transform.position
                        + new Vector3(
                            GlobalData.Sin[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Sin[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f,
                            0.2f,
                            GlobalData.Cos[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Cos[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f), Quaternion.Euler(0, (int)transform.rotation.eulerAngles.y - 90, 0)).GetComponent<Minion>();
                    AngleInstant += 50;
                    if (AngleInstant > 180) AngleInstant %= 180;
                    m.ParentCozarm = this;
                    m.SetCharacteristics();
                    minions.Add(m);
                    text = "Минион добавлен";
                }
                else
                    text = "Недостаточно денег";
            }
            else
                text = "В казарме недостаточно места";
        }
        else if (type == "Luchnik")
        {
            if (minions.Count != MaxMinions)
            {
                if (MayBuyMinion(type))
                {
                    BuyMinion(type);
                    Minion m = Instantiate(Resources.Load("Object/GAME/Minions/Luch") as GameObject, gameObject.transform.position
                        + new Vector3(
                            GlobalData.Sin[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Sin[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f,
                            0.2f,
                            GlobalData.Cos[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Cos[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f), Quaternion.Euler(0, (int)transform.rotation.eulerAngles.y - 90, 0)).GetComponent<Minion>();
                    AngleInstant += 50;
                    if (AngleInstant > 180) AngleInstant %= 180;
                    m.ParentCozarm = this;
                    m.SetCharacteristics();
                    minions.Add(m);
                    text = "Минион добавлен";
                }
                else
                    text = "Недостаточно денег";
            }
            else
                text = "В казарме недостаточно места";
        }
        else if (type == "FireMage")
        {
            if (minions.Count != MaxMinions)
            {
                if (MayBuyMinion(type))
                {
                    BuyMinion(type);
                    Minion m = Instantiate(Resources.Load("Object/GAME/Minions/FireMage") as GameObject, gameObject.transform.position
                        + new Vector3(
                            GlobalData.Sin[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Sin[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f,
                            0.2f,
                            GlobalData.Cos[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Cos[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f), Quaternion.Euler(0, (int)transform.rotation.eulerAngles.y - 90, 0)).GetComponent<Minion>();
                    AngleInstant += 50;
                    if (AngleInstant > 180) AngleInstant %= 180;
                    m.ParentCozarm = this;
                    m.SetCharacteristics();
                    minions.Add(m);
                    text = "Минион добавлен";
                }
                else
                    text = "Недостаточно денег";
            }
            else
                text = "В казарме недостаточно места";
        }
        else if (type == "FrostMage")
        {
            if (minions.Count != MaxMinions)
            {
                if (MayBuyMinion(type))
                {
                    BuyMinion(type);
                    Minion m = Instantiate(Resources.Load("Object/GAME/Minions/FrostMage") as GameObject, gameObject.transform.position
                        + new Vector3(
                            GlobalData.Sin[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Sin[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f,
                            0.2f,
                            GlobalData.Cos[(AngleInstant + 180 + (int)transform.rotation.eulerAngles.y) % 360] / 2f + GlobalData.Cos[((int)transform.rotation.eulerAngles.y + 90) % 360] / 3f), Quaternion.Euler(0, (int)transform.rotation.eulerAngles.y - 90, 0)).GetComponent<Minion>();
                    AngleInstant += 50;
                    if (AngleInstant > 180) AngleInstant %= 180;
                    m.ParentCozarm = this;
                    m.SetCharacteristics();
                    minions.Add(m);
                    text = "Минион добавлен";
                }
                else
                    text = "Недостаточно денег";
            }
            else
                text = "В казарме недостаточно места";
        }     
    }

    public bool MayBuyMinion(string type)
    {
        if (type == "Mechnik")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;

        }
        else if (type == "Luchnik")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        else if (type == "FireMage")
        {
            if ((GlobalData.Money       - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        else if (type == "FrostMage")
        {
            if ((GlobalData.Money - 0 >= 0) &&
                (GlobalData.Minerals[0] - 0 >= 0) &&
                (GlobalData.Minerals[1] - 0 >= 0) &&
                (GlobalData.Minerals[2] - 0 >= 0) &&
                (GlobalData.Minerals[3] - 0 >= 0) &&
                (GlobalData.Minerals[4] - 0 >= 0))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void BuyMinion(string type)
    {
        if(type == "Mechnik")
        {
            GlobalData.Money       -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
        }
        else if (type == "Luchnik")
        {
            GlobalData.Money       -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
        }
        else if (type == "FireMage")
        {
            GlobalData.Money       -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
        }
        else if (type == "FrostMage")
        {
            GlobalData.Money -= 0;
            GlobalData.Minerals[0] -= 0;
            GlobalData.Minerals[1] -= 0;
            GlobalData.Minerals[2] -= 0;
            GlobalData.Minerals[3] -= 0;
            GlobalData.Minerals[4] -= 0;
        }

    }


    public GameObject Sell()
    {
        GlobalData.Money += (int)(DefaultPrice * 0.7f);
        Destroy(gameObject);
        return null;
    }

    public static GameObject MayBuild(ref string text, GameObject go)
    {
        GameObject t = null;
        if (GlobalData.Money - DefaultPrice >= 0)
        {
            GlobalData.Money -= DefaultPrice;
            t = Instantiate(Resources.Load("Object/GAME/Enviroment/Cozarm") as GameObject, go.transform.position, new Quaternion());
            Destroy(go);
            ShopClick.Cancel();
            text = "Установлено";
        }
        else
            text = "Недостаточно денег";
        return t;
    }

    public static void Info()
    {
        string ObjName = "Казарма",
               About = "Это место\nпредназначено для\nсодержания ваших\nуправляемых союзных\nпехотинцев.";
        KazarmUpMenu.Close();
        InfoScr.SetInfo(ObjName, About);
    }

    public void SaveObject(int i)
    {
        PlayerPrefs.SetFloat("Kz" + i + "Px", transform.position.x);
        PlayerPrefs.SetFloat("Kz" + i + "Py", transform.position.y);
        PlayerPrefs.SetFloat("Kz" + i + "Pz", transform.position.z);
        PlayerPrefs.SetFloat("Kz" + i + "Rx", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("Kz" + i + "Ry", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("Kz" + i + "Rz", transform.eulerAngles.z);
        PlayerPrefs.SetInt("Kz" + i + "CPMM", CurrentPriceMaxMinions);
        PlayerPrefs.SetInt("Kz" + i + "ULMM", UpLvlMaxMinions);
        PlayerPrefs.SetInt("Kz" + i + "MM", MaxMinions);
        PlayerPrefs.SetInt("Kz" + i + "CPH", CurrentPriceHealth);
        PlayerPrefs.SetInt("Kz" + i + "CUH", CurrentUpHealth);
        PlayerPrefs.SetInt("Kz" + i + "ULH", UpLvlHealth);
        PlayerPrefs.SetInt("Kz" + i + "CPD", CurrentPriceDamage);
        PlayerPrefs.SetInt("Kz" + i + "CUD", CurrentUpDamage);
        PlayerPrefs.SetInt("Kz" + i + "ULD", UpLvlDamage);
        PlayerPrefs.SetInt("Kz" + i + "AI", AngleInstant);
        PlayerPrefs.SetInt("Kz" + i + "MCnt", minions.Count);
        for (int j = 0; j < minions.Count; j++)
            minions[j].SaveObject(j, i);
    }

    public static void LoadObject(int i)
    {
        cozarm t = Instantiate(Resources.Load("Object/GAME/Enviroment/Cozarm") as GameObject, new Vector3(), new Quaternion()).GetComponent<cozarm>();
        t.transform.position = new Vector3(PlayerPrefs.GetFloat("Kz" + i + "Px"), PlayerPrefs.GetFloat("Kz" + i + "Py"), PlayerPrefs.GetFloat("Kz" + i + "Pz"));
        t.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("Kz" + i + "Rx"), PlayerPrefs.GetFloat("Kz" + i + "Ry"), PlayerPrefs.GetFloat("Kz" + i + "Rz"));
        t.CurrentPriceMaxMinions = PlayerPrefs.GetInt("Kz" + i + "CPMM");
        t.UpLvlMaxMinions        = PlayerPrefs.GetInt("Kz" + i + "ULMM");
        t.MaxMinions             = PlayerPrefs.GetInt("Kz" + i + "MM"  );
        t.CurrentPriceHealth     = PlayerPrefs.GetInt("Kz" + i + "CPH" );
        t.CurrentUpHealth        = PlayerPrefs.GetInt("Kz" + i + "CUH" );
        t.UpLvlHealth            = PlayerPrefs.GetInt("Kz" + i + "ULH" );
        t.CurrentPriceDamage     = PlayerPrefs.GetInt("Kz" + i + "CPD" );
        t.CurrentUpDamage        = PlayerPrefs.GetInt("Kz" + i + "CUD" );
        t.UpLvlDamage            = PlayerPrefs.GetInt("Kz" + i + "ULD" );
        t.AngleInstant           = PlayerPrefs.GetInt("Kz" + i + "AI");
        int cnt = PlayerPrefs.GetInt("Kz" + i + "MCnt");
        t.minions = new List<Minion>();
        for(int j = 0; j < cnt; j++)
        {
            t.minions.Add(Minion.LoadObject(j, i, t));
        }
    }
}
