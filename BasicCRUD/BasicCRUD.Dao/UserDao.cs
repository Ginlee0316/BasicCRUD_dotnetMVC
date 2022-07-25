using BasicCRUD.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCRUD.Dao
{
    public class UserDao
    {
        /// <summary>
        /// 新增人員
        /// </summary>
        /// <param name="user"></param>
        public int CreateUser(User user)
        {
            int userId;

            const string sql = @"
                INSERT INTO dbo.USER
                (
                    USER_ACCOUNT, USER_NAME, EMAIL
                )
                VALUES
                (
                    @UserAccount, @UserName, @Email   
                )
                Select SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(GetDbConnectionStr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserAccount", user.UserAccount));
                cmd.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                userId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }

            return userId;
        }

        /// <summary>
        /// 取得所有人員資料
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserData()
        {
            DataTable dt = new DataTable();

            const string sql = @"
                SELECT USER_ID, USER_ACCOUNT, USER_NAME, EMAIL
                FROM dbo.USER";

            using (SqlConnection conn = new SqlConnection(GetDbConnectionStr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return MappingUserData(dt);
        }

        /// <summary>
        /// 依Id取得人員資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(string userId)
        {
            User user = new User();

            DataTable dt = new DataTable();
            const string sql = @"
                SELECT USER_ID, USER_ACCOUNT, USER_NAME, EMAIL
                FROM dbo.USER
                WHERE USER_ID = @UserId";

            using (SqlConnection conn = new SqlConnection(GetDbConnectionStr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserId", userId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            if(dt.Rows.Count > 0 )
            {
                user.UserId = dt.Rows[0]["USER_ID"].ToString();
                user.UserAccount = dt.Rows[0]["USER_ACCOUNT"].ToString();
                user.UserName = dt.Rows[0]["USER_NAME"].ToString();
                user.Email = dt.Rows[0]["EMAIL"].ToString();
            }

            return user;
        }

        /// <summary>
        /// 修改人員資料
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUserData(User user)
        {
            const string sql = @"
                UPDATE dbo.USER
                SET 
                    USER_ACCOUNT = @@UserAccount, 
                    USER_NAME = @UserName,
                    EMAIL = @Email
                WHERE USER_ID = @UserId";

            using (SqlConnection conn = new SqlConnection(GetDbConnectionStr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                cmd.Parameters.Add(new SqlParameter("@UserAccount", user.UserAccount));
                cmd.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        /// <summary>
        /// 刪除人員資料
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(string userId)
        {
            const string sql = @"
                DELETE FROM dbo.USER WHERE USER_ID = @UserId";

            using (SqlConnection conn = new SqlConnection(GetDbConnectionStr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserId", userId));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private List<User> MappingUserData(DataTable data)
        {
            List<User> result = new List<User>();
            foreach (DataRow row in data.Rows)
            {
                result.Add(new User()
                {
                    UserId = row["USER_ID"].ToString(),
                    UserAccount = row["USER_ACCOUNT"].ToString(),
                    UserName = row["USER_NAME"].ToString(),
                    Email = row["EMAIL"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionStr()
        {
            return "";
        }

    }
}
