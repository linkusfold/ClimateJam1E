using System.Collections.Generic;
using UnityEngine;

/*
 * -------------------------------------------------------
 * ReputationManager.cs
 * Author: Angel
 *
 * Tracks player reputation with each villager and persists
 * across scenes. Will be used to unlock upgrades and save
 * persistent values from conversations.
 * -------------------------------------------------------
 */
public class ReputationManager : MonoBehaviour
{
    public static ReputationManager Instance { get; private set; }

    private Dictionary<string, int> villagerReputations = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetReputation(string villagerID, int value)
    {
        villagerReputations[villagerID] = value;
    }

    private int GetReputation(string villagerID)
    {
        return villagerReputations.ContainsKey(villagerID)
            ? villagerReputations[villagerID]
            : 0;
    }

    public void AddReputation(string villagerID, int delta)
    {
        if (!villagerReputations.ContainsKey(villagerID))
            villagerReputations[villagerID] = 0;

        villagerReputations[villagerID] += delta;
    }

    public bool MeetsThreshold(string villagerID, int threshold)
    {
        return GetReputation(villagerID) >= threshold;
    }
    
    
    public void SaveData()
    {
        foreach (KeyValuePair<string, int> pair in villagerReputations)
        {
            PlayerPrefs.SetInt("Reputation_" + pair.Key, pair.Value);
        }

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        foreach (string villagerID in GetAllKnownVillagerIDs())
        {
            if (PlayerPrefs.HasKey("Reputation_" + villagerID))
            {
                int value = PlayerPrefs.GetInt("Reputation_" + villagerID);
                villagerReputations[villagerID] = value;
            }
        }
    }

    // For list of villagers
    private List<string> GetAllKnownVillagerIDs()
    {
        return new List<string> { };
    }
}