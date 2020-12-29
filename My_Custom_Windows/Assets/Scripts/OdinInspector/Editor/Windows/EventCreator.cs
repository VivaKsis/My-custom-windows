using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventCreator : CreatorWindow {

    private CreateNew<EventData> createNewEvent = new CreateNew<EventData>();

    #region Paths

    private string changeMultiplierPath = "Assets/Resources/Events/Change Multiplier";
    private string changeSpeedPath = "Assets/Resources/Events/Change Speed";
    private string dialogPath = "Assets/Resources/Events/Dialog";
    private string spawnBossPath = "Assets/Resources/Events/Spawn Boss";
    private string spawnItemPath = "Assets/Resources/Events/Spawn Item";
    private string spawnObstaclePath = "Assets/Resources/Events/Spawn Obstacle";
    private string spawnSquadPath = "Assets/Resources/Events/Spawn Squad";
    private string uniqueEventPath = "Assets/Resources/Events/Unique Event";

    #endregion

    #region OdinMethods

    [MenuItem("Tools/Odin Inspector/EventCreator")]
    private static void OpenWindow() {
        GetWindow<EventCreator>().position = GUIHelper.GetEditorWindowRect().AlignCenter(850, 600);
    }

    protected override void OnEnable() {
        createNewEvent.newObject = ScriptableObject.CreateInstance<EventData>();
        base.OnEnable();
    }

    protected override IEnumerable<object> GetTargets() {

        selected = MenuTree.Selection;

        if (selected.SelectedValue != null) {

            selectionType = selected.SelectedValue.GetType();

            if (selected.SelectedValue == createNewEvent) {
                yield return createNewEvent;

                yield return createNewEvent.newObject;
            }

            if (selectionType == typeof(EventData)) {
                yield return this;

                yield return selected.SelectedValue;
            }
        }


    }

    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = true;

        tree.AddAllAssetsAtPath("Items", spawnItemPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Squads", spawnSquadPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Bosses", spawnBossPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Obstacles", spawnObstaclePath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Speed Change", changeSpeedPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Multiplier", changeMultiplierPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Dialogs", dialogPath, typeof(EventData), true);
        tree.AddAllAssetsAtPath("Unique Events", uniqueEventPath, typeof(EventData), true);

        tree.SortMenuItemsByName(true);

        OdinMenuItem createWindow = new OdinMenuItem(tree, "Create", createNewEvent);

        tree.MenuItems.Insert(0, createWindow);

        return tree;
    }

    private void OnFocus() {
        KeepFocus(deleteConfirmer);
        KeepFocus(createNewEvent.notEachFieldInputtedError);
        KeepFocus(createNewEvent.overWriteObjectConfirmer);
        KeepFocus(createNewEvent.newObject.changeTypeConfirmer);
    }

    #endregion

    #region CopyOverride

    [Button("Copy")]
    protected override void CopyObjectButton() {
        selected = MenuTree.Selection;
        if (selected.SelectedValue != null) {
            if (selected.SelectedValue is EventData s) {
                createNewEvent.newObject = CreateInstance<EventData>();
                createNewEvent.newObject.PasteData(s);
            }
        }
    }

    #endregion
}
