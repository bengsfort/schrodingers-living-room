using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private enum State { GAME, WON, LOST }

    private State mGameState;
    private float mGameTime;

    public float mStartingTime;

    private void GameOver()
    {
        if (mGameState == State.WON) Debug.Log("Won");
        else Debug.Log("Lost");
    }

    // Start is called before the first frame update
    void Start()
    {
        mGameTime = mStartingTime;
        mGameState = State.GAME;
    }

    // Update is called once per frame
    void Update()
    {
        if (mGameTime > 0f && mGameState == State.GAME)
        {
            mGameTime -= Time.deltaTime;
            Debug.Log(mGameTime);
        }
        else if (mGameState == State.GAME)
        {
            mGameState = State.WON;
            GameOver();
        }
    }
}
