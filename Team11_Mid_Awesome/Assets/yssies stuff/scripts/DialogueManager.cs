using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
	public Text dialogueText;

	public Animator animators;
    public Button nextSceneButton;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
        nextSceneButton.gameObject.SetActive(false); // Hide button at start
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animators.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
		}
	}

	void EndDialogue()
	{
        animators.SetBool("isOpen", false);
        nextSceneButton.gameObject.SetActive(true);
	}


}
