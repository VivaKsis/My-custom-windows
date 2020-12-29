using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;
using System.IO;

public class CreatorWindow : OdinMenuEditorWindow {

    [HideInInspector]
    protected DeleteConfirmer deleteConfirmer;

    protected OdinMenuTreeSelection selected;
    protected object selectedBackUp;
    protected Type selectionType;

    #region OdinMethods
    
    protected override OdinMenuTree BuildMenuTree() {
        throw new System.NotImplementedException();
    }

    public void KeepFocus(PopUpWindow popUpWindow) {
        if (popUpWindow != null && popUpWindow.window != null) {
            popUpWindow.window.Focus();
        }
    }

    #endregion

    #region Delete

    protected void DeleteConfirmation() {
        if (deleteConfirmer.isOk) {
            DeleteSelected();
        }
    }

    protected void DeleteSelected() {
        selected = this.MenuTree.Selection;
        ScriptableObject asset = selected.SelectedValue as ScriptableObject;
        string path = AssetDatabase.GetAssetPath(asset);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
    }

    [TypeInfoBox("Do you really want to delete this object?")]
    [System.Serializable]
    protected class DeleteConfirmer : OkCancelWindow {

    }

    [HorizontalGroup("Buttons", 0.5f)]

    [HorizontalGroup("Buttons/Left")]
    [GUIColor(0.75f, 1f, 1f), Button("Delete")]

    public void DeleteLevelButton() {

        EntityData deletion = CreateInstance<EntityData>();
        deletion.CreatePopUpWindow(ref deleteConfirmer);
        if (deleteConfirmer != null && deleteConfirmer.window != null) {
            deleteConfirmer.window.OnClose += DeleteConfirmation;
        }
    }

    #endregion

    #region CopyButton

    [HorizontalGroup("Buttons/Right")]
    [GUIColor(0.75f, 1f, 1f)]
    protected virtual void CopyObjectButton() {

    }
    #endregion
}

public class CreateNew<T> where T : EntityData, new() {

    [HideInInspector]
    public T newObject;
    [HideInInspector]
    public T copyObject;

    [HideInInspector]
    public NotEachFieldInputtedError notEachFieldInputtedError;
    [HideInInspector]
    public OverWriteObjectConfirmer overWriteObjectConfirmer;

    [HorizontalGroup("Buttons", 0.5f)]

    #region Save

    [HorizontalGroup("Buttons/Left")]
    [GUIColor(0.75f, 1f, 1f)]
    [Button("Save")]
    public void SaveObjectButton() {

        if (newObject.IsEachFieldInputted()) {

            UnitePathAndName();

            if (!DoesObjectAlreadyExist()) {
                SaveObject();
            }
            else {
                CreateOverWriteConfirmer();
            }
        }
        else {
            newObject.CreatePopUpWindow(ref notEachFieldInputtedError);
        }
    }

    private void UnitePathAndName() {
        newObject.SetPath();

        if (!Directory.Exists(newObject.path)) {
            Directory.CreateDirectory(newObject.path);
        }

        newObject.SetName();

        if (newObject.path != null && newObject.objectName != null) {
            newObject.path += "/" + newObject.objectName;
        }
        else {
            Debug.Log("Path or name do not exist");
        }
    }

    private void SaveObject() {
        AssetDatabase.CreateAsset(newObject, newObject.path);
        AssetDatabase.SaveAssets();
        newObject = ScriptableObject.CreateInstance<T>();
    }

    private bool DoesObjectAlreadyExist() {
        if (File.Exists(newObject.path)) {
            return true;
        }
        return false;
    }

    private void CreateOverWriteConfirmer() {
        newObject.CreatePopUpWindow(ref overWriteObjectConfirmer);
        if (overWriteObjectConfirmer != null && overWriteObjectConfirmer.window != null) {
            overWriteObjectConfirmer.window.OnClose += OverWriteObjectConfirm;
        }
    }

    private void OverWriteObjectConfirm() {
        if (overWriteObjectConfirmer.isOk) {
            AssetDatabase.CreateAsset(newObject, newObject.path);
            AssetDatabase.SaveAssets();
        }
        overWriteObjectConfirmer = null;
    }

    #endregion

    #region ClearButton

    [HorizontalGroup("Buttons/Right")]
    [GUIColor(0.75f, 1f, 1f)]
    [Button("Clear")]
    public void ClearButton() {
        newObject = ScriptableObject.CreateInstance<T>();
    }

    #endregion

    #region PopUpWindowClasses

    [TypeInfoBox("You must fill each field before saving")]
    public class NotEachFieldInputtedError : OKWindow {

    }

    [TypeInfoBox("This object already exists. Do you want to overwrite it?")]
    public class OverWriteObjectConfirmer : OkCancelWindow {

    }

    #endregion
}
