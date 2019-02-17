using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameState : MonoBehaviour
{
    private enum State { INTRO, GAME, END }

    private State mGameState;
    private float mGameTime;

    private Schrodinger mSchrodinger;

    private float mNoiseInterval;
    private float mTimeFromLastNoise;
    private int mLivingAmount;
    private int mDeadAmount;
    private int mSuperAmount;

    public float mStartingTime;
    public GameObject[] mFurniture;

    [Header("Drag Timer from GameUICanvas here")]
    public Text mTimer;
    [Header("Drag Text fields from FurnitureRatio here")]
    public Text mLivingText;
    public Text mDeadText;

    private void GameOver()
    {
        UpdateFurnitureRatio();
        if (mLivingAmount == mDeadAmount && mSuperAmount == 0) Debug.Log("Won");
        else Debug.Log("Lost");
    }

    public void UpdateFurnitureRatio()
    {
        mLivingAmount = mFurniture.Where(x => x.GetComponent<Interactable>().mState == Interactable.State.LIVING).Count(x => x);
        mDeadAmount = mFurniture.Where(x => x.GetComponent<Interactable>().mState == Interactable.State.DEAD).Count(x => x);
        mSuperAmount = mFurniture.Length - mDeadAmount - mLivingAmount;
        mLivingText.text = mLivingAmount.ToString();
        mDeadText.text = mDeadAmount.ToString();
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
            mTimer.text = mGameTime.ToString();
        }
        else if (mGameState == State.GAME)
        {
            mGameState = State.END;
            GameOver();
        }
        if (mTimeFromLastNoise >= mNoiseInterval && mGameState == State.GAME)
        {
            mSchrodinger.MakeNoise();
            mNoiseInterval = mSchrodinger.mTimeBetweenNoise;
            mTimeFromLastNoise = 0;
        }
    }
}
