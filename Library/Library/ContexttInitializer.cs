using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DBLib.Models;
using Library.Models;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library
{
    public class ContexttInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var authorsList = new List<Author>();

            var publishersList = new List<Publisher>();

            var sectionsList = new List<Section>();

            List<Book> booksList = new List<Book>();

            var commentsList = new List<Comment>();

            var booksRentingsList = new List<BooksRenting>();
            
            var a0 = new Author() { Name = "---" }; authorsList.Add(a0);
            var a1 = new Author() { Name = "Юрій Горліс-Горський" }; authorsList.Add(a1);
            var a2 = new Author() { Name = "Анджей Сапковский" }; authorsList.Add(a2);
            var a3 = new Author() { Name = "Дмитрий Глуховский" }; authorsList.Add(a3);
            var a4 = new Author() { Name = "Ліна Костенко" }; authorsList.Add(a4);
            var a5 = new Author() { Name = "Гілл Наполеон" }; authorsList.Add(a5);
            var a6 = new Author() { Name = "Люсінда Райлі" }; authorsList.Add(a6);
            var a7 = new Author() { Name = "Рей Брэдбері" }; authorsList.Add(a7);
            var a8 = new Author() { Name = "Стівен Кінг" }; authorsList.Add(a8);
            var a9 = new Author() { Name = "Толкін Джон" }; authorsList.Add(a9);
            var a10 = new Author() { Name = "Джоан Ролінґ" }; authorsList.Add(a10);
            var a11 = new Author() { Name = "брати Стругацькіґ" }; authorsList.Add(a11);
            var a12 = new Author() { Name = "Олександр Палій" }; authorsList.Add(a12);
            var a13 = new Author() { Name = "Голідей Раян" }; authorsList.Add(a13);
            var a14 = new Author() { Name = "Генсільман Стівен" }; authorsList.Add(a14);
            var a15 = new Author() { Name = "Робін Ніксон" }; authorsList.Add(a15);
            var a16 = new Author() { Name = "Ендрю Троелсен" }; authorsList.Add(a16);
            var a17 = new Author() { Name = "Філіпп Джепкінс" }; authorsList.Add(a17);
            var a18 = new Author() { Name = "Джеффрі Ріхтер" }; authorsList.Add(a18);
            var a19 = new Author() { Name = "Джозеф Албахарі" }; authorsList.Add(a19);
            var a20 = new Author() { Name = "Бен Албахарі" }; authorsList.Add(a20);
            var a21 = new Author() { Name = "Етан Браун" }; authorsList.Add(a21);
            var a22 = new Author() { Name = "Дженніфер Роббінс" }; authorsList.Add(a22);
            var a23 = new Author() { Name = "Джон Скіт" }; authorsList.Add(a23);
            var a24 = new Author() { Name = "Валерій Пекар" }; authorsList.Add(a24);
            var a25 = new Author() { Name = "Елленберґ Джордан" }; authorsList.Add(a25);
            var a26 = new Author() { Name = "Восс Кріс" }; authorsList.Add(a26);
            var a27 = new Author() { Name = "Джефрі Лайкер" }; authorsList.Add(a27);
            var a28 = new Author() { Name = "Ґільбо Кріс" }; authorsList.Add(a28);

            var p1 = new Publisher() { Name = "Книжковий Клуб «Клуб Сімейного Дозвілля", City = "Харків" }; publishersList.Add(p1);
            var p2 = new Publisher() { Name = "АСТ", City = "Москва" }; publishersList.Add(p2);
            var p3 = new Publisher() { Name = "А-ба-ба-га-ла-ма-га", City = "Київ" }; publishersList.Add(p3);
            var p4 = new Publisher() { Name = "Наш Формат", City = "Київ" }; publishersList.Add(p4);
            var p5 = new Publisher() { Name = "Вівіат", City = "Харків" }; publishersList.Add(p5);
            var p6 = new Publisher() { Name = "ФОРС", City = "Київ" }; publishersList.Add(p6);
            var p7 = new Publisher() { Name = "Астролябія", City = "Львів" }; publishersList.Add(p7);
            var p8 = new Publisher() { Name = "Навчальна книга - Богдан", City = "Тернопіль" }; publishersList.Add(p8);

            var p9 = new Publisher() { Name = "АССА", City = "Харків" }; publishersList.Add(p9);
            var p10 = new Publisher() { Name = "Питер", City = "Пітер" }; publishersList.Add(p10);
            var p11 = new Publisher() { Name = "Диалектика - Вильямс", City = "Москва" }; publishersList.Add(p11);
            var p12 = new Publisher() { Name = "Фоліо", City = "Харків" }; publishersList.Add(p9);

            var s1 = new Section() { Name = "Мемуари. Біографії" }; sectionsList.Add(s1);
            var s2 = new Section() { Name = "Фантастика. Фентезі" }; sectionsList.Add(s2);
            var s3 = new Section() { Name = "Художні книги" }; sectionsList.Add(s3);
            var s4 = new Section() { Name = "Бізнес і підприємництво" }; sectionsList.Add(s4);
            var s5 = new Section() { Name = "Освіта і навчання" }; sectionsList.Add(s5);
            var s6 = new Section() { Name = "Технічна література, інструкції, посібники" }; sectionsList.Add(s6);

            var b1 = new Book()
            {
                Id = System.Guid.Parse("dea6286b-52e5-44e6-b557-f8f8372996d0"),
                Title = "Холодний Яр",
                Authors = new List<Author>() { a1 },
                Year = 2017,
                Publisher = p1,
                LibraryReading = false,
                Quantity = 15,
                Pages = 400,
                ISBN = "978-617-12-2503-9",
                Section = s1
            };
            booksList.Add(b1);

            var b2 = new Book()
            {
                Id = System.Guid.Parse("ca2f0f87-008a-4119-8191-5508c9632c2d"),
                Title = "Відьмак. Останнє бажання",
                Authors = new List<Author>() { a2 },
                Year = 2018,
                Publisher = p1,
                LibraryReading = false,
                Quantity = 20,
                Pages = 368,
                ISBN = "978-617-12-0498-0",
                Section = s2
            };
            booksList.Add(b2);

            var b3 = new Book()
            {
                Id = System.Guid.Parse("8ab4278d-9ccc-447e-9451-fed1f84cd052"),
                Title = "Метро 2033",
                Authors = new List<Author>() { a3 },
                Year = 2019,
                Publisher = p2,
                LibraryReading = false,
                Quantity = 8,
                Pages = 384,
                ISBN = "978-5-17-114425-8",
                Section = s2
            };
            booksList.Add(b3);

            var b4 = new Book()
            {
                Id = System.Guid.Parse("2742cafb-24f6-4ec0-af0a-4b616af509b2"),
                Title = "Записки українського самашедшого",
                Authors = new List<Author>() { a4 },
                Year = 2010,
                Publisher = p3,
                LibraryReading = false,
                Quantity = 8,
                Pages = 416,
                ISBN = "978-966-7047-88-7",
                Section = s3
            };
            booksList.Add(b4);

            var b5 = new Book()
            {
                Id = System.Guid.Parse("b5eccede-9c6b-467d-94c9-af9dc77e97d0"),
                Title = "Думай і багатій",
                Authors = new List<Author>() { a5 },
                Year = 2017,
                Publisher = p4,
                LibraryReading = true,
                Quantity = 1,
                Pages = 264,
                ISBN = "978-617-7388-96-7",
                Section = s4
            };
            booksList.Add(b5);

            var b6 = new Book()
            {
                Id = System.Guid.Parse("980b53da-3f2f-4f1c-8a18-8ca17e01a7d0"),
                Title = "Будинок орхідей",
                Authors = new List<Author>() { a6 },
                Year = 2019,
                Publisher = p5,
                LibraryReading = false,
                Quantity = 5,
                Pages = 576,
                ISBN = "978-966942-941",
                Section = s2
            };
            booksList.Add(b6);

            var b7 = new Book()
            {
                Id = System.Guid.Parse("5dec6146-dfa6-40ac-bc25-f8c031d61eba"),
                Title = "451° по Фаренгейту",
                Authors = new List<Author>() { a7 },
                Year = 2019,
                Publisher = p6,
                LibraryReading = false,
                Quantity = 5,
                Pages = 256,
                ISBN = "978-617-7764-15-0",
                Section = s2
            };
            booksList.Add(b7);

            var b8 = new Book()
            {
                Id = System.Guid.Parse("f49222e0-2ca2-4cf8-967e-ffd8e54807cd"),
                Title = "Сплячі красуні",
                Authors = new List<Author>() { a8 },
                Year = 2008,
                Publisher = p1,
                Pages = 880,
                ISBN = "978-617-12-5418-3",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b8);

            var b9 = new Book()
            {
                Id = System.Guid.Parse("23d99d76-b475-43d7-9645-86636cd049f6"),
                Title = "Відьмак. Меч призначення",
                Authors = new List<Author>() { a2 },
                Year = 2018,
                Publisher = p1,
                Pages = 368,
                ISBN = "978-617-12-0498-0",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b9);

            var b10 = new Book()
            {
                Id = System.Guid.Parse("077bbf5a-8514-46f4-9dd0-d96c0d5b2e1f"),
                Title = "Відьмак. Кров Ельфів",
                Authors = new List<Author>() { a2 },
                Year = 2018,
                Publisher = p1,
                Pages = 320,
                ISBN = "978-6-171-21037-0",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b10);

            var b11 = new Book()
            {
                Id = System.Guid.Parse("1446b7d1-1bc6-4ee8-a86d-55ef057bfb48"),
                Title = "Відьмак. Час Погорди",
                Authors = new List<Author>() { a2 },
                Year = 2018,
                Publisher = p1,
                Pages = 320,
                ISBN = "978-6-171-21038-7",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b11);

            var b12 = new Book()
            {
                Id = System.Guid.Parse("f5826314-1a8f-4445-89de-f621cfe014fb"),
                Title = "Сильмариліон",
                Authors = new List<Author>() { a9 },
                Year = 2016,
                Publisher = p1,
                Pages = 576,
                ISBN = "978-6-17664-078-3",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b12);

            var b13 = new Book()
            {
                Id = System.Guid.Parse("71f6e6ed-84bd-44f2-bb6c-3426b6b0454f"),
                Title = "Фантастичні звірі і де їх шукати",
                Authors = new List<Author>() { a10 },
                Year = 2018,
                Publisher = p3,
                Pages = 156,
                ISBN = "978-617-585-143-2",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b13);

            var b14 = new Book()
            {
                Id = System.Guid.Parse("d335c11d-8dde-4373-b64e-f55fb676e53b"),
                Title = "Братство Персня. Володар Перснів",
                Authors = new List<Author>() { a9 },
                Year = 2016,
                Publisher = p7,
                Pages = 704,
                ISBN = "978-6-176-64100-1",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b14);

            var b15 = new Book()
            {
                Id = System.Guid.Parse("23b98644-3558-4725-b4cb-7c5435a7e90e"),
                Title = "Малюк: Повість",
                Authors = new List<Author>() { a11 },
                Year = 2016,
                Publisher = p8,
                Pages = 212,
                ISBN = "978-966-10-1394-9",
                Section = s2,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b15);

            var b16 = new Book()
            {
                Id = System.Guid.Parse("d382d040-8a71-4f7e-9f00-7745c7ec8d7b"),
                Title = "Короткий курс історії України",
                Authors = new List<Author>() { a12 },
                Year = 2017,
                Publisher = p3,
                Pages = 464,
                ISBN = "978-617-585-123-4",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b16);

            var b17 = new Book()
            {
                Id = System.Guid.Parse("f937841f-929d-4489-8978-515bfbe1ce0f"),
                Title = "100 тем. Українська література",
                Authors = new List<Author>() { a0 },
                Year = 2018,
                Publisher = p9,
                Pages = 192,
                ISBN = "78-966-2623-69-7",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b17);

            var b18 = new Book()
            {
                Id = System.Guid.Parse("e98dfdbf-2a8d-491b-9d0a-14acbc6c6920"),
                Title = "100 тем. Англійська мова",
                Authors = new List<Author>() { a0 },
                Year = 2018,
                Publisher = p9,
                Pages = 192,
                ISBN = "978-966-2623-73-4",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b18);

            var b19 = new Book()
            {
                Id = System.Guid.Parse("5d053e08-9262-4a27-bea2-0e0e16d141d6"),
                Title = "100 тем. Географія",
                Authors = new List<Author>() { a0 },
                Year = 2018,
                Publisher = p9,
                Pages = 192,
                ISBN = "978-966-2623-73-4",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b19);

            var b20 = new Book()
            {
                Id = System.Guid.Parse("4a3681dc-4b31-4df0-8a87-e7142eaa3f07"),
                Title = "100 тем. Історія України",
                Authors = new List<Author>() { a0 },
                Year = 2018,
                Publisher = p9,
                Pages = 192,
                ISBN = "978-966-2623-73-4",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b20);

            var b21 = new Book()
            {
                Id = System.Guid.Parse("4fbc851b-d2e7-4143-8123-36ce8afd56dc"),
                Title = "Зберігайте спокій. Щоденна інструкція з вирішення проблем",
                Authors = new List<Author>() { a13, a14 },
                Year = 2018,
                Publisher = p4,
                Pages = 472,
                ISBN = "978-617-7552-79-5",
                Section = s5,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b21);

            var b22 = new Book()
            {
                Id = System.Guid.Parse("e3e70771-e3dc-4f82-a9e7-9db101d95187"),
                Title = "Создаем динамические веб-сайты с помощью PHP, MySQL, JavaScript, CSS и HTML5. 5-е издание",
                Authors = new List<Author>() { a15 },
                Year = 2019,
                Publisher = p10,
                Pages = 816,
                ISBN = "978-5-4461-0825-1",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b22);

            var b23 = new Book()
            {
                Id = System.Guid.Parse("3c290746-9fd4-46ff-9fb3-0515c08c0ee1"),
                Title = "Язык программирования C# 7 и платформы .NET и .NET Core, 8-е издание",
                Authors = new List<Author>() { a16, a17 },
                Year = 2009,
                Publisher = p11,
                Pages = 1328,
                ISBN = "978-5-6040723-1-8",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b23);

            var b24 = new Book()
            {
                Id = System.Guid.Parse("51651da2-0941-4b05-bf74-5a8af4e190a8"),
                Title = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#",
                Authors = new List<Author>() { a18 },
                Year = 2018,
                Publisher = p10,
                Pages = 896,
                ISBN = "978-5-496-00433-6",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b24);

            var b25 = new Book()
            {
                Id = System.Guid.Parse("b02411c0-f531-474a-9b3f-91c2b5f337ba"),
                Title = "C# 7.0. Справочник. Полное описание языка, 7-е издание",
                Authors = new List<Author>() { a19, a20 },
                Year = 2019,
                Publisher = p11,
                Pages = 1024,
                ISBN = "978-5-6040043-7-1",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b25);

            var b26 = new Book()
            {
                Id = System.Guid.Parse("28b440f9-72c8-4362-91db-c3e1c1add7b8"),
                Title = "Изучаем JavaScript: руководство по созданию современных веб-сайтов. 3-е издание",
                Authors = new List<Author>() { a21 },
                Year = 2019,
                Publisher = p11,
                Pages = 368,
                ISBN = "978-5-9908463-9-5",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b26);

            var b27 = new Book()
            {
                Id = System.Guid.Parse("987d3ec3-de77-405c-9860-159bf6b58063"),
                Title = "HTML5: карманный справочник",
                Authors = new List<Author>() { a22 },
                Year = 2016,
                Publisher = p11,
                Pages = 192,
                ISBN = "978-5-8459-1937-3",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b27);

            var b28 = new Book()
            {
                Id = System.Guid.Parse("c52f80dd-c595-4e86-97f1-97b23fee7293"),
                Title = "C# для профессионалов: тонкости программирования",
                Authors = new List<Author>() { a23 },
                Year = 2019,
                Publisher = p11,
                Pages = 608,
                ISBN = "978-5-907114-62-3",
                Section = s6,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b28);

            var b29 = new Book()
            {
                Id = System.Guid.Parse("223a17aa-2b90-4d25-bba9-840dc8dea2f1"),
                Title = "Різнобарвній менеджмент. 2-ге видання",
                Authors = new List<Author>() { a24 },
                Year = 2019,
                Publisher = p9,
                Pages = 192,
                ISBN = "978-966-03-7620-5",
                Section = s4,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b29);

            var b30 = new Book()
            {
                Id = System.Guid.Parse("aad99c3d-da8c-4587-96ca-624435327af5"),
                Title = "Як ніколи не помилятися. Сила математичного мислення",
                Authors = new List<Author>() { a25 },
                Year = 2017,
                Publisher = p4,
                Pages = 408,
                ISBN = "978-617-7388-75-2",
                Section = s4,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b30);

            var b31 = new Book()
            {
                Id = System.Guid.Parse("37f0ac0c-37c7-4857-8bcd-50d682f336df"),
                Title = "Ніколи не йдіть на компроміс. Техніка ефективних переговорів",
                Authors = new List<Author>() { a26 },
                Year = 2019,
                Publisher = p4,
                Pages = 263,
                ISBN = "978-617-7682-22-5",
                Section = s4,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b31);

            var b32 = new Book()
            {
                Id = System.Guid.Parse("3d2fa82f-6cff-4bfb-bc1e-f6d801ea0118"),
                Title = "Філософія Toyota. 14 принципів роботи злагодженої команди",
                Authors = new List<Author>() { a27 },
                Year = 2017,
                Publisher = p4,
                Pages = 424,
                ISBN = "978-617-7388-78-3",
                Section = s4,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b32);

            var b33 = new Book()
            {
                Id = System.Guid.Parse("8e9684bf-9b31-4442-9856-4fa09c8ad4b5"),
                Title = "Пасивний заробіток. Як перетворити ідею на гроші за 27 днів",
                Authors = new List<Author>() { a28 },
                Year = 2019,
                Publisher = p4,
                Pages = 238,
                ISBN = "978-617-7682-42-3",
                Section = s4,
                LibraryReading = false,
                Quantity = 5
            };
            booksList.Add(b33);


            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var adminRole = new IdentityRole { Name = "admin" };
            var librarianRole = new IdentityRole { Name = "librarian" };
            var registeredRole = new IdentityRole { Name = "registered" };

            // добавляем роли в бд
            roleManager.Create(adminRole);
            roleManager.Create(librarianRole);
            roleManager.Create(registeredRole);

            // создаем пользователей
            var adminUser = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com", Name="Василь", Surname="Адміністраторович" };
            string adminPassword = "123qwe";

            var librarianUser = new ApplicationUser { Email = "librarian@gmail.com", UserName = "librarian@gmail.com", Name = "Іванка", Surname = "Бібліотекар" };
            string librarianPassword = "123qwe";

            var registeredUser = new ApplicationUser { Email = "registered@gmail.com", UserName = "registered@gmail.com", Name = "Артем", Surname = "Дуб" };
            string registeredPassword = "123qwe";
            var registeredUser2 = new ApplicationUser { Email = "registered2@gmail.com", UserName = "registered2@gmail.com", Name = "Тимофій", Surname = "Зібров" };
            var registeredUser3 = new ApplicationUser { Email = "registered3@gmail.com", UserName = "registered3@gmail.com", Name = "Олена", Surname = "Ємець" };

            var result1 = userManager.Create(adminUser, adminPassword);
            userManager.AddToRole(adminUser.Id, adminRole.Name);
            userManager.AddToRole(adminUser.Id, librarianRole.Name);
            userManager.AddToRole(adminUser.Id, registeredRole.Name);

            var result2 = userManager.Create(librarianUser, librarianPassword);
            userManager.AddToRole(librarianUser.Id, librarianRole.Name);
            userManager.AddToRole(librarianUser.Id, registeredRole.Name);

            var result3 = userManager.Create(registeredUser, registeredPassword);
            userManager.AddToRole(registeredUser.Id, registeredRole.Name);
            var result4 = userManager.Create(registeredUser2, registeredPassword);
            userManager.AddToRole(registeredUser2.Id, registeredRole.Name);
            var result5 = userManager.Create(registeredUser3, registeredPassword);
            userManager.AddToRole(registeredUser3.Id, registeredRole.Name);


            var c1 = new Comment()
            {
                Book = b18,
                ApplicationUser = registeredUser,
                CommentText = "Гарний посібник для англійської"
            }; commentsList.Add(c1);

            var c2 = new Comment()
            {
                Book = b18,
                ApplicationUser = librarianUser,
                CommentText = "Так, і справді"
            }; commentsList.Add(c2);

            var br1 = new BooksRenting()
            {
                Book = b17,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now.AddDays(-5),
                TakingDate = DateTime.Now.AddDays(-4),
                ReturningDate = DateTime.Now.AddDays(-2)
            }; booksRentingsList.Add(br1);

            var br2 = new BooksRenting()
            {
                Book = b18,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now.AddDays(-2),
                TakingDate = DateTime.Now.AddDays(-1),
            }; booksRentingsList.Add(br2);

            var br3 = new BooksRenting()
            {
                Book = b19,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now.AddDays(-2),
                TakingDate = DateTime.Now.AddDays(-1),
            }; booksRentingsList.Add(br3);

            var br4 = new BooksRenting()
            {
                Book = b20,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now,
            }; booksRentingsList.Add(br4);

            var br5 = new BooksRenting()
            {
                Book = b16,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now,
            }; booksRentingsList.Add(br5);

            var br6 = new BooksRenting()
            {
                Book = b5,
                ApplicationUser = registeredUser,
                OrderDate = DateTime.Now.AddDays(-18),
                TakingDate = DateTime.Now.AddDays(-17),
            }; booksRentingsList.Add(br6);




            booksList.ForEach(el => context.Books.Add(el));
            authorsList.ForEach(el => context.Authors.Add(el));
            publishersList.ForEach(el => context.Publishers.Add(el));
            sectionsList.ForEach(el => context.Sections.Add(el));
            commentsList.ForEach(el => context.Comments.Add(el));
            booksRentingsList.ForEach(el => context.BooksRenting.Add(el));
            base.Seed(context);
        }
    }
}