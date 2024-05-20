using Application._Resources;
using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UserUpdateCommand : IUserUpdate
    {
        private ITaskRepository<User>? _userRepository;
        public UserUpdateCommand(ITaskRepository<User> userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<string> UpdateUserAsync(User entity)
        {
            User userDb = await _userRepository!.GetById(Guid.Parse(entity.Id!));
            userDb.NotificationPreference = entity.NotificationPreference;
            userDb.Password = entity.Password;
            userDb.Email = entity.Email;
            userDb.Name = entity.Name;
            await _userRepository!.Update(userDb);
            return Constants.OK;
        }
    }
}
