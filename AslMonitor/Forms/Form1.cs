using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.DTOs;
using AslMonitor.Forms;
using AslMonitor.Utils;
using MaterialSkin.Controls;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Timers;

namespace AslMonitor
{
    public partial class Form1 : MaterialForm
    {
        private readonly Dashboard _dashboard;
        private readonly SignUp _signUp;

        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}

        //public HomeController(IServiceProvider serviceProvider)
        //{
        //    var service = serviceProvider.GetService<ITestService>();
        //    int rnd = service.GenerateRandom();
        //}

        public Form1(Dashboard dashboard, SignUp signUp)
        {
            _dashboard = dashboard;
            _signUp = signUp;

            InitializeComponent();
        }


        /// <summary>
        /// Checks if internet connection exists
        /// and show connection status by messagebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            bool connected = GlobalFunctions.CheckForInternetConnection();
            if (connected) MaterialMessageBox.Show("Connected");
            else MaterialMessageBox.Show("Not Connected");

        }

        /// <summary>
        /// Hit when form is minimizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;

                //Dashboard formD = new Dashboard();
                //formD.Show();
            }
        }

        /// <summary>
        /// Hit when tray icon is double clicked.
        /// Form will Show and tray icon will hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// Hit when form close button is clicked.
        /// Form hides instead of closing. tray icon gets visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
            e.Cancel = true;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hit when form is shown
        /// Fetch the token from the Local Database.
        /// If token is found then form1 is closed and dashboard form will show in the same location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Form1_Shown(object sender, EventArgs e)
        {
            using DatabaseContext _db = new DatabaseContext();
            LoginToken? loginToken = await _db.LoginTokens.FirstOrDefaultAsync();
            if (loginToken is not null)
            {

                //Dashboard _dashboard = new Dashboard();
                _dashboard.token = loginToken.Token;
                _dashboard.Location = this.Location;

                await _dashboard.loadFirstTime();

                _dashboard.Show();

                Close();
                notifyIcon1.Visible = false;
            }
            else
            {
                _dashboard.Hide();
                _dashboard.notifyIcon1.Visible = false;
                //_dashboard.forceClose = true;
                //_dashboard.Close();
            }
        }

        /// <summary>
        /// close this form and show the signup form in the same location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //SignUp _signUp = new SignUp();
            _signUp._loginForm = this;
            _signUp.Show();

            _signUp.Location = this.Location;

            Close();
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// Sign in the user and move to Dashboard.
        /// First create a login model object and send it to server api,
        /// server returns a token which is saved in the local database.
        /// this form is closed and dashboard form will be shown in the same location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            LoginModel loginModel = new LoginModel()
            {
                UserName = txtLoginID.Text,
                Password = txtLoginPw.Text,
            };

            //var data = new StringContent(JsonConvert.SerializeObject(loginModel));

            //string baseUri = "https://localhost:7110/";
            string baseUri = GlobalFunctions.BaseUri;
            using HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(baseUri);
            using var response = await http.PostAsJsonAsync("api/auth/Login", loginModel);
            if (!response.IsSuccessStatusCode)
            {
                MaterialMessageBox.Show(text: "Login Failed, Please try again.", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }
            Token? token = await response.Content.ReadFromJsonAsync<Token>();
            using DatabaseContext _db = new DatabaseContext();
            if (token == null) return;
            LoginToken loginToken = new LoginToken() { Token = token.token };
            if (!_db.LoginTokens.Any())
            {
                await _db.LoginTokens.AddAsync(loginToken);
                await _db.SaveChangesAsync();
            }

            _dashboard.Show();
            _dashboard.Location = this.Location;

            Close();
            notifyIcon1.Visible = false;
        }
    }
}