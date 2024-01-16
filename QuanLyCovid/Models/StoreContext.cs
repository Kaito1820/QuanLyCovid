using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyCovid.Models
{
    public class StoreContext
    {
        public string ConnectionString { get; set; }
        public StoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        [HttpPost]
        public int InsertDCL(DiemCachLy dcl)
        {
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO diemcachly VALUES(@madcl, @tendcl, @diachi)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madcl", dcl.madcl);
                cmd.Parameters.AddWithValue("tendcl", dcl.tendcl);
                cmd.Parameters.AddWithValue("diachi", dcl.diachi);
                return (cmd.ExecuteNonQuery());
            }
        }

        [HttpPost]
        public List<object> GetSoLuong(int soluong)
        {
            List<object> list = new List<object>();
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT cn.macongnhan, tencongnhan, nuocve, COUNT(cn_tc.matrieuchung) as soluong " +
                    "FROM congnhan cn, cn_tc " +
                    "WHERE cn.macongnhan = cn_tc.macongnhan " +
                    "GROUP BY cn.macongnhan, tencongnhan " +
                    "HAVING soluong >= @soluong";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("soluong", soluong);
                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            macongnhan = reader["macongnhan"].ToString(),
                            tencongnhan = reader["tencongnhan"].ToString(),
                            nuocve = reader["nuocve"].ToString(),
                            soluong = Convert.ToInt32(reader["soluong"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<DiemCachLy> GetTenDCL()
        {
            List<DiemCachLy> list = new List<DiemCachLy>();
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM diemcachly";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DiemCachLy
                        {
                            madcl = reader["madcl"].ToString(),
                            tendcl = reader["tendcl"].ToString(),
                            diachi = reader["diachi"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<CongNhan> LietKeThongTinCNTheoTenDCL(string madcl)
        {
            List<CongNhan> list = new List<CongNhan>();
            using(MySqlConnection conn = GetConnection()) 
            {
                conn.Open();
                var str = "SELECT macongnhan, tencongnhan, gioitinh, namsinh, nuocve " +
                    "FROM congnhan cn, diemcachly dcl " +
                    "WHERE cn.madcl = dcl.madcl " +
                    "AND cn.madcl = @madcl";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madcl", madcl);
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new CongNhan
                        {
                            macongnhan = reader["macongnhan"].ToString(),
                            tencongnhan = reader["tencongnhan"].ToString(),
                            gioitinh = Convert.ToBoolean(reader["gioitinh"]),
                            namsinh = Convert.ToInt32(reader["namsinh"]),
                            nuocve = reader["nuocve"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public int DeleteCN(string? macn)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DeleteCNTC(macn);
                string str = "DELETE FROM congnhan WHERE macongnhan = @macongnhan";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("macongnhan", macn);
                return cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCNTC(string? macn)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "DELETE FROM cn_tc WHERE macongnhan = @macongnhan";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("macongnhan", macn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
        public CongNhan ViewCN(string? macn)
        {
            CongNhan congNhan = new CongNhan();
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM congnhan WHERE macongnhan = @macn";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("macn", macn);
                using(var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    congNhan.macongnhan = reader["macongnhan"].ToString();
                    congNhan.tencongnhan = reader["tencongnhan"].ToString();
                    congNhan.gioitinh = Convert.ToBoolean(reader["gioitinh"]);
                    congNhan.namsinh = Convert.ToInt32(reader["namsinh"]);
                    congNhan.nuocve = reader["nuocve"].ToString();
                    
                    reader.Close();
                }
                conn.Close();
            }
            return congNhan;
        }
    }
}
