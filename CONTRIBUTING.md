# راهنمای مشارکت (Contributing Guide) 🚀

از اینکه می‌خواهید در توسعه **FinalAssetManagement** مشارکت کنید، بسیار خوشحالیم! این پروژه با هدف مدیریت دارایی‌ها و با استفاده از آخرین استانداردهای .NET توسعه داده شده است.

## 💻 نحوه راه‌اندازی پروژه در محیط محلی
برای اجرای پروژه روی سیستم خود، مراحل زیر را دنبال کنید:

1. مخزن را کلون کنید:
   `git clone https://github.com/ensiehyousefi14/FinalAssetManagement.git`
2. به پوشه پروژه بروید:
   `cd FinalAssetManagement`
3. وابستگی‌ها را بازیابی کنید:
   `dotnet restore`
4. پایگاه داده را به‌روزرسانی کنید:
   `dotnet ef database update --project FinalAssetManagement.Infrastructure --startup-project FinalAssetManagement.WebAPI`
5. پروژه را اجرا کنید:
   `dotnet run --project FinalAssetManagement.WebAPI`

## 🛠 استانداردهای کدنویسی
لطفاً هنگام ارسال کد، موارد زیر را رعایت کنید:
- **Clean Architecture:** کدها باید در لایه‌های مربوطه (Domain, Application, Infrastructure, WebAPI) قرار گیرند.
- **Naming:** از استاندارد `PascalCase` برای کلاس‌ها و متدها استفاده کنید.
- **Formatting:** قبل از کامیت، کدها را با دستور `dotnet format` مرتب کنید.
- **CI/CD:** مطمئن شوید که تغییرات شما باعث قرمز شدن Build در GitHub Actions نمی‌شود.

## 🌿 قوانین شاخه‌ها (Branching)
- شاخه اصلی `MainBranch` فقط برای نسخه‌های پایدار است.
- برای ویژگی‌های جدید از شاخه `feature/feature-name` استفاده کنید.
- برای رفع باگ از شاخه `fix/bug-name` استفاده کنید.

## 📬 ارسال Pull Request
1. یک Branch جدید بسازید.
2. تغییرات خود را اعمال و کامیت کنید.
3. به مخزن اصلی یک Pull Request بزنید.
4. منتظر بررسی و تایید CI بمانید.
