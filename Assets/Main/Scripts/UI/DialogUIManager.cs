using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    public GameObject background;
    public GameObject mainText;
    public GameObject responseTab;
    public GameObject continueText;
    public List<Button> responsesHolder;

    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int responses = 0;

    //day 1 added --------------------------------------------------------
    private DialogueBranch branch;


    // Start is called before the first frame update
    void Start()
    {
        DeactiveDialogue();
    }

    public void ActiveDialogue()
    {
        background.SetActive(true);
        mainText.SetActive(true);
        continueText.SetActive(true);
        responseTab.SetActive(true);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    void DeactiveDialogue()
    {
        background.SetActive(false);
        mainText.SetActive(false);
        continueText.SetActive(false);
        responseTab.SetActive(false);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    //day 1 added ---------------------------------------------------------------------
    public void NextBranch(int branchSelect)
    {
        RecieveDialogueBranch(branch.ResponseOption[branchSelect].nextBranch);
        ActiveDialogue();
        NextDialogue();
    }


    //day 1 added ---------------------------------------------------------------------
    public void RecieveDialogueBranch(DialogueBranch newBranch)
    {
        this.branch = newBranch;
        responses = Mathf.Clamp(branch.ResponseOption.Count, 0, 3);
        currentIndex = 0;
    }

    //day 1 changed ---------------------------------------------------------------------
    public void NextDialogue()
    {
        // If at end of branch
        if (currentIndex >= branch.DialogueLines.Count)
        {
            // No response -> End dialogue
            if (responses == 0)
            {
                DeactiveDialogue();
            }
            else
            {
                // Responses -> Activate Buttons
                continueText.SetActive(false);
                for (int i = 0; i < responses; i++)
                {
                    if (i >= 3)
                    {
                        break;
                    }

                    // Activate the button and place response text
                    responsesHolder[i].gameObject.SetActive(true);
                    responsesHolder[i].GetComponentInChildren<TextMeshProUGUI>().text = branch.ResponseOption[i].Text;
                }
            }
        }
        // There is still more text to show
        else
        {
            mainText.GetComponent<TextMeshProUGUI>().text = branch.DialogueLines[currentIndex];
            continueText.SetActive(true);
            currentIndex++;
        }


    }
}
