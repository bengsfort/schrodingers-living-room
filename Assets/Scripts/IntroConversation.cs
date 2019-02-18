using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroConversation : MonoBehaviour
{
	private IEnumerator coroutine;
	private string[] speech = {
		"Living matter\nevades the decay\nto equilibrium", 
		"All the furniture\nin my lovely living\nroom is |dead>+|alive>\nsimultaneously!",
		"So nice and cosy!\nI like it to remain\nundetermined\nforever!",
		"No way!",
		"Show Schroedinger who owns the living room!\nScratch an equal number of undetermined\nfurniture to turn it dead or alive.\nTime’s running!",
		"But be aware...\nSchroedinger will be back with some quantum\nnoise!", 
		"And then,\nnothing will be\ntrivial anymore!\nMuahahahah!"};
	private float[] wait = {3.5f, 3.5f, 3.5f, 1.5f, 6f, 4f, 3.5f};
	private int[] order = {0, 0, 0, 1, 2, 2, 0};

	public AudioClip audioAlive;
	public AudioClip audioDead;
	private AudioSource audioSource;

	// speech bubble
	private GameObject spriteSch;
	private GameObject spriteCat;
	// text in speech bubble
	private TextMesh textSch;
	public GameObject textCat;
	public GameObject textGameInstr;

	// prefabs
	public GameObject Schr;
	public GameObject CatAlive;
	public GameObject CatDead;

	public void LoadMain() {
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}


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

				audioSource.PlayOneShot(audioAlive, 0.7F);
				audioSource.PlayOneShot(audioDead, 0.7F);
				spriteCat.SetActive(true);
				textCat.GetComponent<TextMesh>().text = speech[i];
				
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
				textCat.GetComponent<TextMesh>().text = "";;

				// instructions
			} else {

				textGameInstr.GetComponent<TextMesh>().text = "";

			}
		}

		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}


    // Start is called before the first frame update
    void Start()
    {
		// audio
		audioSource = GetComponent<AudioSource>();
		// speechbubble sprites, hidden at first, activated when textmesh is activated
		spriteSch = GameObject.Find("SpeechBubbleSchr");
		spriteCat = GameObject.Find("SpeechBubbleCat");
		spriteSch.SetActive(false);
		spriteCat.SetActive(false);

		textSch = GetComponent<TextMesh>();

		coroutine = SpeechBubble();
		StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey("space")) {
			LoadMain ();
		}
    }
}
