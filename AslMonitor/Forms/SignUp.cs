using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.Utils;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AslMonitor.Forms
{
    public partial class SignUp : MaterialForm
    {
        private readonly Dashboard _dashboard;
        public  Form1? _loginForm;

        public SignUp(Dashboard dashboard)
        {
            InitializeComponent();
            _dashboard = dashboard;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            //notifyIcon1.Visible = true;
            e.Cancel = true;
        }

        /// <summary>
        /// Checks internet connection, validate password, creates an user, 
        /// gets login token from server and save it into local machine, 
        /// redirect to dashboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            bool isOnline = GlobalFunctions.CheckForInternetConnection();
            if(!isOnline)
            {
                MaterialMessageBox.Show(text: "Internet Connection failed.", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }

            if (txtPassword.Text.Trim().Length < 6)
            {
                MaterialMessageBox.Show(text: "Password should be atleast 6 characters", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MaterialMessageBox.Show(text: "Password didn't match!", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }

            UserInfo userInfo = new UserInfo()
            {
                UserName = txtUserNm.Text.Trim(),
                LoginPW = txtPassword.Text.Trim(),
                EmailID = txtEmail.Text.Trim(),
                MobNo = txtContact.Text.Trim(),
                LoginID = txtContact.Text.Trim(),
                TimeFr = new TimeSpan(00, 00, 00),
                TimeTo = new TimeSpan(23, 59, 59),
                Status = "A",
                Address = txtAddress.Text,
                UserType = "USER",
                LoginBy = "M",
            };

            //string baseUri = "https://localhost:7110/api/auth/signup/";
            //HttpClient http = new HttpClient();
            //using var response = await http.PostAsJsonAsync(baseUri, user);
            //Token? token = await response.Content.ReadFromJsonAsync<Token>();



            //string baseUri = "https://localhost:7110/";
            string baseUri = GlobalFunctions.BaseUri;
            using HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(baseUri);
            //http.DefaultRequestHeaders.Accept.Clear();
            //http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using var response = await http.PostAsJsonAsync("api/auth/signup", userInfo);
            if (!response.IsSuccessStatusCode)
            {
                MaterialMessageBox.Show(text: "Something Went Wrong, Please try again.", UseRichTextBox: true, FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }
            Token? token = await response.Content.ReadFromJsonAsync<Token>();

            //Saves token into local machine
            using DatabaseContext _db = new DatabaseContext();
            if (token == null) return;
            LoginToken loginToken = new LoginToken() { Token = token.token };
            var inserted =await _db.LoginTokens.AddAsync(loginToken);
            await _db.SaveChangesAsync();

            //Dashboard _dashboard = new Dashboard();
            _dashboard.token = token.token;
            await _dashboard.loadFirstTime();
            _dashboard.Show();
            _dashboard.Location = this.Location;
            //formD.token = token.token;
            Close();
            //notifyIcon1.Visible = false;
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            _loginForm!.Show();
            _loginForm!.Location = this.Location;
            Close();
        }
    }


    public class Token
    {
        public string token { get; set; }
    }
}
