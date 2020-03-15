using UnityEngine;

namespace Mps
{
    public partial class AR_CSS451_Mp2_MainController_Shadows : MonoBehaviour
    {

        // reference to all UI elements in the Canvas
        public Camera MainCamera = null;
        public AR_CSS451_Mp2_XformControl_Shadows mXform = null;
        public AR_CSS451_Mp2_DropDownCreate_Shadows mCreateMenu = null;
        public AR_CSS451_Mp2_TheWorld_Shadows mModel = null;


        // Use this for initialization
        void Start()
        {
            Debug.Assert(MainCamera != null);
            Debug.Assert(mXform != null);
            Debug.Assert(mModel != null);
            Debug.Assert(mCreateMenu != null);
        }

        // Update is called once per frame
        void Update()
        {
            FingerSelect();
        }

        private void SelectObject(GameObject g)
        {
            GameObject a = mModel.SelectObject(g);
            mXform.SetSelectedObject(a);
        }
    }
}