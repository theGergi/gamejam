using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;
using System;
using System.Linq;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    string userID;
    FirebaseFirestore db;
    CollectionReference collection;
    DocumentReference playerRef;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

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

        

        //getScores();
        getOwnScore();
    }

    public void updateLeaderboard(ListView leaderboard)
    {
        var items = new List<Dictionary<string,object>>();
        Query query = collection.OrderByDescending("score").Limit(10);
        query.GetSnapshotAsync().ContinueWithOnMainThread((task) =>
        {
            //items = task.Result.Documents.ToList<Dictionary<string, object>>();
            foreach (DocumentSnapshot item in task.Result.Documents)
            {
                Dictionary<string, object> keyValuePairs = item.ToDictionary();
                items.Add(keyValuePairs);
            }
            //leaderboard.Clear();
            Func<VisualElement> makeItem = () => new VisualElement();
            Action<VisualElement, int> bindItem = (e, i) => {
                Label l = new Label(i + 1 + ". " + items[i]["name"].ToString());
                Label l2 = new Label(items[i]["score"].ToString());
                l.style.unityTextAlign = TextAnchor.MiddleLeft;
                l2.style.unityTextAlign = TextAnchor.MiddleRight;
                e.Add(l);
                e.Add(l2);
                e.style.flexDirection = FlexDirection.Row;
                e.style.justifyContent = Justify.SpaceBetween;

            };
            leaderboard.bindItem = bindItem;
            leaderboard.makeItem = makeItem;
            leaderboard.itemsSource = items;
            leaderboard.Rebuild();
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

    public void pushScore(int score)
    {
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "score", score },
            { "name", "poopoo" },
        };
        playerRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
