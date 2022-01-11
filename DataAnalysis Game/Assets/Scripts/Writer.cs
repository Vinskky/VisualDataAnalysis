using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
{
    public void SerializeAndSave(EventHandler eventHandler)
    {
        string playerPosPath = "Assets/Data/playerPosEvent.json";
        string playerHitPath = "Assets/Data/playerHitEvent.json";
        string monsterDeadPath = "Assets/Data/monsterDeadEvent.json";
        string playerDeadPath = "Assets/Data/playerDeadEvent.json";
        string jsonString = string.Empty;

        //------------------Player Position Event----------------------
        if (File.Exists(playerPosPath))
            jsonString = File.ReadAllText(playerPosPath);

        foreach (EventTrackPlayerPosition eventPlayerPos in eventHandler.positionTrackerList)
        {
            jsonString += eventPlayerPos.GetSerialized() + "\n";
        }

        File.WriteAllText(playerPosPath, jsonString);

        jsonString = string.Empty;
        //------------------Player Hit Event----------------------

        if (File.Exists(playerHitPath))
            jsonString = File.ReadAllText(playerHitPath);

        foreach (EventPlayerHit eventPlayerPos in eventHandler.listEventHits)
        {
            jsonString += eventPlayerPos.GetSerialized() + "\n";
        }
        File.WriteAllText(playerHitPath, jsonString);

        jsonString = string.Empty;

        //------------------Monster Dead Event----------------------

        if (File.Exists(monsterDeadPath))
            jsonString = File.ReadAllText(monsterDeadPath);

        foreach (EventMonsterDead eventPlayerPos in eventHandler.listMonsterDeaths)
        {
            jsonString += eventPlayerPos.GetSerialized() + "\n";
        }

        File.WriteAllText(monsterDeadPath, jsonString);

        jsonString = string.Empty;

        //------------------Player Dead Event----------------------

        if (File.Exists(playerDeadPath))
            jsonString = File.ReadAllText(playerDeadPath);

        foreach (EventPlayerDead eventPlayerPos in eventHandler.listPlayerDeaths)
        {
            jsonString += eventPlayerPos.GetSerialized() + "\n";
        }


        File.WriteAllText(playerDeadPath, jsonString);
    }
}
