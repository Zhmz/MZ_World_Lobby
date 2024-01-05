using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tuwan
{
    namespace Chat
    {
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string thumb { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string image { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public float ratio { get; set; }
        }

        public class UploadImg
        {
            /// <summary>
            /// 
            /// </summary>
            public int error { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
        }
    }
}
