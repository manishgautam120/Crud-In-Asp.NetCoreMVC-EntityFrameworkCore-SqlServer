using OA.DATA;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.SERVICE
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfile(long id);
    }
}
