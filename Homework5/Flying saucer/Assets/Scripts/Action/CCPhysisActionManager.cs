﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class CCPhysisActionManager : SSActionManager, SSActionCallback, IActionManager
{
    int count = 0;
    public SSActionEventType Complete = SSActionEventType.Completed;
    UserAction UserActionController;

    public void PlayDisk(Disk Disk)
    {
        Debug.Log("CCPhysisActionManager");
        count ++;
        Complete = SSActionEventType.Started;
        CCPhysisAction action = CCPhysisAction.getAction(Disk.speed);
        addAction(Disk.gameObject, action, this);
    }

    public void SSActionCallback(SSAction source, bool isHit)
    {
        count --;
        Complete = SSActionEventType.Completed;
        UserActionController = SSDirector.getInstance().currentScenceController as UserAction;
        if (!isHit)
        {
            UserActionController.ReduceHealth();
        }
        source.gameObject.SetActive(false);
    }

    public bool IsAllFinished()
    {
        Debug.Log("isALLFInished");
        if (count == 0)
            return true;
        else return false;
    }

}
