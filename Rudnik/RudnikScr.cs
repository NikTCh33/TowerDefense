using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class RudnikScr : BaseObject
    {
        public VagonetkaScr vagonetka;
        public GameObject ustanovka;
        public GameObject Select;
        private static int DefaultPrice = 300;
        void Start()
        {
            transform.SetParent(GlobalData.GameTrans);
        }


        public override void EventMouseDown()
        {
        }

        public override void EventMouseUp()
        {
            ShopClick.Cancel();
            if (ShopMove.Meta.type != null)
                ShopMove.Meta.OffLastObject();
            ShopMove.Meta.type = "Rudnik";
            ShopMove.Meta.RunInfo = Info;
            ShopMove.Meta.RunSell = Sell;
            ShopMove.Meta.OffLastObject = Off;
            ShopMove.Meta.gameObject = gameObject;

            Select.SetActive(true);
            if (!ustanovka.activeSelf)
            {
                ShopMove.SetActPanel(true);
                ShopMove.Selecteditem = 10;
            }
            else
            {
                ShopMove.SetSellPanel(true);
                MenuRudnik.ShowRudnik(vagonetka);                
            }
        }

        public void Off()
        {
            ShopMove.Selecteditem = 0;
            MenuRudnik.Close();
            Select.SetActive(false);
        }

        public GameObject Sell()
        {
            GlobalData.Money += (int)(DefaultPrice * 0.7f);
            ustanovka.SetActive(false);
            return null;
        }

        public static void MayBuild(ref string text, GameObject go)
        {
            if (GlobalData.Money - DefaultPrice >= 0)
            {
                GlobalData.Money -= DefaultPrice;
                RudnikScr r = go.GetComponent<RudnikScr>();
                ShopClick.Cancel();
                r.ustanovka.SetActive(true);
                r.EventMouseUp();
                text = "Установлено";
            }
            else
                text = "Недостаточно денег";
        }

        public static void Info()
        {
            string ObjName = "Рудник",
                   About = "Здесь можно добыть\nполезные ископаемые\nдля постройки новых\nбашен";
            MenuRudnik.Close();
            InfoScr.SetInfo(ObjName, About);
        }

        public void SaveObject(int i)
        {
            PlayerPrefs.SetFloat("Rd" + i + "Px", transform.position.x);
            PlayerPrefs.SetFloat("Rd" + i + "Py", transform.position.y);
            PlayerPrefs.SetFloat("Rd" + i + "Pz", transform.position.z);
            PlayerPrefs.SetFloat("Rd" + i + "Rx", transform.eulerAngles.x);
            PlayerPrefs.SetFloat("Rd" + i + "Ry", transform.eulerAngles.y);
            PlayerPrefs.SetFloat("Rd" + i + "Rz", transform.eulerAngles.z);
            PlayerPrefs.SetInt("Rd" + i + "Us", (ustanovka.activeSelf) ? 1 : 0);
            if(ustanovka.activeSelf)
            {
                PlayerPrefs.SetFloat("Rd" + i + "VPx", vagonetka.transform.position.x);
                PlayerPrefs.SetFloat("Rd" + i + "VPy", vagonetka.transform.position.y);
                PlayerPrefs.SetFloat("Rd" + i + "VPz", vagonetka.transform.position.z);
                PlayerPrefs.SetFloat("Rd" + i + "ST", Time.time - vagonetka.StartTime);
                PlayerPrefs.SetInt("Rd" + i + "F", vagonetka.Flag);
                PlayerPrefs.SetInt("Rd" + i + "SM", vagonetka.SelectedMineral);
                PlayerPrefs.SetInt("Rd" + i + "PMC", vagonetka.progresMinerals.Length);
                for (int j = 0; j < vagonetka.progresMinerals.Length; j++)
                    PlayerPrefs.SetFloat("Rd" + i + "PM" + j, vagonetka.progresMinerals[j]);
            }
        }

        public static GameObject LoadObject(int i)
        {
            RudnikScr t = Instantiate(Resources.Load("Object/GAME/Enviroment/Rudnik") as GameObject, new Vector3(), new Quaternion()).GetComponent<RudnikScr>();
            t.transform.position           = new Vector3(PlayerPrefs.GetFloat("Rd" + i + "Px"), PlayerPrefs.GetFloat("Rd" + i + "Py"), PlayerPrefs.GetFloat("Rd" + i + "Pz"));
            t.transform.eulerAngles        = new Vector3(PlayerPrefs.GetFloat("Rd" + i + "Rx"), PlayerPrefs.GetFloat("Rd" + i + "Ry"), PlayerPrefs.GetFloat("Rd" + i + "Rz"));
            t.ustanovka.SetActive((PlayerPrefs.GetInt("Rd" + i + "Us") == 1) ? true : false);
            if(t.ustanovka.activeSelf)
            {
                t.vagonetka.transform.position = new Vector3(PlayerPrefs.GetFloat("Rd" + i + "VPx"), PlayerPrefs.GetFloat("Rd" + i + "VPy"), PlayerPrefs.GetFloat("Rd" + i + "VPz"));
                t.vagonetka.StartTime = Time.time + PlayerPrefs.GetFloat("Rd" + i + "ST");
                t.vagonetka.Flag = PlayerPrefs.GetInt("Rd" + i + "F");
                t.vagonetka.SelectedMineral = PlayerPrefs.GetInt("Rd" + i + "SM");
                for (int j = 0; j < t.vagonetka.progresMinerals.Length; j++)
                    t.vagonetka.progresMinerals[j] = PlayerPrefs.GetInt("Rd" + i + "PM" + j);
            }
            return t.gameObject;
        }
    }
}