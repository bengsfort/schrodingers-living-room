using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float mRatio;

    public Sprite mLivingSprite;
    public Sprite mDeadSprite;

    public void Interact()
    {
        ChangeRatio(0.1f);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
