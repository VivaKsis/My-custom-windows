using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;

public class LevelData : EntityData {

    #region LevelParameters

    [HorizontalGroup("Parameters", 0.5f)]

    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor(0.8f, 0.8f, 0.8f), LabelWidth(175)]
    private LevelType _levelType;
    public LevelType _LevelType => _levelType;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorSector"), LabelWidth(175)]
    private int _sector;
    public int _Sector => _sector;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorLevel"), LabelWidth(175)]
    private int _level;
    public int _Level => _level;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorResourceCost"), LabelWidth(175)]
    private int _resourceCost;
    public int _ResourceCost => _resourceCost;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor(0.8f, 0.8f, 0.8f), LabelWidth(175)]
    private Difficulty _difficulty;
    public Difficulty _Difficulty => _difficulty;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor(0.8f, 0.8f, 0.8f), LabelWidth(175)]
    private List<Mission> _mission = new List<Mission>();
    public List<Mission> _Mission => _mission;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorPowerLevel"), LabelWidth(175), PropertyTooltip("Starting Base power level of units to be spawned")]
    private int _powerLevel;
    public int _PowerLevel => _powerLevel;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorSpeed"), LabelWidth(175)]
    private int _speed;
    public int _Speed => _speed;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor(0.8f, 0.8f, 0.8f), LabelWidth(175), ReadOnly]
    private string _multiplierMessage = "Not yet created";
    public string _MultiplierMessage => _multiplierMessage;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorMaxSpawnedUnits"), LabelWidth(175), PropertyTooltip("The maximum spawned unit strength in the game scene")]
    private int _maxStrength;
    public int _MaxStrength => _maxStrength;
    [SerializeField, VerticalGroup("Parameters/Left"), GUIColor("GetColorBackgroundScene"), LabelWidth(175)]
    private ScriptableObject _backgroundScene;
    public ScriptableObject _BackgroundScene => _backgroundScene;

    #endregion

    #region MissionTotals

    [HideInInspector, GUIColor(0.8f, 0.8f, 0.8f), ReadOnly, LabelWidth(175)]
    public int unitCount { get; private set; }
    [HideInInspector, GUIColor(0.8f, 0.8f, 0.8f), ReadOnly, LabelWidth(175)]
    public int obstaclesCount { get; private set; }
    [HideInInspector, GUIColor(0.8f, 0.8f, 0.8f), ReadOnly, LabelWidth(175)]
    public int itemCount { get; private set; }
    [HideInInspector, GUIColor(0.8f, 0.8f, 0.8f), ReadOnly, LabelWidth(175)]
    public int bosses { get; private set; }
    [HideInInspector, GUIColor(0.8f, 0.8f, 0.8f), ReadOnly, LabelWidth(175)]
    public string approxMisssionTime { get; private set; }

    #endregion

    #region File Paths

    #region LevelPaths

    private string campaignPath = "Assets/Resources/Levels/Campaign";
    private string conquestPath = "Assets/Resources/Levels/Conquest";
    private string dailyPath = "Assets/Resources/Levels/Daily";
    private string endlessPath = "Assets/Resources/Levels/Endless";

    #endregion

    #region EventPaths

    private string changeMultiplierPath = "Events/Change Multiplier";
    private string changeSpeedPath = "Events/Change Speed";
    private string dialogPath = "Events/Dialog";
    private string spawnBossPath = "Events/Spawn Boss";
    private string spawnItemPath = "Events/Spawn Item";
    private string spawnObstaclePath = "Events/Spawn Obstacle";
    private string spawnSquadPath = "Events/Spawn Squad";
    private string uniqueEventPath = "Events/Unique Event";

    #endregion

    #endregion

    #region Events

    [HorizontalGroup("Events", 0.5f)]

    [HorizontalGroup("Events/Left")]

    [SerializeField, HorizontalGroup("Events/Left", 0.8f), GUIColor(1f, 1f, 0.75f), PropertySpace(SpaceBefore = 10), OnValueChanged("LevelEventsUpdate"), InlineProperty, ListDrawerSettings(HideAddButton = true)]
    private List<EventData> _levelEvents = new List<EventData>();
    public List<EventData> _LevelEvents => _levelEvents;
    [SerializeField, HorizontalGroup("Events/Left", 0.2f), GUIColor(1f, 1f, 0.75f), PropertySpace(SpaceBefore = 10), LabelText("Lanes"), ListDrawerSettings(DraggableItems = false, IsReadOnly = true), InlineProperty]
    private List<FinalLanes> _finalLanes = new List<FinalLanes>();
    public List<FinalLanes> _FinalLanes => _finalLanes;

    [VerticalGroup("Events/Right"), OnValueChanged("PageUpdate"), EnumToggleButtons, GUIColor(1f, 0.85f, 1f), PropertySpace(SpaceBefore = -50), HideLabel]
    public EventType eventPage;

    [VerticalGroup("Events/Right"), OnValueChanged("PageUpdate"), GUIColor(1f, 0.85f, 1f), InlineProperty]
    public List<EventData> availableEvents;

    [SerializeField]
    private Dictionary<FinalLanes, EventData> eventDictionary = new Dictionary<FinalLanes, EventData>();


    #region EventAndLanesMethods

    private void PageUpdate() {
        switch (eventPage) {
            case EventType.spawnItem:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(spawnItemPath));
                break;
            case EventType.spawnSquad:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(spawnSquadPath));
                break;
            case EventType.spawnBoss:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(spawnBossPath));
                break;
            case EventType.spawnObstacle:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(spawnObstaclePath));
                break;
            case EventType.dialog:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(dialogPath));
                break;
            case EventType.changeSpeed:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(changeSpeedPath));
                break;
            case EventType.changeMultiplier:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(changeMultiplierPath));
                break;
            case EventType.uniqueEvent:
                availableEvents = new List<EventData>(Resources.LoadAll<EventData>(uniqueEventPath));
                break;
            default:
                availableEvents = new List<EventData>();
                break;
        }
    }

    private void CopyWithLeftShift(int index) {
        for (int i = index; i < _finalLanes.Count - 1; i++) {
            _finalLanes[i] = _finalLanes[i + 1];
        }
    }

    private void CopyWithRightShift(int index) {
        for (int i = _finalLanes.Count - 1; i > index; i--) {
            _finalLanes[i] = _finalLanes[i - 1];
        }
        _finalLanes[index] = new FinalLanes();
        CopyDefaultLanes(_levelEvents[index], _levelEvents[index]._EventType, _finalLanes[index]);
    }

    private void CopyFromAndTo(int index1, int index2) {
        FinalLanes buffer = _finalLanes[index2];
        for (int i = index2; i > index1; i--) {
            _finalLanes[i] = _finalLanes[i - 1];
        }
        _finalLanes[index1] = buffer;
    }

    private void ConnectDictionary() {
        EventData eventData1, eventData2;

        if (_levelEvents.Count > _finalLanes.Count) // event added
        {
            _finalLanes.Add(new FinalLanes());
            for (int i = 0; i < _finalLanes.Count; i++) {
                if (eventDictionary.TryGetValue(_finalLanes[i], out eventData1)) {
                    if (eventData1 != _levelEvents[i]) {
                        CopyWithRightShift(i);
                    }
                }
                else {
                    CopyDefaultLanes(_levelEvents[i], _levelEvents[i]._EventType, _finalLanes[i]);
                    eventDictionary.Add(_finalLanes[i], _levelEvents[i]);
                }
            }
        }
        else if (_levelEvents.Count < _finalLanes.Count) // event removed
        {
            for (int i = 0; i < _levelEvents.Count; i++) {
                if (eventDictionary.TryGetValue(_finalLanes[i], out eventData1)) {
                    if (eventData1 != _levelEvents[i]) {
                        CopyWithLeftShift(i);
                    }
                }
                else {
                    CopyWithLeftShift(i);
                }
            }
            _finalLanes.RemoveAt(_finalLanes.Count - 1);
        }
        else if (_levelEvents.Count == _finalLanes.Count) // swap
        {
            Debug.Log("swap");
            for (int i = 0; i < _levelEvents.Count; i++) {
                if (eventDictionary.TryGetValue(_finalLanes[i], out eventData1)) {
                    if (eventData1 != _levelEvents[i]) {
                        for (int j = i + 1; j < _levelEvents.Count; j++) {
                            if (eventDictionary.TryGetValue(_finalLanes[j], out eventData2)) {
                                if (eventData2 == _levelEvents[i]) {
                                    Debug.Log("From " + i + " Until " + j);
                                    CopyFromAndTo(i, j);
                                }
                            }
                            else {
                                Debug.LogError("If you see it, there's a BUG2. Index: " + i);
                            }
                        }
                    }
                }
                else {
                    Debug.LogError("If you see it, there's a BUG1. Index: " + i);
                }
            }
        }
    }

    private void LevelEventsUpdate() {
        ConnectDictionary();
        UpdateMissionTotals();
    }

    #region LanesManager

    private void CopyDefaultLanes(EventData levelEvent, EventType eventType, FinalLanes eventLanes) {

        eventLanes.ModifiedLanes = new FinalLanes.LanesWindow();
        eventLanes.ModifiedLanes.ModifiedLanes = new Lanes();

        eventLanes.eventType = eventType;

        switch (eventType) {
            case EventType.spawnItem:
                eventLanes.ModifiedLanes.ModifiedLanes.Copy(levelEvent._ItemLanes);
                eventLanes.ModifiedLanes.defaultLanes = levelEvent._ItemLanes;
                break;
            case EventType.spawnSquad:
                eventLanes.ModifiedLanes.ModifiedLanes.Copy(levelEvent._SquadLanes);
                eventLanes.ModifiedLanes.defaultLanes = levelEvent._SquadLanes;
                break;
            case EventType.spawnBoss:
                eventLanes.ModifiedLanes.ModifiedLanes.Copy(levelEvent._BossLanes);
                eventLanes.ModifiedLanes.defaultLanes = levelEvent._BossLanes;
                break;
            case EventType.spawnObstacle:
                eventLanes.ModifiedLanes.ModifiedLanes.Copy(levelEvent._ObstacleLanes);
                eventLanes.ModifiedLanes.defaultLanes = levelEvent._ObstacleLanes;
                break;
            default:
                eventLanes.ModifiedLanes = null;
                break;
        }
    }

    #endregion

    #endregion
    #endregion

    #region Overrides

    #region SetPath

    public override void SetPath() {
        SetTypePath();
    }

    private void SetTypePath() {
        switch (_levelType) {
            case LevelType.campaign:
                path = campaignPath;
                break;
            case LevelType.endless:
                path = endlessPath;
                break;
            case LevelType.conquest:
                path = conquestPath;
                break;
            case LevelType.daily:
                path = dailyPath;
                break;
            default:
                break;
        }
        SetSectorPath();
    }

    private void SetSectorPath() {
        path = path + "/" + "Sector " + _sector.ToString();
        SetLevelPath();
    }

    private void SetLevelPath() {
        path = path + "/" + "Level " + _level.ToString();
    }

    public override void SetName() {
        objectName = _sector.ToString() + " - " + _level.ToString() + " - ";
        switch (_difficulty) {
            case Difficulty.easy:
                objectName += "Easy" + ".asset";
                break;
            case Difficulty.normal:
                objectName += "Normal" + ".asset";
                break;
            case Difficulty.hard:
                objectName += "Hard" + ".asset";
                break;
            case Difficulty.elite:
                objectName += "Elite" + ".asset";
                break;
            default:
                break;
        }
    }

    #endregion

    public override bool IsEachFieldInputted() {
        if (_sector != 0 && (_level != 0) && (_powerLevel != 0) && (_maxStrength != 0) && (_backgroundScene != null)) {
            return true;
        }
        return false;
    }

    public override void PasteData<SOData>(SOData dataToPaste) {
        var levelToPaste = dataToPaste as LevelData;
        if (levelToPaste == null) {
            Debug.Log("Pasting level failed");
            return;
        }
        _levelType = levelToPaste._levelType;
        _sector = levelToPaste._sector;
        _level = levelToPaste._level;
        _difficulty = levelToPaste._difficulty;
        _mission = new List<Mission>();
        _mission.AddRange(levelToPaste._mission);
        _powerLevel = levelToPaste._powerLevel;
        _maxStrength = levelToPaste._maxStrength;
        _backgroundScene = levelToPaste._backgroundScene;

        _levelEvents = new List<EventData>();
        _levelEvents.AddRange(levelToPaste._levelEvents);

        _finalLanes = new List<FinalLanes>();
        _finalLanes.AddRange(levelToPaste._finalLanes);

        UpdateMissionTotals();
    }

    #endregion

    #region MissionMethods

    private void UpdateMissionTotals() {

        bosses = 0;
        itemCount = 0;
        obstaclesCount = 0;
        unitCount = 0;
        int time = 0;

        foreach (EventData e in _levelEvents) {
            if (e._EventType == EventType.spawnBoss) {
                bosses++;
                time += e._ApproxSpawnBossTime;
                continue;
            }
            if (e._EventType == EventType.spawnItem) {
                itemCount++;
                continue;
            }
            if (e._EventType == EventType.spawnObstacle) {
                time += e._ApproxObstacleTime;
                obstaclesCount++;
                continue;
            }
            if (e._EventType == EventType.spawnSquad) {
                time += e._ApproxSpawnSquadTime;
                unitCount += e._UnitCount;
                continue;
            }
            if (e._EventType == EventType.dialog) {
                time += e._ApproxDialogTime;
                unitCount += e._UnitCount;
                continue;
            }
        }
        CountApproximateTime(time);
    }

    private void CountApproximateTime(int seconds) {
        int min = seconds / 60;
        int sec = seconds % 60;
        approxMisssionTime = min.ToString() + " : " + sec.ToString();
    }

    #endregion

    #region MissionClass

    [Serializable]
    public class Mission {
        [LabelWidth(150), OnValueChanged("ClearMissionVariables")]
        public MissionType missionType;

        [ShowIf("@missionType == MissionType.exterminate"), GUIColor("GetColorExterminateKillCount"), LabelText("Kill Count"), LabelWidth(150), Indent]
        public int exterminateKillCount;
        [ShowIf("@missionType == MissionType.bounty"), GUIColor("GetColorBountyScoreLimit"), LabelText("Score Limit"), LabelWidth(150), Indent]
        public int bountyScoreLimit;
        [ShowIf("@missionType == MissionType.bounty"), GUIColor("GetColorBountyTimeLimit"), LabelText("Time Limit"), LabelWidth(150), Indent]
        public int bountyTimeLimit;
        [ShowIf("@missionType == MissionType.collector"), GUIColor("GetColorCollectorResourceCount"), LabelText("Resource Count"), LabelWidth(150), Indent]
        public int collectorResourceCount;
        [ShowIf("@missionType == MissionType.collector"), GUIColor(0.8f, 0.8f, 0.8f), LabelText("Resource Type"), ReadOnly, LabelWidth(150), Indent]
        public string collectorResourceType = "Not yet created";
        [ShowIf("@missionType == MissionType.collector"), GUIColor("GetColorCollectorTimeLimit"), LabelText("Time Limit"), LabelWidth(150), Indent]
        public int collectorTimeLimit;
        [ShowIf("@missionType == MissionType.assassination"), GUIColor("GetColorAssassinationPowerLevelIncrease"), LabelText("Power Level Increase"), LabelWidth(150), Indent]
        public float assassinationPowerLevelIncrease;
        [ShowIf("@missionType == MissionType.assassination"), GUIColor("GetColorAssassinationScaleSizeIncrease"), LabelText("Scale Size Increase"), LabelWidth(150), Indent]
        public float assassinationScaleSizeIncrease;

        private void ClearMissionVariables() {
            if (exterminateKillCount != 0) {
                exterminateKillCount = 0;
            }
            if (bountyScoreLimit != 0) {
                bountyScoreLimit = 0;
            }
            if (bountyTimeLimit != 0) {
                bountyTimeLimit = 0;
            }
            if (collectorResourceCount != 0) {
                collectorResourceCount = 0;
            }
            if (collectorTimeLimit != 0) {
                collectorTimeLimit = 0;
            }
            if (assassinationPowerLevelIncrease != 0) {
                assassinationPowerLevelIncrease = 0;
            }
            if (assassinationScaleSizeIncrease != 0) {
                assassinationScaleSizeIncrease = 0;
            }
        }

        #region PropertyColorManager
        private Color GetColorExterminateKillCount() { return this.exterminateKillCount == 0 ? redColor : greyColor; }
        private Color GetColorBountyScoreLimit() { return this.bountyScoreLimit == 0 ? redColor : greyColor; }
        private Color GetColorBountyTimeLimit() { return this.bountyTimeLimit == 0 ? redColor : greyColor; }
        private Color GetColorCollectorResourceCount() { return this.collectorResourceCount == 0 ? redColor : greyColor; }
        private Color GetColorCollectorTimeLimit() { return this.collectorTimeLimit == 0 ? redColor : greyColor; }
        private Color GetColorAssassinationPowerLevelIncrease() { return this.assassinationPowerLevelIncrease == 0 ? redColor : greyColor; }
        private Color GetColorAssassinationScaleSizeIncrease() { return this.assassinationScaleSizeIncrease == 0 ? redColor : greyColor; }

        #endregion
    }

    #endregion

    #region FinalLanesClass

    [Serializable]
    public class FinalLanes {

        [HideInInspector]
        public EventType eventType;

        [HideInInspector]
        public NotASpawnEventMessage notASpawnEventMessage;
        [HideInInspector]
        public LanesWindow ModifiedLanes;

        [Button("Lane", ButtonSizes.Small)]
        public void Overridelanes() {
            if (eventType != EventType.spawnItem && eventType != EventType.spawnSquad && eventType != EventType.spawnBoss && eventType != EventType.spawnObstacle) {

                notASpawnEventMessage = new NotASpawnEventMessage();
                CreatePopUpWindow(ref notASpawnEventMessage);
                ModifiedLanes = null;
                return;
            }
            DisableFalseToggles();
            CreatePopUpWindow(ref ModifiedLanes);
        }

        private void DisableFalseToggles()
        {
            Lanes.LanePiece[] lanes = ModifiedLanes.ModifiedLanes.lanes;
            Lanes.LanePiece[] defaultLanes = ModifiedLanes.defaultLanes.lanes;
            for (int i = 0; i < Lanes.length; i++)
            {
                if (defaultLanes[i].Value == false)
                {
                    lanes[i].Enabled = false;
                }
            }
        }

        #region CreatePopUpWindow
        private void CreatePopUpWindow<T>(ref T popUpWindow) where T : PopUpWindow, new() {
            if (popUpWindow != null) {
                OdinEditorWindow window = OdinEditorWindow.InspectObject(popUpWindow);
                window.position = GUIHelper.GetEditorWindowRect().AlignCenter(370, 200);
                popUpWindow.window = window;
            }
        }

        #endregion

        #region PopUpClasses

        [TypeInfoBox("Only items, squads, bosses and obstacles have spawning lanes to override")]
        public class NotASpawnEventMessage : OKWindow {

        }

        [Serializable]
        public class LanesWindow : PopUpWindow {

            [HideInInspector]
            public Lanes defaultLanes;

            [InlineProperty, HideLabel]
            public LanesNumbers lanesNumbers = new LanesNumbers(110f);
            [InlineProperty, LabelText("Modified Lanes"), LabelWidth(102)]
            public Lanes ModifiedLanes;

            [Button(ButtonSizes.Large)]
            [GUIColor(0.75f, 1f, 1f), PropertySpace]
            public void Save() {
                window.Close();
            }
        }

        #endregion
    }

    #endregion

    #region PropertyColorManager
    private Color GetColorSector() { return this._sector == 0 ? redColor : greyColor; }
    private Color GetColorLevel() { return this._level == 0 ? redColor : greyColor; }
    private Color GetColorResourceCost() { return this._resourceCost == 0 ? redColor : greyColor; }
    private Color GetColorPowerLevel() { return this._powerLevel == 0 ? redColor : greyColor; }
    private Color GetColorSpeed() { return this._speed == 0 ? redColor : greyColor; }
    private Color GetColorMaxSpawnedUnits() { return this._maxStrength == 0 ? redColor : greyColor; }
    private Color GetColorBackgroundScene() { return this._backgroundScene == null ? redColor : greyColor; }

    #endregion

    #region Level Editor Param

    [HideInInspector]
    public Rect levelDataEditorWindowCurrentSize = new Rect();

    #endregion
}

#region Enums
public enum LevelType {
    campaign,
    endless,
    conquest,
    daily
}

public enum MissionType {
    exterminate,
    bounty,
    hellFire,
    payloadDelivery,
    hotDrop,
    escortMission,
    collector,
    assassination
}
#endregion

