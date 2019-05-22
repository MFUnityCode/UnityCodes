using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTeller : MonoBehaviour {

	private TypeWritter typeWritter;

	void Start () {
		typeWritter = TypeWritter.instance;
		typeWritter.Type("DialogueTest1.txt");
	}
}
