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

    //day 1 added ----------------------------------------------------
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

    public void NextBranch(int branchSelect)
    {

        //day 3 added ------------------------------------------------------------------------------------------
        DialogueBranch nextBranch = branch.ResponseOption[branchSelect].nextBranch;
        if (branch.ResponseOption[branchSelect].isInventoryResponse)
        {
            if (((InventoryResponse)branch.ResponseOption[branchSelect]).CheckInventory())
            {
                Debug.Log("InvetoryCheck");
            }
            else
            {
                nextBranch = ((InventoryResponse)branch.ResponseOption[branchSelect]).FailedCheckBranch;
            }
        }

        //day 3 changed -----------------------------------------------------------------------------------------
        RecieveDialogueBranch(nextBranch);


        ActiveDialogue();
        NextDialogue();
    }

    
    public void RecieveDialogueBranch(DialogueBranch newBranch)
    {
        branch = newBranch;
        responses = Mathf.Clamp(branch.ResponseOption.Count, 0, 3);
        currentIndex = 0;
    }

    public void NextDialogue()
    {
        if(currentIndex >= branch.DialogueLines.Count)
        {
            if(responses == 0)
            {
                DeactiveDialogue();
            }
            else
            {
                continueText.SetActive(false);
                for(int i = 0; i < responses; i += 1)
                {
                    if (i >= 3) break;
                    responsesHolder[i].gameObject.SetActive(true);
                    responsesHolder[i].GetComponentInChildren<TextMeshProUGUI>().text = branch.ResponseOption[i].Text;
                }
            }
        }
        else
        {
            mainText.GetComponent<TextMeshProUGUI>().text = branch.DialogueLines[currentIndex];
            continueText.SetActive(true);
            currentIndex += 1;
        }

    }
}
