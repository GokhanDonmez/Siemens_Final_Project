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
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;

namespace Arduino_Frm
{
    public partial class Form1 : Form
    {
        private MyData myData = new MyData();
        public bool gonder; 
        public Form1()
        {
            InitializeComponent();
            cbPort.DataSource = SerialPort.GetPortNames();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        }
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string data = serialPort1.ReadLine(); // Seri porttan gelen veriyi satır satır okuyoruz. Veri gönderim şekli-> "xxx,yyy,zzz"
            string[] datas = data.Split(','); // Verileri virgüle göre ayırın

            if (datas.Length == 3)
            {
                
                Invoke(new Action(() => { tbFuel.Text = datas[0]; })); // İlk veriyi ilk TextBox'a yazdırıyoruz
                Invoke(new Action(() => { tbLat.Text = datas[1]; })); // İkinci veriyi ikinci TextBox'a yazdırıyoruz
                Invoke(new Action(() => { tbLon.Text = datas[2]; })); // Üçüncü veriyi üçüncü TextBox'a yazdırıyoruz
                myData.Fuel = datas[0];
                myData.Lat = datas[1];
                myData.Lon = datas[2];
                postData();

            }
        }

        private async void postData()
        {
            string apiUrl = "http://localhost:13440/api/Datas/CreateData";

            string json = JsonConvert.SerializeObject(myData);
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                SetLabelText(responseContent);
            }

        }

        private void SetLabelText(string text)
        {
            if (lblResponse.InvokeRequired)
            {
                lblResponse.Invoke(new Action(() => { lblResponse.Text = text; }));
            }
            else
            {
                lblResponse.Text = text;
            }
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = cbPort.Text;
            serialPort1.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            serialPort1.Open();
        }

        private void btnKes_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        
    }
}
