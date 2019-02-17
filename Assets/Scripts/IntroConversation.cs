using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroConversation : MonoBehaviour
{
	private IEnumerator coroutine;
	private string[] speech = {
		"Living matter\nevades the decay\nto equilibrium", 
		"All the furniture\nin my lovely living\nroom is |dead>+|alive>\nsimultaneously!",
		"So nice and cosy!\nI like it to remain\nundetermined\nforever!",
		"No way!",
		"Show Schroedinger who owns the living room!\nScratch an equal number of undetermined\nfurniture to turn it dead or alive.\nTime’s running!",
		"But be aware that Schrödinger\nwill be back with some quantum noise", 
		"And then,\nnothing will be\ntrivial anymore!\nMuahahahah!"};
	private float[] wait = {2.5f, 2.5f, 2.5f, 2f, 4f, 3f, 3f};
	private int[] order = {0, 0, 0, 1, 2, 2, 0};

	private GameObject spriteSch;
	private GameObject spriteCat;
	private TextMesh textSch;
	public GameObject textCat;
	public GameObject textGameInstr;
	public GameObject Schr;
	public GameObject CatAlive;
	public GameObject CatDead;

	private IEnumerator SpeechBubble() {

		print("couroutine!");

		for (int i = 0; i < speech.Length; i++) {
			
			yield return new WaitForSeconds(0.5f);

			// schroedinger
			if (order[i] == 0) {

				spriteSch.SetActive(true);
				textSch.text = speech[i];
				Schr.GetComponent<SchrController>().SetTalking();

			// cat
			} else if (order[i] == 1) {

				spriteCat.SetActive(true);
				textCat.GetComponent<TextMesh>().text = speech[i];
				//CatDead.GetComponent<SchrController>().SetTalking();
				//CatAlive.GetComponent<SchrController>().SetTalking();
				
			// instructions
			} else {
				
				textGameInstr.GetComponent<TextMesh>().text = speech[i];

			}
					
			yield return new WaitForSeconds(wait[i]);

			// schroedinger
			if (order[i] == 0) {

				spriteSch.SetActive(false);
				textSch.text = "";
				Schr.GetComponent<SchrController>().SetStanding();

				// cat
			} else if (order[i] == 1) {

				spriteCat.SetActive(false);
				textCat.GetComponent<TextMesh>().text = "";
				//CatDead.GetComponent<SchrController>().SetStanding();
				//CatAlive.GetComponent<SchrController>().SetStanding();

				// instructions
			} else {

				textGameInstr.GetComponent<TextMesh>().text = "";

			}
		}

	}


    // Start is called before the first frame update
    void Start()
    {
		spriteSch = GameObject.Find("SpeechBubbleSchr");
		spriteCat = GameObject.Find("SpeechBubbleCat");
		spriteSch.SetActive(false);
		spriteCat.SetActive(false);

		textSch = GetComponent<TextMesh>();
		//textGameInstr = GameObject.Find("GameInstructions");
		//textCat = GameObject.Find("Cat");

		coroutine = SpeechBubble();
		StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
