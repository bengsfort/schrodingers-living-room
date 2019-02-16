using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float mRatio;

    public FurnitureGraphicsController mGraphicsController;

    public void Interact(float change)
    {
        ChangeRatio(change);
    }

    public void ChangeRatio(float change)
    {
        mRatio += change;
        Debug.Log("ratio: " + mRatio);
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
        if (mRatio > 5f)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Live;
        }
        else if (mRatio < -5f)
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Dead;
        }
        else
        {
            mGraphicsController.quantumState = FurnitureGraphicsController.FurnitureQuantumStates.Superpositioned;
        }
    }
}
