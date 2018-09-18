using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

/*
	Script by Mateus Francisco
	Description: This is a code...
 */
public class TypeWritter : MonoBehaviour {

	private string text; 
	private char[] letters;
	private string[] dialogues;
	public Text textBar;
	public Text characterName;
	public Text endIndicator;
	public GameObject hud;
	private bool nomepersonagem = true;
	private bool skipDelay = false;
	private bool controle_space = false; 
	private bool stopTalk;
	[Range(0f, 0.5f)] public float delay;

	void Start(){
		if(!Directory.Exists(Path.Combine(Application.dataPath, @"Assets/StreamingAssets"))){
			Directory.CreateDirectory(@"Assets/StreamingAssets");
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (controle_space) {
				skipDelay = true;
			}
		}
	}

    public void Type(string fileName){
        //read file .txt
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        text = File.ReadAllText (filePath);

		textBar.text = "";

		//begin coroutine;
		StartCoroutine (Typerwrite());
	}


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

			while (Input.GetKeyDown (KeyCode.Space) == false) {
				controle_space = false;
				yield return new WaitForSeconds (0.005f);
				endIndicator.text = "Press Space";
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
