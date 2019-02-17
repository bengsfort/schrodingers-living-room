using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float mRatio;
    public float mLivingTreshold;
    public float mDeadTreshold;

    public enum State { INITIAL, LIVING, DEAD, SUPERPOSITION }
    public State mState;

    public FurnitureGraphicsController mGraphicsController;

    private GameState mGameState;

    public void Interact(float change)
    {
        ChangeRatio(change, true);
    }

    public void ChangeRatio(float change, bool isInteract)
    {
        if (isInteract) mRatio += change;
        else
        {
            if (Mathf.Abs(mRatio) < Mathf.Abs(change)) mRatio = 0;
            else if (mRatio < 0) mRatio += change;
            else if (mRatio > 0) mRatio -= change;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mGraphicsController = GetComponent<FurnitureGraphicsController>();
        mGameState = GameObject.Find("GameState").GetComponent<GameState>();
        mState = State.INITIAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (mRatio > mLivingTreshold && mState != State.LIVING)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Live;
            mState = State.LIVING;
            mGameState.UpdateFurnitureRatio();
        }
        else if (mRatio < mDeadTreshold && mState != State.DEAD)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Dead;
            mState = State.DEAD;
            mGameState.UpdateFurnitureRatio();
        }
        else if (mRatio <= mLivingTreshold && mRatio >= mDeadTreshold && mState != State.SUPERPOSITION)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Superpositioned;
            mState = State.SUPERPOSITION;
            mGameState.UpdateFurnitureRatio();
        }
    }
}
