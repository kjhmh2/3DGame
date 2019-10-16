using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ISceneController
    {
        void LoadResources();
    }

    public interface UserAction
    {
        void Hit(Vector3 pos);
        int GetScore();
        int GetRound();
        int GetHealth();
        void ReduceHealth();
        void GameOver();
        bool RoundStop();
        void Restart();
    }

    public enum SSActionEventType : int { Started, Completed }

    public interface SSActionCallback
    {
        void SSActionCallback(SSAction source, bool isHit);
    }

    public interface IActionManager
    {
        void PlayDisk(Disk Disk);
        bool IsAllFinished();
    }
}