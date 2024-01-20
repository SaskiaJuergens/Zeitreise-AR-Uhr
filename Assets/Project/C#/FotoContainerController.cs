using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class FotoContainerController : MonoBehaviour
{
    public GameObject PortraitPrefab; // Das Portrait-Prefab
    public Transform FotoContainer; // Hier könntest du einen leeren GameObject-Container für die Fotos verwenden
    public Button plusButton;
    public Button minusButton;

    int mm;
    private int currentIndex = 0;
    private List<GameObject> personSprites = new List<GameObject>();

    void Start()
    {
        // Initialisiere die Telefone
        GameObject portraitInstance = Instantiate(PortraitPrefab.gameObject, transform);
        Debug.Log(portraitInstance.transform.childCount);

        //personSprites = new GameObject[25];
        Bluetooth bluetoothInstance = new Bluetooth();

        mm = bluetoothInstance.getMinutes();

        if (portraitInstance.transform.childCount > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                Debug.Log("inizalisiere Foto" + i);
                personSprites.Add(portraitInstance.transform.GetChild(i).gameObject);
                personSprites[i].SetActive(false);
                //personSprites.Add(portraitInstance);
            }
        } else
        {
            Debug.Log("Kein Child im PortraitPrefab");
        }


        // Initialize the personSprites list by cloning the PortraitPrefab
        //for (int i = 0; i < 25; i++)
        //{
        //    GameObject portraitInstance = Instantiate(PortraitPrefab, transform);
        //    portraitInstance.SetActive(false);
        //    personSprites.Add(portraitInstance);
        //}

        // Zeige das erste Portrait an
        ZeigeAktuellesFoto();
        setImages();


        // Register button click events
        plusButton = GameObject.FindGameObjectWithTag("plusButton").GetComponent<Button>();
        minusButton = GameObject.FindGameObjectWithTag("minusButton").GetComponent<Button>();
        Debug.Log(plusButton);
        Debug.Log(plusButton.onClick);
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        minusButton.onClick.AddListener(OnMinusButtonClicked);
    }

    public void setImages()
    {
        //if(mm >= 0 && mm < 2.4)
        //{
        //    currentIndex = 0;
        //} 
        //else if (mm >= 2.4 && mm < 4.8)

        for (int i = 0; i < 25; i++)
        {
            float j = (float)i;
            float jj = j * 2.4f;

           if (mm >= j && mm < j + 2.4f)
           {
              currentIndex = i;
           }
        }

        ZeigeAktuellesFoto();
    }

    // Plus-Button-Methode
    public void OnPlusButtonClicked()
    {
        currentIndex = (currentIndex + 1) % personSprites.Count;
        ZeigeAktuellesFoto();
    }

    // Minus-Button-Methode
    public void OnMinusButtonClicked()
    {
        currentIndex = (currentIndex - 1 + personSprites.Count) % personSprites.Count;
        ZeigeAktuellesFoto();
    }

    void ZeigeAktuellesFoto()
    {
        // Deaktiviere alle Portraits
        foreach (var portrait in personSprites)
        {
            portrait.SetActive(false);
        }

        // Aktiviere das aktuelle Portrait
        personSprites[currentIndex].SetActive(true);
    }
}
