#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SoundController), true)]
public class SoundControllerInspector : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var manager = target as SoundController;

        /*
         #region Sources

        GUILayout.Space(3);
        GUILayout.Label("Audio Sources", EditorStyles.boldLabel);
        GUILayout.Space(3);

        Array sourcesArray = Enum.GetValues(typeof(SoundController.AudioSources));
        List<SoundController.AudioSources> sourcesList = new List<SoundController.AudioSources>();

        foreach (SoundController.AudioSources t in sourcesArray)
            sourcesList.Add(t);

        while (manager._Sources.Count() < sourcesList.Count())
            manager._Sources.Add(null);
        while (manager._Sources.Count() > sourcesList.Count())
            manager._Sources.RemoveAt(manager._Sources.Count() - 1);

        for (int i = 0; i < manager._Sources.Count; i++) {
            manager._Sources[i] =
                (AudioSource)EditorGUILayout.ObjectField(
                    sourcesList[i].ToString(),
                    manager._Sources[i],
                    typeof(AudioSource),
                    null);
        }

        #endregion Sources

        */

        #region Sounds
        GUILayout.Space(3);
        GUILayout.Label("Sounds", EditorStyles.boldLabel);
        GUILayout.Space(3);

        var templateArray = Enum.GetValues(typeof(SoundController.Sounds));
        var templateList = new List<SoundController.Sounds>();

        foreach (SoundController.Sounds t in templateArray)
            templateList.Add(t);
        
        while (manager._AudioClipses.Count() > templateList.Count)
            manager._AudioClipses.RemoveAt(manager._AudioClipses.Count - 1);
        
        while (manager._AudioClipses.Count() < templateList.Count)
            manager._AudioClipses.Add(new SoundController.ListWrapper());

        while (manager._SourcesBaseVolumes.Count() < templateList.Count())
            manager._SourcesBaseVolumes.Add(1.0f);
        while (manager._SourcesBaseVolumes.Count() > templateList.Count())
            manager._SourcesBaseVolumes.RemoveAt(manager._SourcesBaseVolumes.Count() - 1);

        for (var i = 0; i < manager._AudioClipses.Count; i++) {
            EditorGUILayout.BeginHorizontal();

            
            EditorGUILayout.BeginVertical();
            var dirty = false;                        
            var list = manager._AudioClipses[i];
            int newCount = EditorGUILayout.IntSlider(templateArray.GetValue(i).ToString(), list.Clips.Count, 0, 9);
            
            list.Clips.EnsureSize(newCount, k => null);            

            for(int j = 0; j < list.Clips.Count; j++) {
                var oldVal = list.Clips[j];
                
                list.Clips[j] = (AudioClip)EditorGUILayout.ObjectField(list.Clips[j], typeof(AudioClip));
                
                if(oldVal != list.Clips[j])
                    dirty = true;
            }

            manager._AudioClipses[i] = list;                        

            if(dirty)
                UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
            EditorGUILayout.EndVertical();

            var oldVolume = manager._SourcesBaseVolumes[i];
            manager._SourcesBaseVolumes[i] =
                EditorGUILayout.Slider(manager._SourcesBaseVolumes[i], 0.0f, 1.0f, GUILayout.MaxWidth(150));
            if (oldVolume != manager._SourcesBaseVolumes[i]) {
                UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
            }

            EditorGUILayout.EndHorizontal();
        }        
        #endregion Sounds
    }
}
#endif