using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer {
    private Dictionary<string, Type> _typeCache;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        if (property.propertyType != SerializedPropertyType.ManagedReference) {
            EditorGUI.LabelField(position, label.text, "Use [SerializeReference] with [SubclassSelector]");
            return;
        }
        
        if (_typeCache == null) {
            _typeCache = new Dictionary<string, Type>();
            Type fieldType = fieldInfo.FieldType;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++) {
                Type[] types;
                try {
                    types = assemblies[i].GetTypes();
                } catch (ReflectionTypeLoadException e) {
                    types = e.Types;
                }

                if (types == null)
                    continue;

                for (int j = 0; j < types.Length; j++) {
                    Type t = types[j];
                    if (t == null || t.IsInterface || t.IsAbstract)
                        continue;

                    if (fieldType.IsAssignableFrom(t) && !_typeCache.ContainsKey(t.FullName))
                        _typeCache.Add(t.FullName, t);
                }
            }
        }
        
        string fullTypeName = property.managedReferenceFullTypename;
        string currentTypeName = string.IsNullOrEmpty(fullTypeName)
            ? "None"
            : fullTypeName.Split(' ')[1];
        
        List<Type> typeList = new List<Type> { null };
        List<string> displayNames = new List<string> { "None" };

        foreach (Type type in _typeCache.Values) {
            typeList.Add(type);
            displayNames.Add(type.Name);
        }

        int currentIndex = 0;
        for (int i = 1; i < typeList.Count; i++) {
            if (typeList[i].FullName == currentTypeName) {
                currentIndex = i;
                break;
            }
        }
        
        Rect dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int selectedIndex = EditorGUI.Popup(dropdownRect, label.text, currentIndex, displayNames.ToArray());
        
        if (selectedIndex != currentIndex) {
            if (selectedIndex == 0) {
                property.managedReferenceValue = null;
            } else {
                Type selectedType = typeList[selectedIndex];
                property.managedReferenceValue = Activator.CreateInstance(selectedType);
            }
        }

        if (property.managedReferenceValue == null) return;
        EditorGUI.indentLevel++;
        SerializedProperty iterator = property.Copy();
        var endProperty = iterator.GetEndProperty();

        float y = dropdownRect.yMax + EditorGUIUtility.standardVerticalSpacing;
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty)) {
            enterChildren = false;
            float height = EditorGUI.GetPropertyHeight(iterator, true);
            Rect fieldRect = new Rect(position.x, y, position.width, height);
            EditorGUI.PropertyField(fieldRect, iterator, true);
            y += height + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.indentLevel--;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (property.managedReferenceValue == null) return height;
        SerializedProperty iterator = property.Copy();
        var endProperty = iterator.GetEndProperty();

        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty)) {
            enterChildren = false;
            height += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height;
    }
}
