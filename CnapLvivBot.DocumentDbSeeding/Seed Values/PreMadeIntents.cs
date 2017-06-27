using System.Diagnostics.CodeAnalysis;
using CnapLvivBot.Data.Entities;

namespace CnapLvivBot.DocumentDbSeeding.Seed_Values
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class PreMadeIntents
    {
        public static Intent Kid12Years = new Intent() { Content = "дитина 12 років", id = nameof(Kid12Years) };
        public static Intent Absense = new Intent() { Content = "відсутність", id = nameof(Absense) };
        public static Intent Where = new Intent() { Content = "де", id = nameof(Where) };
        public static Intent CNAP = new Intent() { Content = "цнап", id = nameof(CNAP) };
        public static Intent Circumstances = new Intent() { Content = "обставини для зміни", id = nameof(Circumstances) };
        public static Intent Register = new Intent() { Content = "запис", id = nameof(Register) };
        public static Intent DocumentsRequired = new Intent() { Content = "потрібні документи", id = nameof(DocumentsRequired) };
        public static Intent Time = new Intent() { Content = "час", id = nameof(Time) };
        public static Intent Confirm = new Intent() { Content = "підтвердити", id = nameof(Confirm) };
        public static Intent Certificate13 = new Intent() { Content = "довідка 13", id = nameof(Certificate13) };
        public static Intent Photo = new Intent() { Content = "фото", id = nameof(Photo) };
        public static Intent Price = new Intent() { Content = "ціна", id = nameof(Price) };
        public static Intent GivingDocuments = new Intent() { Content = "подання документів", id = nameof(GivingDocuments) };
        public static Intent ForeignPassport = new Intent() { Content = "закордонний проект", id = nameof(ForeignPassport) };
        public static Intent UkrainianPassport = new Intent() { Content = "український паспорт", id = nameof(UkrainianPassport) };

    }
}
