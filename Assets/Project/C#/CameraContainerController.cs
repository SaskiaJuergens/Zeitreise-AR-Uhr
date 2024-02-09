using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class CameraContainerController : MonoBehaviour
{
    public GameObject CameraPrefab;
    public GameObject PortraitPrefab; // Das Portrait-Prefab
    public GameObject BackgroundPrefab;
    public Button plusButton;
    public Button minusButton;

    //int mm;
    private int currentIndex = 0;
    private List<GameObject> personSprites = new List<GameObject>();
    private GameObject backgroundSprite;

    void Start()
    {
        // Initialisiere die Telefone
        GameObject portraitInstance = Instantiate(PortraitPrefab.gameObject, transform);
        backgroundSprite = Instantiate(BackgroundPrefab.gameObject, transform);


        Debug.Log(portraitInstance.transform.childCount);

        if (portraitInstance.transform.childCount > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                Debug.Log("inizalisiere Foto" + i);
                personSprites.Add(portraitInstance.transform.GetChild(i).gameObject);
                personSprites[i].SetActive(false);
                //backgroundSprites.Add(backgroundInstance.transform.GetChild(1).gameObject);
                //backgroundSprites[1].SetActive(false);
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

        Firestore.Instance.CameraRef = this;

        // Zeige das erste Portrait an
        ZeigeAktuellesFoto();
        //setImages(0);


        // Register button click events
        plusButton = GameObject.FindGameObjectWithTag("plusButton").GetComponent<Button>();
        minusButton = GameObject.FindGameObjectWithTag("minusButton").GetComponent<Button>();
        Debug.Log(plusButton);
        Debug.Log(plusButton.onClick);
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        minusButton.onClick.AddListener(OnMinusButtonClicked);
    }

    public void setImages(int mm)
    {
        currentIndex = (mm >= 0 && mm < 60) ? (int)(mm / 2.4f) : currentIndex;

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
        backgroundSprite.SetActive(true);
    }
}
