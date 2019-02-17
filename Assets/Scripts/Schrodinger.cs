using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Schrodinger : MonoBehaviour
{
    public float mMaxNoiseValue;
    public float mMinNoiseValue;
    public float mMaxFurniturePercentage;
    public float mMinFurniturePercentage;
    public float mSpeedUp;
    public float mTimeBetweenNoise;

    [Header("Don't touch these")]
    public int mMaxFurnitureAmount;
    public int mMinFurnitureAmount;

    public GameObject[] mFurniture;

    private GameState mGameState;
    private QuantumManager mQuantum;

    private GameObject mDeadCat;
    private GameObject mLivingCat;

    // Start is called before the first frame update
    void Start()
    {
        mFurniture = GameObject.FindGameObjectsWithTag("interobj");
        mMaxFurnitureAmount = (int)Mathf.Round(mFurniture.Length * mMaxFurniturePercentage);
        mMinFurnitureAmount = (int)Mathf.Round(mFurniture.Length * mMinFurniturePercentage);
        mGameState = GameObject.Find("GameState").GetComponent<GameState>();
        mDeadCat = GameObject.Find("DeadCat");
        mLivingCat = GameObject.Find("LivingCat");
        mQuantum = GetComponent<QuantumManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNoise()
    {
        float left = mLivingCat.transform.position.x + mDeadCat.transform.position.x;
        float right = mLivingCat.transform.position.y + mDeadCat.transform.position.y;
        int seed = 0; //mQuantum.GetNewValue(left, right);
        if (seed != 0)
        {
            Random.InitState(seed);
            Debug.Log("quantum seed (" + seed + ") applied");
        }
        else Random.InitState(System.DateTime.Now.Millisecond);
        System.Random rand = new System.Random();
        int amount = Random.Range(mMinFurnitureAmount, mMaxFurnitureAmount + 1);

        IEnumerable<int> chosenFurniture = Enumerable.Range(0, mFurniture.Length).OrderBy(a => rand.Next()).Take(amount);

        foreach (int i in chosenFurniture)
        {
            float noise = Random.Range(mMinNoiseValue, mMaxNoiseValue);
            //Debug.Log("noise for furniture " + i + ": " + noise);
            mFurniture[i].GetComponent<Interactable>().ChangeRatio(noise, false);
        }

        if (mTimeBetweenNoise > mSpeedUp + 1f) mTimeBetweenNoise -= mSpeedUp;
    }
}
