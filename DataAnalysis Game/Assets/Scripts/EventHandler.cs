using Gamekit3D.Message;
using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour, IMessageReceiver
{
    List<EventPlayerHit> listEventHits = new List<EventPlayerHit>();
    List<EventTrackPlayerPosition> positionTrackerList = new List<EventTrackPlayerPosition>();
    List<EventPlayerDead> listPlayerDeaths = new List<EventPlayerDead>();
    List<EventMonsterDead> listMonsterDeaths = new List<EventMonsterDead>();

    public GameObject playerGO;
    private GameObject monster;
    private GameObject player;
    private float currentTime = 0.0f;
    bool trackPlayerPos = false;

    void Update()
    {
        if(trackPlayerPos)
            currentTime += Time.deltaTime;

        if (currentTime >= 2.0f)
        {
            EventTrackPlayerPosition playerPositionEvent = new EventTrackPlayerPosition(playerGO.transform.position, playerGO.GetComponent<objectData>().objectId, 1, Time.realtimeSinceStartup);

            positionTrackerList.Add(playerPositionEvent);

            currentTime = 0;
        }
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                {
                    if(((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {
                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        EventPlayerHit playerHitEvent = new EventPlayerHit(player.transform.position, player.GetComponent<objectData>().objectId,1, Time.realtimeSinceStartup, 
                                                                            monster.transform.position,monster.GetComponent<objectData>().objectId, ((Damageable)sender).currentHitPoints);
                        
                        listEventHits.Add(playerHitEvent);

                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + monster.name);

                        Debug.Log("Damage Reciever: " + player.name);
                    }
                }
                break;
            case MessageType.DEAD:
                {
                    if (((Damageable)sender).GetComponentInParent<objectData>().isMonster)
                    {
                        monster = ((Damageable)sender).gameObject;

                        player = ((DamageMessage)msg).damager.transform.root.gameObject;

                        EventMonsterDead monsterDeadEvent = new EventMonsterDead(monster.transform.position, player.GetComponent<objectData>().objectId, 1, 
                                                                                 Time.realtimeSinceStartup, monster.transform.position);

                        listMonsterDeaths.Add(monsterDeadEvent);

                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + player.name);

                        Debug.Log("monsterDeadEvent: " + monsterDeadEvent);
                    }
                    else if (((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {

                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        EventPlayerDead playerDeadEvent = new EventPlayerDead(player.transform.position, player.GetComponent<objectData>().objectId, 1, Time.realtimeSinceStartup,
                                                                             monster.GetComponent<objectData>().objectId);

                        listPlayerDeaths.Add(playerDeadEvent);

                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + monster.name);

                        Debug.Log("DEAD: " + player.name);
                    }
                }
                break;
            default:
                throw new System.NotImplementedException();
        }
       
    }

    public void ActivateTrackPlayerPos()
    {
        trackPlayerPos = true;
    }
}
