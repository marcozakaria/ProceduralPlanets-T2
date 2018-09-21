using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;

    //Inside this function you can add your own custom GUI for the inspector of a specific object class.
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope()) // to replace OnValidate methode on planet script
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GeneratePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            planet.GeneratePlanet();
        }

        DrawSettingsEditor(planet.colorSettings,planet.OnColourSettingsUpdated,ref planet.colorSttingsFoldOut, ref colorEditor);
        DrawSettingsEditor(planet.shapeSetings,planet.OnShapeSettingsUpdated,ref planet.shapeSttingsFoldOut , ref shapeEditor);
    }

    void DrawSettingsEditor(Object settings , System.Action onSettingsUpdated , ref bool foldOut, ref Editor editor) // pass by refrence to be changed in planet
    {
        if (settings != null)
        {
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settings); // foldOut to nt minimize the window

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldOut)
                {
                    //Editor editor = CreateEditor(settings);
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed) // if we changed any thing in the editor
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target; // object planet script is attached to.
    }

}
