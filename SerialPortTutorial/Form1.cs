using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System;


namespace SerialPortTutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static SerialPort _serialPort;

        private void si_DataReceived(string data) { lbData.Text = data.Trim(); }

        private delegate void SetTextDeleg(string text);


        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadLine();
       
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           _serialPort = new SerialPort("COM16", 19200, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            _serialPort.Open();

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            string text = tbInput.Text;
            _serialPort.Write(text + "\r\n");
        }

    }
}
