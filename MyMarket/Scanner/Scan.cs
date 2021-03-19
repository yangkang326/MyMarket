using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MyMarket.Scanner
{
    public static class Scan
    {
        public static SerialPort ScanPort = new();
        public static string Receicecode = "";

        public static void GetSerialPort(string PortName)
        {
            var resultport = new SerialPort();
            resultport.PortName = "COM5";
            resultport.BaudRate = 9600;
            resultport.DataBits = 8;
            resultport.StopBits = StopBits.One;
            resultport.Parity = Parity.None;
            ScanPort = resultport;
        }

        public static void OpenPort()
        {
            try
            {
                ScanPort.Open();
                ScanPort.DataReceived += Decode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Decode(object sender, SerialDataReceivedEventArgs e)
        {
            var buffer = new List<byte>();
            var temp = new byte[1024];
            var length = ScanPort.Read(temp, 0, Math.Min(ScanPort.BytesToRead, temp.Length));
            var temp2 = new byte[length - 2];
            Array.Copy(temp, 0, temp2, 0, length - 2);
            buffer.AddRange(temp2);
            var temp3 = Encoding.ASCII.GetChars(temp2);
            Receicecode = new string(temp3);
            WeakReferenceMessenger.Default.Send(Receicecode, "DataCom");
        }
    }
}