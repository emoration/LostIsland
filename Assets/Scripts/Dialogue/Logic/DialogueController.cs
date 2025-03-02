using System.Collections;
using Dialogue.Data;
using UnityEngine;
using Utils;

namespace Dialogue.Logic
{
    public class DialogueController : MonoBehaviour
    {
        public DialogueData_SO dialogueEmpty;
        public DialogueData_SO dialogueFinish;
        private bool _isTalking;
        private int _dialogueEmptyIndex;
        private int _dialogueFinishIndex;

        public void ShowDialogueEmpty()
        {
            if (!_isTalking)
                StartCoroutine(ShowDialogue(dialogueEmpty, _dialogueEmptyIndex++));
        }

        public void ShowDialogueFinish()
        {
            if (!_isTalking)
                StartCoroutine(ShowDialogue(dialogueFinish, _dialogueFinishIndex++));
        }

        private IEnumerator ShowDialogue(DialogueData_SO dialogueData, int index)
        {
            _isTalking = true;
            if (dialogueData.dialogueLines.Count == 0)
            {
                _isTalking = false;
                EventHandler.CallShowDialogueEvent(string.Empty);
                Debug.LogWarning("No dialogue in " + dialogueData.name);
                EventHandler.CallGameStateChangeEvent(GameState.Gameplay);
                yield break;
            }

            if (index >= dialogueData.dialogueLines.Count)
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                EventHandler.CallGameStateChangeEvent(GameState.Gameplay);
                _isTalking = false;
                yield break;
            }

            EventHandler.CallShowDialogueEvent(dialogueData.dialogueLines[index]);
            EventHandler.CallGameStateChangeEvent(GameState.Paused);
            _isTalking = false;
        }
    }
}