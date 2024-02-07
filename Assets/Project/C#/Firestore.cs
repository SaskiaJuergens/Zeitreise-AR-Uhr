using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System; //.TaskExtension; // for ContinueWithOnMainThread

public class Firestore : MonoBehaviour
{
    // Get the root reference location of the database.
    DatabaseReference reference;
    public FotoContainerController FotoRef;
    public TelefonWechselScript TelRef;
    public VideoController VideoRef;
    public ButterflyController ButterflyRef;
    public static Firestore Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //GetData();
        SubscribeToDatabseElement();
    }

public void SubscribeToDatabseElement()
    {
        FirebaseDatabase.DefaultInstance.GetReference("test").ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Snapshot:");
        foreach (var item in args.Snapshot.Children)
        {
            Debug.Log(item);
            Debug.Log(item.Value);
            if(FotoRef != null)
            {

            FotoRef.setImages(Convert.ToInt32(item.Value));
            }
            if (TelRef != null)
            {

                TelRef.setTelefon(Convert.ToInt32(item.Value));
            }
            if (VideoRef != null)
            {

                VideoRef.setVideo(Convert.ToInt32(item.Value));
            }
            if (ButterflyRef != null)
            {

                ButterflyRef.setVideo(Convert.ToInt32(item.Value));
            }

        }
    }
}
