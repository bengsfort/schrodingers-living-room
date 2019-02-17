using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

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

    private Text mTimer;
    private Text mLivingText;
    private Text mDeadText;
    private GameObject mEndPanel;
    private Text mEndText;

    private void GameOver()
    {
        UpdateFurnitureRatio();
        mEndPanel.SetActive(true);
		if (mLivingAmount == mDeadAmount && mSuperAmount == 0) mEndText.text = "Meow!\nSchroedinger cat, you won your owner";
		else mEndText.text = "No one will make\nSchroedingers furniture determined!\nNot even his cat!";
    }

    public void UpdateFurnitureRatio()
    {
        mLivingAmount = mFurniture.Where(x => x.GetComponent<Interactable>().mState == Interactable.State.LIVING).Count(x => x);
        mDeadAmount = mFurniture.Where(x => x.GetComponent<Interactable>().mState == Interactable.State.DEAD).Count(x => x);
        mSuperAmount = mFurniture.Length - mDeadAmount - mLivingAmount;
        mLivingText.text = mLivingAmount.ToString();
        mDeadText.text = mDeadAmount.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("IntroScreen");
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("GameUICanvas");
        mTimer = canvas.transform.Find("Timer").GetComponent<Text>();
        mLivingText = canvas.transform.Find("FurnitureRatio").transform.Find("FurnitureLiving").GetComponent<Text>();
        mDeadText = canvas.transform.Find("FurnitureRatio").transform.Find("FurnitureDead").GetComponent<Text>();
        mEndPanel = canvas.transform.Find("GameOverPanel").gameObject;
        mEndText = mEndPanel.transform.Find("GameOverText").GetComponent<Text>();
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
