using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.DTOs;
using AslMonitor.Utils;
using MaterialSkin.Controls;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AslMonitor.Forms
{
    public partial class Dashboard : MaterialForm
    {

        public string? token { get; set; }
        public Dashboard()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        private CurrentUser? user { get; set; }
        private async void Dashboard_Load(object sender, EventArgs e)
        {
            //string t = this.token;
            using DatabaseContext _db = new DatabaseContext();
            LoginToken? loginToken = await _db.LoginTokens.FirstOrDefaultAsync();
            this.token = loginToken.Token;


            user = GlobalFunctions.GetCurrentUser(token);
            lblUserName.Text = user.UserName;

            await RefreshAsync();

            cmbCurrentStatus.Items.Clear();
            cmbCurrentStatus.Items.Add("Working");
            cmbCurrentStatus.Items.Add("Break");

            //MaterialForm formDailog = new Form1();
            //formDailog.ShowDialog();
        }
        private async Task RefreshAsync()
        {
            string baseUri = "https://localhost:7110/";
            using HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(baseUri);
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using var response = await http.GetAsync("api/auth/lastState");
            if (!response.IsSuccessStatusCode) return;
            UserState? state = await response.Content.ReadFromJsonAsync<UserState>();
            if (state is null) return;
            lblCurrentStatus.Text = state.CurrentState;
            lblTimeFrom.Text = state.TimeFrom.ToString();
            lblTimeTo.Text = state.TimeTo.ToString();
            lblRemarks.Text = state.Remarks;

            if (state.CurrentState.ToUpper() == "WORKING") switchWorkStatus.Checked = true;
            else switchWorkStatus.Checked = false;
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
            e.Cancel = true;
        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {
            SetTimer();
        }


        private void CaptureMyScreen()
        {
            try
            {
                //Creating a new Bitmap object
                //Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(1366, 768, PixelFormat.Format32bppArgb);
                Bitmap captureBitmap = new Bitmap(1366, 768, PixelFormat.Format64bppPArgb);

                //Creating a Rectangle object which will capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                //Copying Image from The Screen
                //captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                captureGraphics.CopyFromScreen(0, 0, 0, 0, captureRectangle.Size);

                Guid guid = Guid.NewGuid();
                string FolderPath = @"D:\SS\";
                string FileName = guid.ToString() + ".jpg";
                string path = FolderPath + FileName;
                //Saving the Image File (I am here Saving it in My drive).
                captureBitmap.Save(path, ImageFormat.Jpeg);

                //string token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjIsXCJVc2VySURcIjoxMDEwMSxcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJSYWhpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIkNPTVBBRE1JTlwiLFwiTG9naW5JRFwiOlwicmFoaW1AZ21haWwuY29tXCIsXCJMdHVkZVwiOlwiXCIsXCJVc2VyUENcIjpcIkRFU0tUT1AtMlRFMjFQRlwiLFwiSVBBZGRyZXNzXCI6XCJmZTgwOjpiNGVhOjNmYzE6YTdhMDpmZmRkJTE5XCJ9IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlMTA1MmM4ZS1hYTg2LTQwMGEtYTFjNi04ODE1MTg2Zjk5Y2EiLCJleHAiOjE2NDczNDM3NTgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCJ9.9a1ySP8o09C3lMBFL0zhkJypT2aZbJvK_cd7gSlhivc";

                //string token2 = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjMsXCJVc2VySURcIjoxMDEwMixcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJLYXJpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIlVTRVJcIixcIkxvZ2luSURcIjpcImthcmltQGdtYWlsLmNvbVwiLFwiTHR1ZGVcIjpcIlwiLFwiVXNlclBDXCI6XCJERVNLVE9QLTJURTIxUEZcIixcIklQQWRkcmVzc1wiOlwiZmU4MDo6YjRlYTozZmMxOmE3YTA6ZmZkZCUxOVwifSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNjNmODQ3NzMtZjM5Ny00MGRhLWI5ODUtMDFjNTIwMjhkOWI3IiwiZXhwIjoxNjQ3NDM0ODUxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAifQ.r2bdBkobquuZOdLUpB6dEZf07clrmm-M02AQJwXuxMo";

                WebClient client = new WebClient();
                client.Headers.Add("Authorization", $"Bearer {token}");
                //client.UploadFile("https://localhost:7110/WeatherForecast/", path);
                client.UploadFile("https://localhost:7110/api/Files/", path);
                //client.UploadFile()

                captureBitmap.Dispose();

                //MessageBox.Show("Screen Captured");
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private static System.Timers.Timer aTimer;
        private readonly DatabaseContext _db;

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30).TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            CaptureMyScreen();
            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
            //                  e.SignalTime);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {

            await SubmitData();
        }

        private async Task SubmitData()
        {
            if (cmbCurrentStatus.SelectedItem is null) return;
            UserState state = new UserState();
            state.CurrentState = cmbCurrentStatus.SelectedItem.ToString();
            state.Remarks = txtRemarks.Text;
            state.TimeFrom = DateTime.Now;
            state.UserID = user.UserID;

            string st = JsonConvert.SerializeObject(state);
            string baseUri = "https://localhost:7110/";
            using HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(baseUri);
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using var response = await http.PostAsJsonAsync("api/auth/changeState", state);
            if (!response.IsSuccessStatusCode)
            {
                //MaterialMessageBox.Show(text: "Something went wrong! Please try again.", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }
            //UserState? updatedState = await response.Content.ReadFromJsonAsync<UserState>();
            if (state.CurrentState?.ToUpper() != "WORKING")
            {
                aTimer.Stop();
                panel1.Hide();
                cmbCurrentStatus.Enabled = false;
            }
            else
            {
                panel1.Show();
                cmbCurrentStatus.Enabled = true;
                aTimer.Start();
            }

            await RefreshAsync();
            MaterialMessageBox.Show(text: "State Changed Successfully", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
        }

        private async void switchWorkStatus_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbCurrentStatus.SelectedItem?.ToString() != "Working")
            {
                cmbCurrentStatus.SelectedItem = "Working";
                await SubmitData();
                panel1.Show();
                panel1.Enabled = true;
            }
        }
    }
}
