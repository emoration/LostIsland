using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.Data
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "SO/Dialogue/DialogueData_SO")]
    public class DialogueData_SO : ScriptableObject
    {
        public List<string> dialogueLines;
    }
}