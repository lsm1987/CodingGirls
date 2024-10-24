using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using System.IO;

/// <summary>
/// iOS 빌드 시 Info.plist 수정.<br />
/// <see href="https://gist.github.com/TiborUdvari/4679d636b17ddff0d83065eefa399c04"/> 참고
/// </summary>
public class IosInfoPlistPostprocessBuild : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.iOS)
        {
            ModifyInfoPlist(report.summary.outputPath);
        }
    }

    private static void ModifyInfoPlist(string buildOutputPath)
    {
        Debug.Log($"BuildOutputPath: {buildOutputPath}");

        string plistPath = buildOutputPath + "/Info.plist";
        Debug.Log($"Info.plist Path: {plistPath}");

        PlistDocument plist = new PlistDocument(); // Read Info.plist file into memory
        plist.ReadFromString(File.ReadAllText(plistPath));

        PlistElementDict rootDict = plist.root;
        rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false); 

        File.WriteAllText(plistPath, plist.WriteToString()); // Override Info.plist
        
        Debug.Log("IosInfoPlistPostprocessBuild Done");
    }
}
