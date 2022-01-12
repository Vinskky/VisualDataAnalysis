﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
{
    string playerPosPath    = "Assets/Data/playerPosEvent.json";
    string playerHitPath    = "Assets/Data/playerHitEvent.json";
    string playerDeadPath   = "Assets/Data/playerDeadEvent.json";
    string monsterDeadPath  = "Assets/Data/monsterDeadEvent.json";
    string sessionPath      = "Assets/Data/sessionEvent.json";
    
    public void SerializeAndSave(EventHandler eventHandler)
    {
        string jsonString = string.Empty;

        //------------------Player Position Event----------------------
        jsonString = File.Exists(playerPosPath) ? File.ReadAllText(playerPosPath) : string.Empty;           // Intellisense does not like putting the condition in ().

        foreach (EventTrackPlayerPosition eventPlayerPos in eventHandler.positionTrackerList)
        {
            jsonString += eventPlayerPos.GetSerialized() + "\n";
        }

        File.WriteAllText(playerPosPath, jsonString);
        
        //------------------Player Hit Event----------------------
        jsonString = File.Exists(playerHitPath) ? File.ReadAllText(playerHitPath) : string.Empty;

        foreach (EventPlayerHit eventPlayerHit in eventHandler.listEventHits)
        {
            jsonString += eventPlayerHit.GetSerialized() + "\n";
        }
        
        File.WriteAllText(playerHitPath, jsonString);

        //------------------Player Dead Event----------------------
        jsonString = File.Exists(playerDeadPath) ? File.ReadAllText(playerDeadPath) : string.Empty;

        foreach (EventPlayerDead eventPlayerDead in eventHandler.listPlayerDeaths)
        {
            jsonString += eventPlayerDead.GetSerialized() + "\n";
        }

        File.WriteAllText(playerDeadPath, jsonString);

        //------------------Monster Dead Event----------------------
        jsonString = File.Exists(monsterDeadPath) ? File.ReadAllText(monsterDeadPath) : string.Empty;

        foreach (EventMonsterDead eventMonsterDead in eventHandler.listMonsterDeaths)
        {
            jsonString += eventMonsterDead.GetSerialized() + "\n";
        }

        File.WriteAllText(monsterDeadPath, jsonString);

        //------------------Session Event----------------------
        jsonString = File.Exists(sessionPath) ? File.ReadAllText(sessionPath) : string.Empty;

        foreach (EventSession eventSession in eventHandler.listSessions)
        {
            jsonString += eventSession.GetSerialized() + "\n";
        }

        File.WriteAllText(sessionPath, jsonString);
    }

    public void DeserializeEventData(EventHandler eventHandler)
    {
        string[] events = new string[4096];

        //------------------Player Position Event----------------------
        if (File.Exists(playerPosPath))
        {
            events = File.ReadAllLines(playerPosPath);

            foreach(string ev in events)
            {
                if (ev == null)
                    break;
                
                eventHandler.positionTrackerList.Add(JsonUtility.FromJson<EventTrackPlayerPosition>(ev));
            }
        }

        //------------------Player Hit Event----------------------
        if (File.Exists(playerHitPath))
        {
            events = File.ReadAllLines(playerHitPath);

            foreach(string ev in events)
            {
                if (ev == null)
                    break;
                
                eventHandler.listEventHits.Add(JsonUtility.FromJson<EventPlayerHit>(ev));
            }
        }

        //------------------Player Dead Event----------------------
        if (File.Exists(playerDeadPath))
        {
            events = File.ReadAllLines(playerDeadPath);

            foreach(string ev in events)
            {
                if (ev == null)
                    break;
                
                eventHandler.listPlayerDeaths.Add(JsonUtility.FromJson<EventPlayerDead>(ev));
            }
        }

        //------------------Monster Dead Event----------------------
        if (File.Exists(monsterDeadPath))
        {
            events = File.ReadAllLines(monsterDeadPath);

            foreach(string ev in events)
            {
                if (ev == null)
                    break;
                
                eventHandler.listMonsterDeaths.Add(JsonUtility.FromJson<EventMonsterDead>(ev));
            }
        }

        //------------------Session Event----------------------
        if (File.Exists(sessionPath))
        {
            events = File.ReadAllLines(sessionPath);
            
            foreach(string ev in events)
            {
                if (ev == null)
                    break;
                
                eventHandler.listSessions.Add(JsonUtility.FromJson<EventSession>(ev));
            }
        }
    }

    public int GetLastRegisteredSessionID()
    {
        if (!File.Exists(sessionPath))
            return -1;
        
        string[] sessionEvs = new string[4092];
        sessionEvs = File.ReadAllLines(sessionPath);

        string prevEv = string.Empty;
        foreach(string ev in sessionEvs)
        {   
            if (ev == null)
                break;
            
            prevEv = ev;
        }

        EventSession lastSession = JsonUtility.FromJson<EventSession>(prevEv);

        return lastSession.sessionId;
    }
}
