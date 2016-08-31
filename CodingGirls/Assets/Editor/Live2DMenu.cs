using UnityEditor;
using UnityEngine;

public static class Live2DMenu
{
    [MenuItem("Live2D/Create Plane 2x2")]
    private static void CreatePlane2x2()
    {
        CreatePlane(2.0f);
    }

    private static void CreatePlane(float size)
    {
        float radius = size / 2.0f;
        string filename = "Plane_" + size.ToString();
        Mesh mesh = new Mesh();
        mesh.name = filename;

        mesh.vertices = new Vector3[]
        {
            new Vector3 (-radius, 0.0f,  radius),
            new Vector3 ( radius, 0.0f,  radius),
            new Vector3 ( radius, 0.0f, -radius),
            new Vector3 (-radius, 0.0f, -radius)
        };
        mesh.triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };
        mesh.uv = new Vector2[]
        {
            new Vector2 (0.0f, 1.0f),
            new Vector2 (1.0f, 1.0f),
            new Vector2 (1.0f, 0.0f),
            new Vector2 (0.0f, 0.0f)
        };

        mesh.RecalculateNormals();  //  法線の再計算
        mesh.RecalculateBounds();   //  バウンディングボリュームの再計算
        mesh.Optimize();

        if (!System.IO.File.Exists("Assets/Resources/Live2D/" + filename + ".asset"))
        {
            System.IO.Directory.CreateDirectory("Assets/Resources/Live2D");
            AssetDatabase.CreateAsset(mesh, "Assets/Resources/Live2D/" + filename + ".asset");
            AssetDatabase.SaveAssets();
        }
        mesh = null;
    }

    [MenuItem("Live2D/Create ModelInfo")]
    private static void CreateModelInfo()
    {
        L2DModelInfo info = new L2DModelInfo();
        info._modelName = "Epsilon";
        info._settingPath = "Epsilon_free/Epsilon_free.model.json";
        info._planeSize = 2.0f;
        info._layout._scale = 1.0f;
        info._layout._y = 0.85f;

        string json = JsonUtility.ToJson(info, true);
        const string filename = "Epsilon";
        string filepath = Define.L2D._modelInfoRoot + "/" + filename;
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + filepath + ".json", json);
        AssetDatabase.Refresh();

        // 테스트
        TextAsset loadedJson = Resources.Load<TextAsset>(filepath);
        L2DModelInfo loadedInfo = JsonUtility.FromJson<L2DModelInfo>(loadedJson.text);
        if (info.IsSame(loadedInfo))
        {
            Debug.Log("ModelInfo created successfully " + filepath);
        }
        else
        {
            Debug.LogError("ModelInfo created but not same " + filepath);
        }
    }
}