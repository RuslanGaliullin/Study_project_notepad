using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

// У меня одна вкладка == одному файлу, поэтому могу заменять слово вкладка и файл. Они эквивалентны.
// Аналогично слово форма == окно, так как каждая форма - отдельное окно.

namespace Main
{
    public partial class Form1 : Form
    {
        // Список всех файлов, открытых(созданных) в окне.
        private readonly List<FileInUse> files = new();

        // Текущий открытый файл(вкладка в окне.
        private FileInUse curFile;

        // Номер формы, чтобы отследить, когда создается новая форма при открытии, а когда из другого окна.
        private readonly bool isFirst;

        /// <summary>
        /// Конструктор форм. Инициализация всех компонентов, запуска StartForm метода.
        /// </summary>
        /// <param name="isFirst">Параметр того, что сто форма создана при запуске приложения.</param>
        public Form1(bool isFirst)
        {
            InitializeComponent();
            this.Text = $"NotePad+";
            this.isFirst = isFirst;
            // Установка основных общих параметров формы.
            StartForm();
        }

        /// <summary>
        /// Установка общих парметров для формы: фильтры для доступных файлов, восстановление при необходимости
        /// настроек предыдущего запуска, установка таймера, добавления контрола с вкладками.
        /// </summary>
        private void StartForm()
        {
            //TabControl page1 = new TabControl();
            timer.Interval = 1;
            timer.Enabled = false;
            timer.Tick += TimerTick;
            openFileDialog.Filter = "Text files(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|C#(*.cs)|*.cs|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|C#(*.cs)|*.cs";
            saveFileDialog.DefaultExt = "*.rtf";
            if (isFirst)
            {
                SetSettingss();
            }
        }

        /// <summary>
        /// Обработчик событий нажатия на кнопки: "Создать в новой вкладке" или "Открыть в новой вкладке".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void OpenInNewTab(object sender, EventArgs e)
        {
            // Если нажали на кнопку "Создать в новой вкладке".
            if (sender.ToString().Contains("&Создать"))
            {
                CreateTheTab(this, "", "Безымянный", true);
            }
            else
            {
                // Если нажали "Открыть в новой вкладке".
                openFileDialog.InitialDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                // Открытие файла, в котором идет проверка на отлов ошибки.
                if (ShowText(openFileDialog.FileName, out string text))
                {
                    // Если все нормально открывается, то создаем новую вкладку.
                    CreateTheTab(this, text, openFileDialog.FileName, false);
                }
                else
                {
                    // Если что-то пошло не так, то выводим сообщение об ошибке.
                    MessageBox.Show(text);
                }
            }
        }

        /// <summary>
        /// Обработчик событий нажатия на кнопки: "Создать в новом окне" или "Открыть в новом окне".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void OpenInNewForm(object sender, EventArgs e)
        {
            // Если требуется создать файл в новом окне.
            if (sender.ToString().Contains("&Создать"))
            {
                Form1 newForm = new(false);
                // Добавление в общий список окон.
                MyForms.Add(newForm);
                CreateTheTab(newForm, "", "Безымянный", true);
                newForm.Show();
            }
            else
            {
                // Открыть существующий файл в новом окне
                openFileDialog.InitialDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                // Открытие файла, в котором идет проверка на отлов ошибки.
                if (ShowText(openFileDialog.FileName, out string text))
                {
                    Form1 newForm = new(false);
                    // Добавление окна в общий список окон.
                    MyForms.Add(newForm);
                    // Если все нормально открывается, то создаем новую вкладку, но в созданном окне.
                    CreateTheTab(newForm, text, openFileDialog.FileName, false);
                    newForm.Show();
                }
                else
                {
                    // Если что-то пошло не так, то выводим сообщение об ошибке.
                    MessageBox.Show(text);
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить в текущей вкладке".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void FileInCurrentPage(object sender, EventArgs e)
        {
            // Если нет открытых вкладок, то выполняем как для события "Открыть в новой вкладке".
            if (tabControl.TabPages.Count == 0)
            {
                OpenInNewTab(openNewTabToolStripMenuItem, e);
                return;
            }
            // Если есть, то закрываем текущую и открываем новую. Если сделать отмену открытия файла, то текущая закроется!
            DialogSaveToCloseCurrentTab(closeCurrentTabToolStripMenuItem, e);
            if (curFile.IsSaved)
            {
                files.Remove(curFile);
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                // Вызываем обработчик нажатия на кнопку открытия в новой вкладке.
                OpenInNewTab(openNewTabToolStripMenuItem, e);
            }
        }

        /// <summary>
        /// Метод для создание новой вкладки в конкретном окне.
        /// </summary>
        /// <param name="form">Форма, в которой будет новая вкладка.</param>
        /// <param name="text">Текст, который должен быть записан в RichTExtBox.</param>
        /// <param name="fileName">Файл, который открывается в новой вкладке.</param>
        /// <param name="isNew">Параметр того, что файл только создан или уже существует.</param>
        private void CreateTheTab(Form1 form, string text, string fileName, bool isNew)
        {
            try
            {
                // Если файл новый, то вкладка будет иметь заголовок "Безымянный", иначе имя файла.
                TabPage newTabPage = new(isNew ? "Безымянный" : Path.GetFileName(fileName));
                RichTextBox textBox = new();
                textBox.Dock = DockStyle.Fill;
                textBox.ContextMenuStrip = contextMenuStrip1;
                newTabPage.Controls.Add(textBox);
                // Открываем уже существующий файл в rtf, если у него соответствующее разрешение.
                if (!isNew && Path.GetExtension(fileName) == ".rtf")
                {
                    textBox.LoadFile(fileName, RichTextBoxStreamType.RichText);
                }
                // В противном случае записываем содержимое файла в текстовом формате.
                else
                {
                    textBox.Text += text;
                }
                form.tabControl.TabPages.Add(newTabPage);
                // Изменяем текущую вкладку в окне.
                form.tabControl.SelectedTab = newTabPage;
                // Изменяем текущий открытый файл в окне.
                form.curFile = new FileInUse(fileName, textBox, newTabPage, isNew);
                // Добавляем новый файл ко всем открытым в окне.
                form.files.Add(form.curFile);
                // Добавляем обработчик изменения текста у RichTextBox'а, чтобы изменять статус сохраненности файла.
                form.curFile.TextBox.TextChanged += new EventHandler(form.TextHasChanged);
            }
            catch (Exception e)
            {
                // Сообщение об ошибке.
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Метод для выгрузки содирежимого файла в формате txt>
        /// </summary>
        /// <param name="filename">Имя файла.</param>
        /// <param name="text">Текст содержимого файла или сообщение об ошибке.</param>
        /// <returns>Информация, что файл открылся без ошибок (true/false).</returns>
        private static bool ShowText(string filename, out string text)
        {
            try
            {
                text = File.ReadAllText(filename);
                return true;
            }
            catch (Exception e)
            {
                text = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Обработчик событий нажатия на кнопку "Сохранить".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void SaveCurrentFile(object sender, EventArgs e)
        {
            // Если сейчас открыт какой-то файл, но он создан в приложении(новый).
            if (curFile != null && curFile.IsNew)
            {
                // Вызываем обработчик события нажания но кнопку "Сохранить как" т.к. файл новый.
                SaveAsCurrentFile(saveAsToolStripMenuItem, e);
            }
            // Если сейчас открыт какой-то файл, который еще не сохранен
            else if (curFile != null && !curFile.IsSaved)
            {
                try
                {
                    // Сохраняем последнюю версию файла.
                    Logging();
                    // Если разрешение файла .txt, то сохраняем записывание просто содержимого RichTextBox  в файл.
                    if (Path.GetExtension(curFile.FileName) != ".rtf")
                    {
                        File.WriteAllText(curFile.FileName, curFile.TextBox.Text);
                    }
                    // В противном случае сохраняем с форматированием как rtf.
                    else
                    {
                        curFile.TextBox.SaveFile(curFile.FileName, RichTextBoxStreamType.RichText);
                    }
                    // Убираем звездочку из заголовка вкладки, которая говорит о том, что файл не сохранен.
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.IsSaved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Обработчки событий нажатия на кнопку "Сохранить как".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void SaveAsCurrentFile(object sender, EventArgs e)
        {
            try
            {
                // Проверка, что есть файл, который сохраняется.
                if (curFile == null)
                {
                    return;
                }
                saveFileDialog.FileName = curFile.FileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK &&
                   saveFileDialog.FileName.Length > 0)
                {
                    // Сохраняем предыдущую версию.
                    Logging();
                    // .txt сохраняем простым записыванием текста в файл.
                    if (Path.GetExtension(saveFileDialog.FileName) != ".rtf")
                    {
                        File.WriteAllText(saveFileDialog.FileName, curFile.TextBox.Text);
                    }
                    // В се остальные файлы с сохранением форматирования.
                    else
                    {
                        curFile.TextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    // Меняем заголовк вкладки.
                    curFile.FileName = saveFileDialog.FileName;
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.IsSaved = true;
                    curFile.IsNew = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик события изменения текста в RichTextBox’е.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void TextHasChanged(object sender, EventArgs e)
        {
            if (curFile != null && curFile.IsSaved)
            {
                // Меняем статус файла на несохраненный и в заголовке добавляем звездочку.
                curFile.TabPage.Text = "*" + curFile.TabPage.Text;
                curFile.IsSaved = false;
            }
        }
        /// <summary>
        /// Добавление обработчика переключения вкладки. При создании формы
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.Selected += new TabControlEventHandler(ChangeSelectedTab);
        }
        /// <summary>
        /// Обрамотчик события изменения текущей вкладки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeSelectedTab(object sender, TabControlEventArgs e)
        {
            if (files.Count != 0)
            {
                // Ищим среди файлов тот, у которого свойство tabPage соответствует открытой вкладке.
                curFile = files.Find(x => x.TabPage == tabControl.SelectedTab);
                // Если мы меняем текущий файл, то меняем и настройки сохранений версий в соотвествии с настройками
                // для файла.
                if (curFile != null)
                {
                    loggingToolStripMenuItem.Checked = curFile.Log;
                }
                // Если нету открытого файла или был создан новый, то выключаем журналирование по умолчанию.
                else
                {
                    loggingToolStripMenuItem.Checked = false;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Закрыть вкладку".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void CloseCurrentTab(object sender, EventArgs e)
        {
            // Вызываем метод для того, чтобы понять что хочет делать с файлом, который будет закрыт
            DialogSaveToCloseCurrentTab(closeCurrentTabToolStripMenuItem, e);
            // Если файл есть и все необхоимые сохранения были выполнены.
            if (curFile != null && curFile.IsSaved)
            {
                // Чистим список открытых файлов и вкладок.
                files.Remove(curFile);
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                if (files.Count != 0)
                {
                    // Ищим среди файлов тот, у которого свойство tabPage соответствует открытой вкладке.
                    curFile = files.Find(x => x.TabPage == tabControl.SelectedTab);
                }
                else
                {
                    // Или присваиваем ему null в противном случае.
                    curFile = null;
                    // Если все закрыли, то можно убрать галочку на журналирование.
                    loggingToolStripMenuItem.Checked = false;
                }
            }
        }

        /// <summary>
        /// Метод для вызова диалогового окна с запросом, чтобы понять хочет пользователь закрыть ТЕКУЩИЙ файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void DialogSaveToCloseCurrentTab(object sender, EventArgs e)
        {
            if (curFile != null && !curFile.IsSaved)
            {
                DialogResult result = CloseDialogWindow($"Вы хотите сохранить" +
                    $" изменения в файле \"{curFile.FileName}\"?");
                switch (result)
                {
                    // Файл нужно сохранить.
                    case DialogResult.Yes:
                        SaveCurrentFile(sender, e);
                        return;
                    // Файл сохранять не нужно, следовательно, он итак сохранен с изначальным значением.
                    case DialogResult.No:
                        curFile.IsSaved = true;
                        return;
                    case DialogResult.Cancel:
                        return;
                }
            }
        }

        /// <summary>
        /// Всомогательный метод, который вызывает MessageBox конкретным текстом и выборо ответов: да, нет, отмена.
        /// </summary>
        /// <param name="message">Сообщение в MessageBox.</param>
        /// <returns>Выбранный пользователем ответ.</returns>
        private static DialogResult CloseDialogWindow(string message)
        {
            DialogResult result = MessageBox.Show(message,
                "Сообщение",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
                );
            return result;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Закрыть все".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void CloseAllTabs(object sender, EventArgs e)
        {
            // Сохранение несохраненных файлов или отмена закрытий.
            CloseAllTabsFunc(sender, e);
            // Если не остались несохраненные, значит пользователь не отменял никакую операцию сохранения.
            if (!files.Exists(x => x.IsSaved == false))
            {
                // Очиства открытых файлов и вкладок.
                files.Clear();
                tabControl.TabPages.Clear();
                curFile = null;
                // Если все закрыли, то можно убрать галочку на журналирование.
                loggingToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Вспомогательный метод для обрабокти ответа от пользователя касательно сохранения файлов при закрытии вкладок
        /// с последующим сохранением всех файлов окна, если надо.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void CloseAllTabsFunc(object sender, EventArgs e)
        {
            int needSave = -1;
            // Если есть несохраненные файлы запрос на то, что с ними делать.
            if (files.Exists(x => x.IsSaved == false))
            {
                switch (CloseDialogWindow($"Вы хотите сохранить изменения в файлах?"))
                {
                    case DialogResult.Yes:
                        needSave = 1;
                        break;
                    case DialogResult.No:
                        needSave = -1;
                        break;
                    case DialogResult.Cancel:
                        needSave = 0;
                        break;
                }
            }
            // Пользователь отменил опрецию закрытия.
            if (needSave == 0)
                return;
            // Идем по всем открытым и сохраняем изменения или не принимаем их.
            FileInUse[] copyFiles = new FileInUse[files.Count];
            files.CopyTo(copyFiles);
            foreach (var i in copyFiles)
            {
                curFile = i;
                if (needSave == -1)
                {
                    // Если сохранять файлы не надо, просто считаем, что они сохранены тк не нужно применять изменения.
                    curFile.IsSaved = true;
                }
                else
                {
                    // Если нужно сохранять файлы, то каждый будет сохраняться, но если он не сохранился,
                    // значит была отмена или ошибка и этот файл нельзя сохранить.
                    SaveCurrentFile(sender, e);
                    if (!curFile.IsSaved)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Функция для журналирования файлов после их сохранения.
        /// </summary>
        private void Logging()
        {
            // Проверка, что есть то, что сохранять и кнопка нажата.
            if (curFile != null && loggingToolStripMenuItem.Checked)
            {
                // Создаем путь к папке с логами для конкретного файла ..\NotePad+2.0\Logs\*fileName
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Logs", Path.GetFileName(curFile.FileName));
                if (!Directory.Exists(path))
                {
                    HideLogDirectory();
                    Directory.CreateDirectory(path);
                }
                // Хранятся только последние 3 версии. Удаляется самая старая.
                if (Directory.GetFiles(path).Length == 3)
                {
                    // Сортируем, так как они создаюся с именами, отличающимися только податам и врмени все норм.
                    Array.Sort(Directory.GetFiles(path));
                    File.Delete(Directory.GetFiles(path)[0]);
                }
                // Новый путь: ..\NotePad+2.0\Logs\fileName\fileName Date Time
                string newFile = Path.GetFileName(curFile.FileName) + " " + DateTime.Now.ToString().Replace(':', '-')
                    + Path.GetExtension(curFile.FileName);
                File.Copy(curFile.FileName, Path.Combine(path, newFile));
            }

        }

        /// <summary>
        /// Вспомогательный метод для того, чтобы если кто-то удалил папку Logs, то когда ее достроет метод Logging
        /// сделать сново скрытой.
        /// </summary>
        private static void HideLogDirectory()
        {
            string dirLogPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Logs");
            if (!Directory.Exists(dirLogPath))
            {
                DirectoryInfo dirLogInfo = Directory.CreateDirectory(dirLogPath);
                dirLogInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// Обработчик события закрытия окна.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void Form1Closing(Object sender, FormClosingEventArgs e)
        {
            // Закрываем все окна. Как нажатие на кнопки "Закрыть все".
            CloseAllTabsFunc(closeAllTabsToolStripMenuItem, e);
            // Если все вкладки закрыты, то есть все сохранились в нужном состоянии.
            if (!files.Exists(x => x.IsSaved == false))
            {
                // Смена родительской(главной) формы, если есть еще открытые формы, помимо текущей.
                if (MyForms.GetOpened(this) != null)
                {

                    // Меняем форму.
                    Program.s_context.MainForm = MyForms.GetOpened(this);
                    // Закрываем текущую.
                    MyForms.Close(this);
                    // Переключаемся на новую главную форму.
                    Program.s_context.MainForm.Focus();
                }
                else
                {
                    // Если текущая форма - последняя, то выполняем сохранение настроек.
                    SaveSettings();
                }
            }
            else
            {
                // Не со всеми файлами был решено вопрос о применении сохранений => форму не закрываем.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик нажатия на одного из элементов меню "0 секунд", "1 секунда", "5 секунд", "30 секунд", "60 секунд".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeIntervalTime(object sender, EventArgs e)
        {
            // Убираем галочки со всех пунтков.
            fiveSecInterval.Checked = false;
            zeroSecInterval.Checked = false;
            twoSecInterval.Checked = false;
            thirtySecInterval.Checked = false;
            sixtySecInterval.Checked = false;
            // Если нажали на "0 секунд", то нужно просто выключить сохранение.
            if (sender.ToString() == "0 секунд")
            {
                zeroSecInterval.Checked = true;
                timer.Enabled = false;
                return;
            }
            // Определяем контрол.
            ToolStripMenuItem chosen = (ToolStripMenuItem)sender;
            timer.Enabled = true;
            // Делаем его нажатым.
            chosen.Checked = true;
            // Установка промежутка времени, т.е. берем первое число из названия элемента меню, умноженное на 1000.
            timer.Interval = int.Parse(chosen.Text.Split()[0]) * 1000;
            return;

        }
        /// <summary>
        /// Обработчик действий на каждое срабатывание таймера.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void TimerTick(object sender, EventArgs e)
        {
            // Рботает только с неновыми файлами, чтобы не мешать вызовом диалогого окна
            if (curFile != null && !curFile.IsNew)
                SaveCurrentFile(sender, e);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "Цвет поля для ввода".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeTextBoxBackGround(object sender, EventArgs e)
        {
            // Если есть открытый файл, и пользователь не отменил выбор цвета.
            if (curFile != null && colorDialog.ShowDialog() == DialogResult.OK)
            {
                curFile.TextBox.BackColor = colorDialog.Color;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "Цвет текста в поле для ввода".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeTextColor(object sender, EventArgs e)
        {
            // Если есть открытый файл, и пользователь не отменил выбор цвета.
            if (curFile != null && colorDialog.ShowDialog() == DialogResult.OK)
                // Если ничего не выбрано, то смениться текст ввода просто.
                curFile.TextBox.SelectionColor = colorDialog.Color;
        }

        /// <summary>
        /// Обработчик событий "Скопировать" и "Вырезать".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void SetTextInClipBoard(object sender, EventArgs e)
        {
            if (curFile != null)
            {
                if (sender.ToString().Contains("Вырезать"))
                {
                    curFile.TextBox.Cut();
                }
                else
                {
                    Clipboard.SetDataObject(curFile.TextBox.SelectedText);
                }
            }
        }

        /// <summary>
        /// Обработчик события "Вставить".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void PasteTextFromClipBoard(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            // Проверка, что пользователь хочет вставить именно текст.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                curFile.TextBox.Paste();
            }
            else
            {
                MessageBox.Show("Вставить можно только текст!");
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Форматировать".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeSelectedTextFont(object sender, EventArgs e)
        {
            if (curFile != null && fontDialog.ShowDialog() == DialogResult.OK)
                curFile.TextBox.SelectionFont = fontDialog.Font;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Выбрать все".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void SelectAllText(object sender, EventArgs e)
        {
            curFile.TextBox.SelectAll();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Undo".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void UndoClick(object sender, EventArgs e)
        {
            if (curFile != null)
                curFile.TextBox.Undo();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Redo".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void RedoClick(object sender, EventArgs e)
        {
            if (curFile != null)
                curFile.TextBox.Redo();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Цвет окна приложения".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeBackColorOfTheForm(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Смена цвета окна.
                this.BackColor = colorDialog.Color;
                // Вызов метода, который красит все элементы в цвет окна.
                SetFormColor();
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Цвет текста окна приложения".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void ChangeForeColorOfTheForm(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Смена цвета окна.
                this.ForeColor = colorDialog.Color;
                // Вызов метода, который красит все элементы в цвет окна.
                SetFormColor();
            }
        }
        /// <summary>
        /// Вспомогательный метод для окраски всех элементов меню в цвет окна(и текст, и фон).
        /// </summary>
        private void SetFormColor()
        {
            for (int i = 0; i < menuStrip.Items.Count; i++)
                SetColor(menuStrip.Items[i] as ToolStripMenuItem, this.BackColor, this.ForeColor);
            menuStrip.BackColor = this.BackColor;
            menuStrip.ForeColor = this.ForeColor;
        }
        /// <summary>
        /// Рекурсивный метод обхода всех элементов меню, начиная с item.
        /// </summary>
        /// <param name="item">Элемент, который красят и у которого смотреть подразделы.</param>
        /// <param name="newBackColor">Новый цвет фона.</param>
        /// <param name="newForeColor">Новый цвет текста.</param>
        private void SetColor(ToolStripMenuItem item, Color newBackColor, Color newForeColor)
        {
            item.BackColor = newBackColor;
            item.ForeColor = newForeColor;
            foreach (ToolStripMenuItem curItem in item.DropDownItems)
                SetColor(curItem, newBackColor, newForeColor);
        }
        /// <summary>
        /// Обработчик нажатия на кнопку "Закрыть все".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void FormHotKeyToClose(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить все".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void SaveAllFiles(object sender, EventArgs e)
        {
            // Запоминаем текущий, чтобы потом снова на него "переключиться".
            FileInUse rememberFile = curFile;
            // Обход всех файлов с их сохранением.
            foreach (var file in files)
            {
                curFile = file;
                if (!curFile.IsSaved)
                {
                    SaveCurrentFile(saveToolStripMenuItem, e);
                }
            }
            curFile = rememberFile;
        }
        /// <summary>
        /// Метод для сохранения настроек, установленных в последнем окне и у последней открытой вкладки,
        /// если такая была.
        /// </summary>
        private void SaveSettings()
        {
            // Собираем список файлов, которые были открыт в последнем окне.
            List<string> addition = new();
            foreach (var i in files)
            {
                // Сохраняем неновые файлы, которые были открыты.
                if (!i.IsNew)
                {
                    addition.Add(i.FileName);
                }
                // Сохраняем настройки каждой открытой вкладки, чтобы остались настройки только последней.
                Properties.Settings.Default.textBoxBack = i.TextBox.BackColor;
                i.TextBox.Select(0, 1);
                Properties.Settings.Default.textInBoxColor = i.TextBox.SelectionColor;
                Properties.Settings.Default.textBoxFont = i.TextBox.SelectionFont;
                Properties.Settings.Default.logging = i.Log;
            }
            // Сохраняем цветовые настройки формы.
            Properties.Settings.Default.formTextColor = this.ForeColor;
            Properties.Settings.Default.formBack = this.BackColor;
            // Добавляем список открытых, сохраненных файлов.
            Properties.Settings.Default.files.AddRange(addition.ToArray());
            // Сохраняем статус таймера.
            Properties.Settings.Default.timerIntervalFlag = timer.Enabled;
            Properties.Settings.Default.timerIntervalVal = timer.Interval;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Метод для восстановления настроек, оставшихся после закрытия.
        /// </summary>
        private void SetSettingss()
        {
            foreach (var file in Properties.Settings.Default.files)
            {
                // Пробуем открыть файлы, которые были открыты при закрытии.
                if (ShowText(file, out string text))
                {
                    // Отркываем в отдельной вкладке.
                    CreateTheTab(this, text, file, false);
                    // Устанавливаем настройки RichTextBox'a.
                    curFile.TextBox.BackColor = Properties.Settings.Default.textBoxBack;
                    curFile.TextBox.SelectAll();
                    curFile.TextBox.SelectionColor = Properties.Settings.Default.textInBoxColor;
                    curFile.TextBox.SelectionFont = Properties.Settings.Default.textBoxFont;
                    curFile.IsSaved = true;
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.Log = Properties.Settings.Default.logging;
                    loggingToolStripMenuItem.Checked = Properties.Settings.Default.logging;
                }
                else
                {
                    MessageBox.Show(text);
                }
            }
            // Настройки цвета окна и текста, установка параметров таймера.
            this.BackColor = Properties.Settings.Default.formBack;
            this.ForeColor = Properties.Settings.Default.formTextColor;
            timer.Enabled = Properties.Settings.Default.timerIntervalFlag;
            timer.Interval = Properties.Settings.Default.timerIntervalVal;
            // Перекрашиваем меню.
            SetCheckedInterval();
            // Ставим галочку у промежутка времени нужного.
            SetFormColor();
            Properties.Settings.Default.files.Clear();
        }

        /// <summary>
        /// Вспомогательный метод, чтобы определить, по заданному интервалу времени таймера, у какого элемента
        /// меню с выбором периодичности сохранения нужно поставить галочку.
        /// </summary>
        private void SetCheckedInterval()
        {
            if (timer.Enabled)
            {
                foreach (var i in new List<ToolStripMenuItem>() {zeroSecInterval, twoSecInterval, fiveSecInterval,
                                                                  thirtySecInterval, sixtySecInterval})
                {
                    // Берем число из названия элемента управления и проверяем на соответствие заданному интервалу.
                    i.Checked = i.Text.Split()[0] == (timer.Interval / 1000).ToString();
                }
            }
        }
        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранять предыдущую версию".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Параметры события.</param>
        private void LogFilesSwitch(object sender, EventArgs e)
        {
            if (curFile != null && !curFile.IsNew)
            {
                // Если есть открытый неновый файл, то меняем "переключатель" на журналирование.
                loggingToolStripMenuItem.Checked = !loggingToolStripMenuItem.Checked;
                // Меняем то же самое у открытого файла
                curFile.Log = !curFile.Log;
            }
        }
    }
}
