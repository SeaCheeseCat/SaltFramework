using System.IO;
using UnityEditor;
using UnityEngine;

public class MyAssetPostprocessor : AssetPostprocessor
{
    void OnPreprocessAsset()
    {
        if (assetImporter.importSettingsMissing)
        {
            var fileName = Path.GetFileName(assetImporter.assetPath);
            var fileType = 0;
            if (fileName.Contains(".jpg") || fileName.Contains(".png")|| fileName.Contains(".gif"))
                fileType = 4;
            else if (fileName.Contains(".mpc3") || fileName.Contains(".wav") )
                fileType = 1;
            else if (fileName.Contains(".mat"))
                fileType = 3;
            else if(fileName.Contains(".fbx"))
                fileType = 5;
            else
                return;
            Rect _rect = new Rect(0, 0, 350, 150);
            ImportFileEdit window = (ImportFileEdit)EditorWindow.GetWindowWithRect(typeof(ImportFileEdit), _rect, true, "资源导入");
            window.Show();
            window.InitFile(assetImporter.assetPath, fileType);
        }
    }
}
