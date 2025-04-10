# 📑 Vietnamese
# 📑 Document Management System

Ứng dụng quản lý chứng từ và chi tiết chứng từ, bao gồm:
- Thêm, sửa, xóa chứng từ
- Tìm kiếm theo số chứng từ, loại, ngày lập
- Quản lý chi tiết chứng từ (thu/chi, số tiền, tài khoản...)
- Giao diện frontend bằng React + Ant Design
- Backend sử dụng ASP.NET Core (CQRS + MediatR)

---

## ⚙️ Công nghệ sử dụng

### 🧠 Backend (.NET Core)
- ASP.NET Core Web API
- CQRS + MediatR
- Entity Framework Core
- MySQL (hoặc SQL Server tùy cấu hình)

### 🌐 Frontend (React)
- React + TypeScript
- Ant Design
- Axios

---

## 🚀 Hướng dẫn chạy dự án

### 📦 Cài đặt frontend

```bash
# Di chuyển vào thư mục frontend
cd frontend

# Cài dependencies
npm install

# Chạy ứng dụng
npm start

# Di chuyển vào thư mục backend
cd backend

# Restore packages
dotnet restore

# Chạy ứng dụng
dotnet run

API sẽ chạy tại: "https://localhost:7227/api"
```

# 📑 English
```bash
# Navigate to frontend folder
cd frontend

# Install dependencies
npm install

# Start development server
npm start

# Navigate to backend folder
cd backend

# Restore .NET packages
dotnet restore

# Start the application
dotnet run
The API will be available at: https://localhost:7227/api
