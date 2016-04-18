using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Globalization;
using System;

public class ReadJson : MonoBehaviour {
    public Text[] montagStunden = new Text[12];
    public Text[] dienstagStunden = new Text[12];
    public Text[] mittwochStunden = new Text[12];
    public Text[] donnerstagStunden = new Text[12];
    public Text[] freitagStunden = new Text[12];
    public Text[][] wochentage = new Text[5][];
    public Transform vertretungsreihePrefab;
    private string jsonString;
    private JsonData itemData;
    private string url = "http://mns.topsch.net/vapp/sp_neu.php?date=";
    private string theDate;
    private DateTime thisDay;
    private DateTime dt;
    private CultureInfo ci;
    
	void Start ()
    {
        GetWoche();
        StartCoroutine(GetVertretung());
    }

    IEnumerator GetVertretung()
    {
        Debug.Log("test");
        /*
        for (int i = 0; i < 5; i++)
        {
            i = int.Parse(itemData[i][6].ToString());
            switch (i)
            {
                case 0:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    break;
                case 1:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday);
                    break;
                case 2:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday);
                    break;
                case 3:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Thursday);
                    break;
                case 4:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
                    break;
                default:
                    break;
            }
        }*/

        theDate = dt.ToString("yyyy-MM-dd");
        url += "2016-01-12";
        WWW www = new WWW(url);
        yield return www;
        jsonString = www.text;
        itemData = JsonMapper.ToObject(jsonString);
        Debug.Log(itemData.Count);
        for (int i = 0; i < itemData.Count; i++) {
            if (itemData[i][2] != null)
            {
                if (itemData[i][1] != null)
                {
                    // Vertretung
                    vertretungsreihePrefab.GetComponentInChildren<Text>().text = "";
                    Debug.Log("Vertretung" + i.ToString());
                }
                else if (itemData[i][1] == null)
                {
                    //Entfall
                    Text[] o = new Text[3];
                    o = vertretungsreihePrefab.GetComponentsInChildren<Text>();
                    o[0].text = itemData[i][4].ToString();
                    Debug.Log(o[0] + "test");
                    Debug.Log("Entfall" + i.ToString());
                }
            }
            else {
                Debug.Log("Nothing" + i.ToString());
            }
        }
    }

    IEnumerator GetWoche()
    {
        wochentage[0] = montagStunden;
        wochentage[1] = dienstagStunden;
        wochentage[2] = mittwochStunden;
        wochentage[3] = donnerstagStunden;
        wochentage[4] = freitagStunden;

        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    break;
                case 1:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday);
                    break;
                case 2:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday);
                    break;
                case 3:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Thursday);
                    break;
                case 4:
                    dt = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
                    break;
                default:
                    break;
            }

            theDate = dt.ToString("yyyy-MM-dd");
            url += theDate;
            WWW www = new WWW(url);
            yield return www;
            jsonString = www.text;
            itemData = JsonMapper.ToObject(jsonString);
            GetStundenplanDay();
            url = "http://mns.topsch.net/vapp/sp_neu.php?date=";
        }
    }

    void GetStundenplanDay() {
        for (int i = 0; i < itemData.Count; i++) {
            int stundenNr = int.Parse(itemData[i][5].ToString()) - 1;
            int tagNr = int.Parse(itemData[i][6].ToString()) - 1;
            wochentage[tagNr][stundenNr].text = itemData[i][8].ToString();
        }
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = dt.DayOfWeek - startOfWeek;
        if (diff < 0)
        {
            diff += 7;
        }

        return dt.AddDays(-1 * diff).Date;
    }
}