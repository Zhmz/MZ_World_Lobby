#if UNITY_IPHONE
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.IO;
using UnityEditor.iOS.Xcode;
using System.Collections.Generic;
using System.Linq;

public class XCodeConfig : MonoBehaviour
{

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

            ArrayList libArray = new ArrayList();
            libArray.Add("libc++.tbd");
            libArray.Add("libz.1.2.8.tbd");
            libArray.Add("libc++.1.tbd");

            ArrayList frameArray = new ArrayList();

            frameArray.Add("CoreTelephony.framework");
            frameArray.Add("AvFoundation.framework");
            frameArray.Add("SystemConfiguration.framework");
            frameArray.Add("WebKit.framework");
            frameArray.Add("JavaScriptCore.framework");
            frameArray.Add("CoreGraphics.framework");
            frameArray.Add("ImageIO.framework");
            frameArray.Add("CoreFoundation.framework");


            Hashtable otherLinkTable = new Hashtable();
            ArrayList otherLinkArray = new ArrayList();
            otherLinkArray.Add("-ObjC");
            otherLinkArray.Add("-fprofile-instr-generate");
            otherLinkArray.Add("-lz");
            otherLinkTable.Add("OTHER_LDFLAGS", otherLinkArray);

            //lib
            SetLibs(proj, libArray);
            //framework
            SetFrameworks(proj, frameArray);
            //other Link
            UpdateBuildPropertier(proj, otherLinkTable);
            //写入
            File.WriteAllText(projPath, proj.WriteToString());

        }
    }
    private static void AddLibToProject(PBXProject inst, string targetGuid, string lib)
    {
        string fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
        inst.AddFileToBuild(targetGuid, fileGuid);
    }
    private static void RemoveLibFromProject(PBXProject inst, string targetGuid, string lib)
    {
        string fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
        inst.RemoveFileFromBuild(targetGuid, fileGuid);
    }
    //设置frameworks
    private static void SetFrameworks(PBXProject proj, ArrayList table)
    {
        if (table != null)
        {

#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityFrameworkTargetGuid();
#else
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
            foreach (string i in table)
            {
                proj.AddFrameworkToProject(target, i, false);
            }

        }
    }
    //设置libs
    private static void SetLibs(PBXProject proj, ArrayList table)
    {
        if (table != null)
        {
#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityFrameworkTargetGuid();
#else
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
            foreach (string i in table)
            {
                AddLibToProject(proj, target, i);
            }


        }
    }
    //设置编译属性
    private static void SetBuildProperties(PBXProject proj, Hashtable table)
    {
        if (table != null)
        {
#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityFrameworkTargetGuid();
#else
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif

            foreach (DictionaryEntry i in table)
            {
                proj.SetBuildProperty(target, i.Key.ToString(), i.Value.ToString());
            }

        }
    }

    private static void UpdateBuildPropertier(PBXProject proj, Hashtable table)
    {
        if (table != null)
        {
#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityFrameworkTargetGuid();
#else
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
            foreach (DictionaryEntry i in table)
            {
                ArrayList array = i.Value as ArrayList;
                List<string> list = new List<string>();
                foreach (var flag in array)
                {
                    list.Add(flag.ToString());
                }
                proj.UpdateBuildProperty(target, i.Key.ToString(), list, null);
            }

        }
    }

}
#endif