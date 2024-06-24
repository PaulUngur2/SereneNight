using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public Transform characterCamera; // Reference to the character's camera
    public int interval = 10; // Interval in seconds between data recordings

    private string _path; // File path for saving JSON data
    private StreamWriter _writer; // StreamWriter for writing to the file
    private StringBuilder _stringBuilder; // StringBuilder for constructing JSON string
    private List<DataPoint> _dataPoints; // List to hold data points

    void Awake()
    {
        // Initialize file path with current date and time
        _path = Application.persistentDataPath + "/player " + DateTime.Now.ToString("yyyy-M-d HH-mm") + ".json";
        
        _writer = new StreamWriter(_path);
        
        _stringBuilder = new StringBuilder();
        
        // Initialize list to hold data points
        _dataPoints = new List<DataPoint>();

        // Start recording data at intervals using InvokeRepeating
        InvokeRepeating(nameof(Record), 0, interval);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // Add a data point when focus is lost
            _dataPoints.Add(new DataPointMessage("Lost focus/quit"));
            
            // Cancel data recording
            CancelInvoke();
        }
        else
        {
            // Add a data point when focus is regained
            _dataPoints.Add(new DataPointMessage("Regained focus"));
            
            // Resume data recording
            InvokeRepeating(nameof(Record), 0, interval);
        }
    }

    private void OnApplicationQuit()
    {
        // Begin constructing JSON array
        _stringBuilder.Append("[");

        // Iterate through data points to convert to JSON format
        for (int i = 0; i < _dataPoints.Count; i++)
        {
            _stringBuilder.Append(JsonUtility.ToJson(_dataPoints[i], true));
            _stringBuilder.Append(i != _dataPoints.Count - 1 ? "," : "]"); // Add comma if not the last data point
        }

        // Convert StringBuilder to string
        var jsonString = _stringBuilder.ToString();
        
        _writer.Write(jsonString);
        
        _writer.Close();
    }

    private void Record()
    {
        // Record the current position, rotation, and forward direction of the character's camera
        _dataPoints.Add(new DataPointTransform(
            characterCamera.position,
            characterCamera.rotation.eulerAngles,
            characterCamera.forward)
        );
    }
}

// Base class for all data points
internal class DataPoint
{
    private static int _id = 0; // Static ID counter for all data points
    public int id; // Unique ID of each data point
    public string dateTime;

    protected DataPoint()
    {
        id = _id; // Assign unique ID
        dateTime = DateTime.Now.ToString("u"); // Get current date and time in universal sortable format (ISO 8601)

        _id++; // Increment ID counter for the next data point
    }
}

// Data point class for recording transform data (position, rotation, forward direction)
internal class DataPointTransform : DataPoint
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Forward;

    public DataPointTransform(Vector3 position, Vector3 rotation, Vector3 forward)
    {
        Position = position;
        Rotation = rotation;
        Forward = forward;
    }
}

// Data point class for recording messages
internal class DataPointMessage : DataPoint
{
    public string Message;

    public DataPointMessage(string message)
    {
        Message = message;
    }
}