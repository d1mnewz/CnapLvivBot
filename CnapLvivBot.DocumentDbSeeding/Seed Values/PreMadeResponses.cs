using CnapLvivBot.Data.Entities;
using static CnapLvivBot.DocumentDbSeeding.Seed_Values.PreMadeIntents;

namespace CnapLvivBot.DocumentDbSeeding.Seed_Values
{
    public static class PreMadeResponses
    {
        public static readonly Response DocumentsForeignPassport = new Response()
        {
            Content = "Для оформлення закордонного паспорту потрібні:\r\n" +
                      "* Паспорт громадянина України;\r\n" +
                      "* Квитанція про оплату адміністративного збору" +
                      " (оплатити можна безпосередньо у територіальних підрозділах ЦНАП м. Львова);\r\n" +
                      "* Паспорт громадянина України для виїзду за кордон (за наявності);\r\n",
            id = nameof(DocumentsForeignPassport),
            Intents = new Intent[] { DocumentsRequired, ForeignPassport }
        };

        public static readonly Response DocumentsForeignPassportKid = new Response()
        {
            Content = "Для оформлення паспорта громадянина України для дитини потрібно:\r\n" +
                      "* Свідоцтво про народження дитини (оригінал,копія);\r\n" +
                      "* Фотокартки: 3,5 х 4,5 (2шт.), 10 х 15 (1шт.);\r\n" +
                      "* Довідка форми №2 про склад сім’ї ;\r\n" +
                      "* Присутність обох батьків з паспортами громадян України (оригінали,копії);\r\n" +
                      "* Квитанція про оплату адміністративного збору \r\n",
            id = nameof(DocumentsForeignPassportKid),
            Intents = new Intent[] { DocumentsRequired, ForeignPassport, Kid12Years }
        };

        public static readonly Response DocumentsForeignPassportCnap = new Response()
        {
            Content = "Так, документи на оформлення паспорта громадянина України для виїзду закордон " +
                      "можна подати у будь – якому територіальному підрозділі ЦНАП м. Львова," +
                      " крім підрозділу за адресою площа Ринок,1(бічний вхід).",
            id = nameof(DocumentsForeignPassportCnap),
            Intents = new Intent[] { ForeignPassport, GivingDocuments, CNAP }
        };

        public static readonly Response PriceForeignPassport = new Response()
        {
            Content = "557,32 грн – 20 робочих днів термін виконання," +
                      " 810,32 грн. – 7 робочих днів термін виконання;",
            id = nameof(PriceForeignPassport),
            Intents = new Intent[] { Price, ForeignPassport }
        };

        public static readonly Response PriceForeignPassportKid = new Response()
        {
            Content = "557,32 грн – 20 робочих днів термін виконання," +
                      " 810,32 грн. – 7 робочих днів термін виконання;",
            id = nameof(PriceForeignPassportKid),
            Intents = new Intent[] { Price, ForeignPassport, Kid12Years }
        };

        public static readonly Response UkrainianPassportKid = new Response()
        {
            Content = "* Свідоцтво про народження дитини (оригінал,копія);\r\n" +
                      "* Оригінали паспортів батьків;\r\n" +
                      "* Ідентифікаційний код дитини(оригінал,копія);\r\n" +
                      "* Довідка форми №13  про реєстрацію місця проживання; \r\n",
            id = nameof(UkrainianPassportKid),
            Intents = new Intent[] { UkrainianPassport, Kid12Years }
        };

        public static readonly Response UkrainianPassportChange = new Response()
        {
            Content = "* Паспорт громадянина України, що підлягає обміну;\r\n" +
                      "* Документи, що підтверджують обставини, у зв’язку з якими паспорт підлягає обміну (крім випадків коли строк дії паспорта закінчився);\r\n" +
                      "* Квитанція про оплату адміністративного збору;\r\n" +
                      "* Ідентифікаційний код (оригінал,копія);\r\n" +
                      "* Свідоцтва про народження дітей/шлюб і розірвання шлюбу/ про зміну імені (за наявності);\r\n",
            id = nameof(UkrainianPassportChange),
            Intents = new Intent[] { UkrainianPassport, Circumstances }
        };

        public static readonly Response WhereCertificate13 = new Response()
        {
            Content =
                "Довідку форми №13 про реєстрацію місця проживання можна оформити у будь – якому територіальному підрозділі ЦНАП м. Львова.",
            id = nameof(WhereCertificate13),
            Intents = new Intent[] { Where, Certificate13 }
        };

        public static readonly Response DocumentsRequiredCertifcate13 = new Response()
        {
            Content = "Для оформлення довідки форми № 13 потрібні:\r\n" +
                      "* Паспорт громадянина України (оригінал,копія);\r\n" +
                      "* Свідоцтво про народження дитини (для дітей які не досягли 14 років, оригінал і копія);\r\n" +
                      "* Довідка форми № 2 про склад сім’ї/ будинкова книга;\r\n",
            id = nameof(DocumentsRequiredCertifcate13),
            Intents = new Intent[] { DocumentsRequired, Certificate13 }
        };

        public static readonly Response RegisterQueuePassport = new Response()
        {
            Content =
                "Зареєструватися у чергу Ви можете двома способами: через електронний сервіс \"Попередній запис до ЦНАП\" " +
                "або отримавши талончик безпосередньо у територіальних підрозділах Центру" +
                " надання адміністративних послуг м. Львова. Попередній запис до ЦНАП здійснюється на офіційному " +
                "сайті Львівської міської ради.  Щодня оновлюється система  на сайті Львівської міської ради" +
                " та додає можливість попереднього запису до ЦНАП м. Львова. Зареєстувавшись в електронній черзі" +
                " (ЕСКЧ) у терміналі, що знаходиться безпосередньо в офісі, Ви отримуєте чек з порядковим номером" +
                " та в цей же день можете подавати документи .\r\nЗаписатися у чергу для оформлення паспорту " +
                "громадянина України/паспорта громадянина України" +
                " для виїзду закордон можна за посиланням: http://city-adm.lviv.ua/services/zapys-do-tsnap\r\n",
            id = nameof(RegisterQueuePassport),
            Intents = new Intent[] { Register, UkrainianPassport, ForeignPassport }
        };

        public static readonly Response ForeignPassportTime = new Response()
        {
            Content = "20 робочих днів/ 7 робочих днів не враховуючи терміну доставки.\r\n",
            id = nameof(ForeignPassportTime),
            Intents = new Intent[] { Time, ForeignPassport, Kid12Years }
        };

        public static readonly Response PhotoCnap = new Response()
        {
            Content = "Так, фотографію для оформлення паспорта громадянина України/для виїзду" +
                      " за кордон можна зробити у територіальних підрозділах " +
                      "ЦНАП м. Львова для дорослих та дітей віком від 12 років.",
            id = nameof(PhotoCnap),
            Intents = new Intent[] { Photo, CNAP /*, ForeignPassport, UkrainianPassport*/}
        };

        public static readonly Response WhereGiveDocumentsPassport = new Response()
        {
            Content = "Подати документи для оформлення паспорта громадянина України можна у " +
                      "будь – якому територіальному підрозділі ЦНАП м. Львова," +
                      " крім територіального підрозділу за адресою площа Ринок ,1 (бічний вхід).",
            id = nameof(WhereGiveDocumentsPassport),
            Intents = new Intent[] { Where, GivingDocuments, CNAP /*, ForeignPassport, UkrainianPassport*/}
        };

        public static readonly Response RegisterAbsentPassport = new Response()
        {
            Content = "Станом на сьогодні  в електронному сервісі " +
                      "\"Попередній запис до ЦНАП\" проводяться технічні налаштування. " +
                      "Оскільки попередній запис на червень місяць повністю заповнений," +
                      " пропонуємо Вам звернутися у територіальні підрозділи Центру надання адміністративних послуг " +
                      "і отримати талон на подачу документів безпосередньо на місці (крім суботи та понеділка)" +
                      ", можливо найближчим часом робота відновиться." +
                      " Моніторити роботу сервісу можна за посиланням: http://city-adm.lviv.ua/services/zapys-do-tsnap",
            id = nameof(RegisterAbsentPassport),
            Intents = new Intent[] { Absense, Register /*, ForeignPassport, UkrainianPassport*/}
        };

        public static readonly Response ConfirmRegisterForPassport = new Response()
        {
            Content = "Підтвердити реєстрацію можна трьома способами:" +
                      " за допомогою смс-повідомлення, електронного листа або " +
                      "талона про реєстрацію, який можна роздрукувати чи сфотографувати." +
                      " Якщо Ви не мали можливості розрукувати/сфотографувати талон," +
                      " не отримали смс чи електронного листа з підтвердженням," +
                      " але загалом бачили посилання на роздрук талона," +
                      " в такому випадку Ви можете звернутися в Центр надання адміністративних послуг м. Львова," +
                      " в який записувалися, і в день реєстрації адміністратор зможе ідентифікувати Вас за прізвищем." +
                      " Також працівники територіальних підрозділів" +
                      " ЦНАП м. Львова  можуть зателефонувати до Вас з метою підтвердження реєстрації на ту чи іншу дату.",
            id = nameof(ConfirmRegisterForPassport),
            Intents = new Intent[] { Confirm, Register /*, UkrainianPassport, ForeignPassport*/}
        };
    }


}
