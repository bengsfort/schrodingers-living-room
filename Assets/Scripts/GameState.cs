using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private enum State { GAME, WON, LOST }

    private State mGameState;
    private float mGameTime;

    public float mStartingTime;
    public GameObject[] mFurniture;

    private Schrodinger mSchrodinger;

    private float mNoiseInterval;
    private float mTimeFromLastNoise;

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
        mFurniture = GameObject.FindGameObjectsWithTag("interobj");
        mSchrodinger = GameObject.Find("Schrodinger").GetComponent<Schrodinger>();
        mNoiseInterval = mSchrodinger.mTimeBetweenNoise;
        mTimeFromLastNoise = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mGameTime > 0f && mGameState == State.GAME)
        {
            mGameTime -= Time.deltaTime;
            mTimeFromLastNoise += Time.deltaTime;
        }
        else if (mGameState == State.GAME)
        {
            mGameState = State.WON;
            GameOver();
        }
        if (mTimeFromLastNoise >= mNoiseInterval)
        {
            mSchrodinger.MakeNoise();
            mNoiseInterval = mSchrodinger.mTimeBetweenNoise;
            mTimeFromLastNoise = 0;
        }
    }
}
