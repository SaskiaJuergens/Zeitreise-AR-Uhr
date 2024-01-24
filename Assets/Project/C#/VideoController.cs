using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    //public GameObject videoPrefab;  // Das Video-Prefab
    public Transform videoContainer; // Hier könntest du einen leeren GameObject-Container für die Videos verwenden
    public Button plusButton;
    public Button minusButton;

    private int currentVideoIndex = 0;
    //private GameObject[] videos;
    private VideoPlayer videoPlayer;

    public VideoClip[] videoClips;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // Initialisiere die Videos
        //GameObject videoObject = Instantiate(videoPrefab.gameObject, videoContainer);

        //videos = new GameObject[20];
        //for (int i = 0; i < 20; i++)
        //{
        //    //videos[i] = videoObject;
        //    //videoObject.SetActive(false);

        //    string videoName = "Video" + (i + 1);
        //    string videoPath = "Assets/Project/Mario/" + videoName + ".mp4";
        //    videoClips[i] = Resources.Load<VideoClip>(videoPath);
        //    //videoClips[i] = Resources.Load<VideoClip>("Mario/" + videoName);

        //    if (videoClips == null)
        //    {
        //        Debug.LogError("VideoClip " + (i + 1) + " konnte nicht geladen werden! Name: " + videoName);
        //    }
        //    else
        //    {
        //        videoPlayer.clip = videoClips[i];
        //        Debug.Log("VideoClip " + (i + 1) + " erfolgreich geladen! Name: " + videoName);
        //    }
        //}

        if (videoClips == null || videoClips.Length == 0)
        {
            Debug.LogError("Keine VideoClips zugewiesen oder Liste ist leer.");
            return;
        }

        Firestore.Instance.VideoRef = this;

        // Spiele das erste Video ab
        PlayCurrentVideo();
        //setVideo(0);

        // Setze die Button-Handler
        plusButton = GameObject.FindGameObjectWithTag("plusButton").GetComponent<Button>();
        minusButton = GameObject.FindGameObjectWithTag("minusButton").GetComponent<Button>();

        plusButton.onClick.AddListener(PlayNextVideo);
        minusButton.onClick.AddListener(PlayPreviousVideo);
    }

    public void setVideo(int mm)
    {
        currentVideoIndex = (mm >= 0 && mm < 60) ? (int)(mm / 2.86f) : currentVideoIndex;


        //for (int i = 0; i < 21; i++)
        //{
        //    float j = (float)i;
        //    float jj = j * 2.86f;

        //    if (mm >= j && mm < j + 2.86f)
        //    {
        //        currentVideoIndex = i;
        //    }
        //}

        PlayCurrentVideo();
    }
    void PlayNextVideo()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Length;
        PlayCurrentVideo();
    }

    void PlayPreviousVideo()
    {
        currentVideoIndex = (currentVideoIndex - 1 + videoClips.Length) % videoClips.Length;
        PlayCurrentVideo();
    }

    void PlayCurrentVideo()
    {

        //// Aktiviere das aktuelle Video
        //videos[currentVideoIndex].SetActive(true);
        videoPlayer.clip = videoClips[currentVideoIndex];

        // Starte das Video ab dem Anfang
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
