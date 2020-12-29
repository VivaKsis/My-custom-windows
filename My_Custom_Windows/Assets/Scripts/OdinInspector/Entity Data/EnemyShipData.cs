using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "EnemyShipData", menuName = "SO Data/Enemy Ship Data")]
public class EnemyShipData : EntityData {

    #region GeneralParameters

    [HorizontalGroup("Parameters")]
    [BoxGroup("Parameters/General")]

    [SerializeField, HorizontalGroup("Parameters/General/Left", 75), PreviewField(75), HideLabel, GUIColor("GetColorImage")]
    private Sprite _enemyShipSprite;
    public Sprite _EnemyShipSprite => _enemyShipSprite;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor(0.8f, 0.8f, 0.8f)]
    private EnemyFaction _faction;
    public EnemyFaction _Faction => _faction;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor("GetColorName")]
    private string _shipName;
    public string _ShipName => _shipName;
    [SerializeField, VerticalGroup("Parameters/General/Left/Right"), LabelWidth(110), GUIColor(0.8f, 0.8f, 0.8f)]
    private EnemyShipClass _enemyShipClass;
    public EnemyShipClass _EnemyShipClass => _enemyShipClass;

    #endregion

    #region Stats

    [FoldoutGroup("Stats")]

    [TitleGroup("Stats/Ship")]
    [HorizontalGroup("Stats/Ship/General", 0.5f)]

    [SerializeField, VerticalGroup("Stats/Ship/General/Left"), LabelWidth(100), GUIColor("GetColorHealth")]
    private float _health;
    public float _Health => _health;
    [SerializeField, VerticalGroup("Stats/Ship/General/Left"), LabelWidth(100), GUIColor("GetColorArmor"), OnValueChanged("ConfirmArmor")]
    private float _armor;
    public float _Armor => _armor;

    [SerializeField, VerticalGroup("Stats/Ship/General/Right"), LabelWidth(130), GUIColor("GetColorShield"), OnValueChanged("ConfirmShield")]
    private float _shield;
    public float _Shield => _shield;
    [SerializeField, VerticalGroup("Stats/Ship/General/Right"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f)]
    private float _shieldRegenSpeed;
    public float _ShieldRegenSpeed => _shieldRegenSpeed;
    [SerializeField, VerticalGroup("Stats/Ship/General/Right"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f)]
    private float _shieldDelayRegen;
    public float _ShieldDelayRegen => _shieldDelayRegen;

    [TitleGroup("Stats/Weapon")]
    [HorizontalGroup("Stats/Weapon/General", 0.5f)]

    [SerializeField, VerticalGroup("Stats/Weapon/General/Left"), LabelWidth(100), GUIColor("GetColorDamage")]
    private float _damage;
    public float _Damage => _damage;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left"), LabelWidth(100), GUIColor("GetColorMagazineCap"), OnValueChanged("ConfirmMagazineCap")]
    private float _magazineCap;
    public float _MagazineCap => _magazineCap;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left"), LabelWidth(100), GUIColor("GetColorFireRate")]
    private float _fireRate;
    public float _FireRate => _fireRate;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left"), LabelWidth(100), GUIColor("GetColorProjectileSpeed"), OnValueChanged("ConfirmProjectileSpeed")]
    private float _projectileSpeed;
    public float _ProjectileSpeed => _projectileSpeed;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Left"), LabelWidth(100), GUIColor(0.8f, 0.8f, 0.8f)]
    private float _lifeSteal;
    public float _LifeSteal => _lifeSteal;

    [SerializeField, VerticalGroup("Stats/Weapon/General/Right"), LabelWidth(130), GUIColor("GetColorCD"), OnValueChanged("ConfirmCD")]
    private float _cD;
    public float _CD => _cD;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right"), LabelWidth(130), GUIColor("GetColorCC"), OnValueChanged("ConfirmCC")]
    private float _cC;
    public float _CC => _cC;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right"), LabelWidth(130), GUIColor("GetColorRange"), OnValueChanged("ConfirmRange")]
    private float _range;
    public float _Range => _range;
    [SerializeField, VerticalGroup("Stats/Weapon/General/Right"), LabelWidth(130), GUIColor(0.8f, 0.8f, 0.8f)]
    private float _reloadSpeed;
    public float _ReloadSpeed => _reloadSpeed;

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

    private string enemyShipPath = "Assets/Prefab/EnemyShips";

    #endregion

    #region Overrides

    public override void SetPath() {
        path = enemyShipPath;
    }
    public override void SetName() {
        objectName = _shipName + ".asset";

    }
    public override bool IsEachFieldInputted() {
        if (_enemyShipSprite != null && _shipName != null && _health != 0 && _damage != 0 && _fireRate != 0 && _offense != 0 && _defense != 0 && _utility != 0) {
            objectName = _shipName;
            return true;
        }
        return false;
    }

    public override void PasteData<SOData>(SOData dataToPaste) {
        var enemyShipToPaste = dataToPaste as EnemyShipData;
        if (enemyShipToPaste == null) {
            Debug.Log("Pasting level failed");
            return;
        }

        _enemyShipSprite = enemyShipToPaste._enemyShipSprite;
        _faction = enemyShipToPaste._faction;
        _shipName = enemyShipToPaste._shipName;
        _enemyShipClass = enemyShipToPaste._enemyShipClass;

        _health = enemyShipToPaste._health;
        _armor = enemyShipToPaste._armor;
        _shield = enemyShipToPaste._shield;
        _shieldRegenSpeed = enemyShipToPaste._shieldRegenSpeed;
        _shieldDelayRegen = enemyShipToPaste._shieldDelayRegen;
        _cD = enemyShipToPaste._cD;
        _cC = enemyShipToPaste._cC;
        _range = enemyShipToPaste._range;
        _reloadSpeed = enemyShipToPaste._reloadSpeed;
        _damage = enemyShipToPaste._damage;
        _lifeSteal = enemyShipToPaste._lifeSteal;
        _magazineCap = enemyShipToPaste._magazineCap;
        _fireRate = enemyShipToPaste._fireRate;
        _projectileSpeed = enemyShipToPaste._projectileSpeed;

        _offense = enemyShipToPaste._offense;
        _defense = enemyShipToPaste._defense;
        _utility = enemyShipToPaste._utility;

        _description = enemyShipToPaste._description;
    }

    #endregion

    #region PropertyColorManager

    private Color GetColorImage() { return this._enemyShipSprite == null ? redColor : greyColor; }


    private Color GetColorName() { return (this._shipName == null || this._shipName == "") ? redColor : greyColor; }


    //Hardly required stats
    private Color GetColorHealth() { return this._health == 0 ? redColor : greyColor; }
    private Color GetColorDamage() { return this._damage == 0 ? redColor : greyColor; }
    private Color GetColorFireRate() { return this._fireRate == 0 ? redColor : greyColor; }


    // Modifiers
    private Color GetColorOffense() { return this._offense == 0 ? redColor : greyColor; }
    private Color GetColorDefense() { return this._defense == 0 ? redColor : greyColor; }
    private Color GetColorUtility() { return this._utility == 0 ? redColor : greyColor; }


    //Recommended to be more than '0'
    private bool isArmorConfirmed, isShieldConfirmed, isCDConfirmed, isCCConfirmed, isRangeConfirmed, isMagazineCapConfirmed, isProjectileSpeedConfirmed;
    private void ConfirmArmor() { isArmorConfirmed = true; }
    private void ConfirmShield() { isShieldConfirmed = true; }
    private void ConfirmCD() { isCDConfirmed = true; }
    private void ConfirmCC() { isCCConfirmed = true; }
    private void ConfirmRange() { isRangeConfirmed = true; }
    private void ConfirmMagazineCap() { isMagazineCapConfirmed = true; }
    private void ConfirmProjectileSpeed() { isProjectileSpeedConfirmed = true; }

    private Color GetColorArmor() { return !isArmorConfirmed ? redColor : greyColor; }
    private Color GetColorShield() { return !isShieldConfirmed ? redColor : greyColor; }
    private Color GetColorCD() { return !isCDConfirmed ? redColor : greyColor; }
    private Color GetColorCC() { return !isCCConfirmed ? redColor : greyColor; }
    private Color GetColorRange() { return !isRangeConfirmed ? redColor : greyColor; }
    private Color GetColorMagazineCap() { return !isMagazineCapConfirmed ? redColor : greyColor; }
    private Color GetColorProjectileSpeed() { return !isProjectileSpeedConfirmed ? redColor : greyColor; }

    #endregion
}

#region Enums

public enum EnemyFaction {
    StarCatchers,
    Entropods,
    Pirates,
    Opulids
}

public enum EnemyShipClass {
    lightFighter,
    heavyFighter,
    kamikaze,
    ace,
    heavyBomber
}

#endregion
