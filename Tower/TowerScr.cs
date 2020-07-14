using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScr : BaseObject
{

    public class Upgrades
    {
        public Dictionary<string, int[] > Upgrade;
        public TowerScr tower;
        public Upgrades(TowerScr tow)
        {
            tower = tow;
            Upgrade = new Dictionary<string, int[]>    //0 - Price resurs 0
            {                                          //1 - Price resurs 1
                { "Damage"       , new int[7] },       //2 - Price resurs 2
                { "Range"        , new int[7] },       //3 - Price resurs 3
                { "RotationSpeed", new int[7] },       //4 - Price resurs 4
                { "SpeedShot"    , new int[7] },       //5 - Price money
                { "RangeDamage"  , new int[7] },       //6 - Level upgrade
                { "SlowEffect"   , new int[7] },       //
                { "ScorchEffect" , new int[7] },       //
                { "PoisonEffect" , new int[7] },       //
                { "PoisonTime"   , new int[7] }        //
            };
        }

        public void SetUpgradeType(string type)
        {
            if (type == "Damage")
            {
                tower.Damage += tower.Damage * 0.4f;
            }
            else if (type == "PoisonEffect")
            {
                tower.PoisonEffect += tower.PoisonEffect * 0.1f;
            }
            else if (type == "Range")
            {
                tower.Range += tower.Range * 0.2f;
            }
            else if (type == "RangeDamage")
            {
                tower.RangeDamage += tower.RangeDamage * 0.2f;
            }
            else if (type == "RotationSpeed")
            {
                tower.RotationSpeed += tower.RotationSpeed * 0.3f;
            }
            else if (type == "SpeedShot")
            {
                tower.SpeedShot += tower.SpeedShot * 0.2f;
            }
            else if (type == "ScorchEffect")
            {
                tower.ScorchEffect += tower.ScorchEffect * 0.3f;
            }
            else if (type == "SlowEffect")
            {
                tower.SlowEffect += tower.SlowEffect * 0.3f;
            }
            else if (type == "PoisonTime")
            {
                tower.PoisonTime += tower.PoisonTime * 0.2f;
            }
            Buy(type);
        }

        public float ShowNextUpgrade(string type)
        {
            if (type == "Damage")
                return tower.Damage * 0.4f;
            else if (type == "PoisonEffect")
                return tower.PoisonEffect * 0.1f;
            else if (type == "Range")
                return tower.Range * 0.2f;
            else if (type == "RangeDamage")
                return tower.RangeDamage * 0.2f;
            else if (type == "RotationSpeed")
                return tower.RotationSpeed * 0.3f;
            else if (type == "SpeedShot")
                return tower.SpeedShot * 0.2f;
            else if (type == "ScorchEffect")
                return tower.ScorchEffect * 0.3f;
            else if (type == "SlowEffect")
                return tower.SlowEffect * 0.3f;
            else if (type == "PoisonTime")
                return tower.PoisonTime * 0.3f;
            return 0;
        }

        public float ShowCurrentValue(string type)
        {
            if (type == "Damage")
                return tower.Damage ;
            else if (type == "PoisonEffect")
                return tower.PoisonEffect ;
            else if (type == "Range")
                return tower.Range;
            else if (type == "RangeDamage")
                return tower.RangeDamage;
            else if (type == "RotationSpeed")
                return tower.RotationSpeed;
            else if (type == "SpeedShot")
                return tower.SpeedShot;
            else if (type == "ScorchEffect")
                return tower.ScorchEffect;
            else if (type == "SlowEffect")
                return tower.SlowEffect;
            else if (type == "PoisonTime")
                return tower.PoisonTime;
            return 0;
        }

        public bool MayBuy(string type)
        {
            if ((GlobalData.Money       - Upgrade[type][5] >= 0) &&
                (GlobalData.Minerals[0] - Upgrade[type][0] >= 0) &&
                (GlobalData.Minerals[1] - Upgrade[type][1] >= 0) &&
                (GlobalData.Minerals[2] - Upgrade[type][2] >= 0) &&
                (GlobalData.Minerals[3] - Upgrade[type][3] >= 0) &&
                (GlobalData.Minerals[4] - Upgrade[type][4] >= 0))
                return true;
            else
                return false;
        }

        public void Buy(string type)
        {
            GlobalData.Money       -= Upgrade[type][5];
            GlobalData.Minerals[0] -= Upgrade[type][0];
            GlobalData.Minerals[1] -= Upgrade[type][1];
            GlobalData.Minerals[2] -= Upgrade[type][2];
            GlobalData.Minerals[3] -= Upgrade[type][3];
            GlobalData.Minerals[4] -= Upgrade[type][4];
            Upgrade[type][6]--;
            tower.SumPrice += Upgrade[type][5];
            Upgrade[type][5] += 2 * (int)Mathf.Sqrt(Upgrade[type][5]);
        }
    }

    //Tower characteristics 
    public float rotationspeed;
    public float RotationSpeed
    {
        get
        {
            return rotationspeed;
        }
        set
        {
            if (value < 0)
                rotationspeed = 0;
            else if (value > 360)
                rotationspeed = 360;
            else
                rotationspeed = value;
        }
    }
    public float Range;
    public float RangeDamage;
    public float SlowEffect;
    public float ScorchEffect;
    public float PoisonEffect;
    public float PoisonTime;
    public float Damage;
    protected float speedshot;
    public float SpeedShot
    {
        get
        {
            return speedshot;
        }
        set
        {
            if (value <= 10f)
                speedshot = value;
            else
                speedshot = 10f;
        }
    }
    public float SpeedTurn;

    public int X;
    public int Y;
    public int SellPrice
    {
        get
        {
            return (int)((SumPrice + DefaultPrice) * 0.7f);
        }
    }
    public int TargetPriority;
    public static string TowerName = "Default Tower";
    public static string About = "About";
    public static byte Tag { get; set; }
    public static int DefaultPrice;
    public static int DefaultPriceSapphir;
    public static int DefaultPriceRubin;
    public static int DefaultPriceIron;
    public static int DefaultPriceOil;
    public static int DefaultPriceCoal;

    public string TypeTower;
    public Transform rootBullet;

    //Tower Upgrades
    public  Upgrades UpgradeList;
    public int UpPriceDamage;
    public int UpPriceRange;
    public int UpPriceRangeDamage;
    public int UpPriceRotationSpeed;
    public int UpPriceSlowEffect;
    public int UpPriceScorchEffect;
    public int UpPricePoisonEffect; 
    public int UpPricePoisonTime;
    public int UpPriceSpeedShot;
    public int SumPrice;

    //Target var
    protected EnemyScr TempTarget;

    //Tower elements
    protected Transform Stvol;
    public GameObject towerrange;

    public bool IsHideRange
    {
        get
        {
            return towerrange.activeSelf;
        }
        set
        {
            if(value)
            {
                if(ShopMove.Meta != null && ShopMove.Meta.type == "Tower")
                    ShopMove.Meta.OffLastObject();
                ShopMove.Meta.type         = "Tower";
                ShopMove.Meta.RunInfo      = Info;
                ShopMove.Meta.RunSell      = Sell;
                ShopMove.Meta.OffLastObject = OffTower;
                ShopMove.Meta.gameObject   = gameObject;
            }
            MenuTowerUpgrade.Close();
            towerrange.transform.localScale = new Vector3(Range * 0.5f, Range * 0.5f, 1);
            towerrange.SetActive(value);
        }
    }

    protected float DefaultRotationSpeed;
    protected float DefaultRange;
    protected float DefaultRangeDamage;
    protected float DefaultSlowEffect;
    protected float DefaultScorchEffect;
    protected float DefaultPoisonEffect;
    protected float DefaultPoisonTime;
    protected float DefaultDamage;
    protected float SpeedShotStartTime = 0;
    protected float TurnStartTime = 0;
    protected bool SearchEnemy;
    protected float del;
    void Update()
    {
        del = Time.time - TurnStartTime;
        if(SearchEnemy)
        {
            int BeginX = Mathf.CeilToInt((X - Range < 0)  ? 0  : X - Range);
            int BeginY = Mathf.CeilToInt((Y - Range < 0)  ? 0  : Y - Range);
            int EndX   = Mathf.CeilToInt((X + Range > GlobalData.LevelSize) ? GlobalData.LevelSize : X + Range);
            int EndY   = Mathf.CeilToInt((Y + Range > GlobalData.LevelSize) ? GlobalData.LevelSize : Y + Range);
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
                            else if(TargetPriority == 2)
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
                Turn(TempTarget);
            else
            {
                SearchEnemy = true;
                TempTarget  = null;
            }  
        }
        TurnStartTime = Time.time;
    }

    protected virtual void Turn(EnemyScr target)
    {
        
            Vector3 diff = target.transform.position - Stvol.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            if (rot_z < 0)
                rot_z += 360;
     
        if (Mathf.Abs(Stvol.rotation.eulerAngles.y - rot_z) <= RotationSpeed * del)
            {
                Stvol.rotation = Quaternion.Euler(-90f, rot_z, 0);
                if (Time.time - SpeedShotStartTime >= 1f / SpeedShot)
                {
                    Shot(target);
                    SpeedShotStartTime = Time.time;
                }
            }
            else
            {
                float tmpz = Stvol.rotation.eulerAngles.y;
                if (tmpz < rot_z) tmpz += 360;
            
                if (tmpz - rot_z > 180)
                    Stvol.rotation = Quaternion.Euler(-90f, Stvol.rotation.eulerAngles.y + RotationSpeed * del, 0f);
                else
                    Stvol.rotation = Quaternion.Euler(-90f, Stvol.rotation.eulerAngles.y - RotationSpeed * del, 0f);    
            }
            TurnStartTime = Time.time;
    }

    protected virtual void Shot(EnemyScr enemy)
    {
        float tmpz = Stvol.rotation.eulerAngles.y * Mathf.Deg2Rad;
        BulletScr bull = Instantiate(Resources.Load("Object/GAME/Towers/Bullet") as GameObject, Stvol.GetChild(0).position, new Quaternion()).GetComponent<BulletScr>();
        bull.Target = enemy;
        bull.TempTarget = enemy.transform.position;
        bull.Damage = Damage;
    }

    void Start()
    {
        transform.SetParent(GlobalData.GameTrans);
        Stvol = transform.GetChild(1);
        TowerName = "Default Tower";
        TypeTower = "Default Tower";

        towerrange = transform.Find("Range").gameObject;


        //Tower characteristic
        UpgradeList = new Upgrades(this);
        UpgradeList.Upgrade["Damage"][0] = 0;   UpgradeList.Upgrade["SpeedShot"][0] = 2;   UpgradeList.Upgrade["RotationSpeed"][0] = 6;   UpgradeList.Upgrade["Range"][0] = 2;
        UpgradeList.Upgrade["Damage"][1] = 0;   UpgradeList.Upgrade["SpeedShot"][1] = 1;   UpgradeList.Upgrade["RotationSpeed"][1] = 1;   UpgradeList.Upgrade["Range"][1] = 7;
        UpgradeList.Upgrade["Damage"][2] = 0;   UpgradeList.Upgrade["SpeedShot"][2] = 0;   UpgradeList.Upgrade["RotationSpeed"][2] = 0;   UpgradeList.Upgrade["Range"][2] = 0;
        UpgradeList.Upgrade["Damage"][3] = 0;   UpgradeList.Upgrade["SpeedShot"][3] = 0;   UpgradeList.Upgrade["RotationSpeed"][3] = 0;   UpgradeList.Upgrade["Range"][3] = 0;
        UpgradeList.Upgrade["Damage"][4] = 0;   UpgradeList.Upgrade["SpeedShot"][4] = 0;   UpgradeList.Upgrade["RotationSpeed"][4] = 0;   UpgradeList.Upgrade["Range"][4] = 0;
        UpgradeList.Upgrade["Damage"][5] = 228; UpgradeList.Upgrade["SpeedShot"][5] = 228; UpgradeList.Upgrade["RotationSpeed"][5] = 228; UpgradeList.Upgrade["Range"][5] = 228;
        UpgradeList.Upgrade["Damage"][6] = 5;   UpgradeList.Upgrade["SpeedShot"][6] = 5;   UpgradeList.Upgrade["RotationSpeed"][6] = 5;   UpgradeList.Upgrade["Range"][6] = 5;  

        X = (int)Mathf.Round(transform.position.x);
        Y = (int)Mathf.Round(transform.position.z);
        Range         = DefaultRange            = 5;
        SpeedShot                               = 1.5f;
        RotationSpeed = DefaultRotationSpeed    = 80;
        Damage        = DefaultDamage           = 80;
        TargetPriority = 1;
        DefaultPrice = 100;
        DefaultPriceIron = 0;
        DefaultPriceCoal = 0;
        DefaultPriceRubin = 0;
        DefaultPriceSapphir = 0;
        DefaultPriceOil = 0;

        //Set Upgrades
        UpPriceDamage        = (int)(DefaultPrice * 0.38f);
        UpPriceRange         = (int)(DefaultPrice * 0.35f);
        UpPriceRotationSpeed = (int)(DefaultPrice * 0.25f);
        UpPriceSpeedShot     = (int)(DefaultPrice * 0.38f);

        SpeedShotStartTime = Time.time;

        TurnStartTime      = Time.time;
        SpeedTurn = 0.1f;
        SearchEnemy = true;
        TurnStartTime = Time.time;
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
            MenuTowerUpgrade.SetCharacteristics();
            ShopMove.SetSellPanel(true);
        }
        if (ShopMove.TempPlace != null)
            ShopMove.TempPlace.GetComponent<PlaceBuildScr>().Select = false;
    }

    /// <summary>
    /// Проверяет заданую точку и кол-во денег, на возможность постановки башни. Параметр text возвращает сообщение с результатом
    /// </summary>
    public static GameObject MayBuild(ref string text, GameObject go)
    {
        GameObject t = null;
        if((go != null) && (ShopMove.Meta.type == "PlaceBuild"))
        {
            if(MayBuy())
            {
                Buy();
                t = Instantiate(Resources.Load("Object/GAME/Towers/Tower1") as GameObject, go.transform.position, new Quaternion());
                Destroy(go); ShopClick.Cancel();
                text = "Установлено";
            }
            else
                text = "Недостаточно ресурсов";
        }
        else
            text = "Выберите место установки";
        return t;
    }

    public static bool MayBuy()
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

    public static void Buy()
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
    public virtual GameObject Sell()
    {
        GlobalData.Money += SellPrice;
        Destroy(gameObject);
        ShopMove.Meta.type = null;
        GameObject t = Instantiate(Resources.Load("Object/GAME/Enviroment/PlaceBuild") as GameObject, transform.position, new Quaternion());
        UpgradeClick.Close();
        return t;
    }
    public virtual void OffTower()
    {
        towerrange.SetActive(false);
    }

    public virtual void RefreshRange()
    {
        towerrange.transform.localScale = new Vector3(Range * 0.5f, Range * 0.5f, 1);
    }
    public static void Info()
    {
        InfoScr.SetInfo(TowerName, About);
    }

    public void SaveObject(int i)
    {
        PlayerPrefs.SetString("Tw" + i + "T",   TypeTower);
        PlayerPrefs.SetFloat("Tw" + i + "Px",   transform.position.x);
        PlayerPrefs.SetFloat("Tw" + i + "Py",   transform.position.y);
        PlayerPrefs.SetFloat("Tw" + i + "Pz",   transform.position.z);
        PlayerPrefs.SetFloat("Tw" + i + "Rx",   transform.eulerAngles.x);
        PlayerPrefs.SetFloat("Tw" + i + "Ry",   transform.eulerAngles.y);
        PlayerPrefs.SetFloat("Tw" + i + "Rz",   transform.eulerAngles.z);
        PlayerPrefs.SetFloat("Tw" + i + "Rs",   rotationspeed);
        PlayerPrefs.SetFloat("Tw" + i + "Rn",   Range);
        PlayerPrefs.SetFloat("Tw" + i + "RD",   RangeDamage);
        PlayerPrefs.SetFloat("Tw" + i + "SlE",  SlowEffect);
        PlayerPrefs.SetFloat("Tw" + i + "ScE",  ScorchEffect);
        PlayerPrefs.SetFloat("Tw" + i + "PE",   PoisonEffect);
        PlayerPrefs.SetFloat("Tw" + i + "PT",   PoisonTime);
        PlayerPrefs.SetFloat("Tw" + i + "Dm",   Damage);
        PlayerPrefs.SetFloat("Tw" + i + "SS",   speedshot);
        PlayerPrefs.SetFloat("Tw" + i + "ST",   SpeedTurn);
        PlayerPrefs.SetFloat("Tw" + i + "SSST", Time.time - SpeedShotStartTime);
        PlayerPrefs.SetInt("Tw" + i + "TP",     TargetPriority);
        PlayerPrefs.SetInt("Tw" + i + "UPD",    UpPriceDamage);
        PlayerPrefs.SetInt("Tw" + i + "UPR",    UpPriceRange);
        PlayerPrefs.SetInt("Tw" + i + "UPRD",   UpPriceRangeDamage);
        PlayerPrefs.SetInt("Tw" + i + "UPRS",   UpPriceRotationSpeed);
        PlayerPrefs.SetInt("Tw" + i + "UPSlE",  UpPriceSlowEffect);
        PlayerPrefs.SetInt("Tw" + i + "UPScE",  UpPriceScorchEffect);
        PlayerPrefs.SetInt("Tw" + i + "UPPE",   UpPricePoisonEffect);
        PlayerPrefs.SetInt("Tw" + i + "UPPT",   UpPricePoisonTime);
        PlayerPrefs.SetInt("Tw" + i + "UPSS",   UpPriceSpeedShot);
        PlayerPrefs.SetInt("Tw" + i + "SmP",    SumPrice);
        PlayerPrefs.SetFloat("Tw" + i + "DRS",  DefaultRotationSpeed);
        PlayerPrefs.SetFloat("Tw" + i + "DR",   DefaultRange);
        PlayerPrefs.SetFloat("Tw" + i + "DRD",  DefaultRangeDamage);
        PlayerPrefs.SetFloat("Tw" + i + "DSlE", DefaultSlowEffect);
        PlayerPrefs.SetFloat("Tw" + i + "DScE", DefaultScorchEffect);
        PlayerPrefs.SetFloat("Tw" + i + "DPE",  DefaultPoisonEffect);
        PlayerPrefs.SetFloat("Tw" + i + "DPT",  DefaultPoisonTime);
        PlayerPrefs.SetFloat("Tw" + i + "DD",   DefaultDamage);

    }

    public static void LoadObject(int i)
    {
        string typ = PlayerPrefs.GetString("Tw" + i + "T");
        TowerScr t = null;
        if(typ == TowerScr.TowerName)
            t = Instantiate(Resources.Load("Object/GAME/Towers/Tower1")                 as GameObject, new Vector3(), new Quaternion()).GetComponent<TowerScr>();
        else if (typ == TowerMageScr.TowerName)                                                                                      
            t = Instantiate(Resources.Load("Object/GAME/Towers/FrostMage/TowerMage")    as GameObject, new Vector3(), new Quaternion()).GetComponent<TowerScr>();
        else if (typ == TowerFireScr.TowerName)                                                                                     
            t = Instantiate(Resources.Load("Object/GAME/Towers/FireMage/TowerFireMage") as GameObject, new Vector3(), new Quaternion()).GetComponent<TowerScr>();
        else if (typ == TowerCoreScr.TowerName)                                                                                     
            t = Instantiate(Resources.Load("Object/GAME/Towers/Core/TowerCore")         as GameObject, new Vector3(), new Quaternion()).GetComponent<TowerScr>();
        else if (typ == TowerPoisonScr.TowerName)                                                                                   
            t = Instantiate(Resources.Load("Object/GAME/Towers/Poison/Tow")             as GameObject, new Vector3(), new Quaternion()).GetComponent<TowerScr>();

        t.transform.position    = new Vector3(PlayerPrefs.GetFloat("Tw" + i + "Px"), PlayerPrefs.GetFloat("Tw" + i + "Py"), PlayerPrefs.GetFloat("Tw" + i + "Pz"));
        t.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("Tw" + i + "Rx"), PlayerPrefs.GetFloat("Tw" + i + "Ry"), PlayerPrefs.GetFloat("Tw" + i + "Rz"));
        t.rotationspeed        = PlayerPrefs.GetFloat("Tw" + i + "Rs");
        t.Range                = PlayerPrefs.GetFloat("Tw" + i + "Rn");
        t.RangeDamage          = PlayerPrefs.GetFloat("Tw" + i + "RD");
        t.SlowEffect           = PlayerPrefs.GetFloat("Tw" + i + "SlE");
        t.ScorchEffect         = PlayerPrefs.GetFloat("Tw" + i + "ScE");
        t.PoisonEffect         = PlayerPrefs.GetFloat("Tw" + i + "PE");
        t.PoisonTime           = PlayerPrefs.GetFloat("Tw" + i + "PT");
        t.Damage               = PlayerPrefs.GetFloat("Tw" + i + "Dm");
        t.speedshot            = PlayerPrefs.GetFloat("Tw" + i + "SS");
        t.SpeedTurn            = PlayerPrefs.GetFloat("Tw" + i + "ST");
        t.SpeedShotStartTime   = Time.time + PlayerPrefs.GetFloat("Tw" + i + "SSST");
        t.TargetPriority       = PlayerPrefs.GetInt("Tw" + i + "TP");
        t.UpPriceDamage        = PlayerPrefs.GetInt("Tw" + i + "UPD");
        t.UpPriceRange         = PlayerPrefs.GetInt("Tw" + i + "UPR");
        t.UpPriceRangeDamage   = PlayerPrefs.GetInt("Tw" + i + "UPRD");
        t.UpPriceRotationSpeed = PlayerPrefs.GetInt("Tw" + i + "UPRS");
        t.UpPriceSlowEffect    = PlayerPrefs.GetInt("Tw" + i + "UPSlE");
        t.UpPriceScorchEffect  = PlayerPrefs.GetInt("Tw" + i + "UPScE");
        t.UpPricePoisonEffect  = PlayerPrefs.GetInt("Tw" + i + "UPPE");
        t.UpPricePoisonTime    = PlayerPrefs.GetInt("Tw" + i + "UPPT");
        t.UpPriceSpeedShot     = PlayerPrefs.GetInt("Tw" + i + "UPSS");
        t.SumPrice             = PlayerPrefs.GetInt("Tw" + i + "SmP");  
        t.DefaultRotationSpeed = PlayerPrefs.GetFloat("Tw" + i + "DRS");
        t.DefaultRange         = PlayerPrefs.GetFloat("Tw" + i + "DR");
        t.DefaultRangeDamage   = PlayerPrefs.GetFloat("Tw" + i + "DRD");
        t.DefaultSlowEffect    = PlayerPrefs.GetFloat("Tw" + i + "DSlE");
        t.DefaultScorchEffect  = PlayerPrefs.GetFloat("Tw" + i + "DScE");
        t.DefaultPoisonEffect  = PlayerPrefs.GetFloat("Tw" + i + "DPE");
        t.DefaultPoisonTime    = PlayerPrefs.GetFloat("Tw" + i + "DPT");
        t.DefaultDamage        = PlayerPrefs.GetFloat("Tw" + i + "DD");

        t.UpgradeList = new Upgrades(t);
    }
}
