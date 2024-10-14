using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaglogue_trigger : MonoBehaviour
{
    public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
