using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DeviceSubApp
{
    public partial class FrmMain : Form
    {
        MqttClient client;
        string connectionString;                            // DB 연결 문자열 : MQTT broker address
        ulong lineCount;                                    // 리치텍스트박스에 라인넘버 만들기 !
        delegate void updateTextCallback(string message);   // 스레드상에서 윈폼 리치텍스트박스에 출력 필요

        Stopwatch sw = new Stopwatch();   // 스탑왓치 추가 ! (using diagnostics)

        public FrmMain()
        {
            InitializeComponent();
            InitializeAllData();
        }

        private void InitializeAllData()
        {
            connectionString = "Data Source=210.119.12.86;" +
                "Initial Catalog=MRP;Persist Security Info=True;User ID=sa;password=mssql_p@ssw0rd!";
            lineCount = 0;
            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;
            IPAddress brokerAddress;

            try
            {
                brokerAddress = IPAddress.Parse(TxtConnectionString.Text);
                client = new MqttClient(brokerAddress);
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // 타이머 선언
            Timer.Enabled = true;
            Timer.Interval = 1000;  // 1000m/s -> 1초
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        // 타이머 메서드
        private void Timer_Tick(object sender, EventArgs e)
        {
            LblResult.Text = sw.Elapsed.Seconds.ToString();
            if (sw.Elapsed.Seconds >= 2) // 데이터를 받은지 2초가 경과하면,,,,
            {
                // 스탑워치 종료, 리셋 후 데이터 읽기 작업 시작 !
                sw.Stop();
                sw.Reset();

                // 데이터 처리 프로세스 
                PrcCorrentDataToDB(); // --> 라즈베리 데이터를 DB에 저장하는 메서드
                // ClearData(); --> 전역 list를 초기화하는 메서드
            }
        }

        // 여러 데이터 중 최종 데이터만 DB에 입력하는 메서드
        private void PrcCorrentDataToDB()
        {
            if (iotData.Count > 0)
            {
                var correctData = iotData[iotData.Count - 1];
                // correctData를 DB에 입력 !!
                using (var conn = new SqlConnection(connectionString)) // using을 사용하면 close()안해도됨!!
                {
                    var prcResult = correctData["PRC_MSG"] == "OK" ? 1 : 0; // OK면 1저장, FAIL이면 0저장
                    // DB에서 마지막 값을 계속 수정하는 쿼리문 
                    string strUpQry = $"UPDATE Process" +
                                        $" SET PrcResult = '{prcResult}'" +
                                         $"  , ModDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                                         $"  , ModID = '{"SYS"}'" +
                                     $"  WHERE PrcIdx = " +
                                     $"  (SELECT TOP 1 PrcIdx FROM Process ORDER BY PrcIdx DESC)";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(strUpQry, conn);
                        if(cmd.ExecuteNonQuery() == 1)
                        {
                            UpdateText("[DB] 센싱값 업데이트 성공 !");
                        }
                        else
                        {
                            UpdateText("[DB] 센싱값 업데이트 실패 !");
                        }
                    }
                    catch (Exception ex)
                    {
                        UpdateText($">>>>> DB 업데이트 ERROR!! : {ex.Message}");
                    }
                }
            }
            iotData.Clear(); // DB 저장후 iotData에 있는 데이터 모두 삭제
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Message);
                UpdateText($">>>>> 받은 메시지 : {message}");
                // message(json메시지) --> C#(메시지로 변환)
                var currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                PrcInputDataToList(currentData); // 데이터를 List에 저장하는 메서드


                // 스탑워치 실행!!(메시지가 올때마다 리셋하고 다시 실행)
                sw.Stop();
                sw.Reset();
                sw.Start();
            }
            catch (Exception ex)
            {
                UpdateText($">>>>> ERROR!! : {ex.Message}");
            }
        }

        // iotData라는 전역 변수(리스트) 선언 --> OK 또는 FAIL만 저장!!
        List<Dictionary<string, string>> iotData = new List<Dictionary<string, string>>();

        // 라즈베리에서 들어온 데이터를 전역 List에 저장하는 메서드
        private void PrcInputDataToList(Dictionary<string, string> currentData)
        {
            if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
                iotData.Add(currentData);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(TxtClientID.Text);   // SUBSCR01 커넥트
            UpdateText(">>>>> Client Connected!");
            client.Subscribe(new string[] { TxtSubscriptionTopic.Text }, // topic 설정
                new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE }); // QOS 사용 : 0번 값
            UpdateText(">>>>> Subscribing to : " + TxtSubscriptionTopic.Text);

            BtnConnect.Enabled = false;
            BtnDisconnect.Enabled = true;
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            UpdateText(">>>>> Client Disconnected!");

            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;
        }

        // message 출력하는 함수 따로 구현!!
        private void UpdateText(string message)
        {
            if (RtbSubscr.InvokeRequired)
            {
                updateTextCallback callback = new updateTextCallback(UpdateText);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                lineCount++;
                RtbSubscr.AppendText($"{lineCount} : {message}\n");
                RtbSubscr.ScrollToCaret();
            }
        }
    }
}
