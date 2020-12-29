using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public class PopUpWindow {

    [HideInInspector]
    public OdinEditorWindow window;

    public void KeepFocus() {
        if (window != null) {
            window.Focus();
        }
    }
}

public class OKWindow : PopUpWindow {
    [Button(ButtonSizes.Large)]
    [GUIColor(0.85f, 1f, 0.85f)]
    public void OK() {
        window.Close();
    }
}

public class OkCancelWindow : PopUpWindow {
    [HideInInspector]
    public bool isOk;

    [HorizontalGroup("Buttons", 0.5f)]

    [Button(ButtonSizes.Large)]
    [HorizontalGroup("Buttons/Left")]
    [GUIColor(0.85f, 1f, 0.85f)]
    public void OK() {
        isOk = true;
        window.Close();
    }

    [Button(ButtonSizes.Large)]
    [HorizontalGroup("Buttons/Right")]
    [GUIColor(1f, 0.85f, 0.85f)]
    public void Cancel() {
        window.Close();
    }
}
