using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ButterflyController : MonoBehaviour
{
    public VideoClip videoClip; // Referenz auf das Videoclip
    public Button plusButton;
    public Button minusButton;

    private float currentSecond = 0f;
    private VideoPlayer videoPlayer;
  
    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // VideoPlayer-Komponente hinzufügen
        videoPlayer.playOnAwake = false; // Das Video nicht automatisch abspielen
        videoPlayer.clip = videoClip; // Das Videoclip zuweisen


        if (videoPlayer == null)
        {
            Debug.LogError("Kein VideoPlayer-Komponente gefunden.");
            return;
        }


        Firestore.Instance.ButterflyRef = this;

        // Setze die Button-Handler
        plusButton = GameObject.FindGameObjectWithTag("plusButton").GetComponent<Button>();
        minusButton = GameObject.FindGameObjectWithTag("minusButton").GetComponent<Button>();
        // Setze die Button-Handler
        plusButton.onClick.AddListener(PlayNextFrame);
        minusButton.onClick.AddListener(PlayPreviousFrame);

        // Setze die Zeit des Videos auf den vierten Frame
        videoPlayer.frame = 4;
        //SetVideoTime(1f);
        videoPlayer.Pause(); 

    }

    public void setVideo(int mm)
    {
       
        if(currentSecond > mm)
        {
            PlayPreviousFrame();
        }

        if (currentSecond < mm)
        {
            PlayNextFrame();
        }

        currentSecond = mm;
    }

    void PlayNextFrame()
    {
        if (videoPlayer.frame < (long)(videoPlayer.frameCount - 1)) // Überprüfen, ob es noch Frames gibt
        {
            videoPlayer.frame = videoPlayer.frame + 10; // Nächsten Frame anzeigen
        }
    }

    void PlayPreviousFrame()
    {
        if (videoPlayer.frame > 0) // Überprüfen, ob es vorherige Frames gibt
        {
            videoPlayer.frame= videoPlayer.frame-10; // Vorherigen Frame anzeigen
        }
    }
}
