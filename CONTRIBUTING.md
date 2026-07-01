# راهنمای مشارکت در پروژه FinalAssetManagement 🤝

از اینکه می‌خواهید در توسعه و بهبود این پروژه مشارکت کنید، بسیار خوشحالیم! برای حفظ یکپارچگی، کیفیت کدها و ساختار پروژه (Clean Architecture & .NET 10)، لطفاً قبل از شروع، این راهنما را به طور کامل مطالعه کنید.

---

## 🚀 نحوه راه‌اندازی پروژه در محیط محلی (Local Setup)

برای اجرای پروژه روی سیستم خود، مراحل زیر را دنبال کنید:

### پیش‌نیازها
*   **نصب .NET 10 SDK** (آخرین نسخه)
*   **IDE مناسب:** Visual Studio 2022 (v17.12 به بالا) یا JetBrains Rider یا VS Code (همراه با C# Dev Kit)
*   **پایگاه داده:** SQL Server یا PostgreSQL (با توجه به کانفیگ فایل `appsettings.json`)

### مراحل اجرا
۱. ابتدا پروژه را فورک (Fork) کنید و سپس آن را کلون کنید:
```bash
git clone https://github.com/YOUR_USERNAME/FinalAssetManagement.git
cd FinalAssetManagement
```

۲. وابستگی‌ها و پکیج‌های NuGet را بازیابی (Restore) کنید:
```bash
dotnet restore
```
۳. مایگریشن‌های دیتابیس را اعمال کنید (پروژه وب/API را به عنوان Startup انتخاب کنید):
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
```
۴. پروژه را در حالت Development اجرا کنید:
```bash
dotnet run --project src/WebAPI
```

🏗️ ساختار پروژه و اصول معماری (Clean Architecture)
پروژه بر اساس اصول معماری پیاز/تمیز (Clean Architecture) پیاده‌سازی شده است. هنگام اضافه کردن کدهای جدید حتماً قوانین زیر را رعایت کنید:
<div dir="rtl" align="right">

<p><span dir="rtl">• Domain:</span><br>
شامل Entityها، Value Objectها و اینترفیس‌های اصلی. هیچ وابستگی بیرونی نباید به این لایه اضافه شود.</p>

<p><span dir="rtl">• Application:</span><br>
شامل Use Caseها، CQRS (Commands/Queries)، سرویس‌های اپلیکیشن و DTOها. وابستگی فقط به لایه Domain مجاز است.</p>

<p><span dir="rtl">• Infrastructure:</span><br>
پیاده‌سازی‌های مربوط به EF Core، پایگاه داده، احراز هویت (JWT) و سرویس‌های بیرونی.</p>

<p><span dir="rtl">• WebAPI / Presentation:</span><br>
کنترلرها، روتینگ‌ها، کانفیگ‌های پایپلاین و Middlewares.</p>

🎨 استانداردهای کدنویسی (Code Style & Quality)


ما برای یکپارچگی کدها از استانداردهای رسمی سی‌شارپ و ابزار `dotnet format` استفاده می‌کنیم.


### ۱. فرمت‌دهی خودکار کدها
قبل از ثبت هر کامیت، دستور زیر را در ریشه پروژه اجرا کنید تا فرمت فایل‌ها اصلاح شود:

```bash
dotnet format
```

### ۲. قوانین نام‌گذاری
* **نام کلاس‌ها، متدها و پراپرتی‌ها:** به صورت `PascalCase` (مانند `GetAssetByIdQuery`)
* **نام متغیرهای محلی و پارامترها:** به صورت `camelCase` (مانند `assetId`)
* **فیلدهای Private در کلاس‌ها:** همراه با آندرلاین و `camelCase` (مانند `_assetRepository`)
* **اینترفیس‌ها:** شروع با حرف بزرگ `I` (مانند `IAssetRepository`)

---

🌿 قوانین گیت و کامیت‌ها (Git Hygiene)


برای تمیز ماندن تاریخچه گیت، موارد زیر را رعایت کنید:

### نام‌گذاری شاخه‌ها (Branches):
* **برای ویژگی جدید:** `feature/feature-name`
* **برای رفع باگ:** `bugfix/bug-name`
* **برای مستندات:** `docs/docs-name`

### فرمت پیام‌های کامیت (Commit Messages):
سعی کنید پیام‌های کامیت کوتاه، واضح و ترجیحاً به زبان انگلیسی باشند. مثال:
```text
feat: add JWT authentication middleware
fix: resolve asset routing issue in controller
docs: update contributing guide
```

---

📥 فرآیند ثبت Pull Request (PR)

۱. یک شاخه جدید از روی شاخه اصلی (`main`) بگیرید.

۲. تغییرات خود را اعمال کنید و مطمئن شوید که پروژه بدون خطا کامپایل می‌شود:
```bash
dotnet build
```

۳. تست‌های واحد (Unit Tests) را اجرا کنید و مطمئن شوید همگی پاس می‌شوند:
```bash
dotnet test
```

۴. ابزار فرمت‌ساز را اجرا کنید:
```bash
dotnet format
```

۵. تغییرات خود را Push کنید و یک Pull Request به شاخه `main` پروژه اصلی ثبت کنید.

۶. در توضیحات PR، الگوی Pull Request را به طور کامل پر کنید و مشخص کنید چه باگی رفع شده یا چه قابلیتی اضافه شده است.

---
از مشارکت شما در بهتر شدن **FinalAssetManagement** سپاسگزاریم! ❤️
```

