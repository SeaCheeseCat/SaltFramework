
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// ABC文件打包後處理工具
/// </summary>
public class AbcBuildPostProcess : IPostprocessBuildWithReport
{
    /// <summary>
    /// Abc文件根目錄
    /// </summary>
    public const string AbcFolder = "";/*你可以填空，默認掃描全局abc文件。我的建議起碼填所有abc資源的根目錄或父目录，減少打包時的準備時間*/
    const string KUnsupportedTarget = "Alembic only supports the following build targets: Windows 64-bit, macOS X, and Linux 64-bit.";
    static readonly HashSet<KeyValuePair<string, string>> FilesToCopy = new();

    public int callbackOrder { get; }

    /// <summary>
    /// 打包后回调
    /// </summary>
    /// <param name="report"></param>
    public void OnPostprocessBuild(BuildReport report)
    {
        var target = report.summary.platform;
        if (!TargetIsSupported(target))
        {
            Debug.LogException(new Exception(KUnsupportedTarget));
            return;
        }

        //初始化 filesToCopy
        InitFilesToCopy(report.summary);

        Copy();
    }

    static void Copy()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var files in FilesToCopy)
        {
            if (!File.Exists(files.Key))
            {
                continue;
            }

            var dir = Path.GetDirectoryName(files.Value);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(files.Value))
            {
                var attrs = File.GetAttributes(files.Value);
                attrs &= ~FileAttributes.ReadOnly;
                File.SetAttributes(files.Value, attrs);
            }
            File.Copy(files.Key, files.Value, true);
            sb.Append(Path.GetFileName(files.Key) + ",");
        }

        Debug.Log($"PostProcess Copy ABC:{sb}");
        FilesToCopy.Clear();
    }

    static void InitFilesToCopy(BuildSummary summary)
    {
        var streamingAssetsPath = GetOutPutStreamingAssetsPath(summary);

        //get all abc files
        var abcFiles = Directory.GetFiles(Application.dataPath + AbcFolder, "*.abc", SearchOption.AllDirectories);
        int assetsIndex = 0;
        foreach (var abcFile in abcFiles)
        {
            assetsIndex = abcFile.IndexOf("/Assets", StringComparison.Ordinal);
            var localPath = abcFile.Substring(assetsIndex, abcFile.Length - assetsIndex);
            FilesToCopy.Add(new KeyValuePair<string, string>(abcFile, $"{streamingAssetsPath}{localPath}"));
        }
    }

    static string GetOutPutStreamingAssetsPath(BuildSummary summary)
    {
        switch (summary.platform)
        {
            case BuildTarget.StandaloneOSX:
                return Path.Combine(summary.outputPath, "Contents/Resources/Data/StreamingAssets");
            case BuildTarget.StandaloneLinux64:
            case BuildTarget.StandaloneWindows64:
                var name = Path.ChangeExtension(summary.outputPath, null);
                return name + "_Data/StreamingAssets";
            default:
                throw new NotImplementedException();
        }
    }

    static bool TargetIsSupported(BuildTarget target)
    {
        return target == BuildTarget.StandaloneOSX || target == BuildTarget.StandaloneWindows64 || target == BuildTarget.StandaloneLinux64;
    }
}
