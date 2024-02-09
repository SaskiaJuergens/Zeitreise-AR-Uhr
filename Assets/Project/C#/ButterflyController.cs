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
    private float videoLengthInSeconds = 29f; // Die L�nge des Videos betr�gt 29 Sekunden

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // VideoPlayer-Komponente hinzuf�gen
        videoPlayer.playOnAwake = false; // Das Video nicht automatisch abspielen
        videoPlayer.clip = videoClip; // Das Videoclip zuweisen
        videoLengthInSeconds = (float)videoClip.length; // Die L�nge des Videos in Sekunden erhalten
        videoPlayer.time = videoLengthInSeconds;

        if (videoPlayer == null)
        {
            Debug.LogError("Kein VideoPlayer-Komponente gefunden.");
            return;
        }

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

    public void setVideo(float seconds)
    {
        currentSecond = seconds;
        if(currentSecond > seconds)
        {
            PlayPreviousFrame();
        }

        if (currentSecond < seconds)
        {
            PlayNextFrame();
        }
    }

    void PlayNextFrame()
    {
        if (videoPlayer.frame < (long)(videoPlayer.frameCount - 1)) // �berpr�fen, ob es noch Frames gibt
        {
            videoPlayer.frame = videoPlayer.frame + 10; // N�chsten Frame anzeigen
        }
    }

    void PlayPreviousFrame()
    {
        if (videoPlayer.frame > 0) // �berpr�fen, ob es vorherige Frames gibt
        {
            videoPlayer.frame= videoPlayer.frame-10; // Vorherigen Frame anzeigen
        }
    }
}
