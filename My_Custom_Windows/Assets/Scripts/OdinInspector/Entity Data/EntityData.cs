using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class EntityData : ScriptableObject {

    [HideInInspector]
    public string objectName;
    [HideInInspector]
    public string path;

    protected static Color redColor = new Color(1f, 0.8f, 0.8f);
    protected static Color greyColor = new Color(0.8f, 0.8f, 0.8f);

    #region VirtualMethods

    public virtual bool IsEachFieldInputted() {
        return false;
    }

    public virtual void SetPath() {

    }

    public virtual void SetName() {

    }

    public virtual void PasteData<SOData>(SOData dataToPaste) {

    }

    #endregion


    #region CreatePopUpWindow
    public void CreatePopUpWindow<T>(ref T popUpWindow) where T : PopUpWindow, new() {
        popUpWindow = new T();
        if (popUpWindow != null) {
            OdinEditorWindow window = OdinEditorWindow.InspectObject(popUpWindow);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(270, 100);
            popUpWindow.window = window;
        }
    }

    #endregion
}
