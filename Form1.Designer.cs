namespace YorumAnalizi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader     = new System.Windows.Forms.Panel();
            this.lblBaslik     = new System.Windows.Forms.Label();
            this.pnlGiris      = new System.Windows.Forms.Panel();
            this.lblYorum      = new System.Windows.Forms.Label();
            this.txtYorum      = new System.Windows.Forms.TextBox();
            this.lblDuygu      = new System.Windows.Forms.Label();
            this.rbPozitif     = new System.Windows.Forms.RadioButton();
            this.rbNotr        = new System.Windows.Forms.RadioButton();
            this.rbNegatif     = new System.Windows.Forms.RadioButton();
            this.lblKonu       = new System.Windows.Forms.Label();
            this.cmbKonu       = new System.Windows.Forms.ComboBox();
            this.btnEkle       = new System.Windows.Forms.Button();
            this.pnlAraclar    = new System.Windows.Forms.Panel();
            this.lblFiltre     = new System.Windows.Forms.Label();
            this.cmbFiltre     = new System.Windows.Forms.ComboBox();
            this.btnSil        = new System.Windows.Forms.Button();
            this.btnKaydet     = new System.Windows.Forms.Button();
            this.btnYukle      = new System.Windows.Forms.Button();
            this.btnRapor      = new System.Windows.Forms.Button();
            this.lvYorumlar    = new System.Windows.Forms.ListView();
            this.colYorum      = new System.Windows.Forms.ColumnHeader();
            this.colInsan      = new System.Windows.Forms.ColumnHeader();
            this.colAI         = new System.Windows.Forms.ColumnHeader();
            this.colEsleme     = new System.Windows.Forms.ColumnHeader();
            this.colKonu       = new System.Windows.Forms.ColumnHeader();
            this.colTarih      = new System.Windows.Forms.ColumnHeader();
            this.lblIstatistik = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlGiris.SuspendLayout();
            this.pnlAraclar.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.Text = "Yorum Analiz Sistemi";
            this.Size = new System.Drawing.Size(980, 700);
            this.MinimumSize = new System.Drawing.Size(980, 700);
            this.MaximumSize = new System.Drawing.Size(980, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.Controls.Add(this.lblIstatistik);
            this.Controls.Add(this.lvYorumlar);
            this.Controls.Add(this.pnlAraclar);
            this.Controls.Add(this.pnlGiris);
            this.Controls.Add(this.pnlHeader);

            // HEADER
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 52;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(70, 110, 160);
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.lblBaslik.Text = "Yorum Analiz Sistemi";
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // GİRİŞ PANELİ
            this.pnlGiris.Location = new System.Drawing.Point(0, 52);
            this.pnlGiris.Size = new System.Drawing.Size(980, 185);
            this.pnlGiris.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.pnlGiris.Controls.Add(this.lblYorum);
            this.pnlGiris.Controls.Add(this.txtYorum);
            this.pnlGiris.Controls.Add(this.lblDuygu);
            this.pnlGiris.Controls.Add(this.rbPozitif);
            this.pnlGiris.Controls.Add(this.rbNotr);
            this.pnlGiris.Controls.Add(this.rbNegatif);
            this.pnlGiris.Controls.Add(this.lblKonu);
            this.pnlGiris.Controls.Add(this.cmbKonu);
            this.pnlGiris.Controls.Add(this.btnEkle);

            this.lblYorum.Text = "Yorum:";
            this.lblYorum.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblYorum.Location = new System.Drawing.Point(20, 10);
            this.lblYorum.Size = new System.Drawing.Size(60, 18);

            this.txtYorum.Location = new System.Drawing.Point(20, 30);
            this.txtYorum.Size = new System.Drawing.Size(930, 75);
            this.txtYorum.Multiline = true;
            this.txtYorum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtYorum.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.txtYorum.BackColor = System.Drawing.Color.White;
            this.txtYorum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYorum.PlaceholderText = "Yorumunuzu buraya yazın...";

            this.lblDuygu.Text = "Duygu (İnsan):";
            this.lblDuygu.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblDuygu.Location = new System.Drawing.Point(20, 118);
            this.lblDuygu.Size = new System.Drawing.Size(105, 18);

            this.rbPozitif.Text = "Pozitif";
            this.rbPozitif.Location = new System.Drawing.Point(130, 116);
            this.rbPozitif.Size = new System.Drawing.Size(80, 22);
            this.rbPozitif.ForeColor = System.Drawing.Color.Green;
            this.rbPozitif.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.rbNotr.Text = "Nötr";
            this.rbNotr.Location = new System.Drawing.Point(215, 116);
            this.rbNotr.Size = new System.Drawing.Size(65, 22);
            this.rbNotr.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.rbNotr.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.rbNegatif.Text = "Negatif";
            this.rbNegatif.Location = new System.Drawing.Point(285, 116);
            this.rbNegatif.Size = new System.Drawing.Size(75, 22);
            this.rbNegatif.ForeColor = System.Drawing.Color.Red;
            this.rbNegatif.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.lblKonu.Text = "Konu:";
            this.lblKonu.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblKonu.Location = new System.Drawing.Point(20, 150);
            this.lblKonu.Size = new System.Drawing.Size(50, 18);

            this.cmbKonu.Location = new System.Drawing.Point(75, 147);
            this.cmbKonu.Size = new System.Drawing.Size(740, 26);
            this.cmbKonu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKonu.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.cmbKonu.BackColor = System.Drawing.Color.White;
            this.cmbKonu.Items.Add("-- Konu Seçiniz --");
            this.cmbKonu.Items.AddRange(new object[] { "Lezzet", "Servis", "Temizlik/Oda", "Fiyat", "Atmosfer", "Genel" });
            this.cmbKonu.SelectedIndex = 0;

            this.btnEkle.Text = "Yorum Ekle";
            this.btnEkle.Location = new System.Drawing.Point(825, 145);
            this.btnEkle.Size = new System.Drawing.Size(125, 32);
            this.btnEkle.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEkle.FlatAppearance.BorderSize = 0;
            this.btnEkle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);

            // ARAÇLAR PANELİ
            this.pnlAraclar.Location = new System.Drawing.Point(0, 237);
            this.pnlAraclar.Size = new System.Drawing.Size(980, 40);
            this.pnlAraclar.BackColor = System.Drawing.Color.FromArgb(225, 228, 232);
            this.pnlAraclar.Controls.Add(this.lblFiltre);
            this.pnlAraclar.Controls.Add(this.cmbFiltre);
            this.pnlAraclar.Controls.Add(this.btnSil);
            this.pnlAraclar.Controls.Add(this.btnKaydet);
            this.pnlAraclar.Controls.Add(this.btnYukle);
            this.pnlAraclar.Controls.Add(this.btnRapor);

            this.lblFiltre.Text = "Filtre:";
            this.lblFiltre.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblFiltre.Location = new System.Drawing.Point(10, 11);
            this.lblFiltre.Size = new System.Drawing.Size(45, 18);

            this.cmbFiltre.Location = new System.Drawing.Point(55, 8);
            this.cmbFiltre.Size = new System.Drawing.Size(155, 24);
            this.cmbFiltre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltre.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.cmbFiltre.Items.AddRange(new object[] { "Tümü", "Pozitif", "Nötr", "Negatif", "Eşleşti", "Farklı" });
            this.cmbFiltre.SelectedIndex = 0;
            this.cmbFiltre.SelectedIndexChanged += new System.EventHandler(this.cmbFiltre_SelectedIndexChanged);

            this.btnSil.Text = "Seçiliyi Sil";
            this.btnSil.Location = new System.Drawing.Point(224, 7);
            this.btnSil.Size = new System.Drawing.Size(105, 28);
            this.btnSil.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.FlatAppearance.BorderSize = 0;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);

            this.btnKaydet.Text = "CSV Kaydet";
            this.btnKaydet.Location = new System.Drawing.Point(340, 7);
            this.btnKaydet.Size = new System.Drawing.Size(105, 28);
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(26, 58, 107);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            this.btnYukle.Text = "CSV Yükle";
            this.btnYukle.Location = new System.Drawing.Point(458, 7);
            this.btnYukle.Size = new System.Drawing.Size(105, 28);
            this.btnYukle.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnYukle.ForeColor = System.Drawing.Color.White;
            this.btnYukle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYukle.FlatAppearance.BorderSize = 0;
            this.btnYukle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnYukle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYukle.Click += new System.EventHandler(this.btnYukle_Click);

            this.btnRapor.Text = "📊 Rapor Aç";
            this.btnRapor.Location = new System.Drawing.Point(576, 7);
            this.btnRapor.Size = new System.Drawing.Size(115, 28);
            this.btnRapor.BackColor = System.Drawing.Color.FromArgb(111, 66, 193);
            this.btnRapor.ForeColor = System.Drawing.Color.White;
            this.btnRapor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRapor.FlatAppearance.BorderSize = 0;
            this.btnRapor.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnRapor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRapor.Click += new System.EventHandler(this.btnRapor_Click);

            // LİST VIEW
            this.lvYorumlar.Location = new System.Drawing.Point(10, 282);
            this.lvYorumlar.Size = new System.Drawing.Size(958, 358);
            this.lvYorumlar.View = System.Windows.Forms.View.Details;
            this.lvYorumlar.FullRowSelect = true;
            this.lvYorumlar.GridLines = true;
            this.lvYorumlar.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lvYorumlar.BackColor = System.Drawing.Color.White;
            this.lvYorumlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvYorumlar.Columns.Add(this.colYorum);
            this.lvYorumlar.Columns.Add(this.colInsan);
            this.lvYorumlar.Columns.Add(this.colAI);
            this.lvYorumlar.Columns.Add(this.colEsleme);
            this.lvYorumlar.Columns.Add(this.colKonu);
            this.lvYorumlar.Columns.Add(this.colTarih);

            this.colYorum.Text  = "Yorum";        this.colYorum.Width  = 355;
            this.colInsan.Text  = "İnsan Duygu";  this.colInsan.Width  = 105;
            this.colAI.Text     = "AI Duygu";     this.colAI.Width     = 95;
            this.colEsleme.Text = "Eşleşme";      this.colEsleme.Width = 90;
            this.colKonu.Text   = "Konu";         this.colKonu.Width   = 120;
            this.colTarih.Text  = "Tarih";        this.colTarih.Width  = 140;

            // İSTATİSTİK
            this.lblIstatistik.Location = new System.Drawing.Point(10, 645);
            this.lblIstatistik.Size = new System.Drawing.Size(958, 22);
            this.lblIstatistik.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblIstatistik.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblIstatistik.BackColor = System.Drawing.Color.FromArgb(225, 228, 232);
            this.lblIstatistik.Text = "Henüz yorum eklenmedi.";
            this.lblIstatistik.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIstatistik.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);

            this.pnlHeader.ResumeLayout(false);
            this.pnlGiris.ResumeLayout(false);
            this.pnlAraclar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlGiris;
        private System.Windows.Forms.Label lblYorum;
        private System.Windows.Forms.TextBox txtYorum;
        private System.Windows.Forms.Label lblDuygu;
        private System.Windows.Forms.RadioButton rbPozitif;
        private System.Windows.Forms.RadioButton rbNotr;
        private System.Windows.Forms.RadioButton rbNegatif;
        private System.Windows.Forms.Label lblKonu;
        private System.Windows.Forms.ComboBox cmbKonu;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Panel pnlAraclar;
        private System.Windows.Forms.Label lblFiltre;
        private System.Windows.Forms.ComboBox cmbFiltre;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnYukle;
        private System.Windows.Forms.Button btnRapor;
        private System.Windows.Forms.ListView lvYorumlar;
        private System.Windows.Forms.ColumnHeader colYorum;
        private System.Windows.Forms.ColumnHeader colInsan;
        private System.Windows.Forms.ColumnHeader colAI;
        private System.Windows.Forms.ColumnHeader colEsleme;
        private System.Windows.Forms.ColumnHeader colKonu;
        private System.Windows.Forms.ColumnHeader colTarih;
        private System.Windows.Forms.Label lblIstatistik;
    }
}
