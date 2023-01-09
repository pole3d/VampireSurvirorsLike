using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    SerializedProperty _feedbacks;
    ReorderableList _list;

    private static List<Type> s_types;
    private static Dictionary<string, GameFeedbackAttribute> s_typesAttributes;

   // [InitializeOnLoadMethod]
    private static void UpdateTypes()
    {
        s_types = FetchTypes<GameFeedback>(typeof(BaseConditionFeedback));
        
        
        s_typesAttributes = new Dictionary<string, GameFeedbackAttribute>();
         
        foreach (Type type in s_types)
        {
            if (Attribute.GetCustomAttribute(type, typeof(GameFeedbackAttribute)) is GameFeedbackAttribute attribute)
                s_typesAttributes.Add(type.Name, attribute);
            else s_typesAttributes.Add(type.Name, new GameFeedbackAttribute(255, 255, 255));
        }
    }

    private static List<Type> FetchTypes<T>(Type typetoIgnore) where T : class
    {
        return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where assemblyType.IsSubclassOf(typeof(T)) && assemblyType.IsAbstract == false && assemblyType.IsSubclassOf(typetoIgnore) == false
                select assemblyType).ToList();
    }

    private void OnEnable()
    {
        UpdateTypes();

        _feedbacks = serializedObject.FindProperty("Feedbacks");

        _list = new ReorderableList(serializedObject, _feedbacks, true, true, true, true);
        _list.drawElementCallback = DrawListItems; 
        _list.drawHeaderCallback = DrawHeader;
        _list.onAddDropdownCallback = AddDropDown;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Update the array property's representation in the inspector

        var comments = serializedObject.FindProperty("Comments");
        comments.stringValue = EditorGUILayout.TextArea(comments.stringValue);

        if ( GUILayout.Button("Play" ))
        {
            PlayEvent();
        }

        _list.DoLayoutList(); // Have the ReorderableList do its work

        // We need to call this so that changes on the Inspector are saved by Unity.
        serializedObject.ApplyModifiedProperties();
    }

    private void PlayEvent()
    {
        foreach (var item in Selection.transforms)
        {
            GameEventsManager.PlayEvent(target as GameEvent, item.gameObject);
        }
    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        GameEvent gameEvent = target as GameEvent;
        if (gameEvent == null) return;

        GameFeedback feedback = gameEvent.Feedbacks[index];
        if (feedback == null) return;

        SerializedProperty element = _feedbacks.GetArrayElementAtIndex(index);
        Rect line = rect;
        line.x = 5;//+= line.width - 5;
        line.width = 5;
        line.height -= 2;
        line.y += 1;

        Type type = gameEvent.Feedbacks[index].GetType();

        rect.x += 15;
        gameEvent.Feedbacks[index].Enabled = EditorGUI.Toggle(rect, GUIContent.none, gameEvent.Feedbacks[index].Enabled);
        EditorGUI.DrawRect(line, s_typesAttributes[type.Name].Color);

        rect.x += 30;

        EditorGUI.LabelField(rect, gameEvent.Feedbacks[index].ToString());

        if (isFocused == false && isActive == false) return;

        foreach (SerializedProperty child in GetChildren(element))
        {
            EditorGUILayout.PropertyField(child);
        }

    }
    
    // public static System.Type GetType(SerializedProperty property)
    // {
    //     System.Type parentType = property.serializedObject.targetObject.GetType();
    //     System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
    //     return fi.FieldType;
    // }
    //


    private IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
    {
        SerializedProperty currentProperty = property.Copy();
        SerializedProperty nextProperty = property.Copy();
        nextProperty.Next(false);

        if (currentProperty.Next(true))
        {
            do
            {
                if (SerializedProperty.EqualContents(currentProperty, nextProperty)) break;
                yield return currentProperty;
            } while (currentProperty.Next(false));
        }
    }

    void DrawHeader(Rect rect)
    {
        string name = "Feedbacks";
        EditorGUI.LabelField(rect, name);
    }

    private void AddDropDown(Rect rect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();
        for (int i = 0; i < s_types.Count; i++)
        {
            int index = i;
            string name = s_types[i].Name;

            if ( s_typesAttributes.ContainsKey(name) && String.IsNullOrEmpty(s_typesAttributes[name].MenuName)==false)
            {
                name = s_typesAttributes[name].MenuName;
            }

            menu.AddItem(new GUIContent(name), false, () => {
                AddItem(s_types[index]);
            }
            );
        }
        menu.ShowAsContext();

    }

    private void AddItem(Type type)
    {
        serializedObject.Update();

        _feedbacks.arraySize++;
        SerializedProperty newProp= _feedbacks.GetArrayElementAtIndex(_feedbacks.arraySize - 1);
        newProp.managedReferenceValue = System.Activator.CreateInstance(type) as GameFeedback;
        serializedObject.ApplyModifiedProperties();

    }
}