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
using Timer = System.Windows.Forms.Timer;

namespace AslMonitor.Forms
{
    public partial class Dashboard : MaterialForm
    {
        private static System.Timers.Timer aTimer;
        private readonly IUserStateService _userStateService;
        private readonly IGenericRepo<ScreenShot> _ssRepo;
        private readonly IGenericRepo<CLog> _clogRepo;

        public string? token { get; set; }
        //public bool Connected { get; set; }

        public bool forceClose { get; set; }


        //private bool previousConnectionStatus { get; set; }
        private bool offlineDataExists
        {
            get
            {
                using var db = new DatabaseContext();
                return (db.CLogs.Any() || db.ScreenShots.Any());
            }
        }
        /// <summary>
        /// Checks internet connection status, sets reload buttons color, synchronize offline data with server.
        /// </summary>
        //public bool Connected
        //{
        //    get
        //    {
        //        bool isOnline = GlobalFunctions.CheckForInternetConnection();
        //        ChangeReloadButtonColor(isOnline);
        //        if (isOnline && offlineDataExists)
        //        {
        //            SynchronizeOfflineDataWithServerAsync();
        //        }
        //        //previousConnectionStatus = isOnline;
        //        return isOnline;
        //    }
        //}

        public async Task<bool> ConnectedAsync()
        {
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            ChangeReloadButtonColor(isOnline);
            if (isOnline && offlineDataExists)
            {
                await SynchronizeOfflineDataWithServerAsync();

                //await ReloadGridPreviousStatesAsync();
            }
            //previousConnectionStatus = isOnline;
            return isOnline;
        }

        /// <summary>
        /// Constructor method.
        /// gets the dependencies
        /// Sets its connected property based on internet connection.
        /// </summary>
        /// <param name="userStateService"></param>
        /// <param name="ssRepo"></param>
        /// <param name="clogRepo"></param>
        public Dashboard(IUserStateService userStateService, IGenericRepo<ScreenShot> ssRepo, IGenericRepo<CLog> clogRepo)
        {
            InitializeComponent();
            _userStateService = userStateService;
            _ssRepo = ssRepo;
            _clogRepo = clogRepo;
            //Connected = GlobalFunctions.CheckForInternetConnection();
        }

        /// <summary>
        /// Hit when tray icon clicked.
        /// Shows the form and hide the tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.IsDisposed)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
            else notifyIcon1.Visible = false;
            //catch (Exception ex)
            //{
            //    notifyIcon1.Visible = false;
            //}
        }
        /// <summary>
        /// Represent the current user
        /// </summary>
        private CurrentUser? user { get; set; }

        /// <summary>
        /// Hits when the form loads.
        /// Sets reload button's color based on internet connection.
        /// Loads the token from the database and sets the form's token property.
        /// Load current User information and sets the forms's user property.
        /// If connected then loads last state of the user from the server.
        /// If offline and no user state exist then create a userState and saves it to database and Update the UI with the inserted user state.
        /// if offline and userState exist in the local database, Updates the user state into working state and Update the UI with the updated user state.
        /// Clear combobox and add items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Dashboard_Load(object sender, EventArgs e)
        {
            forceClose = false;
            //bool isOnline = Connected;

            //Loads the token from local database.
            using DatabaseContext _db = new DatabaseContext();
            LoginToken? loginToken = await _db.LoginTokens.FirstOrDefaultAsync();
            this.token = loginToken?.Token;

            //Extract informations about the current user from the token
            if (token == null)
            {
                //notifyIcon1.Visible = false;
                //forceClose = true;
                //this.Close();
                return;
            }
            user = GlobalFunctions.GetCurrentUser(token);
            lblUserName.Text = user.UserName;

            //Sync Offline Data first with server
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            ChangeReloadButtonColor(isOnline);
            if (isOnline && offlineDataExists)
            {
                await SynchronizeOfflineDataWithServerAsync();
            }
            //previousConnectionStatus = isOnline;


            await RefreshAsync();

            ////ChangeReloadButtonColor(isOnline);
            //if (isOnline)
            //{
            //    //If Online then loads last state of the user from the server
            //    await RefreshAsync();
            //}
            //else
            //{
            //    //If offline
            //    if (await _userStateService.GetLastStateAsync(user.UserID) is null)
            //    {
            //        //If offline and no user state exist then create a userState and saves it to database
            //        UserState userState = new UserState()
            //        {
            //            UserID = user.UserID,
            //            CurrentState = "Working",
            //            TimeFrom = DateTime.Now,
            //            Remarks = "Signed In"
            //        };
            //        switchWorkStatus.Checked = true;
            //        await _userStateService.CreateUserStateAsync(userState);

            //        //Update the UI with the inserted user state
            //        UpdateUI(userState);
            //    }
            //    else
            //    {
            //        //if offline and userState exist in the local database

            //        //Updates the user state into working state.
            //        UserState userState = new UserState()
            //        {
            //            UserID = user.UserID,
            //            CurrentState = "Working",
            //            TimeFrom = DateTime.Now,
            //            Remarks = "Started Working offline."
            //        };
            //        switchWorkStatus.Checked = true;
            //        await _userStateService.ChangeUserStateAsync(userState);

            //        //Update the UI with the updated user state
            //        UpdateUI(userState);
            //    }
            //}

            ReloadComboBoxStatus();

            DeletePrevDirectories();
        }


        public async Task loadFirstTime()
        {
            user = GlobalFunctions.GetCurrentUser(token);
            lblUserName.Text = user.UserName;

            //Sync Offline Data first with server
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            ChangeReloadButtonColor(isOnline);
            if (isOnline && offlineDataExists)
            {
                await SynchronizeOfflineDataWithServerAsync();
            }
            //previousConnectionStatus = isOnline;


            await RefreshAsync();

            ReloadComboBoxStatus();

            DeletePrevDirectories();
        }

        private void DeletePrevDirectories()
        {
            if (!Directory.Exists(GlobalFunctions.LocalImagePath))
            {
                return;
            }
            var Directories = Directory.GetDirectories(GlobalFunctions.LocalImagePath);

            foreach (string directory in Directories)
            {
                var subDirs = Directory.GetDirectories(directory);

                foreach (var subDir in subDirs)
                {
                    if (subDir != directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        var files = Directory.GetFiles(subDir);
                        if (files.Length == 0)
                            Directory.Delete(subDir);
                    }
                }
            }
        }

        /// <summary>
        /// Clear combobox and add different items based on current status.
        /// </summary>
        private void ReloadComboBoxStatus()
        {
            cmbCurrentStatus.Items.Clear();

            if (!switchWorkStatus.Checked)
                cmbCurrentStatus.Items.Add("Working");
            else
            {
                cmbCurrentStatus.Items.Add("Prayer Break");
                cmbCurrentStatus.Items.Add("Snacks Break");
                cmbCurrentStatus.Items.Add("Personal Activity Break");
                cmbCurrentStatus.Items.Add("Others Break");
            }
        }

        /// <summary>
        /// Updates User Interface
        /// </summary>
        /// <param name="state"></param>
        private void UpdateUI(UserState? state)
        {
            if (state is null) return;
            lblCurrentStatus.Text = state.CurrentState;
            lblTimeFrom.Text = state.TimeFrom.ToString();
            //lblTimeTo.Text = state.TimeTo.ToString();
            lblRemarks.Text = state.Remarks;

            ShowHideGui(state.CurrentState);
        }
        ///<summary>
        ///Loads last user state...
        ///if online, then get last state from the server and update UI state...
        ///if offline, then get last state from the local database and update UI state
        ///</summary>
        private async Task RefreshAsync()
        {
            bool isOnline = await ConnectedAsync();

            txtRemarks.Text = "";

            if (isOnline)
            {
                // if online, then get last state from the server and update UI state
                //string baseUri = "https://localhost:7110/";
                string baseUri = GlobalFunctions.BaseUri;
                using HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(baseUri);
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                using var response = await http.GetAsync("api/auth/lastState");
                if (!response.IsSuccessStatusCode) return;
                UserState? state = await response.Content.ReadFromJsonAsync<UserState>();

                //Added on 9/7/22
                if (state?.TimeFrom?.Date < DateTime.Now.Date)
                {
                    state.TimeTo = state.TimeFrom?.Date.AddDays(1).AddMinutes(-1);
                    //if online then the new userState will be pushed to server
                    using var response2 = await http.PostAsJsonAsync("api/auth/changeState", state);
                    if (!response2.IsSuccessStatusCode)
                    {
                        return;
                    }


                    state.TimeFrom = DateTime.Now;
                    state.TimeTo = null;
                    //if online then the new userState will be pushed to server
                    using var response3 = await http.PostAsJsonAsync("api/auth/changeState", state);
                    if (!response3.IsSuccessStatusCode)
                    {
                        return;
                    }

                    //Also update state in local database
                    bool changed = await _userStateService.ChangeUserStateWithoutLogAsync(state);
                    //bool a = true;

                }
                UpdateUI(state);

                if (state.CurrentState.ToUpper() == "WORKING") switchWorkStatus.Checked = true;
                else switchWorkStatus.Checked = false;


                //For local data
                if (await _userStateService.GetLastStateAsync(user.UserID) is null)
                {
                    UserState userState = new UserState()
                    {
                        UserID = user.UserID,
                        CurrentState = "Working",
                        TimeFrom = DateTime.Now,
                        Remarks = "Signed In"
                    };
                    switchWorkStatus.Checked = true;
                    await _userStateService.CreateUserStateAsync(userState);
                }

            }
            else
            {
                //If offline
                if (await _userStateService.GetLastStateAsync(user.UserID) is null)
                {
                    //If offline and no user state exist then create a userState and saves it to database
                    UserState userState = new UserState()
                    {
                        UserID = user.UserID,
                        CurrentState = "Working",
                        TimeFrom = DateTime.Now,
                        Remarks = "Signed In"
                    };
                    switchWorkStatus.Checked = true;
                    await _userStateService.CreateUserStateAsync(userState);

                    //Update the UI with the inserted user state
                    UpdateUI(userState);
                }
                else
                {
                    //if offline and userState exist in the local database

                    ////Updates the user state into working state.
                    //UserState userState = new UserState()
                    //{
                    //    UserID = user.UserID,
                    //    CurrentState = "Working",
                    //    TimeFrom = DateTime.Now,
                    //    Remarks = "Started Working offline."
                    //};
                    //switchWorkStatus.Checked = true;
                    //await _userStateService.ChangeUserStateAsync(userState);


                    ////Update the UI with the updated user state
                    //UpdateUI(userState);



                    // if offline and userstate exists, then get last state from the local database and update UI state
                    UserState state = await _userStateService.GetLastStateAsync(user.UserID);

                    //Added on 9/7/22
                    if (state?.TimeFrom?.Date < DateTime.Now.Date)
                    {
                        state.TimeTo = state.TimeFrom?.Date.AddDays(1).AddSeconds(-1);

                        //Also update state in local database
                        bool changed = await _userStateService.ChangeUserStateWithoutLogAsync(state);
                        bool a = true;

                    }

                    UpdateUI(state);


                    if (state.CurrentState.ToUpper() == "WORKING") switchWorkStatus.Checked = true;
                    else switchWorkStatus.Checked = false;
                }
            }

            await ReloadGridPreviousStatesAsync();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                Hide();
                notifyIcon1.Visible = true;
                e.Cancel = true;
            }
            else
            {
                notifyIcon1.Visible = false;
            }
        }

        ///<summary>
        ///Hits after form is shown
        ///</summary>
        private void Dashboard_Shown(object sender, EventArgs e)
        {
            SetTimer();
        }
        ///<summary>
        ///Capture the screenshot and send it to server or save it locally.
        ///</summary>
        private async Task CaptureMyScreen()
        {
            bool isOnline = await ConnectedAsync();
            ChangeReloadButtonColor(isOnline);
            try
            {
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;


                //Creating a new Bitmap object
                //Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(1366, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(1366, 768, PixelFormat.Format64bppPArgb);
                Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format64bppPArgb);

                //Creating a Rectangle object which will capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                //Copying Image from The Screen
                //captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                captureGraphics.CopyFromScreen(0, 0, 0, 0, captureRectangle.Size);

                //Guid guid = Guid.NewGuid();
                string FolderPath = GenerateFolders(GlobalFunctions.LocalImagePath);  // @"D:\SS\";
                string FileName = GenerateFileName();//guid.ToString() + ".jpg";
                string path = FolderPath + "\\" + FileName;
                //Saving the Image File (I am here Saving it in My drive).
                captureBitmap.Save(path, ImageFormat.Jpeg);

                #region commented code
                //string token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjIsXCJVc2VySURcIjoxMDEwMSxcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJSYWhpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIkNPTVBBRE1JTlwiLFwiTG9naW5JRFwiOlwicmFoaW1AZ21haWwuY29tXCIsXCJMdHVkZVwiOlwiXCIsXCJVc2VyUENcIjpcIkRFU0tUT1AtMlRFMjFQRlwiLFwiSVBBZGRyZXNzXCI6XCJmZTgwOjpiNGVhOjNmYzE6YTdhMDpmZmRkJTE5XCJ9IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlMTA1MmM4ZS1hYTg2LTQwMGEtYTFjNi04ODE1MTg2Zjk5Y2EiLCJleHAiOjE2NDczNDM3NTgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzExMCJ9.9a1ySP8o09C3lMBFL0zhkJypT2aZbJvK_cd7gSlhivc";

                //string token2 = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlckluZm9JRFwiOjMsXCJVc2VySURcIjoxMDEwMixcIkJyYW5jaENEXCI6bnVsbCxcIlVzZXJOYW1lXCI6XCJLYXJpbSBVZGRpblwiLFwiVXNlclR5cGVcIjpcIlVTRVJcIixcIkxvZ2luSURcIjpcImthcmltQGdtYWlsLmNvbVwiLFwiTHR1ZGVcIjpcIlwiLFwiVXNlclBDXCI6XCJERVNLVE9QLTJURTIxUEZcIixcIklQQWRkcmVzc1wiOlwiZmU4MDo6YjRlYTozZmMxOmE3YTA6ZmZkZCUxOVwifSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNjNmODQ3NzMtZjM5Ny00MGRhLWI5ODUtMDFjNTIwMjhkOWI3IiwiZXhwIjoxNjQ3NDM0ODUxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjcxMTAifQ.r2bdBkobquuZOdLUpB6dEZf07clrmm-M02AQJwXuxMo";
                #endregion

                if (isOnline)
                {
                    //if online then the file is uploaded to the server and then delete it.
                    WebClient client = new WebClient();
                    client.Headers.Add("Authorization", $"Bearer {token}");
                    string uri = GlobalFunctions.BaseUri + "api/Files/";
                    //var response = client.UploadFile("https://localhost:7110/api/Files/", path);
                    var response = client.UploadFile(uri, path);

                    File.Delete(path);
                }
                else
                {
                    //To rename file extenstion to dll and hide it
                    string oldNameFullPath = $"{FolderPath}\\{FileName}";
                    string newNameFullPath = oldNameFullPath.Substring(0, oldNameFullPath.Count() - 4) + ".dll";
                    File.Move(oldNameFullPath, newNameFullPath);
                    //File.SetAttributes(newNameFullPath, FileAttributes.Hidden);

                    //if offline then create a ss object and save it in the local database
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
                        //File.Delete(path);
                        File.Delete(newNameFullPath);
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
            aTimer = new System.Timers.Timer(TimeSpan.FromSeconds(GlobalFunctions.SsIntervalInSeconds).TotalMilliseconds);
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += OnTimedEvent;
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        ///<summary>
        ///Capture screen frequently after given time interval.
        ///</summary>
        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (token is not null)
                await CaptureMyScreen();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {

            await SubmitData();

            ReloadComboBoxStatus();
        }


        ///<summary>
        ///Submit Updated state to the server.
        ///</summary>
        private async Task SubmitData()
        {
            bool isOnline = await ConnectedAsync();

            if (cmbCurrentStatus.SelectedItem is null) return;
            UserState state = new UserState();
            state.CurrentState = cmbCurrentStatus.SelectedItem.ToString();
            state.Remarks = txtRemarks.Text;
            state.TimeFrom = DateTime.Now;
            state.UserID = user.UserID;

            if (isOnline)
            {
                //if online then the new userState will be pushed to server
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

                //Also update state in local database
                bool changed = await _userStateService.ChangeUserStateWithoutLogAsync(state);
                bool a = true;
            }
            else
            {
                //if offline then new userstate will be saved locally.
                bool changed = await _userStateService.ChangeUserStateAsync(state);
                if (!changed) return;
            }

            ShowHideGui(state.CurrentState);
            StartStopTimer(state.CurrentState);


            await RefreshAsync();
            MaterialMessageBox.Show(text: "State Changed Successfully", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
        }

        private void ShowHideGui(string? CurrentState)
        {
            if (CurrentState?.ToUpper() != "WORKING")
            {
                //if user is not in working state then timer will stop and part of ui will be hidden
                materialCard2.Hide();
                btnStart.Visible = true;
                cmbCurrentStatus.Enabled = false;
            }
            else
            {
                //if user is in working state then timer will start and hidden part of the ui will be visible
                materialCard2.Show();
                btnStart.Visible = false;
                cmbCurrentStatus.Enabled = true;
            }
        }

        private void StartStopTimer(string? CurrentState)
        {
            if (CurrentState?.ToUpper() != "WORKING")
            {
                //if user is not in working state then timer will stop 
                aTimer.Stop();
            }
            else
            {
                //if user is in working state then timer will start 
                aTimer.Start();
            }
        }
        private async void switchWorkStatus_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// if user isnt in working state then set userstate in working state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            ReloadComboBoxStatus();
        }

        /// <summary>
        /// Changes reload buttons text and color based on internet connection status.
        /// </summary>
        /// <param name="online"></param>
        private void ChangeReloadButtonColor(bool online)
        {
            try
            {
                if (online)
                {
                    btnReload.Text = "Connected";
                    btnReload.ForeColor = Color.Black;
                    btnReload.BackColor = Color.Green;
                }
                else
                {
                    btnReload.Text = "Disconnected";
                    btnReload.ForeColor = Color.Black;
                    btnReload.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }
        }

        /// <summary>
        /// if user is online then the offline data will be synced on server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnReload_Click(object sender, EventArgs e)
        {
            //bool isOnline = Connected;
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            ChangeReloadButtonColor(isOnline);
            if (isOnline)
            {
                //if Online 

                await SynchronizeOfflineDataWithServerAsync();

                await ReloadGridPreviousStatesAsync();

                MaterialMessageBox.Show("Reloaded");
            }
        }


        /// <summary>
        /// Reload previous states in the grid.
        /// If online then reload data from server, 
        /// If offline then reload data from local database.
        /// </summary>
        /// <returns></returns>
        private async Task ReloadGridPreviousStatesAsync()
        {
            //bool isOnline = await ConnectedAsync();
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            ChangeReloadButtonColor(isOnline);
            if (isOnline && offlineDataExists)
            {
                await SynchronizeOfflineDataWithServerAsync();
            };


            if (isOnline)
            {
                string baseUri = GlobalFunctions.BaseUri;
                using HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(baseUri);
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                using var response = await http.GetAsync("api/sync/getPreviousStates");

                List<UserState>? userStates = await response.Content.ReadFromJsonAsync<List<UserState>>();

                int count = 1;
                gridPreviousStates.DataSource = (from us in userStates
                                                 orderby us.TimeTo descending
                                                 select new { sl = count++, us.CurrentState, us.TimeFrom, us.TimeTo, us.Remarks }).ToList();
            }
            else
            {
                int UserID = user.UserID;
                using DatabaseContext _db = new DatabaseContext();
                List<CLog> logs = await _db.CLogs.Where(l => l.UserID == UserID
                                                            && ((DateTime)l.LogTime!).Date == DateTime.Now.Date
                                                            && l.TableName == "UserStateS").OrderByDescending(l => l.LogTime).ToListAsync();
                List<UserState> userStates = new List<UserState>();
                foreach (CLog log in logs)
                {
                    UserState? state = JsonConvert.DeserializeObject<UserState>(log.LogData);
                    if (state is not null)
                        userStates.Add(state);
                }
                int count = 1;
                gridPreviousStates.DataSource = (from us in userStates
                                                 orderby us.TimeTo descending
                                                 select new { sl = count++, us.CurrentState, us.TimeFrom, us.TimeTo, us.Remarks }).ToList();
            }
        }

        /// <summary>
        /// Uploads Offline Data to server
        /// </summary>
        /// <returns></returns>
        private async Task SynchronizeOfflineDataWithServerAsync()
        {
            try
            {
                //Sync Current state to server.
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

                //Sync offline logs to server.
                if (await _db.CLogs.CountAsync() > 0)
                {
                    foreach (var log in _db.CLogs)
                    {
                        log.ClogID = 0;
                        using var response = await http.PostAsJsonAsync("api/sync/addLogs", log);
                    }
                    await _db.Database.ExecuteSqlRawAsync("Delete From CLogs");
                }

                //sync screenshot objects and files on server.
                WebClient client = new WebClient();
                client.Headers.Add("Authorization", $"Bearer {token}");
                if (await _db.ScreenShots.CountAsync() > 0)
                {
                    foreach (var ss in _db.ScreenShots)
                    {
                        try
                        {
                            using var response = await http.PostAsJsonAsync("api/sync/addss", ss);

                            string path = ss.DirPath + "\\" + ss.FileName;


                            //To rename file extenstion to dll and hide it
                            string newNameFullPath = path;//$"{FolderPath}\\{FileName}";
                            string oldNameFullPath = newNameFullPath.Substring(0, newNameFullPath.Count() - 4) + ".dll";
                            File.Move(oldNameFullPath, newNameFullPath);
                            //File.SetAttributes(newNameFullPath, FileAttributes.Hidden);



                            var response2 = client.UploadFile($"{baseUri}api/sync/files/", newNameFullPath);// path);
                            File.Delete(newNameFullPath);

                            //string s = client.Encoding.GetString(response2);
                            //FileInformation fileDetails = JsonConvert.DeserializeObject<FileInformation>(s);
                            //string dir = fileDetails.dir;
                            //ss.DirPath = dir;

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    await _db.Database.ExecuteSqlRawAsync("Delete From ScreenShots");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
