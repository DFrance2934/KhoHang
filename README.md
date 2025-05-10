# 💼 Sales Warehouse Management - Basic Inventory Software

The "Sales Warehouse" system is a Windows Forms (WinForms) application built with C# and SQL Server. It supports managing inventory, suppliers, customers, and point accumulation for small and medium-sized businesses.

---

## 🎯 Project Goals

- Provide a simple, easy-to-deploy software solution
- Help sellers manage product, supplier, and customer information
- Automate ID generation, point tracking, and default status handling
- Serve as a foundation for learning or building larger systems

---

## 🧰 Technologies Used

| Component        | Technology                     |
|------------------|-------------------------------|
| UI               | C# WinForms                   |
| Database         | Microsoft SQL Server 2019     |
| Architecture     | 3-layer (Presentation, Business, Data) |
| IDE              | Visual Studio 2022            |

---

## 🚀 Main Features

### 1. Supplier Management
- Automatically generates supplier codes like `NCC0001`
- Default status set to "Active" for new suppliers
- Search by name, address, or phone number

### 2. Customer Management
- Auto-generate customer codes like `KH0001`
- Initial point accumulation set to 0
- Points can be updated after transactions (extendable)

### 3. Product Management
- Store item info: code, name, price, stock quantity
- Add, edit, and delete products

### 4. Reports and Search
- Quick filtering and listing
- Summary report of customers by points

---

## 🗂️ Project Folder Structure

```bash
KHO_BANHANG/
├── Presentation/        # UI Forms
├── Business/            # Business logic layer
├── DataAccess/          # Data access and SQL interaction
├── DTO/                 # Transfer object classes
├── App.config           # Database connection string
└── KhoBanHang.sql       # SQL script to create the database
