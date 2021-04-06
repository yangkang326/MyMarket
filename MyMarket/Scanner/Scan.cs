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

        public static void GetSerialPort(string portName)
        {
            var Resultport = new SerialPort();
            Resultport.PortName = "COM5";
            Resultport.BaudRate = 9600;
            Resultport.DataBits = 8;
            Resultport.StopBits = StopBits.One;
            Resultport.Parity = Parity.None;
            ScanPort = Resultport;
        }

        public static void OpenPort()
        {
            try
            {
                ScanPort.Open();
                ScanPort.DataReceived += Decode;
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
            }
        }

        public static void Decode(object sender, SerialDataReceivedEventArgs e)
        {
            var Buffer = new List<byte>();
            var Temp = new byte[1024];
            var Length = ScanPort.Read(Temp, 0, Math.Min(ScanPort.BytesToRead, Temp.Length));
            var Temp2 = new byte[Length - 2];
            Array.Copy(Temp, 0, Temp2, 0, Length - 2);
            Buffer.AddRange(Temp2);
            var Temp3 = Encoding.ASCII.GetChars(Temp2);
            Receicecode = new string(Temp3);
            WeakReferenceMessenger.Default.Send(Receicecode, "DataCom");
        }
    }
}