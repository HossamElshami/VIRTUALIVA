using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCubeQuest : Quest
{    
    [SerializeField]
    GameObject cube;

    private void Start()
    {
        questSteps = _questSteps.Count;

        if (!_isActive) return;
        QuestManager.instance.addQuestStepsToUI(questSteps, this);
    }

    private void Update()
    {
        if (!_isActive) return;
        check();
    }
    public override void check()
    {
        if (!_isActive) return;
        if (_isFinished) return;
        _isFinished = QuestManager.instance.isFinished(questSteps, activeStep);

        activeStep = QuestManager.instance.isCorrect(isHolding(), activeStep, questSteps, _questSteps[activeStep].grade);
        activeStep = QuestManager.instance.isCorrect(onTheCorrectTable(), activeStep, questSteps, _questSteps[activeStep].grade);
    }

    bool isHolding()
    {
        if (activeStep == 0 && cube.GetComponent<DragableItem>().isDragging) return true;
        return false;
    }
    bool onTheCorrectTable()
    {
        if (activeStep == 1 && cube.GetComponent<DragableItem>().onTable) return true;
        return false;
    }
}
