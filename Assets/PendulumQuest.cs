using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumQuest : Quest
{
    GameObject mainObject;
    GameObject ball;
    private void Start()
    {
        questSteps = _questSteps.Count;

        if (!_isActive) return;
        QuestManager.instance.addQuestStepsToUI(questSteps, this);
    }
    private void Update()
    {
        if (QuestManager.instance.mainObject && !mainObject)
        {
            mainObject = QuestManager.instance.mainObject;
            ball = mainObject.transform.GetChild(0).gameObject;
        }
        if (!_isActive) return;
        check();
    }
    public override void check()
    {
        if (!_isActive) return;
        if (_isFinished) return;
        _isFinished = QuestManager.instance.isFinished(questSteps, activeStep);
        if (activeStep == questSteps)
        {
            Debug.Log("finished");
            return;
        }

        activeStep = QuestManager.instance.isCorrect(openInventoryPanel(0), activeStep, questSteps, _questSteps[activeStep].grade);
        activeStep = QuestManager.instance.isCorrect(havePendulum(1), activeStep, questSteps, _questSteps[activeStep].grade);
        activeStep = QuestManager.instance.isCorrect(onTable(2), activeStep, questSteps, _questSteps[activeStep].grade);
        activeStep = QuestManager.instance.isCorrect(holdTheBall(3), activeStep, questSteps, _questSteps[activeStep].grade);

    }
    bool openInventoryPanel(int x)
    {
        if (activeStep == x && UI_Manager.instance.inventoryPanel.activeInHierarchy) return true;
        return false;
    }
    bool havePendulum(int x)
    {
        if (activeStep == x && mainObject) return true;
        return false;
    }
    bool onTable(int x)
    {
        if (activeStep == x && mainObject && mainObject.GetComponent<DragableItem>().onTable) return true;
        return false;
    }
    bool holdTheBall(int x)
    {
        if (!mainObject) return false;
        if (activeStep == x && ball.GetComponent<DragableItem>().isDragging)
        {
            return true;
        }
        return false;
    }
}
