using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelRecorder))]
public class LevelRecorderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelRecorder levelRecorder = (LevelRecorder)target;

        if (GUILayout.Button("Save Level"))
        {
            levelRecorder.SaveLevel();
        }
    }
}
