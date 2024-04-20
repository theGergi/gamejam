using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    string userID;
    FirebaseFirestore db;
    CollectionReference collection;
    DocumentReference playerRef;

    // Start is called before the first frame update
    void Start()
    {
        userID = PlayerPrefs.GetString("userID");
        if (userID == "") { 
            userID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("userID", userID);
        }
        Debug.Log(userID);

        db = FirebaseFirestore.DefaultInstance;

        collection = db.Collection("user_scores");

        playerRef = collection.Document(userID);

        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "score", 122 },
            { "name", "poopoo" },
        };
        playerRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data");
        });

        getScores();
        getOwnScore();
    }

    void getScores()
    {
        Query query = collection.OrderByDescending("score").Limit(10);
        query.GetSnapshotAsync().ContinueWithOnMainThread((task) =>
        {   
            foreach (DocumentSnapshot item in task.Result.Documents) {
                Dictionary<string, object> keyValuePairs = item.ToDictionary();
                Debug.Log(string.Format("Player {0} has score {1}", keyValuePairs["name"], keyValuePairs["score"]));
            }
        });
    }

    void getOwnScore()
    {
        playerRef.GetSnapshotAsync().ContinueWithOnMainThread((task) =>
        {
            DocumentSnapshot snapshot = task.Result;
            Dictionary<string, object> keyValuePairs = snapshot.ToDictionary();
            Debug.Log(string.Format("This player has name {0} and score {1}", keyValuePairs["name"], keyValuePairs["score"]));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
