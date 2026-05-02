using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YorumAnalizi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Database.Baslat();
            VeritabaniYukle();
        }

        // Uygulama açılınca veritabanından yükle
        private void VeritabaniYukle()
        {
            lvYorumlar.Items.Clear();
            var yorumlar = Database.TumYorumlariGetir();
            foreach (var y in yorumlar)
            {
                ListViewItem item = new ListViewItem(y.Metin);
                item.SubItems.Add(y.InsanDuygu);
                item.SubItems.Add(y.AIDuygu);
                item.SubItems.Add(y.Esleme);
                item.SubItems.Add(y.Konu);
                item.SubItems.Add(y.Tarih);
                item.Tag = y.Id; // DB id'yi sakla
                item.BackColor = RenkVer(y.InsanDuygu);
                lvYorumlar.Items.Add(item);
            }
            GuncellIstatistik();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string metin = txtYorum.Text.Trim();
            if (metin == "")
            {
                MessageBox.Show("Lütfen bir yorum girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYorum.Focus(); return;
            }
            if (cmbKonu.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen konu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbKonu.Focus(); return;
            }
            string insanDuygu = "";
            if (rbPozitif.Checked) insanDuygu = "Pozitif";
            else if (rbNotr.Checked) insanDuygu = "Nötr";
            else if (rbNegatif.Checked) insanDuygu = "Negatif";
            if (insanDuygu == "")
            {
                MessageBox.Show("Lütfen duygu etiketini seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string konu   = cmbKonu.SelectedItem.ToString();
            string tarih  = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            // AI analizi — süreyi ölç
            var sw = Stopwatch.StartNew();
            string aiDuygu = DuyguAnaliz(metin);
            sw.Stop();
            string sure = sw.ElapsedMilliseconds + " ms";

            string esleme = insanDuygu == aiDuygu ? "Eşleşti" : "Farklı";

            var yorum = new YorumData
            {
                Metin = metin, InsanDuygu = insanDuygu,
                AIDuygu = aiDuygu, Esleme = esleme,
                Konu = konu, Tarih = tarih
            };

            // Veritabanına kaydet
            int newId = Database.YorumEkle(yorum);
            Database.AIRequestLogla(newId, metin, aiDuygu, sure);

            // Tabloya ekle
            ListViewItem item = new ListViewItem(metin);
            item.SubItems.Add(insanDuygu);
            item.SubItems.Add(aiDuygu);
            item.SubItems.Add(esleme == "Eşleşti" ? "✓ Eşleşti" : "✗ Farklı");
            item.SubItems.Add(konu);
            item.SubItems.Add(tarih);
            item.Tag = newId;
            item.BackColor = RenkVer(insanDuygu);
            lvYorumlar.Items.Add(item);
            GuncellIstatistik();

            txtYorum.Clear();
            cmbKonu.SelectedIndex = 0;
            rbPozitif.Checked = false; rbNotr.Checked = false; rbNegatif.Checked = false;
            txtYorum.Focus();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lvYorumlar.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz yorumu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Seçili yorum silinsin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var secili = lvYorumlar.SelectedItems[0];
                if (secili.Tag != null)
                    Database.YorumSil(Convert.ToInt32(secili.Tag));
                secili.Remove();
                GuncellIstatistik();
            }
        }

        private void cmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            UygulaFiltre();
        }

        private void UygulaFiltre()
        {
            string filtre = cmbFiltre.SelectedItem?.ToString() ?? "Tümü";
            var tumYorumlar = new List<ListViewItem>();
            foreach (ListViewItem i in lvYorumlar.Items)
                tumYorumlar.Add((ListViewItem)i.Clone());

            lvYorumlar.Items.Clear();
            foreach (var item in tumYorumlar)
            {
                bool goster = filtre == "Tümü"
                    || item.SubItems[1].Text == filtre
                    || (filtre == "Eşleşti" && item.SubItems[3].Text.Contains("Eşleşti"))
                    || (filtre == "Farklı"  && item.SubItems[3].Text.Contains("Farklı"));
                if (goster)
                {
                    item.BackColor = RenkVer(item.SubItems[1].Text);
                    lvYorumlar.Items.Add(item);
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (lvYorumlar.Items.Count == 0)
            {
                MessageBox.Show("Kaydedilecek yorum yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Dosyası|*.csv";
            sfd.FileName = "yorumlar_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine("Yorum,InsanDuygu,AIDuygu,Esleme,Konu,Tarih");
                    foreach (ListViewItem item in lvYorumlar.Items)
                        sw.WriteLine($"\"{item.Text}\",{item.SubItems[1].Text},{item.SubItems[2].Text},{item.SubItems[3].Text},{item.SubItems[4].Text},{item.SubItems[5].Text}");
                }
                MessageBox.Show("CSV olarak kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Dosyası|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var satirlar = File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);
                    for (int i = 1; i < satirlar.Length; i++)
                    {
                        var p = satirlar[i].Split(',');
                        if (p.Length < 6) continue;
                        var yorum = new YorumData
                        {
                            Metin = p[0].Trim('"'), InsanDuygu = p[1],
                            AIDuygu = p[2], Esleme = p[3].Replace("✓ ", "").Replace("✗ ", ""),
                            Konu = p[4], Tarih = p[5]
                        };
                        int newId = Database.YorumEkle(yorum);
                        ListViewItem item = new ListViewItem(yorum.Metin);
                        item.SubItems.Add(yorum.InsanDuygu);
                        item.SubItems.Add(yorum.AIDuygu);
                        item.SubItems.Add(p[3]);
                        item.SubItems.Add(yorum.Konu);
                        item.SubItems.Add(yorum.Tarih);
                        item.Tag = newId;
                        item.BackColor = RenkVer(yorum.InsanDuygu);
                        lvYorumlar.Items.Add(item);
                    }
                    GuncellIstatistik();
                    MessageBox.Show("Veriler yüklendi ve veritabanına kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch { MessageBox.Show("Dosya okunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            if (lvYorumlar.Items.Count == 0)
            {
                MessageBox.Show("Rapor için önce yorum ekleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var liste = new List<YorumData>();
            foreach (ListViewItem item in lvYorumlar.Items)
            {
                liste.Add(new YorumData
                {
                    Metin      = item.Text,
                    InsanDuygu = item.SubItems[1].Text,
                    AIDuygu    = item.SubItems[2].Text,
                    Esleme     = item.SubItems[3].Text,
                    Konu       = item.SubItems[4].Text,
                    Tarih      = item.SubItems[5].Text
                });
            }
            new FormRapor(liste).ShowDialog();
        }

        private void GuncellIstatistik()
        {
            int toplam  = lvYorumlar.Items.Count;
            int pozitif = lvYorumlar.Items.Cast<ListViewItem>().Count(i => i.SubItems[1].Text == "Pozitif");
            int notr    = lvYorumlar.Items.Cast<ListViewItem>().Count(i => i.SubItems[1].Text == "Nötr");
            int negatif = lvYorumlar.Items.Cast<ListViewItem>().Count(i => i.SubItems[1].Text == "Negatif");
            int eslesen = lvYorumlar.Items.Cast<ListViewItem>().Count(i => i.SubItems[3].Text.Contains("Eşleşti"));
            double dogruluk = toplam > 0 ? (double)eslesen / toplam * 100 : 0;
            lblIstatistik.Text = $"Toplam: {toplam}   |   Pozitif: {pozitif}   Nötr: {notr}   Negatif: {negatif}   |   AI Doğruluğu: %{dogruluk:F0}   ({eslesen}/{toplam} eşleşti)";
        }

        private Color RenkVer(string duygu)
        {
            if (duygu == "Pozitif") return Color.FromArgb(220, 255, 225);
            if (duygu == "Negatif") return Color.FromArgb(255, 220, 220);
            return Color.FromArgb(255, 250, 210);
        }

        private string DuyguAnaliz(string metin)
        {
            metin = metin.ToLower();
            string[] poz = { "harika", "muhteşem", "güzel", "lezzetli", "mükemmel", "iyi", "temiz",
                             "güler yüzlü", "hızlı", "makul", "fiyat performans", "tavsiye",
                             "beğendim", "süper", "memnun", "çok iyi", "başarılı" };
            string[] neg = { "yavaş", "kötü", "berbat", "hayal kırıklığı", "çalışmıyor", "şikayet",
                             "küçük", "pahalı", "kirli", "soğuk", "beklettiler", "ilgisiz",
                             "rezalet", "beğenmedim", "kötüydü" };
            int puan = 0;
            foreach (var k in poz) if (metin.Contains(k)) puan++;
            foreach (var k in neg) if (metin.Contains(k)) puan--;
            return puan > 0 ? "Pozitif" : puan < 0 ? "Negatif" : "Nötr";
        }
    }
}
