using System;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ShipComponentData", menuName = "SO Data/Ship Component Data")]
public class ShipComponentData : EntityData {

    #region GeneralParameters
    [HorizontalGroup("Parameters")]
    [BoxGroup("Parameters/General")]

    [SerializeField, HorizontalGroup("Parameters/General/Left", 75), PreviewField(75), HideLabel, GUIColor("GetColorImage")]
    private Sprite _sprite;
    public Sprite _Sprite => _sprite;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelText("Name"), LabelWidth(50), GUIColor("GetColorName")]
    private string _componentName;
    public string _ComponentName => _componentName;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor("GetColorPattern"), ShowIf("@_componentType == ComponentType.weapon")]
    private ScriptableObject _projectilePattern;
    public ScriptableObject _ProjectilePattern => _projectilePattern;

    [SerializeField, HorizontalGroup("Parameters/General/Type"), LabelWidth(110), GUIColor(0.8f, 0.8f, 0.8f), OnValueChanged("ReNew")]
    private ComponentType _componentType;
    public ComponentType _ComponentType => _componentType;
    [SerializeField, HorizontalGroup("Parameters/General/Rarity"), LabelWidth(110), GUIColor(0.8f, 0.8f, 0.8f), OnValueChanged("ReNew")]
    private ComponentRarity _componentRarity;
    public ComponentRarity _ComponentRarity => _componentRarity;

    private void ReNew() {
        if (_componentType == ComponentType.specialist && _componentRarity != ComponentRarity.epic) {
            ForcefullySetSpecialistRarity();
            return;
        }
        _statList.ReNewData(_componentType, _componentRarity);
        if (_componentType != ComponentType.weapon) {
            ClearProjectilePattern();
        }
    }

    private void ClearProjectilePattern() {
        _projectilePattern = null;
    }

    private void ForcefullySetSpecialistRarity() {
        _componentRarity = ComponentRarity.epic;
        Debug.Log("Specialist rarity should be only epic");
        ReNew();
    }
    #endregion

    #region Stats
    [SerializeField, HideInTables, FoldoutGroup("Stat List"), HideLabel]
    private StatList _statList;
    public StatList _StatList => _statList;
    #endregion

    #region Modifiers
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(75), GUIColor("GetColorOffense"), HideInTables]
    private float _offense;
    public float _Offense => _offense;
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(75), GUIColor("GetColorDefense"), HideInTables]
    private float _defense;
    public float _Defense => _defense;
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(75), GUIColor("GetColorUtility"), HideInTables]
    private float _utility;
    public float _Utility => _utility;
    #endregion

    #region Description
    [SerializeField, FoldoutGroup("Description"), TextArea, LabelWidth(150), HideLabel, GUIColor(0.8f, 0.8f, 0.8f), HideInTables]
    private string _description;
    public string _Description => _description;
    #endregion

    #region Path

    private string shipComponentPath = "Assets/Prefab/ShipComponents";

    #endregion

    #region Override

    public override void SetPath() {
        path = shipComponentPath;
    }

    public override void SetName() {
        objectName = _componentName + ".asset";
    }

    #region IsEachFieldInputted

    public override bool IsEachFieldInputted() {
        if (_sprite != null && _componentName != null && IsAllStatsInputted() && _offense != 0 && _defense != 0 && _utility != 0) {
            objectName = _componentName;
            return true;
        }
        return false;
    }

    private bool IsAllStatsInputted() {
        if (_statList.primaryStatName != null && _statList.primaryStat.value != 0 && IsAllSecondaryStatsInputted()) {
            return true;
        }
        return false;
    }

    private bool IsAllSecondaryStatsInputted() {
        if (_statList.secondaryStatsNumber >= 2) {
            if (IsASecondaryStatInputted(_statList.secondaryStat1Name, _statList.secondaryStat1) && IsASecondaryStatInputted(_statList.secondaryStat2Name, _statList.secondaryStat2)) {
                if (_statList.secondaryStatsNumber >= 4) {
                    if (IsASecondaryStatInputted(_statList.secondaryStat3Name, _statList.secondaryStat3) && IsASecondaryStatInputted(_statList.secondaryStat4Name, _statList.secondaryStat4)) {
                        if (_componentRarity == ComponentRarity.legendary || _componentRarity == ComponentRarity.epic) {
                            if (_statList.rolledAbility != null) {
                                if (_componentRarity == ComponentRarity.epic) {
                                    if (_statList.setAbility != null) {
                                        return true; // epic fully inputted
                                    }
                                    else {
                                        return false; // epic without setAbility
                                    }
                                }
                                else {
                                    return true; // it's just legendary
                                }
                            }
                            else {
                                return false; // it's at least legendary but without rolledAbility
                            }
                        }
                        else {
                            return true; // it's rare
                        }
                    }
                    else {
                        return false; // it's uncommon but doesn't have stats
                    }
                }
                else {
                    return true; // it's uncommon
                }
            }
            else {
                return false; // it's common but doesn't have stats
            }
        }
        else {
            return true; // it's common
        }
    }

    private bool IsASecondaryStatInputted(string name, Stat stat) {
        if (name != null && stat.value != 0) {
            return true;
        }
        return false;
    }

    #endregion

    public override void PasteData<SOData>(SOData dataToPaste) {
        var componnetToPaste = dataToPaste as ShipComponentData;
        if (componnetToPaste == null) {
            Debug.Log("Pasting level failed");
            return;
        }

        _sprite = componnetToPaste._sprite;
        _componentName = componnetToPaste._componentName;
        _projectilePattern = componnetToPaste._projectilePattern;
        _componentType = componnetToPaste._componentType;
        _componentRarity = componnetToPaste._componentRarity;

        _statList = componnetToPaste._statList;

        _offense = componnetToPaste._offense;
        _defense = componnetToPaste._defense;
        _utility = componnetToPaste._utility;

        _description = componnetToPaste._description;
    }

    #endregion

    #region StatClasses
    [Serializable]
    public class StatList {
        private ComponentType componentType;
        [HideInInspector]
        public ComponentRarity componentRarity;
        [HideInInspector]
        public int secondaryStatsNumber = 0;

        #region ThePrimaryStat

        [HorizontalGroup("Stats")]

        [BoxGroup("Stats/Primary Stat")]
        [HorizontalGroup("Stats/Primary Stat/Split", 0.5f)]

        [HorizontalGroup("Stats/Primary Stat/Split/Left")]
        [HideLabel, ValueDropdown("AvailablePrimaryStats"), GUIColor("GetColorPrimaryStatName"), OnValueChanged("SetStatName")]
        public string primaryStatName;
        [HorizontalGroup("Stats/Primary Stat/Split/Right"), GUIColor("GetColorPrimaryStat"), HideLabel]
        public Stat primaryStat;

        #endregion

        #region TheSecondaryStats
        [HorizontalGroup("Stats2")]
        [BoxGroup("Stats2/Secondary Stats")]

        [HorizontalGroup("Stats2/Secondary Stats/First")]
        [HorizontalGroup("Stats2/Secondary Stats/First/Split", 0.5f)]

        [HorizontalGroup("Stats2/Secondary Stats/First/Split/Left")]
        [HideLabel, ValueDropdown("AvailableSecondaryStats"), OnValueChanged("SetStatName"), ShowInInspector, PropertySpace(10)]
        [ShowIf("@secondaryStatsNumber >= 2"), GUIColor("GetColorSecondaryStat1Name")]
        public string secondaryStat1Name;
        [HorizontalGroup("Stats2/Secondary Stats/First/Split/Right")]
        [HideLabel, ShowInInspector, PropertySpace(10), ShowIf("@secondaryStatsNumber >= 2"), GUIColor("GetColorSecondaryStat1")]
        public Stat secondaryStat1;

        [HorizontalGroup("Stats2/Secondary Stats/Second")]
        [HorizontalGroup("Stats2/Secondary Stats/Second/Split", 0.5f)]

        [HorizontalGroup("Stats2/Secondary Stats/Second/Split/Left")]
        [HideLabel, ValueDropdown("AvailableSecondaryStats"), OnValueChanged("SetStatName"), ShowInInspector, PropertySpace(10)]
        [ShowIf("@secondaryStatsNumber >= 2"), GUIColor("GetColorSecondaryStat2Name")]
        public string secondaryStat2Name;
        [HorizontalGroup("Stats2/Secondary Stats/Second/Split/Right")]
        [HideLabel, ShowInInspector, PropertySpace(10), ShowIf("@secondaryStatsNumber >= 2"), GUIColor("GetColorSecondaryStat2")]
        public Stat secondaryStat2;

        [HorizontalGroup("Stats2/Secondary Stats/Third")]
        [HorizontalGroup("Stats2/Secondary Stats/Third/Split", 0.5f)]

        [HorizontalGroup("Stats2/Secondary Stats/Third/Split/Left")]
        [HideLabel, ValueDropdown("AvailableSecondaryStats"), OnValueChanged("SetStatName"), ShowInInspector, PropertySpace(10)]
        [ShowIf("@secondaryStatsNumber >= 4"), GUIColor("GetColorSecondaryStat3Name")]
        public string secondaryStat3Name;
        [HorizontalGroup("Stats2/Secondary Stats/Third/Split/Right")]
        [HideLabel, ShowInInspector, PropertySpace(10), ShowIf("@secondaryStatsNumber >= 4"), GUIColor("GetColorSecondaryStat3")]
        public Stat secondaryStat3;

        [HorizontalGroup("Stats2/Secondary Stats/Forth")]
        [HorizontalGroup("Stats2/Secondary Stats/Forth/Split", 0.5f)]

        [HorizontalGroup("Stats2/Secondary Stats/Forth/Split/Left")]
        [HideLabel, ValueDropdown("AvailableSecondaryStats"), OnValueChanged("SetStatName"), ShowInInspector, PropertySpace(10)]
        [ShowIf("@secondaryStatsNumber >= 4"), GUIColor("GetColorSecondaryStat4Name")]
        public string secondaryStat4Name;
        [HorizontalGroup("Stats2/Secondary Stats/Forth/Split/Right")]
        [HideLabel, ShowInInspector, PropertySpace(10), ShowIf("@secondaryStatsNumber >= 4"), GUIColor("GetColorSecondaryStat4")]
        public Stat secondaryStat4;

        [HorizontalGroup("Stats2/Secondary Stats/RollRandomButton")]
        [ShowInInspector, PropertySpace(10), ShowIf("@secondaryStatsNumber >= 2")]
        [Button, GUIColor(0.75f, 1f, 1f)]
        public void RollRandomSecondaryStats() {
            secondaryStat1Name = null;
            secondaryStat2Name = null;
            secondaryStat3Name = null;
            secondaryStat4Name = null;

            RollRandomSecondaryStat(ref secondaryStat1Name);
            RollRandomSecondaryStat(ref secondaryStat2Name);
            RollRandomSecondaryStat(ref secondaryStat3Name);
            RollRandomSecondaryStat(ref secondaryStat4Name);

            SetStatName(secondaryStat1Name);
            SetStatName(secondaryStat2Name);
            SetStatName(secondaryStat3Name);
            SetStatName(secondaryStat4Name);
        }
        #endregion

        #region TheAbilities
        [ShowIf("@this.componentRarity == ComponentRarity.legendary || this.componentRarity == ComponentRarity.epic")]
        [TitleGroup("Rolled Ability"), GUIColor("GetColorRolledAbility"), HideLabel]
        public ScriptableObject rolledAbility;
        [TitleGroup("Set Ability")]
        [ShowIf("@this.componentRarity == ComponentRarity.epic"), GUIColor("GetColorSetAbility"), HideLabel]
        public ScriptableObject setAbility;
        #endregion

        #region AllStatsInString

        #region PrimaryStats
        private string[] HullPrimaryStats = new string[] { "Health float", "Health %" };
        private string[] ShieldPrimaryStats = new string[] { "Shield float", "Shield %" };
        private string[] CargoPrimaryStats = new string[] { "Cargo Cap float", "Cargo Cap %" };
        private string[] WeaponPrimaryStats = new string[] { "Damage float", "Damage %" };

        private string[] SpecialistPrimaryStats = new string[] { "Health float", "Health %", "Armor float", "Armor %", "Shield float", "Shield %",
                                                                 "Cargo Cap float", "Cargo Cap %", "Shield Regen Speed float", "Shield Regen Speed %",
                                                                 "CD float", "CD %", "CC float", "CC %", "Range float", "Range %", "Reload Speed float", "Reload Speed %", "LifeSteal float",
                                                                 "LifeSteal %", "Mag. Cap float", "Mag. Cap %", "FireRate %", "Projectile Speed float",
                                                                 "Projectile Speed %" };
        #endregion

        #region SecondaryStats
        private string[] HullSecondaryStats = new string[] { "Armor float", "Armor %", "Shield float", "Shield %", "Cargo Cap float",
                                                             "Cargo Cap %", "Heat Resistance float", "Heat Resistance %", "Cold Resistance float",
                                                             "Cold Resistance %", "Acid Resistance float", "Acid Resistance %",
                                                             "Electric Resistance float", "Electric Resistance %" };

        private string[] ShieldSecondaryStats = new string[] { "Shield Regen Speed float", "Shield Regen Speed %", "Shield Regen Delay float",
                                                               "Shield Regen Delay %", "Heat Resistance float", "Heat Resistance %",
                                                               "Cold Resistance float", "Cold Resistance %", "Acid Resistance float",
                                                               "Acid Resistance %", "Electric Resistance float", "Electric Resistance %" };

        private string[] WeaponSecondaryStats = new string[] { "Cargo Cap float", "Cargo Cap %", "Shield Regen Speed float", "Shield Regen Speed %",
                                                               "CD float", "CD %", "CC float", "CC %",
                                                               "Range float", "Range %", "Reload Speed float", "Reload Speed %", "LifeSteal float",
                                                               "LifeSteal %", "Mag. Cap float", "Mag. Cap %", "FireRate %", "Projectile Speed float",
                                                               "Projectile Speed %" };

        private string[] SpecialistSecondaryStats = new string[] { "Health float", "Health %", "Armor float", "Armor %", "Shield float", "Shield %",
                                                                   "Cargo Cap float", "Cargo Cap %", "Shield Regen Speed float", "Shield Regen Speed %",
                                                                   "CD float", "CD %", "CC float", "CC %",
                                                                   "Range float", "Range %", "Reload Speed float", "Reload Speed %", "LifeSteal float",
                                                                   "LifeSteal %", "Mag. Cap float", "Mag. Cap %", "FireRate %", "Projectile Speed float",
                                                                   "Projectile Speed %" };

        private string[] CargoSecondaryStats = new string[] { "Health float", "Health %", "Armor float", "Armor %", "Shield float", "Shield %",
                                                              "Heat Resistance float", "Heat Resistance %", "Cold Resistance float",
                                                              "Cold Resistance %", "Acid Resistance float", "Acid Resistance %",
                                                              "Electric Resistance float", "Electric Resistance %" };
        #endregion

        #region DropDownAvailableStats
        private string[] AvailablePrimaryStats = new string[] { "Health float", "Health %" };

        private string[] AvailableSecondaryStats = new string[] { "Armor float", "Armor %", "Shield float", "Shield %", "Cargo Cap float",
                                                                  "Cargo Cap %", "Heat Resistance float", "Heat Resistance %", "Cold Resistance float",
                                                                  "Cold Resistance %", "Acid Resistance float", "Acid Resistance %",
                                                                  "Electric Resistance float", "Electric Resistance %" };
        #endregion

        #endregion

        #region StatMethods

        public void ReNewData(ComponentType componentType, ComponentRarity componentRarity) {
            this.componentType = componentType;
            this.componentRarity = componentRarity;

            SetStatsNumber();
            SetAvailableStatLists();
            SetPrimaryStat();
            ClearSecondaryStats();

            if (this.componentRarity != ComponentRarity.legendary && this.componentRarity != ComponentRarity.epic) {
                rolledAbility = null;
            }

            if (this.componentRarity != ComponentRarity.epic) {
                setAbility = null;
            }
        }

        private void RollRandomSecondaryStat(ref string target) {
            int random = RollRandomNumber();
            string stat = AvailableSecondaryStats[random];
            if (secondaryStat1Name != stat && secondaryStat2Name != stat && secondaryStat3Name != stat && secondaryStat4Name != stat) {
                target = stat;
                return;
            }
            else {
                RollRandomSecondaryStat(ref target);
            }
        }

        private int RollRandomNumber() {
            return UnityEngine.Random.Range(0, AvailableSecondaryStats.Length - 1);
        }

        private void SetAvailableStatLists() {
            switch (componentType) {
                case ComponentType.hull:
                    AvailablePrimaryStats = HullPrimaryStats;
                    AvailableSecondaryStats = HullSecondaryStats;
                    break;
                case ComponentType.shield:
                    AvailablePrimaryStats = ShieldPrimaryStats;
                    AvailableSecondaryStats = ShieldSecondaryStats;
                    break;
                case ComponentType.weapon:
                    AvailablePrimaryStats = WeaponPrimaryStats;
                    AvailableSecondaryStats = WeaponSecondaryStats;
                    break;
                case ComponentType.specialist:
                    AvailablePrimaryStats = SpecialistPrimaryStats;
                    AvailableSecondaryStats = SpecialistSecondaryStats;
                    break;
                case ComponentType.cargo:
                    AvailablePrimaryStats = CargoPrimaryStats;
                    AvailableSecondaryStats = CargoSecondaryStats;
                    break;
            }
        }

        private void SetStatName(string statName) {
            if (System.Object.ReferenceEquals(statName, secondaryStat1Name)) {
                secondaryStat1.name = secondaryStat1Name;
                return;
            }

            if (System.Object.ReferenceEquals(statName, secondaryStat2Name)) {
                secondaryStat2.name = secondaryStat2Name;
                return;
            }

            if (System.Object.ReferenceEquals(statName, secondaryStat3Name)) {
                secondaryStat3.name = secondaryStat3Name;
                return;
            }

            if (System.Object.ReferenceEquals(statName, secondaryStat4Name)) {
                secondaryStat4.name = secondaryStat4Name;
                return;
            }

            if (System.Object.ReferenceEquals(statName, primaryStatName)) {
                primaryStat.name = primaryStatName;
                return;
            }
        }

        public void SetStatsNumber() {
            switch (this.componentRarity) {
                case ComponentRarity.common:
                    secondaryStatsNumber = 0;
                    break;
                case ComponentRarity.uncommon:
                    secondaryStatsNumber = 2;
                    break;
                case ComponentRarity.rare:
                    secondaryStatsNumber = 4;
                    break;
                case ComponentRarity.legendary:
                    secondaryStatsNumber = 4;
                    break;
                case ComponentRarity.epic:
                    secondaryStatsNumber = 4;
                    break;
            }
        }

        private void SetPrimaryStat() {
            switch (this.componentType) {
                case ComponentType.hull:
                    primaryStat.name = "Health float";
                    primaryStat.value = 0;
                    primaryStatName = primaryStat.name;
                    break;
                case ComponentType.shield:
                    primaryStat.name = "Shield float";
                    primaryStat.value = 0;
                    primaryStatName = primaryStat.name;
                    break;
                case ComponentType.weapon:
                    primaryStat.name = "Damage float";
                    primaryStat.value = 0;
                    primaryStatName = primaryStat.name;
                    break;
                case ComponentType.specialist:
                    primaryStat.name = "Health float";
                    primaryStat.value = 0;
                    primaryStatName = primaryStat.name;
                    break;
                case ComponentType.cargo:
                    primaryStat.name = "Cargo Cap float";
                    primaryStat.value = 0;
                    primaryStatName = primaryStat.name;
                    break;
            }
        }

        private void ClearSecondaryStats() {
            if (secondaryStatsNumber < 4) {
                secondaryStat3Name = null;
                secondaryStat3 = null;

                secondaryStat4Name = null;
                secondaryStat4 = null;
            }

            if (secondaryStatsNumber < 2) {
                secondaryStat1Name = null;
                secondaryStat1 = null;

                secondaryStat2Name = null;
                secondaryStat2 = null;
            }
        }

        #endregion

        #region PropertyColorManager

        private Color GetColorPrimaryStatName() { return (this.primaryStatName == null || this.primaryStatName == "") ? redColor : greyColor; }
        private Color GetColorPrimaryStat() { return this.primaryStat.value == 0 ? redColor : greyColor; }


        private Color GetColorSecondaryStat1Name() { return (this.secondaryStat1Name == null || this.secondaryStat1Name == "") ? redColor : greyColor; }
        private Color GetColorSecondaryStat1() { return this.secondaryStat1.value == 0 ? redColor : greyColor; }
        private Color GetColorSecondaryStat2Name() { return (this.secondaryStat2Name == null || this.secondaryStat2Name == "") ? redColor : greyColor; }
        private Color GetColorSecondaryStat2() { return this.secondaryStat2.value == 0 ? redColor : greyColor; }
        private Color GetColorSecondaryStat3Name() { return (this.secondaryStat3Name == null || this.secondaryStat3Name == "") ? redColor : greyColor; }
        private Color GetColorSecondaryStat3() { return this.secondaryStat3.value == 0 ? redColor : greyColor; }
        private Color GetColorSecondaryStat4Name() { return (this.secondaryStat4Name == null || this.secondaryStat4Name == "") ? redColor : greyColor; }
        private Color GetColorSecondaryStat4() { return this.secondaryStat4.value == 0 ? redColor : greyColor; }


        private Color GetColorRolledAbility() { return this.rolledAbility == null ? redColor : greyColor; }
        private Color GetColorSetAbility() { return this.setAbility == null ? redColor : greyColor; }

        #endregion
    }

    [Serializable]
    public class Stat {
        [HideLabel, HideInInspector]
        public string name;
        [VerticalGroup("Value"), HideLabel, MinValue(0)]
        public float value;
    }
    #endregion

    #region PropertyColorManager

    private Color GetColorImage() { return this._sprite == null ? redColor : greyColor; }
    private Color GetColorName() { return (this._componentName == null || this._componentName == "") ? redColor : greyColor; }
    private Color GetColorPattern() { return this._projectilePattern == null ? redColor : greyColor; }
    private Color GetColorOffense() { return this._offense == 0 ? redColor : greyColor; }
    private Color GetColorDefense() { return this._defense == 0 ? redColor : greyColor; }
    private Color GetColorUtility() { return this._utility == 0 ? redColor : greyColor; }

    #endregion
}

#region Enums

public enum ComponentType {
    hull,
    shield,
    weapon,
    specialist,
    cargo
}

public enum ComponentRarity {
    common,
    uncommon,
    rare,
    legendary,
    epic
}

#endregion