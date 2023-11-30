using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoodManager))]
public class MoodManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsbile for changing the mood of the UMA VR Avatars.", MessageType.Info);

        MoodManager moodManager = (MoodManager)target;
        MoodManager.Moods newmoodValue = (MoodManager.Moods)EditorGUILayout.EnumPopup(moodManager.myMood);
        if (newmoodValue !=moodManager.myMood)
        {
            moodManager.myMood = newmoodValue;
            moodManager.ChangeMood(moodManager.myMood);
            Debug.Log("Mood is changed to:" +moodManager.myMood.ToString());
        }

    }

}
