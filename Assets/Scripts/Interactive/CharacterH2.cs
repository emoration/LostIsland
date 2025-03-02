﻿using System;
using Dialogue.Logic;
using UnityEngine;

namespace Interactive
{
    [RequireComponent(typeof(DialogueController))]
    public class CharacterH2 : Interactive
    {
        private DialogueController _dialogueController;

        private void Awake()
        {
            _dialogueController = GetComponent<DialogueController>();
        }

        public override void EmptyClicked()
        {
            if(isDone)
                _dialogueController.ShowDialogueFinish();
            else
                _dialogueController.ShowDialogueEmpty();
        }
        
        protected override void OnClickedAction()
        {
            _dialogueController.ShowDialogueFinish();
        }
    }
}