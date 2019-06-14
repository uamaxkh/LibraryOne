using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DBLib.Models;
using Library.Models;
using System.Web;

namespace DBLib
{
    public class ContexttInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var authorsList = new List<Author>();

            var publishersList = new List<Publisher>();

            var sectionsList = new List<Section>();

            List<Book> booksList = new List<Book>();

            //var commentsList = new List<Comment>();
            //var c1 = new Comment()
            //{
            //    //Book = ,
            //    //ApplicationUser = ,
            //    //Date = ,
            //    CommentText = ""
            //};

            //var booksRentingsList = new List<BooksRenting>();
            //var br1 = new BooksRenting()
            //{
            //    //Book = ,
            //    //ApplicationUser = ,
            //    //TakingDate = ;
            //    //ReturningDate = 
            //};


            var Gorlis = new Author() { Name = "Ю. Горліс-Горський" };
            var KSD = new Publisher() { Name = "Книжковий Клуб «Клуб Сімейного Дозвілля", City = "Харків" }; 
            var Memuary = new Section() { Name = "Мемуари. Біографії" }; 
            var b1 = new Book()
            {
                Title = "Холодний Яр",
                TitlePic = "44342_66652_k.jpg",
                Authors = new List<Author>() { Gorlis },
                Year = 2017,
                Publisher = KSD,
                LibraryReading = false,
                Quantity = 15,
                Pages = 400,
                ISBN = "978-617-12-2503-9",
                Section = Memuary
            };
            authorsList.Add(Gorlis);
            publishersList.Add(KSD);
            sectionsList.Add(Memuary);
            booksList.Add(b1);

            var AnjeySapkovski = new Author() { Name = "Анджей Сапковский" }; 
            var Fantastyka = new Section() { Name = "Фантастика. Фентезі" };
            var b2 = new Book()
            {
                Title = "Відьмак.Останнє бажання",
                TitlePic = "7733757_images_1488273965.jpg",
                Authors = new List<Author>() { AnjeySapkovski },
                Year = 2018,
                Publisher = KSD,
                LibraryReading = false,
                Quantity = 20,
                Pages = 368,
                ISBN = "978-617-12-0498-0",
                Section = Fantastyka
            };
            sectionsList.Add(Fantastyka);
            authorsList.Add(AnjeySapkovski);
            booksList.Add(b2);

            var DmitriyGluhovski = new Author() { Name = "Дмитрий Глуховский" };
            var ACT = new Publisher() { Name = "АСТ", City = "Москва" };
            var b3 = new Book()
            {
                Title = "Метро 2033",
                TitlePic = "89082873_images_12235641285.jpg",
                Authors = new List<Author>() { DmitriyGluhovski },
                Year = 2019,
                Publisher = ACT,
                LibraryReading = false,
                Quantity = 8,
                Pages = 384,
                ISBN = "978-5-17-114425-8",
                Section = Fantastyka
            };
            authorsList.Add(DmitriyGluhovski);
            publishersList.Add(ACT);
            booksList.Add(b3);

            var LinaKostenko = new Author() { Name = "Ліна Костенко " };
            var Ababa = new Publisher() { Name = "А-ба-ба-га-ла-ма-га", City = "Київ" };
            var HudogniKnygy = new Section() { Name = "Художні книги" };
            var b4 = new Book()
            {
                Title = "Записки українського самашедшого",
                TitlePic = "a_ba_ba_ga_la_ma_ga_9789667047887_images_1847982452.jpg",
                Authors = new List<Author>() { LinaKostenko },
                Year = 2010,
                Publisher = Ababa,
                LibraryReading = false,
                Quantity = 8,
                Pages = 416,
                ISBN = "978-966-7047-88-7",
                Section = HudogniKnygy
            };
            authorsList.Add(LinaKostenko);
            publishersList.Add(Ababa);
            sectionsList.Add(HudogniKnygy);
            booksList.Add(b4);

            var GillNapoleon = new Author() { Name = "Гілл Наполеон" };
            var NashFormat = new Publisher() { Name = "Наш Формат", City = "Київ" };
            var BisnesiPidpr = new Section() { Name = "Бізнес і підприємництво" };
            var b5 = new Book()
            {
                Title = "Думай і багатій",
                TitlePic = "nash_format_9786177388967_images_11478842126.jpg",
                Authors = new List<Author>() { GillNapoleon },
                Year = 2017,
                Publisher = NashFormat,
                LibraryReading = true,
                Quantity = 4,
                Pages = 264,
                ISBN = "978-617-7388-96-7",
                Section = BisnesiPidpr
            };
            authorsList.Add(GillNapoleon);
            publishersList.Add(NashFormat);
            sectionsList.Add(BisnesiPidpr);
            booksList.Add(b5);


            booksList.ForEach(el => context.Books.Add(el));
            authorsList.ForEach(el => context.Authors.Add(el));
            publishersList.ForEach(el => context.Publisher.Add(el));
            sectionsList.ForEach(el => context.Sections.Add(el));
            //commentsList.ForEach(el => context.Comments.Add(el));
            //booksRentingsList.ForEach(el => context.BooksRenting.Add(el));
            context.SaveChanges();
        }
    }
}