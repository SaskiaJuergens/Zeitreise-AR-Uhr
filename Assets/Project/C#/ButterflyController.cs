using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ButterflyController : MonoBehaviour
{
    public Transform videoContainer;
    public Button plusButton;
    public Button minusButton;

    private float currentSecond = 0f;
    private VideoPlayer videoPlayer;
    private float videoLengthInSeconds = 29f; // Die Länge des Videos beträgt 29 Sekunden

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("Kein VideoPlayer-Komponente gefunden.");
            return;
        }

        // Setze die Button-Handler
        plusButton.onClick.AddListener(PlayNextVideo);
        minusButton.onClick.AddListener(PlayPreviousVideo);

        videoPlayer.Play();

    }

    public void setVideo(float seconds)
    {
        currentSecond = Mathf.Clamp(seconds, 0f, videoLengthInSeconds); // Wir möchten sicherstellen, dass die Sekunde im Bereich von 0 bis zur Länge des Videos liegt
        SetVideoTime(currentSecond);
    }

    void PlayNextVideo()
    {
        currentSecond = Mathf.Min(currentSecond + 1f, videoLengthInSeconds);
        SetVideoTime(currentSecond);
    }

    void PlayPreviousVideo()
    {
        currentSecond = Mathf.Max(currentSecond - 1f, 0f); // Wir möchten sicherstellen, dass die Sekunde nicht unter 0 fällt
        SetVideoTime(currentSecond);
    }

    void SetVideoTime(float second)
    {
        float normalizedTime = second / videoLengthInSeconds;
        videoPlayer.time = normalizedTime * videoLengthInSeconds;
        videoPlayer.Play();
    }
}
