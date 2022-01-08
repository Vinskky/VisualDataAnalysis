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

    private GameObject monster;
    private GameObject player;
    private float currentTime = 0.0f;

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                {
                    if(((Damageable)sender).GetComponentInParent<objectData>().isMonster)
                    {

                        monster = ((Damageable)sender).gameObject;

                        player = ((DamageMessage)msg).damager.transform.root.gameObject;

                        

                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + player.name);

                        Debug.Log("Damage Reciever: " + monster.name);

                    }
                    else if(((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {
                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        EventPlayerHit playerHitEvent = new EventPlayerHit(player.transform.position, player.GetComponent<objectData>().objectId,1, currentTime, 
                                                                            monster.transform.position,monster.GetComponent<objectData>().objectId, ((Damageable)sender).currentHitPoints);

                        EventTrackPlayerPosition playerPositionEvent = new EventTrackPlayerPosition(player.transform.position, player.GetComponent<objectData>().objectId, 1, currentTime);
                        
                        listEventHits.Add(playerHitEvent);

                        positionTrackerList.Add(playerPositionEvent);

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

                        EventMonsterDead monsterDeadEvent = new EventMonsterDead(monster.transform.position, player.GetComponent<objectData>().objectId, 1, currentTime,
                                                                            monster.transform.position);

                        EventTrackPlayerPosition playerPositionEvent = new EventTrackPlayerPosition(player.transform.position, player.GetComponent<objectData>().objectId, 1, currentTime);

                        listMonsterDeaths.Add(monsterDeadEvent);

                        positionTrackerList.Add(playerPositionEvent);


                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + player.name);

                        Debug.Log("monsterDeadEvent: " + monsterDeadEvent);
                    }
                    else if (((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {

                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        EventPlayerDead playerDeadEvent = new EventPlayerDead(player.transform.position, player.GetComponent<objectData>().objectId, 1, currentTime,
                                                                             monster.GetComponent<objectData>().objectId);

                        EventTrackPlayerPosition playerPositionEvent = new EventTrackPlayerPosition(player.transform.position, player.GetComponent<objectData>().objectId, 1, currentTime);

                        listPlayerDeaths.Add(playerDeadEvent);

                        positionTrackerList.Add(playerPositionEvent);


                        //-----LOGS------
                        Debug.Log("Damage Attacker: " + monster.name);

                        Debug.Log("DEAD: " + player.name);
                    }
                }
                break;
            case MessageType.RESPAWN:
                break;
            default:
                throw new System.NotImplementedException();
        }
       
    }
}
