#  Restaurant & Hotel Review Analysis System

A **C# Windows Forms** desktop application that analyzes restaurant and hotel reviews using sentiment analysis. The system compares **human labels** with **AI-generated labels** and provides detailed statistics and visualizations.

---

##  Screenshots

<img width="1249" height="913" alt="Screenshot 2026-06-01 225606" src="https://github.com/user-attachments/assets/ba0d777f-5f9c-4d6b-94b7-c8afbde2f48c" />
<img width="1150" height="833" alt="Screenshot 2026-06-01 225754" src="https://github.com/user-attachments/assets/4aa191ee-7392-478d-8aee-f5edf650963d" />




---

##  Features

- **Sentiment Analysis** — Automatically classifies reviews as Positive, Neutral, or Negative using a rule-based NLP engine
- **Human vs AI Comparison** — Users manually label each review; the system compares it with the AI label and shows match/mismatch
- **Topic Classification** — Categorizes reviews by topic: Taste, Service, Cleanliness/Room, Price, Atmosphere, General
- **Color-coded Table** — Green for Positive, Red for Negative, Yellow for Neutral
- **Filtering** — Filter by sentiment or match status (All / Positive / Neutral / Negative / Matched / Different)
- **Report Screen** — Pie chart, bar chart, statistics cards and AI accuracy progress bar
- **CSV Import/Export** — Save and load review data as CSV files
- **SQLite Database** — All reviews and AI request logs are stored persistently in a local database
- **Delete Reviews** — Remove selected reviews with confirmation dialog
- **Real-time Statistics** — Live accuracy rate displayed in the status bar

---

##  Technologies Used

| Technology | Purpose |
|---|---|
| C# | Programming language |
| Windows Forms (.NET Framework 4.7.2) | Desktop UI framework |
| SQLite (System.Data.SQLite) | Local database |
| GDI+ (System.Drawing) | Chart rendering (pie & bar charts) |
| CSV | Data import/export |

---

##  Database Schema

### Yorumlar (Reviews)
| Column | Type | Description |
|---|---|---|
| Id | INTEGER PK | Auto-incremented ID |
| Metin | TEXT | Review text |
| InsanDuygu | TEXT | Human label (Positive/Neutral/Negative) |
| AIDuygu | TEXT | AI-generated label |
| Esleme | TEXT | Match result (Matched/Different) |
| Konu | TEXT | Topic category |
| Tarih | TEXT | Timestamp |

### AIRequests (AI Log)
| Column | Type | Description |
|---|---|---|
| Id | INTEGER PK | Auto-incremented ID |
| YorumId | INTEGER FK | References Yorumlar.Id |
| Girdi | TEXT | Input text sent to AI |
| Cikti | TEXT | AI output label |
| IslemSuresi | TEXT | Processing time (ms) |
| ZamanDamgasi | TEXT | Request timestamp |

---

##  AI Algorithm

The sentiment analysis engine uses a **rule-based keyword scoring** approach:

```csharp
private string DuyguAnaliz(string metin)
{
    string[] positive = { "excellent", "amazing", "delicious", "clean", "friendly", ... };
    string[] negative = { "terrible", "slow", "dirty", "expensive", "awful", ... };
    
    int score = 0;
    foreach (var k in positive) if (metin.Contains(k)) score++;
    foreach (var k in negative) if (metin.Contains(k)) score--;
    
    return score > 0 ? "Positive" : score < 0 ? "Negative" : "Neutral";
}
```

- Score **> 0** → Positive
- Score **= 0** → Neutral  
- Score **< 0** → Negative

---


##  Sample Dataset

The repository includes `sample_reviews_english.csv` with **50 labeled reviews** covering all topic categories:

| Topic | Count |
|---|---|
| Taste (Lezzet) | 10 |
| Service (Servis) | 12 |
| Cleanliness/Room (Temizlik/Oda) | 10 |
| Price (Fiyat) | 8 |
| Atmosphere (Atmosfer) | 6 |
| General (Genel) | 4 |

---

##  Project Structure

```
YorumAnalizi/
│
├── Form1.cs              # Main screen logic
├── Form1.Designer.cs     # Main screen UI layout
├── FormRapor.cs          # Report screen with charts
├── Database.cs           # SQLite database operations
├── YorumData.cs          # Data model class
├── Program.cs            # Application entry point
├── YorumAnalizi.csproj   # Project file
└── sample_reviews_english.csv  # Sample dataset (50 reviews)
```



