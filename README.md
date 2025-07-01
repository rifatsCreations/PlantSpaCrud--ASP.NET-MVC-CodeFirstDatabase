🌿 Plant Spa CRUD (ASP.NET MVC)
A simple web app to manage plants, categories, and care types using ASP.NET MVC and Entity Framework (Code First).

🔧 Features
Add, edit, delete plants

Assign category and care types

Upload plant image (default if missing)

Partial views + AJAX for smooth UI

Responsive navbar (Home, Plant List)

🛠️ Tech Stack
ASP.NET MVC 5

Entity Framework 6 (Code First)

SQL Server

Bootstrap + jQuery

🖼️ Layout Highlights
_Layout.cshtml uses Bootstrap navbar

Custom hover styles on nav links

Uses @RenderBody() and @RenderSection()

📂 Image Handling
Uploaded to /images/ folder

Unique filename via GUID

Defaults to noimage.png

🧩 Notes
Create /images/ folder manually

Run Add-Migration → Update-Database

Uses AJAX and modals for forms
