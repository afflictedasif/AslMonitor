using AslMonitor.DAL.Models;
using AslMonitor.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AslMonitor.Services;

public interface IUserStateService
{
    /// <summary>
    /// Gets the previous state of the user from the database, 
    /// updates its timeTo field if the last state is from current day. then Insert log, and apply final update to UserState. 
    /// </summary>
    /// <param name="userState"></param>
    /// <returns></returns>
    public bool ChangeUserState(UserState userState);
    /// <summary>
    /// Gets the previous state of the user from the database, 
    /// updates its timeTo field if the last state is from current day. then Insert log, and apply final update to UserState. 
    /// </summary>
    /// <param name="userState"></param>
    /// <returns></returns>
    public Task<bool> ChangeUserStateAsync(UserState userState);
    public Task<bool> ChangeUserStateWithoutLogAsync(UserState userState);
    /// <summary>
    /// Inserts a new UserState
    /// </summary>
    /// <param name="userState"></param>
    /// <returns>Inserted UserState</returns>
    public UserState? CreateUserState(UserState userState);
    /// <summary>
    /// Inserts a new UserState
    /// </summary>
    /// <param name="userState"></param>
    /// <returns>Inserted UserState</returns>
    public Task<UserState?> CreateUserStateAsync(UserState userState);
    /// <summary>
    /// Return UserState of the given userID
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns></returns>
    public Task<UserState> GetLastStateAsync(int UserID);

}

public class UserStateService : IUserStateService
{
    private readonly IUserStateRepo _userStateRepo;
    private readonly ILogService _logService;

    public UserStateService(IUserStateRepo userStateRepo, ILogService logService)
    {
        _userStateRepo = userStateRepo;
        _logService = logService;
    }

    /// <summary>
    /// Gets the previous state of the user from the database, 
    /// updates its timeTo field  if the last state is from current day. then Insert log, and apply final update to UserState. 
    /// </summary>
    /// <param name="userState"></param>
    /// <returns></returns>
    public bool ChangeUserState(UserState userState)
    {
        try
        {
            UserState? prevState = _userStateRepo.Get(userState.UserID);
            if (prevState == null) return false;

            if (prevState.TimeTo == null)
            {
                if (prevState.TimeFrom != null && prevState.TimeFrom.Value.Date == DateTime.Now.Date)
                {
                    prevState.TimeTo = DateTime.Now;
                    if (!_userStateRepo.Update(prevState)) return false;
                }
            }

            bool logGenerated = _logService.InsertLog<UserState>(TableName: "UserStateS", logType: "UPDATE", userState.UserStateId);
            if (!logGenerated) return false;

            return _userStateRepo.Update(userState);
        }
        catch
        {
            return false;
        }

    }

    /// <summary>
    /// Gets the previous state of the user from the database, 
    /// updates its timeTo field if the last state is from current day. then Insert log, and apply final update to UserState. 
    /// </summary>
    /// <param name="userState"></param>
    /// <returns></returns>
    public async Task<bool> ChangeUserStateAsync(UserState userState)
    {
        UserState? prevState = await _userStateRepo.GetAsync(userState.UserID);
        if (prevState == null) return false;

        if (prevState.TimeTo == null)
        {
            if (prevState.TimeFrom != null && prevState.TimeFrom.Value.Date == DateTime.Now.Date)
            {
                prevState.TimeTo = DateTime.Now;
                if (!await _userStateRepo.UpdateAsync(prevState)) return false;
            }
        }
        userState.UserStateId = prevState.UserStateId;

        bool logGenerated = await _logService.InsertLogAsync<UserState>(TableName: "UserStateS", logType: "UPDATE", userState.UserStateId);
        if (!logGenerated) return false;

        return await _userStateRepo.UpdateAsync(userState);
    }

    public async Task<bool> ChangeUserStateWithoutLogAsync(UserState userState)
    {
        return await _userStateRepo.UpdateAsync(userState);
    }

    /// <summary>
    /// Inserts a new UserState
    /// </summary>
    /// <param name="userState"></param>
    /// <returns>Inserted UserState</returns>
    public UserState? CreateUserState(UserState userState)
    {
        if (userState == null) return null;
        return _userStateRepo.Create(userState);
    }

    /// <summary>
    /// Inserts a new UserState
    /// </summary>
    /// <param name="userState"></param>
    /// <returns>Inserted UserState</returns>
    public async Task<UserState?> CreateUserStateAsync(UserState userState)
    {
        if (userState == null) return null;
        return await _userStateRepo.CreateAsync(userState);
    }

    /// <summary>
    /// Return UserState of the given userID
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns></returns>
    public async Task<UserState?> GetLastStateAsync(int UserID)
    {
        //return await _userStateRepo.GetAll().FirstOrDefaultAsync(s => s.UserID == UserID);
        return await _userStateRepo.GetAsync(UserID);
    }
}

