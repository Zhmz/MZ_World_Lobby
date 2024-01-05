using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;

namespace Home
{
    public class HomeStarter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameEntry.UI.OpenUIForm(UIFormId.HomeForm);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
