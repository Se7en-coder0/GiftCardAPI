# 🎁 GiftCardAPI

A .NET Core RESTful API for managing users, their addresses, gift cards, and transactions.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Microsoft SQL Server
- [Postman](https://www.postman.com/downloads/) (optional for testing)

### Setup

1. **Clone the repository**

2. **Configure the database connection**
   Update `appsettings.json` with your local SQL Server connection string.

3. **Run migrations**
```bash
dotnet ef database update
```

4. **Run the API**
```bash
dotnet run
```

5. Open Swagger UI at `https://localhost:5001/swagger` (adjust port if needed)

---

## 📬 Postman Collection

You can test the API using the provided Postman collection:  
📥 [GiftCardAPI_PostmanCollection.json](./GiftCardAPI_PostmanCollection.json)

Import it into Postman and make requests like:
- Create/Get Users
- Create/Get Gift Cards
- Create Addresses
- Create/Redeem Transactions

---

## 📁 Project Structure

- `Controllers/` – API endpoints
- `Services/` – Business logic
- `Repositories/` – Data access (Repository Pattern)
- `DTOs/` – Request/response models
- `Middleware/` – Exception handling
- `Logs/` – Serilog output

---

## ✅ Features

- Full CRUD for Users, Addresses, Gift Cards
- Transactions with redeem logic
- Validation via DataAnnotations
- Logging via Serilog to `Logs/log.txt`
- Clean architecture with repository + service pattern
- Swagger API docs

---

## ✍️ Author
Generated with ♥️ for a .NET developer assignment.
