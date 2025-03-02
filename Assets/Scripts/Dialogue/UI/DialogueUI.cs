using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Dialogue.UI
{
    public class DialogueUI : MonoBehaviour
    {
        public GameObject panel;
        public Text dialogueText;

        private void OnEnable()
        {
            EventHandler.ShowDialogueEvent += ShowDialogue;
        }

        private void OnDisable()
        {
            EventHandler.ShowDialogueEvent -= ShowDialogue;
        }

        private void ShowDialogue(string dialogue)
        {
            panel.SetActive(dialogue != string.Empty);
            dialogueText.text = dialogue;
        }
    }
}