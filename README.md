# 🖼️ GalleryProject

**GalleryProject** is a backend API built with **ASP.NET Core**, designed for managing image galleries. Users can upload photos, organize them into folders, tag them, and perform full CRUD operations. All features are protected by **JWT-based authentication** to ensure security and user privacy.

---

## 🚀 Technologies Used

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **MySQL**
- **JWT Authentication**
- **AutoMapper**
- **Dependency Injection**
- **wwwroot File Storage** (images are stored physically in the `wwwroot` folder)

---

## ⚙️ Main Features

| Feature | Description |
|----------|-------------|
| 🔐 **JWT Authentication** | Secure user login and access control using JSON Web Tokens |
| 📸 **Image Upload** | Upload and store images in the `wwwroot/uploads` directory |
| 🗂️ **Folder Management** | Create and organize image folders |
| 🏷️ **Tag Management** | Add, update, or remove tags for uploaded images |
| 🗑️ **Delete Functionality** | Delete photos, folders, or tags easily |
| 🧾 **Metadata Handling** | Store and retrieve photo details such as name, date, and tags |

---

⚙️ How to Run the Project

```bash
git clone https://github.com/yourusername/GalleryProject.git
cd GalleryProject
