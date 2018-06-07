using UnityEngine;
using UnityEditor;

/// <summary>
/// Essse script acessa as arvores instanciadas pelo terreno e as substitui pelos objetos setados na variaval tree.
/// Associe um terreno de uma cena, jogue os objetos que substituirão as árvores em trees e aperte Convert to objects
/// </summary>
[ExecuteInEditMode]
public class CustomTreeBrush : EditorWindow
{

    Terrain terrain;

    GameObject[] trees = new GameObject[4];


    [MenuItem("Tools/Custom/Terrain")]
    static void Init()
    {
        CustomTreeBrush window = (CustomTreeBrush)GetWindow(typeof(CustomTreeBrush));
    }

    void OnGUI()
    {
        terrain = (Terrain)EditorGUILayout.ObjectField(terrain, typeof(Terrain), true);

        trees[0] = (GameObject)EditorGUILayout.ObjectField(trees[0], typeof(GameObject), true);
        trees[1] = (GameObject)EditorGUILayout.ObjectField(trees[1], typeof(GameObject), true);
        trees[2] = (GameObject)EditorGUILayout.ObjectField(trees[2], typeof(GameObject), true);
        trees[3] = (GameObject)EditorGUILayout.ObjectField(trees[3], typeof(GameObject), true);

        if (GUILayout.Button("Convert to objects"))
        {
            Convert();
        }
    }

    public void Convert()
    {
        TerrainData data = terrain.terrainData;
        TreeInstance tree;
        GameObject instantiatedPrefab;
        float width = data.size.x;
        float height = data.size.z;
        float y = data.size.y;
        int instanctiatedTrees = 0;
        for(int i = 0; i < data.treeInstances.Length; i++)
        {
            if (i % 5 == 0)
            {
                tree = data.treeInstances[i];
                Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);
                //usamos IntantiatePrefab para que o objeto instanciado esteja linkado à um prefab. 
                //Modificações no prefab aterarão todos os objetos instanciados por esse script
                instantiatedPrefab = (GameObject) PrefabUtility.InstantiatePrefab(trees[tree.prototypeIndex]);
                instantiatedPrefab.transform.position = position;
                instanctiatedTrees++;
            }
        }
        Debug.Log(instanctiatedTrees + " trees instantiated");
    }

}