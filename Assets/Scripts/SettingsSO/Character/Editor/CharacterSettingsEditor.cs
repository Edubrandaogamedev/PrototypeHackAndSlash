using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CharacterSettings))]
public class CharacterSettingsEditor : Editor
{
    private new CharacterSettings target;
    private void OnEnable()
    {
        target = (CharacterSettings) base.target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        for (var i = 0; i < target.ScriptableActions.Length; i++)
        {
            var activateWarning = true;
            for (var j = 0; j < target.ActionSettings.Length; j++)
            {
                if (!target.ScriptableActions[i].settingNeeded)
                {
                    activateWarning = false;
                    break;
                }
                if (target.ScriptableActions[i]?.Key == target.ActionSettings[j]?.Key)
                {
                    activateWarning = false;
                }
            }
            var actionName = target.ScriptableActions[i]?.name;
            if (activateWarning)
            {
                EditorGUILayout.HelpBox($"There is no settings for the {actionName} you need to add a setting to this action at the settings array",MessageType.Warning,true);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
