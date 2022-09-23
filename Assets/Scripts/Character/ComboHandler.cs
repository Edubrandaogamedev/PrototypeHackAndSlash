using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    private ComboSequenceData[] comboData;
    private ComboSequenceData _currentComboSequence;
    private bool goToNextSequence;
    private int currentComboId;

    public event Action<int,ComboSequenceData> OnComboStarted = delegate {};
    public event Action<int,ComboSequenceData> OnComboTransiction = delegate {};
    public event Action<int,ComboSequenceData> OnComboEnded = delegate {};

    public bool IsOnCombo { get; private set;}

    public void Setup(ComboSequenceData[] comboData)
    {
        this.comboData = comboData;
    }
    public async Task StartCombo()
    {
        IsOnCombo = true;
        currentComboId = 1;
        _currentComboSequence = comboData[currentComboId - 1];
        OnComboStarted(currentComboId,_currentComboSequence);
        await CheckNextSequence();
    }

    public async Task<bool> CheckNextSequence()
    {
        await Task.Delay(1000);
        return goToNextSequence;
    }
    public async Task NextSequence()
    {
        await CheckNextSequence();
        currentComboId++;
        _currentComboSequence = comboData[currentComboId - 1];
        OnComboTransiction(currentComboId,_currentComboSequence);
    }
    public void EndCombo()
    {
        IsOnCombo = false;
        currentComboId = 0;
        _currentComboSequence = default;
        OnComboEnded(currentComboId,_currentComboSequence);
    }
}
