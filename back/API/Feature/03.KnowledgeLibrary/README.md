# مستندات کامل فیچر کتابخانه دانش

این فیچر برای ثبت و جست‌وجوی دانش سازمانی ساخته شده است. هر رکورد دانش یک «عنوان مسئله»، یک موضوع از اطلاعات پایه `Subject`، حداقل یک تگ، وضعیت، اطلاعات اعتبار، ایجادکننده و تاریخ‌های ثبت و ویرایش دارد.

## مسیر درخواست

کلاینت درخواست HTTP را به آدرس `/api/knowledge` یا `/api/knowledge/search` می‌فرستد. FastEndpoints درخواست را به کلاس endpoint مربوط می‌رساند. endpoint داده را از request می‌گیرد، entity دامنه را می‌سازد یا تغییر می‌دهد و با `AppDbContext` در SQL Server ذخیره می‌کند. در جست‌وجو، entityها خوانده و به response قابل ارسال برای کلاینت تبدیل می‌شوند.

## ساختار پوشه

| مسیر | مسئولیت |
|---|---|
| `Domain/Entity.cs` | تعریف فیلدها، enum وضعیت، رابطه‌ها و تنظیمات EF Core |
| `Domain/Domain.cs` | قوانین ساخت و ویرایش دانش و اعتبارسنجی عنوان و تگ |
| `Endpoint/Create` | ایجاد دانش جدید |
| `Endpoint/Search` | فیلتر و نمایش دانش‌ها |
| `Endpoint/Update` | ویرایش دانش بدون تغییر مستقیم وضعیت |
| `Endpoint/Delete` | حذف دانش |
| `Endpoint/Review` | صف بررسی و تصمیم‌های تایید، اصلاح و رد |

## مدل داده

جدول `Knowledge` به `Subject` و `User` وصل است. تگ‌ها در جدول جداگانه `KnowledgeTag` ذخیره می‌شوند؛ بنابراین یک دانش می‌تواند یک تا چند تگ داشته باشد. در پاسخ API، این رابطه برای راحتی مصرف‌کننده به شکل `string[]` دیده می‌شود.

`CreatedByUserId` از کاربر جاری و claim به نام `UserId` خوانده می‌شود و از request دریافت نمی‌شود؛ این کار مانع ثبت دانش به نام کاربر دیگر می‌شود.

## وضعیت‌ها

| مقدار | معنی |
|---|---|
| `PendingReview` | در انتظار بررسی |
| `Approved` | تایید شده |
| `NeedsRevision` | نیازمند اصلاح |
| `Rejected` | رد شده |
| `EditedPendingReview` | ویرایش شده و در انتظار بررسی |
| `Expired` | منقضی شده |

در زمان ایجاد، وضعیت پیش‌فرض `PendingReview` است. تغییر وضعیت فقط از endpointهای بررسی انجام می‌شود.

## Endpointها

### ایجاد: `POST /api/knowledge`

نمونه body:

```json
{
  "problemTitle": "روش پشتیبان‌گیری از پایگاه داده چیست؟",
  "subjectId": 1,
  "tags": ["database", "backup"],
  "validityDate": "2027-01-01T00:00:00",
  "isPermanently": false
}
```

`tags` باید حداقل یک مقدار غیرخالی داشته باشد. تگ‌های خالی حذف و تگ‌های تکراری یکی می‌شوند.

### جست‌وجو: `GET /api/knowledge/search`

همه فیلدهای request اختیاری هستند؛ نمونه:

`/api/knowledge/search?problemTitle=backup&subjectId=1&tag=database&status=Approved&isPermanently=false`

فیلترهای تاریخ `validityFrom`، `validityTo`، `createdFrom`، `createdTo`، `updatedFrom` و `updatedTo` نیز پشتیبانی می‌شوند.

### ویرایش: `PUT /api/knowledge`

در body باید `id`، عنوان، موضوع، آرایه تگ‌ها، تاریخ اعتبار و `isPermanently` ارسال شود. وضعیت از body دریافت نمی‌شود و تاریخ `UpdateAt` خودکار تغییر می‌کند.

### صف بررسی: `GET /api/knowledge/review`

این endpoint به‌صورت پیش‌فرض فقط دانش‌های `PendingReview` و `EditedPendingReview` را برمی‌گرداند. با ارسال `status` می‌توان یکی از همین دو وضعیت را محدود کرد.

### تصمیم‌های بررسی

- `POST /api/knowledge/review/approve` با body شامل `id`
- `POST /api/knowledge/review/request-revision` با body شامل `id` و `reason`
- `POST /api/knowledge/review/reject` با body شامل `id` و `reason`

هر تصمیم در جدول `KnowledgeReviewHistory` ذخیره می‌شود.

### حذف: `DELETE /api/knowledge`

```json
{ "id": 10 }
```

با حذف دانش، تگ‌های وابسته نیز به‌دلیل رابطه cascade حذف می‌شوند.

نمونه پاسخ جست‌وجو:

```json
[
  {
    "id": 10,
    "problemTitle": "روش پشتیبان‌گیری از پایگاه داده چیست؟",
    "subjectId": 1,
    "subjectTitle": "زیرساخت",
    "tags": ["database", "backup"],
    "status": "Approved",
    "createdByUserId": 4,
    "createdByUserName": "کاربر سامانه",
    "validityDate": "2027-01-01T00:00:00",
    "isPermanently": false,
    "createdAt": "2026-09-13T20:00:00",
    "updateAt": "2026-09-13T20:00:00"
  }
]
```

## یک سناریوی ساده از ابتدا تا انتها

ابتدا کاربر یک `Subject` انتخاب می‌کند و عنوان و تگ‌ها را می‌فرستد. سرور شناسه کاربر واردشده را به‌عنوان ایجادکننده ثبت می‌کند و دانش را در وضعیت «در انتظار بررسی» ذخیره می‌کند. مدیر با صف review رکورد را پیدا می‌کند و با commandهای جداگانه آن را تایید، رد یا نیازمند اصلاح می‌کند. دانش تاییدشده از مسیر ویرایش عمومی قابل تغییر نیست؛ اگر دانش نیازمند اصلاح ویرایش شود، دوباره در وضعیت `EditedPendingReview` قرار می‌گیرد. اگر تاریخ اعتبار بگذرد، می‌توان وضعیت آن را `Expired` کرد.

## نکات مهم برای توسعه‌دهنده مبتدی

- `long` نوع عددی شناسه‌های دیتابیس است.
- `DateTime?` یعنی تاریخ می‌تواند خالی باشد؛ برای دانش دائمی معمولاً `validityDate` خالی است.
- `private set` یعنی بیرون از entity نمی‌توان مقدار را مستقیم تغییر داد؛ تغییر باید از متدهای دامنه انجام شود.
- `IQueryable` اجازه می‌دهد فیلترها قبل از اجرای query به SQL تبدیل شوند.
- `Include` رابطه‌های موضوع، کاربر و تگ‌ها را برای response بارگذاری می‌کند.
- migration فایل تغییر ساختار دیتابیس است و باید همراه تغییر entity نگهداری شود.
