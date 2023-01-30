using UnityEditor;
using UnityEngine;

namespace Additions.RetractableUI.Editor
{
    [CustomEditor(typeof(RetractableBase), true)]
    public class RetractableBaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var retractable = (RetractableBase)target;

            if (GUILayout.Button("Copy Show Position")) retractable.CopyShowPosition();

            if (GUILayout.Button("Copy Hidden Position")) retractable.CopyHiddenPosition();
        }
    }
}