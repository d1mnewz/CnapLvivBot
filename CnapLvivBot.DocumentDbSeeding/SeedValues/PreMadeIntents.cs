using System.Diagnostics.CodeAnalysis;
using CnapLvivBot.Data.Entities;

namespace CnapLvivBot.DocumentDbSeeding.SeedValues
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class PreMadeIntents
    {
        public static readonly Intent Kid12Years = new Intent() { Content = "дитина 12 років", id = nameof(Kid12Years) };
        public static readonly Intent Absense = new Intent() { Content = "відсутність", id = nameof(Absense) };
        public static readonly Intent Where = new Intent() { Content = "де", id = nameof(Where) };
        public static readonly Intent CNAP = new Intent() { Content = "цнап", id = nameof(CNAP) };
        public static readonly Intent Circumstances = new Intent() { Content = "обставини для зміни", id = nameof(Circumstances) };
        public static readonly Intent Register = new Intent() { Content = "запис", id = nameof(Register) };
        public static readonly Intent DocumentsRequired = new Intent() { Content = "потрібні документи", id = nameof(DocumentsRequired) };
        public static readonly Intent Time = new Intent() { Content = "час", id = nameof(Time) };
        public static readonly Intent Confirm = new Intent() { Content = "підтвердити", id = nameof(Confirm) };
        public static readonly Intent Certificate13 = new Intent() { Content = "довідка 13", id = nameof(Certificate13) };
        public static readonly Intent Photo = new Intent() { Content = "фото", id = nameof(Photo) };
        public static readonly Intent Price = new Intent() { Content = "ціна", id = nameof(Price) };
        public static readonly Intent GivingDocuments = new Intent() { Content = "подання документів", id = nameof(GivingDocuments) };
        public static readonly Intent ForeignPassport = new Intent() { Content = "закордонний паспорт", id = nameof(ForeignPassport) };
        public static readonly Intent UkrainianPassport = new Intent() { Content = "український паспорт", id = nameof(UkrainianPassport) };

    }
}
