using System;

namespace QuanLyKiTucXa
{
    public static class UserSession
    {
        public static string TenDangNhap { get; set; }
        public static string Quyen { get; set; }
        public static string ID { get; set; }
        public static bool IsLoggedIn { get; set; }

        // Phương thức đăng nhập
        public static void Login(string id, string tenDangNhap, string quyen)
        {
            ID = id;
            TenDangNhap = tenDangNhap;
            Quyen = quyen;
            IsLoggedIn = true;
        }

        // Phương thức đăng xuất
        public static void Logout()
        {
            ID = null;
            TenDangNhap = null;
            Quyen = null;
            IsLoggedIn = false;
        }

        // Kiểm tra quyền Admin
        public static bool IsAdmin()
        {
            return Quyen?.ToLower() == "admin";
        }

        // Kiểm tra quyền Quản lý
        public static bool IsQuanLy()
        {
            return Quyen?.ToLower() == "quanly";
        }
    }
}