using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class _123123727215476541SendBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "Books",
        columns: new[] { "Title", "Autor", "ISBN", "IsAvailable", "Description" },
        values: new object[,]
        {
            { "Podróż przez Galaktykę", "Jan Kowalski", "978-83-123456-01-1", true, "Historia młodego odkrywcy, który przemierza kosmos w poszukiwaniu nowej planety dla ludzkości." },
            { "Sekretna Dolina", "Anna Nowak", "978-83-123456-02-2", false, "Młoda kobieta odkrywa magiczne miejsce w górach, gdzie każda roślina ma swoje tajemnice." },
            { "Mistrzowie Alchemii", "Tomasz Wiśniewski", "978-83-123456-03-3", true, "Opowieść o dwóch przyjaciołach, którzy odkrywają starożytne tajniki alchemii." },
            { "Zbrodnia w Pałacu", "Katarzyna Wolska", "978-83-123456-04-4", true, "Detektyw bada tajemnicze morderstwo w arystokratycznym pałacu pełnym sekretów." },
            { "Królewskie Intrygi", "Michał Zieliński", "978-83-123456-05-5", false, "Wiek XVII, walka o tron i potężne zdrady wśród europejskiej arystokracji." },
            { "Pod Stalowym Niebem", "Barbara Kowalska", "978-83-123456-06-6", true, "Science fiction o walce ludzi z maszynami w świecie po katastrofie." },
            { "Niebo nad Wenecją", "Paweł Marciniak", "978-83-123456-07-7", false, "Romans w czasach renesansowej Wenecji, pełnej tajemnic i zakazanych miłości." },
            { "Księga Cieni", "Agnieszka Lewandowska", "978-83-123456-08-8", true, "Tajemnicza księga odkryta w opuszczonym klasztorze, która prowadzi do niebezpiecznych odkryć." },
            { "Na końcu świata", "Adam Szymański", "978-83-123456-09-9", false, "Historia wyprawy do niezbadanych części Antarktydy." },
            { "Czas Apokalipsy", "Alicja Nowicka", "978-83-123456-10-0", true, "Wizja świata w obliczu końca cywilizacji i ostatnich dni ludzkości." },
            { "Czarny Szlak", "Marek Wójcik", "978-83-123456-11-1", true, "Trzymający w napięciu kryminał o serii morderstw w małym miasteczku." },
            { "Światło Północy", "Zofia Jankowska", "978-83-123456-12-2", false, "Opowieść o podróży po Arktyce i poznawaniu plemion polarnych." },
            { "Cień w Mroku", "Wojciech Olszewski", "978-83-123456-13-3", true, "Tajemnicze morderstwo w miejskich podziemiach prowadzi do szokującego odkrycia." },
            { "Zielone Królestwo", "Ewa Mazur", "978-83-123456-14-4", true, "Niezwykła historia odkrycia nowego świata pełnego egzotycznych roślin i zwierząt." },
            { "Przebudzenie Feniksa", "Krzysztof Wiśniewski", "978-83-123456-15-5", false, "Opowieść o starożytnym imperium, które wskrzesza legendarną broń Feniksa." },
            { "Labirynt Czasu", "Anna Nowakowska", "978-83-123456-16-6", true, "Historia o zagubionych w czasie i przestrzeni poszukiwaczach." },
            { "Prawda i Zemsta", "Piotr Kaczmarek", "978-83-123456-17-7", true, "Opowieść o konflikcie rodzin, w którym każdy dąży do odkrycia prawdy." },
            { "Córka Pustyni", "Maria Słowińska", "978-83-123456-18-8", false, "Życie młodej kobiety, która pragnie wyrwać się ze swojego plemienia na pustyni." },
            { "Władcy Mórz", "Andrzej Ratajczak", "978-83-123456-19-9", true, "Historia o przygodach piratów i żeglarzy w poszukiwaniu mitycznych skarbów." },
            { "Niebo nad Saharą", "Grażyna Michalska", "978-83-123456-20-0", false, "Odkrywanie piękna pustyni oraz niezwykłych ludzi, którzy na niej żyją." }
        });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
