using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace YorumAnalizi
{
    public class Database
    {
        private static string dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "yorumlar.db");
        private static string connStr = $"Data Source={dbPath};Version=3;";

        // Veritabanını oluştur (ilk çalıştırmada)
        public static void Baslat()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS Yorumlar (
                        Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                        Metin       TEXT NOT NULL,
                        InsanDuygu  TEXT NOT NULL,
                        AIDuygu     TEXT NOT NULL,
                        Esleme      TEXT NOT NULL,
                        Konu        TEXT NOT NULL,
                        Tarih       TEXT NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS AIRequests (
                        Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                        YorumId     INTEGER NOT NULL,
                        Girdi       TEXT NOT NULL,
                        Cikti       TEXT NOT NULL,
                        IslemSuresi TEXT NOT NULL,
                        ZamanDamgasi TEXT NOT NULL,
                        FOREIGN KEY (YorumId) REFERENCES Yorumlar(Id)
                    );";
                new SQLiteCommand(sql, conn).ExecuteNonQuery();
            }
        }

        // Yorum ekle
        public static int YorumEkle(YorumData y)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"INSERT INTO Yorumlar 
                    (Metin, InsanDuygu, AIDuygu, Esleme, Konu, Tarih)
                    VALUES (@m, @id, @ad, @e, @k, @t);
                    SELECT last_insert_rowid();";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@m",  y.Metin);
                cmd.Parameters.AddWithValue("@id", y.InsanDuygu);
                cmd.Parameters.AddWithValue("@ad", y.AIDuygu);
                cmd.Parameters.AddWithValue("@e",  y.Esleme);
                cmd.Parameters.AddWithValue("@k",  y.Konu);
                cmd.Parameters.AddWithValue("@t",  y.Tarih);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // AI isteğini logla
        public static void AIRequestLogla(int yorumId, string girdi, string cikti, string sure)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"INSERT INTO AIRequests 
                    (YorumId, Girdi, Cikti, IslemSuresi, ZamanDamgasi)
                    VALUES (@yid, @g, @c, @s, @z)";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@yid", yorumId);
                cmd.Parameters.AddWithValue("@g",   girdi);
                cmd.Parameters.AddWithValue("@c",   cikti);
                cmd.Parameters.AddWithValue("@s",   sure);
                cmd.Parameters.AddWithValue("@z",   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        // Yorum sil
        public static void YorumSil(int id)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                new SQLiteCommand($"DELETE FROM Yorumlar WHERE Id={id}", conn).ExecuteNonQuery();
                new SQLiteCommand($"DELETE FROM AIRequests WHERE YorumId={id}", conn).ExecuteNonQuery();
            }
        }

        // Tüm yorumları getir
        public static List<YorumData> TumYorumlariGetir()
        {
            var liste = new List<YorumData>();
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                var reader = new SQLiteCommand(
                    "SELECT Id, Metin, InsanDuygu, AIDuygu, Esleme, Konu, Tarih FROM Yorumlar ORDER BY Id",
                    conn).ExecuteReader();
                while (reader.Read())
                {
                    liste.Add(new YorumData
                    {
                        Id         = reader.GetInt32(0),
                        Metin      = reader.GetString(1),
                        InsanDuygu = reader.GetString(2),
                        AIDuygu    = reader.GetString(3),
                        Esleme     = reader.GetString(4),
                        Konu       = reader.GetString(5),
                        Tarih      = reader.GetString(6)
                    });
                }
            }
            return liste;
        }
    }
}

// YorumData modeli buraya taşındı (FormRapor.cs'ten kaldırılabilir)
