using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Leaderboards.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject _scoresPanel;
    [SerializeField] private LeaderboardChannelSO _leaderboardChannel = default;

    private List<ScoreEntry> _scoreEntries = new List<ScoreEntry>();

    private void Start()
    {
        // get score entry objects
        GameObject[] childObjects = _scoresPanel.transform.Cast<Transform>().Select(child => child.gameObject).ToArray();

        foreach(GameObject obj in childObjects)
        {
            TextMeshProUGUI[] textObjects = obj.GetComponentsInChildren<TextMeshProUGUI>();
            
            _scoreEntries.Add(new ScoreEntry (textObjects[0], textObjects[1], textObjects[2]));
        }
    }

    private void OnEnable()
    {
        UpdateScoresUI();
    }

    private void UpdateScoresUI()
    {
        Debug.Log("Updating Scores UI");
        LeaderboardEntry[] leaderboard = _leaderboardChannel.GetLeaderboardEntries();
        Debug.Log("Got Entries from Server");
        
        for(int i = 0; i < _scoreEntries.Count; i ++)
        {
            _scoreEntries[i].nameText.text = leaderboard[i].PlayerName.ToString();
            _scoreEntries[i].scoreText.text = leaderboard[i].Score.ToString();
            _scoreEntries[i].rankText.text = leaderboard[i].Rank.ToString();
        }
    }
}

public class ScoreEntry
{
    public TextMeshProUGUI nameText { get; set; }
    public TextMeshProUGUI scoreText { get; set; }
    public TextMeshProUGUI rankText { get; set; }

    public ScoreEntry(TextMeshProUGUI nameText, TextMeshProUGUI scoreText, TextMeshProUGUI rankText)
    {
        this.nameText = nameText;
        this.scoreText = scoreText;
        this.rankText = rankText;
    }
}
