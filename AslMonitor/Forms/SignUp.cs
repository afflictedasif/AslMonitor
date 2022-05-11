using AslMonitor.DAL;
using AslMonitor.DAL.Models;
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

        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo()
            {
                UserName = txtUserNm.Text,
                LoginPW = txtPassword.Text,
                EmailID = txtEmail.Text,
                MobNo = txtContact.Text,
                LoginID = txtContact.Text,
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



            string baseUri = "https://localhost:7110/";
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

            using DatabaseContext _db = new DatabaseContext();
            if (token == null) return;
            LoginToken loginToken = new LoginToken() { Token = token.token };
            var inserted =await _db.LoginTokens.AddAsync(loginToken);
            await _db.SaveChangesAsync();

            //Dashboard _dashboard = new Dashboard();
            _dashboard.Show();
            _dashboard.Location = this.Location;
            //formD.token = token.token;
            Close();
            //notifyIcon1.Visible = false;

        }
    }


    public class Token
    {
        public string token { get; set; }
    }
}
