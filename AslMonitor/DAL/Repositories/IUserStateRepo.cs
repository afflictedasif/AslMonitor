using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.DTOs;
//using AslMonitor.Services;
using AslMonitor.Utils;
using Microsoft.EntityFrameworkCore;

namespace AslMonitor.DAL.Repositories
{
    public interface IUserStateRepo
    {
        public UserState? Create(UserState userState);
        public bool Delete(int UserStateId);
        public bool Delete(UserState userState);
        public IQueryable<UserState> GetAll();
        public UserState? Get(int UserID);
        public bool Update(UserState userState);


        public Task<UserState?> CreateAsync(UserState userState);
        public Task<bool> DeleteAsync(int userStateId);
        public Task<bool> DeleteAsync(UserState userState);
        public Task<List<UserState>> GetAllListAsync();
        public Task<UserState?> GetAsync(int userStateId);
        public Task<bool> UpdateAsync(UserState userState);
    }

    public class UserStateRepo : IUserStateRepo
    {
        //private DatabaseContext context;
        private CurrentUser? currentUser;
        //public UserStateRepo(DatabaseContext dc)
        //{

        //}
        public UserState? Create(UserState userState)
        {
            using DatabaseContext context = new DatabaseContext();
            string token = context.LoginTokens.FirstOrDefault()!.Token;
            if (string.IsNullOrEmpty(token))
                currentUser = GlobalFunctions.GetCurrentUser(token);
            try
            {
                if (userState == null) return null;

                userState.InTime = DateTime.Now;
                userState.InIPAddress = GlobalFunctions.IpAddress();
                userState.InUserPC = GlobalFunctions.UserPc();
                userState.InUserID = currentUser?.UserID;

                context.UserStates.Add(userState);
                int rowAffected = context.SaveChanges();
                if (rowAffected > 0) return userState;
                else return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Delete(int UserID)
        {
            using DatabaseContext context = new DatabaseContext();

            UserState? userState = Get(UserID: UserID);
            if (userState == null) return false;
            context.UserStates.Remove(userState);
            int rowAffected = context.SaveChanges();
            return rowAffected > 0;
        }
        public bool Delete(UserState userState)
        {
            using DatabaseContext context = new DatabaseContext();

            try
            {
                context.ChangeTracker.Clear();
                context.Entry(userState).State = EntityState.Deleted;

                int rowsAffected = context.SaveChanges();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }
        public IQueryable<UserState> GetAll()
        {
            using DatabaseContext context = new DatabaseContext();


            return context.UserStates.AsNoTracking().OrderBy(m => m.UserID);
        }
        public UserState? Get(int UserID)
        {
            using DatabaseContext context = new DatabaseContext();


            return context.UserStates.SingleOrDefault(us => us.UserID == UserID);
        }
        public bool Update(UserState userState)
        {
            using DatabaseContext context = new DatabaseContext();
            string token = context.LoginTokens.FirstOrDefault()!.Token;
            if (string.IsNullOrEmpty(token))
                currentUser = GlobalFunctions.GetCurrentUser(token);

            try
            {
                if (userState == null) return false;

                userState.UpTime = DateTime.Now;
                userState.UpIPAddress = GlobalFunctions.IpAddress();
                userState.UpUserPC = GlobalFunctions.UserPc();
                userState.UpUserID = currentUser?.UserID;

                context.ChangeTracker.Clear();
                context.Entry(userState).State = EntityState.Modified;
                int rowsAffected = context.SaveChanges();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<UserState>> GetAllListAsync()
        {
            using DatabaseContext context = new DatabaseContext();

            return await context.UserStates.AsNoTracking().OrderBy(m => m.UserID).ToListAsync(); ;
        }
        public async Task<UserState?> GetAsync(int UserID)
        {
            using DatabaseContext context = new DatabaseContext();

            return await context.UserStates.AsNoTracking().FirstOrDefaultAsync(s => s.UserID == UserID);
        }
        public async Task<UserState?> CreateAsync(UserState userState)
        {
            using DatabaseContext context = new DatabaseContext();
            try
            {
                if (userState == null) return null;

                userState.InTime = DateTime.Now;
                userState.InIPAddress = GlobalFunctions.IpAddress();
                userState.InUserPC = GlobalFunctions.UserPc();
                userState.InUserID = currentUser?.UserID;


                context.UserStates.Add(userState);
                int rowsAffected = await context.SaveChangesAsync();
                if (rowsAffected > 0) return userState;
                else return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteAsync(int UserID)
        {
            using DatabaseContext context = new DatabaseContext();

            UserState? userState = Get(UserID: UserID);
            if (userState == null) return false;
            context.UserStates.Remove(userState);
            int rowsAffected = await context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(UserState userState)
        {
            using DatabaseContext context = new DatabaseContext();
            try
            {
                context.ChangeTracker.Clear();
                context.Entry(userState).State = EntityState.Deleted;

                int rowsAffected = await context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(UserState userState)
        {
            try
            {
                using DatabaseContext context = new DatabaseContext();
                string token = context.LoginTokens.FirstOrDefault()!.Token;
                if (string.IsNullOrEmpty(token))
                    currentUser = GlobalFunctions.GetCurrentUser(token);

                context.ChangeTracker.Clear();

                if (userState == null) return false;

                userState.UpTime = DateTime.Now;
                userState.UpIPAddress = GlobalFunctions.IpAddress();
                userState.UpUserPC = GlobalFunctions.UserPc();
                userState.UpUserID = currentUser?.UserID;


                context.Entry(userState).State = EntityState.Modified;
                int rowsAffected = await context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
