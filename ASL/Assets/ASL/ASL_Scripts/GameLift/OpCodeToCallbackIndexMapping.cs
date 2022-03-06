using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASL
{
    /*
     * Temporary file for maintaining un-fixed op functions to have no error.
     * The op code list below are the one fixed already.
     * TODO: Remove this file once the callback functionality has been added to all op functions.
     */
    public static class OpCodeToCallbackIndexMapping
    {
        public static HashSet<int> _CallbackIndex = new HashSet<int>
        {
            15, //"SetObjectColor":15,
            24, //"IncrementWorldPosition":24,
            26, //"IncrementWorldRotation":26,
            28,//"IncrementWorldScale":28,
        };
    }
}
