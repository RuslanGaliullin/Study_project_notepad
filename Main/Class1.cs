using System.Collections.Generic;
using System.Windows.Forms;


namespace Main
{
    /// <summary>
    ///  Тип данных FileInUse, используемый для присваивании соответствующих свойств файлам, который открыты в окне.
    /// </summary>
    internal sealed class FileInUse
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Проверка на то, что файл сохранен уже.
        /// </summary>
        public bool IsSaved { get; set; }
        /// <summary>
        /// Параметр того, что файл создан в приложении и не сохранен на компьютер.
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// Параметр того, что у файла нужно сохранять прошлые версии.
        /// </summary>
        public bool Log { get; set; }
        /// <summary>
        /// Вкладка, которой принадлежит файл.
        /// </summary>
        public TabPage TabPage { get; init; }
        /// <summary>
        /// RichTextBox, который привязан к файлу, т.е. в нем показывается содержимое файла.
        /// </summary>
        public RichTextBox TextBox { get; private set; }

        // Конструктор.
        public FileInUse(string fileName, RichTextBox text, TabPage tabPage, bool isNew)
        {

            this.FileName = fileName;
            this.IsSaved = true;
            this.IsNew = isNew;
            this.TextBox = text;
            this.TabPage = tabPage;
            this.Log = false;
        }
    }

    /// <summary>
    /// Статический класс для взаимодействия с окнами приложения.
    /// </summary>
    internal static class MyForms
    {
        // Список открытых форм.
        private static readonly List<Form1> s_windows = new();

        /// <summary>
        /// Метод, возвращающий первую открытую форму, отличную от текущей.
        /// </summary>
        /// <param name="curForm">Текущая главная форма.</param>
        /// <returns>Новую откртыую форму или null, если ее нет.</returns>
        public static Form1 GetOpened(Form curForm)
        {
            return s_windows.Find(x => x != curForm);
        }

        /// <summary>
        /// Метод удаления формы из открытых.
        /// </summary>
        /// <param name="form">Форма для удаления.</param>
        public static void Close(Form1 form)
        {
            s_windows.Remove(form);
        }

        /// <summary>
        /// Метод добавления формы в список открытых формы.
        /// </summary>
        /// <param name="form">Форма для добавления.</param>
        public static void Add(Form1 form)
        {
            s_windows.Add(form);
        }
    }
}
