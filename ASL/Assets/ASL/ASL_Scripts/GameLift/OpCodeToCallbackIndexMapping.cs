using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASL
{
    public static class OpCodeToCallbackIndexMapping
    {
        public static Dictionary<int, int> _CallbackIndex = new Dictionary<int, int>
    {
        {15, 4}, //"SetObjectColor":15,
        {24, 2}, //"IncrementWorldPosition":24,
        {26, 2}, //"IncrementWorldRotation":26,
        {28, 2},//"IncrementWorldScale":28,
    };
    }
}
