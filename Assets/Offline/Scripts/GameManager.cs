using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
    public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.

    //public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
    public GameObject[] m_players;               // A collection of managers for enabling and disabling different aspects of the tanks.


    private int m_RoundNumber;                  // Which round the game is currently on.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private Capture FlagScript;

    public int blueScore = 0, redScore = 0;
    public Text blue, red;

    private void Start()
    {
        FlagScript = GetComponent<Capture>();
        // Create the delays so they only have to be made once.
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();

        // Once the tanks have been created and the camera is using them as targets, start the game.
        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        // for later
    }

    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundPreStarting());
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        StartCoroutine(GameLoop());

        // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
        /*   if (m_GameWinner != null)
           {
               // If there is a game winner, restart the level.
               Application.LoadLevel(Application.loadedLevel);
           }
           else
           {
               // If there isn't a winner yet, restart this coroutine so the loop continues.
               // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
               StartCoroutine(GameLoop());
        }*/
    }

    private IEnumerator RoundPreStarting()
    {
        // As soon as the round starts reset the tanks and make sure they can't move.
        DisablePlayerControl();
        yield return new WaitForSeconds(0);
    }

    private IEnumerator RoundStarting()
    {
        // As soon as the round starts reset the tanks and make sure they can't move.
        Debug.Log(1);
        ResetAllPlayers();
        Debug.Log(5);
        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_StartWait;
        // As soon as the round begins playing let the players control the tanks.
        EnablePlayerControl();
    }

    public void Score (int team)
    {
        if (team == 1) {
            blueScore++;
            blue.text = blueScore.ToString();
        }
        else redScore++; red.text = redScore.ToString();
    }


    private IEnumerator RoundPlaying()
    {
        // While there is not one tank left...
        while (!hasRoundWinner())
        {
         
            // ... return on the next frame.
            yield return null;
        }
    }

    private bool hasRoundWinner ()
    {
        if (FlagScript.teamRoundWinner != 0)
        {
            Score(FlagScript.teamRoundWinner);
            FlagScript.reset();
            return true;
        }
        return false;
    }


    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

        // reset the winner from the previous round.

      /*  // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        m_MessageText.text = message;*/

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_EndWait;
    }


    // This is used to check if there is one or fewer tanks remaining and thus the round should end.
    /*
    private bool OneTankLeft()
    {
        // Start the count of tanks left at zero.
        int numTanksLeft = 0;

        // Go through all the tanks...
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        // If there are one or fewer tanks remaining return true, otherwise return false.
        return numTanksLeft <= 1;
    }*/

    // This function is used to turn all the tanks back on and reset their positions and properties.
    private void ResetAllPlayers()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            m_players[i].GetComponent<Death_O>().resetPosition();
        }
    }


    private void EnablePlayerControl()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            m_players[i].GetComponent<MoveRB_O>().enabled = true;
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
             // play idle animation
            m_players[i].GetComponent<MoveRB_O>().enabled = false;
            m_players[i].GetComponent<Death_O>().stopAnim();
        }
    }
}
