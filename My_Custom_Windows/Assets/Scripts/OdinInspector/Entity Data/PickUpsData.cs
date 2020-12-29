using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PickUpsData", menuName = "SO Data/Pick Ups Data")]
public class PickUpsData : EntityData {

    #region GeneralParameters
    [HorizontalGroup("Parameters")]
    [BoxGroup("Parameters/General")]

    [SerializeField, HorizontalGroup("Parameters/General/Left", 75), PreviewField(75), HideLabel, GUIColor("GetColorImage")]
    private Sprite _sprite;
    public Sprite _Sprite => _sprite;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor("GetColorName")]
    private string _pickUpName;
    public string _PickUpName => _pickUpName;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor(0.7f, 0.7f, 0.7f)]
    private PickUps _pickUpType;
    public PickUps _PickUpType => _pickUpType;
    #endregion

    #region Stats
    [SerializeField, FoldoutGroup("Stats"), LabelWidth(100), GUIColor("GetColorRate")]
    [Range(0, 100)]
    private float _dropRate;
    public float _DropRate => _dropRate;
    [SerializeField, FoldoutGroup("Stats"), LabelWidth(100), GUIColor("GetColorCount")]
    private float _dropCount;
    public float _DropCount => _dropCount;
    #endregion

    #region File Path

    private string pickUpPath = "Assets/Prefab/PickUps";

    #endregion

    #region Overrides

    public override void SetPath() {
        path = pickUpPath;
    }

    public override void SetName() {
        objectName = _pickUpName + ".asset";
    }

    public override bool IsEachFieldInputted() {
        if (_sprite != null && _pickUpName != null && _dropRate != 0 && _dropCount != 0) {
            objectName = _pickUpName;
            return true;
        }
        return false;
    }

    public override void PasteData<SOData>(SOData dataToPaste) {
        var pickUpToPaste = dataToPaste as PickUpsData;
        if (pickUpToPaste == null) {
            Debug.Log("Pasting pick up failed");
            return;
        }
        _sprite = pickUpToPaste._sprite;
        _pickUpName = pickUpToPaste._pickUpName;
        _pickUpType = pickUpToPaste._pickUpType;
        _dropRate = pickUpToPaste._dropRate;
        _dropCount = pickUpToPaste._dropCount;
    }

    #endregion

    #region PropertyColorManager

    private Color GetColorImage() { return this._sprite == null ? redColor : greyColor; }
    private Color GetColorName() { return (this._pickUpName == null || this._pickUpName == "") ? redColor : greyColor; }
    private Color GetColorRate() { return this._dropRate == 0 ? redColor : greyColor; }
    private Color GetColorCount() { return this._dropCount == 0 ? redColor : greyColor; }

    #endregion
}

#region Enums

public enum PickUps {
    currency,
    resource,
    pickup
}

#endregion