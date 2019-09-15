using System.IO;
using UnityEditor;

public class HLSLTemplateCreator
{
    private static string PostProcessingTemplatePath = "Assets/Templates/HLSLPostProcessingTemplate.txt";

    [MenuItem("Assets/Create/Shader/New PostProcessing Shader")]
    private static void CreatePostProcessingHLSLTemplate()
    {
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if(File.Exists(path))
            path = Path.GetDirectoryName(path);

        if(string.IsNullOrEmpty(path))
            path = "Assets/";

        File.Copy(PostProcessingTemplatePath, Path.Combine(path, "NewPostProcessingShader.shader"));
        AssetDatabase.Refresh();
    }
}
