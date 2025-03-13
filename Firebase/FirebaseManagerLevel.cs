using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManagerLevel : MonoBehaviour
{
    public static FirebaseManagerLevel instance = null;

    public string m_strFoodEventName = "";
    public string m_strLightEventName = "";
    public GameObject m_kControlObj = null;
    private DatabaseReference m_kdbReference;

    private void Awake()
    {
        m_kdbReference = FirebaseDatabase.DefaultInstance.RootReference;
        instance = this;
    }

    public void OnStartGame()
    {
        m_kdbReference.Child("CentralData/" + m_strFoodEventName).ValueChanged  += OnEventChanged;
        m_kdbReference.Child("CentralData/" + m_strLightEventName).ValueChanged += OnEventChanged;
    }

    public void OnEndGame()
    {
        m_kdbReference.Child("CentralData/" + m_strFoodEventName).ValueChanged  -= OnEventChanged;
        m_kdbReference.Child("CentralData/" + m_strLightEventName).ValueChanged -= OnEventChanged;
    }

    private void OnEventChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Firebase Error: " + args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot.Exists)
        {
            string eventName = args.Snapshot.Reference.Key;
            bool eventValue = (bool)args.Snapshot.Value;
            Debug.Log($"{eventName} changed: {eventValue}");
            m_kControlObj.SendMessage(eventName == m_strFoodEventName ? "OnSetFoodEvent" : "OnSetLightEvent", eventValue);
        }
    }

}
