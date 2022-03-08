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
            10, //"ReleaseClaim":10,
            11, //"ClaimObject":11,
            12, //"RejectClaim":12,
            13, //"ClaimObjectWithResponse":13,
            14, //"ClaimObjectResponse":14,
            15, //"SetObjectColor":15,
            16, //"DeleteObject"
            17, //"SetLocalPosition":17,
            18, //"IncrementLocalPosition":18,
            19, //"SetLocalRotation":19,
            20, //"IncrementLocalRotation":20,
            21, //"SetLocalScale":21,
            22, //"IncrementLocalScale":22,
            23, //"SetWorldPosition":23,
            24, //"IncrementWorldPosition":24,
            25, //"SetWorldRotation":25,
            26, //"IncrementWorldRotation":26,
            27, //"SetWorldScale":27,
            28, //"IncrementWorldScale":28,
            29,	//"SpawnPrefab":29,
            30, //SpawnPrimitive,
            31, //"SendFloats":31,
            32, //"SendTexture2D":32,
            38 //"TagUpdate":38,


        };
    }
}
