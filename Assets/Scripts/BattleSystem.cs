using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum battleState{START, ATTACK, DEFEND, WIN, LOSE};

public class BattleSystem : MonoBehaviour
{
    // State of Battle
    public battleState state;
    public battleState[] order;

    // Player and Enemy Objects
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // Player and Enemy battle grounds
    public Transform playerBattleGround;
    public Transform enemyBattleGround;

    // Start is called before the first frame update
    void Start()
    {
        state = battleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(playerPrefab, playerBattleGround);
        Instantiate(enemyPrefab, enemyBattleGround);
        DeterminePriority();
    }

    void DeterminePriority()
    {
        if (playerPrefab.GetComponent<PlayerController>().speed > enemyPrefab.GetComponent<PlayerController>().speed)
        {
            print("P1 goes first!");
            order = new battleState[] {battleState.ATTACK, battleState.DEFEND};

        }
        else if(playerPrefab.GetComponent<PlayerController>().speed > enemyPrefab.GetComponent<PlayerController>().speed)
        {
            print("P2 goes first!");
            order = new battleState[] { battleState.DEFEND, battleState.ATTACK };
        }
        else
        {
            print("Speed is tied!");
            int coinflip = Random.Range(0, 1);
            if(coinflip == 0) {
                print("P1 goes first!");
                order = new battleState[] { battleState.ATTACK, battleState.DEFEND };
            }
            else
            {
                print("P2 goes first!");
                order = new battleState[] { battleState.DEFEND, battleState.ATTACK };
            }
        }
    }
}
