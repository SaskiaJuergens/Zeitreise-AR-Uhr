using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TelefonWechselScript : MonoBehaviour
{
    public GameObject telefonPrefab; // Das Telefon-Prefab
    public Transform telefonContainer; // Hier könntest du einen leeren GameObject-Container für die Telefone verwenden
    public Button plusButton;
    public Button minusButton;

    private int aktuellesTelefonIndex = 0;
    private GameObject[] telefone;

    void Start()
    {
        // Initialisiere die Telefone
        GameObject telefonInstance = Instantiate(telefonPrefab.gameObject, transform);
        Debug.Log(telefonInstance.transform.childCount);

        telefone = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("inizalisiere Telefon" + i);
            telefone[i] = telefonInstance.transform.GetChild(i).gameObject;
            telefone[i].SetActive(false);
        }

        Firestore.Instance.TelRef = this;

        // Zeige das erste Telefon an
        ZeigeAktuellesTelefon();
        //setTelefon(0);


        // Setze die Button-Handler
        plusButton = GameObject.FindGameObjectWithTag("plusButton").GetComponent<Button>();
        minusButton = GameObject.FindGameObjectWithTag("minusButton").GetComponent<Button>();

        Debug.Log(plusButton);
        Debug.Log(plusButton.onClick);

        plusButton.onClick.AddListener(WechsleTelefonPlus);
        minusButton.onClick.AddListener(WechsleTelefonMinus);

    }

    public void setTelefon(int mm)
    {
        aktuellesTelefonIndex = (mm >= 0 && mm < 60) ? mm / 10 : aktuellesTelefonIndex;

        //if (mm >= 0 && mm < 10)
        //{
        //    aktuellesTelefonIndex = 0;
        //}
        //if (mm >= 10 && mm < 20)
        //{
        //    aktuellesTelefonIndex = 1;
        //}
        //if (mm >= 20 && mm < 30)
        //{
        //    aktuellesTelefonIndex = 2;
        //}
        //if (mm >= 30 && mm < 40)
        //{
        //    aktuellesTelefonIndex = 3;
        //}
        //if (mm >= 40 && mm < 50)
        //{
        //    aktuellesTelefonIndex = 4;
        //}
        //if (mm >= 50 && mm < 60)
        //{
        //    aktuellesTelefonIndex = 5;
        //}

        //for (int i = 0; i < 6; i++)
        //{
        //    float j = (float)i;
        //    float jj = j * 10;

        //    if (mm >= j && mm < j + 10)
        //    {
        //        aktuellesTelefonIndex = i;
        //    }
        //}

        ZeigeAktuellesTelefon();
    }

    void WechsleTelefonPlus()
    {
        aktuellesTelefonIndex = (aktuellesTelefonIndex + 1) % telefone.Length;
        ZeigeAktuellesTelefon();
    }

    void WechsleTelefonMinus()
    {
        aktuellesTelefonIndex = (aktuellesTelefonIndex - 1 + telefone.Length) % telefone.Length;
        ZeigeAktuellesTelefon();
    }

    void ZeigeAktuellesTelefon()
    {
        Debug.Log("Zeige aktuelles Telefon 1");
        // Deaktiviere alle Telefone
        foreach (var telefon in telefone)
        {
            telefon.SetActive(false);
        }

        // Aktiviere das aktuelle Telefon
        telefone[aktuellesTelefonIndex].SetActive(true);
        Debug.Log("Zeige aktuelles Telefon 2");
    }
}