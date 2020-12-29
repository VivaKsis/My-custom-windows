using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "EventData", menuName = "EventCreator/EventData")]
public class EventData : EntityData {

    #region EventGeneral

    [BoxGroup("Event")]
    [SerializeField, BoxGroup("Event"), LabelText("Name"), GUIColor("GetColorEventName")]
    private string _eventName;
    public string _EventName => _eventName;
    [SerializeField, BoxGroup("Event"), OnValueChanged("EventTypeManagement"), LabelText("Type"), GUIColor(0.8f, 0.8f, 0.8f)]
    private EventType _eventType;
    public EventType _EventType => _eventType;

    #endregion

    #region SpawnItem

    [SerializeField, HorizontalGroup("$GetParametersTitle/Left", 75), ShowIf("@_eventType == EventType.spawnItem"), PreviewField(75), HideLabel, GUIColor("GetColorItemPrefab")]
    private GameObject _itemPrefab;
    public GameObject _ItemPrefab => _itemPrefab;

    [BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnItem"), PropertySpace(SpaceBefore = -20), InlineProperty]
    public LanesNumbers ItemLanesNumbers = new LanesNumbers(106f);
    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Default Spawners:"), LabelWidth(220), ShowIf("@_eventType == EventType.spawnItem"), InlineProperty, GUIColor(0.8f, 0.8f, 0.8f)]
    private Lanes _itemLanes = new Lanes();
    public Lanes _ItemLanes => _itemLanes;

    #endregion

    #region SpawnSquad

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Faction"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor(0.8f, 0.8f, 0.8f)]
    private EnemyFaction _squadFaction;
    public EnemyFaction _SquadFaction => _squadFaction;

    [SerializeField, BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor(0.8f, 0.8f, 0.8f), PropertyTooltip("An estimated difficulty of flight pattern of this event")]
    private Difficulty _patternDifficulty;
    public Difficulty _PatternDifficulty => _patternDifficulty;

    [SerializeField, BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor("GetColorSquadUnitCount"), PropertyTooltip("The cumulative amount of units in this squad")]
    private int _unitCount;
    public int _UnitCount => _unitCount;

    [SerializeField, BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor("GetColorSquadEntryDuration"), PropertyTooltip("This is a measurement of the time it takes for the squad to enter the play area from the time it was instantiated"), SuffixLabel("Seconds", Overlay = true)]
    private float _spawnEntryDuration;
    public float _SpawnEntryDuration => _spawnEntryDuration;

    [SerializeField, BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor(0.8f, 0.8f, 0.8f), PropertyTooltip("If Enabled, stops the spawner from instantiating any more events, until this event has been completed or destroyed")]
    private bool _stopSpawner;
    public bool _StopSpawner => _stopSpawner;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Approximate Time"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor("GetColorSquadTime"), PropertyTooltip("The approximated amount to time it takes for the event to transpire"), SuffixLabel("Seconds", Overlay = true)]
    private int _approxSpawnSquadTime;
    public int _ApproxSpawnSquadTime => _approxSpawnSquadTime;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Timeline Prefab"), ShowIf("@_eventType == EventType.spawnSquad"), GUIColor("GetColorSquadPrefab"), AssetList(CustomFilterMethod = "HasPlayableDirector")]
    private GameObject _squadTimelinePrefab;
    public GameObject _SquadTimelinePrefab => _squadTimelinePrefab;

    [BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnSquad"), PropertySpace(SpaceBefore = 20), InlineProperty]
    public LanesNumbers squadLanesNumbers = new LanesNumbers(106f);
    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Default Spawners:"), LabelWidth(220), ShowIf("@_eventType == EventType.spawnSquad"), InlineProperty, GUIColor(0.8f, 0.8f, 0.8f)]
    private Lanes _squadLanes = new Lanes();
    public Lanes _SquadLanes => _squadLanes;

    #endregion

    #region SpawnBoss

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Faction"), ShowIf("@_eventType == EventType.spawnBoss"), GUIColor(0.8f, 0.8f, 0.8f)]
    private EnemyFaction _bossFaction;
    public EnemyFaction _BossFaction => _bossFaction;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Approximate Time"), ShowIf("@_eventType == EventType.spawnBoss"), GUIColor("GetColorBossTime"), PropertyTooltip("The approximated amount to time it takes for the event to transpire"), SuffixLabel("Seconds", Overlay = true)]
    private int _approxSpawnBossTime;
    public int _ApproxSpawnBossTime => _approxSpawnBossTime;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Timeline Prefab"), ShowIf("@_eventType == EventType.spawnBoss"), GUIColor("GetColorBossPrefab"), AssetList(CustomFilterMethod = "HasPlayableDirector")]
    private GameObject _bossTimelinePrefab;
    public GameObject _BossTimelinePrefab => _bossTimelinePrefab;

    [BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnBoss"), PropertySpace(SpaceBefore = 20), InlineProperty]
    public LanesNumbers bossLanesNumbers = new LanesNumbers(106f);
    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Default Spawners:"), LabelWidth(220), ShowIf("@_eventType == EventType.spawnBoss"), InlineProperty, GUIColor(0.8f, 0.8f, 0.8f)]
    private Lanes _bossLanes = new Lanes();
    public Lanes _BossLanes => _bossLanes;

    #endregion

    #region SpawnObstacle

    [SerializeField, BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnObstacle"), GUIColor(0.8f, 0.8f, 0.8f)]
    private bool _dropResource;
    public bool _DropResource => _dropResource;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Approximate Time"), ShowIf("@_eventType == EventType.spawnObstacle"), GUIColor("GetColorObstacleTime"), PropertyTooltip("The approximated amount to time it takes for the event to transpire"), SuffixLabel("Seconds", Overlay = true)]
    private int _approxObstacleTime;
    public int _ApproxObstacleTime => _approxObstacleTime;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Timeline Prefab"), ShowIf("@_eventType == EventType.spawnObstacle"), GUIColor("GetColorObstaclePrefab"), AssetList(CustomFilterMethod = "HasPlayableDirector")]
    private GameObject _obstacleTimelinePrefab;
    public GameObject _ObstacleTimelinePrefab => _obstacleTimelinePrefab;

    [BoxGroup("$GetParametersTitle"), ShowIf("@_eventType == EventType.spawnObstacle"), PropertySpace(SpaceBefore = 20), InlineProperty]
    public LanesNumbers obstacleLanesNumbers = new LanesNumbers(106f);
    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Default Spawners:"), LabelWidth(220), ShowIf("@_eventType == EventType.spawnObstacle"), InlineProperty, GUIColor(0.8f, 0.8f, 0.8f)]
    private Lanes _obstacleLanes = new Lanes();
    public Lanes _ObstacleLanes => _obstacleLanes;

    #endregion

    #region ChangeSpeed

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Option"), ShowIf("@_eventType == EventType.changeSpeed"), GUIColor(0.8f, 0.8f, 0.8f)]
    private ChangeSpeedOption _changeSpeedOption;
    public ChangeSpeedOption _ChangeSpeedOption => _changeSpeedOption;
    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Speed Modifier"), ShowIf("@_eventType == EventType.changeSpeed && (_changeSpeedOption == ChangeSpeedOption.increaseTime || _changeSpeedOption == ChangeSpeedOption.decreaseTime)"), GUIColor("GetColorChangeTimeModifier")]
    private float _changeSpeedModifier;
    public float _ChangeSpeedModifier => _changeSpeedModifier;

    #endregion

    #region Multiplier

    [BoxGroup("$GetParametersTitle"), LabelText("Message"), ReadOnly, ShowIf("@_eventType == EventType.changeMultiplier"), GUIColor(0.8f, 0.8f, 0.8f)]
    public string multiplierMessage = "Coming soon";

    #endregion

    #region Dialog

    [BoxGroup("$GetParametersTitle"), ReadOnly, ShowIf("@_eventType == EventType.dialog"), GUIColor(0.8f, 0.8f, 0.8f)]
    public string _character = "Coming Soon (Will be Enum)";

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Image"), ShowIf("@_eventType == EventType.dialog"), GUIColor("GetColorDialogImage")]
    private Sprite _dialogImage;
    public Sprite _DialogImage => _dialogImage;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Approximate Time"), ShowIf("@_eventType == EventType.dialog"), GUIColor("GetColorDialogTime"), PropertyTooltip("The approximated amount to time it takes for the event to transpire"), SuffixLabel("Seconds", Overlay = true)]
    private int _approxDialogTime;
    public int _ApproxDialogTime => _approxDialogTime;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Type"), ShowIf("@_eventType == EventType.dialog"), GUIColor(0.8f, 0.8f, 0.8f)]
    private DialogType _dialogType;
    public DialogType _DialogType => _dialogType;

    [SerializeField, BoxGroup("$GetParametersTitle"), LabelText("Text"), ShowIf("@_eventType == EventType.dialog"), TextArea, GUIColor("GetColorDialogText")]
    private string _dialogText;
    public string _DialogText => _dialogText;

    #endregion

    #region UniqueEvent

    [BoxGroup("$GetParametersTitle"), LabelText("Message"), ReadOnly, ShowIf("@_eventType == EventType.uniqueEvent"), GUIColor(0.8f, 0.8f, 0.8f)]
    public string uniqueEventMessage = "Coming soon";

    #endregion

    #region File Paths
    private string changeMultiplierPath = "Assets/Resources/Events/Change Multiplier";
    private string changeSpeedPath = "Assets/Resources/Events/Change Speed";
    private string dialogPath = "Assets/Resources/Events/Dialog";
    private string spawnBossPath = "Assets/Resources/Events/Spawn Boss";
    private string spawnItemPath = "Assets/Resources/Events/Spawn Item";
    private string spawnObstaclePath = "Assets/Resources/Events/Spawn Obstacle";
    private string spawnSquadPath = "Assets/Resources/Events/Spawn Squad";
    private string uniqueEventPath = "Assets/Resources/Events/Unique Event";
    #endregion

    #region ChangeTypeManagement

    [HideInInspector]
    public ChangeTypeConfirmer changeTypeConfirmer;

    private EventType backUpType = EventType.spawnItem;
    private EventType chosenType;
    private bool isAnyDataOnPage;

    private bool isEventTypeManagementRunning;

    private void EventTypeManagement() {
        if (isEventTypeManagementRunning) {
            return;
        }
        isEventTypeManagementRunning = true;
        if (changeTypeConfirmer == null) {
            isAnyDataOnPage = CheckForDataInput();
            if (isAnyDataOnPage) {
                PopUpChangeTypeWarningWindow();
            }
            else {
                backUpType = _eventType;
            }
        }
        isEventTypeManagementRunning = false;
    }

    private bool CheckForDataInput() {
        if (_eventName != null) {
            return true;
        }
        switch (backUpType) {
            case EventType.spawnItem:
                if (_itemPrefab != null) {
                    return true;
                }
                else {
                    return false;
                }
            case EventType.spawnSquad:
                if (_unitCount != 0) {
                    return true;
                }
                else if (_spawnEntryDuration != 0) {
                    return true;
                }
                else if (_stopSpawner) {
                    return true;
                }
                else if (_approxSpawnSquadTime != 0) {
                    return true;
                }
                else if (_squadTimelinePrefab != null) {
                    return true;
                }
                else {
                    return false;
                }
            case EventType.spawnBoss:
                if (_approxSpawnBossTime != 0) {
                    return true;
                }
                else if (_bossTimelinePrefab != null) {
                    return true;
                }
                else {
                    return false;
                }
            case EventType.spawnObstacle:
                if (_dropResource) {
                    return true;
                }
                else if (_approxObstacleTime != 0) {
                    return true;
                }
                else if (_obstacleTimelinePrefab != null) {
                    return true;
                }
                else {
                    return false;
                }
            case EventType.changeSpeed:
                if (_changeSpeedOption == ChangeSpeedOption.decreaseTime || _changeSpeedOption == ChangeSpeedOption.increaseTime) {
                    if (_changeSpeedModifier != 0) {
                        return true;
                    }
                }
                return false;
            case EventType.changeMultiplier:
                return false;
            case EventType.dialog:
                if (_dialogImage) {
                    return true;
                }
                else if (_approxDialogTime != 0) {
                    return true;
                }
                else if (_dialogText != null) {
                    return true;
                }
                else {
                    return false;
                }
            case EventType.uniqueEvent:
                return false;
            default:
                return false;
        }
    }

    private void PopUpChangeTypeWarningWindow() {
        CreatePopUpWindow(ref changeTypeConfirmer);
        if (changeTypeConfirmer != null && changeTypeConfirmer.window != null) {
            changeTypeConfirmer.window.OnClose += CheckIfConfirmed;
        }
        chosenType = _eventType;
        _eventType = backUpType;
    }

    private void CheckIfConfirmed() {
        if (changeTypeConfirmer.isOk) {
            ChangeTypeToChosen();
        }
        changeTypeConfirmer = null;
    }

    private void ChangeTypeToChosen() {
        _eventType = chosenType;
        ClearEachVariable();
    }

    private void ClearEachVariable() {

        switch (backUpType) {
            case EventType.spawnItem:
                if (_itemPrefab != null) {
                    _itemPrefab = null;
                }
                break;
            case EventType.spawnSquad:
                if (_unitCount != 0) {
                    _unitCount = 0;
                }
                if (_spawnEntryDuration != 0) {
                    _spawnEntryDuration = 0;
                }
                if (_stopSpawner) {
                    _stopSpawner = false;
                }
                if (_approxSpawnSquadTime != 0) {
                    _approxSpawnSquadTime = 0;
                }
                if (_squadTimelinePrefab != null) {
                    _squadTimelinePrefab = null;
                }
                break;
            case EventType.spawnBoss:
                if (_approxSpawnBossTime != 0) {
                    _approxSpawnBossTime = 0;
                }
                if (_bossTimelinePrefab != null) {
                    _bossTimelinePrefab = null;
                }
                break;
            case EventType.spawnObstacle:
                if (_dropResource) {
                    _dropResource = true;
                }
                if (_approxObstacleTime != 0) {
                    _approxObstacleTime = 0;
                }
                if (_obstacleTimelinePrefab != null) {
                    _obstacleTimelinePrefab = null;
                }
                break;
            case EventType.changeSpeed:
                if (_changeSpeedModifier != 0) {
                    _changeSpeedModifier = 0;
                }
                break;
            case EventType.changeMultiplier:
                break;
            case EventType.dialog:
                if (_dialogImage != null) {
                    _dialogImage = null;
                }
                if (_approxDialogTime != 0) {
                    _approxDialogTime = 0;
                }
                if (_dialogText != null) {
                    _dialogText = null;
                }
                break;
            case EventType.uniqueEvent:
                break;
            default:
                break;
        }
    }

    private void OnEnable() {
        backUpType = _eventType;
    }

    [TypeInfoBox("If you change type, data will be lost")]
    public class ChangeTypeConfirmer : OkCancelWindow {

    }

    #endregion

    #region Overrides

    public override void SetPath() {
        switch (_eventType) {
            case EventType.spawnItem:
                path = spawnItemPath;
                break;
            case EventType.spawnSquad:
                path = spawnSquadPath;
                break;
            case EventType.spawnBoss:
                path = spawnBossPath;
                break;
            case EventType.spawnObstacle:
                path = spawnObstaclePath;
                break;
            case EventType.changeSpeed:
                path = changeSpeedPath;
                break;
            case EventType.changeMultiplier:
                path = changeMultiplierPath;
                break;
            case EventType.dialog:
                path = dialogPath;
                break;
            case EventType.uniqueEvent:
                path = uniqueEventPath;
                break;
            default:
                break;
        }
        SetFactionPath();
    }

    private void SetFactionPath()
    {
        if (_eventType == EventType.spawnBoss)
        {
            switch (_bossFaction)
            {
                case EnemyFaction.StarCatchers:
                    path += "/" + "Star Catchers";
                    break;
                case EnemyFaction.Entropods:
                    path += "/" + "Entropods";
                    break;
                case EnemyFaction.Pirates:
                    path += "/" + "Pirates";
                    break;
                case EnemyFaction.Opulids:
                    path += "/" + "Opulids";
                    break;
                default:
                    break;
            }
            return;
        }
        else if (_eventType == EventType.spawnSquad)
        {
            switch (_squadFaction)
            {
                case EnemyFaction.StarCatchers:
                    path += "/" + "Star Catchers";
                    break;
                case EnemyFaction.Entropods:
                    path += "/" + "Entropods";
                    break;
                case EnemyFaction.Pirates:
                    path += "/" + "Pirates";
                    break;
                case EnemyFaction.Opulids:
                    path += "/" + "Opulids";
                    break;
                default:
                    break;
            }
        }
        SetDifficultyPath();
    }

    private void SetDifficultyPath()
    {
        if(_eventType != EventType.spawnSquad)
        {
            return;
        }
        switch (_patternDifficulty)
        {
            case Difficulty.easy:
                path += "/" + "Easy";
                break;
            case Difficulty.normal:
                path += "/" + "Normal";
                break;
            case Difficulty.hard:
                path += "/" + "Hard";
                break;
            case Difficulty.elite:
                path += "/" + "Elite";
                break;
            default:
                break;
        }
    }

    #region SetName

    public override void SetName() {
        objectName = _eventName + ".asset";
    }

    #endregion

    public override bool IsEachFieldInputted() {
        if (_eventName == null && _eventType != EventType.spawnBoss && _eventType != EventType.spawnSquad) {
            return false;
        }
        switch (_eventType) {
            case EventType.spawnItem:
                if (_itemPrefab != null) {
                    return true;
                }
                break;
            case EventType.spawnSquad:
                if (_unitCount != 0 && _spawnEntryDuration != 0 && _squadTimelinePrefab != null) {
                    return true;
                }
                break;
            case EventType.spawnBoss:
                if (_bossTimelinePrefab != null) {
                    return true;
                }
                break;
            case EventType.spawnObstacle:
                if (_obstacleTimelinePrefab != null) {
                    return true;
                }
                break;
            case EventType.changeSpeed:
                if (_changeSpeedOption == ChangeSpeedOption.decreaseTime || _changeSpeedOption == ChangeSpeedOption.increaseTime) {
                    if (_changeSpeedModifier != 0) {
                        return true;
                    }
                }
                else {
                    return true;
                }
                break;
            case EventType.changeMultiplier:
                return true;
            case EventType.dialog:
                if (_dialogImage != null && _dialogText != null) {
                    return true;
                }
                break;
            case EventType.uniqueEvent:
                return true;
            default:
                break;
        }
        return false;
    }
    public override void PasteData<SOData>(SOData dataToPaste) {
        var eventToPaste = dataToPaste as EventData;
        if (eventToPaste == null) {
            Debug.Log("Pasting event failed");
            return;
        }
        _eventName = eventToPaste._eventName;
        _eventType = eventToPaste._eventType;
        switch (_eventType) {
            case EventType.spawnItem:
                _itemPrefab = eventToPaste._itemPrefab;
                break;
            case EventType.spawnSquad:
                _squadFaction = eventToPaste._squadFaction;
                _patternDifficulty = eventToPaste._patternDifficulty;
                _unitCount = eventToPaste._unitCount;
                _spawnEntryDuration = eventToPaste._spawnEntryDuration;
                _stopSpawner = eventToPaste._stopSpawner;
                _approxSpawnSquadTime = eventToPaste._approxSpawnSquadTime;
                _squadTimelinePrefab = eventToPaste._squadTimelinePrefab;
                break;
            case EventType.spawnBoss:
                _bossFaction = eventToPaste._bossFaction;
                _approxSpawnBossTime = eventToPaste._approxSpawnBossTime;
                _bossTimelinePrefab = eventToPaste._bossTimelinePrefab;
                break;
            case EventType.spawnObstacle:
                _dropResource = eventToPaste._dropResource;
                _approxObstacleTime = eventToPaste._approxObstacleTime;
                _obstacleTimelinePrefab = eventToPaste._obstacleTimelinePrefab;
                break;
            case EventType.changeSpeed:
                _changeSpeedOption = eventToPaste._changeSpeedOption;
                _changeSpeedModifier = eventToPaste._changeSpeedModifier;
                break;
            case EventType.changeMultiplier:
                break;
            case EventType.dialog:
                _character = eventToPaste._character;
                _dialogImage = eventToPaste._dialogImage;
                _approxDialogTime = eventToPaste._approxDialogTime;
                _dialogType = eventToPaste._dialogType;
                _dialogText = eventToPaste._dialogText;
                break;
            case EventType.uniqueEvent:
                break;
            default:
                break;
        }
    }

    #endregion

    #region GetParametersTitle

    private string GetParametersTitle() {
        switch (_eventType) {
            case EventType.spawnItem:
                return "Item General";
            case EventType.spawnSquad:
                return "Squad General";
            case EventType.spawnBoss:
                return "Boss General";
            case EventType.spawnObstacle:
                return "Obstacle General";
            case EventType.changeSpeed:
                return "Change Speed";
            case EventType.changeMultiplier:
                return "Change Multiplier";
            case EventType.dialog:
                return "Dialog General";
            case EventType.uniqueEvent:
                return "Unique Event";
            default:
                return "";
        }
    }

    #endregion

    #region PropertyColorManager

    private Color GetColorEventName() { return (this._eventName == null || this._eventName == "") ? redColor : greyColor; }


    private Color GetColorItemPrefab() { return _itemPrefab == null ? redColor : greyColor; }


    private Color GetColorSquadUnitCount() { return _unitCount == 0 ? redColor : greyColor; }
    private Color GetColorSquadEntryDuration() { return _spawnEntryDuration == 0 ? redColor : greyColor; }
    private Color GetColorSquadTime() { return _approxSpawnSquadTime == 0 ? redColor : greyColor; }
    private Color GetColorSquadPrefab() { return _squadTimelinePrefab == null ? redColor : greyColor; }


    private Color GetColorBossTime() { return _approxSpawnBossTime == 0 ? redColor : greyColor; }
    private Color GetColorBossPrefab() { return _bossTimelinePrefab == null ? redColor : greyColor; }


    private Color GetColorObstacleTime() { return _approxObstacleTime == 0 ? redColor : greyColor; }
    private Color GetColorObstaclePrefab() { return _obstacleTimelinePrefab == null ? redColor : greyColor; }


    private Color GetColorChangeTimeModifier() { return _changeSpeedModifier == 0 ? redColor : greyColor; }


    private Color GetColorDialogImage() { return _dialogImage == null ? redColor : greyColor; }
    private Color GetColorDialogTime() { return _approxDialogTime == 0 ? redColor : greyColor; }
    private Color GetColorDialogText() { return (this._dialogText == null || this._dialogText == "") ? redColor : greyColor; }

    #endregion

    #region Timeline Custom Filter
    private bool HasPlayableDirector(GameObject obj)
    {
        return obj.GetComponent<PlayableDirector>() != null;
    }
    #endregion
}

#region Enums
public enum EventType {
    spawnItem,
    spawnSquad,
    spawnBoss,
    spawnObstacle,
    changeSpeed,
    changeMultiplier,
    dialog,
    uniqueEvent
}

public enum Difficulty {
    easy,
    normal,
    hard,
    elite
}

public enum DialogType {
    small,
    large
}

public enum ChangeSpeedOption {
    increaseTime,
    decreaseTime,
    stop,
    start
}
#endregion

#region LanesClass

[Serializable]
public class Lanes {
    [HideInInspector]
    public static int length = 11;
    [HideInInspector]
    public LanePiece[] lanes = new LanePiece[length];

    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(115f)]
    public LanePiece lane1;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(135f)]
    public LanePiece lane2;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(155f)]
    public LanePiece lane3;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(175f)]
    public LanePiece lane4;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(195f)]
    public LanePiece lane5;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(215f)]
    public LanePiece lane6;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(235f)]
    public LanePiece lane7;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(255f)]
    public LanePiece lane8;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(275f)]
    public LanePiece lane9;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(295f)]
    public LanePiece lane10;
    [HorizontalGroup("Lanes", 20), OnValueChanged("LanesArrayUpdate"), HideLabel, DisableToggleOn(315f)]
    public LanePiece lane11;

    public void LanesArrayUpdate() {
        int i = 0;
        lanes[i++] = lane1;
        lanes[i++] = lane2;
        lanes[i++] = lane3;
        lanes[i++] = lane4;
        lanes[i++] = lane5;
        lanes[i++] = lane6;
        lanes[i++] = lane7;
        lanes[i++] = lane8;
        lanes[i++] = lane9;
        lanes[i++] = lane10;
        lanes[i++] = lane11;
    }

    public void LanesTogglesUpdate() {
        int i = 0;
        lane1 = lanes[i++];
        lane2 = lanes[i++];
        lane3 = lanes[i++];
        lane4 = lanes[i++];
        lane5 = lanes[i++];
        lane6 = lanes[i++];
        lane7 = lanes[i++];
        lane8 = lanes[i++];
        lane9 = lanes[i++];
        lane10 = lanes[i++];
        lane11 = lanes[i++];
    }

    public Lanes() {
        LanesTogglesUpdate();
    }

    public void Copy(Lanes lanesToCopy) {
        for (int i = 0; i < length; i++)
        {
            lanes[i] = new LanePiece();
            lanes[i].Value = lanesToCopy.lanes[i].Value;
        }
        LanesTogglesUpdate();
    }

    [Serializable]
    public class LanePiece
    {
        [VerticalGroup, HideLabel]
        public bool Value = true;
        public static string VALUE_LABEL_TAG = "Value";

        [HideInInspector]
        public bool Enabled = true;
        public static string ENABLED_LABEL_TAG = "Enabled";
    }
}

[Serializable]
public class LanesNumbers {
    public float indent;
    public LanesNumbers(float indent) {
        this.indent = indent;
    }
}
#endregion