using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;
using System;

public class Bluetooth : MonoBehaviour
{
    BluetoothHelper bluetoothHelper;
    string deviceName;
    string received_minutes;
    int Minutes;

    // Start is called before the first frame update
    void Start()
    {
        deviceName = "ESP32-S3"; //bluetooth should be turned ON;

        BluetoothHelper.BLE = false;
        bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
        bluetoothHelper.OnConnected += OnConnected;
        bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
        bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data
        bluetoothHelper.OnScanEnded += OnScanEnded;
    }

    void OnMessageReceived(BluetoothHelper helper)
    {
        received_minutes = helper.Read();
        Debug.Log(received_minutes);
        Minutes = int.Parse(received_minutes);
        Debug.Log(Minutes);
    }

    public int getMinutes()
    {
        return Minutes;
    }

    void OnConnected(BluetoothHelper helper)
    {
        helper.StartListening();
    }

    void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {

        if (helper.isDevicePaired()) //we did found our device (with BLE) or we already paired the device (for Bluetooth Classic)
            helper.Connect();
        else
            helper.ScanNearbyDevices(); //we didn't
    }
    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Connection Failed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }
}
