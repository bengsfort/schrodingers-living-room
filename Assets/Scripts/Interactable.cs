using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float mRatio;
    public float mLivingTreshold;
    public float mDeadTreshold;

    public FurnitureGraphicsController mGraphicsController;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: highlight or something?
    }

    // Start is called before the first frame update
    void Start()
    {
        mGraphicsController = GetComponent<FurnitureGraphicsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mRatio > mLivingTreshold)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Live;
        }
        else if (mRatio < mDeadTreshold)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Dead;
        }
        else
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Superpositioned;
        }
    }
}
