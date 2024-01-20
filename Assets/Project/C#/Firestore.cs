using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;
using Firebase;
using System;

public class Firestore : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseApp app;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    private void Start()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        SetData();
        GetData();
    }

    public void SetData()
    {
        Debug.Log("SetData start");
        DocumentReference docRef = db.Collection("Zeituhr").Document("Variablen");


        Dictionary<string, object> Variablen = new Dictionary<string, object>
        {
                { "Hour", 7 },
                { "Button", false }
        };
        docRef.SetAsync(Variablen).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
        });

    }

    public void GetData()
    {
        DocumentReference docRef = db.Collection("Zeituhr").Document("Variablen");
        docRef.Listen(snapshot => {
            Debug.Log("Callback received document snapshot.");
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
            Dictionary<string, object> Variablen = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in Variablen)
            {
                Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
            }
        });
    }
}
