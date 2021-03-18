using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static bool IsDialogue;
    public string[] sentences;
    public int currentSentence;
    public Text nameText;
    public Text dialogText;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        IsDialogue = false;
        animator.SetBool("IsOpen", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&IsDialogue)
        {
            DisplayNextSentences();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        IsDialogue = true;
        animator.SetBool("IsOpen", true);
        //Debug.Log("Starting with" + dialogue.name);
        currentSentence = 0;
        nameText.text = dialogue.name;
        sentences = dialogue.sentences;
        DisplayNextSentences();
    }
    public void DisplayNextSentences()
    {
        if (sentences.Length != 0 && currentSentence < sentences.Length)
        {
            StartCoroutine(TypeSentence(sentences[currentSentence++]));
            IsDialogue = true;
        }
        else
            EndDialogue();
    }
    public void DisplaySentences(int sentenceNumb)
    {
        if (sentences.Length != 0 && sentenceNumb < sentences.Length && sentenceNumb >= 0)
        {
            StartCoroutine(TypeSentence(sentences[sentenceNumb]));
            IsDialogue = true;
        }
        else
            EndDialogue();
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        IsDialogue = false;
        animator.SetBool("IsOpen", false);
    }
}
