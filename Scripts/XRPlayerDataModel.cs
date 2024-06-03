using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class XRPlayerDataModel
{
    [RealtimeProperty(1, true, true)] private string _playerName;
    [RealtimeProperty(2, true, true)] private int _hour;
    [RealtimeProperty(3, true, true)] private int _minute;
    [RealtimeProperty(4, true, true)] private float _latitude;
    [RealtimeProperty(5, true, true)] private float _longitude;
    [RealtimeProperty(6, true, true)] private bool _useCurrentTime;
    [RealtimeProperty(7, true, true)] private int _solarPoint;

}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class XRPlayerDataModel : RealtimeModel 
{
    
    public string playerName {
        get {
            return _playerNameProperty.value;
        }
        set {
            if (_playerNameProperty.value == value) return;
            _playerNameProperty.value = value;
            InvalidateReliableLength();
            FirePlayerNameDidChange(value);
        }
    }
    
    public int hour {
        get {
            return _hourProperty.value;
        }
        set {
            if (_hourProperty.value == value) return;
            _hourProperty.value = value;
            InvalidateReliableLength();
            FireHourDidChange(value);
        }
    }
    
    public int minute {
        get {
            return _minuteProperty.value;
        }
        set {
            if (_minuteProperty.value == value) return;
            _minuteProperty.value = value;
            InvalidateReliableLength();
            FireMinuteDidChange(value);
        }
    }
    
    public float latitude {
        get {
            return _latitudeProperty.value;
        }
        set {
            if (_latitudeProperty.value == value) return;
            _latitudeProperty.value = value;
            InvalidateReliableLength();
            FireLatitudeDidChange(value);
        }
    }
    
    public float longitude {
        get {
            return _longitudeProperty.value;
        }
        set {
            if (_longitudeProperty.value == value) return;
            _longitudeProperty.value = value;
            InvalidateReliableLength();
            FireLongitudeDidChange(value);
        }
    }
    
    public bool useCurrentTime {
        get {
            return _useCurrentTimeProperty.value;
        }
        set {
            if (_useCurrentTimeProperty.value == value) return;
            _useCurrentTimeProperty.value = value;
            InvalidateReliableLength();
            FireUseCurrentTimeDidChange(value);
        }
    }
    
    public int solarPoint {
        get {
            return _solarPointProperty.value;
        }
        set {
            if (_solarPointProperty.value == value) return;
            _solarPointProperty.value = value;
            InvalidateReliableLength();
            FireSolarPointDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(XRPlayerDataModel model, T value);
    public event PropertyChangedHandler<string> playerNameDidChange;
    public event PropertyChangedHandler<int> hourDidChange;
    public event PropertyChangedHandler<int> minuteDidChange;
    public event PropertyChangedHandler<float> latitudeDidChange;
    public event PropertyChangedHandler<float> longitudeDidChange;
    public event PropertyChangedHandler<bool> useCurrentTimeDidChange;
    public event PropertyChangedHandler<int> solarPointDidChange;
    
    public enum PropertyID : uint {
        PlayerName = 1,
        Hour = 2,
        Minute = 3,
        Latitude = 4,
        Longitude = 5,
        UseCurrentTime = 6,
        SolarPoint = 7,
    }
    
    #region Properties
    
    private ReliableProperty<string> _playerNameProperty;
    
    private ReliableProperty<int> _hourProperty;
    
    private ReliableProperty<int> _minuteProperty;
    
    private ReliableProperty<float> _latitudeProperty;
    
    private ReliableProperty<float> _longitudeProperty;
    
    private ReliableProperty<bool> _useCurrentTimeProperty;
    
    private ReliableProperty<int> _solarPointProperty;
    
    #endregion
    
    public XRPlayerDataModel() : base(null) {
        _playerNameProperty = new ReliableProperty<string>(1, _playerName);
        _hourProperty = new ReliableProperty<int>(2, _hour);
        _minuteProperty = new ReliableProperty<int>(3, _minute);
        _latitudeProperty = new ReliableProperty<float>(4, _latitude);
        _longitudeProperty = new ReliableProperty<float>(5, _longitude);
        _useCurrentTimeProperty = new ReliableProperty<bool>(6, _useCurrentTime);
        _solarPointProperty = new ReliableProperty<int>(7, _solarPoint);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _playerNameProperty.UnsubscribeCallback();
        _hourProperty.UnsubscribeCallback();
        _minuteProperty.UnsubscribeCallback();
        _latitudeProperty.UnsubscribeCallback();
        _longitudeProperty.UnsubscribeCallback();
        _useCurrentTimeProperty.UnsubscribeCallback();
        _solarPointProperty.UnsubscribeCallback();
    }
    
    private void FirePlayerNameDidChange(string value) {
        try {
            playerNameDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHourDidChange(int value) {
        try {
            hourDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireMinuteDidChange(int value) {
        try {
            minuteDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireLatitudeDidChange(float value) {
        try {
            latitudeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireLongitudeDidChange(float value) {
        try {
            longitudeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireUseCurrentTimeDidChange(bool value) {
        try {
            useCurrentTimeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSolarPointDidChange(int value) {
        try {
            solarPointDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _playerNameProperty.WriteLength(context);
        length += _hourProperty.WriteLength(context);
        length += _minuteProperty.WriteLength(context);
        length += _latitudeProperty.WriteLength(context);
        length += _longitudeProperty.WriteLength(context);
        length += _useCurrentTimeProperty.WriteLength(context);
        length += _solarPointProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _playerNameProperty.Write(stream, context);
        writes |= _hourProperty.Write(stream, context);
        writes |= _minuteProperty.Write(stream, context);
        writes |= _latitudeProperty.Write(stream, context);
        writes |= _longitudeProperty.Write(stream, context);
        writes |= _useCurrentTimeProperty.Write(stream, context);
        writes |= _solarPointProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.PlayerName: {
                    changed = _playerNameProperty.Read(stream, context);
                    if (changed) FirePlayerNameDidChange(playerName);
                    break;
                }
                case (uint) PropertyID.Hour: {
                    changed = _hourProperty.Read(stream, context);
                    if (changed) FireHourDidChange(hour);
                    break;
                }
                case (uint) PropertyID.Minute: {
                    changed = _minuteProperty.Read(stream, context);
                    if (changed) FireMinuteDidChange(minute);
                    break;
                }
                case (uint) PropertyID.Latitude: {
                    changed = _latitudeProperty.Read(stream, context);
                    if (changed) FireLatitudeDidChange(latitude);
                    break;
                }
                case (uint) PropertyID.Longitude: {
                    changed = _longitudeProperty.Read(stream, context);
                    if (changed) FireLongitudeDidChange(longitude);
                    break;
                }
                case (uint) PropertyID.UseCurrentTime: {
                    changed = _useCurrentTimeProperty.Read(stream, context);
                    if (changed) FireUseCurrentTimeDidChange(useCurrentTime);
                    break;
                }
                case (uint) PropertyID.SolarPoint: {
                    changed = _solarPointProperty.Read(stream, context);
                    if (changed) FireSolarPointDidChange(solarPoint);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _playerName = playerName;
        _hour = hour;
        _minute = minute;
        _latitude = latitude;
        _longitude = longitude;
        _useCurrentTime = useCurrentTime;
        _solarPoint = solarPoint;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
