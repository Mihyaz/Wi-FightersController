using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{

    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private IPAddress _myIpAddress;
    #endregion

    public string[] IPText = new string[2];
    public string MyName;
    public string MyClass;
    public bool Connected;

    [Header("Text Fields")]
    public TMP_InputField NameField;
    public TMP_InputField IP_Field_1;
    public TMP_InputField IP_Field_2;

    public Button Submit;

    [Header("Sections")]
    public GameObject SectionIP;
    public GameObject SectionName;
    public GameObject SectionClass;
    public GameObject SectionConnectionDimmer;

    // Use this for initialization 	
    void Start()
    {
        //ConnectToTcpServer();
    }

    public void AssignIP()
    {
        IPText[0] = IP_Field_1.text;
        IPText[1] = IP_Field_2.text;
        SectionIP.SetActive(false);
        SectionClass.SetActive(true);
    }

    public void CreatePlayerName()
    {
        MyName = NameField.text;
        SectionName.SetActive(false);
        SectionConnectionDimmer.SetActive(false);
        ConnectToTcpServer();
    }

    public void CreatePlayerClass(int index)
    {
        SectionClass.SetActive(false);
        SectionName.SetActive(true);
        switch (index)
        {
            case 0:
                MyClass = "Rifle";
                break;
            case 1:
                MyClass = "Shotgun";
                break;
            case 2:
                MyClass = "Handgun";
                break;
            case 3:
                MyClass = "Laser";
                break;
            default:
                MyClass = "Rifle";
                break;
        }
    }

    public void Reconnect()
    {
        SectionIP.SetActive(true);
        SectionName.SetActive(false);
        SectionConnectionDimmer.SetActive(true);
    }

    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData))
            {
                IsBackground = true
            };
            clientReceiveThread.Start();

        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient("192.168." + IPText[0] + "." + IPText[1], 8000);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        string[] message = serverMessage.Split('+');

                        if (MyName == message[0])
                        {
                            if(message[1] == "Shoot")
                            {
                                SoundManager.Instance.isShooting = true;
                            }
                            else if(message[1] == "Reload")
                            {
                                SoundManager.Instance.isReloading = true;
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    public async virtual void SendMessageToServer(string clientMessage, string type)
    {
        if (socketConnection == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            if (stream.CanWrite)
            {
               await writer.WriteLineAsync(clientMessage + "+" + type);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public virtual void SendImageToServer(UnityEngine.UI.Image x)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));

            if (stream.CanWrite)
            {
                //stream.Write(xByte, 0, xByte.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    public virtual void SendMessageToServer(string clientMessage)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            StreamWriter writer = new StreamWriter(socketConnection.GetStream());
            writer.AutoFlush = true; //Either this, or you Flush manually every time you send something.

            if (stream.CanWrite)
            {
                // Convert string message to byte array. 
                //byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.
                writer.WriteLine(clientMessage);

                //stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}