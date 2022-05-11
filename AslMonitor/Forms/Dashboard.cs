using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.DAL.Repositories;
using AslMonitor.DTOs;
using AslMonitor.Services;
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
        private static System.Timers.Timer aTimer;
        private readonly IUserStateService _userStateService;
        private readonly IGenericRepo<ScreenShot> _ssRepo;
        private readonly IGenericRepo<CLog> _clogRepo;

        public string? token { get; set; }
        public bool Connected { get; set; }
        public Dashboard(IUserStateService userStateService, IGenericRepo<ScreenShot> ssRepo, IGenericRepo<CLog> clogRepo)
        {
            InitializeComponent();
            _userStateService = userStateService;
            _ssRepo = ssRepo;
            _clogRepo = clogRepo;
            Connected = GlobalFunctions.CheckForInternetConnection();
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
            if (Connected) btnReload.BackColor = Color.Green;
            else btnReload.BackColor = Color.Red;

            //Loads the token from local database.
            using DatabaseContext _db = new DatabaseContext();
            LoginToken? loginToken = await _db.LoginTokens.FirstOrDefaultAsync();
            this.token = loginToken?.Token;

            //Extract informations about the current user from the token
            if (token == null)
            {
                return;
            }
            user = GlobalFunctions.GetCurrentUser(token);
            lblUserName.Text = user.UserName;
            if (Connected)
            { 
                await RefreshAsync(); 
            }
            else
            {
                if (await _userStateService.GetLastStateAsync(user.UserID) is null)
                {
                    UserState userState = new UserState()
                    {
                        UserID = user.UserID,
                        CurrentState = "Working",
                        TimeFrom = DateTime.Now,
                        Remarks = "Signed In"
                    };
                    await _userStateService.CreateUserStateAsync(userState);

                    if (userState is null) return;
                    lblCurrentStatus.Text = userState.CurrentState;
                    lblTimeFrom.Text = userState.TimeFrom.ToString();
                    lblTimeTo.Text = userState.TimeTo.ToString();
                    lblRemarks.Text = userState.Remarks;
                }
                else
                {
                    UserState userState = new UserState()
                    {
                        UserID = user.UserID,
                        CurrentState = "Working",
                        TimeFrom = DateTime.Now,
                        Remarks = "Signed In"
                    };
                    await _userStateService.ChangeUserStateAsync(userState);

                    if (userState is null) return;
                    lblCurrentStatus.Text = userState.CurrentState;
                    lblTimeFrom.Text = userState.TimeFrom.ToString();
                    lblTimeTo.Text = userState.TimeTo.ToString();
                    lblRemarks.Text = userState.Remarks;
                }
            }

            cmbCurrentStatus.Items.Clear();
            cmbCurrentStatus.Items.Add("Working");
            cmbCurrentStatus.Items.Add("Break");

            //MaterialForm formDailog = new Form1();
            //formDailog.ShowDialog();
        }

        ///<summary>
        ///Loads last user state from the server.
        ///</summary>
        private async Task RefreshAsync()
        {
            txtRemarks.Text = "";

            if (Connected)
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
            else
            {
                UserState state = await _userStateService.GetLastStateAsync(user.UserID);

                if (state is null) return;
                lblCurrentStatus.Text = state.CurrentState;
                lblTimeFrom.Text = state.TimeFrom.ToString();
                lblTimeTo.Text = state.TimeTo.ToString();
                lblRemarks.Text = state.Remarks;

                if (state.CurrentState.ToUpper() == "WORKING") switchWorkStatus.Checked = true;
                else switchWorkStatus.Checked = false;
            }
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
            e.Cancel = true;
        }

        ///<summary>
        ///Hits after form is shown
        ///</summary>
        private void Dashboard_Shown(object sender, EventArgs e)
        {
            SetTimer();
        }


        ///<summary>
        ///Capture the screenshot and save it in the drive.
        ///</summary>
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

                //Guid guid = Guid.NewGuid();
                string FolderPath =  GenerateFolders( GlobalFunctions.LocalImagePath);  // @"D:\SS\";
                string FileName = GenerateFileName();//guid.ToString() + ".jpg";
                string path = FolderPath + "\\" + FileName;
                //Saving the Image File (I am here Saving it in My drive).
                captureBitmap.Save(path, ImageFormat.Jpeg);

                //string token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjIsXCJVc2VySURcIjoxMDEwMSxcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJSYWhpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIkNPTVBBRE1JTlwiLFwiTG9naW5JRFwiOlwicmFoaW1AZ21haWwuY29tXCIsXCJMdHVkZVwiOlwiXCIsXCJVc2VyUENcIjpcIkRFU0tUT1AtMlRFMjFQRlwiLFwiSVBBZGRyZXNzXCI6XCJmZTgwOjpiNGVhOjNmYzE6YTdhMDpmZmRkJTE5XCJ9IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlMTA1MmM4ZS1hYTg2LTQwMGEtYTFjNi04ODE1MTg2Zjk5Y2EiLCJleHAiOjE2NDczNDM3NTgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCJ9.9a1ySP8o09C3lMBFL0zhkJypT2aZbJvK_cd7gSlhivc";

                //string token2 = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjMsXCJVc2VySURcIjoxMDEwMixcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJLYXJpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIlVTRVJcIixcIkxvZ2luSURcIjpcImthcmltQGdtYWlsLmNvbVwiLFwiTHR1ZGVcIjpcIlwiLFwiVXNlclBDXCI6XCJERVNLVE9QLTJURTIxUEZcIixcIklQQWRkcmVzc1wiOlwiZmU4MDo6YjRlYTozZmMxOmE3YTA6ZmZkZCUxOVwifSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNjNmODQ3NzMtZjM5Ny00MGRhLWI5ODUtMDFjNTIwMjhkOWI3IiwiZXhwIjoxNjQ3NDM0ODUxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAifQ.r2bdBkobquuZOdLUpB6dEZf07clrmm-M02AQJwXuxMo";


                //client.UploadFile()
                if (Connected)
                {
                    WebClient client = new WebClient();
                    client.Headers.Add("Authorization", $"Bearer {token}");
                    //client.UploadFile("https://localhost:7110/WeatherForecast/", path);
                    var response = client.UploadFile("https://localhost:7110/api/Files/", path);

                    File.Delete(path);
                }
                else
                {
                    ScreenShot ss = new ScreenShot()
                    {
                        UserID = user.UserID,
                        DirPath = FolderPath,
                        FileName = FileName,
                        InTime = DateTime.Now,
                        InUserID = user.UserID,
                        InUserPC = GlobalFunctions.UserPc(),
                        InIPAddress = GlobalFunctions.IpAddress()
                    };
                    ScreenShot? ssCreated = _ssRepo.Create(ss);
                    if (ssCreated == null)
                    {
                        string filePath = $"{FolderPath}\\{FileName}";
                        File.Delete(path);
                    }
                }
                captureBitmap.Dispose();

                //MessageBox.Show("Screen Captured");
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private string GenerateFolders(string rootPath)
        {
            string userID = user!.UserID.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string folderPath = $"{rootPath}\\{userID}\\{date}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        private string GenerateFileName()
        {
            string guid = Guid.NewGuid().ToString();
            return DateTime.Now.ToString("HH-mm-ss") + guid.Substring(0, 8) + ".jpg";
        }

        ///<summary>
        ///Start the timer on a fixed interval.
        ///</summary>
        private void SetTimer()
        {
            // Create a timer with a 30 second interval.
            aTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30).TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        ///<summary>
        ///Calls the method frequently after given time interval.
        ///</summary>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            CaptureMyScreen();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {

            await SubmitData();
        }


        ///<summary>
        ///Submit Updated state to the server.
        ///</summary>
        private async Task SubmitData()
        {
            if (cmbCurrentStatus.SelectedItem is null) return;
            UserState state = new UserState();
            state.CurrentState = cmbCurrentStatus.SelectedItem.ToString();
            state.Remarks = txtRemarks.Text;
            state.TimeFrom = DateTime.Now;
            state.UserID = user.UserID;

            if (Connected)
            {
                //string st = JsonConvert.SerializeObject(state);
                //string baseUri = "https://localhost:7110/";
                string baseUri = GlobalFunctions.BaseUri;
                using HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(baseUri);
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                using var response = await http.PostAsJsonAsync("api/auth/changeState", state);
                if (!response.IsSuccessStatusCode)
                {
                    //MaterialMessageBox.Show(text: "Something went wrong! Please try again.", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                    return;
                }
            }
            else
            {
                bool changed = await _userStateService.ChangeUserStateAsync(state);
                if (!changed) return;
            }

            //UserState? updatedState = await response.Content.ReadFromJsonAsync<UserState>();
            if (state.CurrentState?.ToUpper() != "WORKING")
            {
                aTimer.Stop();
                materialCard2.Hide();
                btnStart.Visible = true;
                cmbCurrentStatus.Enabled = false;
            }
            else
            {
                materialCard2.Show();
                btnStart.Visible = false;
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
                txtRemarks.Text = "Started working";
                cmbCurrentStatus.SelectedItem = "Working";
                await SubmitData();
                materialCard2.Show();
                materialCard2.Enabled = true;
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            if(GlobalFunctions.CheckForInternetConnection())
            {
                Connected = true;
                btnReload.BackColor = Color.Green;
                using DatabaseContext _db = new DatabaseContext();
                string baseUri = GlobalFunctions.BaseUri;
                using HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(baseUri);
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                if (await _db.UserStates.CountAsync() > 0)
                {
                    foreach (var state in _db.UserStates)
                    {
                        using var response = await http.PostAsJsonAsync("api/sync/updateState", state);
                    }
                }

                if (await _db.CLogs.CountAsync() > 0)
                {
                    foreach (var log in _db.CLogs)
                    {
                        log.ClogID = 0;
                        using var response = await http.PostAsJsonAsync("api/sync/addLogs", log);
                    }
                    await _db.Database.ExecuteSqlRawAsync("Delete From CLogs");
                }

                WebClient client = new WebClient();
                client.Headers.Add("Authorization", $"Bearer {token}");

                if (await _db.ScreenShots.CountAsync() > 0)
                {
                    foreach (var ss in _db.ScreenShots)
                    {
                        using var response = await http.PostAsJsonAsync("api/sync/addss", ss);
                        string path = ss.DirPath + "\\" + ss.FileName;

                        var response2 = client.UploadFile("https://localhost:7110/api/sync/files/", path);
                        File.Delete(path);
                    }
                    await _db.Database.ExecuteSqlRawAsync("Delete From ScreenShots");
                }

                MaterialMessageBox.Show("Reloaded");
            }
            else
            {
                Connected = false;
                btnReload.BackColor = Color.Red;
            }
        }
    }
}
