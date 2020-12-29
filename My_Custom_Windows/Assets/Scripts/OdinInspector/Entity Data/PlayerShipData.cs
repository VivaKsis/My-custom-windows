using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "PlayerShipData", menuName = "SO Data/Player Ship Data")]
public class PlayerShipData : EntityData {

    #region GeneralParameters
    [HorizontalGroup("Parameters")]
    [BoxGroup("Parameters/General")]

    [SerializeField, HorizontalGroup("Parameters/General/Left", 75), PreviewField(75), HideLabel, GUIColor("GetColorImage")]
    private Sprite _sprite;
    public Sprite _Sprite => _sprite;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor("GetColorName")]
    private string _shipName;
    public string _ShipName => _shipName;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor(0.8f, 0.8f, 0.8f)]
    private PlayerShipClass _playerShipClass;
    public PlayerShipClass _PlayerShipClass => _playerShipClass;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor("GetColorTokens")]
    private int _tokensToUnlock;
    public int _TokensToUnlock => _tokensToUnlock;
    #endregion

    #region Components

    [SerializeField, LabelWidth(150), GUIColor(0.75f, 1f, 1f), OnValueChanged("SumComponentsStatsToBonuses"), AssetSelector(Paths = "Assets/Prefab/ShipComponents")]
    public ShipComponentData[] _components;
    public ShipComponentData[] _Components => _components;

    public void SumComponentsStatsToBonuses() {
        ClearBonuses();
        foreach (ShipComponentData component in _components) {
            AddStatsFromComponent(component);
        }
        BonusesToString();
    }

    private void AddStatsFromComponent(ShipComponentData component) {
        AddStat(component._StatList.primaryStat);
        AddStat(component._StatList.secondaryStat1);
        AddStat(component._StatList.secondaryStat2);
        AddStat(component._StatList.secondaryStat3);
        AddStat(component._StatList.secondaryStat4);
    }

    private void AddStat(ShipComponentData.Stat stat) {
        if (stat.name.Length == 0) {
            return;
        }
        switch (stat.name) {
            #region FloatStats
            case "Health float":
                healthBonus.value += stat.value;
                break;
            case "Armor float":
                armorBonus.value += stat.value;
                break;
            case "Shield float":
                shieldBonus.value += stat.value;
                break;
            case "Shield Regen Speed float":
                shieldRegenSpeedBonus.value += stat.value;
                break;
            case "Cargo Cap float":
                cargoCapBonus.value += stat.value;
                break;
            case "Shield Regen Delay float":
                shieldDelayRegenBonus.value += stat.value;
                break;
            case "CD float":
                CriticalDamageBonus.value += stat.value;
                break;
            case "CC float":
                CriticalChanceBonus.value += stat.value;
                break;
            case "Range float":
                rangeBonus.value += stat.value;
                break;
            case "Reload Speed float":
                reloadSpeedBonus.value += stat.value;
                break;
            case "Damage float":
                damageBonus.value += stat.value;
                break;
            case "LifeSteal float":
                lifeStealBonus.value += stat.value;
                break;
            case "Mag. Cap float":
                magazineCapBonus.value += stat.value;
                break;
            case "FireRate float":
                fireRateBonus.value += stat.value;
                break;
            case "Projectile Speed float":
                projectileSpeedBonus.value += stat.value;
                break;
            case "Heat Resistance float":
                heatResBonus.value += stat.value;
                break;
            case "Cold Resistance float":
                coldResBonus.value += stat.value;
                break;
            case "Acid Resistance float":
                acidResBonus.value += stat.value;
                break;
            case "Electric Resistance float":
                electricResBonus.value += stat.value;
                break;
            #endregion

            #region %Stats
            case "Health %":
                healthBonus.value += _health * (stat.value / 100);
                break;
            case "Armor %":
                armorBonus.value += _armor * (stat.value / 100);
                break;
            case "Shield %":
                shieldBonus.value += _shield * (stat.value / 100);
                break;
            case "Shield Regen Speed %":
                shieldRegenSpeedBonus.value += _shieldRegenSpeed * (stat.value / 100);
                break;
            case "Cargo Cap %":
                cargoCapBonus.value += _cargoCap * (stat.value / 100);
                break;
            case "Shield Regen Delay %":
                shieldDelayRegenBonus.value += _shieldDelayRegen * (stat.value / 100);
                break;
            case "CD %":
                CriticalDamageBonus.value += _criticalDamage * (stat.value / 100);
                break;
            case "CC %":
                CriticalChanceBonus.value += _criticalChance * (stat.value / 100);
                break;
            case "Range %":
                rangeBonus.value += _range * (stat.value / 100);
                break;
            case "Reload Speed %":
                reloadSpeedBonus.value += _reloadSpeed * (stat.value / 100);
                break;
            case "Damage %":
                damageBonus.value += _damage * (stat.value / 100);
                break;
            case "LifeSteal %":
                lifeStealBonus.value += _lifeSteal * (stat.value / 100);
                break;
            case "Mag. Cap %":
                magazineCapBonus.value += _magazineCap * (stat.value / 100);
                break;
            case "FireRate %":
                fireRateBonus.value += _fireRate * (stat.value / 100);
                break;
            case "Projectile Speed %":
                projectileSpeedBonus.value += _projectileSpeed * (stat.value / 100);
                break;
            case "Heat Resistance %":
                heatResBonus.value += _heatRes * (stat.value / 100);
                break;
            case "Cold Resistance %":
                coldResBonus.value += _coldRes * (stat.value / 100);
                break;
            case "Acid Resistance %":
                acidResBonus.value += _acidRes * (stat.value / 100);
                break;
            case "Electric Resistance %":
                electricResBonus.value += _electricRes * (stat.value / 100);
                break;
            #endregion

            default:
                Debug.Log("Problem with: " + stat.name);
                break;
        }
    }

    private void ClearBonuses() {
        healthBonus.value = 0;
        armorBonus.value = 0;
        cargoCapBonus.value = 0;

        shieldBonus.value = 0;
        shieldRegenSpeedBonus.value = 0;
        shieldDelayRegenBonus.value = 0;

        damageBonus.value = 0;
        magazineCapBonus.value = 0;
        fireRateBonus.value = 0;
        projectileSpeedBonus.value = 0;
        lifeStealBonus.value = 0;

        CriticalDamageBonus.value = 0;
        CriticalChanceBonus.value = 0;
        rangeBonus.value = 0;
        reloadSpeedBonus.value = 0;

        heatResBonus.value = 0;
        coldResBonus.value = 0;

        acidResBonus.value = 0;
        electricResBonus.value = 0;
    }

    private void BonusesToString() {
        healthBonus.ValueToString();
        armorBonus.ValueToString();
        cargoCapBonus.ValueToString();

        shieldBonus.ValueToString();
        shieldRegenSpeedBonus.ValueToString();
        shieldDelayRegenBonus.ValueToString();

        damageBonus.ValueToString();
        magazineCapBonus.ValueToString();
        fireRateBonus.ValueToString();
        projectileSpeedBonus.ValueToString();
        lifeStealBonus.ValueToString();

        CriticalDamageBonus.ValueToString();
        CriticalChanceBonus.ValueToString();
        rangeBonus.ValueToString();
        reloadSpeedBonus.ValueToString();

        heatResBonus.ValueToString();
        coldResBonus.ValueToString();

        acidResBonus.ValueToString();
        electricResBonus.ValueToString();
    }

    #endregion

    #region Stats

    [FoldoutGroup("Stats")]

    [TitleGroup("Stats/Ship")]
    [HorizontalGroup("Stats/Ship/General", 0.5f)]

    [HorizontalGroup("Stats/Ship/General/Left")]

    [SerializeField, VerticalGroup("Stats/Ship/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorHealth"), SuffixLabel("@healthBonus.ValueToString()")]
    private float _health;
    public float _Health => _health;
    [HideInInspector]
    public ComponentBonus healthBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Ship/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorArmor"), OnValueChanged("ConfirmArmor"), SuffixLabel("@armorBonus.ValueToString()")]
    private float _armor;
    public float _Armor => _armor;
    [HideInInspector]
    public ComponentBonus armorBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Ship/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorCargoCap"), OnValueChanged("ConfirmCargoCap"), SuffixLabel("@cargoCapBonus.ValueToString()")]
    private float _cargoCap;
    public float _CargoCap => _cargoCap;
    [HideInInspector]
    public ComponentBonus cargoCapBonus = new ComponentBonus();

    [HorizontalGroup("Stats/Ship/General/Right")]

    [SerializeField, VerticalGroup("Stats/Ship/General/Right/Stat"), LabelWidth(130), GUIColor("GetColorShield"), OnValueChanged("ConfirmShield"), SuffixLabel("@shieldBonus.ValueToString()")]
    private float _shield;
    public float _Shield => _shield;
    [HideInInspector]
    public ComponentBonus shieldBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Ship/General/Right/Stat"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@shieldRegenSpeedBonus.ValueToString()")]
    private float _shieldRegenSpeed;
    public float _ShieldRegenSpeed => _shieldRegenSpeed;
    [HideInInspector]
    public ComponentBonus shieldRegenSpeedBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Ship/General/Right/Stat"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@shieldDelayRegenBonus.ValueToString()")]
    private float _shieldDelayRegen;
    public float _ShieldDelayRegen => _shieldDelayRegen;
    [HideInInspector]
    public ComponentBonus shieldDelayRegenBonus = new ComponentBonus();

    [TitleGroup("Stats/Weapon")]
    [HorizontalGroup("Stats/Weapon/General", 0.5f)]

    [HorizontalGroup("Stats/Weapon/General/Left")]

    [SerializeField, VerticalGroup("Stats/Weapon/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorDamage"), SuffixLabel("@damageBonus.ValueToString()")]
    private float _damage;
    public float _Damage => _damage;
    [HideInInspector]
    public ComponentBonus damageBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorMagazineCap"), OnValueChanged("ConfirmMagazineCap"), SuffixLabel("@magazineCapBonus.ValueToString()")]
    private float _magazineCap;
    public float _MagazineCap => _magazineCap;
    [HideInInspector]
    public ComponentBonus magazineCapBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorFireRate"), SuffixLabel("@fireRateBonus.ValueToString()")]
    private float _fireRate;
    public float _FireRate => _fireRate;
    [HideInInspector]
    public ComponentBonus fireRateBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left/Stat"), LabelWidth(100), GUIColor("GetColorProjectileSpeed"), OnValueChanged("ConfirmProjectileSpeed"), SuffixLabel("@projectileSpeedBonus.ValueToString()")]
    private float _projectileSpeed;
    public float _ProjectileSpeed => _projectileSpeed;
    [HideInInspector]
    public ComponentBonus projectileSpeedBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left/Stat"), LabelWidth(100), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@lifeStealBonus.ValueToString()")]
    private float _lifeSteal;
    public float _LifeSteal => _lifeSteal;
    [HideInInspector]
    public ComponentBonus lifeStealBonus = new ComponentBonus();

    [HorizontalGroup("Stats/Weapon/General/Right")]

    [SerializeField, VerticalGroup("Stats/Weapon/General/Right/Stat"), LabelWidth(130), GUIColor("GetColorCriticalDamage"), OnValueChanged("ConfirmCriticalDamage"), SuffixLabel("@CriticalDamageBonus.ValueToString()")]
    private float _criticalDamage;
    public float _CriticalDamage => _criticalDamage;
    [HideInInspector]
    public ComponentBonus CriticalDamageBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right/Stat"), LabelWidth(130), GUIColor("GetColorCriticalChance"), OnValueChanged("ConfirmCriticalChance"), SuffixLabel("@CriticalChanceBonus.ValueToString()")]
    private float _criticalChance;
    public float _CriticalChance => _criticalChance;
    [HideInInspector]
    public ComponentBonus CriticalChanceBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right/Stat"), LabelWidth(130), GUIColor("GetColorRange"), OnValueChanged("ConfirmRange"), SuffixLabel("@rangeBonus.ValueToString()")]
    private float _range;
    public float _Range => _range;
    [HideInInspector]
    public ComponentBonus rangeBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right/Stat"), LabelWidth(130), GUIColor("GetColorReloadSpeed"), OnValueChanged("ConfirmReloadSpeed"), SuffixLabel("@reloadSpeedBonus.ValueToString()")]
    private float _reloadSpeed;
    public float _ReloadSpeed => _reloadSpeed;
    [HideInInspector]
    public ComponentBonus reloadSpeedBonus = new ComponentBonus();

    [TitleGroup("Stats/Resistance")]
    [HorizontalGroup("Stats/Resistance/General", 0.5f)]

    [HorizontalGroup("Stats/Resistance/General/Left")]

    [SerializeField, VerticalGroup("Stats/Resistance/General/Left/Stat"), LabelText("Heat"), LabelWidth(100), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@heatResBonus.ValueToString()")]
    private float _heatRes;
    public float _HeatRes => _heatRes;
    [HideInInspector]
    public ComponentBonus heatResBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Resistance/General/Left/Stat"), LabelText("Cold"), LabelWidth(100), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@coldResBonus.ValueToString()")]
    private float _coldRes;
    public float _ColdRes => _coldRes;
    [HideInInspector]
    public ComponentBonus coldResBonus = new ComponentBonus();

    [HorizontalGroup("Stats/Resistance/General/Right")]

    [SerializeField, VerticalGroup("Stats/Resistance/General/Right/Stat"), LabelText("Acid"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@acidResBonus.ValueToString()")]
    private float _acidRes;
    public float _AcidRes => _acidRes;
    [HideInInspector]
    public ComponentBonus acidResBonus = new ComponentBonus();
    [SerializeField, VerticalGroup("Stats/Resistance/General/Right/Stat"), LabelText("Electric"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f), SuffixLabel("@electricResBonus.ValueToString()")]
    private float _electricRes;
    public float _ElectricRes => _electricRes;
    [HideInInspector]
    public ComponentBonus electricResBonus = new ComponentBonus();

    #endregion

    #region Modifiers
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(100), GUIColor("GetColorOffense")]
    private float _offense;
    public float _Offense => _offense;
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(100), GUIColor("GetColorDefense")]
    private float _defense;
    public float _Defense => _defense;
    [SerializeField, FoldoutGroup("Modifiers"), LabelWidth(100), GUIColor("GetColorUtility")]
    private float _utility;
    public float _Utility => _utility;
    #endregion

    #region Description
    [SerializeField, FoldoutGroup("Description"), TextArea, LabelWidth(150), HideLabel, GUIColor(0.8f, 0.8f, 0.8f)]
    private string _description;
    public string _Description => _description;
    #endregion

    #region File Path
    private string playerShipPath = "Assets/Prefab/PlayerShips";
    #endregion

    #region Overrides

    public override void SetPath() {
        path = playerShipPath;
    }
    public override void SetName() {
        objectName = _shipName + ".asset";
    }
    public override bool IsEachFieldInputted() {
        if (_sprite != null && _shipName != null && _tokensToUnlock != 0 && _health != 0 && _damage != 0 && _fireRate != 0 &&
                                                                          _offense != 0 && _defense != 0 && _utility != 0) {
            objectName = _shipName;
            return true;
        }
        return false;
    }

    public override void PasteData<SOData>(SOData dataToPaste) {
        var playerShipToPaste = dataToPaste as PlayerShipData;
        if (playerShipToPaste == null) {
            Debug.Log("Pasting level failed");
            return;
        }

        _sprite = playerShipToPaste._sprite;
        _shipName = playerShipToPaste._shipName;
        _playerShipClass = playerShipToPaste._playerShipClass;
        _tokensToUnlock = playerShipToPaste._tokensToUnlock;

        _components = playerShipToPaste._components;

        _health = playerShipToPaste._health;
        _armor = playerShipToPaste._armor;
        _shield = playerShipToPaste._shield;
        _shieldRegenSpeed = playerShipToPaste._shieldRegenSpeed;
        _cargoCap = playerShipToPaste._cargoCap;
        _shieldDelayRegen = playerShipToPaste._shieldDelayRegen;
        _criticalDamage = playerShipToPaste._criticalDamage;
        _criticalChance = playerShipToPaste._criticalChance;
        _range = playerShipToPaste._range;
        _reloadSpeed = playerShipToPaste._reloadSpeed;
        _damage = playerShipToPaste._damage;
        _lifeSteal = playerShipToPaste._lifeSteal;
        _magazineCap = playerShipToPaste._magazineCap;
        _fireRate = playerShipToPaste._fireRate;
        _projectileSpeed = playerShipToPaste._projectileSpeed;
        _heatRes = playerShipToPaste._heatRes;
        _coldRes = playerShipToPaste._coldRes;
        _acidRes = playerShipToPaste._acidRes;
        _electricRes = playerShipToPaste._electricRes;

        _offense = playerShipToPaste._offense;
        _defense = playerShipToPaste._defense;
        _utility = playerShipToPaste._utility;

        _description = playerShipToPaste._description;
    }

    #endregion

    #region ComponentBonus

    [Serializable]
    public class ComponentBonus {
        [HideInInspector]
        public float value = 0;

        [GUIColor(0.8f, 0.8f, 0.8f), HideLabel, ReadOnly]
        public string valueString;

        public string ValueToString() {
            valueString = "+" + value.ToString();
            return "+" + value.ToString();
        }
    }

    #endregion

    #region PropertyColorManager

    private Color GetColorImage() { return this._sprite == null ? redColor : greyColor; }
    private Color GetColorName() { return (this._shipName == null || this._shipName == "") ? redColor : greyColor; }
    private Color GetColorTokens() { return this._tokensToUnlock == 0 ? redColor : greyColor; }


    //Hardly required stats
    private Color GetColorHealth() { return this._health == 0 ? redColor : greyColor; }
    private Color GetColorDamage() { return this._damage == 0 ? redColor : greyColor; }
    private Color GetColorFireRate() { return this._fireRate == 0 ? redColor : greyColor; }


    // Modifiers
    private Color GetColorOffense() { return this._offense == 0 ? redColor : greyColor; }
    private Color GetColorDefense() { return this._defense == 0 ? redColor : greyColor; }
    private Color GetColorUtility() { return this._utility == 0 ? redColor : greyColor; }


    //Recommended to be more than '0'
    private bool isArmorConfirmed, isCargoCapConfirmed, isShieldConfirmed, isCriticalDamageConfirmed, isCriticalChanceConfirmed, isRangeConfirmed, isReloadSpeedConfirmed, isMagazineCapConfirmed, isProjectileSpeedConfirmed;
    private void ConfirmArmor() { isArmorConfirmed = true; }
    private void ConfirmCargoCap() { isCargoCapConfirmed = true; }
    private void ConfirmShield() { isShieldConfirmed = true; }
    private void ConfirmCriticalDamage() { isCriticalDamageConfirmed = true; }
    private void ConfirmCriticalChance() { isCriticalChanceConfirmed = true; }
    private void ConfirmRange() { isRangeConfirmed = true; }
    private void ConfirmReloadSpeed() { isReloadSpeedConfirmed = true; }
    private void ConfirmMagazineCap() { isMagazineCapConfirmed = true; }
    private void ConfirmProjectileSpeed() { isProjectileSpeedConfirmed = true; }

    private Color GetColorArmor() { return !isArmorConfirmed ? redColor : greyColor; }
    private Color GetColorCargoCap() { return !isCargoCapConfirmed ? redColor : greyColor; }
    private Color GetColorShield() { return !isShieldConfirmed ? redColor : greyColor; }
    private Color GetColorCriticalDamage() { return !isCriticalDamageConfirmed ? redColor : greyColor; }
    private Color GetColorCriticalChance() { return !isCriticalChanceConfirmed ? redColor : greyColor; }
    private Color GetColorRange() { return !isRangeConfirmed ? redColor : greyColor; }
    private Color GetColorReloadSpeed() { return !isReloadSpeedConfirmed ? redColor : greyColor; }
    private Color GetColorMagazineCap() { return !isMagazineCapConfirmed ? redColor : greyColor; }
    private Color GetColorProjectileSpeed() { return !isProjectileSpeedConfirmed ? redColor : greyColor; }

    #endregion

    #region Level Editor Param

    [HideInInspector]
    public Rect playerShipDataEditorWindowCurrentSize = new Rect();

    #endregion
}

#region Enums

public enum PlayerShipClass {
    tank,
    support,
    cargo,
    fighter
}

#endregion
