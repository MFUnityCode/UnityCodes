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
	public Image imagem_Personagem;
	public Image imagem_Dialogo;
	private bool nomepersonagem = true;
	private bool skipDelay = false;
	private bool controle_space = false; 
	private bool stopTalk;
	[Range(0f, 1f)] public float delay;
	private GameObject myGo;

	void Start(){
		GameObject myGo = new GameObject();
        Canvas canvas = myGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler cs = myGo.AddComponent<CanvasScaler>();
		cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		cs.referenceResolution = new Vector2(1280, 720);

        GraphicRaycaster gr = myGo.AddComponent<GraphicRaycaster>();
        myGo.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        myGo.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

        GameObject g2 = new GameObject();
        g2.name = "Text";
        g2.transform.parent = myGo.transform;

        Text t = g2.AddComponent<Text>();
        g2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        g2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
        t.alignment = TextAnchor.MiddleCenter;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;

        Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
        t.font = ArialFont;
        t.fontSize = 7;
        t.text = "Test";
        t.enabled = true;
        t.color = Color.black;
 
        myGo.name = "CanvasTypeWritter";
        bool bWorldPosition = false;
 
        myGo.GetComponent<RectTransform>().SetParent(this.transform, bWorldPosition);
        //g.transform.localPosition = new Vector3(0f, fTextLabelHeight, 0f);
        myGo.transform.localScale = new Vector3(1.0f / this.transform.localScale.x * 0.1f, 1.0f / this.transform.localScale.y * 0.1f, 1.0f / this.transform.localScale.z * 0.1f );

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
