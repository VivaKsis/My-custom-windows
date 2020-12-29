using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : CreatorWindow {

    private CreateNew<LevelData> newLevel = new CreateNew<LevelData>();

    #region Paths

    private string campaignPath = "Assets/Resources/Levels/Campaign";
    private string conquestPath = "Assets/Resources/Levels/Conquest";
    private string dailyPath = "Assets/Resources/Levels/Daily";
    private string endlessPath = "Assets/Resources/Levels/Endless";

    #endregion

    #region OdinMethods

    [MenuItem("Tools/Odin Inspector/LevelCreator")]
    private static void OpenWindow() {
        GetWindow<LevelCreator>().position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 600);
    }

    protected override void OnEnable() {

        newLevel.newObject = CreateInstance<LevelData>();
        newLevel.newObject.availableEvents = new List<EventData>(Resources.LoadAll<EventData>("Events/Spawn Item"));

        base.OnEnable();
    }

    protected override IEnumerable<object> GetTargets() {

        selected = MenuTree.Selection;

        if (selected != null && selected.SelectedValue != null) {

            selectionType = selected.SelectedValue.GetType();

            if (selected.SelectedValue == newLevel) {

                newLevel.newObject.levelDataEditorWindowCurrentSize = position;

                yield return newLevel;

                yield return newLevel.newObject;
            }

            if (selected.SelectedValue is LevelData levelData) {

                levelData.levelDataEditorWindowCurrentSize = position;

                yield return this;

                yield return levelData;
            }
        }

    }

    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = true;

        tree.AddAllAssetsAtPath("Campaign", campaignPath, typeof(LevelData), true);
        tree.AddAllAssetsAtPath("Endless", endlessPath, typeof(LevelData), true);
        tree.AddAllAssetsAtPath("Daily", dailyPath, typeof(LevelData), true);
        tree.AddAllAssetsAtPath("Conquest", conquestPath, typeof(LevelData), true);

        tree.SortMenuItemsByName(true);

        OdinMenuItem createLevel = new OdinMenuItem(tree, "Create", newLevel);

        tree.MenuItems.Insert(0, createLevel);

        return tree;
    }

    private void OnFocus() {
        KeepFocus(deleteConfirmer);
        KeepFocus(newLevel.notEachFieldInputtedError);
        KeepFocus(newLevel.overWriteObjectConfirmer);
    }

    #endregion

    #region CopyOverride

    [Button("Copy")]
    protected override void CopyObjectButton() {
        selected = MenuTree.Selection;
        if (selected.SelectedValue != null) {
            if (selected.SelectedValue is LevelData s) {
                newLevel.newObject = CreateInstance<LevelData>();
                newLevel.newObject.PasteData(s);
            }
        }
    }

    #endregion
}
