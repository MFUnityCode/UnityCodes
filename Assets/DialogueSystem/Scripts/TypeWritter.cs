using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

/*
	Script by Mateus Francisco
	Description: This is a basic dialogue code, create your dialogues :)
*/
public class TypeWritter : MonoBehaviour {

	//Static Variable;
	public static TypeWritter instance;

	[Header("Settings")]
	[Range(0.001f, 0.3f)] public float delay;
	[SerializeField] private KeyCode skipKey;

	[Header("UI Components")]
	[SerializeField] private Text textBar;
	[SerializeField] private Text characterName;
	[SerializeField] private Text endIndicator;
	[SerializeField] private GameObject hud;

	private string text; 
	private char[] letters;
	private string[] dialogues;
	private bool nomepersonagem = true;
	private bool skipDelay = false;
	private bool controle_space = false; 
	private bool stopTalk;

	void Awake() {
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else{
			Destroy(gameObject);
		}
	}

	void Start(){

		//KeyCode to skip dialogue;
		//skipKey = KeyCode.Space;

		if(!Directory.Exists(Path.Combine(Application.dataPath, @"Assets/StreamingAssets"))){
			Directory.CreateDirectory(@"Assets/StreamingAssets");
		}
	}//end: Start

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (controle_space) {
				skipDelay = true;
			}
		}
	}//end: Update;

    public void Type(string fileName){
        //read file .txt
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        text = File.ReadAllText (filePath);

		textBar.text = "";

		//begin coroutine;
		StartCoroutine (Typerwrite());
	}//end: Type;

	private IEnumerator Typerwrite(){

		text.Replace ("\r\n", "");
		dialogues = text.Split ('@');

		stopTalk = false;

		for (int l = 0; l < dialogues.Length; l++) {

			string[] subDialogue = dialogues [l].Split ('/');
			string nome = subDialogue [0].Replace ("\r\n", "");
			characterName.text = nome;

			letters = subDialogue [1].ToCharArray();
			
			for (int i = 0; i < letters.Length; i++) {
				controle_space = true;
				textBar.text += letters [i];

				if (skipDelay != true) {
					yield return new WaitForSeconds (delay);	
				}
			}
			yield return new WaitForSeconds (0.1f);

			while (Input.GetKeyDown (skipKey) == false) {
				controle_space = false;
				yield return new WaitForSeconds (0.005f);
				endIndicator.text = "Press " + skipKey;
			}

			textBar.text = "";
			characterName.text = "";
			endIndicator.text = "";
			skipDelay = false;

		}
		hud.SetActive (false);

		stopTalk = true;
		letters = text.ToCharArray ();
	}//end: Typerwriter;
}
