using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Shadow editor allows quick overview of Renderers in selected GameObject.
/// </summary>
public class ShadowEditor : EditorWindow 
{
    private const int COLUMN_COUNT = 6;
    private const float TOGGLE_WIDTH = 70;
    private const float CASTING_MODE_WIDTH = 90;
    
    public static ShadowEditor window;   // The static instance of the window
    private static Vector2 scrollPosition;
    private GameObject selectedGameObject;
    
    [MenuItem ("Window/Shadow Editor")]
    public static void Init ()
    {
        // Get existing open window or if none, make new one
        window = (ShadowEditor)EditorWindow.GetWindow (typeof (ShadowEditor));
        window.titleContent = new GUIContent ("Shadow Editor");
    }
    
    void OnGUI ()
    {
        if (window == null)
            Init ();
        
        if (Selection.activeGameObject != null)
        {
            selectedGameObject = Selection.activeGameObject;
        }
        
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        {
            if (selectedGameObject != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.ObjectField(selectedGameObject, typeof(GameObject), true);
                EditorGUILayout.Space();
                
                Renderer[] meshRenderers = selectedGameObject.GetComponentsInChildren<Renderer>(true);
                
                if (meshRenderers != null && meshRenderers.Length > 0)
                {
                    DrawTable(meshRenderers);
                    DrawTools(meshRenderers);
                }
                else
                {
                    GUILayout.Label("No renderer in this GameObject.");
                }
                
            }
            else
            {
                GUILayout.Label("Select object with renderer.");
            }
        }
        GUILayout.EndScrollView();
    }
    
    void OnSelectionChange()
    {
        Repaint();
    }
    
    private void DrawTable(Renderer[] meshRenderers)
    {
        GUILayout.BeginVertical();
        {
            // Draw Labels
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                for (int i = 0; i < COLUMN_COUNT; i++)
                {
                    DrawRow(null, i, true);
                }
            }
            GUILayout.EndHorizontal();
            
            // Draw Rows
            foreach (Renderer mesh in meshRenderers)
            {
                GUILayout.BeginHorizontal();
                {
                    for (int i = 0; i < COLUMN_COUNT; i++)
                    {
                        DrawRow(mesh, i, false);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }
    
    /// <summary>
    /// Draws the row.
    /// Switch over columns to keep same width for label and field.
    /// </summary>
    private void DrawRow(Renderer renderer, int column, bool isLabelRow)
    {
        if (isLabelRow)
        {
            switch (column)
            {
            case 0:
                GUILayout.Label("GameObject", EditorStyles.miniLabel, GUILayout.Width(TOGGLE_WIDTH));
                break;
            case 1:
                GUILayout.Label("Renderer", EditorStyles.miniLabel, GUILayout.Width(TOGGLE_WIDTH));
                break;
                
            case 2:
                GUILayout.Label("Mesh", EditorStyles.miniLabel);
                break;
                
            case 3:
                GUILayout.Label("Material", EditorStyles.miniLabel);
                break;
                
            case 4:
                GUILayout.Label("Receive", EditorStyles.miniLabel, GUILayout.Width(TOGGLE_WIDTH));
                break;
                
            case 5:
                GUILayout.Label("Cast", EditorStyles.miniLabel, GUILayout.Width(CASTING_MODE_WIDTH));
                break;
            }
        }
        else
        {
            switch (column)
            {
            case 0:
                renderer.gameObject.SetActive(EditorGUILayout.Toggle(renderer.gameObject.activeSelf, GUILayout.Width(TOGGLE_WIDTH)));
                break;
            case 1:
                renderer.enabled = EditorGUILayout.Toggle(renderer.enabled, GUILayout.Width(TOGGLE_WIDTH));
                break;
                
            case 2:
                EditorGUILayout.ObjectField(renderer, typeof(Renderer), true);
                break;
                
            case 3:
                renderer.sharedMaterial = (Material) EditorGUILayout.ObjectField(renderer.sharedMaterial, typeof(Material), true);
                break;
                
            case 4:
                renderer.receiveShadows = EditorGUILayout.Toggle(renderer.receiveShadows, GUILayout.Width(TOGGLE_WIDTH));
                break;
                
            case 5:
                renderer.shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode) EditorGUILayout.EnumPopup(renderer.shadowCastingMode, GUILayout.Width(CASTING_MODE_WIDTH));
                break;
            }
        }
    }
    
    private void DrawTools(Renderer[] meshRenderers)
    {
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Select GameObjects"))
            {
                GameObject[] gameObjects = new GameObject[meshRenderers.Length];
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    gameObjects[i] = meshRenderers[i].gameObject;
                }
                
                Selection.objects = gameObjects;
            }
            
            if (GUILayout.Button("Select Materials"))
            {
                // HashSet for no repetitions.
                HashSet<Material> uniqueMaterials = new HashSet<Material>();
                foreach (Renderer renderer in meshRenderers)
                {
                    uniqueMaterials.Add(renderer.sharedMaterial);
                }
                
                Selection.objects = uniqueMaterials.ToArray();
            }
        }
        GUILayout.EndHorizontal();
    }
}
