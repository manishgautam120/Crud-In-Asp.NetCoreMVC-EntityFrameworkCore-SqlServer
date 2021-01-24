using OA.DATA;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.SERVICE
{
   public class UserProfileService: IUserProfileService  
    {  
        private IRepository<UserProfile> userProfileRepository;
        public UserProfileService(IRepository<UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }
        public UserProfile GetUserProfile(long id)
        {
            return userProfileRepository.Get(id);
        }
    }  
}
