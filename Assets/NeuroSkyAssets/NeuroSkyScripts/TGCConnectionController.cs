using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public class TGCConnectionController : MonoBehaviour
{
    private TcpClient client;
    private Stream stream;
    private byte[] buffer;

    public delegate void UpdateIntValueDelegate(int value);
    public delegate void UpdateFloatValueDelegate(float value);

    public event UpdateIntValueDelegate UpdatePoorSignalEvent;
    public event UpdateIntValueDelegate UpdateAttentionEvent;
    public event UpdateIntValueDelegate UpdateMeditationEvent;

    void Start()
    {
        Connect();
    }

    public void Disconnect()
    {
        if (IsInvoking("ParseData"))
        {
            CancelInvoke("ParseData");
            stream.Close();
        }
    }

    public void Connect()
    {
        if (!IsInvoking("ParseData"))
        {

            client = new TcpClient("127.0.0.1", 13854);
            stream = client.GetStream();
            buffer = new byte[1024];
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true, ""format"": ""Json""}");
            stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

            InvokeRepeating("ParseData", 0.1f, 0.05f);
        }
    }

    void ParseData()
    {
        if (stream.CanRead)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string[] packets = Encoding.ASCII.GetString(buffer, 0, bytesRead).Split('\r');

                packets = packets.Where(x => x.Contains("eSense")).ToArray();

                if (packets.Any())
                {
                    foreach (string packet in packets)
                    {
                        if (packet.Length == 0)
                            continue;

                        IDictionary primary = (IDictionary)JsonConvert.DeserializeObject(packet, typeof(IDictionary));

                        if (primary.Contains("poorSignalLevel"))
                        {

                            if (UpdatePoorSignalEvent != null)
                            {
                                UpdatePoorSignalEvent(int.Parse(primary["poorSignalLevel"].ToString()));
                            }
                        }

                        if (primary.Contains("eSense"))
                        {
                            JContainer eSense = (JContainer)primary["eSense"];

                            var container = eSense.ToObject<Dictionary<string, int>>();

                            if (UpdateAttentionEvent != null)
                            {
                                UpdateAttentionEvent(container["attention"]);
                            }
                            if (UpdateMeditationEvent != null)
                            {
                                UpdateMeditationEvent(container["meditation"]);
                            }
                        }
                    }
                }
            }
            catch (IOException e) { Debug.Log("IOException " + e); }
            catch (System.Exception e) { Debug.Log("Exception " + e); }
        }

    }// end ParseData

    void OnApplicationQuit()
    {
        Disconnect();
    }


}
