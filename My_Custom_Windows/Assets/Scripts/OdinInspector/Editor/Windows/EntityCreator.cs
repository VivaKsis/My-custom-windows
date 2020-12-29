using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class EntityCreator : CreatorWindow {

    private CreateNew<PlayerShipData> newPlayerShips = new CreateNew<PlayerShipData>();
    private CreateNew<EnemyShipData> newEnemyShips = new CreateNew<EnemyShipData>();
    private CreateNew<ShipComponentData> newShipComponents = new CreateNew<ShipComponentData>();
    private CreateNew<PickUpsData> newPickUps = new CreateNew<PickUpsData>();

    #region Paths

    private string playerShipsPath = "Assets/Prefab/PlayerShips";
    private string enemyShipsPath = "Assets/Prefab/EnemyShips";
    private string shipComponentsPath = "Assets/Prefab/ShipComponents";
    private string pickUpsPath = "Assets/Prefab/PickUps";

    #endregion

    #region OdinMethods

    [MenuItem("Tools/Odin Inspector/Entity Creator")]
    private static void OpenWindow() {
        GetWindow<EntityCreator>().position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 850);
    }

    protected override void OnEnable() {
        newPlayerShips.newObject = ScriptableObject.CreateInstance<PlayerShipData>();
        newEnemyShips.newObject = ScriptableObject.CreateInstance<EnemyShipData>();
        newShipComponents.newObject = ScriptableObject.CreateInstance<ShipComponentData>();
        newPickUps.newObject = ScriptableObject.CreateInstance<PickUpsData>();
        base.OnEnable();
    }

    protected override IEnumerable<object> GetTargets() {

        selected = MenuTree.Selection;

        if (selected.SelectedValue != null) {

            selectionType = selected.SelectedValue.GetType();

            if (selected.SelectedValue == newPlayerShips) {
                newPlayerShips.newObject.playerShipDataEditorWindowCurrentSize = position;

                yield return newPlayerShips;

                yield return newPlayerShips.newObject;
            }


            if (selected.SelectedValue == newEnemyShips) {
                yield return newEnemyShips;

                yield return newEnemyShips.newObject;
            }

            if (selected.SelectedValue == newShipComponents) {
                yield return newShipComponents;

                yield return newShipComponents.newObject;
            }

            if (selected.SelectedValue == newPickUps) {
                yield return newPickUps;

                yield return newPickUps.newObject;
            }

            if (selectionType == typeof(EnemyShipData) ||
                selectionType == typeof(ShipComponentData) ||
                selectionType == typeof(PickUpsData)) {

                yield return this;
                yield return selected.SelectedValue;
            }

            if (selected.SelectedValue is PlayerShipData playerShipData)
            {
                playerShipData.playerShipDataEditorWindowCurrentSize = position;

                yield return this;
                yield return selected.SelectedValue;
            }
        }

    }

    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Player Ships", newPlayerShips);
        tree.AddAllAssetsAtPath("Player Ships", playerShipsPath, typeof(PlayerShipData), true);
        tree.Add("Enemy Ships", newEnemyShips);
        tree.AddAllAssetsAtPath("Enemy Ships", enemyShipsPath, typeof(EnemyShipData), true);
        tree.Add("Ship Components", newShipComponents);
        tree.AddAllAssetsAtPath("Ship Components", shipComponentsPath, typeof(ShipComponentData), true);
        tree.Add("Pick Ups", newPickUps);
        tree.AddAllAssetsAtPath("Pick Ups", pickUpsPath, typeof(PickUpsData), true);

        return tree;
    }

    private void OnFocus() {
        KeepFocus(deleteConfirmer);

        if (selected == null) {
            return;
        }

        if (newPlayerShips != null && selected.SelectedValue == newPlayerShips) {
            KeepFocus(newPlayerShips.notEachFieldInputtedError);
            KeepFocus(newPlayerShips.overWriteObjectConfirmer);
        }

        if (newEnemyShips != null && selected.SelectedValue == newEnemyShips) {
            KeepFocus(newEnemyShips.notEachFieldInputtedError);
            KeepFocus(newEnemyShips.overWriteObjectConfirmer);
        }

        if (newShipComponents != null && selected.SelectedValue == newShipComponents) {
            KeepFocus(newShipComponents.notEachFieldInputtedError);
            KeepFocus(newShipComponents.overWriteObjectConfirmer);
        }

        if (newPickUps != null && selected.SelectedValue == newPickUps) {
            KeepFocus(newPickUps.notEachFieldInputtedError);
            KeepFocus(newPickUps.overWriteObjectConfirmer);
        }
    }

    #endregion

    #region CopyOverride

    [Button("Copy")]
    protected override void CopyObjectButton() {
        selected = MenuTree.Selection;
        if (selected.SelectedValue != null) {
            if (selected.SelectedValue is PlayerShipData a) {
                newPlayerShips.newObject = CreateInstance<PlayerShipData>();
                newPlayerShips.newObject.PasteData(a);
                return;
            }
            if (selected.SelectedValue is EnemyShipData b) {
                newEnemyShips.newObject = CreateInstance<EnemyShipData>();
                newEnemyShips.newObject.PasteData(b);
                return;
            }
            if (selected.SelectedValue is ShipComponentData c) {
                newShipComponents.newObject = CreateInstance<ShipComponentData>();
                newShipComponents.newObject.PasteData(c);
                return;
            }
            if (selected.SelectedValue is PickUpsData d) {
                newPickUps.newObject = CreateInstance<PickUpsData>();
                newPickUps.newObject.PasteData(d);
                return;
            }
        }
    }

    #endregion
}