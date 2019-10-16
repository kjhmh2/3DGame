using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface Scene
    {
        void LoadResources();
    }

    public interface Objects
    {
        void MoveBoat();
        void ObjectIsClicked(GameObjects characterCtrl);
        void Restart();
    }

}