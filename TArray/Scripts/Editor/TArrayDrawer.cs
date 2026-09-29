#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Extensions.Editor
{
    /// <summary>
    /// Custom property drawer for the <see cref="TArray{T}"/> type.
    /// </summary>
    [CustomPropertyDrawer(typeof(TArray<>))]
    public class TArrayDrawer : PropertyDrawer
    {
        /* Cached property values, to avoid repeated calls to serialized property getters. */
        [SerializeField] private SerializedProperty size;
        [SerializeField] private SerializedProperty data;

        /* Scroll position for the array elements ScrollView. */
        [SerializeField] private Vector2 scrollPosition;

        /// <summary>
        /// Draw the property inspector GUI.
        /// </summary>
        /// <param name="position">The position of the property.</param>
        /// <param name="property">The property to draw.</param>
        /// <param name="label">The label of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            /* Begin the serialized property and remove the default inspector spacing.*/
            EditorGUI.BeginProperty(position, label, property);
            EditorGUILayout.Space(-20);

            /* Draw the foldout and return if it is not expanded.*/
            if (!(property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, GetLabel(property), true)))
            {
                return;
            }

            /* Cache the serialized property values.*/
            size ??= property.FindPropertyRelative("size");
            data ??= property.FindPropertyRelative("data");

            int originalIndent = EditorGUI.indentLevel;

            /* Begin the property drawing. Indent the content and draw a pretty box around it.*/
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical(GUI.skin.box);

            /* Draw the "Size" property. */
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Size:");
            EditorGUILayout.PropertyField(size, GUIContent.none);

            /* Get the size value and use it as a valid representation of changed size of the array.*/
            Vector2Int sizeValue = size.vector2IntValue;

            /* Draw the array elements if the size is valid (greater than zero).*/
            if (sizeValue.x * sizeValue.y > 0)
            {
                /* Determine the minimum width of the elements. It is simply based on the type of array element.*/
                float minWidth = data.arrayElementType == "bool" ? 20 : 50;

                /* Draw the "Values" property.
                Indent the content to match the indent of the property.*/
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Values:");
                EditorGUI.indentLevel = originalIndent;

                // Begin the scroll view and assing its position.
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

                /* Draw the array elements in a grid layout.
                Begin a horizontal layout for the row. 
                Indent the elements inside the scroll view to match the indent of the property. */
                for (int y = 0; y < sizeValue.y; y++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(15);

                    for (int x = 0; x < sizeValue.x; x++)
                    {
                        /* Draw the element if it is within the bounds of the array.
                        Assign the new value to the array. */
                        if ((sizeValue.x * y) + x < data.arraySize)
                        {
                            EditorGUILayout.PropertyField(data.GetArrayElementAtIndex((sizeValue.x * y) + x), GUIContent.none, GUILayout.MinWidth(minWidth));
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }

            /* Restore the original indent level and end the property.
            Add some spacing between the properties.
            End the vertical layout.*/
            EditorGUI.indentLevel = originalIndent;
            EditorGUI.EndProperty();
            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();

            // Apply the changes to the serialized property.
            if (GUI.changed)
            {
                data.arraySize = sizeValue.y * sizeValue.x;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Get the label of the property.
        /// </summary>
        /// <param name="property">The property to get the label of.</param>
        /// <returns>The custom label of the serialized two-diamensional array property.</returns>
        private GUIContent GetLabel(SerializedProperty property)
        {
            Vector2Int sizeValue = (size ?? property.FindPropertyRelative("size")).vector2IntValue;
            return new GUIContent($"{property.displayName} [A2D: {sizeValue.x}x{sizeValue.y}]");
        }
    }
}
#endif