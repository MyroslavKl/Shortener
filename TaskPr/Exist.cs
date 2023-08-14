using TaskPr.Data;

namespace TaskPr
{
    public class Exist
    {
        public bool IsEmailExist(ApplycationDbContext _db, string email) //Email
        {
            bool existEmail = _db.PesonalInformations.Any(information => information.Email == email);
            if (existEmail == false)
            {
                return false;
            }
            return true;
        }

        public bool IsPasswordExist(ApplycationDbContext _db, string password) //Password
        {
            bool existPassword = _db.PesonalInformations.Any(information => information.Password == password);
            if (existPassword == false)
            {
                return false;
            }
            return true;
        }
        public bool IsUrlExist(ApplycationDbContext _db, string url) //Url
        {
            bool existUrl = _db.UrlsInfo.Any(information => information.Url == url);
            if (existUrl == false)
            {
                return false;
            }
            return true;
        }
    }
}
