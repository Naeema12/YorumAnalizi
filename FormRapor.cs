using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace YorumAnalizi
{
    public class FormRapor : Form
    {
        private List<YorumData> veriler;

        public FormRapor(List<YorumData> yorumlar)
        {
            this.veriler = yorumlar;
            InitializeRapor();
        }

        private void InitializeRapor()
        {
            this.Text = "Rapor & İstatistik";
            this.Size = new System.Drawing.Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 9.5f);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header
            Panel pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            pnlHeader.BackColor = Color.FromArgb(70, 110, 160);
            Label lblBaslik = new Label();
            lblBaslik.Text = "Rapor & İstatistik";
            lblBaslik.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.Dock = DockStyle.Fill;
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            pnlHeader.Controls.Add(lblBaslik);
            this.Controls.Add(pnlHeader);

            // İstatistik kartları
            int toplam  = veriler.Count;
            int pozitif = veriler.Count(v => v.InsanDuygu == "Pozitif");
            int notr    = veriler.Count(v => v.InsanDuygu == "Nötr");
            int negatif = veriler.Count(v => v.InsanDuygu == "Negatif");
            int eslesen = veriler.Count(v => v.InsanDuygu == v.AIDuygu);
            double dogruluk = toplam > 0 ? (double)eslesen / toplam * 100 : 0;

            Panel pnlKartlar = new Panel();
            pnlKartlar.Location = new Point(10, 60);
            pnlKartlar.Size = new Size(865, 100);
            pnlKartlar.BackColor = Color.Transparent;
            this.Controls.Add(pnlKartlar);

            string[] kartBaslik = { "Toplam Yorum", "Pozitif", "Nötr", "Negatif", "AI Doğruluğu" };
            string[] kartDeger  = { toplam.ToString(), pozitif.ToString(), notr.ToString(), negatif.ToString(), $"%{dogruluk:F0}" };
            Color[]  kartRenk   = {
                Color.FromArgb(70,110,160),
                Color.FromArgb(40,167,69),
                Color.FromArgb(255,193,7),
                Color.FromArgb(220,53,69),
                Color.FromArgb(111,66,193)
            };

            for (int i = 0; i < 5; i++)
            {
                Panel kart = new Panel();
                kart.Location = new Point(i * 175, 0);
                kart.Size = new Size(165, 90);
                kart.BackColor = kartRenk[i];
                kart.BorderStyle = BorderStyle.None;

                Label lDeger = new Label();
                lDeger.Text = kartDeger[i];
                lDeger.Font = new Font("Segoe UI", 22f, FontStyle.Bold);
                lDeger.ForeColor = Color.White;
                lDeger.Location = new Point(0, 12);
                lDeger.Size = new Size(165, 40);
                lDeger.TextAlign = ContentAlignment.MiddleCenter;

                Label lBaslik = new Label();
                lBaslik.Text = kartBaslik[i];
                lBaslik.Font = new Font("Segoe UI", 9f);
                lBaslik.ForeColor = Color.FromArgb(220, 230, 255);
                lBaslik.Location = new Point(0, 54);
                lBaslik.Size = new Size(165, 22);
                lBaslik.TextAlign = ContentAlignment.MiddleCenter;

                kart.Controls.Add(lDeger);
                kart.Controls.Add(lBaslik);
                pnlKartlar.Controls.Add(kart);
            }

            // Pasta grafik paneli
            Panel pnlPasta = new Panel();
            pnlPasta.Location = new Point(10, 170);
            pnlPasta.Size = new Size(420, 280);
            pnlPasta.BackColor = Color.White;
            pnlPasta.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlPasta);

            Label lblPastaBaslik = new Label();
            lblPastaBaslik.Text = "Duygu Dağılımı";
            lblPastaBaslik.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblPastaBaslik.ForeColor = Color.FromArgb(60, 60, 60);
            lblPastaBaslik.Location = new Point(0, 8);
            lblPastaBaslik.Size = new Size(420, 24);
            lblPastaBaslik.TextAlign = ContentAlignment.MiddleCenter;
            pnlPasta.Controls.Add(lblPastaBaslik);

            // Pasta grafik çiz
            PictureBox pbPasta = new PictureBox();
            pbPasta.Location = new Point(10, 36);
            pbPasta.Size = new Size(400, 230);
            pbPasta.BackColor = Color.White;
            pnlPasta.Controls.Add(pbPasta);

            pbPasta.Paint += (s, e) => {
                if (toplam == 0) {
                    e.Graphics.DrawString("Veri yok", new Font("Segoe UI", 12f), Brushes.Gray, 150, 100);
                    return;
                }
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                float[] degerler = { pozitif, notr, negatif };
                Color[] renkler  = { Color.FromArgb(40,167,69), Color.FromArgb(255,193,7), Color.FromArgb(220,53,69) };
                string[] etiketler = { "Pozitif", "Nötr", "Negatif" };
                float baslacAci = -90f;
                int cx = 120, cy = 110, r = 90;
                for (int i = 0; i < 3; i++) {
                    if (degerler[i] == 0) continue;
                    float aci = (degerler[i] / toplam) * 360f;
                    using (SolidBrush br = new SolidBrush(renkler[i]))
                        e.Graphics.FillPie(br, cx - r, cy - r, r*2, r*2, baslacAci, aci);
                    e.Graphics.DrawPie(Pens.White, cx - r, cy - r, r*2, r*2, baslacAci, aci);
                    baslacAci += aci;
                }
                // Legend
                for (int i = 0; i < 3; i++) {
                    using (SolidBrush br = new SolidBrush(renkler[i]))
                        e.Graphics.FillRectangle(br, 255, 60 + i * 36, 18, 18);
                    e.Graphics.DrawString($"{etiketler[i]}: {degerler[i]} ({(toplam>0?degerler[i]/toplam*100:0):F0}%)",
                        new Font("Segoe UI", 9f), Brushes.Black, 280, 60 + i * 36);
                }
            };

            // Konu dağılımı paneli
            Panel pnlKonu = new Panel();
            pnlKonu.Location = new Point(445, 170);
            pnlKonu.Size = new Size(430, 280);
            pnlKonu.BackColor = Color.White;
            pnlKonu.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlKonu);

            Label lblKonuBaslik = new Label();
            lblKonuBaslik.Text = "Konu Dağılımı";
            lblKonuBaslik.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblKonuBaslik.ForeColor = Color.FromArgb(60, 60, 60);
            lblKonuBaslik.Location = new Point(0, 8);
            lblKonuBaslik.Size = new Size(430, 24);
            lblKonuBaslik.TextAlign = ContentAlignment.MiddleCenter;
            pnlKonu.Controls.Add(lblKonuBaslik);

            PictureBox pbKonu = new PictureBox();
            pbKonu.Location = new Point(10, 36);
            pbKonu.Size = new Size(410, 230);
            pbKonu.BackColor = Color.White;
            pnlKonu.Controls.Add(pbKonu);

            pbKonu.Paint += (s, e) => {
                if (toplam == 0) {
                    e.Graphics.DrawString("Veri yok", new Font("Segoe UI", 12f), Brushes.Gray, 150, 100);
                    return;
                }
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                string[] konular = { "Lezzet", "Servis", "Temizlik/Oda", "Fiyat", "Atmosfer", "Genel" };
                Color[]  konuRenk = {
                    Color.FromArgb(70,130,180), Color.FromArgb(255,140,0),
                    Color.FromArgb(60,179,113), Color.FromArgb(220,53,69),
                    Color.FromArgb(147,112,219), Color.FromArgb(128,128,128)
                };
                int barGenislik = 48, aralik = 16, baslangic = 20;
                for (int i = 0; i < konular.Length; i++) {
                    int sayi = veriler.Count(v => v.Konu == konular[i]);
                    int barYukseklik = toplam > 0 ? (int)((double)sayi / toplam * 160) : 0;
                    int x = baslangic + i * (barGenislik + aralik);
                    int y = 175 - barYukseklik;
                    using (SolidBrush br = new SolidBrush(konuRenk[i]))
                        e.Graphics.FillRectangle(br, x, y, barGenislik, barYukseklik > 0 ? barYukseklik : 2);
                    e.Graphics.DrawString(sayi.ToString(), new Font("Segoe UI", 8.5f, FontStyle.Bold), Brushes.Black, x + 16, y - 18);
                    // Konu etiketi (kısa)
                    string etiket = konular[i].Length > 7 ? konular[i].Substring(0, 7) : konular[i];
                    e.Graphics.DrawString(etiket, new Font("Segoe UI", 7.5f), Brushes.DimGray, x - 2, 180);
                }
            };

            // Karşılaştırma özet
            Panel pnlOzet = new Panel();
            pnlOzet.Location = new Point(10, 460);
            pnlOzet.Size = new Size(865, 120);
            pnlOzet.BackColor = Color.White;
            pnlOzet.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlOzet);

            Label lblOzetBaslik = new Label();
            lblOzetBaslik.Text = "İnsan vs AI Karşılaştırma Özeti";
            lblOzetBaslik.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblOzetBaslik.ForeColor = Color.FromArgb(60, 60, 60);
            lblOzetBaslik.Location = new Point(10, 8);
            lblOzetBaslik.Size = new Size(400, 22);
            pnlOzet.Controls.Add(lblOzetBaslik);

            int eslesmedi = toplam - eslesen;
            string ozetMetin =
                $"Toplam karşılaştırılan yorum: {toplam}     " +
                $"Eşleşen: {eslesen}     " +
                $"Eşleşmeyen: {eslesmedi}     " +
                $"AI Doğruluk Oranı: %{dogruluk:F1}";

            Label lblOzetMetin = new Label();
            lblOzetMetin.Text = ozetMetin;
            lblOzetMetin.Font = new Font("Segoe UI", 9.5f);
            lblOzetMetin.ForeColor = Color.FromArgb(50, 50, 50);
            lblOzetMetin.Location = new Point(10, 38);
            lblOzetMetin.Size = new Size(840, 22);
            pnlOzet.Controls.Add(lblOzetMetin);

            // Progress bar - doğruluk
            Label lblProg = new Label();
            lblProg.Text = "Doğruluk:";
            lblProg.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblProg.Location = new Point(10, 68);
            lblProg.Size = new Size(75, 20);
            pnlOzet.Controls.Add(lblProg);

            ProgressBar pb = new ProgressBar();
            pb.Location = new Point(90, 68);
            pb.Size = new Size(680, 20);
            pb.Minimum = 0; pb.Maximum = 100;
            pb.Value = (int)dogruluk;
            pb.ForeColor = Color.FromArgb(40, 167, 69);
            pnlOzet.Controls.Add(pb);

            Label lblPct = new Label();
            lblPct.Text = $"%{dogruluk:F0}";
            lblPct.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblPct.ForeColor = Color.FromArgb(40, 167, 69);
            lblPct.Location = new Point(775, 68);
            lblPct.Size = new Size(60, 20);
            pnlOzet.Controls.Add(lblPct);

            // Kapat butonu
            Button btnKapat = new Button();
            btnKapat.Text = "Kapat";
            btnKapat.Location = new Point(385, 590);
            btnKapat.Size = new Size(120, 34);
            btnKapat.BackColor = Color.FromArgb(70, 110, 160);
            btnKapat.ForeColor = Color.White;
            btnKapat.FlatStyle = FlatStyle.Flat;
            btnKapat.FlatAppearance.BorderSize = 0;
            btnKapat.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            btnKapat.Click += (s, e2) => this.Close();
            this.Controls.Add(btnKapat);
        }
    }
}
