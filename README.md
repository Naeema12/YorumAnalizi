#  Restaurant & Hotel Review Analysis System

A **C# Windows Forms** desktop application that analyzes restaurant and hotel reviews using real AI-powered sentiment analysis. The system lets users label reviews themselves and compares their judgment against a live **HuggingFace** sentiment model, storing everything in a local **SQLite** database for full traceability.

---

##  Screenshots
<img width="1226" height="908" alt="Screenshot 2026-06-21 182527" src="https://github.com/user-attachments/assets/7563fe5b-2133-47f1-8c45-1a63f8b2e588" />

<img width="1150" height="833" alt="Screenshot 2026-06-01 225754" src="https://github.com/user-attachments/assets/1b202aee-ae39-4e71-b2f3-9c1c11f92504" />



---

##  Features

- **Real AI Sentiment Analysis** — Calls the HuggingFace Inference API (`cardiffnlp/twitter-roberta-base-sentiment-latest`) to classify each review as Positive, Neutral, or Negative
- **Human vs AI Comparison** — Users manually label each review; the app calls the API in parallel and shows whether the two labels match
- **Topic Classification** — Categorizes reviews by topic: Taste, Service, Cleanliness/Room, Price, Atmosphere, General
- **Persistent SQLite Database** — Every review and every AI request (input, output, latency, timestamp) is saved locally and survives app restarts
- **Automatic Fallback Engine** — If the API is unreachable, the app transparently switches to a rule-based keyword engine so the workflow never breaks
- **Color-coded Table** — Green for Positive, Red for Negative, Yellow for Neutral
- **Filtering** — Filter by sentiment or match status (All / Positive / Neutral / Negative / Matched / Different)
- **Report Screen** — Pie chart, bar chart, statistics cards and an AI accuracy progress bar, all drawn with GDI+
- **CSV Import/Export** — Save and load review data as CSV files for quick bulk testing
- **In-App API Key Management** — Set or update your HuggingFace token directly from the UI; the key is stored outside the source code
- **Delete Reviews** — Remove selected reviews with a confirmation dialog
- **Real-time Statistics** — Live accuracy rate displayed in the status bar

---

##  Technologies Used

| Technology | Purpose |
|---|---|
| C# | Programming language |
| Windows Forms (.NET Framework 4.7.2) | Desktop UI framework |
| HuggingFace Inference API | Real-time sentiment classification |
| SQLite (System.Data.SQLite) | Local persistent database |
| HttpClient | Async REST calls to the AI model |
| GDI+ (System.Drawing) | Chart rendering (pie & bar charts) |
| CSV | Data import/export |

---

##  AI Model

The sentiment engine calls the HuggingFace-hosted model:

**`cardiffnlp/twitter-roberta-base-sentiment-latest`** — a RoBERTa model fine-tuned on Twitter data for 3-class sentiment classification (positive / neutral / negative).

**Request:**
```json
{ "inputs": "The food was absolutely delicious, highly recommend" }
```

**Response:**
```json
[[
  { "label": "positive", "score": 0.9812 },
  { "label": "neutral",  "score": 0.0134 },
  { "label": "negative", "score": 0.0054 }
]]
```

The label with the highest score is selected and mapped to Turkish (`positive → Pozitif`, `neutral → Nötr`, `negative → Negatif`) for display.

### Fallback engine
If the API call fails (no internet, rate limit, model loading), the app automatically falls back to a local rule-based keyword scorer so the user never sees a broken workflow:

```csharp
private string DuyguAnaliz(string metin)
{
    string[] positive = { "delicious", "amazing", "friendly", "clean", "superb", ... };
    string[] negative = { "terrible", "slow", "dirty", "expensive", "awful", ... };

    int score = 0;
    foreach (var k in positive) if (metin.Contains(k)) score++;
    foreach (var k in negative) if (metin.Contains(k)) score--;

    return score > 0 ? "Pozitif" : score < 0 ? "Negatif" : "Nötr";
}
```

---

##  Database Schema (SQLite)

The database file `yorumlar.db` is created automatically on first run.

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

### AIRequests (AI Call Log)
| Column | Type | Description |
|---|---|---|
| Id | INTEGER PK | Auto-incremented ID |
| YorumId | INTEGER FK | References Yorumlar.Id |
| Girdi | TEXT | Input text sent to the API |
| Cikti | TEXT | Label returned by the API |
| IslemSuresi | TEXT | Processing time (ms) |
| ZamanDamgasi | TEXT | Request timestamp |

This second table means every single API call is fully traceable — what was sent, what came back, and how long it took.

---

##  Getting Started

### Prerequisites
- Windows 7 or later
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- A free [HuggingFace](https://huggingface.co/) account and access token
- Internet connection (for live API calls)



### Load Sample Data
- Click **CSV Yükle**
- Select `sample_reviews.csv`
- sample reviews will be loaded automatically

---

##  Sample Dataset & Results

`sample_reviews_english.csv` contains **50 labeled reviews** across all topic categories. Running them through the live HuggingFace model produced:

| Metric | Result |
|---|---|
| Overall agreement (Human vs AI) | ~90% |
| Positive accuracy | ~96% |
| Negative accuracy | ~94% |
| Neutral accuracy | ~71% |
| Average API latency | ~850 ms |

Almost all mismatches occurred on **Neutral** reviews with mixed or contextual phrasing (e.g. *"Not bad for the price"*, *"It was okay"*) — a known weak spot for sentiment models trained mainly on clearly polarized text.

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
├── Form1.cs              # Main screen logic (UI events, filtering, CSV, async API calls)
├── Form1.Designer.cs     # Main screen UI layout
├── FormRapor.cs          # Report screen with GDI+ charts
├── Database.cs           # SQLite setup and CRUD operations
├── HuggingFaceAPI.cs     # HTTP client, JSON parsing, API key management
├── YorumData.cs          # Data model class
├── Program.cs            # Application entry point
├── YorumAnalizi.csproj   # Project file
└── sample_reviews_english.csv  # Sample dataset (50 reviews)
```

---

##  Security Note

The HuggingFace API key is **never hardcoded** in the source. It is entered through the app's UI and stored in a local `api_key.txt` file, which is excluded from version control via `.gitignore`. Anyone cloning this repo needs to provide their own token via the **API Ayarı** button.

---

##  Project Evolution

This project grew in three stages, which is reflected in the codebase:

1. **v1 — Rule-based engine:** Started as a simple Windows Forms app with a keyword-scoring sentiment classifier and in-memory storage.
2. **v2 — Persistence:** Added a SQLite database so reviews survive app restarts, plus an `AIRequests` log table for traceability.
3. **v3 — Real AI integration:** Replaced/augmented the rule-based engine with a live call to a HuggingFace transformer model, keeping the original engine as an automatic fallback.

---

##  Course Information

- **University:** Karamanoğlu Mehmetbey University
- **Department:** Computer Engineering
- **Course:** Advanced Visual Programming
- **Instructor:** Dr. Öğr. Üyesi Hüseyin ELDEM
- **Assistant:** Araş. Gör. İlya KUŞ

---

##  License

This project is developed for educational purposes as part of a university course project.
