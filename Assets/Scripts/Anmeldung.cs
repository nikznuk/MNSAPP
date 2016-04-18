using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Anmeldung : MonoBehaviour {

    public Toggle toggle;
    public MenuManager canvas;
    public Menu anmeldung;
    public Menu menu;
    string benutzerHV;
    string passwortHV;
    string errechneterHash;

    void Start ()
    {
        if (PlayerPrefs.GetInt ("Soll angemeldet bleiben") == 0 || PlayerPrefs.HasKey ("Soll angemeldet bleiben") || PlayerPrefs.HasKey("Benutzername") || PlayerPrefs.HasKey("Passwort Hash"))
        {
            Debug.Log("not");
            canvas.ShowMenu(anmeldung);
        }
        else
        {
            Debug.Log("an");
            Anmelden(PlayerPrefs.GetString("Benutzername"), PlayerPrefs.GetString("Passwort Hash"));
        }
    }

    void Anmelden(string benutzername, string passwortHash)
    {
        // errechneterHash = GetHash(passwortHV);
        // Serveranfrage 

        if (PlayerPrefs.GetInt("Soll angemeldet bleiben") == 1 /* && wenn Hash OK*/)
        {
            SpeichereBenutzerdaten(benutzerHV, errechneterHash);
        }

        else if (true /*wenn Hash OK*/)
        {
            canvas.ShowMenu(menu);
        }
    }

    void SpeichereBenutzerdaten(string benutzername, string hash)
    {
        PlayerPrefs.SetString("Benutzername", benutzername);
        PlayerPrefs.SetString("Passwort Hash", hash);
    }

    string GetHash(string passwort)
    {
        // Hash erstellen sha 512
        passwortHV = "";
        return null;
    }

    public void SpeichereBenutzernamen (string name)
    {
        benutzerHV = name;
    }

    public void SpeicherePasswort(string name)
    {
        passwortHV = name;
    }

    public void ChangeAngemeldetBleiben(int value)
    {
        PlayerPrefs.SetInt("Soll angemeldet bleiben", value);
        Debug.Log(PlayerPrefs.GetInt ("Soll angemeldet bleiben"));
    }
}
