using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMageScr : TowerScr
{
    public new static string TowerName = "Magic Tower(Frost)";
    public new static string About = "About";
    public new static byte Tag { get; set; }
    public new static int DefaultPrice = 100;
    public new static int DefaultPriceSapphir = 3;
    public new static int DefaultPriceRubin   = 0;
    public new static int DefaultPriceIron    = 2;
    public new static int DefaultPriceOil     = 0;
    public new static int DefaultPriceCoal    = 0;

    void Start()
    {
        transform.SetParent(GlobalData.GameTrans);
        towerrange = transform.Find("Range").gameObject;
        TowerName = "Magic Tower(Frost)"; TypeTower = "Magic Tower(Frost)";
        //Tower characteristic
        UpgradeList = new Upgrades(this);
        X = (int)Mathf.Round(transform.position.x); Y = (int)Mathf.Round(transform.position.z);
        //Static characteristic
        SpeedShot = 0.25f;
        DefaultPrice = 100;
        DefaultPriceIron = 2;
        DefaultPriceCoal = 0;
        DefaultPriceRubin = 0;
        DefaultPriceSapphir = 3;
        DefaultPriceOil = 0;
        //Dynamic characteristic
        Damage = DefaultDamage = 100;
        Range = DefaultRange = 3;
        RangeDamage = DefaultRangeDamage = 3;
        SlowEffect = DefaultSlowEffect = 3;
        TargetPriority = 1;



        //Set Upgrades
        UpPriceDamage      = (int)(DefaultPrice * 0.38f);
        UpPriceRange       = (int)(DefaultPrice * 0.35f);
        UpPriceSlowEffect  = (int)(DefaultPrice * 0.38f);
        UpPriceRangeDamage = (int)(DefaultPrice * 0.4f);

        SpeedShotStartTime = Time.time;

        //zalupa
        SearchEnemy = true;
    }

    void Update()
    {
        if (SearchEnemy)
        {
            int BeginX = Mathf.CeilToInt((X - Range < 0) ? 0 : X - Range);
            int BeginY = Mathf.CeilToInt((Y - Range < 0) ? 0 : Y - Range);
            int EndX = Mathf.CeilToInt((X + Range > GlobalData.LevelSize) ? GlobalData.LevelSize : X + Range);
            int EndY = Mathf.CeilToInt((Y + Range > GlobalData.LevelSize) ? GlobalData.LevelSize : Y + Range);
            bool flag = false;
            for (int i = BeginX; i < EndX; i++)
                for (int j = BeginY; j < EndY; j++)
                {
                    foreach (EnemyScr enemy in GlobalData.EnemyMatrix[j, i])
                    {
                        if (enemy != null && Vector3.Magnitude(enemy.transform.position - transform.position) <= Range)
                        {
                            
                            if (flag == false)
                            {
                                TempTarget = enemy;
                                flag = true;
                            }
                            if (TargetPriority == 1)
                            {
                                if (enemy.Health > TempTarget.Health)
                                {
                                    TempTarget = enemy;
                                }
                            }
                            else if (TargetPriority == 2)
                            {
                                if (enemy.Health < TempTarget.Health)
                                {
                                    TempTarget = enemy;
                                }
                            }
                        }
                    }
                }
            if (TempTarget != null) SearchEnemy = false;
        }
        else
        {
            if (TempTarget != null && Vector3.Magnitude(TempTarget.transform.position - transform.position) <= Range)
                Shot(TempTarget);
            else
            {
                SearchEnemy = true;
                TempTarget = null;
            }
        }
    }

    protected override void Shot(EnemyScr enemy)
    {
        if (Time.time - SpeedShotStartTime >= 1f / SpeedShot)
        {
            MageBullet bull = Instantiate(Resources.Load("Object/GAME/Towers/FrostMage/BulletMage") as GameObject, new Vector3(X, 2f, Y), new Quaternion()).GetComponent<MageBullet>();
            bull.Target = enemy;
            bull.Damage = Damage;
            bull.TempTarget = enemy.transform.position;
            bull.SlowDown = SlowEffect;
            bull.RangeDamage = RangeDamage;
            SpeedShotStartTime = Time.time;
        }
    }



    public override void EventMouseDown()
    {
   
    }

    public override void EventMouseUp()
    {
        if (IsHideRange)
        {
            IsHideRange = false;
            ShopMove.IsHide = true;
        }
        else
        {
            IsHideRange = true;
            ShopClick.Cancel();
            UpgradeScr.SetCharacteristics();
            ShopMove.SetSellPanel(true);
        }
        if (ShopMove.TempPlace != null)
            ShopMove.TempPlace.GetComponent<PlaceBuildScr>().Select = false;
    }

    /// <summary>
    /// Проверяет заданую точку и кол-во денег, на возможность постановки башни. Параметр text возвращает сообщение с результатом
    /// </summary>
    public new static void MayBuild(ref string text, GameObject go)
    {
        if ((go != null) && (ShopMove.Meta.type == "PlaceBuild"))
        {
            if (MayBuy())
            {
                Buy();
                Instantiate(Resources.Load("Object/GAME/Towers/FrostMage/TowerMage") as GameObject, go.transform.position, Quaternion.Euler(-90, 0, 0));
                Destroy(go); ShopClick.Cancel();
                text = "Установлено";
            }
            else
                text = "Недостаточно ресурсов";
        }
        else
            text = "Выберите место установки";
    }

    public new static bool MayBuy()
    {
        if ((GlobalData.Money - DefaultPrice >= 0) &&
            (GlobalData.Minerals[0] - DefaultPriceIron >= 0) &&
            (GlobalData.Minerals[1] - DefaultPriceCoal >= 0) &&
            (GlobalData.Minerals[2] - DefaultPriceRubin >= 0) &&
            (GlobalData.Minerals[3] - DefaultPriceSapphir >= 0) &&
            (GlobalData.Minerals[4] - DefaultPriceOil >= 0))
            return true;
        else
            return false;
    }

    public new static void Buy()
    {
        GlobalData.Money -= DefaultPrice;
        GlobalData.Minerals[0] -= DefaultPriceIron;
        GlobalData.Minerals[1] -= DefaultPriceCoal;
        GlobalData.Minerals[2] -= DefaultPriceRubin;
        GlobalData.Minerals[3] -= DefaultPriceSapphir;
        GlobalData.Minerals[4] -= DefaultPriceOil;
    }

    /// <summary>
    /// Метод продажи башни
    /// </summary>
    public override GameObject Sell()
    {
        GlobalData.Money += SellPrice;
        Destroy(gameObject);
        ShopMove.Meta.type = null;
        GameObject t = Instantiate(Resources.Load("Object/GAME/Enviroment/PlaceBuild") as GameObject, transform.position, new Quaternion());
        UpgradeClick.Close();
        return t;
    }
    public override void OffTower()
    {
        towerrange.SetActive(false);
    }

    public override void RefreshRange()
    {
        towerrange.transform.localScale = new Vector3(Range * 0.5f, Range * 0.5f, 1);
    }
    public new static void Info()
    {
        InfoScr.SetInfo(TowerName, About);
    }
}
