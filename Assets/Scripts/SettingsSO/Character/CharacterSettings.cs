using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Settings", menuName = "Game/Settings/Character/Protagonist")]
public class CharacterSettings : Settings
{
    [SerializeField] private NavAgentSettings agentOverride;
    [SerializeField] private CharacterActionSO[] scriptableActions;
    [SerializeField] private ActionSetting[] actionSettings;
    private Dictionary<CharacterActionSO, ActionSetting> actionSettingDict = new Dictionary<CharacterActionSO, ActionSetting>();
    private void OnDisable()
    {
        actionSettingDict = new Dictionary<CharacterActionSO, ActionSetting>();
    }
    public NavAgentSettings AgentOverride => agentOverride;
    public CharacterActionSO[] ScriptableActions => scriptableActions;
    public ActionSetting[] ActionSettings => actionSettings;
    public ActionSetting GetSettingForAction(CharacterActionSO scriptableAction)
    {
        if (actionSettingDict.ContainsKey(scriptableAction))
        {
            return actionSettingDict[scriptableAction];
        }
        foreach (var action in scriptableActions)
        {
            var setting = ActionSettings.FirstOrDefault(settings => settings.Key == action.Key);
            if (setting == null) return null;
            actionSettingDict.Add(action, setting);
            return actionSettingDict[scriptableAction];
        }
        return null;
    }
}