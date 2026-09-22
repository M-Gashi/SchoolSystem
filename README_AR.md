# School System

تطبيق مكتبي لإدارة بيانات المدرسة ومتابعة الطلاب والمعلمين والبنية التعليمية، مبني باستخدام Windows Forms وطبقات منفصلة لواجهة المستخدم ومنطق الأعمال والوصول إلى البيانات.

لنسخة العرض الموجهة للتوظيف وGitHub، راجع [دراسة حالة المشروع](PORTFOLIO.md).

## الوظائف الحالية

- إدارة الأشخاص والطلاب والمعلمين.
- إدارة المراحل والصفوف والشُّعب والمواد الدراسية.
- ربط الطلاب بالمواد الدراسية.
- متابعة أجزاء وصفحات القرآن وتقدّم الطلاب فيها.
- تسجيل أخطاء التلاوة ومراجعة التقدّم.

## بنية المشروع

| المشروع | المسؤولية |
| --- | --- |
| `SchoolSystem.Presentation` | واجهة المستخدم المبنية بـ Windows Forms |
| `SchoolSystem.Business` | منطق الأعمال والتحقق من العمليات |
| `SchoolSystem.Data` | الوصول إلى SQL Server باستخدام ADO.NET |
| `SchoolSystem.Enums` | التعدادات والأنواع المشتركة بين الطبقات |
| `SchoolSystem.Tests` | اختبارات وحدة لمنطق الأعمال الذي لا يعتمد على قاعدة البيانات |

## التنظيم حسب المجال

تُجمع الملفات داخل كل طبقة بحسب مجال العمل بدل توزيعها في مجلدات عامة. المجالات الرئيسية هي:

| المجال | أمثلة على المحتوى |
| --- | --- |
| `People` | الأشخاص والدول والبيانات الأساسية |
| `Students` | بيانات الطلاب وشاشاتهم |
| `Teachers` | بيانات المعلمين وشاشاتهم |
| `AcademicStructure` | المراحل والصفوف والشُّعب والشجرة التعليمية |
| `Subjects` | المواد الدراسية وربط الطلاب بها |
| `Quran` | الصفحات والأجزاء والمسارات وتقدّم الطلاب |

يظهر المجال نفسه عبر الطبقات عند الحاجة؛ فمثلًا توجد واجهات الطلاب في `SchoolSystem.Presentation/Students`، ومنطقهم في `SchoolSystem.Business/Students`، والوصول إلى بياناتهم في `SchoolSystem.Data/Students`.

## المتطلبات

- Windows 10 أو أحدث.
- Visual Studio 2022 مع حزمة **.NET desktop development**.
- .NET Framework 4.8 Developer Pack.
- Microsoft SQL Server.

## إعداد قاعدة البيانات

يتوقع التطبيق وجود قاعدة بيانات باسم `SchoolSystemDatabase` على SQL Server المحلي.

إعداد الاتصال موجود في:

```text
SchoolSystem.Presentation/App.config
```

القيمة الافتراضية تستخدم Windows Authentication:

```xml
<add name="SchoolSystemDatabase"
     connectionString="Data Source=.;Initial Catalog=SchoolSystemDatabase;Integrated Security=True;TrustServerCertificate=True;"
     providerName="System.Data.SqlClient" />
```

يمكن تعديل `Data Source` ليتوافق مع اسم خادم SQL Server لديك. لا تضع اسم مستخدم أو كلمة مرور حقيقية داخل المستودع؛ استخدم إعدادًا محليًا آمنًا عند الحاجة.

يمكن إنشاء مخطط قاعدة جديد بتنفيذ الملف:

```text
Database/Scripts/001_CreateSchema.sql
```

توجد تعليمات الإعداد وملاحظات البيانات المرجعية في `Database/README.md`. لا يتضمن السكربت بيانات الأشخاص أو الطلاب أو المعلمين.

## التشغيل

1. افتح الحل `SchoolSystem.Presentation/SchoolSystem.sln` باستخدام Visual Studio.
2. تأكد من توفر قاعدة البيانات ومن صحة سلسلة الاتصال في `SchoolSystem.Presentation/App.config`.
3. اجعل `SchoolSystem.Presentation` هو Startup Project.
4. ابنِ الحل باستخدام **Build > Build Solution**.
5. شغّل التطبيق بالضغط على `F5`.

يمكن بناء الحل من Developer PowerShell for Visual Studio أيضًا:

```powershell
msbuild .\SchoolSystem.Presentation\SchoolSystem.sln /p:Configuration=Debug
```

## الاختبارات

يحتوي `SchoolSystem.Tests` على اختبارات MSTest تستهدف:

- القيم الافتراضية لعقد شجرة المواد والهيكل التعليمي.
- إنشاء العقد من البيانات وإضافة الأبناء.
- استخراج رقم صفحة القرآن من اسم ملف الصورة.

لا تتصل هذه الاختبارات بقاعدة البيانات ولا تغيّر أي بيانات. لتشغيلها من Visual Studio افتح **Test Explorer** ثم اختر **Run All Tests**. ويمكن تشغيلها من Developer PowerShell بعد بناء الحل:

```powershell
vstest.console .\SchoolSystem.Tests\bin\Debug\SchoolSystem.Tests.dll
```

## الأصول المحلية

- يحتوي `Image` على الصور المستخدمة في القوائم الرئيسية. ينسخها المشروع تلقائيًا إلى مجلد `Image` بجوار الملف التنفيذي عند البناء.
- يحتوي `Quran` على صور صفحات القرآن المستخدمة في المزامنة والعرض. لا تُنسخ هذه الصور إلى مجلد البناء بسبب حجمها.

عند مزامنة صفحات القرآن يبحث التطبيق بالترتيب عن:

1. المجلد الذي اختاره المستخدم وحُفظ في الإعداد `QuranFolderPath`.
2. مجلد `Quran` بجوار الملف التنفيذي.
3. مجلد `Quran` الموجود في جذر المشروع أثناء التطوير.
4. مجلد يختاره المستخدم من نافذة اختيار المجلد.

إذا نُقل مجلد القرآن إلى جهاز آخر، فإن تشغيل المزامنة من المجلد الجديد يحدّث مسارات الصفحات في قاعدة البيانات.

## ملاحظات الحالة الحالية

- يستهدف الحل .NET Framework 4.8.
- التطبيق مصمم للعمل على Windows.
- توجد 9 اختبارات وحدة، ولا توجد اختبارات تكامل لقاعدة البيانات في الوقت الحالي.
- يلزم توفير قاعدة البيانات قبل تشغيل الوظائف المعتمدة عليها.
