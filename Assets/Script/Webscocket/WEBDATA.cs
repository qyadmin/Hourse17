
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BestHTTP;
using BestHTTP.WebSocket;

public class WabData
{
    public static string m_ipaddress;
    
	/// <summary>  
	/// The WebSocket address to connect 
	/// </summary>  
	private string address ="ws://"+ m_ipaddress+":8282";
    //private readonly string address = "ws://47.94.43.248:8282"; 
    /// <summary>  
    /// Default text to send  
    /// </summary>  
    private string _msgToSend = "Hello World!";

	/// <summary>  
	/// Debug text to draw on the gui  
	/// </summary>  
	private string _text = string.Empty;

	/// <summary>  
	/// Saved WebSocket instance  
	/// </summary>  
	private WebSocket _webSocket;

	private Queue<DataInfo> _msgQueue = new Queue<DataInfo>();
	private Queue<string> _msgQueueStr = new Queue<string>();

	public Queue<string> MsgQueueStr { get { return _msgQueueStr; } } 
	public Queue<DataInfo> MsgQueue { get { return _msgQueue; } } 
	public WebSocket WebSocket { get { return _webSocket; } }
    public string Address { get { return address; } }
    public string Text { get { return _text; } }

	public string MsgToSend
	{
		get { return _msgToSend; }
		set
		{
			_msgToSend = value;
			SendMsg(value);
		}
	}

	public void OpenWebSocket()
	{
		if (_webSocket == null)
		{
			// Create the WebSocket instance  
			_webSocket = new WebSocket(new Uri(address));

			if (HTTPManager.Proxy != null)
				_webSocket.InternalRequest.Proxy = new HTTPProxy(HTTPManager.Proxy.Address, HTTPManager.Proxy.Credentials, false);

			// Subscribe to the WS events  
			_webSocket.OnOpen += OnOpen;
			_webSocket.OnMessage += OnMessageReceived;
			_webSocket.OnClosed += OnClosed;
			_webSocket.OnError += OnError;
			// Start connecting to the server  
			_webSocket.Open();
		}
	}

	public bool IsContent{ get { return _webSocket.IsOpen; } }

	public void SendMsg(string msg)
	{
		// Send message to the server  
		_webSocket.Send(msg);
	}

	public void CloseSocket()
	{
		// Close the connection  
		_webSocket.Close(1000, "Bye!");
	}

	/// <summary>  
	/// Called when the web socket is open, and we are ready to send and receive data  
	/// </summary>  
	void OnOpen(WebSocket ws)
	{
		Debug.Log("-WebSocket Open!\n");
	}

	/// <summary>  
	/// Called when we received a text message from the server  
	/// </summary>  
	void OnMessageReceived(WebSocket ws, string message)
	{
        //Debug.Log (message);
        //DataInfo datainfo = JsonUtility.FromJson<DataInfo>(message);
        //Debug.Log("1212121212121212");
		if (message != null) _msgQueueStr.Enqueue(message);
	}

	/// <summary>  
	/// Called when the web socket closed  
	/// </summary>  
	void OnClosed(WebSocket ws, UInt16 code, string message)
	{
		Debug.Log(string.Format("-WebSocket closed! Code: {0} Message: {1}\n", code, message));
		_webSocket = null;
        //Debug.LogError("Close");
	}

	/// <summary>  
	/// Called when an error occured on client side  
	/// </summary>  
	void OnError(WebSocket ws, Exception ex)
	{
		string errorMsg = string.Empty;
		if (ws.InternalRequest.Response != null)
			errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);

		Debug.Log(string.Format("-An error occured: {0}\n",ex != null ? ex.Message : "Unknown Error " + errorMsg));
		_webSocket = null;
        //Debug.LogError("Eorror");
    }
}

//{"info":[{"area":11,"x":80,"y":50},{"area":5,"x":76,"y":48}]}
[System.Serializable]
public class DataInfo
{
	public Data[] info;
}

[System.Serializable]
public class Data
{
	public int area;
	public int x;
	public int y;
}