using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Battle State
public enum BattleState { Start, Player_Turn, Enemy_Turn, Win, Lose };
public class PlayerController : MonoBehaviour
{
    // Player Stats
    public int maxHealth;
    public int curHealth;
    public int overHeal = 0;
    public int attack;
    public int speed;

    // Player State Flags
    public BattleState state;

    // Player Refrences
    private Animator animator;
    private string currentState;

    // Player Animations
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_ENTER = "Enter";
    const string PLAYER_BASIC_LIGHT = "Basic - Light";
    const string PLAYER_BASIC_HEAVY = "Basic - Heavy";
    const string PLAYER_BASIC_HIGH = "Basic - High";
    const string PLAYER_BASIC_LOW = "Basic - Low";
    const string PLAYER_DEFEND_LIGHT = "Defend - Light";
    const string PLAYER_DEFEND_HEAVY = "Defend - Heavy";
    const string PLAYER_DEFEND_HIGH = "Defend - High";
    const string PLAYER_DEFEND_LOW = "Defend - Low";
    const string PLAYER_CRITICAL = "Critical";
    const string PLAYER_SPECIAL = "Special";
    const string PLAYER_CHARGE = "Power";
    const string PLAYER_HURT_LIGHT = "Hurt - Light";
    const string PLAYER_HURT_HEAVY = "Hurt - Heavy";
    const string PLAYER_HURT_HIGH = "Hurt - High";
    const string PLAYER_HURT_LOW = "Hurt - Low";
    const string PLAYER_EXIT_WIN = "Exit - Win";
    const string PLAYER_EXIT_LOSE = "Exit - Lose";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = BattleState.Start;
        curHealth = maxHealth;
        StartCoroutine(entrance());
    }

    void ChangeAnimationState(string newState)
    {
        // Don't Interrupt
        if (currentState == newState) return;

        // Play Animation
        animator.Play(newState);

        // Update Current State
        currentState = newState;
    }
    // Entrance animation (Ignore for now)
    IEnumerator entrance()
    {
        print("Entering");
        ChangeAnimationState(PLAYER_ENTER);
        yield return new WaitForSeconds(1);
        state = BattleState.Player_Turn;
        ChangeAnimationState(PLAYER_IDLE);
    }

    void playerTurn()
    {
        // Do Player Turn Stuff
        print("It's you're turn now!");
        state = BattleState.Enemy_Turn;
    }

    void enemyTurn()
    {
        // Do Enemy Turn Stuff
        print("It's enemy turn now!");
        state = BattleState.Player_Turn;
    }

    void win()
    {
        print("You WIN!");
        ChangeAnimationState(PLAYER_EXIT_WIN);
    }

    void lose()
    {
        print("You LOSE!");
        ChangeAnimationState(PLAYER_EXIT_LOSE);
    }

    // Update is called once per frame
    void Update()
    {
        // Check Health
        if(curHealth <= 0)
        {
            state = BattleState.Lose;
        }

        // If Healing goes above maxHealth
        if(curHealth > maxHealth)
        {
            overHeal = overHeal + (curHealth - maxHealth);
            curHealth = maxHealth;
        }

        // Check if Game has been decided

        if(state == BattleState.Win)
        {
            win();
        }
        else if(state == BattleState.Lose)
        {
            lose();
        }

    }

}
